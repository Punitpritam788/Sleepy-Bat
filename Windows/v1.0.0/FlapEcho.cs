// Unity 6.x -- real-scene version of FlapEcho.cs. Same GameObject-attach
// workflow (drop this on any empty GameObject in an empty Scene) but this
// one builds an actual GameObject hierarchy at runtime instead of drawing
// with OnGUI: a Camera, SpriteRenderers for background/ground/top/bird/
// pillars, a ParticleSystem for the death burst, and a Canvas+Text HUD.
// Everything shows up in the Hierarchy once you press Play, renders
// through Unity's normal sprite pipeline (proper batching/culling), and
// is the kind of structure a build/export pipeline expects -- unlike
// OnGUI, which only exists in the Game view during Play and isn't part
// of the real rendering path.
//
// Art fields are now Sprite[] instead of Texture2D[] (SpriteRenderer
// needs Sprites). If your images are already imported as "Sprite (2D and
// UI)" -- which this project's images already are -- Unity auto-generates
// the Sprite for you; just drag the same files into the new slots.
//
// World-space convention: 1 Unity unit = 1 pixel. The camera's
// orthographic size is set to Screen.height/2 every frame, which makes
// the visible world area exactly Screen.width x Screen.height regardless
// of aspect ratio -- so every existing pixel-based value (gravity, sizes,
// speeds, the Scale ratio) ports over unchanged, just converted to world
// position via ScreenToWorld() (simple arithmetic, no Camera API calls).
//
// Tiling (background/ground/top/pillar body) is done the same way as the
// OnGUI version conceptually, just via material.mainTextureScale/Offset
// on a SpriteRenderer instead of GUI.DrawTextureWithTexCoords -- same
// wrapMode.Repeat requirement on the source texture.
//
// Ground/top collision, echolocation, difficulty curves, taunts, save
// data, dev F9 reset, screen-size scaling, safe-area insets -- all
// identical logic to FlapEcho.cs. Only rendering changed.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlapScene : MonoBehaviour
{
    class Pillar { public float x, w, center, gap; public bool passed, shifted, shifted2; }
    class PillarView
    {
        public Transform root;
        public SpriteRenderer topBody, topCap, botBody, botCap;
    }

    const float GRAVITY = 1400f, FLAP_V = -430f, HIT_PAD = 4f, BIRD_R = 14f;
    const int PROFILE_RES = 128;
    const int PILLAR_POOL = 8; // generous headroom over what's ever simultaneously on screen

    static readonly string[] PILLAR_TAUNTS = { "missed it by THAT much", "the gap moved. definitely.", "that felt rigged. it was.", "so close, right?" };
    static readonly string[] FALL_TAUNTS = { "gravity said no", "skill issue", "you flinched.", "physics is fake" };

    const float REFERENCE_HEIGHT = 900f;
    float Scale => Mathf.Clamp(Screen.height / REFERENCE_HEIGHT, 0.6f, 2.2f);
    float BirdR => BIRD_R * Scale;
    float Pad => HIT_PAD * Scale;

    const float BACK_DOUBLE_WINDOW = 0.8f;
    float lastBackPressTime = -10f;

    Vector2 birdPos;
    float birdVy;
    List<Pillar> pillars = new List<Pillar>();

    int score, best, deaths;
    bool running, dead;
    bool pausedForExit; // back/escape opens the pause/exit screen; resume starts a fresh run
    float shake;
    float gameTime, nextGlitch, glitchUntil, scrollTime;
    bool glitchInvert, trollBlock, echoVisible = true;
    string deathMsg = "";

    float[] groundProfile, topProfile;
    Texture2D groundProfileTex, topProfileTex;

    [Header("Artwork (Sprites now, not Texture2D -- SpriteRenderer needs Sprites)")]
    [Tooltip("1 image = static. 2+ images = looping animation at Animation Fps.")]
    public Sprite[] backgroundFrames;
    public Sprite[] birdFrames;
    public Sprite deathFrame; // shown instead of Bird Frames once dead -- optional
    public Sprite[] pillarFrames;
    public Sprite[] pillarCapFrames;
    public float animationFps = 8f;

    [Header("Pillar sizing")]
    public float pillarBodyTileHeight = 890f;
    public float pillarCapHeight = 40f;
    [Tooltip("How far the cap sinks into the pillar body, in reference pixels. This removes visible seams caused by transparent padding in the cap artwork.")]
    public float pillarCapOverlap = 8f;

    [Header("Ground & top layers (optional -- skipped entirely if empty)")]
    public Sprite[] groundFrames;
    public float groundHeight = 120f; // taller than a flat "strip" so the rock peaks have room to read as solid mass, not a thin sliver
    public Sprite[] topFrames;
    public float topHeight = 120f;

    [Header("Parallax scroll speed (px/s at Scale = 1)")]
    public float backgroundScrollSpeed = 40f;
    public float groundScrollSpeed = 220f;
    public float topScrollSpeed = 90f;

    // ---------- scene objects (created at runtime, visible in Hierarchy once Play starts) ----------
    Camera cam;
    Transform worldRoot; // shake moves this camera's parent-relative offset, not individual sprites
    SpriteRenderer backgroundSrA, backgroundSrB, groundSrA, groundSrB, topSrA, topSrB, glitchSr, birdSr;
    ParticleSystem burstFx;
    List<PillarView> pillarPool = new List<PillarView>();
    Sprite whitePixel;

    Canvas canvas;
    Text scoreText, bestText, deathsText, warnText, echoText, titleText, subText;

    AudioSource[] voices = new AudioSource[4];
    int nextVoice;
    Dictionary<string, AudioClip> sfx = new Dictionary<string, AudioClip>();

    void Start()
    {
        Application.targetFrameRate = 60;
        Random.InitState(System.Environment.TickCount);
        best = PlayerPrefs.GetInt("flap_best", 0);
        deaths = PlayerPrefs.GetInt("flap_deaths", 0);

        // set BEFORE any scene/canvas construction that could throw, so a
        // failure downstream can never again leave the bird stuck at the
        // default (0,0) position -- this exact failure shape (one Start()
        // exception cascading and skipping everything after it) already
        // bit FlapEcho.cs once, via GUI.skin.
        birdPos.x = Mathf.Max(80f * Scale, Screen.width * 0.27f);

        try { BuildScene(); }
        catch (System.Exception e) { Debug.LogError("[FlapScene] BuildScene failed: " + e); }

        try { BuildAudio(); }
        catch (System.Exception e) { Debug.LogError("[FlapScene] BuildAudio failed: " + e); }

        BuildCollisionProfiles(); // already self-guarded internally

        nextGlitch = 5f + Random.value * 4f;
        ResetGame();
    }

    // ---------- scene construction ----------

    void BuildScene()
    {
        cam = Camera.main;
        if (cam == null)
        {
            var camGO = new GameObject("Main Camera");
            camGO.tag = "MainCamera";
            cam = camGO.AddComponent<Camera>();
        }
        cam.orthographic = true;
        cam.backgroundColor = new Color(0.106f, 0.122f, 0.231f);
        cam.clearFlags = CameraClearFlags.SolidColor;

        var texPixel = new Texture2D(1, 1);
        texPixel.SetPixel(0, 0, Color.white);
        texPixel.Apply();
        whitePixel = Sprite.Create(texPixel, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);

        backgroundSrA = MakeLayer("Background_A", -100);
        backgroundSrB = MakeLayer("Background_B", -100);
        glitchSr = MakeLayer("GlitchOverlay", -90);
        glitchSr.sprite = whitePixel;
        glitchSr.enabled = false;
        groundSrA = MakeLayer("Ground_A", -40);
        groundSrB = MakeLayer("Ground_B", -40);
        topSrA = MakeLayer("Top_A", -40);
        topSrB = MakeLayer("Top_B", -40);
        birdSr = MakeLayer("Bird", -10);

        for (int i = 0; i < PILLAR_POOL; i++)
        {
            var root = new GameObject("Pillar_" + i).transform;
            root.SetParent(transform, false);
            var view = new PillarView
            {
                root = root,
                topBody = MakeChildSprite(root, "TopBody", -50),
                topCap = MakeChildSprite(root, "TopCap", -49),
                botBody = MakeChildSprite(root, "BotBody", -50),
                botCap = MakeChildSprite(root, "BotCap", -49)
            };
            root.gameObject.SetActive(false);
            pillarPool.Add(view);
        }

        var fxGO = new GameObject("DeathBurst");
        fxGO.transform.SetParent(transform, false);
        burstFx = fxGO.AddComponent<ParticleSystem>();
        var main = burstFx.main;
        main.loop = false;
        main.startLifetime = 0.6f;
        main.startSpeed = 4f;
        main.startSize = 0.15f;
        main.startColor = Color.white;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        var emission = burstFx.emission;
        emission.enabled = false; // fired manually via Emit()
        var shapeMod = burstFx.shape;
        shapeMod.shapeType = ParticleSystemShapeType.Sphere;
        shapeMod.radius = 0.01f;
        var vol = burstFx.velocityOverLifetime;
        vol.enabled = true;
        vol.y = new ParticleSystem.MinMaxCurve(-9f, -2f); // gravity-ish pull
        var pr = burstFx.GetComponent<ParticleSystemRenderer>();
        pr.sortingOrder = 0; // uses its own default material -- no tiling needed here

        BuildCanvas();
    }

    SpriteRenderer MakeLayer(string name, int order)
    {
        var go = new GameObject(name);
        go.transform.SetParent(transform, false);
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sortingOrder = order;
        // no manual Material construction -- SetVerticalTile accesses
        // access sr.material (not sharedMaterial) when they actually need
        // to set tiling properties, which safely auto-clones on first use
        // regardless of render pipeline (Built-in, URP, etc.)
        return sr;
    }

    SpriteRenderer MakeChildSprite(Transform parent, string name, int order)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sortingOrder = order;
        return sr;
    }

    void BuildCanvas()
    {
        var canvasGO = new GameObject("HUD Canvas");
        canvasGO.transform.SetParent(transform, false);
        canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize; // 1 UI unit = 1 screen px, matches our existing pixel math
        canvasGO.AddComponent<GraphicRaycaster>();

        // "Arial.ttf" was Unity's builtin-font resource name pre-2022.2;
        // it's "LegacyRuntime.ttf" since. Try the current name first, fall
        // back to the old one so this still compiles/runs on either.
        var font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        if (font == null) font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        if (font == null) Debug.LogWarning("[FlapScene] no builtin font found -- HUD text will render without an assigned font.");

        scoreText = MakeText("Score", font, TextAnchor.UpperLeft, new Vector2(0, 1));
        bestText = MakeText("Best", font, TextAnchor.UpperLeft, new Vector2(0, 1));
        deathsText = MakeText("Deaths", font, TextAnchor.UpperLeft, new Vector2(0, 1));
        warnText = MakeText("?!", font, TextAnchor.UpperRight, new Vector2(1, 1));
        echoText = MakeText("((*))", font, TextAnchor.UpperCenter, new Vector2(0.5f, 1));
        titleText = MakeText("FLAP", font, TextAnchor.MiddleCenter, new Vector2(0.5f, 0.5f));
        subText = MakeText("", font, TextAnchor.MiddleCenter, new Vector2(0.5f, 0.5f));
    }

    Text MakeText(string label, Font font, TextAnchor anchor, Vector2 anchorPoint)
    {
        var go = new GameObject(label);
        go.transform.SetParent(canvas.transform, false);
        var txt = go.AddComponent<Text>();
        txt.font = font;
        txt.alignment = anchor;
        txt.color = Color.white;
        txt.horizontalOverflow = HorizontalWrapMode.Overflow;
        txt.verticalOverflow = VerticalWrapMode.Overflow;
        var rt = txt.rectTransform;
        rt.anchorMin = rt.anchorMax = anchorPoint;
        rt.pivot = anchorPoint;
        rt.sizeDelta = new Vector2(400, 80);
        return txt;
    }

    void BuildAudio()
    {
        for (int i = 0; i < voices.Length; i++) voices[i] = gameObject.AddComponent<AudioSource>();
        sfx["flap"] = MakeTone(300f, 0.06f, "tri");
        sfx["flap_glitch"] = MakeTone(180f, 0.06f, "tri");
        sfx["pass"] = MakeTone(540f, 0.08f, "sine");
        sfx["die"] = MakeTone(140f, 0.3f, "saw");
        sfx["glitch_warn"] = MakeTone(90f, 0.25f, "saw");
        sfx["troll"] = MakeTone(80f, 0.15f, "square");
        sfx["sleepy"] = MakeTone(220f, 0.6f, "sine");
    }

    AudioClip MakeTone(float freq, float dur, string wave)
    {
        int rate = 22050;
        int n = Mathf.CeilToInt(rate * dur);
        var data = new float[n];
        for (int i = 0; i < n; i++)
        {
            float t = (float)i / rate;
            float v;
            switch (wave)
            {
                case "sine": v = Mathf.Sin(2f * Mathf.PI * freq * t); break;
                case "square": v = Mathf.Sign(Mathf.Sin(2f * Mathf.PI * freq * t)); break;
                case "saw": v = 2f * (freq * t - Mathf.Floor(freq * t)) - 1f; break;
                default: v = 2f * Mathf.Abs(2f * (freq * t + 0.25f - Mathf.Floor(freq * t + 0.25f)) - 1f) - 1f; break;
            }
            data[i] = v * 0.2f * (1f - t / dur);
        }
        var clip = AudioClip.Create("tone_" + freq, n, 1, rate, false);
        clip.SetData(data, 0);
        return clip;
    }

    void PlaySfx(string name)
    {
        var src = voices[nextVoice];
        nextVoice = (nextVoice + 1) % voices.Length;
        src.clip = sfx[name];
        src.Play();
    }

    // ---------- ground/top collision profiles (same technique as FlapEcho.cs) ----------

    void BuildCollisionProfiles()
    {
        try
        {
            if (groundFrames != null && groundFrames.Length > 0 && groundFrames[0] != null)
            {
                groundProfileTex = groundFrames[0].texture;
                groundProfile = BuildHeightProfile(groundProfileTex, topmost: true);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("[FlapScene] ground collision profile failed -- enable 'Read/Write Enabled' on the texture. Falling back to a flat floor. (" + e.Message + ")");
            groundProfile = null;
        }
        try
        {
            if (topFrames != null && topFrames.Length > 0 && topFrames[0] != null)
            {
                topProfileTex = topFrames[0].texture;
                topProfile = BuildHeightProfile(topProfileTex, topmost: false);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("[FlapScene] top collision profile failed -- enable 'Read/Write Enabled' on the texture. Falling back to a flat ceiling. (" + e.Message + ")");
            topProfile = null;
        }
    }

    float[] BuildHeightProfile(Texture2D tex, bool topmost)
    {
        var pixels = tex.GetPixels32();
        int tw = tex.width, th = tex.height;
        var profile = new float[PROFILE_RES];
        for (int i = 0; i < PROFILE_RES; i++)
        {
            int x = Mathf.Clamp(Mathf.RoundToInt(i / (float)(PROFILE_RES - 1) * (tw - 1)), 0, tw - 1);
            bool any = false; int foundY = 0;
            if (topmost)
            {
                for (int y = 0; y < th; y++) if (pixels[y * tw + x].a > 25) { foundY = y; any = true; break; }
                profile[i] = any ? foundY / (float)(th - 1) : 1f;
            }
            else
            {
                for (int y = th - 1; y >= 0; y--) if (pixels[y * tw + x].a > 25) { foundY = y; any = true; break; }
                profile[i] = any ? foundY / (float)(th - 1) : 0f;
            }
        }
        return profile;
    }

    float SampleProfile(float[] profile, Texture2D tex, float scrollSpeed, float atX)
    {
        if (profile == null || tex == null) return -1f;
        float tileW = tex.width * Scale;
        if (tileW <= 0f) return -1f;
        float uOffset = (gameTime * scrollSpeed * Scale / tileW) % 1f;
        float u = (uOffset + atX / tileW) % 1f;
        if (u < 0f) u += 1f;
        int idx = Mathf.Clamp(Mathf.FloorToInt(u * profile.Length), 0, profile.Length - 1);
        return profile[idx];
    }

    // ---------- world-space helpers ----------
    // 1 world unit = 1 pixel: camera orthographicSize = Screen.height/2
    // every frame makes the visible world area == Screen.width x
    // Screen.height exactly, at any aspect ratio.

    Vector3 ScreenToWorld(float sx, float sy) => new Vector3(sx - Screen.width / 2f, Screen.height / 2f - sy, 0f);

    // stretches a sprite's transform.localScale (PPU-aware) so it renders
    // at exactly worldW x worldH pixels, regardless of the asset's own
    // Pixels Per Unit import setting.
    void SetSimpleSize(SpriteRenderer sr, float worldW, float worldH)
    {
        if (sr.sprite == null) return;
        float natW = sr.sprite.rect.width / sr.sprite.pixelsPerUnit;
        float natH = sr.sprite.rect.height / sr.sprite.pixelsPerUnit;
        sr.transform.localScale = new Vector3(natW > 0f ? worldW / natW : 1f, natH > 0f ? worldH / natH : 1f, 1f);
    }

    // horizontal scrolling loop (background/ground/top): tiles at the
    // image's own native width (scaled) and scrolls continuously.
    // horizontal scrolling loop, done with plain transform.position instead
    // of material UV offsets (which turned out not to reliably affect the
    // render -- likely a render-pipeline/atlas interaction). Two full-width
    // copies of the same sprite slide left together; xa cycles through
    // (-worldW, 0] via modulo, and panel B (always worldW to A's right)
    // seamlessly covers the screen as A exits -- the classic two-panel
    // conveyor-belt loop. time is scrollTime for these three layers, so
    // they freeze together the instant the bird dies.
    void SyncScrollPair(SpriteRenderer a, SpriteRenderer b, Sprite[] frames, float worldW, float worldH, float centerScreenY, float scrollSpeed, float time)
    {
        Sprite spr = CurrentFrame(frames, time);
        if (spr == null) { a.enabled = false; b.enabled = false; return; }
        a.enabled = true; b.enabled = true;
        a.sprite = spr; b.sprite = spr;
        SetSimpleSize(a, worldW, worldH);
        SetSimpleSize(b, worldW, worldH);

        float travel = time * scrollSpeed * Scale;
        float xa = worldW > 0f ? -(travel % worldW) : 0f;
        a.transform.position = ScreenToWorld(xa + worldW / 2f, centerScreenY);
        b.transform.position = ScreenToWorld(xa + worldW * 1.5f, centerScreenY);
    }

    Sprite CurrentFrame(Sprite[] frames, float time)
    {
        if (frames == null || frames.Length == 0) return null;
        if (frames.Length == 1) return frames[0];
        int i = Mathf.FloorToInt(time * animationFps) % frames.Length;
        return frames[i];
    }

    // ---------- tuning ----------

    float GapFor(int s) => Mathf.Max(120f, 190f - s * 2f) * Scale;
    float SpeedFor(int s) => (190f + Mathf.Min(s, 60) * 1.6f) * Scale;
    float ClampC(float c, float gap) => Mathf.Clamp(c, gap / 2f + 20f * Scale, Screen.height - gap / 2f - 20f * Scale);
    float FlyThreshold(int s) => Mathf.Lerp(0f, 260f, Mathf.Clamp01(s / 300f)) * Scale;

    // ---------- state ----------

    void ResetGame()
    {
        pausedForExit = false;
        birdPos.y = Screen.height * 0.45f;
        birdVy = 0f;
        pillars.Clear();
        score = 0; dead = false; deathMsg = ""; shake = 0f;
        echoVisible = true;
    }

    void ResetSavedStats()
    {
        PlayerPrefs.DeleteKey("flap_best");
        PlayerPrefs.DeleteKey("flap_deaths");
        best = 0; deaths = 0;
        Debug.Log("[FlapScene] best/deaths reset");
    }

    void SpawnNext(Pillar prev)
    {
        bool joined = prev != null && Random.value < 0.4f;
        float spacing = (joined ? 130f + Random.value * 40f : 170f + Random.value * 110f) * Scale;
        float x = prev != null ? prev.x + spacing : Screen.width + 100f * Scale;
        float gap = GapFor(score);
        float center = Screen.height * 0.2f + Random.value * Screen.height * 0.6f;
        if (joined)
        {
            center = prev.center + (Random.value < 0.5f ? -1f : 1f) * (40f + Random.value * 50f) * Scale;
            gap = Mathf.Max(95f * Scale, gap - 35f * Scale);
        }
        center = ClampC(center, gap);
        pillars.Add(new Pillar { x = x, w = 60f * Scale, center = center, gap = gap });
    }

    void MaybeSpawn()
    {
        Pillar last = pillars.Count > 0 ? pillars[pillars.Count - 1] : null;
        if (last == null || last.x < Screen.width) SpawnNext(last);
    }

    // ---------- pause / exit controls ----------

    void HandleBackOrEscape()
    {
        float now = Time.unscaledTime;

        if (pausedForExit)
        {
            // Two back/escape presses in quick succession exit the game.
            if (now - lastBackPressTime <= BACK_DOUBLE_WINDOW)
            {
                QuitGame();
                return;
            }

            // Still paused; this becomes the first half of another double-back.
            lastBackPressTime = now;
            return;
        }

        // Pause an active run. Android's system Back button/back gesture is
        // handled here through the same Escape input path.
        if (running && !dead)
        {
            pausedForExit = true;
            running = false;
            lastBackPressTime = now;
            return;
        }

        // On the title/game-over screen there is nothing to pause, so Back/Escape
        // simply exits the application.
        QuitGame();
    }

    void RestartFromPause()
    {
        // "Resume" intentionally starts a completely fresh run.
        gameTime = 0f;
        scrollTime = 0f;
        glitchUntil = 0f;
        nextGlitch = 5f + Random.value * 4f;
        glitchInvert = false;
        trollBlock = false;
        ResetGame();
        running = true;
        pausedForExit = false;
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void FlapBird()
    {
        if (dead)
        {
            if (trollBlock)
            {
                trollBlock = false;
                deathMsg = "LOL NO";
                shake = 0.6f;
                PlaySfx("troll");
                return;
            }
            ResetGame(); running = true; return;
        }
        if (!running) { ResetGame(); running = true; }
        bool glitched = gameTime < glitchUntil;
        birdVy = (glitched ? (glitchInvert ? Mathf.Abs(FLAP_V) * 0.5f : FLAP_V * 0.35f) : FLAP_V) * Scale;
        PlaySfx(glitched ? "flap_glitch" : "flap");
    }

    void Die(string cause)
    {
        if (dead) return;
        dead = true; running = false;
        deaths++;
        PlayerPrefs.SetInt("flap_deaths", deaths);
        shake = 1f;
        trollBlock = Random.value < 0.3f;
        burstFx.transform.position = ScreenToWorld(birdPos.x, birdPos.y);
        burstFx.Emit(24);
        PlaySfx(cause == "cap" ? "sleepy" : "die");

        if (cause == "cap")
            deathMsg = "999. THAT'S IT. THAT'S ALL YOU GET.\ni just wanna sleep leave me alone";
        else
        {
            var pool = cause == "pillar" ? PILLAR_TAUNTS : FALL_TAUNTS;
            deathMsg = pool[Random.Range(0, pool.Length)];
        }
        deathMsg += "  [death #" + deaths + "]";

        bool newBest = score > best;
        if (newBest && Random.value < 0.3f) deathMsg += " (NEW BEST? ...jk, not saving that)";
        else
        {
            best = Mathf.Max(best, score);
            PlayerPrefs.SetInt("flap_best", best);
            if (newBest) deathMsg += " (new best)";
        }
        PlayerPrefs.Save();
    }

    // ---------- loop ----------

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F9)) ResetSavedStats();
#endif

        float dt = Time.deltaTime;

        // Escape on Windows and Android's Back button/back gesture are both
        // surfaced through KeyCode.Escape here. A second back while the
        // pause/exit screen is open exits the game.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleBackOrEscape();
        }

        // While paused, the whole game state and animation clocks are frozen.
        // Any normal flap/tap input acts as "resume", but intentionally starts
        // a brand-new run instead of continuing the old one.
        if (pausedForExit)
        {
            bool resumePressed = Input.GetKeyDown(KeyCode.Space)
                || Input.GetKeyDown(KeyCode.UpArrow)
                || Input.GetMouseButtonDown(0)
                || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);

            if (resumePressed) RestartFromPause();

            SyncVisuals();
            return;
        }

        gameTime += dt;
        if (!dead) scrollTime = gameTime;

        if (shake > 0f) shake = Mathf.Max(0f, shake - dt * 3f);

        bool flapPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetMouseButtonDown(0)
            || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
        if (flapPressed) FlapBird();

        if (running && !dead)
        {
            if (gameTime > nextGlitch)
            {
                glitchUntil = gameTime + 1.2f;
                glitchInvert = Random.value < 0.5f;
                nextGlitch = gameTime + 6f + Random.value * 6f;
                PlaySfx("glitch_warn");
            }

            birdVy += GRAVITY * Scale * dt;
            birdPos.y += birdVy * dt;
            echoVisible = birdVy >= FlyThreshold(score);

            float speed = SpeedFor(score);
            foreach (var p in pillars) p.x -= speed * dt;
            while (pillars.Count > 0 && pillars[0].x < birdPos.x - 200f * Scale) pillars.RemoveAt(0);
            MaybeSpawn();

            foreach (var p in pillars)
            {
                if (dead) break;
                float dx = p.x - birdPos.x;
                if (!p.shifted && dx < 140f * Scale && dx > 0f)
                {
                    p.shifted = true;
                    if (Random.value < 0.5f)
                        p.center = ClampC(p.center + (Random.value < 0.5f ? -1f : 1f) * (40f + Random.value * 35f) * Scale, p.gap);
                }
                if (!p.shifted2 && dx < 55f * Scale && dx > 0f)
                {
                    p.shifted2 = true;
                    if (Random.value < 0.4f)
                        p.center = ClampC(p.center + (Random.value < 0.5f ? -1f : 1f) * (18f + Random.value * 20f) * Scale, p.gap);
                }
                if (!p.passed && p.x + p.w / 2f < birdPos.x)
                {
                    p.passed = true; score++; PlaySfx("pass");
                    if (score >= 999) { Die("cap"); break; }
                }
                if (dead) break;

                bool hitX = Mathf.Abs(p.x - birdPos.x) < p.w / 2f + BirdR + Pad;
                float top = p.center - p.gap / 2f, bottom = p.center + p.gap / 2f;
                if (hitX && (birdPos.y - BirdR - Pad < top || birdPos.y + BirdR + Pad > bottom)) { Die("pillar"); break; }
            }

            float groundF = SampleProfile(groundProfile, groundProfileTex, groundScrollSpeed, birdPos.x);
            float floorY = groundF >= 0f
                ? (Screen.height - groundHeight * Scale) + groundF * (groundHeight * Scale)
                : ((groundFrames != null && groundFrames.Length > 0) ? Screen.height - groundHeight * Scale : Screen.height);
            float topF = SampleProfile(topProfile, topProfileTex, topScrollSpeed, birdPos.x);
            float ceilY = topF >= 0f ? topF * (topHeight * Scale)
                : ((topFrames != null && topFrames.Length > 0) ? topHeight * Scale : 0f);

            if (!dead && (birdPos.y + BirdR + Pad > floorY || birdPos.y - BirdR - Pad < ceilY))
                Die("fall");
        }

        SyncVisuals();
    }

    // ---------- visual sync: mirrors OnGUI's draw calls, but positions real GameObjects ----------

    void SyncVisuals()
    {
        float w = Screen.width, h = Screen.height, s = Scale;
        // Freeze pillar/cap animation on the exact visual frame where the bat dies.
        // scrollTime stops advancing while dead, so it is the frozen animation clock.
        float visualTime = dead ? scrollTime : gameTime;

        cam.orthographicSize = h / 2f;
        Vector3 shakeOffset = shake > 0f
            ? new Vector3((Random.value - 0.5f) * shake * 14f * s, (Random.value - 0.5f) * shake * 14f * s, -10f)
            : new Vector3(0, 0, -10f);
        cam.transform.position = shakeOffset;

        SyncScrollPair(backgroundSrA, backgroundSrB, backgroundFrames, w, h, h / 2f, backgroundScrollSpeed, scrollTime);

        glitchSr.enabled = !pausedForExit && gameTime < glitchUntil;
        if (glitchSr.enabled)
        {
            SetSimpleSize(glitchSr, w, h);
            glitchSr.transform.position = ScreenToWorld(w / 2f, h / 2f);
            glitchSr.color = new Color(1f, 0.08f, 0.235f, 0.12f);
        }

        bool hasCap = pillarCapFrames != null && pillarCapFrames.Length > 0 && pillarCapFrames[0] != null;
        float capH = pillarCapHeight * s;
        float pulse = 0.55f + 0.45f * Mathf.Sin(visualTime * 18f);
        Color pulseColor = new Color(1f, 1f, 1f, pulse);

        for (int i = 0; i < pillarPool.Count; i++)
        {
            var view = pillarPool[i];
            if (i >= pillars.Count) { view.root.gameObject.SetActive(false); continue; }
            view.root.gameObject.SetActive(echoVisible);
            if (!echoVisible) continue;

            var p = pillars[i];
            float top = p.center - p.gap / 2f, bottom = p.center + p.gap / 2f;
            float topCapH = hasCap ? Mathf.Min(capH, top) : 0f;
            float botCapH = hasCap ? Mathf.Min(capH, h - bottom) : 0f;

            // body's far end follows the ground/top rock silhouette at
            // THIS pillar's own X, not the literal screen edge -- same
            // profile collision uses, just sampled at a different X. This
            // is what actually prevents the "floating" look: the pillar's
            // edge and the rock surface are now the same number by
            // construction, not two independent guesses that happen to
            // (dis)agree.
            float groundFHere = SampleProfile(groundProfile, groundProfileTex, groundScrollSpeed, p.x);
            float pillarFloorY = groundFHere >= 0f
                ? Mathf.Max(bottom, (h - groundHeight * s) + groundFHere * (groundHeight * s))
                : h;
            float topFHere = SampleProfile(topProfile, topProfileTex, topScrollSpeed, p.x);
            float pillarCeilY = topFHere >= 0f
                ? Mathf.Min(top, topFHere * (topHeight * s))
                : 0f;

            // The body runs all the way to the gap edge. The cap is then
            // layered on top of the body and deliberately overlaps it.
            // This guarantees there is no visible seam/gap under the cap.
            float topBodyH = Mathf.Max(0.01f, top - pillarCeilY);
            float botBodyH = Mathf.Max(0.01f, pillarFloorY - bottom);

            Sprite bodySpr = CurrentFrame(pillarFrames, visualTime);
            view.topBody.sprite = bodySpr != null ? bodySpr : whitePixel;
            view.topBody.color = bodySpr != null ? pulseColor : new Color(0.62f, 0.32f, 1f, pulse);
            SetSimpleSize(view.topBody, p.w, topBodyH);
            view.topBody.transform.position = ScreenToWorld(p.x, pillarCeilY + topBodyH / 2f);
            // no vertical tiling here anymore -- same material-property
            // unreliability as the old scroll bug. plain stretch-fill
            // until a dynamic multi-panel version is worth building.

            view.botBody.sprite = view.topBody.sprite;
            view.botBody.color = view.topBody.color;
            SetSimpleSize(view.botBody, p.w, botBodyH);
            view.botBody.transform.position = ScreenToWorld(p.x, bottom + botBodyH / 2f);

            view.topCap.gameObject.SetActive(hasCap);
            view.botCap.gameObject.SetActive(hasCap);
            if (hasCap)
            {
                Sprite capSpr = CurrentFrame(pillarCapFrames, visualTime);
                view.topCap.sprite = capSpr;
                view.topCap.color = pulseColor;
                view.topCap.flipY = false;
                SetSimpleSize(view.topCap, p.w, topCapH);
                // Move the top cap slightly INTO the body so its visible
                // lower edge always sits on/over the pillar body.
                view.topCap.transform.position = ScreenToWorld(
                    p.x,
                    top - topCapH / 2f + pillarCapOverlap * s
                );

                view.botCap.sprite = capSpr;
                view.botCap.color = pulseColor;
                view.botCap.flipY = true;
                SetSimpleSize(view.botCap, p.w, botCapH);
                // Bottom cap is flipped, so it is moved upward by the same
                // amount to overlap the body rather than float below it.
                view.botCap.transform.position = ScreenToWorld(
                    p.x,
                    bottom + botCapH / 2f - pillarCapOverlap * s
                );
            }
        }

        SyncScrollPair(groundSrA, groundSrB, groundFrames, w, groundHeight * s, h - groundHeight * s / 2f, groundScrollSpeed, scrollTime);
        SyncScrollPair(topSrA, topSrB, topFrames, w, topHeight * s, topHeight * s / 2f, topScrollSpeed, scrollTime);

        if (dead && deathFrame != null)
        {
            birdSr.sprite = deathFrame;
            birdSr.color = Color.white;
        }
        else
        {
            Sprite bs = CurrentFrame(birdFrames, gameTime);
            birdSr.sprite = bs;
            birdSr.color = bs != null ? Color.white : new Color(1f, 0.831f, 0.278f);
            birdSr.enabled = true;
        }
        if (birdSr.sprite != null)
        {
            SetSimpleSize(birdSr, BirdR * 2f, BirdR * 2f);
            birdSr.transform.position = ScreenToWorld(birdPos.x, birdPos.y);
        }
        else
        {
            birdSr.enabled = false; // no bird art and generated fallback needs a real sprite too -- see note below
        }

        float topInset = 0f, leftInset = 0f, rightInset = 0f;
        Rect safe = Screen.safeArea;
        topInset = h - safe.yMax;
        leftInset = safe.x;
        rightInset = w - safe.xMax;

        scoreText.text = "Score " + score;
        scoreText.fontSize = Mathf.RoundToInt(20 * s);
        scoreText.rectTransform.anchoredPosition = new Vector2(leftInset + 16 * s, -(topInset + 10 * s));

        bestText.text = "Best " + best;
        bestText.fontSize = Mathf.RoundToInt(20 * s);
        bestText.rectTransform.anchoredPosition = new Vector2(leftInset + 16 * s, -(topInset + 34 * s));

        deathsText.text = "Deaths " + deaths;
        deathsText.fontSize = Mathf.RoundToInt(20 * s);
        deathsText.rectTransform.anchoredPosition = new Vector2(leftInset + 16 * s, -(topInset + 58 * s));

        warnText.gameObject.SetActive(!pausedForExit && gameTime < glitchUntil);
        warnText.fontSize = Mathf.RoundToInt(20 * s);
        warnText.rectTransform.anchoredPosition = new Vector2(-(rightInset + 16 * s), -(topInset + 10 * s));

        echoText.gameObject.SetActive(echoVisible);
        echoText.fontSize = Mathf.RoundToInt(20 * s);
        echoText.rectTransform.anchoredPosition = new Vector2(0, -(topInset + 10 * s));

        bool showMenu = !running;
        titleText.gameObject.SetActive(showMenu);
        subText.gameObject.SetActive(showMenu);
        if (showMenu)
        {
            if (pausedForExit)
            {
                titleText.text = "PAUSED";
                subText.text = "Tap / Click / Space = start from beginning\nESC / Back again = Exit";
            }
            else if (dead)
            {
                titleText.text = deathMsg;
                subText.text = "Score: " + score + "  (tap to retry)";
            }
            else
            {
                titleText.text = "FLAP";
                subText.text = "Tap / Click / Space to start";
            }

            titleText.fontSize = Mathf.RoundToInt(30 * s);
            titleText.rectTransform.anchoredPosition = new Vector2(0, h * 0.1f * s);

            subText.fontSize = Mathf.RoundToInt(16 * s);
            subText.rectTransform.anchoredPosition = new Vector2(0, h * 0.1f * s - 54f * s);
        }
    }
}
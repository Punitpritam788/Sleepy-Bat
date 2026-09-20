# 🦇 Sleepy Bat

<p align="center">
  <strong>FLAP INTO THE DARK — I NEED SOME SLEEP.</strong>
</p>

<p align="center">
  A dark, pixel-styled, rage-inducing arcade survival game where <strong>your movement controls your vision</strong>.
</p>

<p align="center">

![HTML5](https://img.shields.io/badge/HTML5-Canvas-E34F26?style=for-the-badge\&logo=html5\&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-Game%20Logic-F7DF1E?style=for-the-badge\&logo=javascript\&logoColor=black)
![Unity](https://img.shields.io/badge/Unity-6.x-000000?style=for-the-badge\&logo=unity\&logoColor=white)
![C%23](https://img.shields.io/badge/C%23-Gameplay-239120?style=for-the-badge\&logo=csharp\&logoColor=white)
![Web](https://img.shields.io/badge/Platform-Web-4285F4?style=for-the-badge\&logo=googlechrome\&logoColor=white)
![Windows](https://img.shields.io/badge/Platform-Windows-0078D6?style=for-the-badge\&logo=windows\&logoColor=white)
![Android](https://img.shields.io/badge/Platform-Android-3DDC84?style=for-the-badge\&logo=android\&logoColor=white)

</p>

<p align="center">

**HTML5 Web Build** • **Windows EXE** • **Android APK** • **Responsive Gameplay** • **Pixel Rendering** • **Procedural Audio**

</p>

---

## 🌙 What Is Sleepy Bat?

**Sleepy Bat** is a fast-paced arcade survival game inspired by the simple-to-learn, difficult-to-master design of classic one-button games — but with a much darker twist.

You play as a sleepy little bat flying through a dangerous cave.

The catch?

### 🦇 When the bat rises, the cave disappears.

### 👁️ When the bat falls, echolocation reveals the danger.

That means your own movement becomes part of the difficulty.

Every flap can change what you can see.

Every descent can reveal a pillar exactly when you least want it.

And once the game enters **RAGE MODE**, the cave starts fighting back.

> **"I just wanna sleep."**

---

# 🎮 Gameplay

The objective is brutally simple:

**Fly. Survive. Pass pillars. Increase your score. Don't hit anything.**

The longer you survive, the more unpredictable the environment becomes.

The game combines:

* 🦇 One-button flying
* 👁️ Echolocation-style visibility
* 🪨 Randomized cave pillars
* 🔄 Moving pillars
* 🎯 Last-second gap shifts
* 📉 Increasing difficulty
* ⚡ Control glitches
* 💢 Gravity pressure pulses
* 💀 Death animations
* 💥 Death particles
* 📳 Screen shake
* 🔊 Procedural sound effects
* 🏆 Local high-score tracking
* 😈 Rage-game taunts

---

# 👁️ Core Mechanic — Echolocation

The main gameplay mechanic of **Sleepy Bat** is its visibility system.

Instead of keeping the obstacles permanently visible, the game uses the bat's vertical movement to determine whether the environment should be revealed.

### 🪽 While the bat is rising

The cave obstacles become hidden.

The pillars and their caps effectively vanish from view.

You are flying partially blind.

### 🦇 While the bat is falling

The bat enters its **echo/sonar state**.

The pillars become visible again using a rapid flickering appearance rather than a smooth fade.

This creates a gameplay loop where:

```text
FLAP
  ↓
OBSTACLES DISAPPEAR
  ↓
FALL
  ↓
ECHLOCATION REVEALS THE CAVE
  ↓
REACT
  ↓
FLAP AGAIN
```

The visibility threshold itself scales with progression, making the mechanic become less predictable as the score increases.

---

# 🪨 Dynamic Pillar System

Sleepy Bat does not rely on completely static obstacles.

The pillar system contains several layers of randomness and movement.

### Random pillar generation

Each pillar receives randomized:

* Horizontal spacing
* Vertical gap position
* Gap size
* Movement phase
* Drift amount
* Drift speed
* Jump timing

The game also has a chance to create **joined / closely spaced pillar patterns**, reducing the amount of recovery space between obstacles.

### Continuous vertical drift

Some pillars continuously move using a smooth oscillating motion.

Instead of instantly teleporting, the pillar gradually follows its randomized target position.

### Fake-out movement

The game contains multiple last-second movement events.

A pillar can:

1. Move before the bat reaches it.
2. Perform a second smaller movement closer to the player.
3. Smoothly animate between positions instead of instantly teleporting.

So even when you think you understand the gap...

> **THE GAP MOVED.**

---

# 📈 Difficulty System

The difficulty is not simply increased by making everything faster.

Multiple gameplay variables scale with the score.

### Gap Size

The safe opening becomes smaller as the score increases.

```text
Higher Score
      ↓
Smaller Gap
      ↓
Less Recovery Space
```

### Movement Speed

Pillar movement gradually becomes faster.

### Joined Obstacles

Higher scores increase the probability of tighter pillar sequences.

### Movement Probability

Moving-pillar behavior becomes more common at higher score ranges.

### Pressure Events

After reaching a certain score, temporary gravity pressure pulses can appear.

During these moments:

```text
Normal Gravity
      ↓
Temporary Gravity Increase
      ↓
The Same Flap Timing Suddenly Feels Wrong
```

That's where the rage begins.

---

# ⚡ Rage Mechanics

Sleepy Bat intentionally contains mechanics designed to create unpredictable moments.

## 1. Retry Troll

After death, there is a chance that the first retry input will be rejected.

The game may respond with:

> **LOL. NO.**

This is intentionally unfair-looking behavior designed to make the player question what just happened.

---

## 2. Flap Glitch

At random intervals, a temporary control glitch can activate.

Two possible states can occur:

### `WEAK`

Your flap becomes significantly weaker.

### `INVERTED`

The flap direction is temporarily altered.

The game also displays an event message so the player knows something strange is happening.

---

## 3. Gravity Pressure

At higher scores, temporary gravity pulses can make the bat fall faster.

This creates situations where a familiar timing pattern suddenly stops working.

---

# 💀 Death System

Death is treated as an event rather than simply stopping the game.

When the bat dies, the game can trigger:

* 🖼️ Dedicated death frame
* 💥 Particle burst
* 📳 Screen shake
* 🔊 Death sound
* 💬 Random death taunt
* 🧮 Death counter
* 🏆 Best-score update

The death frame immediately replaces the normal flying animation after impact.

### Example death messages

```text
missed it by THAT much
the gap moved. definitely.
that felt rigged. it was.
so close, right?

gravity said no
skill issue
you flinched.
physics is fake
```

And when the maximum score is reached:

```text
999. THAT'S IT. THAT'S ALL YOU GET.
```

---

# 🏆 Score System

Every successfully passed pillar increases the score.

The game keeps track of:

```text
SCORE
BEST SCORE
TOTAL DEATHS
```

The score progression also drives the difficulty system.

The current browser implementation stores the statistics using:

```text
localStorage
```

The Unity version uses:

```text
PlayerPrefs
```

So your best score and death count can persist between sessions.

---

# 🕹️ Controls

## 💻 Windows / PC

| Input         | Action            |
| ------------- | ----------------- |
| `SPACE`       | Flap              |
| `ARROW UP`    | Flap              |
| `Mouse Click` | Flap              |
| `ESC`         | Pause / Exit flow |

## 📱 Android

| Input                        | Action            |
| ---------------------------- | ----------------- |
| `Tap`                        | Flap              |
| `System Back / Back Gesture` | Pause / Exit flow |

### Pause / Exit behavior

The Unity version implements an intentionally simple back-button system:

```text
Active Game
    ↓
Back / ESC
    ↓
Pause / Exit Screen
    ↓
Tap / Space / Up
    ↓
Fresh Run
```

A second Back / ESC input while the pause screen is open exits the application.

---

# 🧠 Technical Architecture

Sleepy Bat currently has **two implementations**.

## 🌐 HTML5 Version

The standalone browser build uses:

```text
HTML
CSS
JavaScript
HTML5 Canvas
Web Audio API
Pointer Events
Keyboard Events
localStorage
requestAnimationFrame
```

The game renders directly onto an HTML `<canvas>` and uses a pixel-oriented visual presentation.

The canvas is configured for full-screen responsive rendering with pixelated image scaling.

### Browser rendering pipeline

```text
Input
  ↓
Game State
  ↓
Physics
  ↓
Pillar Simulation
  ↓
Collision Detection
  ↓
Visibility / Echo State
  ↓
Canvas Rendering
  ↓
requestAnimationFrame
```

The browser build also embeds its artwork directly into the HTML file, allowing the game to start without waiting for external image loading.

---

# 🛠️ Unity Version

The Unity implementation is designed around a real runtime scene rather than relying on GUI-only rendering.

The project dynamically creates a scene hierarchy containing:

```text
Main Camera
Background
Ground
Top Cave Layer
Pillars
Bird
HUD Canvas
Particle System
Audio Sources
Glitch Overlay
```

The Unity version uses:

* Unity 6.x
* C#
* `SpriteRenderer`
* `Canvas`
* `Text`
* `CanvasScaler`
* `ParticleSystem`
* `AudioSource`
* `AudioClip`
* `PlayerPrefs`
* Runtime `GameObject` creation

The project creates its actual visual objects at runtime, allowing the scene hierarchy to contain real game objects and sprite renderers instead of relying entirely on an `OnGUI` drawing approach.

---

# 🎨 Sprite-Based Rendering

Game artwork is handled through sprites.

The Unity implementation supports sprite arrays for:

```text
Background Animation
Bird Animation
Pillar Animation
Pillar Cap Animation
Ground Animation
Top Cave Animation
Death Frame
```

The system can use a single sprite for a static image or multiple sprites for frame-based animation.

Animation speed is configurable through:

```text
animationFps
```

---

# 📐 Responsive Pixel Scaling

The game uses a reference-resolution approach so gameplay values can remain consistent across different screen sizes.

The Unity implementation uses:

```text
Reference Height = 900 pixels
```

and calculates a scale factor from the current screen height.

The world-space setup uses the convention:

```text
1 Unity world unit ≈ 1 pixel
```

with an orthographic camera.

This allows gameplay values such as:

* gravity
* player size
* pillar size
* movement speed
* collision padding
* gap distances

to scale with the display.

---

# 🪨 Texture-Based Cave Collision

One of the more interesting technical systems is the cave collision profile.

Instead of simply assuming that the ground and ceiling are flat rectangles, the Unity implementation can build a height profile directly from the alpha information of the cave texture.

Conceptually:

```text
Cave Texture
     ↓
Read Alpha Pixels
     ↓
Sample Horizontal Positions
     ↓
Build Height Profile
     ↓
Sample Collision Height During Gameplay
```

This allows irregular cave artwork to influence collision behavior.

If the texture cannot be read, the implementation falls back to simpler flat collision behavior.

---

# 🎥 Scrolling & Parallax

The cave uses independent scrolling layers.

The project separates:

```text
Background
Ground
Top Cave
Pillars
```

and assigns different movement speeds.

This produces a simple layered parallax effect while maintaining the arcade-game feel.

The browser implementation also uses continuous scrolling and wraps the scene for an endless-run effect.

---

# 🔊 Procedural Audio

Rather than requiring a large collection of external sound files, both implementations include lightweight procedural sound generation concepts.

### Unity

The Unity version creates audio clips at runtime using synthesized waveforms such as:

* Sine
* Triangle
* Saw
* Square

Different frequencies and durations are mapped to gameplay events.

Examples include:

```text
Flap
Glitch Flap
Pass
Death
Glitch Warning
Rage Troll
Sleepy / Special Death
```

### HTML5

The browser implementation uses the Web Audio API and creates short oscillator-based sound effects dynamically.

This keeps the audio system lightweight and highly controllable.

---

# 💥 Particle Effects

Death creates a small burst effect.

The Unity version uses a `ParticleSystem` for the death burst.

The browser version uses lightweight manually simulated particles.

The effect is intentionally short and arcade-like:

```text
Collision
   ↓
Death State
   ↓
Burst
   ↓
Screen Shake
   ↓
Death Frame
   ↓
Death Message
```

---

# 📊 Game State Design

The game maintains a compact state machine around several important states:

```text
TITLE
  ↓
RUNNING
  ↓
DEAD
  ↓
RETRY
```

The Unity version additionally includes:

```text
PAUSED / EXIT SCREEN
```

Important state variables include:

```text
running
dead
score
best
deaths
pausedForExit
gameTime
scrollTime
glitchUntil
glitchInvert
trollBlock
echoVisible
```

This keeps gameplay behavior deterministic and makes the different rage mechanics easier to manage.

---

# 🧮 Core Gameplay Formulae

The game uses score-driven functions rather than hardcoding a single difficulty level.

### Gap

```text
gap = max(minimumGap, baseGap - score × scaling)
```

### Speed

```text
speed = baseSpeed + progressiveScoreBonus
```

### Echo Threshold

The visibility state depends on the bat's vertical velocity.

```text
falling faster
      ↓
echo becomes active
      ↓
pillars revealed
```

### Pillar Movement

Pillars combine:

```text
Random Spawn
+
Smooth Drift
+
Pre-Contact Fake-Out
+
Last-Second Fake-Out
```

This produces an obstacle system that feels alive instead of purely procedural in a predictable way.

---

# 🧩 Performance-Oriented Techniques

The project is designed around lightweight real-time game techniques rather than heavy systems.

### Object pooling

The Unity implementation creates a reusable pillar pool instead of continuously creating unlimited pillar objects.

### Two-panel scrolling

Background-style layers use paired sprites that continuously reposition to create an endless scrolling loop.

### Lightweight particles

Death particles are kept small and short-lived.

### Procedural audio

Short runtime-generated tones eliminate the need for a large collection of sound assets.

### Responsive rendering

The game calculates display scaling instead of rendering at one fixed gameplay size.

### Standalone browser build

The HTML version embeds artwork directly into the file, helping keep the demo self-contained.

---

# 📁 Suggested Repository Structure

```text
Sleepy-Bat/
│
├── README.md
│
├── Web/
│   └── SleepyBat_DeathFrame_Caps180.html
│
├── Windows/
│   └── SleepyBat.exe
│
├── Android/
│   └── SleepyBat.apk
│
├── Unity/
│   ├── Assets/
│   ├── Packages/
│   ├── ProjectSettings/
│   └── ...
│
└── Screenshots/
    ├── gameplay.png
    ├── death-screen.png
    ├── echo-mode.png
    └── rage-mode.png
```

> You can change the folder names to match your final GitHub repository structure.

---

# 🚀 Running the HTML Version

The browser version is designed as a standalone HTML game.

You can simply open:

```text
SleepyBat_DeathFrame_Caps180.html
```

in a modern browser.

Recommended browsers:

* Google Chrome
* Microsoft Edge
* Firefox
* Chromium-based browsers

For hosting, the same HTML file can be deployed through:

* GitHub Pages
* Netlify
* Vercel
* Any static web server

---

# 🖥️ Windows Build

The Unity project can be exported as a Windows executable.

Typical release structure:

```text
Windows/
├── SleepyBat.exe
├── SleepyBat_Data/
├── UnityPlayer.dll
└── ...
```

The exact folder contents depend on the Unity build configuration.

---

# 📱 Android Build

The Unity project can also be exported as an Android APK.

```text
Android/
└── SleepyBat.apk
```

The gameplay input system supports touch input, while Android's Back input is routed into the game's pause/exit behavior.

---

# 🏗️ Build Targets

| Build   | Technology                  | Target  |
| ------- | --------------------------- | ------- |
| 🌐 Web  | HTML5 + JavaScript + Canvas | Browser |
| 🖥️ EXE | Unity 6.x + C#              | Windows |
| 📱 APK  | Unity 6.x + C#              | Android |

---

# 🧪 Development Features

The Unity implementation also contains development-oriented functionality, including:

* Runtime scene construction
* Debug logging
* Persistent score statistics
* Development reset functionality
* Dynamic object creation
* Runtime collision profile generation
* Configurable animation settings
* Configurable pillar dimensions
* Configurable scroll speeds
* Safe display scaling

A development `F9` reset is included in the Unity implementation for clearing saved best/death statistics while testing.

---

# 🎯 Design Philosophy

Sleepy Bat is intentionally built around **simple controls + unpredictable consequences**.

The player only really needs one primary action:

```text
FLAP
```

But that one action affects:

```text
Vertical velocity
        ↓
Echo visibility
        ↓
Obstacle readability
        ↓
Reaction timing
        ↓
Survival
```

The game therefore turns a very simple control scheme into a layered timing challenge.

---

# 💜 Visual Direction

The game's visual identity combines:

* Dark cave environments
* Purple / violet atmosphere
* Pixel-oriented rendering
* Minimal HUD
* High-contrast character art
* Flickering obstacle visibility
* CRT-like / arcade-inspired presentation
* Sleepy and humorous messaging

The contrast between the cute sleepy bat and deliberately frustrating mechanics is a major part of the game's personality.

---

# 😴 The Personality of the Game

Sleepy Bat isn't supposed to feel like a perfectly polite arcade game.

It's supposed to feel like the game itself is getting annoyed with you.

You miss a pillar?

> **"missed it by THAT much"**

The gap changes?

> **"the gap moved. definitely."**

Gravity ruins your timing?

> **"gravity said no"**

You reach the maximum?

> **"999. THAT'S IT. THAT'S ALL YOU GET."**

The result is a game that treats failure as part of the entertainment.

---

# 🧰 Technology Stack

### Web Build

```text
HTML5
CSS3
JavaScript
HTML5 Canvas
Web Audio API
Pointer Events
Keyboard Events
localStorage
requestAnimationFrame
```

### Unity Build

```text
Unity 6.x
C#
SpriteRenderer
Canvas UI
ParticleSystem
AudioSource
AudioClip
PlayerPrefs
Runtime GameObjects
Orthographic Camera
```

---

# ✨ Features At A Glance

| Feature                            | Sleepy Bat |
| ---------------------------------- | :--------: |
| One-button gameplay                |      ✅     |
| Touch support                      |      ✅     |
| Keyboard support                   |      ✅     |
| Responsive screen scaling          |      ✅     |
| Echolocation mechanic              |      ✅     |
| Hidden obstacles while rising      |      ✅     |
| Flickering obstacles while falling |      ✅     |
| Random pillar generation           |      ✅     |
| Moving pillars                     |      ✅     |
| Last-second fake-outs              |      ✅     |
| Progressive difficulty             |      ✅     |
| Gravity pressure events            |      ✅     |
| Flap glitches                      |      ✅     |
| Retry trolling                     |      ✅     |
| Death frame                        |      ✅     |
| Death particles                    |      ✅     |
| Screen shake                       |      ✅     |
| Procedural audio                   |      ✅     |
| Persistent best score              |      ✅     |
| Death counter                      |      ✅     |
| Windows build                      |      ✅     |
| Android build                      |      ✅     |
| Browser build                      |      ✅     |

---

# 📸 Screenshots & Media

Add your screenshots here to make the repository more visual:

```md
## 📸 Screenshots

<p align="center">
  <img src="Screenshots/gameplay.png" width="45%">
  <img src="Screenshots/echo-mode.png" width="45%">
</p>

<p align="center">
  <img src="Screenshots/rage-mode.png" width="45%">
  <img src="Screenshots/death-screen.png" width="45%">
</p>
```

A short gameplay GIF or video is highly recommended for the repository's front page.

Example:

```md
## 🎬 Gameplay

![Sleepy Bat Gameplay](Screenshots/gameplay.gif)
```

---

# ▶️ Play / Download

### 🌐 Play in Browser

```text
[ INSERT YOUR GITHUB PAGES / WEB DEMO LINK HERE ]
```

### 🖥️ Download Windows EXE

```text
[ INSERT YOUR WINDOWS RELEASE LINK HERE ]
```

### 📱 Download Android APK

```text
[ INSERT YOUR ANDROID APK RELEASE LINK HERE ]
```

For GitHub, the cleanest approach is to place the EXE and APK inside **GitHub Releases** rather than committing very large binaries directly into the repository.

---

# 🛠️ Future Ideas

Possible future expansions include:

* More bat animations
* Additional cave environments
* More glitch events
* New rage mechanics
* Difficulty presets
* More death animations
* Additional sound effects
* New particle effects
* Cosmetic bat skins
* Challenge modes
* Endless score leaderboards
* Achievement system
* More platform builds

---

# 🦇 Why "Sleepy Bat"?

Because the bat doesn't want to save the world.

It doesn't want to become a hero.

It doesn't even want to fly.

### It just wants to sleep.

Unfortunately...

**the cave has other plans.**

---

# 👨‍💻 Project

**Sleepy Bat** is an experimental arcade game project focused on combining simple gameplay mechanics with procedural systems, responsive rendering, visual feedback and deliberately frustrating game design.

It was developed in two forms:

```text
HTML5 / JavaScript
        +
Unity 6.x / C#
```

The HTML version provides a lightweight standalone web implementation, while the Unity version provides a full game-engine implementation suitable for desktop and mobile builds.

---

# ❤️ Built For Fun, Frustration & Sleep Deprivation

If you survived long enough to read this README...

you probably haven't survived **999**.

Yet.

## 🦇 FLAP.

## 🌑 FALL.

## 👁️ LISTEN.

## 💀 DIE.

## 🔁 TRY AGAIN.

**I NEED SOME SLEEP.**

---

<p align="center">
  <strong>🦇 Sleepy Bat • Dark Arcade • Rage Game • Pixel Survival</strong>
</p>

<p align="center">
  <sub>Built with HTML5 Canvas + JavaScript and Unity 6.x</sub>
</p>

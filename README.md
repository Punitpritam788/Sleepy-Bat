# 🦇 SLEEPY BAT

<p align="center">
  <img src="https://img.shields.io/badge/🦇-SLEEPY%20BAT-6E2CA8?style=for-the-badge" alt="Sleepy Bat">
  <img src="https://img.shields.io/badge/STATUS-IN%20DEVELOPMENT-8B5CF6?style=for-the-badge" alt="Status">
</p>

<p align="center">
  <strong>FLAP INTO THE DARK.</strong><br>
  <em>I just want some sleep.</em>
</p>

<p align="center">
  A dark arcade survival game where <strong>your movement controls your vision.</strong>
</p>

<p align="center">
  <a href="#-play">🎮 Play</a> •
  <a href="#-features">✨ Features</a> •
  <a href="#-gameplay">🦇 Gameplay</a> •
  <a href="#-technology">🛠️ Technology</a> •
  <a href="#-downloads">⬇️ Downloads</a>
</p>

---

## 🌑 THE IDEA

Most arcade games give you a clear view of the obstacles.

**Sleepy Bat doesn't.**

Your bat uses echolocation.

When it rises, the darkness takes over.

When it falls, the cave briefly reveals itself.

So every flap changes not only your position...

### it changes what you can see.

---

## 🎬 GAMEPLAY

<p align="center">
  <!-- Replace with your gameplay GIF -->
  <img src="Screenshots/gameplay.gif" alt="Sleepy Bat Gameplay" width="85%">
</p>

<p align="center">
  <sub>🎥 Replace the image above with a gameplay GIF or short gameplay clip.</sub>
</p>

---

## ⚡ GAMEPLAY LOOP

```text
                🦇 FLAP
                   │
                   ▼
          🌑 CAVE DISAPPEARS
                   │
                   ▼
             FALL THROUGH
                   │
                   ▼
          👁️ ECHO REVEALS
          THE OBSTACLES
                   │
                   ▼
             🪨 REACT FAST
                   │
                   ▼
              ✅ SURVIVE
                   │
                   ▼
             📈 SCORE UP
                   │
                   └───────────────┐
                                   │
                              DO IT AGAIN
```

---

# ✨ FEATURES

<table>
<tr>
<td width="50%">

### 🦇 Echolocation

Your vertical movement controls obstacle visibility.

### 🪨 Dynamic Pillars

Pillars can spawn at different positions and move during gameplay.

### 🎯 Moving Gaps

The safe opening isn't always where you expect it to be.

### ⚡ Glitch Events

Temporary gameplay disruptions can alter your flap behavior.

</td>

<td width="50%">

### 💀 Rage Deaths

Death comes with animation, particles, shake and taunts.

### 🏆 Persistent Score

Best score and death statistics are stored locally.

### 🔊 Procedural Audio

Gameplay sounds can be generated at runtime.

### 📱 Multi-Platform

Built for Web, Windows and Android.

</td>
</tr>
</table>

---

# 🧠 THE MAIN MECHANIC

## 👁️ Echo Visibility

The most important mechanic in Sleepy Bat is the relationship between **vertical velocity and visibility**.

```text
                    FLAP ↑
                      │
                      ▼
              ┌─────────────┐
              │  RISING     │
              │             │
              │  🌑 DARK    │
              │             │
              └─────────────┘
                      │
                      ▼
                FALLING ↓
                      │
                      ▼
              ┌─────────────┐
              │ ECHO MODE   │
              │             │
              │ 🪨 👁️ 🪨    │
              │ OBSTACLES   │
              │ REVEALED    │
              └─────────────┘
```

The result is a timing game where:

> **You are not only controlling the bat.
> You are controlling your information.**

---

# 🪨 DYNAMIC PILLARS

The cave isn't built from perfectly static obstacles.

Pillars can use:

* Random horizontal spacing
* Random vertical placement
* Variable gap sizes
* Closely-spaced obstacle patterns
* Smooth vertical movement
* Last-second gap adjustments
* Progressive movement difficulty

This means memorizing a single obstacle pattern won't save you.

### The gap can move.

And yes...

> **It probably wasn't moving five seconds ago.**

---

# 😈 RAGE SYSTEM

Sleepy Bat intentionally introduces moments that can disrupt your rhythm.

### ⚡ Flap Glitch

A temporary glitch can modify the normal flap response.

### 🔥 Gravity Pressure

At higher difficulty, gravity can become more aggressive.

### 🃏 Retry Troll

The game can occasionally mess with the first retry interaction after death.

### 📺 Visual Glitches

Temporary visual effects can warn you that something is about to go wrong.

The goal isn't to make the game impossible.

The goal is to make you say:

> **"WHAT JUST HAPPENED?"**

---

# 💀 DEATH IS PART OF THE GAME

When the bat dies, the game doesn't simply stop.

A death event can trigger:

```text
💥 Particle Burst
      +
📳 Screen Shake
      +
🦇 Death Frame
      +
🔊 Death Sound
      +
💬 Random Taunt
      +
☠️ Death Counter
      +
🏆 Score Check
```

Example reactions:

```text
"missed it by THAT much"

"the gap moved. definitely."

"that felt rigged. it was."

"so close, right?"

"gravity said no"

"skill issue"

"you flinched."

"physics is fake"
```

---

# 🏆 SCORE SYSTEM

Every successfully cleared obstacle increases the score.

The game tracks:

| Statistic | Purpose             |
| --------- | ------------------- |
| 🎯 Score  | Current run         |
| 🏆 Best   | Highest saved score |
| 💀 Deaths | Total deaths        |

The Unity implementation persists gameplay statistics using `PlayerPrefs`, while the standalone web implementation uses browser-side storage.

---

# 🎮 CONTROLS

## 💻 PC

| Input        | Action          |
| ------------ | --------------- |
| `SPACE`      | 🦇 Flap         |
| `↑ UP`       | 🦇 Flap         |
| `LEFT CLICK` | 🦇 Flap         |
| `ESC`        | ⏸️ Pause / Exit |

## 📱 Android

| Input  | Action          |
| ------ | --------------- |
| `TAP`  | 🦇 Flap         |
| `BACK` | ⏸️ Pause / Exit |

The Unity version routes Escape/back handling through its pause/exit logic and supports restarting from the pause screen as a fresh run.

---

# 🛠️ TECHNOLOGY

## 🌐 HTML5 VERSION

```text
HTML5
├── Canvas
├── JavaScript
├── CSS
├── Web Audio API
├── Keyboard Input
├── Touch / Pointer Input
└── Browser Storage
```

The HTML version is designed as a self-contained browser game with a full-screen canvas and pixel-oriented rendering.

---

## 🎮 UNITY VERSION

```text
Unity 6.x
├── C#
├── SpriteRenderer
├── GameObjects
├── Canvas UI
├── ParticleSystem
├── AudioSource
├── Procedural Audio
├── PlayerPrefs
└── Orthographic Camera
```

The Unity implementation builds an actual runtime hierarchy containing the camera, sprite layers, pillars, particles, HUD and audio components.

---

# 🧩 UNITY ARCHITECTURE

The runtime scene is structured approximately like this:

```text
FlapScene
│
├── Main Camera
│
├── Background_A
├── Background_B
│
├── Ground_A
├── Ground_B
│
├── Top_A
├── Top_B
│
├── GlitchOverlay
│
├── Bird
│
├── Pillar_0
│   ├── TopBody
│   ├── TopCap
│   ├── BotBody
│   └── BotCap
│
├── Pillar_1
├── Pillar_2
├── ...
│
├── DeathBurst
│
└── HUD Canvas
    ├── Score
    ├── Best
    ├── Deaths
    ├── Warning
    └── Echo
```

The project uses a reusable pillar pool and creates the visual runtime objects directly through Unity's normal sprite pipeline.

---

# 🎨 SPRITE & ANIMATION SYSTEM

The Unity implementation supports sprite arrays for:

```text
🖼️ Background Frames
🦇 Bird Frames
💀 Death Frame
🪨 Pillar Frames
🔺 Pillar Cap Frames
🪨 Ground Frames
⬆️ Top Cave Frames
```

A single sprite can be used as a static image, while multiple sprites can be cycled as animation frames.

---

# 📐 RESPONSIVE RENDERING

The Unity version uses a pixel-oriented world-space convention with an orthographic camera.

```text
Screen Size
    ↓
Scale Calculation
    ↓
World Position
    ↓
Sprite Size
    ↓
Responsive Gameplay
```

The implementation calculates a screen-dependent scale factor and converts screen coordinates into world positions so gameplay values remain consistent across different display sizes.

---

# 🪨 TEXTURE-BASED COLLISION

The cave isn't required to behave like a simple rectangular floor and ceiling.

The Unity version can analyse the alpha information of the cave textures and build a sampled height profile.

```text
Cave Texture
     ↓
Read Pixels
     ↓
Analyse Alpha
     ↓
Create Height Profile
     ↓
Sample During Collision
```

This allows irregular artwork to participate in the collision system.

---

# 🔊 PROCEDURAL AUDIO

The Unity version generates short sound effects dynamically instead of requiring a separate audio asset for every simple interaction.

Generated effects include:

```text
🦇 Flap
⚡ Glitch Flap
✅ Pass
💀 Death
⚠️ Glitch Warning
😈 Troll
😴 Sleepy
```

The implementation generates waveform-based `AudioClip` data at runtime.

---

# 🌌 SCROLLING & PARALLAX

Multiple environmental layers move at different speeds:

```text
Background  → Slow
Top Cave    → Medium
Ground      → Fast
Pillars     → Gameplay Speed
```

This creates the perception of movement without requiring a fully 3D environment.

The Unity implementation uses paired sprite layers to create a continuous scrolling loop.

---

# 📈 DIFFICULTY SYSTEM

Difficulty is driven by the score.

As the player progresses:

```text
SCORE ↑
  │
  ├── GAP ↓
  ├── SPEED ↑
  ├── MOVEMENT ↑
  ├── REACTION TIME ↓
  └── RAGE EVENTS ↑
```

The Unity gameplay logic calculates score-dependent gap size, movement speed and flying thresholds rather than using a single fixed difficulty value.

---

# 🧪 DEVELOPMENT

The project was built around experimentation with:

* 2D gameplay programming
* Procedural systems
* Runtime scene construction
* Sprite-based animation
* Collision sampling
* Responsive scaling
* Procedural sound synthesis
* Particle effects
* Persistent game statistics
* Cross-platform input

The Unity implementation also includes a development reset for saved score/death statistics.

---

# 📦 BUILDS

| Version | Technology         | Platform |
| ------- | ------------------ | -------- |
| 🌐 Web  | HTML5 + JavaScript | Browser  |
| 🖥️ EXE | Unity 6.x + C#     | Windows  |
| 📱 APK  | Unity 6.x + C#     | Android  |

---

# 🚀 PLAY / DOWNLOAD

## 🌐 PLAY ONLINE

**[▶️ PLAY SLEEPY BAT](YOUR-GITHUB-PAGES-LINK)**

## 🖥️ WINDOWS

**[⬇️ DOWNLOAD EXE](YOUR-GITHUB-RELEASE-LINK)**

## 📱 ANDROID

**[⬇️ DOWNLOAD APK](YOUR-GITHUB-RELEASE-LINK)**

> Replace the placeholder links above with your final GitHub Pages and Releases URLs.

---

# 📁 REPOSITORY

```text
Sleepy-Bat/
│
├── README.md
│
├── Web/
│   └── SleepyBat_DeathFrame_Caps180.html
│
├── Unity/
│   ├── Assets/
│   ├── Packages/
│   └── ProjectSettings/
│
├── Windows/
│   └── SleepyBat.exe
│
├── Android/
│   └── SleepyBat.apk
│
└── Screenshots/
    ├── gameplay.png
    ├── echo-mode.png
    ├── rage-mode.png
    └── death-screen.png
```

---

# 🎯 DESIGN PHILOSOPHY

Sleepy Bat follows one simple rule:

### **Easy to understand. Hard to survive.**

You only need one main action:

```text
               FLAP
                ↓
        ┌──────────────┐
        │              │
        │   SURVIVE    │
        │              │
        └──────────────┘
                ↓
              DIE
                ↓
             RETRY
```

The complexity comes from what happens **around** that button.

---

# 🦇 WHY A SLEEPY BAT?

Because this bat doesn't want to become a hero.

It doesn't want treasure.

It doesn't want adventure.

### It wants a nap.

Unfortunately...

**the cave wants violence.**

---

# ❤️ MADE WITH

<p align="center">

![Unity](https://img.shields.io/badge/Made%20with-Unity-000000?style=flat-square\&logo=unity\&logoColor=white)
![CSharp](https://img.shields.io/badge/Gameplay-C%23-239120?style=flat-square\&logo=csharp\&logoColor=white)
![JavaScript](https://img.shields.io/badge/Web-JavaScript-F7DF1E?style=flat-square\&logo=javascript\&logoColor=black)
![HTML5](https://img.shields.io/badge/Web-HTML5-E34F26?style=flat-square\&logo=html5\&logoColor=white)
![Canvas](https://img.shields.io/badge/Rendering-Canvas-6E2CA8?style=flat-square)
![Android](https://img.shields.io/badge/Mobile-Android-3DDC84?style=flat-square\&logo=android\&logoColor=white)

</p>

---

# 🦇 FINAL TRANSMISSION

```text
╔══════════════════════════════════════╗
║                                      ║
║        🦇  S L E E P Y  B A T       ║
║                                      ║
║              FLAP.                   ║
║              FALL.                   ║
║              ECHO.                   ║
║              PANIC.                  ║
║              DIE.                    ║
║                                      ║
║        "I NEED SOME SLEEP."          ║
║                                      ║
╚══════════════════════════════════════╝
```

<p align="center">
  <strong>🌑 Welcome to the cave.</strong><br>
  <strong>🦇 Good luck.</strong><br>
  <strong>💀 You're going to need it.</strong>
</p>

---

<p align="center">
  <sub>Sleepy Bat • Arcade Survival • Echolocation • Rage Game</sub>
</p>

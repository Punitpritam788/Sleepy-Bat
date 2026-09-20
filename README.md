<div align="center">

# 🦇 SLEEPY BAT

### **FLAP INTO THE DARK - I NEED SOME SLEEP.**

<p>
A dark, pixel-styled, rage-inducing arcade survival game where<br>
<strong>your movement controls your vision.</strong>
</p>

<br>

<a href="https://punitpritam788.github.io/Sleepy-Bat/">
  <img src="https://img.shields.io/badge/🦇%20PLAY%20SLEEPY%20BAT-LIVE%20DEMO-8A2BE2?style=for-the-badge&logo=github&logoColor=white">
</a>

<br><br>

<img src="https://img.shields.io/badge/HTML5-Canvas-E34F26?style=for-the-badge&logo=html5&logoColor=white">
<img src="https://img.shields.io/badge/JavaScript-Game%20Logic-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black">
<img src="https://img.shields.io/badge/Unity-6.x-000000?style=for-the-badge&logo=unity&logoColor=white">
<img src="https://img.shields.io/badge/C%23-Gameplay-239120?style=for-the-badge&logo=csharp&logoColor=white">

<br>

<img src="https://img.shields.io/badge/Web-Browser-4285F4?style=for-the-badge&logo=googlechrome&logoColor=white">
<img src="https://img.shields.io/badge/Windows-EXE-0078D6?style=for-the-badge&logo=windows&logoColor=white">
<img src="https://img.shields.io/badge/Android-APK-3DDC84?style=for-the-badge&logo=android&logoColor=white">
<img src="https://img.shields.io/badge/Pixel%20Style-Arcade-6F42C1?style=for-the-badge">

<br><br>

<a href="https://punitpritam788.github.io/Sleepy-Bat/">
  <img src="https://img.shields.io/badge/🎮%20PLAY%20NOW-Sleepy%20Bat-6f2dbd?style=for-the-badge">
</a>

</div>

---

<div align="center">

# 🌙 WHAT IS SLEEPY BAT?

</div>

<div align="center">

**Sleepy Bat** is a fast-paced arcade survival game built around one deceptively simple idea:

### 🦇 **When the bat rises, the cave disappears.**

### 👁️ **When the bat falls, echolocation reveals the danger.**

You control a sleepy little bat flying through a dangerous cave while the game constantly tries to disrupt your rhythm.

Every flap changes your movement.

Your movement changes your visibility.

Your visibility changes what you can react to.

And the longer you survive...

**the more the game fights back.**

</div>

---

<div align="center">

# 🎮 GAMEPLAY

</div>

<div align="center">

The objective is simple:

### **FLY → SURVIVE → PASS PILLARS → SCORE → DON'T DIE**

But the game combines multiple systems to make those simple controls increasingly difficult.

<br>

🦇 One-button flying
👁️ Echolocation-style visibility
🪨 Randomized cave pillars
🔄 Moving obstacles
🎯 Last-second gap shifts
📈 Progressive difficulty
⚡ Temporary control glitches
💢 Gravity pressure events
💀 Death animations
💥 Particle effects
📳 Screen shake
🔊 Procedural audio
🏆 Persistent score tracking
😈 Rage-game taunts

</div>

---

<div align="center">

# 👁️ ECHOLOCATION SYSTEM

</div>

<div align="center">

The defining mechanic of **Sleepy Bat** is the visibility system.

The cave isn't always fully visible.

Instead, the bat's vertical movement determines how much information the player receives.

<br>

### 🪽 WHILE RISING

**The cave becomes harder to see.**

Pillars and caps can vanish from view, forcing the player to fly partially blind.

<br>

### 🦇 WHILE FALLING

**Echolocation activates.**

The cave becomes visible again, with pillar visibility using a flickering / reveal effect.

<br>

### THE GAMEPLAY LOOP

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

This converts the player's own movement into part of the information system.

</div>

---

<div align="center">

# 🪨 DYNAMIC PILLAR SYSTEM

</div>

<div align="center">

Sleepy Bat doesn't rely on completely static obstacles.

The pillar generation system introduces randomness and movement to make every attempt less predictable.

</div>

<div align="center">

### RANDOMIZED SPAWNING

Each pillar can receive randomized:

**Horizontal spacing • Vertical position • Gap size • Movement behavior • Timing**

<br>

### 🔄 MOVING PILLARS

Some pillars smoothly move vertically rather than staying in one position.

<br>

### 🎯 FAKE-OUT MOVEMENT

Pillars can shift shortly before the player reaches them.

The movement is designed to create those:

> **"WAIT... DID THAT JUST MOVE?"**

moments.

<br>

### 🧩 TIGHT PATTERNS

Some obstacle sequences can spawn closer together, reducing the amount of recovery time available to the player.

</div>

---

<div align="center">

# 📈 PROGRESSIVE DIFFICULTY

</div>

<div align="center">

The difficulty doesn't simply increase by making everything faster.

Multiple gameplay variables change as the score rises.

<br>

### GAP SIZE

Higher score → smaller safe openings.

<br>

### MOVEMENT SPEED

Higher score → faster obstacle movement.

<br>

### OBSTACLE DENSITY

Higher score → less recovery time between some sequences.

<br>

### MOVEMENT EVENTS

Moving and fake-out behavior can become more important as the run progresses.

<br>

### PRESSURE EVENTS

Later sections can introduce temporary gravity-pressure effects.

</div>

---

<div align="center">

# ⚡ RAGE MECHANICS

</div>

<div align="center">

Sleepy Bat intentionally contains mechanics that can make the player question whether the game is helping them...

It isn't.

</div>

<div align="center">

### 1️⃣ RETRY TROLL

After dying, the game can sometimes reject a retry input.

Example response:

### **LOL. NO.**

<br>

### 2️⃣ FLAP GLITCH

Temporary control glitches can affect the flap response.

Possible behaviors include:

**WEAK FLAP**

or

**INVERTED / DISTORTED FLAP BEHAVIOR**

<br>

### 3️⃣ GRAVITY PRESSURE

Temporary gravity increases can make the bat fall faster than expected.

That means a timing pattern that worked before...

**might suddenly stop working.**

</div>

---

<div align="center">

# 💀 DEATH SYSTEM

</div>

<div align="center">

Death is treated as an actual gameplay event rather than simply stopping movement.

When the bat dies, multiple effects can trigger:

<br>

🖼️ **Dedicated Death Frame**
💥 **Particle Burst**
📳 **Screen Shake**
🔊 **Death Audio**
💬 **Random Death Message**
🧮 **Death Counter**
🏆 **Best Score Update**

<br>

Possible death messages include:

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

<br>

And when the game reaches the final score:

### **999. THAT'S IT. THAT'S ALL YOU GET.**

</div>

---

<div align="center">

# 🏆 SCORE SYSTEM

</div>

<div align="center">

Every successfully cleared pillar increases your score.

The game keeps track of:

### SCORE

### BEST SCORE

### TOTAL DEATHS

<br>

The score also drives the difficulty system.

The implementations use persistent local storage:

### HTML5

`localStorage`

### Unity

`PlayerPrefs`

</div>

---

<div align="center">

# 🕹️ CONTROLS

</div>

<div align="center">

## 💻 WINDOWS / PC

|     INPUT     |        ACTION        |
| :-----------: | :------------------: |
|    `SPACE`    |        🦇 Flap       |
|     `↑ UP`    |        🦇 Flap       |
| `MOUSE CLICK` |        🦇 Flap       |
|     `ESC`     | ⏸️ Pause / Exit Flow |

<br>

## 📱 ANDROID

|     INPUT     |        ACTION        |
| :-----------: | :------------------: |
|     `TAP`     |        🦇 Flap       |
| `SYSTEM BACK` | ⏸️ Pause / Exit Flow |

</div>

---

<div align="center">

# ⏸️ PAUSE / EXIT SYSTEM

</div>

<div align="center">

The Unity implementation includes a back/escape flow designed for both Windows and Android.

```text
ACTIVE GAME
    ↓
BACK / ESC
    ↓
PAUSE / EXIT SCREEN
    ↓
TAP / SPACE / UP
    ↓
FRESH RUN
```

A second Back / ESC input while the pause screen is open exits the application.

</div>

---

<div align="center">

# 🧠 TECHNICAL ARCHITECTURE

## 🌐 HTML5 VERSION

</div>

<div align="center">

The standalone browser build uses:

### `HTML5`

### `CSS3`

### `JavaScript`

### `HTML5 Canvas`

### `Web Audio API`

### `Keyboard Input`

### `Pointer / Touch Input`

### `localStorage`

### `requestAnimationFrame`

</div>

<div align="center">

### Browser Game Loop

```text
INPUT
  ↓
GAME STATE
  ↓
PHYSICS
  ↓
PILLAR SIMULATION
  ↓
COLLISION DETECTION
  ↓
ECHO / VISIBILITY STATE
  ↓
CANVAS RENDERING
  ↓
requestAnimationFrame
```

</div>

---

<div align="center">

# 🛠️ UNITY VERSION

</div>

<div align="center">

The Unity version is designed around an actual runtime scene instead of relying only on GUI drawing.

It dynamically creates:

<br>

📷 **Camera**
🎨 **Background SpriteRenderers**
🪨 **Ground / Top SpriteRenderers**
🏗️ **Pillar GameObjects**
🦇 **Bird SpriteRenderer**
💥 **ParticleSystem**
🖥️ **HUD Canvas**
🔊 **Audio Sources**
⚡ **Glitch Overlay**

</div>

---

<div align="center">

# 🎨 SPRITE-BASED RENDERING

</div>

<div align="center">

The Unity implementation supports sprite arrays for:

<br>

🖼️ Background frames
🦇 Bird animation frames
🪨 Pillar frames
🧢 Pillar cap frames
🌑 Ground frames
⬆️ Top cave frames
💀 Death frame

<br>

A single sprite can be used for static artwork, while multiple sprites can be used as frame-based animation.

</div>

---

<div align="center">

# 📐 RESPONSIVE PIXEL SCALING

</div>

<div align="center">

The Unity implementation uses a reference-height scaling system so gameplay remains consistent across different display sizes.

### Reference Height

`900 px`

<br>

Gameplay properties such as:

**Gravity • Bird Size • Pillar Size • Speed • Collision Padding • Gap Distance**

are scaled according to the current display.

This allows the same core gameplay logic to work across multiple aspect ratios and screen sizes.

</div>

---

<div align="center">

# 🪨 TEXTURE-BASED COLLISION

</div>

<div align="center">

The cave isn't required to behave like a perfectly flat rectangle.

The Unity implementation can analyze the alpha information from the cave artwork and build a height profile.

```text
CAVE TEXTURE
      ↓
READ ALPHA PIXELS
      ↓
SAMPLE HORIZONTAL POSITIONS
      ↓
BUILD HEIGHT PROFILE
      ↓
SAMPLE COLLISION HEIGHT
      ↓
GAMEPLAY COLLISION
```

This allows irregular cave artwork to influence collision boundaries.

</div>

---

<div align="center">

# 🎥 SCROLLING & PARALLAX

</div>

<div align="center">

Different environmental layers can use different scrolling speeds.

```text
BACKGROUND
   ↓
GROUND
   ↓
TOP CAVE
   ↓
PILLARS
```

This produces a layered arcade-style movement effect while keeping the endless-run presentation.

</div>

---

<div align="center">

# 🔊 PROCEDURAL AUDIO

</div>

<div align="center">

The game can generate lightweight audio at runtime rather than relying entirely on large collections of external sound files.

The Unity version creates tones using different waveform types such as:

<br>

**Sine • Triangle • Saw • Square**

<br>

Gameplay events can trigger sounds for:

🪽 Flap
⚡ Glitch Flap
✅ Pillar Pass
💀 Death
⚠️ Glitch Warning
😈 Troll
😴 Sleepy / Special Death

<br>

The HTML version uses browser audio capabilities for lightweight runtime sound generation.

</div>

---

<div align="center">

# 💥 PARTICLE EFFECTS

</div>

<div align="center">

When the bat dies, a short particle burst can be triggered.

```text
COLLISION
   ↓
DEATH
   ↓
PARTICLE BURST
   ↓
SCREEN SHAKE
   ↓
DEATH FRAME
   ↓
DEATH MESSAGE
```

The effect is intentionally fast and arcade-like.

</div>

---

<div align="center">

# 🧩 GAME STATE

</div>

<div align="center">

The game revolves around a compact state-driven architecture.

```text
TITLE
  ↓
RUNNING
  ↓
DEAD
  ↓
RETRY
```

The Unity implementation additionally supports:

```text
PAUSED / EXIT SCREEN
```

Important state data includes:

`running`
`dead`
`score`
`best`
`deaths`
`pausedForExit`
`gameTime`
`scrollTime`
`glitchUntil`
`glitchInvert`
`trollBlock`
`echoVisible`

</div>

---

<div align="center">

# 🧮 CORE GAMEPLAY LOGIC

</div>

<div align="center">

The game uses score-driven gameplay functions instead of one static difficulty configuration.

### GAP

```text
Higher Score
      ↓
Smaller Gap
```

### SPEED

```text
Higher Score
      ↓
Faster Pillars
```

### ECHO

```text
Vertical Velocity
      ↓
Echo Visibility State
      ↓
Obstacle Readability
```

### PILLAR MOVEMENT

```text
Random Spawn
      +
Smooth Drift
      +
Pre-Contact Movement
      +
Last-Second Fake-Out
```

</div>

---

<div align="center">

# 🚀 PERFORMANCE APPROACH

</div>

<div align="center">

The project focuses on lightweight real-time techniques.

<br>

### ♻️ OBJECT POOLING

Reusable pillar objects reduce unnecessary runtime creation.

### 🔁 TWO-PANEL SCROLLING

Paired background layers create an endless scrolling loop.

### 💥 LIGHTWEIGHT PARTICLES

Death effects are intentionally short-lived.

### 🔊 PROCEDURAL AUDIO

Short generated tones reduce dependence on large audio libraries.

### 📱 RESPONSIVE SCALING

The gameplay adapts to different display sizes.

### 🌐 STANDALONE WEB BUILD

The HTML implementation can embed artwork directly into the file.

</div>

---

<div align="center">

# 📁 PROJECT STRUCTURE

</div>

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
    ├── echo-mode.png
    ├── rage-mode.png
    └── death-screen.png
```

---

<div align="center">

# ▶️ PLAY THE GAME

### 🌐 LIVE WEB DEMO

<a href="https://punitpritam788.github.io/Sleepy-Bat/">

<img src="https://img.shields.io/badge/🦇%20PLAY%20SLEEPY%20BAT-LIVE%20DEMO-8A2BE2?style=for-the-badge&logo=github&logoColor=white">

</a>

<br>

**https://punitpritam788.github.io/Sleepy-Bat/**

<br><br>

### 🖥️ WINDOWS

Add your Windows `.exe` build to the GitHub Releases section.

<br>

### 📱 ANDROID

Add your Android `.apk` build to the GitHub Releases section.

</div>

---

<div align="center">

# 📸 SCREENSHOTS

</div>

<div align="center">

Add your screenshots here:

```md
<p align="center">
  <img src="Screenshots/gameplay.png" width="45%">
  <img src="Screenshots/echo-mode.png" width="45%">
</p>

<p align="center">
  <img src="Screenshots/rage-mode.png" width="45%">
  <img src="Screenshots/death-screen.png" width="45%">
</p>
```

</div>

---

<div align="center">

# 🎬 GAMEPLAY VIDEO / GIF

</div>

<div align="center">

You can add a gameplay GIF or video preview directly to the README:

```md
<p align="center">
  <img src="Screenshots/gameplay.gif" width="80%">
</p>
```

</div>

---

<div align="center">

# 🧰 TECHNOLOGY STACK

|          WEB          |        UNITY        |
| :-------------------: | :-----------------: |
|         HTML5         |      Unity 6.x      |
|          CSS3         |          C#         |
|       JavaScript      |    SpriteRenderer   |
|       Canvas API      |      Canvas UI      |
|     Web Audio API     |    ParticleSystem   |
| Pointer / Touch Input |     AudioSource     |
|     Keyboard Input    |      AudioClip      |
|      localStorage     |     PlayerPrefs     |
| requestAnimationFrame | Orthographic Camera |

</div>

---

<div align="center">

# ✨ FEATURES

| FEATURE                      | STATUS |
| :--------------------------- | :----: |
| 🦇 One-button gameplay       |    ✅   |
| 📱 Touch support             |    ✅   |
| ⌨️ Keyboard support          |    ✅   |
| 📐 Responsive scaling        |    ✅   |
| 👁️ Echolocation mechanic    |    ✅   |
| 🌑 Hidden obstacles          |    ✅   |
| ✨ Flickering obstacle reveal |    ✅   |
| 🪨 Random pillar generation  |    ✅   |
| 🔄 Moving pillars            |    ✅   |
| 🎯 Last-second fake-outs     |    ✅   |
| 📈 Progressive difficulty    |    ✅   |
| ⚡ Flap glitches              |    ✅   |
| 💢 Gravity pressure events   |    ✅   |
| 😈 Retry trolling            |    ✅   |
| 💀 Death frame               |    ✅   |
| 💥 Death particles           |    ✅   |
| 📳 Screen shake              |    ✅   |
| 🔊 Procedural audio          |    ✅   |
| 🏆 Persistent best score     |    ✅   |
| ☠️ Death counter             |    ✅   |
| 🌐 Browser version           |    ✅   |
| 🖥️ Windows version          |    ✅   |
| 📱 Android version           |    ✅   |

</div>

---

<div align="center">

# 🦇 DESIGN PHILOSOPHY

### **SIMPLE CONTROLS + UNPREDICTABLE CONSEQUENCES**

The player essentially has one main action:

# `FLAP`

But that one action affects:

```text
Vertical Velocity
      ↓
Echo Visibility
      ↓
Obstacle Readability
      ↓
Reaction Timing
      ↓
Survival
```

The result is a simple control scheme with a surprisingly layered difficulty curve.

</div>

---

<div align="center">

# 💜 VISUAL IDENTITY

Sleepy Bat combines:

🌑 Dark cave environments
💜 Purple / violet atmosphere
🟪 Pixel-oriented rendering
🦇 Cute sleepy character design
✨ Flickering visibility effects
📺 Arcade / CRT-inspired presentation
💀 Comedic rage-game feedback

The contrast between a cute sleepy bat and deliberately frustrating mechanics is a core part of the game's personality.

</div>

---

<div align="center">

# 😴 WHY "SLEEPY BAT"?

The bat doesn't want to save the world.

It doesn't want to become a hero.

It doesn't even want to fly.

### It just wants to sleep.

Unfortunately...

## **THE CAVE HAS OTHER PLANS.**

</div>

---

<div align="center">

# 👨‍💻 PROJECT

**Sleepy Bat** is an experimental arcade game project focused on combining:

### 🎮 Arcade Gameplay

### 🧠 Procedural Systems

### 🎨 Pixel Art

### ⚡ Real-Time Effects

### 🔊 Procedural Audio

### 📱 Multi-Platform Deployment

It was developed in two forms:

```text
HTML5 / JavaScript
        +
Unity 6.x / C#
```

The browser version provides a lightweight standalone web implementation, while the Unity version provides a game-engine implementation suitable for desktop and mobile builds.

</div>

---

<div align="center">

# ❤️ BUILT FOR FUN, FRUSTRATION & SLEEP DEPRIVATION

If you made it this far...

you probably haven't survived **999** yet.

<br>

## 🦇 FLAP.

## 🌑 FALL.

## 👁️ LISTEN.

## 💀 DIE.

## 🔁 TRY AGAIN.

<br>

# **I NEED SOME SLEEP.**

<br>

<a href="https://punitpritam788.github.io/Sleepy-Bat/">
<img src="https://img.shields.io/badge/🎮%20PLAY%20NOW-SLEEPY%20BAT-8A2BE2?style=for-the-badge&logo=github&logoColor=white">
</a>

<br><br>

<strong>🦇 Sleepy Bat • Dark Arcade • Rage Game • Pixel Survival</strong>

<br>

<sub>Built with HTML5 Canvas + JavaScript and Unity 6.x</sub>

</div>

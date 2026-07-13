# NETA JI 3D

Android-first third-person prototype for **NETA JI: Rise of a Leader**.

## Prototype 1

- Azad third-person movement with a collision-aware camera
- Android touch controls and PC keyboard/mouse controls
- Daraganj/ghat-inspired greybox
- Hinglish NPC interaction and ordered mission objectives
- Public Trust, Money, and Reputation progression
- Local JSON save data
- First chapter: `Ravivaar Ki Seva: Ghat Se Ghar Tak`
- Story introductions for Shanti, Sandhya, Constable Samrat, and Helpers Hand
- Objective direction and distance guidance for mobile play
- Animated boats/trees plus procedural ghat ambience, footsteps, and mission chimes
- Role-specific character details and labeled Daraganj, home, and NGO landmarks
- Full-screen 3D title scene with Continue, New Game, and Azad story modal
- Save-compatible chapter selection with unlock and resume state
- Second playable chapter: `Shaam Ki Paathshala`, with nine community-education objectives
- Third playable chapter: `Sandhya Kahan Hai?`, with evidence-led community search objectives
- Save v3 migration, three-chapter unlock flow, and deterministic chapter smoke tests
- Reusable two-option decision UI with persisted story consequences
- Fourth playable chapter: `Operation Umeed`, a non-graphic police-led rescue
- Save v4 migration and independent safe/risky branch verification
- Persistent `PROOF` stat for documents, evidence chains, and future opposition cases
- Fifth playable chapter: `Dawa Ka Sach`, a fictional hospital-supply investigation
- Save v5 migration and independently verified case-file/public-post outcomes
- Persistent `POWER`, `TEAM`, and `PRESSURE` political-organization stats
- Sixth playable chapter: `India Helping Party`, covering accountable party foundation
- Save v6 migration and independently verified dialogue/confrontation opposition outcomes

The existing browser/PWA game remains separate in the repository root. This Unity project does not depend on its capture-game code.

## Editor

- Unity `6000.3.18f1` (Unity 6.3 LTS)
- Built-in render pipeline for the first low-spec prototype
- Android Build Support, OpenJDK 17, SDK 36, and NDK r27c installed

## Verified Builds

- Windows: `Builds/Windows/NETA-JI-Prototype.exe`
- Android: `Builds/Android/NETA-JI-Prototype.apk`
- Android development APK: Mono, ARMv7, minimum Android 8 (API 26), target API 36
- Store/release builds will switch to IL2CPP ARM64 after the prototype is approved

The Android artifact is a debug-signed development build. Its package id is
`com.skenterprises.netaji.prototype`, version `0.7.0` (`versionCode 7`).

## Generate The Scene

Open the project once, then use `NETA JI > Build Prototype Scene`. The same action can run in batch mode:

```powershell
Unity.exe -batchmode -quit -projectPath . -executeMethod NetaJi.Prototype.Editor.PrototypeSceneBuilder.BuildFromCommandLine
```

The generated menu and chapter scenes are saved under `Assets/NETAJI/Scenes/`.

## Controls

- `WASD` / arrow keys: move
- Hold right mouse and drag: orbit camera
- `Shift`: run
- `E`: interact
- Android: left virtual stick, right-side swipe, and `HELP` button

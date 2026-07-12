# NETA JI 3D

Android-first third-person prototype for **NETA JI: Rise of a Leader**.

## Prototype 1

- Azad third-person movement with a collision-aware camera
- Android touch controls and PC keyboard/mouse controls
- Daraganj/ghat-inspired greybox
- Hinglish NPC interaction and ordered mission objectives
- Public Trust, Money, and Reputation progression
- Local JSON save data
- First mission: `Ravivaar Ki Seva`

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
`com.skenterprises.netaji.prototype`.

## Generate The Scene

Open the project once, then use `NETA JI > Build Prototype Scene`. The same action can run in batch mode:

```powershell
Unity.exe -batchmode -quit -projectPath . -executeMethod NetaJi.Prototype.Editor.PrototypeSceneBuilder.BuildFromCommandLine
```

The generated scene is saved as `Assets/NETAJI/Scenes/Prototype01.unity`.

## Controls

- `WASD` / arrow keys: move
- Hold right mouse and drag: orbit camera
- `Shift`: run
- `E`: interact
- Android: left virtual stick, right-side swipe, and `HELP` button

# Shared-World Chapter 1 - 0.30

## Player Flow

- Helpers Hand starts Chapter 1 in place; the active scene remains `FreeRoam` throughout the mission.
- Nine ordered objectives connect Helpers Hand, three Daraganj ghat cleanup points, the volunteer route, Azad's Allahpur home, the civic records district, and the final NGO pension-file desk.
- The player can walk or use the existing car/scooter between locations.
- Completion saves Chapter 1, updates the hub to `1/24`, and offers either continued Prayagraj exploration or Chapter 2.
- The original standalone Chapter 1 scene remains available for chapter review and regression coverage.

## Shared Runtime

- `MissionPresentation` decouples mission logic from one scene-specific HUD.
- `OpenWorldMissionDirector` activates and retires the Chapter 1 mission root without reloading the city.
- `OpenWorldMissionHud` provides compact mission, stats, route, dialogue, milestone, decision, and completion presentation.
- `FreeRoamMapHud` tracks the current objective with a pulsing marker and a labelled full-map destination.
- The same mission controller/objective logic and save rewards are retained, so later chapters can migrate incrementally.

## Verified Evidence

- The 844x390 and 1280x720 end-to-end smokes complete all nine objectives while asserting that the scene remains `FreeRoam`.
- Final values remain Trust 35, Funds Rs 950, and Reputation 16.
- The save-aware hub advances to Chapter 2 after an in-world return to Helpers Hand.
- Compact screenshots verify confirmation, mission start, full-map route, Daraganj, Allahpur, civic records, completion actions, and Chapter 2 readiness.
- Existing free-roam vehicle/audio/map, standalone Chapter 1, story-hub, and main-menu regressions pass.

## Android Artifact

- Package: `com.skenterprises.netaji.prototype`
- Version: `0.30.0` (`versionCode 30`)
- Android: min SDK 26, target/compile SDK 36, landscape orientation
- Native code: ARM64 and ARMv7 with `libil2cpp.so` and `libunity.so` for both ABIs
- APK size: 26,513,817 bytes (25.29 MiB)
- SHA-256: `5130D62EE6D537BA5BF5BE8D18217F9E028033249E5ED9ED6163C1F5A99E0FCC`
- The manifest has no debuggable attribute and APK Signature Scheme v2 verification passes.
- The signer remains the Android debug certificate for local/company-demo installs; production requires a private keystore and AAB.
- ADB found no connected physical phone, so install, touch feel, sustained FPS, thermals, battery use, and device-specific compatibility remain unverified.

## Deferred Work

Chapters 2-24 still use their dedicated scenes and will move into the shared world incrementally. Production character/environment assets, physical Android profiling, final signing, repository separation, and Google website deployment remain later milestones. No backend is required for this offline single-player checkpoint.

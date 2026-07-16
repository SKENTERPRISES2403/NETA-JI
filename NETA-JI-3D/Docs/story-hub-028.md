# Story Hub 0.28

## Connected Player Loop

- `NAYA SAFAR` still plays the seven-shot origin prologue and starts Chapter 1.
- `SEVA JAARI RAKHEIN` now opens Prayagraj beside the Helpers Hand story marker instead of jumping into an unexplained scene.
- Free exploration remains available through `PRAYAGRAJ GHUMEIN`.
- The Helpers Hand marker reads the save and offers the first incomplete unlocked chapter.
- A confirmation panel shows chapter number, title, completed count, and explicit start/cancel actions.
- A completed chapter offers `PRAYAGRAJ WAPAS`, next chapter, and main-menu actions.
- Returning to Prayagraj places Azad at Helpers Hand, refreshes the HUD, and offers the newly unlocked chapter.
- Direct chapter selection remains available for review and testing.

## Runtime Integration

- `StoryChapterCatalog` is the shared source for all 24 titles and scene names.
- `StoryHubController` owns marker interaction, save-aware chapter selection, confirmation UI, and return placement.
- `StoryHubFlow` carries the return request across scene loads without a backend.
- `GameSession.IsChapterComplete` provides one completion query across all 24 save fields.
- The mobile HUD displays current chapter, story title, and completed count while walking or driving.
- Modal presentation suppresses the map and status HUD so compact screens do not obscure mission text.

## Verified Build Evidence

- Unity Windows release build completed successfully with no script compiler errors.
- The 844x390 free-roam smoke passed live map, story marker, car, scooter, rider visibility, wheel contact, and exit lifecycle checks.
- The end-to-end story smoke launched Chapter 1 from Helpers Hand, completed it at Trust 35 / Funds Rs 950 / Reputation 16, persisted the save, returned near the marker, displayed `1/24`, and offered Chapter 2.
- Confirmation, return, driving, scooter, and map screenshots were visually checked for clipping and incoherent overlap.
- Android APK: `0.28.0`, `versionCode 28`, package `com.skenterprises.netaji.prototype`, min SDK 26, target/compile SDK 36.
- APK size: 26,206,696 bytes. SHA-256: `E955174FE592DD23BA4BFAF6F9DB63D652ADD39D27A13CF353013B07205A6C88`.
- APK contains ARM64 and ARMv7 `libil2cpp.so` and `libunity.so`, has no debuggable manifest flag, and verifies with Android APK Signature Scheme v2.

## Remaining Production Work

The APK uses the Android debug certificate for local and company-demo installs. Production release still needs a private keystore, an Android App Bundle, store assets, and policy review. No physical Android phone was connected, so touch feel, sustained FPS, thermals, battery use, and device-specific compatibility are not yet proven. Custom rigged characters, authored animation, denser traffic and crowds, stronger architecture, and final environmental sound remain the largest presentation upgrades.

Google website deployment and separating the old color-fill PWA from this Unity game are intentionally deferred until the game is substantially complete.

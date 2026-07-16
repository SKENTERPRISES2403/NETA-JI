# NETA JI APK Prep

## Current Position

NETA JI is currently a static, installable PWA. APK packaging should start only after the public PWA demo is stable on real phones. The repo now includes APK/PWA-ready PNG icons generated from `tools/generate-icons.cjs`.

## Recommended Path

1. Keep the web/PWA as the source of truth.
2. Package with a lightweight Android wrapper later, using either a Trusted Web Activity or a Capacitor-style WebView shell.
3. Use the public demo URL for review builds until native-only features are required.

## Required Before APK

- Final app icon, splash screen, and adaptive icon assets. Current placeholders: `assets/icon-192.png`, `assets/icon-512.png`, and `assets/icon-maskable-512.png`.
- Mobile gameplay check on low-end Android, mid-range Android, and one large-screen device.
- Offline cache check after install.
- Poster save/share check on Android browser and APK shell.
- Touch controls checked for gesture navigation phones.
- Safety text and privacy copy reviewed.

## Build Checklist

- Run `npm.cmd run icons` after changing app branding.
- Confirm `npm.cmd run build` passes.
- Run `npm.cmd run smoke:mobile` for local mobile/desktop screenshots.
- Run `npm.cmd run smoke:public` after deploy.
- Confirm `manifest.webmanifest` has correct app name, icons, orientation, theme color, and display mode.
- Verify portrait and landscape gameplay because the PWA manifest intentionally allows `orientation: any`.
- Confirm `sw.js` cache version is bumped for every public release.
- Generate signed internal test APK.
- Install APK on a real Android phone.
- Test `?demo=1`, fresh setup, state selection, yatra movement, win result, poster save, reset progress.

## Not In This Stage

- Multiplayer.
- Real user accounts.
- Real-money purchases.
- Public user-generated poster hosting.

# NETA JI Mobile Test Checklist

Use this before showing the game to a company reviewer.

## Automated Smoke

```powershell
npm.cmd run icons
npm.cmd run build
npm.cmd run logic:qa
npm.cmd run map:qa
npm.cmd run smoke:mobile
npm.cmd run smoke:map
npm.cmd run smoke:arena
```

After public deploy:

```powershell
npm.cmd run smoke:public
npm.cmd run smoke:public:map
npm.cmd run smoke:public:arena
```

The smoke script checks the page, manifest, service worker, main game code, and captures mobile plus desktop screenshots under `dist/smoke/`.
The logic QA proves that a small closed loop captures only its enclosed cells and an open route captures nothing. The map QA verifies all 36 regions, normalized coordinates, source/license metadata, mobile point budget, and polygon hit centroids.
Map data lives in `data/india-map-shapes.json`; see `docs/map-data.md`.

## Real Phone Pass

- Open `https://neta-ji-demo.sd-skenterprises.chatgpt.site/?demo=1`.
- Confirm Quick Demo opens without popup overlap.
- Tap Next through onboarding, then OK for Uttar Pradesh.
- Tap a few tricky map regions: Goa, Delhi, Sikkim, Assam, Tripura, Puducherry, and Andaman/Nicobar.
- Swipe up, down, left, and right inside the arena.
- Close a small loop and confirm only the enclosed patch changes color.
- Confirm one small loop does not end the round or color the whole state.
- Confirm a body collision does not convert a rival; cutting the rival's dark route does.
- Trigger at least one funny event.
- Cut one rival route and confirm supporter conversion feels readable.
- Win the state and confirm the red flag/victory flow.
- Install from browser menu and relaunch.
- Turn off data after first load and confirm the cached app still opens.

## Devices To Cover

- Low-end Android phone.
- Mid-range Android phone.
- Large-screen Android or tablet.
- One laptop browser for desktop layout.

## Evidence Still Requiring A Physical Device

- Sustained 60-second swipe session with no accidental page scroll.
- Haptic strength and sound balance on an actual Android handset.
- PWA install/relaunch and offline start after first load.
- Controls fully visible at 320px, 360px, and 390px CSS widths.

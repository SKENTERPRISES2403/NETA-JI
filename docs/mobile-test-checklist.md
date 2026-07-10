# NETA JI Mobile Test Checklist

Use this before showing the game to a company reviewer.

## Automated Smoke

```powershell
npm.cmd run icons
npm.cmd run build
npm.cmd run smoke:mobile
```

After public deploy:

```powershell
npm.cmd run smoke:public
```

The smoke script checks the page, manifest, service worker, main game code, and captures mobile plus desktop screenshots under `dist/smoke/`.

## Real Phone Pass

- Open `https://neta-ji-demo.sd-skenterprises.chatgpt.site/?demo=1`.
- Confirm Quick Demo opens without popup overlap.
- Tap Next through onboarding, then OK for Uttar Pradesh.
- Tap a few tricky map regions: Goa, Delhi, Sikkim, Assam, Tripura, Puducherry, and Andaman/Nicobar.
- Swipe up, down, left, and right inside the arena.
- Close a loop and confirm influence increases.
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

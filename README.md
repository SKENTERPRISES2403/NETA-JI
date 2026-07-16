# NETA JI

NETA JI is a mobile-first political comedy capture game inspired by simple territory games. The player creates a fictional party, picks a color and symbol, runs campaign routes, and wins areas by closing loops on the map.

The tone is intentionally fictional, light, and meme-friendly. The game must not use real parties, real leaders, real party symbols, real slogans, caste/religion targeting, or hate content.

## Current Build

- Static HTML5 canvas game
- Works on mobile and laptop browsers
- Party name, color, slogan, and symbol customization
- Paper-style campaign route and territory fill
- Fictional AI opponents
- Opponent conversion into supporters
- Miniature leader, opponent candidate, and supporter worker visuals
- Rally booths, poster crews, booth queues, walking yatra routes, flags, and stage props inside arenas
- Mobile-smoothed canvas rendering with capped DPR, cached sizing, and tuned swipe controls
- Textured arena ground, shaded territory, rounded campaign trails, and player motion dust
- Neta Meter with public support, funds, local power, and reputation
- Campaign decisions: Tea Rally, Poster Wave, and Donor Lunch tradeoffs
- Real-shape India state/UT campaign hub drawn in canvas
- Open-license geoBoundaries ADM1 silhouettes for all 28 states and 8 union territories
- Correct geographic placement, polygon hit targets, source metadata, and visible attribution
- The same real region silhouette expands to fill the gameplay arena after selection
- Flag-touch state/UT selection with confirmation
- Small-loop capture is state-edge aware and cannot color the whole arena by mistake
- Harder win pacing: multiple meaningful loops plus mandate score are required
- Rival conversion requires cutting a rival route; touching the rival body alone is not enough
- State/UT win progression with saved progress
- Paidal-yatra style next-region flow
- National mandate completion when all 36 regions are won, with world-yatra teaser
- Random comedy campaign events
- Safety filter for party names and slogans
- Obfuscation-aware filtering for real politics, hate, abuse, and sensitive identity terms
- Meme-style result card with share text
- Party-colored result card with mandate stamp and symbol badge
- Lightweight generated sound effects
- PWA manifest, service worker, and in-game install button for mobile app mode
- PNG 192/512/maskable icons for PWA/APK packaging prep
- Safe-area mobile layout support for notch/gesture-bar phones
- Pause/resume support on mobile button and P key
- Auto-pause when the app/tab is hidden
- Quick Demo and `?demo=1` pitch flow for company/mobile demos
- In-app pitch card and reviewer notes in [docs/pitch.md](docs/pitch.md)
- APK prep notes in [docs/apk-prep.md](docs/apk-prep.md)
- Mobile QA checklist in [docs/mobile-test-checklist.md](docs/mobile-test-checklist.md)
- Production moderation roadmap in [docs/production-roadmap.md](docs/production-roadmap.md)
- Third-party map attribution in [docs/third-party-notices.md](docs/third-party-notices.md)
- Offline-demo privacy note in [docs/privacy.md](docs/privacy.md)

## 3D Story Prototype

`NETA-JI-3D` is the separate Unity 6 Android-first story prototype. It follows Azad from community service in Prayagraj through fictional local, state, and national public leadership.

- 24 playable, save-backed chapters with a complete Prototype 1 story arc
- Semi-realistic stylized third-person movement, mobile touch controls, dialogue, decisions, missions, and chapter selection
- Seven-shot, wordless opening cinematic establishes Azad's family, public service, crisis, recovery, and resolve without graphic violence
- Connected Prayagraj free-roam district with homes, public buildings, market, park, ghats, clean riverfront, stalls, roads, trees, and pedestrians
- Live top-left minimap plus a full-screen labelled city map that tracks the player
- Enter, drive, and exit car and scooter vehicles with mobile/keyboard controls and speed HUD
- Save-aware Helpers Hand story hub launches the next incomplete chapter, returns Azad to Prayagraj after completion, and advances the visible 24-chapter progress
- Chapter 10 political outfit is consistent across front, back, left, and right views
- Safe and risky branches with computed public-support, governance, election, development, and global-cooperation outcomes
- Final Chapter 24 earns the fictional Vishwa Guru outcome through fair trade, open science, defensive peace, and humanitarian-climate leadership rather than conquest
- Unity version `0.28.0`, Android `versionCode 28`, min SDK 26, target SDK 36
- Clean demo APK contains ARM64 and ARMv7 IL2CPP libraries, has no development-build flag, and passes Android v2 signature verification
- Windows release automation covers the menu, all seven prologue shots, free-roam vehicles/map, the hub-to-Chapter-1-to-hub progression loop, outfit views, and both Chapter 24 routes at compact 844x390 landscape presentation
- Current art is a procedural low-poly prototype, not final GTA-level production art
- Physical Android install, thermals, touch feel, and device-specific performance still require a connected phone

See [NETA-JI-3D/Docs/prototype-1.md](NETA-JI-3D/Docs/prototype-1.md), [NETA-JI-3D/Docs/open-world-027.md](NETA-JI-3D/Docs/open-world-027.md), [NETA-JI-3D/Docs/story-hub-028.md](NETA-JI-3D/Docs/story-hub-028.md), and [NETA-JI-3D/Docs/chapter-24.md](NETA-JI-3D/Docs/chapter-24.md).

## Run Locally

Use any static server from the repo root:

```powershell
python -m http.server 5173
```

Or use the included no-cache dev server:

```powershell
node dev-server.cjs 5174 0.0.0.0
```

Then open:

```text
http://localhost:5174
```

Generate app icons and run a local mobile smoke check:

```powershell
npm.cmd run icons
npm.cmd run build
npm.cmd run logic:qa
npm.cmd run map:qa
npm.cmd run smoke:mobile
npm.cmd run smoke:map
npm.cmd run smoke:arena
```

## Public Demo

Current pitch demo:

```text
https://neta-ji-demo.sd-skenterprises.chatgpt.site/?demo=1
```

This is the preferred company/mobile review link. It avoids localhost issues and opens the safe Quick Demo flow.

QA views:

```text
?demo=1&mapqa=1
?demo=1&arenaqa=1
```

## Product Direction

Build order:

1. Playable district arena
2. Better capture algorithm and AI (current pitch pass complete)
3. Stronger crowd movement and follower polish
4. Deeper money, power, reputation, and public support systems
5. Real map geometry using open map data with CC attribution (complete)
6. PWA and Android APK
7. Optional multiplayer after the offline version is fun

## Safety Direction

NETA JI should feel like a funny fictional election game, not a real politics simulator. See [docs/safety-policy.md](docs/safety-policy.md).

## Pitch Demo

For a company-style demo, use the `Quick Demo` button or open the PWA with:

```text
?demo=1
```

See [docs/pitch.md](docs/pitch.md) for the pitch script, reviewer checklist, and monetization options.

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
- Rough India state/UT campaign hub
- Cartoon India map home screen drawn in canvas
- Flag-touch state/UT selection with confirmation
- Selected region opens as a large outlined gameplay arena
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
- Safe-area mobile layout support for notch/gesture-bar phones
- Pause/resume support on mobile button and P key
- Auto-pause when the app/tab is hidden
- Quick Demo and `?demo=1` pitch flow for company/mobile demos
- In-app pitch card and reviewer notes in [docs/pitch.md](docs/pitch.md)
- APK prep notes in [docs/apk-prep.md](docs/apk-prep.md)
- Production moderation roadmap in [docs/production-roadmap.md](docs/production-roadmap.md)

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
http://localhost:5173
```

## Public Demo

Current pitch demo:

```text
https://neta-ji-demo.sd-skenterprises.chatgpt.site/?demo=1
```

This is the preferred company/mobile review link. It avoids localhost issues and opens the safe Quick Demo flow.

## Product Direction

Build order:

1. Playable district arena
2. Better capture algorithm and AI
3. Stronger crowd movement and follower polish
4. Deeper money, power, reputation, and public support systems
5. Real map geometry using open map data after license review
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

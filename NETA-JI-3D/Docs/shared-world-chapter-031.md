# Shared World Chapter 2 - v0.31

## Scope

`Shaam Ki Paathshala` now runs inside the connected Prayagraj `FreeRoam` scene instead of loading an isolated mission arena. Chapter 1 can hand control directly to Chapter 2 while the player, city, minimap, camera, save state, and HUD remain alive.

## Playable Flow

1. Meet Shanti at the Allahpur courtyard and review the class plan.
2. Arrange two study desks and donated books.
3. Help Raju check a fictional admission form.
4. Coordinate a streetlight safety update with Constable Samrat.
5. Activate the temporary solar light and begin the evening lesson.
6. Report the completed class to the Helpers Hand coordinator.

The mission contains nine ordered interaction objectives and preserves the established final progression values: Trust 58, Funds Rs 1050, and Reputation 30.

## Presentation

- Temporary evening sky, fog, low warm sun, and courtyard point lights
- Community hall, blackboard, desks, books, neem tree, learners, parent, and coordinator staging
- Compact landscape HUD with route direction, distance, stats, dialogue, milestones, and completion actions
- Live objective marker on the minimap and full Prayagraj map
- Automatic atmosphere restoration when returning to free roam

All people, school details, records, and events remain fictional. The chapter contains community-service gameplay and no real-party messaging.

## Verification

- Shared-world Chapter 2 mobile smoke: passed at 844x390
- Chapter 1 to Chapter 2 same-scene continuity smoke: passed
- Chapter 1 regression: passed
- Free-roam map, car, scooter, ambient traffic, and audio regression: passed
- Standalone Chapter 2 fallback: passed
- Android `0.31.0` / code 31 build: passed
- Android package: API 26 minimum, API 36 target, ARM64 plus ARMv7 IL2CPP, non-debuggable, APK Signature Scheme v2
- APK SHA-256: `969F69C0B99479625A4586D2433533CC41456E26D6435DCB1BECAC2399713A81`

## Remaining

- Migrate Chapter 3 and later missions into the shared city framework
- Replace procedural prototype people and environment pieces with authored production assets
- Tune controls, performance, thermals, and touch feedback on physical Android phones
- Create a production keystore and store-ready AAB only after the game reaches release-candidate status
- Split the legacy color-fill game and deploy the finished game website only after the main game is substantially complete

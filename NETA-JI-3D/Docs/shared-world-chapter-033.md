# Shared World Chapter 4 - v0.33

## Scope

`Operation Umeed` now runs inside the connected Prayagraj `FreeRoam` scene. Chapter 3 can advance directly into the dawn rescue without reloading the city, while retaining the same player, camera, minimap, save, vehicles, ambience, and compact mission HUD.

## Playable Flow

1. Meet Constable Samrat at the fictional riverside perimeter.
2. Verify the suspect van and Sandhya's watch signal before entry.
3. Choose the coordinated police plan or the mechanically penalized solo route.
4. Unlock the police-controlled gate and send the medical-team signal.
5. Reach Sandhya, provide basic first aid, and move her to safety.
6. Seal the fictional payment ledger and complete the police evidence handover.
7. Reunite Sandhya with Shanti at the family safe point.

The coordinated route contains eleven ordered objectives and ends at Trust 83, Funds Rs 650, and Reputation 56.

## Safety And Presentation

- Temporary dawn atmosphere, warm perimeter lamps, a fictional open-front warehouse, police barricades, backup officers, medic, van, evidence props, and family safe point
- No combat, graphic harm, vigilantism, real allegation, real gang, or real political party
- The safer police-led route earns better trust and reputation than the risky solo route
- Evidence remains in a documented police chain; the family reunion is non-graphic and hopeful
- The minimap hides while the decision modal is open, preventing mobile UI overlap
- Dawn settings restore automatically when the mission ends

## Verification

- Shared-world Chapter 4 passed at 844x390 and 1280x720
- Eleven-objective flow, safe decision, exact rewards, save completion, dawn restoration, and Chapter 5 unlock passed
- Chapter 3 to Chapter 4 same-scene continuity passed
- Free-roam map, car, scooter, six ambient vehicles, physics, and location-aware audio regression passed
- Standalone Chapter 4 safe and risky branches passed
- Android `0.33.0` / code 33 build passed
- Android package: API 26 minimum, API 36 target, ARM64 plus ARMv7 IL2CPP, non-debuggable, APK Signature Scheme v2
- APK SHA-256: `2FD6FE35DCEF5AA70597DB971F5E22D56CC6C60BC180A349F6D23D2F7A6C2EEA`

## Remaining

- Migrate Chapter 5 and later missions into the shared city framework
- Replace procedural prototype people and environment pieces with authored production assets
- Tune controls, performance, thermals, and touch feedback on physical Android phones
- Create a production keystore and store-ready AAB only after release-candidate status
- Split the legacy color-fill game and deploy the finished game website only after the main game is substantially complete

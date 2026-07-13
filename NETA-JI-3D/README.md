# NETA JI 3D

Android-first third-person prototype for **NETA JI: Rise of a Leader**.

## Prototype 1

- Azad third-person movement with a collision-aware camera
- Android touch controls and PC keyboard/mouse controls
- Daraganj/ghat-inspired greybox
- Hinglish NPC interaction and ordered mission objectives
- Public Trust, Money, and Reputation progression
- Local JSON save data
- First chapter: `Ravivaar Ki Seva: Ghat Se Ghar Tak`
- Story introductions for Shanti, Sandhya, Constable Samrat, and Helpers Hand
- Objective direction and distance guidance for mobile play
- Animated boats/trees plus procedural ghat ambience, footsteps, and mission chimes
- Role-specific character details and labeled Daraganj, home, and NGO landmarks
- Full-screen 3D title scene with Continue, New Game, and Azad story modal
- Save-compatible chapter selection with unlock and resume state
- Second playable chapter: `Shaam Ki Paathshala`, with nine community-education objectives
- Third playable chapter: `Sandhya Kahan Hai?`, with evidence-led community search objectives
- Save v3 migration, three-chapter unlock flow, and deterministic chapter smoke tests
- Reusable two-option decision UI with persisted story consequences
- Fourth playable chapter: `Operation Umeed`, a non-graphic police-led rescue
- Save v4 migration and independent safe/risky branch verification
- Persistent `PROOF` stat for documents, evidence chains, and future opposition cases
- Fifth playable chapter: `Dawa Ka Sach`, a fictional hospital-supply investigation
- Save v5 migration and independently verified case-file/public-post outcomes
- Persistent `POWER`, `TEAM`, and `PRESSURE` political-organization stats
- Sixth playable chapter: `India Helping Party`, covering accountable party foundation
- Save v6 migration and independently verified dialogue/confrontation opposition outcomes
- Persistent ward `SUPPORT`, `BOOTH`, vote-share, and election-result state
- Reusable stat-driven local-election calculation with no scripted victory
- Seventh playable chapter: `Ward Ka Faisla`, covering an ethical ward campaign and polling day
- Save v7 migration and independently verified ground-campaign/mega-rally outcomes
- Persistent `DELIVERY`, `INTEGRITY`, public `BUDGET`, and 100-day `REVIEW` state
- Computed governance review with integrity and overspending gates instead of a scripted pass
- Eighth playable chapter: `Pehle 100 Din`, covering measurable local governance
- Save v8 migration and independently verified open-tender/shortcut outcomes
- Persistent constituency `REACH`, coalition `UNITY`, assembly `READY`, and computed `NOM` state
- Stats-driven MLA candidate nomination gated by the passed 100-day public audit
- Ninth playable chapter: `Vidhansabha Ki Raah`, covering ethical urban-rural expansion
- Save v9 migration and independently verified ground-jan-sabha/dhol-roadshow outcomes
- Persistent assembly `SUPPORT`, campaign `RULES`, polling `OPS`, vote share, and seat result
- Computed Vidhansabha election using nomination, organization, compliance, reputation, proof, and pressure
- Tenth playable chapter: `Janata Ka Mandate`, covering the full lawful assembly campaign
- Save v10 migration and independently verified issue-yatra/media-convoy outcomes
- Candidate-era Azad outfit with white kurta-pajama, teal-yellow stole, and chappals
- Persistent MLA `LEGISLATIVE`, constituency `SERVICE`, `ETHICS`, public allocation, and review state
- Computed MLA performance with election, ethics, fund-balance, governance, reputation, and pressure gates
- Eleventh playable chapter: `Janata Ka MLA`, covering constituency and legislative duty
- Save v11 migration and independently verified open-scorecard/closed-list outcomes
- Persistent district `REACH`, candidate `QUALITY`, organization `DISCIPLINE`, and expansion review state
- Computed district-network readiness gated by MLA performance, candidate quality, discipline, and pressure
- Twelfth playable chapter: `Zila Sangathan`, covering accountable district organization
- Save v12 migration and independently verified open-primary/closed-appointment outcomes
- Persistent state-campaign `REACH`, candidate-slate `INTEGRITY`, polling `OPS`, score, and seat result
- Computed eight-seat foothold gated by district readiness, clean candidates, operations, score, and seats won
- Thirteenth playable chapter: `Pradesh Ki Dastak`, covering a fictional multi-seat state campaign
- Save v13 migration and independently verified phased-field/all-seat-blitz outcomes

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
`com.skenterprises.netaji.prototype`, version `0.14.0` (`versionCode 14`).

## Generate The Scene

Open the project once, then use `NETA JI > Build Prototype Scene`. The same action can run in batch mode:

```powershell
Unity.exe -batchmode -quit -projectPath . -executeMethod NetaJi.Prototype.Editor.PrototypeSceneBuilder.BuildFromCommandLine
```

The generated menu and chapter scenes are saved under `Assets/NETAJI/Scenes/`.

## Controls

- `WASD` / arrow keys: move
- Hold right mouse and drag: orbit camera
- `Shift`: run
- `E`: interact
- Android: left virtual stick, right-side swipe, and `HELP` button

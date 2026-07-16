# Prayagraj World Polish 0.29

## Visual Upgrade

- The city ground now stops at the actual western river bank, so the clean Ganga is visible instead of being hidden below a grass slab.
- Ten continuous Daraganj steps descend from the ghat road to the waterline, with dark tread edges that remain readable on a compact mobile screen.
- Boats, animated sun glints, shade canopies, benches, trees, and food stalls make the riverfront usable as an exploration destination.
- Food stalls now have colored counter fronts, display shelves, serving plates, food pieces, and supply crates.
- Main and secondary roads have footpaths, lane markings, white crossings, and safety bollards.
- Reusable building facades now include a foundation, entrance step/canopy, side windows, balconies, floor bands, parapets, water tanks, and solar panels.
- Six fictional autos and buses move along separate city routes. They are lightweight ambient visuals and do not replace the driveable player car and scooter.
- Citizen zones now include the northern/southern ghats and Helpers Hand area in addition to market, park, university, residential, and commercial districts.

## Presentation And Sound

- `OpenWorldPresentation` applies a 60 FPS target and selects 0x/2x MSAA, shadow distance, cascades, and anisotropic filtering from mobile memory/GPU capability.
- Desktop verification applied 2x MSAA and 72m shadows; normal mobile devices target 2x/38m, while detected low-spec mobile devices use 0x/22m.
- `PrototypeAudio` retains mission feedback while adding procedural river, Loknath market, and traffic layers that crossfade from Azad's position.
- Distant fictional horn cues use generated tones; no copied commercial audio is included.

## Verified Evidence

- Unity Windows release build completed without script compiler errors.
- The 844x390 smoke visually captured ghat, market, civic district, map, story marker/modal, car, scooter, and exit states.
- Runtime assertions found all six ambient vehicles and measured movement, applied adaptive presentation, enabled location-aware audio, and retained grounded car/scooter physics.
- The full story regression launched Chapter 1 from Helpers Hand, completed it, saved progress, returned Azad to the hub, and offered Chapter 2.
- Compact main-menu and all 24 chapter cards passed their runtime smoke.

## Android Artifact

- Package: `com.skenterprises.netaji.prototype`
- Version: `0.29.0` (`versionCode 29`)
- Android: min SDK 26, target/compile SDK 36
- Native code: ARM64 and ARMv7 IL2CPP plus Unity libraries
- APK size: 26,481,653 bytes (25.25 MiB)
- SHA-256: `96EB7B0F867F7666EE236CBFCC1D70FA9D558358B7EE34427BAFB1D75D9C8523`
- Manifest has no debuggable flag and APK Signature Scheme v2 verification passes.
- The signer remains the Android debug certificate for local/company-demo installs, not production store signing.

## Remaining Production Work

This is a stronger procedural prototype, not final GTA-quality art. Custom rigged characters, authored animation sets, higher-quality vehicle/building assets, traffic collision AI, interiors, richer missions inside the shared world, final music/voice/sound design, and accessibility/settings UI remain. A physical Android phone is still required to verify touch feel, sustained FPS, thermal behavior, battery use, audio output, and device-specific compatibility.

Google website deployment and repository separation remain deferred until the game is substantially complete.

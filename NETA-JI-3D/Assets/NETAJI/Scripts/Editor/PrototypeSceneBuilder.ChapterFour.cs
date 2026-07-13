using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterFourScene(
            Material sand,
            Material stone,
            Material darkStone,
            Material teal,
            Material yellow,
            Material white,
            Material shirt,
            Material trousers,
            Material skin,
            Material hair,
            Material shantiDress,
            Material sandhyaDress,
            Material volunteerDress,
            Material policeKhaki,
            Material foliage,
            Material trunk)
        {
            Scene chapterScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject environment = new GameObject("Riverside Godown Rescue Perimeter");
            CreateBox("Godown Yard", new Vector3(0f, -0.3f, 1f), new Vector3(52f, 0.6f, 42f), stone, environment.transform);
            CreateBox("Approach Road", new Vector3(0f, 0.02f, -10f), new Vector3(10f, 0.08f, 24f), darkStone, environment.transform);
            CreateBox("Old Godown", new Vector3(0f, 3.6f, 13f), new Vector3(24f, 7.2f, 12f), darkStone, environment.transform);
            CreateBox("Godown Main Gate", new Vector3(0f, 2.2f, 6.8f), new Vector3(6f, 4.4f, 0.35f), teal, environment.transform);
            CreateBox("Godown Side Office", new Vector3(12f, 2.2f, 11f), new Vector3(7f, 4.4f, 8f), sand, environment.transform);
            CreateBox("Safe Point Tent", new Vector3(-14f, 1.5f, -3f), new Vector3(8f, 3f, 6f), teal, environment.transform);
            CreateWorldLabel("Safe Point Sign", "FAMILY SAFE POINT", new Vector3(-14f, 2.4f, -6.08f), Vector3.zero, yellow, environment.transform, 0.025f);
            CreateWorldLabel("Godown Sign", "OLD RIVERSIDE STORAGE", new Vector3(0f, 5.1f, 6.6f), Vector3.zero, yellow, environment.transform, 0.028f);

            CreateBox("Police Barricade Left", new Vector3(-5f, 0.65f, -3f), new Vector3(7f, 1.3f, 0.35f), policeKhaki, environment.transform);
            CreateBox("Police Barricade Right", new Vector3(5f, 0.65f, -3f), new Vector3(7f, 1.3f, 0.35f), policeKhaki, environment.transform);
            CreateWorldLabel("Barricade Text", "POLICE  /  KEEP CLEAR", new Vector3(0f, 1.35f, -3.18f), Vector3.zero, darkStone, environment.transform, 0.021f);

            GameObject van = new GameObject("Suspect White Van");
            van.transform.position = new Vector3(9f, 0f, 1f);
            CreatePrimitiveChild("Van Body", PrimitiveType.Cube, van.transform, new Vector3(0f, 1.25f, 0f), new Vector3(4.8f, 2.5f, 2.2f), white);
            CreatePrimitiveChild("Van Cabin", PrimitiveType.Cube, van.transform, new Vector3(1.55f, 1.4f, 0f), new Vector3(1.8f, 2.1f, 2.1f), white);
            for (int i = 0; i < 4; i++)
            {
                float x = i < 2 ? -1.45f : 1.45f;
                float z = i % 2 == 0 ? -1.05f : 1.05f;
                CreatePrimitiveChild($"Van Wheel {i + 1}", PrimitiveType.Cylinder, van.transform, new Vector3(x, 0.45f, z), new Vector3(0.42f, 0.18f, 0.42f), darkStone).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            }

            for (int i = 0; i < 5; i++)
            {
                CreateBox($"Storage Crate {i + 1}", new Vector3(-8f + i * 4f, 0.7f, 16f), new Vector3(2.2f, 1.4f, 2.2f), i % 2 == 0 ? sand : teal, environment.transform);
            }
            CreateTree("Godown Neem", new Vector3(-19f, 0f, 8f), foliage, trunk, environment.transform);
            CreateStreetLamp("Perimeter Lamp Left", new Vector3(-8f, 0f, -1f), darkStone, yellow, environment.transform);
            CreateStreetLamp("Perimeter Lamp Right", new Vector3(8f, 0f, -1f), darkStone, yellow, environment.transform);

            GameObject systems = new GameObject("Chapter 4 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(4, 83, 650, 56);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad", new Vector3(0f, 0f, -15f), shirt, trousers, skin, hair, true);
            AzadController controller = azad.AddComponent<AzadController>();
            CharacterController characterController = azad.GetComponent<CharacterController>();
            characterController.center = new Vector3(0f, 0.9f, 0f);
            characterController.height = 1.8f;
            characterController.radius = 0.34f;
            characterController.stepOffset = 0.42f;
            characterController.slopeLimit = 48f;
            azad.AddComponent<ProceduralWalker>();
            SetLayerRecursively(azad, 2);

            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            Camera gameCamera = cameraObject.AddComponent<Camera>();
            gameCamera.clearFlags = CameraClearFlags.Skybox;
            gameCamera.fieldOfView = 62f;
            gameCamera.nearClipPlane = 0.12f;
            gameCamera.farClipPlane = 160f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.6f, -20f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterFourMission(
                mission,
                van,
                shantiDress,
                sandhyaDress,
                volunteerDress,
                policeKhaki,
                teal,
                yellow,
                white,
                darkStone,
                skin,
                hair);
            CreateDawnRescueLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterFourScenePath);
        }

        private static void ConfigureChapterFourMission(
            MissionController mission,
            GameObject van,
            Material shantiDress,
            Material sandhyaDress,
            Material volunteerDress,
            Material policeKhaki,
            Material teal,
            Material yellow,
            Material white,
            Material darkStone,
            Material skin,
            Material hair)
        {
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject samrat = CreatePerson("Constable Samrat", new Vector3(-4f, 0f, -5f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            objectives.Add(AddObjective(samrat, "rescue-brief", "Samrat se rescue brief lein", "Constable Samrat",
                "Van mil gayi hai. Backup paanch minute door hai. Pehle plate verify karo aur watch receiver ka signal lock karo; bina plan koi andar nahi jayega.",
                1, 0, 1, false));
            labels.Add("Constable Samrat se perimeter briefing lein");

            objectives.Add(AddObjective(van, "verify-van", "Van ki plate verify karein", "Azad",
                "Plate ka aakhri number 27 hai aur side par wahi scratch hai jo CCTV mein tha. Evidence photo Samrat ko bhej diya.",
                1, 0, 1, false));
            labels.Add("CCTV clue se suspect van verify karein");

            GameObject receiver = new GameObject("Sandhya Watch Receiver");
            receiver.transform.position = new Vector3(5f, 0.95f, -4f);
            CreatePrimitiveChild("Receiver Body", PrimitiveType.Cube, receiver.transform, Vector3.zero, new Vector3(0.78f, 0.58f, 0.32f), darkStone);
            CreatePrimitiveChild("Signal Screen", PrimitiveType.Cube, receiver.transform, new Vector3(0f, 0.04f, -0.19f), new Vector3(0.55f, 0.30f, 0.05f), teal);
            CreatePrimitiveChild("Antenna", PrimitiveType.Cylinder, receiver.transform, new Vector3(0.28f, 0.52f, 0f), new Vector3(0.05f, 0.34f, 0.05f), yellow);
            objectives.Add(AddObjective(receiver, "watch-signal", "Watch signal lock karein", "Volunteer",
                "Weak signal side office se aa raha hai. Receiver ka live location police tablet par share ho gaya.",
                1, -50, 1, false));
            labels.Add("Sandhya ki watch ka short-range signal lock karein");

            GameObject briefingTable = new GameObject("Rescue Approach Board");
            briefingTable.transform.position = new Vector3(0f, 0.85f, -4f);
            CreatePrimitiveChild("Plan Board", PrimitiveType.Cube, briefingTable.transform, Vector3.zero, new Vector3(2.6f, 1.35f, 0.18f), white);
            CreatePrimitiveChild("Safe Route", PrimitiveType.Cube, briefingTable.transform, new Vector3(-0.5f, 0.16f, -0.12f), new Vector3(1.15f, 0.10f, 0.04f), teal).transform.localRotation = Quaternion.Euler(0f, 0f, 22f);
            CreatePrimitiveChild("Risk Route", PrimitiveType.Cube, briefingTable.transform, new Vector3(0.52f, -0.20f, -0.12f), new Vector3(1.05f, 0.10f, 0.04f), shantiDress).transform.localRotation = Quaternion.Euler(0f, 0f, -26f);
            MissionObjective decision = AddObjective(briefingTable, "rescue-decision", "Rescue approach chunein", "Azad",
                "Samrat, perimeter aur backup ke saath chalenge. Sandhya ki safety mere ego se badi hai.",
                3, -100, 3, false);
            decision.ConfigureDecision(
                "rescue-approach",
                "RESCUE APPROACH",
                "Backup bas kuch minute door hai. Coordinated entry safer hai; solo entry tez lag sakti hai, par Sandhya aur team dono ko risk hoga.",
                "SAMRAT KA PLAN\nFunds -100 / Trust +3",
                "AKELA SIDE GATE\nTrust -4 / Rep -3",
                "Azad akela side gate tak badhta hai. Alarm trigger hota hai aur Samrat ko team jaldi move karni padti hai.",
                -4, 0, -3);
            objectives.Add(decision);
            labels.Add("Rescue ke liye coordinated ya solo approach chunein");

            GameObject gate = new GameObject("Side Gate Control");
            gate.transform.position = new Vector3(0f, 1.4f, 6.6f);
            CreatePrimitiveChild("Gate Lock", PrimitiveType.Cube, gate.transform, Vector3.zero, new Vector3(1.1f, 1.3f, 0.34f), darkStone);
            CreatePrimitiveChild("Police Key", PrimitiveType.Cube, gate.transform, new Vector3(0f, 0.18f, -0.24f), new Vector3(0.22f, 0.55f, 0.08f), yellow);
            objectives.Add(AddObjective(gate, "open-gate", "Backup gate unlock karein", "Constable Samrat",
                "Lock khul gaya. Shield team pehle, Azad mere peeche. Kisi ko hero banne ki zarurat nahi.",
                1, 0, 1, true));
            labels.Add("Samrat ke saath side gate safely unlock karein");

            GameObject radio = new GameObject("Patrol Signal Radio");
            radio.transform.position = new Vector3(-5f, 0.95f, 5f);
            CreatePrimitiveChild("Radio", PrimitiveType.Cube, radio.transform, Vector3.zero, new Vector3(0.72f, 0.78f, 0.34f), policeKhaki);
            CreatePrimitiveChild("Transmit Key", PrimitiveType.Cube, radio.transform, new Vector3(0f, 0.12f, -0.21f), new Vector3(0.28f, 0.22f, 0.06f), yellow);
            objectives.Add(AddObjective(radio, "patrol-signal", "Entry signal bhejein", "Police Radio",
                "Control, child located zone confirmed. Medical team aur family safe point standby par rahein.",
                1, 0, 1, false));
            labels.Add("Patrol aur medical team ko entry signal bhejein");

            GameObject sandhya = CreatePerson("Sandhya", new Vector3(0f, 0f, 11f), sandhyaDress, darkStone, skin, hair, false);
            AddPigtails(sandhya.transform, hair);
            sandhya.transform.localScale = Vector3.one * 0.72f;
            objectives.Add(AddObjective(sandhya, "reach-sandhya", "Sandhya tak pahunchein", "Sandhya",
                "Papa... mujhe pata tha aap aaoge. Samrat uncle bhi aaye hain na?",
                3, 0, 2, false));
            labels.Add("Side office mein Sandhya tak pahunchein");

            GameObject firstAid = new GameObject("Child First Aid Kit");
            firstAid.transform.position = new Vector3(3f, 0.65f, 10f);
            CreatePrimitiveChild("Medical Case", PrimitiveType.Cube, firstAid.transform, Vector3.zero, new Vector3(1.15f, 0.68f, 0.72f), white);
            CreatePrimitiveChild("Medical Mark", PrimitiveType.Cube, firstAid.transform, new Vector3(0f, 0f, -0.39f), new Vector3(0.48f, 0.15f, 0.05f), shantiDress);
            objectives.Add(AddObjective(firstAid, "first-aid", "First aid aur paani dein", "Azad",
                "Sandhya hosh mein hai. Paani dheere, blanket pehle. Medical team bahar poora check karegi.",
                1, -100, 1, false));
            labels.Add("Sandhya ko basic first aid aur paani dein");

            GameObject ledger = new GameObject("Gang Payment Ledger");
            ledger.transform.position = new Vector3(10f, 0.82f, 10f);
            CreatePrimitiveChild("Ledger", PrimitiveType.Cube, ledger.transform, Vector3.zero, new Vector3(0.78f, 0.14f, 1.0f), yellow);
            CreatePrimitiveChild("Evidence Bag", PrimitiveType.Cube, ledger.transform, new Vector3(0f, 0.18f, 0f), new Vector3(1.1f, 0.05f, 1.35f), white);
            objectives.Add(AddObjective(ledger, "secure-ledger", "Payment ledger secure karein", "Constable Samrat",
                "Is register ko haath mat lagao; evidence bag mein seal hoga. Ismein aur extortion cases ki entries ho sakti hain.",
                1, 0, 1, true));
            labels.Add("Samrat ke liye payment ledger evidence secure karein");

            GameObject handover = new GameObject("Evidence Handover Point");
            handover.transform.position = new Vector3(-2.5f, 0.8f, -2f);
            CreatePrimitiveChild("Evidence Crate", PrimitiveType.Cube, handover.transform, Vector3.zero, new Vector3(1.4f, 0.75f, 1.0f), policeKhaki);
            objectives.Add(AddObjective(handover, "evidence-handover", "Evidence handover complete karein", "Constable Samrat",
                "Van, call recording, watch signal aur ledger chain mein hain. Ab case facts par chalega, gusse par nahi.",
                1, 0, 2, false));
            labels.Add("Police evidence handover record complete karein");

            GameObject shanti = CreatePerson("Shanti", new Vector3(-14f, 0f, -6.5f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            objectives.Add(AddObjective(shanti, "family-reunion", "Shanti ke paas Sandhya ko le jayein", "Shanti",
                "Sandhya ghar aa gayi. Aaj Samrat aur mohalla saath tha; kal humein aisa system banana hai jahan kisi maa ko akela na padna pade.",
                2, 0, 2, false));
            labels.Add("Family safe point par Shanti se milen");

            mission.Configure(
                "Operation Umeed",
                objectives,
                labels,
                "CHAPTER 4 COMPLETE",
                "Sandhya safe hai. Evidence police custody mein hai aur family saath hai.");
            mission.ConfigureMilestones(
                new List<int> { 3, 6, 8 },
                new List<string> { "LOCATION CONFIRMED", "ENTRY READY", "SANDHYA SAFE" },
                new List<string>
                {
                    "Van aur watch signal match karte hain. Ab rescue approach decide karna hai.",
                    "Gate aur patrol signal ready hain. Samrat ki team ke saath side office tak pahuncho.",
                    "Sandhya mil gayi hai. First aid ke baad evidence secure karna hoga."
                });
            mission.ConfigureChapter(4, "Chapter05");
            mission.ConfigureIntro(
                "CHAPTER 4 / OPERATION UMEED",
                "Riverside godown. Sandhya ki safety pehle; gussa aur jaldbazi baad mein.");
        }

        private static void CreateDawnRescueLighting()
        {
            GameObject lightObject = new GameObject("Dawn Rescue Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.74f, 0.52f);
            sunlight.intensity = 0.94f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.50f;
            lightObject.transform.rotation = Quaternion.Euler(32f, -35f, 0f);

            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.34f, 0.48f, 0.67f);
            RenderSettings.ambientEquatorColor = new Color(0.48f, 0.39f, 0.35f);
            RenderSettings.ambientGroundColor = new Color(0.15f, 0.17f, 0.18f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.48f, 0.55f, 0.66f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 44f;
            RenderSettings.fogEndDistance = 125f;
        }
    }
}

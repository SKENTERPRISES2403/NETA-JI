using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterSixScene(
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
            Material volunteerDress,
            Material policeKhaki,
            Material foliage,
            Material trunk)
        {
            Scene chapterScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject environment = new GameObject("Prayagraj Community Foundation Sabha");
            CreateBox("Sabha Ground", new Vector3(0f, -0.3f, 0f), new Vector3(50f, 0.6f, 38f), stone, environment.transform);
            CreateBox("Public Walkway", new Vector3(0f, 0.02f, -8f), new Vector3(10f, 0.08f, 22f), sand, environment.transform);
            CreateBox("Community Hall", new Vector3(0f, 3f, 13f), new Vector3(28f, 6f, 10f), teal, environment.transform);
            CreateBox("Founding Stage", new Vector3(0f, 0.55f, 6.5f), new Vector3(14f, 1.1f, 5f), darkStone, environment.transform);
            CreateBox("Charter Backboard", new Vector3(0f, 3.2f, 10.4f), new Vector3(15f, 4.2f, 0.25f), white, environment.transform);
            CreateWorldLabel("Party Name", "INDIA HELPING PARTY", new Vector3(0f, 3.75f, 10.22f), Vector3.zero, teal, environment.transform, 0.036f);
            CreateWorldLabel("Party Values", "SEVA  /  SABOOT  /  SAFAI", new Vector3(0f, 2.90f, 10.22f), Vector3.zero, darkStone, environment.transform, 0.022f);
            CreateWorldLabel("Party Symbol", "HELPING HANDS", new Vector3(0f, 2.25f, 10.22f), Vector3.zero, yellow, environment.transform, 0.019f);

            CreateBox("NGO Boundary Desk", new Vector3(-14f, 0.72f, -2f), new Vector3(6f, 1.4f, 2f), yellow, environment.transform);
            CreateWorldLabel("NGO Boundary Sign", "HELPERS HAND  /  NONPARTISAN", new Vector3(-14f, 1.65f, -0.95f), Vector3.zero, darkStone, environment.transform, 0.017f);
            CreateBox("Party Accounts Desk", new Vector3(14f, 0.72f, -2f), new Vector3(6f, 1.4f, 2f), teal, environment.transform);
            CreateWorldLabel("Accounts Sign", "PARTY ACCOUNTS  /  RECEIPTS", new Vector3(14f, 1.65f, -0.95f), Vector3.zero, yellow, environment.transform, 0.017f);

            for (int i = 0; i < 5; i++)
            {
                CreateBench($"Sabha Bench {i + 1}", new Vector3(-12f + i * 6f, 0f, 1f), 0f, darkStone, i % 2 == 0 ? teal : yellow, environment.transform);
            }
            CreateTree("Sabha Neem Left", new Vector3(-19f, 0f, 8f), foliage, trunk, environment.transform);
            CreateTree("Sabha Neem Right", new Vector3(19f, 0f, 8f), foliage, trunk, environment.transform);
            CreateStreetLamp("Sabha Lamp Left", new Vector3(-10f, 0f, -7f), darkStone, yellow, environment.transform);
            CreateStreetLamp("Sabha Lamp Right", new Vector3(10f, 0f, -7f), darkStone, yellow, environment.transform);

            GameObject systems = new GameObject("Chapter 6 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(6, 100, 450, 84, 31, 18, 35, 8);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad", new Vector3(0f, 0f, -14f), shirt, trousers, skin, hair, true);
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
            cameraObject.transform.position = new Vector3(0f, 3.6f, -19f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterSixMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white, darkStone, skin, hair);
            CreateFoundationLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterSixScenePath);
        }

        private static void ConfigureChapterSixMission(
            MissionController mission,
            Material shantiDress,
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

            GameObject shanti = CreatePerson("Shanti", new Vector3(-6f, 0f, -8f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            objectives.Add(AddObjective(shanti, "shanti-purpose", "Shanti se baat karein", "Shanti",
                "Case file ek shuruaat hai. Agar system badalna hai toh logon ko sirf followers nahi, zimmedar saathi banana hoga.",
                0, 0, 0, false));
            labels.Add("Shanti ke saath system-change sankalp samjhein");

            GameObject resident = CreatePerson("Community Representative", new Vector3(5f, 0f, -7f), volunteerDress, darkStone, skin, hair, false);
            MissionObjective residentObjective = AddObjective(resident, "community-call", "Community proposal sunein", "Community Representative",
                "Azad bhai, pension, school, rescue aur hospital case mein aap saath rahe. Ab ward ke decisions mein bhi saaf awaaz chahiye.",
                1, 0, 0, false);
            residentObjective.ConfigurePoliticalReward(1, 5, 0);
            objectives.Add(residentObjective);
            labels.Add("Mohalla representatives ka political proposal sunein");

            GameObject ngoCoordinator = CreatePerson("Helpers Hand Director", new Vector3(-14f, 0f, -4f), volunteerDress, darkStone, skin, hair, false);
            MissionObjective ngoObjective = AddObjective(ngoCoordinator, "ngo-boundary", "NGO boundary document karein", "Helpers Hand Director",
                "Helpers Hand kisi party ka wing nahi banega. Staff, donor list, office aur funds alag rahenge; volunteers personal capacity mein decide karenge.",
                0, 0, 2, false, 2);
            ngoObjective.ConfigurePoliticalReward(1, 0, 0);
            objectives.Add(ngoObjective);
            labels.Add("NGO aur political organization ki boundary lock karein");

            GameObject constitution = new GameObject("Party Constitution Draft");
            constitution.transform.position = new Vector3(-6f, 0.9f, 5f);
            CreatePrimitiveChild("Constitution Folder", PrimitiveType.Cube, constitution.transform, Vector3.zero, new Vector3(0.95f, 0.16f, 1.15f), teal);
            MissionObjective constitutionObjective = AddObjective(constitution, "constitution", "Founding charter padhein", "Azad",
                "Internal elections, audited accounts, no hate speech, no real-party symbols, aur public grievance register: yahi founding rules hain.",
                0, 0, 0, false, 2);
            constitutionObjective.ConfigurePoliticalReward(2, 0, 0);
            objectives.Add(constitutionObjective);
            labels.Add("India Helping Party ka fictional founding charter review karein");

            GameObject ledger = new GameObject("Transparent Donation Ledger");
            ledger.transform.position = new Vector3(14f, 0.95f, -2f);
            CreatePrimitiveChild("Party Ledger", PrimitiveType.Cube, ledger.transform, Vector3.zero, new Vector3(0.88f, 0.14f, 1.1f), yellow);
            MissionObjective ledgerObjective = AddObjective(ledger, "party-ledger", "Donation ledger activate karein", "Party Treasurer",
                "Founding donations Rs 500. Har receipt public board par; NGO ka ek rupaya bhi party account mein nahi aayega.",
                0, 500, 0, false, 1);
            ledgerObjective.ConfigurePoliticalReward(1, 2, 0);
            objectives.Add(ledgerObjective);
            labels.Add("Separate party donation ledger aur receipts activate karein");

            GameObject identity = new GameObject("Party Identity Board");
            identity.transform.position = new Vector3(0f, 1.3f, 9.8f);
            CreatePrimitiveChild("Identity Panel", PrimitiveType.Cube, identity.transform, Vector3.zero, new Vector3(4.4f, 2.0f, 0.18f), white);
            CreatePrimitiveChild("Helping Hand Left", PrimitiveType.Cube, identity.transform, new Vector3(-0.55f, 0f, -0.14f), new Vector3(0.28f, 1.1f, 0.05f), teal).transform.localRotation = Quaternion.Euler(0f, 0f, -25f);
            CreatePrimitiveChild("Helping Hand Right", PrimitiveType.Cube, identity.transform, new Vector3(0.55f, 0f, -0.14f), new Vector3(0.28f, 1.1f, 0.05f), yellow).transform.localRotation = Quaternion.Euler(0f, 0f, 25f);
            MissionObjective identityObjective = AddObjective(identity, "party-identity", "Party identity approve karein", "Azad",
                "Temporary naam India Helping Party, rang teal aur yellow, symbol Helping Hands. National flag party symbol nahi hoga.",
                2, 0, 1, false);
            identityObjective.ConfigurePoliticalReward(2, 3, 0);
            objectives.Add(identityObjective);
            labels.Add("Fictional party name, colors aur Helping Hands symbol approve karein");

            GameObject signatures = new GameObject("Founding Member Register");
            signatures.transform.position = new Vector3(6f, 0.9f, 4.5f);
            CreatePrimitiveChild("Signature Register", PrimitiveType.Cube, signatures.transform, Vector3.zero, new Vector3(1.05f, 0.15f, 1.25f), white);
            MissionObjective signaturesObjective = AddObjective(signatures, "member-register", "Founding signatures verify karein", "Founding Secretary",
                "Har member ne code of conduct aur conflict declaration sign kiya. Duplicate entries remove ho gayi hain.",
                2, 0, 1, false);
            signaturesObjective.ConfigurePoliticalReward(3, 10, 0);
            objectives.Add(signaturesObjective);
            labels.Add("Founding members aur conflict declarations verify karein");

            GameObject samrat = CreatePerson("Constable Samrat", new Vector3(14f, 0f, 5f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            objectives.Add(AddObjective(samrat, "samrat-boundary", "Samrat ki service boundary samjhein", "Constable Samrat",
                "Main dost aur witness hoon, party member nahi. Duty mein neutral rahunga; lawful safety issue ho toh process ke through help karunga.",
                0, 0, 2, false, 2));
            labels.Add("Samrat ki nonpartisan police-service boundary record karein");

            GameObject leaflet = new GameObject("Anonymous Opposition Leaflet");
            leaflet.transform.position = new Vector3(-3f, 0.2f, -2f);
            CreatePrimitiveChild("Leaflet", PrimitiveType.Cube, leaflet.transform, Vector3.zero, new Vector3(0.95f, 0.05f, 1.25f), shantiDress);
            MissionObjective leafletObjective = AddObjective(leaflet, "opposition-rumour", "Anonymous leaflet preserve karein", "Volunteer",
                "Ismein NGO funds chori ka jhootha claim hai. Original leaflet, distribution photo aur accounts audit ek saath preserve karte hain.",
                0, 0, 0, false, 2);
            leafletObjective.ConfigurePoliticalReward(0, 0, 5);
            objectives.Add(leafletObjective);
            labels.Add("First opposition rumour ka evidence preserve karein");

            GameObject responseBoard = new GameObject("Opposition Response Board");
            responseBoard.transform.position = new Vector3(0f, 1f, 3f);
            CreatePrimitiveChild("Response Panel", PrimitiveType.Cube, responseBoard.transform, Vector3.zero, new Vector3(3f, 1.6f, 0.18f), darkStone);
            MissionObjective response = AddObjective(responseBoard, "opposition-response", "Opposition response chunein", "Azad",
                "Audit, receipts aur independent questions ke saath open jan-samvad karenge. Awaaz kam, saboot zyada.",
                2, -100, 3, false, 3);
            response.ConfigurePoliticalReward(3, 8, 3);
            response.ConfigureDecision(
                "opposition-response",
                "FIRST OPPOSITION TEST",
                "Anonymous rumour viral ho raha hai. Documented public dialogue slow hai; direct confrontation tez, par pressure badha sakta hai.",
                "OPEN JAN-SAMVAD\nProof +3 / Pressure +3",
                "DIRECT CONFRONTATION\nPower +5 / Pressure +12",
                "Azad rival meeting ke bahar direct jawab deta hai. Core supporters energize hote hain, par conflict aur pressure dono badhte hain.",
                3, 0, -4, -2, 5, 5, 12);
            objectives.Add(response);
            labels.Add("Documented dialogue ya direct confrontation response chunein");

            GameObject charterStage = new GameObject("Public Charter Microphone");
            charterStage.transform.position = new Vector3(0f, 1.4f, 6f);
            CreatePrimitiveChild("Microphone Stand", PrimitiveType.Cylinder, charterStage.transform, Vector3.zero, new Vector3(0.08f, 1.1f, 0.08f), darkStone);
            CreatePrimitiveChild("Microphone", PrimitiveType.Sphere, charterStage.transform, new Vector3(0f, 1.18f, 0f), new Vector3(0.18f, 0.14f, 0.18f), yellow);
            MissionObjective charterObjective = AddObjective(charterStage, "public-charter", "Public charter announce karein", "Azad",
                "India Helping Party vote se pehle service report degi, hate se vote nahi maangegi, aur har donation ka hisaab dikhayegi.",
                3, 0, 2, false);
            charterObjective.ConfigurePoliticalReward(3, 5, 0);
            objectives.Add(charterObjective);
            labels.Add("Founding public charter aur safety promise announce karein");

            GameObject filing = new GameObject("Ward Election Preparation File");
            filing.transform.position = new Vector3(10f, 0.9f, 5f);
            CreatePrimitiveChild("Election File", PrimitiveType.Cube, filing.transform, Vector3.zero, new Vector3(0.92f, 0.16f, 1.15f), teal);
            MissionObjective filingObjective = AddObjective(filing, "election-prep", "Ward filing checklist complete karein", "Founding Secretary",
                "Candidate disclosure, expense account, member list aur fictional symbol artwork ready hai. Agla kadam local ward campaign hoga.",
                1, -100, 2, false, 2);
            filingObjective.ConfigurePoliticalReward(2, 2, 0);
            objectives.Add(filingObjective);
            labels.Add("Local ward election filing checklist complete karein");

            mission.Configure(
                "India Helping Party",
                objectives,
                labels,
                "CHAPTER 6 COMPLETE",
                "A movement became an accountable fictional party. Ward election path is now open.");
            mission.ConfigureMilestones(
                new List<int> { 4, 8, 10 },
                new List<string> { "RULES BEFORE POWER", "FOUNDING TEAM READY", "OPPOSITION TEST" },
                new List<string>
                {
                    "NGO boundary aur founding charter locked hain. Ab separate accounts aur identity banao.",
                    "Members, symbol aur service boundaries ready hain. First opposition rumour aa gaya hai.",
                    "Response record ho gaya. Ab public charter aur ward filing complete karo."
                });
            mission.ConfigureChapter(6, "Chapter07");
            mission.ConfigureIntro(
                "CHAPTER 6 / SEVA SE SIYASAT",
                "Community movement ko accountable political organization mein badalne ka din.");
        }

        private static void CreateFoundationLighting()
        {
            GameObject lightObject = new GameObject("Foundation Day Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.86f, 0.66f);
            sunlight.intensity = 1.02f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.48f;
            lightObject.transform.rotation = Quaternion.Euler(40f, -28f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.48f, 0.66f, 0.76f);
            RenderSettings.ambientEquatorColor = new Color(0.58f, 0.52f, 0.42f);
            RenderSettings.ambientGroundColor = new Color(0.20f, 0.22f, 0.20f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.66f, 0.73f, 0.74f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 45f;
            RenderSettings.fogEndDistance = 130f;
        }
    }
}

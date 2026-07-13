using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterNineScene(
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
            GameObject environment = new GameObject("Prayagraj Assembly Outreach Route");

            CreateBox("Constituency Ground", new Vector3(0f, -0.3f, 0f), new Vector3(68f, 0.6f, 54f), sand, environment.transform);
            CreateBox("Jan Sampark Ring Road North", new Vector3(0f, 0.02f, 10f), new Vector3(48f, 0.10f, 7f), darkStone, environment.transform);
            CreateBox("Jan Sampark Ring Road South", new Vector3(0f, 0.02f, -10f), new Vector3(48f, 0.10f, 7f), darkStone, environment.transform);
            CreateBox("Jan Sampark Link West", new Vector3(-20.5f, 0.02f, 0f), new Vector3(7f, 0.10f, 24f), darkStone, environment.transform);
            CreateBox("Jan Sampark Link East", new Vector3(20.5f, 0.02f, 0f), new Vector3(7f, 0.10f, 24f), darkStone, environment.transform);

            CreateChapterNineUrbanBlock(environment.transform, stone, teal, yellow, white, darkStone);
            CreateChapterNineRuralBlock(environment.transform, sand, teal, yellow, foliage, trunk);
            CreateChapterNineCampaignPlaza(environment.transform, darkStone, teal, yellow, white);

            for (int i = 0; i < 8; i++)
            {
                float x = -17.5f + i * 5f;
                CreateBox($"Road Marker North {i + 1}", new Vector3(x, 0.09f, 10f), new Vector3(2.2f, 0.03f, 0.16f), white, environment.transform);
                CreateBox($"Road Marker South {i + 1}", new Vector3(x, 0.09f, -10f), new Vector3(2.2f, 0.03f, 0.16f), white, environment.transform);
            }

            for (int i = 0; i < 6; i++)
            {
                CreateStreetLamp($"Outreach Lamp {i + 1}", new Vector3(-14f + i * 5.6f, 0f, i % 2 == 0 ? 14f : -14f), darkStone, yellow, environment.transform);
            }

            CreateExpansionFlag("Volunteer Flag West", new Vector3(-25f, 0f, -14f), darkStone, teal, environment.transform);
            CreateExpansionFlag("Volunteer Flag East", new Vector3(25f, 0f, 14f), darkStone, yellow, environment.transform);
            CreateExpansionFlag("Jan Sabha Flag Left", new Vector3(-4f, 0f, 18f), darkStone, teal, environment.transform);
            CreateExpansionFlag("Jan Sabha Flag Right", new Vector3(4f, 0f, 18f), darkStone, yellow, environment.transform);

            GameObject systems = new GameObject("Chapter 9 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                9, 100, 150, 100, 68, 38, 82, 42, 67, 85, 59, true,
                65, 87, 5, 85, true, 70, 75, 79, 91, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad", new Vector3(0f, 0f, -20f), shirt, trousers, skin, hair, true);
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
            gameCamera.fieldOfView = 63f;
            gameCamera.nearClipPlane = 0.12f;
            gameCamera.farClipPlane = 190f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.7f, -25f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterNineMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white, darkStone, skin, hair);
            CreateExpansionLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterNineScenePath);
        }

        private static void ConfigureChapterNineMission(
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

            GameObject auditor = CreatePerson("Citizen Audit Convener", new Vector3(0f, 0f, -16f), white, darkStone, skin, hair, false);
            MissionObjective audit = AddObjective(auditor, "audit-closure", "100-day audit closure lein", "Citizen Audit Convener",
                "Ward files rechecked hain. Pending objections close, Rs 3 lakh recovery credited aur integrity trail updated. Ab assembly outreach clean record se shuru ho sakta hai.",
                0, 0, 0, false, 2);
            audit.ConfigureGovernanceReward(0, 12, 3);
            audit.ConfigureAssemblyReward(2, 5, 2);
            audit.ConfigureHundredDayReview();
            objectives.Add(audit);
            labels.Add("Citizen panel se corrected 100-day audit closure lein");

            GameObject eligibility = new GameObject("Candidate Eligibility Desk");
            eligibility.transform.position = new Vector3(-10f, 1f, -13f);
            CreatePrimitiveChild("Desk", PrimitiveType.Cube, eligibility.transform, Vector3.zero, new Vector3(2.8f, 1.0f, 1.1f), teal);
            CreatePrimitiveChild("Disclosure Files", PrimitiveType.Cube, eligibility.transform, new Vector3(0f, 0.64f, 0f), new Vector3(0.9f, 0.10f, 0.65f), yellow);
            MissionObjective eligibilityObjective = AddObjective(eligibility, "eligibility-desk", "Eligibility file verify karein", "Independent Election Trainer",
                "Age, voter record, assets, liabilities aur fictional party constitution checklist complete. Real party symbol ya restricted identity targeting allowed nahi hai.",
                0, 0, 0, false, 2);
            eligibilityObjective.ConfigurePoliticalReward(1, 0, 0);
            eligibilityObjective.ConfigureAssemblyReward(5, 8, 8);
            objectives.Add(eligibilityObjective);
            labels.Add("Independent desk par candidate eligibility aur disclosures verify karein");

            GameObject waterVolunteer = CreatePerson("Urban Water Volunteer", new Vector3(-18f, 0f, -7f), teal, darkStone, skin, hair, false);
            MissionObjective waterCamp = AddObjective(waterVolunteer, "urban-water-camp", "Water grievance camp sunein", "Resident Volunteer",
                "Teen urban wards ki supply timing, tanker complaints aur leak photos ward-wise tagged hain. Speech se pehle resolution owner aur deadline assign karte hain.",
                0, 0, 0, false);
            waterCamp.ConfigurePoliticalReward(0, 2, 0);
            waterCamp.ConfigureAssemblyReward(8, 4, 4);
            objectives.Add(waterCamp);
            labels.Add("Urban water camp mein ward-wise complaints aur deadlines lock karein");

            GameObject youth = CreatePerson("Youth Skills Coordinator", new Vector3(-12f, 0f, 0f), yellow, darkStone, skin, hair, false);
            MissionObjective youthRoundtable = AddObjective(youth, "youth-roundtable", "Youth jobs roundtable karein", "Youth Skills Coordinator",
                "Local workshops, nursing centres aur digital service desks 40 paid apprenticeships list kar rahe hain. Fake placement claims nahi, verified employer aur stipend sheet public hogi.",
                0, 0, 0, false);
            youthRoundtable.ConfigurePoliticalReward(0, 3, 0);
            youthRoundtable.ConfigureAssemblyReward(7, 6, 5);
            objectives.Add(youthRoundtable);
            labels.Add("Youth ke saath verified apprenticeship aur stipend plan banayein");

            GameObject farmer = CreatePerson("Rural Seva Coordinator", new Vector3(-18f, 0f, 8f), volunteerDress, darkStone, skin, hair, false);
            MissionObjective ruralCamp = AddObjective(farmer, "rural-irrigation-camp", "Rural seva camp chalayein", "Rural Seva Coordinator",
                "Canal roster, crop insurance correction aur land-copy help ek hi camp mein. Kisi gaon ko vote promise nahi, har application ko receipt milegi.",
                0, 0, 0, false);
            ruralCamp.ConfigurePoliticalReward(0, 2, 0);
            ruralCamp.ConfigureAssemblyReward(8, 6, 5);
            objectives.Add(ruralCamp);
            labels.Add("Rural irrigation aur paperwork camp mein receipt-based help karein");

            GameObject shanti = CreatePerson("Shanti Safety Walk Lead", new Vector3(-8f, 0f, 12f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            GameObject samrat = CreatePerson("Constable Samrat Neutral Liaison", new Vector3(-5.7f, 0f, 12.5f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective safetyWalk = AddObjective(shanti, "safety-walk", "Women safety walk karein", "Shanti",
                "Dark lanes, bus stops aur complaint access ka route audit karenge. Samrat sirf legal process explain karega; police campaign stage ya party endorsement par nahi jayegi.",
                0, 0, 0, false);
            safetyWalk.ConfigurePoliticalReward(0, 0, 2);
            safetyWalk.ConfigureAssemblyReward(7, 8, 6);
            objectives.Add(safetyWalk);
            labels.Add("Shanti aur neutral police liaison ke saath safety route audit karein");

            GameObject coalition = CreatePerson("Civic Coalition Moderator", new Vector3(0f, 0f, 15f), white, darkStone, skin, hair, false);
            CreateChapterNineCrowd("Coalition Delegates", new Vector3(0f, 0f, 17f), 6, volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective coalitionDialogue = AddObjective(coalition, "civic-coalition", "Civic coalition dialogue karein", "Civic Coalition Moderator",
                "Teachers, traders, workers, students aur resident groups common charter par baat karenge. Caste, religion aur fear targeting agenda se bahar rahegi.",
                0, 0, 0, false);
            coalitionDialogue.ConfigurePoliticalReward(1, 2, 0);
            coalitionDialogue.ConfigureAssemblyReward(6, 12, 5);
            objectives.Add(coalitionDialogue);
            labels.Add("Issue-based civic coalition ka common charter agree karein");

            GameObject disclosureBoard = new GameObject("Public Candidate Disclosure Board");
            disclosureBoard.transform.position = new Vector3(8f, 1.45f, 13f);
            CreatePrimitiveChild("Board", PrimitiveType.Cube, disclosureBoard.transform, Vector3.zero, new Vector3(3.8f, 2.5f, 0.22f), teal);
            CreatePrimitiveChild("Asset Sheet", PrimitiveType.Cube, disclosureBoard.transform, new Vector3(-0.9f, 0f, -0.16f), new Vector3(1.2f, 1.55f, 0.05f), white);
            CreatePrimitiveChild("Promise Sheet", PrimitiveType.Cube, disclosureBoard.transform, new Vector3(0.9f, 0f, -0.16f), new Vector3(1.2f, 1.55f, 0.05f), yellow);
            MissionObjective disclosure = AddObjective(disclosureBoard, "candidate-disclosure", "Candidate disclosure publish karein", "Independent Ethics Volunteer",
                "Income, assets, pending matters, donations aur five measurable promises public. Correction window seven days open rahegi.",
                0, 0, 0, false, 4);
            disclosure.ConfigurePoliticalReward(0, 0, 2);
            disclosure.ConfigureAssemblyReward(4, 5, 10);
            objectives.Add(disclosure);
            labels.Add("Assets, donations aur measurable promises ka public disclosure karein");

            GameObject strategyStage = new GameObject("Assembly Outreach Strategy Stage");
            strategyStage.transform.position = new Vector3(14f, 1.05f, 8f);
            CreatePrimitiveChild("Stage", PrimitiveType.Cube, strategyStage.transform, Vector3.zero, new Vector3(5.2f, 1.0f, 3.0f), darkStone);
            CreatePrimitiveChild("Backdrop", PrimitiveType.Cube, strategyStage.transform, new Vector3(0f, 1.7f, 1.25f), new Vector3(5.2f, 2.5f, 0.20f), teal);
            CreateWorldLabel("Strategy Stage Label", "JAN SAMVAD  /  NO HATE", new Vector3(14f, 2.9f, 9.10f), Vector3.zero, yellow, strategyStage.transform.parent, 0.021f);
            MissionObjective strategy = AddObjective(strategyStage, "expansion-strategy", "Outreach strategy chunein", "Campaign Treasurer",
                "Ground jan-sabha chhoti hogi: verified issue desks, spending register aur volunteer training. Reach measured hogi, unity aur booth discipline strong rahenge.",
                0, -150, 0, false, 2);
            strategy.ConfigurePoliticalReward(2, 4, 3);
            strategy.ConfigureAssemblyReward(10, 10, 12);
            strategy.ConfigureDecision(
                "expansion-strategy",
                "VIDHANSABHA OUTREACH",
                "Rs 350 available hain. Ground jan-sabha slower but accountable hai; loud dhol roadshow faster reach deta hai par team discipline aur evidence trail weak karta hai.",
                "GROUND JAN-SABHA\nReach +10 / Ready +12",
                "DHOL ROADSHOW\nReach +18 / Pressure +12",
                "Roadshow viral hota hai, par route permissions, spending slips aur volunteer briefing adhuri reh jaati hai. Crowd bada, coalition unity kam.",
                0, -300, -4, -3, 4, 1, 12, 0, 0, 0, 0, 0, 18, -8, -5);
            objectives.Add(strategy);
            labels.Add("Ground jan-sabha ya high-pressure dhol roadshow mein decision lein");

            GameObject rival = CreatePerson("Fictional Opposition Spokesperson", new Vector3(18f, 0f, 1f), darkStone, white, skin, hair, false);
            MissionObjective debate = AddObjective(rival, "public-debate", "Issue debate ka jawab dein", "Fictional Opposition Spokesperson",
                "Hum aapke 100-day numbers challenge karte hain. Azad, slogans nahi: source file, ward baseline aur next-year funding plan public screen par dikhaiye.",
                0, 0, 0, false, 2);
            debate.ConfigurePoliticalReward(1, 0, 3);
            debate.ConfigureAssemblyReward(8, 6, 4);
            objectives.Add(debate);
            labels.Add("Fictional opposition ko source-backed public debate mein jawab dein");

            GameObject boothMap = new GameObject("Constituency Booth Map Table");
            boothMap.transform.position = new Vector3(14f, 0.85f, -7f);
            CreatePrimitiveChild("Table", PrimitiveType.Cube, boothMap.transform, Vector3.zero, new Vector3(4.4f, 0.22f, 3.2f), darkStone);
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    CreatePrimitiveChild($"Booth Cell {row + 1}-{column + 1}", PrimitiveType.Cube, boothMap.transform,
                        new Vector3(-1.45f + column * 0.96f, 0.16f, -0.9f + row * 0.90f),
                        new Vector3(0.76f, 0.08f, 0.68f), (row + column) % 2 == 0 ? teal : yellow);
                }
            }
            MissionObjective mapping = AddObjective(boothMap, "booth-mapping", "Booth readiness map lock karein", "Volunteer Data Lead",
                "Accessibility, volunteer shifts, issue records aur legal polling guidance mapped hain. Voter profiling by caste or religion bilkul nahi hoga.",
                0, -50, 0, false);
            mapping.ConfigurePoliticalReward(0, 5, 1);
            mapping.ConfigureAssemblyReward(5, 5, 18);
            objectives.Add(mapping);
            labels.Add("Privacy-safe booth map aur volunteer shifts lock karein");

            GameObject nominationBoard = new GameObject("Independent Nomination Panel");
            nominationBoard.transform.position = new Vector3(4f, 1.5f, -11f);
            CreatePrimitiveChild("Nomination Screen", PrimitiveType.Cube, nominationBoard.transform, Vector3.zero, new Vector3(4.4f, 2.7f, 0.22f), darkStone);
            CreatePrimitiveChild("Score Light Left", PrimitiveType.Sphere, nominationBoard.transform, new Vector3(-1.25f, 0.35f, -0.18f), Vector3.one * 0.28f, teal);
            CreatePrimitiveChild("Score Light Right", PrimitiveType.Sphere, nominationBoard.transform, new Vector3(1.25f, 0.35f, -0.18f), Vector3.one * 0.28f, yellow);
            CreateWorldLabel("Nomination Label", "CONSTITUENCY NOMINATION PANEL", new Vector3(4f, 2.15f, -11.16f), Vector3.zero, white, nominationBoard.transform.parent, 0.019f);
            MissionObjective nomination = AddObjective(nominationBoard, "assembly-nomination", "Nomination score dekhein", "Independent Constituency Panel",
                "Public record, constituency reach, coalition unity aur booth readiness ka computed review ready hai.",
                0, 0, 0, false);
            nomination.ConfigureAssemblyNomination();
            objectives.Add(nomination);
            labels.Add("Independent panel par computed MLA nomination score dekhein");

            mission.Configure(
                "Vidhansabha Ki Raah",
                objectives,
                labels,
                "CHAPTER 9 COMPLETE",
                "Ward seva ab constituency organization ban chuki hai. Reach, unity aur readiness ka har number save ho gaya.");
            mission.ConfigureMilestones(
                new List<int> { 3, 6, 9 },
                new List<string> { "CLEAN RECORD VERIFIED", "URBAN-RURAL ROUTE ACTIVE", "PUBLIC CANDIDACY READY" },
                new List<string>
                {
                    "Audit, eligibility aur urban water camp complete. Expansion ab verified record par khadi hai.",
                    "Youth, rural paperwork aur safety route linked hain. Ab issue-based coalition banani hai.",
                    "Coalition, disclosures aur outreach strategy locked. Debate aur booth map nomination decide karenge."
                });
            mission.ConfigureChapter(9, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 9 / VIDHANSABHA KI RAAH",
                "Ward ki jeet ko constituency bhar ke accountable public network mein badlo.");
        }

        private static void CreateChapterNineUrbanBlock(
            Transform root,
            Material stone,
            Material teal,
            Material yellow,
            Material white,
            Material darkStone)
        {
            CreateBox("Urban Skills Centre", new Vector3(-26f, 2.8f, -3f), new Vector3(9f, 5.6f, 10f), teal, root);
            CreateBox("Urban Water Office", new Vector3(-26f, 2.2f, -15f), new Vector3(9f, 4.4f, 8f), white, root);
            CreateBox("Urban Clinic Annex", new Vector3(-14f, 2.3f, -20f), new Vector3(10f, 4.6f, 7f), stone, root);
            CreateWorldLabel("Skills Centre Sign", "SKILLS + APPRENTICESHIP", new Vector3(-26f, 3.65f, 2.10f), Vector3.zero, yellow, root, 0.020f);
            CreateWorldLabel("Water Camp Sign", "URBAN WATER HELP CAMP", new Vector3(-26f, 2.85f, -10.90f), Vector3.zero, teal, root, 0.019f);
            CreateBox("Safe Bus Stop Roof", new Vector3(-7f, 2.6f, 18f), new Vector3(7f, 0.22f, 3f), yellow, root);
            CreateBox("Safe Bus Stop Back", new Vector3(-7f, 1.35f, 19.3f), new Vector3(7f, 2.7f, 0.18f), darkStone, root);
            CreateWorldLabel("Safety Route Sign", "SAFE ROUTE AUDIT", new Vector3(-7f, 2.0f, 19.18f), Vector3.zero, white, root, 0.020f);
        }

        private static void CreateChapterNineRuralBlock(
            Transform root,
            Material sand,
            Material teal,
            Material yellow,
            Material foliage,
            Material trunk)
        {
            for (int row = 0; row < 4; row++)
            {
                CreateBox($"Irrigation Field {row + 1}", new Vector3(27f, -0.02f, -17f + row * 4.5f), new Vector3(10f, 0.18f, 3.2f), row % 2 == 0 ? foliage : yellow, root);
                CreateBox($"Canal Strip {row + 1}", new Vector3(21.5f, 0.03f, -17f + row * 4.5f), new Vector3(0.8f, 0.20f, 3.7f), teal, root);
            }
            CreateBox("Rural Paperwork Tent Roof", new Vector3(27f, 3.6f, 9f), new Vector3(10f, 0.28f, 7f), yellow, root);
            for (int i = 0; i < 4; i++)
            {
                float x = i < 2 ? 22.5f : 31.5f;
                float z = i % 2 == 0 ? 6f : 12f;
                CreateBox($"Rural Tent Pole {i + 1}", new Vector3(x, 1.8f, z), new Vector3(0.18f, 3.6f, 0.18f), trunk, root);
            }
            CreateWorldLabel("Rural Camp Sign", "RURAL SEVA + IRRIGATION", new Vector3(27f, 3.45f, 5.45f), Vector3.zero, teal, root, 0.020f);
            CreateTree("Rural Neem One", new Vector3(29f, 0f, 18f), foliage, trunk, root);
            CreateTree("Rural Neem Two", new Vector3(21f, 0f, 19f), foliage, trunk, root);
            CreateBox("Village Path", new Vector3(26f, 0.02f, 0f), new Vector3(4f, 0.10f, 22f), sand, root);
        }

        private static void CreateChapterNineCampaignPlaza(
            Transform root,
            Material darkStone,
            Material teal,
            Material yellow,
            Material white)
        {
            CreateBox("Jan Sabha Plaza", new Vector3(6f, -0.08f, 18f), new Vector3(23f, 0.18f, 9f), white, root);
            CreateBox("Public Debate Stage", new Vector3(18f, 0.65f, 1f), new Vector3(6f, 1.3f, 5f), darkStone, root);
            CreateBox("Public Debate Backdrop", new Vector3(20.8f, 2.4f, 1f), new Vector3(0.22f, 3.4f, 5f), teal, root);
            CreateWorldLabel("Debate Stage Sign", "PUBLIC ISSUE DEBATE", new Vector3(20.65f, 2.65f, 1f), new Vector3(0f, -90f, 0f), yellow, root, 0.021f);
            CreateBox("Nomination Plaza", new Vector3(5f, -0.05f, -14f), new Vector3(16f, 0.20f, 7f), teal, root);
        }

        private static void CreateChapterNineCrowd(
            string name,
            Vector3 centre,
            int count,
            Material first,
            Material second,
            Material third,
            Material lower,
            Material skin,
            Material hair)
        {
            GameObject crowd = new GameObject(name);
            crowd.transform.position = centre;
            for (int i = 0; i < count; i++)
            {
                float x = -4.2f + i * 1.65f;
                float z = i % 2 == 0 ? 0f : 1.2f;
                Material top = i % 3 == 0 ? first : i % 3 == 1 ? second : third;
                GameObject person = CreatePerson($"{name} Person {i + 1}", centre + new Vector3(x, 0f, z), top, lower, skin, hair, false);
                person.transform.localScale = Vector3.one * (0.88f + (i % 3) * 0.04f);
                WorldMotion motion = person.AddComponent<WorldMotion>();
                motion.Configure(WorldMotionKind.Float, 0.035f, 1.2f + i * 0.08f, Vector3.up);
            }
        }

        private static void CreateExpansionFlag(
            string name,
            Vector3 position,
            Material pole,
            Material cloth,
            Transform parent)
        {
            GameObject flag = new GameObject(name);
            flag.transform.SetParent(parent);
            flag.transform.position = position;
            CreatePrimitiveChild("Pole", PrimitiveType.Cylinder, flag.transform, new Vector3(0f, 2.3f, 0f), new Vector3(0.10f, 2.3f, 0.10f), pole);
            GameObject fabric = CreatePrimitiveChild("Fabric", PrimitiveType.Cube, flag.transform, new Vector3(0.95f, 3.75f, 0f), new Vector3(1.9f, 1.05f, 0.08f), cloth);
            WorldMotion motion = fabric.AddComponent<WorldMotion>();
            motion.Configure(WorldMotionKind.Sway, 5f, 1.4f, Vector3.forward);
        }

        private static void CreateExpansionLighting()
        {
            GameObject lightObject = new GameObject("Constituency Late Afternoon Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.91f, 0.72f);
            sunlight.intensity = 1.04f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.48f;
            lightObject.transform.rotation = Quaternion.Euler(47f, -38f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.45f, 0.68f, 0.82f);
            RenderSettings.ambientEquatorColor = new Color(0.66f, 0.58f, 0.43f);
            RenderSettings.ambientGroundColor = new Color(0.22f, 0.23f, 0.18f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.70f, 0.79f, 0.82f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 62f;
            RenderSettings.fogEndDistance = 165f;
        }
    }
}

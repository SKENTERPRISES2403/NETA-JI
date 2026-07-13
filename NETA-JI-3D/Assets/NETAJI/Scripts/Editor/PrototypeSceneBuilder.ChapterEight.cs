using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterEightScene(
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
            GameObject environment = new GameObject("Fictional Prayagraj Ward Governance");
            CreateBox("Ward Ground", new Vector3(0f, -0.3f, 0f), new Vector3(58f, 0.6f, 46f), stone, environment.transform);
            CreateBox("Main Civic Road", new Vector3(0f, 0.02f, -3f), new Vector3(11f, 0.08f, 40f), darkStone, environment.transform);
            CreateBox("Public Walkway Left", new Vector3(-8f, 0.03f, -3f), new Vector3(4f, 0.10f, 40f), sand, environment.transform);
            CreateBox("Public Walkway Right", new Vector3(8f, 0.03f, -3f), new Vector3(4f, 0.10f, 40f), sand, environment.transform);

            CreateBox("Ward Jan Seva Kendra", new Vector3(-17f, 2.7f, -10f), new Vector3(15f, 5.4f, 10f), teal, environment.transform);
            CreateBox("Water Works", new Vector3(17f, 2.7f, -10f), new Vector3(15f, 5.4f, 10f), yellow, environment.transform);
            CreateBox("Community Clinic", new Vector3(-17f, 2.7f, 10f), new Vector3(15f, 5.4f, 10f), white, environment.transform);
            CreateBox("Ward Learning Room", new Vector3(17f, 2.7f, 10f), new Vector3(15f, 5.4f, 10f), sand, environment.transform);
            CreateWorldLabel("Ward Office Sign", "WARD JAN SEVA KENDRA", new Vector3(-17f, 3.75f, -4.88f), Vector3.zero, yellow, environment.transform, 0.024f);
            CreateWorldLabel("Water Sign", "WATER WORKS  /  PUBLIC LOG", new Vector3(17f, 3.75f, -4.88f), Vector3.zero, darkStone, environment.transform, 0.021f);
            CreateWorldLabel("Clinic Sign", "COMMUNITY CLINIC  /  MEDICINE LOG", new Vector3(-17f, 3.75f, 4.88f), Vector3.zero, teal, environment.transform, 0.018f);
            CreateWorldLabel("School Sign", "WARD LEARNING ROOM", new Vector3(17f, 3.75f, 4.88f), Vector3.zero, darkStone, environment.transform, 0.023f);

            GameObject waterTank = new GameObject("Ward Water Tank");
            waterTank.transform.position = new Vector3(25.5f, 0f, -10f);
            CreatePrimitiveChild("Tank", PrimitiveType.Cylinder, waterTank.transform, new Vector3(0f, 3.4f, 0f), new Vector3(2.2f, 2.1f, 2.2f), teal);
            for (int i = 0; i < 4; i++)
            {
                float x = i < 2 ? -1.2f : 1.2f;
                float z = i % 2 == 0 ? -1.2f : 1.2f;
                CreatePrimitiveChild($"Tank Leg {i + 1}", PrimitiveType.Cube, waterTank.transform, new Vector3(x, 1.45f, z), new Vector3(0.28f, 2.9f, 0.28f), darkStone);
            }
            CreateBox("Visible Water Pipe", new Vector3(11.5f, 0.35f, -7f), new Vector3(10f, 0.30f, 0.30f), teal, environment.transform);

            for (int i = 0; i < 4; i++)
            {
                CreateStreetLamp($"Service Streetlight {i + 1}", new Vector3(i % 2 == 0 ? -6f : 6f, 0f, -12f + i * 8f), darkStone, yellow, environment.transform);
            }
            CreateTree("Ward Neem Left", new Vector3(-25f, 0f, 0f), foliage, trunk, environment.transform);
            CreateTree("Ward Neem Right", new Vector3(25f, 0f, 0f), foliage, trunk, environment.transform);

            GameObject systems = new GameObject("Chapter 8 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(8, 100, 350, 100, 56, 33, 64, 31, 67, 85, 59, true, 65, 75, 2, 80, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad", new Vector3(0f, 0f, -19f), shirt, trousers, skin, hair, true);
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
            gameCamera.farClipPlane = 175f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.6f, -24f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterEightMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white, darkStone, skin, hair);
            CreateGovernanceLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterEightScenePath);
        }

        private static void ConfigureChapterEightMission(
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

            GameObject secretary = CreatePerson("Ward Secretary", new Vector3(-17f, 0f, -4f), volunteerDress, darkStone, skin, hair, false);
            MissionObjective handover = AddObjective(secretary, "ward-handover", "Ward charge aur budget lein", "Ward Secretary",
                "Rs 30 lakh opening grant hai. Har work order, measurement aur payment public register mein jayega; party fund aur public budget alag rahenge.",
                0, 0, 0, false);
            handover.ConfigurePoliticalReward(1, 0, 0);
            handover.ConfigureGovernanceReward(0, 10, 30);
            objectives.Add(handover);
            labels.Add("Ward secretary se public budget aur responsibility handover lein");

            GameObject shanti = CreatePerson("Shanti Citizen Audit Lead", new Vector3(-10f, 0f, -1f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            MissionObjective audit = AddObjective(shanti, "baseline-audit", "100-day public baseline lock karein", "Shanti",
                "Paani, paperwork, lights, clinic aur learning room ki baseline photo, complaint count aur deadline yahan hai. Review slogans se nahi, isi sheet se hoga.",
                0, 0, 1, false, 2);
            audit.ConfigurePoliticalReward(0, 0, 1);
            audit.ConfigureGovernanceReward(4, 10, 0);
            objectives.Add(audit);
            labels.Add("Shanti ke saath citizen-audit baseline publish karein");

            GameObject engineer = CreatePerson("Fictional Water Engineer", new Vector3(16f, 0f, -4f), yellow, darkStone, skin, hair, false);
            MissionObjective water = AddObjective(engineer, "water-repair", "Water line repair verify karein", "Ward Engineer",
                "Leak section replace, pressure test pass aur lane-wise supply timing public. Rs 6 lakh measurement book ke baad release honge.",
                0, 0, 0, false);
            water.ConfigurePoliticalReward(0, 0, 2);
            water.ConfigureGovernanceReward(8, 4, -6);
            objectives.Add(water);
            labels.Add("Water line ka pressure test aur measured payment verify karein");

            GameObject serviceDesk = new GameObject("Paperwork Help Desk");
            serviceDesk.transform.position = new Vector3(-16f, 1f, -1f);
            CreatePrimitiveChild("Desk", PrimitiveType.Cube, serviceDesk.transform, Vector3.zero, new Vector3(2.6f, 1.0f, 1.1f), teal);
            CreatePrimitiveChild("Receipt Tray", PrimitiveType.Cube, serviceDesk.transform, new Vector3(0f, 0.65f, 0f), new Vector3(0.75f, 0.08f, 0.55f), yellow);
            MissionObjective paperwork = AddObjective(serviceDesk, "paperwork-desk", "Public help desk kholein", "Helpers Hand Volunteer",
                "Pension, birth certificate aur ration correction ke token free hain. Rs 3 lakh desk setup aur outreach ke liye, receipt har applicant ko milegi.",
                0, 0, 0, false, 2);
            paperwork.ConfigurePoliticalReward(0, 3, 0);
            paperwork.ConfigureGovernanceReward(7, 4, -3);
            objectives.Add(paperwork);
            labels.Add("Free paperwork help desk aur receipt system shuru karein");

            GameObject lightPanel = new GameObject("Streetlight Repair Panel");
            lightPanel.transform.position = new Vector3(0f, 1.2f, 1f);
            CreatePrimitiveChild("Panel", PrimitiveType.Cube, lightPanel.transform, Vector3.zero, new Vector3(2.8f, 1.8f, 0.18f), darkStone);
            CreatePrimitiveChild("Lamp Status A", PrimitiveType.Sphere, lightPanel.transform, new Vector3(-0.8f, 0.25f, -0.16f), Vector3.one * 0.22f, yellow);
            CreatePrimitiveChild("Lamp Status B", PrimitiveType.Sphere, lightPanel.transform, new Vector3(0f, 0.25f, -0.16f), Vector3.one * 0.22f, yellow);
            CreatePrimitiveChild("Lamp Status C", PrimitiveType.Sphere, lightPanel.transform, new Vector3(0.8f, 0.25f, -0.16f), Vector3.one * 0.22f, yellow);
            MissionObjective lights = AddObjective(lightPanel, "streetlights", "Streetlight work close karein", "Ward Electrician",
                "Twelve dark spots repaired, serial numbers logged aur night inspection complete. Rs 5 lakh bill tabhi close hoga jab residents verify karein.",
                0, 0, 0, false, 2);
            lights.ConfigurePoliticalReward(0, 0, 2);
            lights.ConfigureGovernanceReward(8, 3, -5);
            objectives.Add(lights);
            labels.Add("Resident verification ke saath streetlight repair close karein");

            GameObject clinicRegister = new GameObject("Clinic Medicine Register");
            clinicRegister.transform.position = new Vector3(-16f, 1f, 4f);
            CreatePrimitiveChild("Medicine Ledger", PrimitiveType.Cube, clinicRegister.transform, Vector3.zero, new Vector3(1.0f, 0.15f, 1.25f), teal);
            CreatePrimitiveChild("Clinic Cross Vertical", PrimitiveType.Cube, clinicRegister.transform, new Vector3(0f, 1.4f, 0f), new Vector3(0.32f, 1.2f, 0.18f), yellow);
            CreatePrimitiveChild("Clinic Cross Horizontal", PrimitiveType.Cube, clinicRegister.transform, new Vector3(0f, 1.4f, 0f), new Vector3(1.2f, 0.32f, 0.18f), yellow);
            MissionObjective clinic = AddObjective(clinicRegister, "clinic-stock", "Clinic medicine stock audit karein", "Clinic Pharmacist",
                "Batch, expiry aur daily issue register match hai. Essential stock display public rahega; shortage ko same din district portal par report karenge.",
                0, 0, 1, false, 2);
            clinic.ConfigurePoliticalReward(0, 0, 2);
            clinic.ConfigureGovernanceReward(7, 6, -4);
            objectives.Add(clinic);
            labels.Add("Community clinic ka medicine stock aur batch register audit karein");

            GameObject teacher = CreatePerson("Ward Learning Teacher", new Vector3(16f, 0f, 4f), white, darkStone, skin, hair, false);
            MissionObjective learning = AddObjective(teacher, "learning-room", "Ward learning room kholein", "Teacher",
                "School ke baad reading, English aur form-filling classes hongi. Rs 3 lakh books aur lights par, attendance bina party branding ke public hogi.",
                0, 0, 0, false);
            learning.ConfigurePoliticalReward(0, 2, 0);
            learning.ConfigureGovernanceReward(6, 3, -3);
            objectives.Add(learning);
            labels.Add("Non-partisan evening learning room aur attendance system shuru karein");

            GameObject tenderBoard = new GameObject("Ward Tender Decision Board");
            tenderBoard.transform.position = new Vector3(0f, 1.2f, 7f);
            CreatePrimitiveChild("Tender Board", PrimitiveType.Cube, tenderBoard.transform, Vector3.zero, new Vector3(3.4f, 2.0f, 0.20f), darkStone);
            CreateWorldLabel("Tender Label", "DRAIN WORK  /  DECISION", new Vector3(0f, 1.45f, 6.84f), Vector3.zero, yellow, tenderBoard.transform.parent, 0.023f);
            MissionObjective tender = AddObjective(tenderBoard, "governance-approach", "Drain work process chunein", "Azad",
                "Open e-tender, three bids, public measurement aur resident monitor. Kaam measured speed se hoga, par har rupee traceable rahega.",
                0, 0, 3, false, 2);
            tender.ConfigurePoliticalReward(2, 0, 3);
            tender.ConfigureGovernanceReward(8, 12, -5);
            tender.ConfigureDecision(
                "governance-approach",
                "DRAIN WORK KA RAASTA",
                "Monsoon paas hai. Open tender slower but auditable hai; ek verbal contractor fast completion promise karta hai, bina complete cost sheet ke.",
                "OPEN E-TENDER\nDelivery +8 / Integrity +12",
                "VERBAL SHORTCUT\nDelivery +14 / Integrity -15",
                "Verbal contractor kaam tez karta hai, par final bill estimate se Rs 3 lakh zyada aur measurement trail incomplete nikalta hai.",
                0, 0, -5, -3, 4, 0, 12, 0, 0, 14, -15, -8);
            objectives.Add(tender);
            labels.Add("Open e-tender ya unaudited verbal shortcut mein decision lein");

            GameObject samrat = CreatePerson("Constable Samrat Emergency Liaison", new Vector3(13f, 0f, 1f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective emergency = AddObjective(samrat, "monsoon-response", "Monsoon response coordinate karein", "Constable Samrat",
                "Police barricade aur safe route sambhalegi; ward team drain clearing, clinic access aur elderly residents ki list. Credit kisi party ka nahi, duty sabki hai.",
                0, 0, 1, false);
            emergency.ConfigurePoliticalReward(0, 4, 2);
            emergency.ConfigureGovernanceReward(10, 5, -2);
            objectives.Add(emergency);
            labels.Add("Samrat ke saath non-partisan monsoon emergency route coordinate karein");

            GameObject dashboard = new GameObject("Public Expenditure Dashboard");
            dashboard.transform.position = new Vector3(8f, 1.25f, 12f);
            CreatePrimitiveChild("Dashboard", PrimitiveType.Cube, dashboard.transform, Vector3.zero, new Vector3(3.4f, 2.1f, 0.20f), teal);
            for (int i = 0; i < 4; i++)
            {
                CreatePrimitiveChild($"Dashboard Bar {i + 1}", PrimitiveType.Cube, dashboard.transform,
                    new Vector3(-1.0f + i * 0.67f, -0.35f + i * 0.16f, -0.16f), new Vector3(0.34f, 0.45f + i * 0.28f, 0.06f), i % 2 == 0 ? yellow : white);
            }
            MissionObjective publicDashboard = AddObjective(dashboard, "public-dashboard", "Public expenditure dashboard publish karein", "Citizen Auditor",
                "Sanction, vendor, work photo, measurement aur balance ek board par. Objection window seven days open rahegi aur raw files bhi milengi.",
                0, 0, 1, false, 4);
            publicDashboard.ConfigurePoliticalReward(0, 0, 2);
            publicDashboard.ConfigureGovernanceReward(2, 12, 0);
            objectives.Add(publicDashboard);
            labels.Add("Work-wise payment aur remaining budget public dashboard par publish karein");

            GameObject moderator = CreatePerson("Jan Sunwai Moderator", new Vector3(0f, 0f, 13f), volunteerDress, darkStone, skin, hair, false);
            for (int i = 0; i < 6; i++)
            {
                float x = -5f + i * 2f;
                GameObject resident = CreatePerson($"Jan Sunwai Resident {i + 1}", new Vector3(x, 0f, 16f + (i % 2) * 1.2f),
                    i % 2 == 0 ? yellow : teal, darkStone, skin, hair, false);
                resident.transform.localScale = Vector3.one * (0.90f + (i % 3) * 0.04f);
            }
            MissionObjective sunwai = AddObjective(moderator, "jan-sunwai", "100-day jan-sunwai karein", "Independent Moderator",
                "Complaints pehle, speech baad mein. Closed, pending aur rejected cases reason ke saath read out honge; opposition residents ko equal mic time milega.",
                0, 0, 4, false);
            sunwai.ConfigurePoliticalReward(2, 0, 0);
            sunwai.ConfigureCampaignReward(5, 0);
            sunwai.ConfigureGovernanceReward(5, 6, 0);
            objectives.Add(sunwai);
            labels.Add("Independent moderator ke saath open jan-sunwai complete karein");

            GameObject reviewBoard = new GameObject("Hundred Day Public Review Board");
            reviewBoard.transform.position = new Vector3(17f, 1.4f, 17f);
            CreatePrimitiveChild("Review Screen", PrimitiveType.Cube, reviewBoard.transform, Vector3.zero, new Vector3(4.2f, 2.5f, 0.20f), darkStone);
            CreateWorldLabel("Review Ready", "100-DAY PUBLIC REVIEW", new Vector3(17f, 1.72f, 16.84f), Vector3.zero, yellow, reviewBoard.transform.parent, 0.023f);
            MissionObjective review = AddObjective(reviewBoard, "hundred-day-review", "100-day score dekhein", "Citizen Audit Panel",
                "Public review calculation ready.", 0, 0, 0, false);
            review.ConfigureHundredDayReview();
            objectives.Add(review);
            labels.Add("Citizen audit panel par computed 100-day review dekhein");

            mission.Configure(
                "Pehle 100 Din",
                objectives,
                labels,
                "CHAPTER 8 COMPLETE",
                "Mandate ab measurable public work mein badla. Har shortcut ka hisaab save ho chuka hai.");
            mission.ConfigureMilestones(
                new List<int> { 4, 7, 10 },
                new List<string> { "PUBLIC BASELINE LIVE", "CORE SERVICES MOVING", "AUDIT WINDOW OPEN" },
                new List<string>
                {
                    "Budget, audit baseline, water work aur paperwork desk public record mein hain.",
                    "Lights, clinic aur learning room active hain. Ab monsoon drain process ka hard decision lo.",
                    "Emergency response complete. Dashboard aur jan-sunwai se 100-day review lock hoga."
                });
            mission.ConfigureChapter(8, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 8 / PEHLE 100 DIN",
                "Election jeetna shuruaat thi. Ab budget, delivery aur imaandari ka public test hai.");
        }

        private static void CreateGovernanceLighting()
        {
            GameObject lightObject = new GameObject("Hundred Day Morning Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.92f, 0.76f);
            sunlight.intensity = 1.02f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.46f;
            lightObject.transform.rotation = Quaternion.Euler(42f, -34f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.48f, 0.69f, 0.82f);
            RenderSettings.ambientEquatorColor = new Color(0.61f, 0.58f, 0.47f);
            RenderSettings.ambientGroundColor = new Color(0.21f, 0.23f, 0.21f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.69f, 0.77f, 0.80f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 52f;
            RenderSettings.fogEndDistance = 145f;
        }
    }
}

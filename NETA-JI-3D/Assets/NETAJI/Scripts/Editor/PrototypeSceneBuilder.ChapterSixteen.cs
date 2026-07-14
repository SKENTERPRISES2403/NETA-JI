using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterSixteenScene(
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
            GameObject environment = new GameObject("Fictional State Capital Governance Campus");
            CreateChapterSixteenEnvironment(
                environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 16 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                16, 100, 10, 100, 100, 62, 263, 83, 67, 85, 59, true,
                65, 87, 5, 85, true, 88, 93, 99, 91, true, 58, 92, 69, 58, true,
                78, 80, 90, 30, 78, true, 78, 90, 92, 89, true,
                80, 89, 98, 82, 6, true, 84, 86, 88, 87, true,
                78, 100, 90, 56, 27, true, 92, 100, 80, 88, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson(
                "Azad Fictional Chief Minister", new Vector3(0f, 0f, -28f), white, white, skin, hair, true);
            AddCandidateOutfit(azad.transform, white, teal, yellow, darkStone);
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
            gameCamera.farClipPlane = 240f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 4.0f, -33f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterSixteenMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterSixteenLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterSixteenScenePath);
        }

        private static void ConfigureChapterSixteenMission(
            MissionController mission,
            Material shantiDress,
            Material volunteerDress,
            Material policeKhaki,
            Material teal,
            Material yellow,
            Material white,
            Material shirt,
            Material trousers,
            Material darkStone,
            Material skin,
            Material hair)
        {
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject oathAuditor = CreatePerson(
                "Constitutional Mandate Auditor", new Vector3(0f, 0f, -22f), white, darkStone, skin, hair, false);
            MissionObjective oath = AddObjective(
                oathAuditor,
                "cm-mandate-oath",
                "CM mandate aur oath record verify karein",
                "Independent Constitutional Auditor",
                "Twenty-seven of forty fictional seats, public asset disclosure, conflict register and constitutional oath verified. Party office aur state administration ke records alag rahenge.",
                0, 0, 0, false, 1);
            oath.ConfigurePoliticalReward(0, 0, -12);
            oath.ConfigureChiefMinisterGovernanceReward(5, 10, 4);
            objectives.Add(oath);
            labels.Add("Independent auditor se majority, oath, assets and conflict register verify karein");

            GameObject cabinetWall = new GameObject("Cabinet Disclosure Wall");
            cabinetWall.transform.position = new Vector3(-14f, 2.1f, -15f);
            CreatePrimitiveChild("Cabinet Wall Screen", PrimitiveType.Cube, cabinetWall.transform, Vector3.zero, new Vector3(8.4f, 4.1f, 0.28f), darkStone);
            for (int index = 0; index < 12; index++)
            {
                CreatePrimitiveChild(
                    $"Cabinet Public File {index + 1}", PrimitiveType.Cube, cabinetWall.transform,
                    new Vector3(-3.15f + (index % 6) * 1.26f, 0.78f - (index / 6) * 1.55f, -0.18f),
                    new Vector3(0.82f, 0.68f, 0.06f), index % 3 == 0 ? yellow : teal);
            }
            MissionObjective cabinet = AddObjective(
                cabinetWall,
                "cabinet-disclosures",
                "Cabinet disclosure wall publish karein",
                "Cabinet Ethics Secretary",
                "Twelve fictional ministers ke qualifications, assets, conflicts, department targets and recusal rules public hain. Family data aur identity-based scoring excluded hai.",
                0, 0, 0, false, 2);
            cabinet.ConfigurePoliticalReward(0, 0, -3);
            cabinet.ConfigureChiefMinisterGovernanceReward(6, 14, 5);
            objectives.Add(cabinet);
            labels.Add("Twelve-member cabinet ka public disclosure and recusal register publish karein");

            GameObject budgetHall = new GameObject("State Open Budget Hall");
            budgetHall.transform.position = new Vector3(-28f, 1.25f, -6f);
            CreatePrimitiveChild("Budget Public Counter", PrimitiveType.Cube, budgetHall.transform, Vector3.zero, new Vector3(7.4f, 1.4f, 2.4f), teal);
            for (int index = 0; index < 5; index++)
            {
                float height = 0.55f + index * 0.24f;
                CreatePrimitiveChild(
                    $"Budget Priority Bar {index + 1}", PrimitiveType.Cube, budgetHall.transform,
                    new Vector3(-2.4f + index * 1.2f, 1.0f + height * 0.5f, 0f),
                    new Vector3(0.72f, height, 0.42f), index % 2 == 0 ? yellow : white);
            }
            MissionObjective budget = AddObjective(
                budgetHall,
                "state-open-budget",
                "State open budget lock karein",
                "Public Finance Secretary",
                "Health, schools, local services, safety and resilience ke costed allocations, debt limits, contingency reserve and monthly spend data public dashboard par locked hain.",
                0, 0, 0, false);
            budget.ConfigurePoliticalReward(0, 0, 2);
            budget.ConfigureChiefMinisterGovernanceReward(5, 8, 14);
            objectives.Add(budget);
            labels.Add("Costed priorities, debt limit and contingency reserve ke saath open budget lock karein");

            GameObject healthLead = CreatePerson(
                "State Health Command Lead", new Vector3(-29f, 0f, 7f), shirt, trousers, skin, hair, false);
            GameObject healthStore = new GameObject("Essential Medicine Buffer Store");
            healthStore.transform.position = new Vector3(-34f, 0.55f, 9f);
            for (int index = 0; index < 12; index++)
            {
                CreatePrimitiveChild(
                    $"Audited Medicine Crate {index + 1}", PrimitiveType.Cube, healthStore.transform,
                    new Vector3((index % 4) * 1.05f, (index / 4) * 0.78f, 0f),
                    new Vector3(0.82f, 0.58f, 0.82f), index % 2 == 0 ? white : teal);
            }
            MissionObjective health = AddObjective(
                healthLead,
                "health-command",
                "Essential health command activate karein",
                "State Health Command Lead",
                "Fictional district hospitals ke medicine stock, maternal referrals, ambulance response and complaint escalation live hai. Failed tender network ko open re-bid aur audit hold mila.",
                0, 0, 0, false, 1);
            health.ConfigurePoliticalReward(0, 6, 2);
            health.ConfigureChiefMinisterGovernanceReward(12, 6, 8);
            objectives.Add(health);
            labels.Add("Medicine, maternal referral, ambulance and hospital grievance command activate karein");

            GameObject educationBoard = new GameObject("State Learning Mission Board");
            educationBoard.transform.position = new Vector3(-20f, 2.0f, 20f);
            CreatePrimitiveChild("Learning Mission Screen", PrimitiveType.Cube, educationBoard.transform, Vector3.zero, new Vector3(8.0f, 3.9f, 0.28f), darkStone);
            for (int index = 0; index < 8; index++)
            {
                CreatePrimitiveChild(
                    $"Learning Indicator {index + 1}", PrimitiveType.Cube, educationBoard.transform,
                    new Vector3(-2.9f + (index % 4) * 1.9f, 0.72f - (index / 4) * 1.45f, -0.18f),
                    new Vector3(1.25f, 0.55f, 0.06f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective education = AddObjective(
                educationBoard,
                "state-learning-mission",
                "Learning mission baseline karein",
                "Public Education Mission Lead",
                "Teacher support, attendance, reading-maths baseline, accessible classrooms and dropout outreach district-wise measured hain. Rank publicity se pehle learning evidence aayega.",
                0, 0, 0, false);
            education.ConfigurePoliticalReward(0, 8, 1);
            education.ConfigureChiefMinisterGovernanceReward(12, 4, 8);
            objectives.Add(education);
            labels.Add("Teacher support, foundational learning and dropout outreach baseline karein");

            GameObject shanti = CreatePerson(
                "Shanti State Safety Forum", new Vector3(-4f, 0f, 22f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            CreateChapterNineCrowd(
                "Women Youth Caregiver Safety Forum", new Vector3(-4f, 0f, 25f), 10,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective safetyForum = AddObjective(
                shanti,
                "inclusive-state-safety",
                "Shanti ke saath safety forum karein",
                "Shanti",
                "Women, children, caregivers, students and disabled residents ne helpline, safe transport, shelter and case-follow-up gaps anonymous records ke saath review kiye. Photo-op ke bina owners fixed hain.",
                0, 0, 0, false);
            safetyForum.ConfigurePoliticalReward(0, 6, 2);
            safetyForum.ConfigureChiefMinisterGovernanceReward(10, 8, 4);
            objectives.Add(safetyForum);
            labels.Add("Shanti-led safety and access forum se accountable action owners fix karein");

            GameObject strategyStage = new GameObject("CM Governance Control Stage");
            strategyStage.transform.position = new Vector3(15f, 1.15f, 19f);
            CreatePrimitiveChild("Governance Stage", PrimitiveType.Cube, strategyStage.transform, Vector3.zero, new Vector3(8.0f, 1.0f, 4.8f), darkStone);
            CreatePrimitiveChild("Governance Backdrop", PrimitiveType.Cube, strategyStage.transform, new Vector3(0f, 2.0f, 2.1f), new Vector3(8.0f, 3.2f, 0.24f), teal);
            CreateWorldLabel(
                "Governance Strategy Label", "OPEN METRICS  /  SHARED OWNERSHIP",
                new Vector3(15f, 3.42f, 21.0f), Vector3.zero, yellow, strategyStage.transform.parent, 0.020f);
            MissionObjective strategy = AddObjective(
                strategyStage,
                "cm-governance-approach",
                "100-day governance model chunein",
                "Independent Delivery Council",
                "Open district dashboard slower aur Rs 20 costly hai, lekin cabinet targets, audit trails and fiscal corrections public rahenge.",
                0, -20, 0, false);
            strategy.ConfigurePoliticalReward(3, 10, 4);
            strategy.ConfigureChiefMinisterGovernanceReward(12, 16, 10);
            strategy.ConfigureDecision(
                "cm-governance-approach",
                "GOVERNANCE CONTROL MODEL",
                "Open dashboard departments ko measured ownership deta hai. Centralized headline drive fast orders and visibility deta hai, par cabinet scrutiny and fiscal checks weak kar sakta hai.",
                "OPEN DISTRICT DASHBOARD\nIntegrity +16 / Fiscal +10",
                "CENTRALIZED HEADLINE DRIVE\nDelivery +20 / Power +9",
                "Central command ne visible targets tez kiye, lekin cabinet dissent, procurement review and contingency checks cut hue. Independent audit ab tougher hoga.",
                0, 0, -10, -8, 9, 4, 15,
                riskyChiefMinisterDelivery: 20,
                riskyCabinetIntegrity: -12,
                riskyStateFiscalDiscipline: -8);
            objectives.Add(strategy);
            labels.Add("Open district dashboard ya centralized headline drive ka governance decision lein");

            GameObject grievanceBoard = new GameObject("District Grievance Command Board");
            grievanceBoard.transform.position = new Vector3(28f, 2.2f, 9f);
            CreatePrimitiveChild("Grievance Command Screen", PrimitiveType.Cube, grievanceBoard.transform, Vector3.zero, new Vector3(8.2f, 4.3f, 0.28f), darkStone);
            for (int index = 0; index < 18; index++)
            {
                CreatePrimitiveChild(
                    $"Fictional District Status {index + 1}", PrimitiveType.Cube, grievanceBoard.transform,
                    new Vector3(-3.15f + (index % 6) * 1.25f, 1.12f - (index / 6) * 1.05f, -0.18f),
                    new Vector3(0.78f, 0.54f, 0.06f), index % 4 == 0 ? yellow : teal);
            }
            MissionObjective grievance = AddObjective(
                grievanceBoard,
                "district-grievance-command",
                "District grievance command run karein",
                "Citizen Service Commissioner",
                "Eighteen fictional districts ke application delays, health, school, pension and local-service complaints receipt, deadline, appeal and closure proof ke saath tracked hain.",
                0, 0, 0, false);
            grievance.ConfigurePoliticalReward(0, 6, 2);
            grievance.ConfigureChiefMinisterGovernanceReward(10, 8, 5);
            objectives.Add(grievance);
            labels.Add("Eighteen fictional districts ka receipt-to-appeal grievance dashboard run karein");

            GameObject samrat = CreatePerson(
                "Constable Samrat State Safety Liaison", new Vector3(30f, 0f, -4f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective disaster = AddObjective(
                samrat,
                "state-disaster-readiness",
                "Disaster and crowd protocol drill karein",
                "Constable Samrat",
                "Heatwave, flood, stampede and missing-child response ka neutral multi-agency drill pass hua. Police public safety karegi; political office operations direct nahi karega.",
                0, 0, 0, false, 1);
            disaster.ConfigurePoliticalReward(0, 4, -2);
            disaster.ConfigureChiefMinisterGovernanceReward(8, 8, 7);
            objectives.Add(disaster);
            labels.Add("Samrat ke saath heat, flood, crowd and missing-child response drill pass karein");

            GameObject procurementDesk = new GameObject("Transparent Procurement Audit Desk");
            procurementDesk.transform.position = new Vector3(23f, 1.0f, -16f);
            CreatePrimitiveChild("Procurement Counter", PrimitiveType.Cube, procurementDesk.transform, Vector3.zero, new Vector3(6.5f, 1.25f, 2.2f), teal);
            CreatePrimitiveChild("Bid Register", PrimitiveType.Cube, procurementDesk.transform, new Vector3(-1.5f, 0.78f, 0f), new Vector3(1.2f, 0.16f, 0.85f), white);
            CreatePrimitiveChild("Conflict Register", PrimitiveType.Cube, procurementDesk.transform, new Vector3(0f, 0.78f, 0f), new Vector3(1.2f, 0.16f, 0.85f), yellow);
            CreatePrimitiveChild("Appeal Register", PrimitiveType.Cube, procurementDesk.transform, new Vector3(1.5f, 0.78f, 0f), new Vector3(1.2f, 0.16f, 0.85f), white);
            MissionObjective procurement = AddObjective(
                procurementDesk,
                "state-procurement-audit",
                "Procurement red-flag audit karein",
                "Independent Procurement Auditor",
                "Tender notices, bidder conflicts, price comparison, delivery inspection, appeals and cancelled contracts sample-checked. Political donor ko automatic preference nahi mil sakti.",
                0, 0, 0, false, 2);
            procurement.ConfigurePoliticalReward(0, 0, -3);
            procurement.ConfigureChiefMinisterGovernanceReward(5, 12, 10);
            objectives.Add(procurement);
            labels.Add("Tender, conflict, price, delivery and appeal records ka red-flag audit karein");

            GameObject hearingLead = CreatePerson(
                "State Jan Sunwai Moderator", new Vector3(6f, 0f, -18f), volunteerDress, darkStone, skin, hair, false);
            CreateChapterNineCrowd(
                "Open State Jan Sunwai", new Vector3(6f, 0f, -21f), 12,
                shirt, volunteerDress, yellow, darkStone, skin, hair);
            MissionObjective hearing = AddObjective(
                hearingLead,
                "state-public-hearing",
                "Open 100-day jan sunwai karein",
                "Independent Jan Sunwai Moderator",
                "Residents, opposition legislators, press and service workers ko equal question queue mila. Unresolved questions, corrections and deadlines public minutes mein hain.",
                0, 0, 0, false);
            hearing.ConfigurePoliticalReward(0, 5, 2);
            hearing.ConfigureChiefMinisterGovernanceReward(7, 7, 5);
            objectives.Add(hearing);
            labels.Add("Residents, opposition and press ke saath equal-time 100-day jan sunwai karein");

            GameObject reviewWall = new GameObject("Independent CM Hundred Day Review Wall");
            reviewWall.transform.position = new Vector3(0f, 2.45f, -9f);
            CreatePrimitiveChild("Review Wall", PrimitiveType.Cube, reviewWall.transform, Vector3.zero, new Vector3(10.5f, 4.9f, 0.30f), darkStone);
            CreatePrimitiveChild("Delivery Score Bar", PrimitiveType.Cube, reviewWall.transform, new Vector3(-2.7f, 0.8f, -0.19f), new Vector3(1.25f, 2.2f, 0.07f), teal);
            CreatePrimitiveChild("Integrity Score Bar", PrimitiveType.Cube, reviewWall.transform, new Vector3(0f, 1.0f, -0.19f), new Vector3(1.25f, 2.6f, 0.07f), yellow);
            CreatePrimitiveChild("Fiscal Score Bar", PrimitiveType.Cube, reviewWall.transform, new Vector3(2.7f, 0.55f, -0.19f), new Vector3(1.25f, 1.7f, 0.07f), white);
            CreateWorldLabel(
                "CM Review Wall Label", "INDEPENDENT 100-DAY STATE REVIEW",
                new Vector3(0f, 5.12f, -9.2f), Vector3.zero, yellow, reviewWall.transform.parent, 0.024f);
            MissionObjective review = AddObjective(
                reviewWall,
                "cm-hundred-day-review",
                "Independent CM review dekhein",
                "Independent State Performance Panel",
                "Delivery, cabinet integrity, fiscal discipline, leadership record and opposition pressure ka computed public audit ready hai.",
                0, 0, 0, false);
            review.ConfigureChiefMinisterHundredDayReview();
            objectives.Add(review);
            labels.Add("Independent panel par delivery, integrity, fiscal and full-term result dekhein");

            mission.Configure(
                "CM Ke Pehle 100 Din",
                objectives,
                labels,
                "CHAPTER 16 COMPLETE",
                "Independent 100-day audit pass hua. State reform programme full term ke liye approved hai.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "CABINET AND BUDGET OPEN", "DELIVERY MODEL ACTIVE", "STATE SYSTEMS AUDITED" },
                new List<string>
                {
                    "Mandate, cabinet and budget records public hain. Health, education and safety delivery next hai.",
                    "Health, learning, safety and governance model active hain. District execution and audits next hain.",
                    "Grievance, disaster and procurement systems audited. Open jan sunwai final review unlock karegi."
                });
            mission.ConfigureChapter(16, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 16 / CM KE PEHLE 100 DIN",
                "Kursi mil gayi. Ab har order ko result, record aur public audit mein badalna hoga.");
        }

        private static void CreateChapterSixteenEnvironment(
            Transform root,
            Material sand,
            Material stone,
            Material darkStone,
            Material teal,
            Material yellow,
            Material white,
            Material foliage,
            Material trunk)
        {
            CreateBox("State Capital Campus Ground", new Vector3(0f, -0.32f, 0f), new Vector3(100f, 0.64f, 72f), sand, root);
            CreateBox("Public Accountability Avenue", new Vector3(0f, 0.02f, -3f), new Vector3(11f, 0.08f, 66f), stone, root);
            CreateBox("Delivery Mission Crossway", new Vector3(0f, 0.03f, 12f), new Vector3(84f, 0.08f, 7f), stone, root);

            CreateBox("Fictional State Secretariat", new Vector3(0f, 4.6f, 31f), new Vector3(40f, 9.2f, 9f), white, root);
            CreateBox("Secretariat Central Portico", new Vector3(0f, 3.2f, 25.8f), new Vector3(15f, 6.4f, 2.5f), teal, root);
            for (int index = 0; index < 6; index++)
            {
                CreateBox(
                    $"Secretariat Column {index + 1}", new Vector3(-6.0f + index * 2.4f, 3.2f, 24.35f),
                    new Vector3(0.48f, 6.4f, 0.48f), index % 2 == 0 ? white : yellow, root);
            }
            CreateBox("Secretariat Accountability Canopy", new Vector3(0f, 8.8f, 26.8f), new Vector3(43f, 0.48f, 4.1f), darkStone, root);
            CreateWorldLabel(
                "Secretariat Sign", "FICTIONAL STATE SECRETARIAT",
                new Vector3(0f, 6.4f, 24.48f), Vector3.zero, teal, root, 0.030f);
            CreateWorldLabel(
                "Secretariat Motto", "SEVA  /  RECORD  /  REVIEW",
                new Vector3(0f, 4.9f, 24.46f), Vector3.zero, yellow, root, 0.023f);

            CreateBox("Open Budget Pavilion", new Vector3(-36f, 3.1f, -5f), new Vector3(17f, 6.2f, 20f), teal, root);
            CreateWorldLabel(
                "Budget Pavilion Sign", "OPEN STATE BUDGET",
                new Vector3(-27.38f, 4.2f, -5f), new Vector3(0f, -90f, 0f), yellow, root, 0.022f);

            CreateBox("Health And Learning Mission Hall", new Vector3(-31f, 3.0f, 20f), new Vector3(25f, 6f, 12f), white, root);
            CreateWorldLabel(
                "Mission Hall Sign", "HEALTH  +  LEARNING COMMAND",
                new Vector3(-31f, 4.15f, 13.88f), Vector3.zero, teal, root, 0.021f);

            CreateBox("District Delivery Command", new Vector3(35f, 3.2f, 7f), new Vector3(18f, 6.4f, 24f), teal, root);
            CreateWorldLabel(
                "District Command Sign", "18 FICTIONAL DISTRICTS",
                new Vector3(25.88f, 4.35f, 7f), new Vector3(0f, 90f, 0f), yellow, root, 0.021f);

            CreateBox("Audit And Safety Hall", new Vector3(30f, 3f, -19f), new Vector3(26f, 6f, 12f), white, root);
            CreateWorldLabel(
                "Audit Hall Sign", "SAFETY  /  PROCUREMENT  /  APPEAL",
                new Vector3(30f, 4.1f, -12.88f), Vector3.zero, teal, root, 0.020f);

            CreateBox("Jan Sunwai Plaza", new Vector3(5f, -0.07f, -21f), new Vector3(26f, 0.18f, 11f), white, root);
            CreateBox("Cabinet Accountability Plaza", new Vector3(-13f, -0.07f, -14f), new Vector3(20f, 0.18f, 10f), white, root);

            for (int index = 0; index < 12; index++)
            {
                CreateExpansionFlag(
                    $"Governance Campus Flag {index + 1}", new Vector3(-44f + index * 8f, 0f, -32f),
                    darkStone, index % 3 == 0 ? yellow : teal, root);
            }

            for (int index = 0; index < 11; index++)
            {
                CreateStreetLamp(
                    $"Capital Avenue Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -7f : 7f, 0f, -27f + index * 5.9f), darkStone, yellow, root);
            }

            CreateTree("Capital Neem North West", new Vector3(-46f, 0f, 31f), foliage, trunk, root);
            CreateTree("Capital Neem North East", new Vector3(46f, 0f, 31f), foliage, trunk, root);
            CreateTree("Capital Neem South West", new Vector3(-46f, 0f, -29f), foliage, trunk, root);
            CreateTree("Capital Neem South East", new Vector3(46f, 0f, -29f), foliage, trunk, root);
        }

        private static void CreateChapterSixteenLighting()
        {
            GameObject lightObject = new GameObject("State Governance Morning Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.93f, 0.79f);
            sunlight.intensity = 1.08f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.48f;
            lightObject.transform.rotation = Quaternion.Euler(47f, -35f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.48f, 0.71f, 0.88f);
            RenderSettings.ambientEquatorColor = new Color(0.72f, 0.65f, 0.50f);
            RenderSettings.ambientGroundColor = new Color(0.25f, 0.26f, 0.21f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.76f, 0.84f, 0.89f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 84f;
            RenderSettings.fogEndDistance = 225f;
        }
    }
}

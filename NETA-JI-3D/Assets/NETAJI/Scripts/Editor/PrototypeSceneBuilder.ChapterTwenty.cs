using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterTwentyScene(
            Material sand, Material stone, Material darkStone, Material teal, Material yellow,
            Material white, Material shirt, Material trousers, Material skin, Material hair,
            Material shantiDress, Material volunteerDress, Material policeKhaki,
            Material foliage, Material trunk)
        {
            Scene chapterScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject environment = new GameObject("Fictional Five Year Opposition Service Campus");
            CreateChapterTwentyEnvironment(environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 20 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                20, 100, 0, 100, 100, 75, 490, 88, 67, 85, 59, true,
                65, 87, 5, 85, true, 88, 93, 99, 91, true, 58, 92, 69, 58, true,
                78, 80, 90, 30, 78, true, 78, 90, 92, 89, true,
                80, 89, 98, 82, 6, true, 84, 86, 88, 87, true,
                78, 100, 90, 56, 27, true, 92, 100, 80, 88, true,
                93, 87, 92, 85, 87, true, 96, 100, 96, 90, 18, true,
                84, 100, 92, 49, 42, true, false, 94, 92, 92, 84, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson(
                "Azad Responsible Opposition Leader", new Vector3(0f, 0f, -31f), white, white, skin, hair, true);
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
            gameCamera.fieldOfView = 63f;
            gameCamera.nearClipPlane = 0.12f;
            gameCamera.farClipPlane = 250f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 4.0f, -36f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterTwentyMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterTwentyLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterTwentyScenePath);
        }

        private static void ConfigureChapterTwentyMission(
            MissionController mission, Material shantiDress, Material volunteerDress,
            Material policeKhaki, Material teal, Material yellow, Material white,
            Material shirt, Material trousers, Material darkStone, Material skin, Material hair)
        {
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject acceptance = CreatePerson("Election Result Acceptance Archivist", new Vector3(0f, 0f, -24f), shirt, darkStone, skin, hair, false);
            MissionObjective acceptanceAudit = AddObjective(
                acceptance, "result-acceptance-record", "Election result acceptance record sign karein",
                "Independent Democratic Record Archivist",
                "Count forms, concession statement, supporter de-escalation and peaceful transition record archived hain. Defeat ko conspiracy story mein convert nahi kiya gaya.",
                0, 0, 0, false);
            acceptanceAudit.ConfigurePoliticalReward(0, 0, -8);
            acceptanceAudit.ConfigureOppositionTermReward(6, 6, 4);
            objectives.Add(acceptanceAudit);
            labels.Add("Count, concession, de-escalation and peaceful-transition record sign karein");

            GameObject attendanceBoard = new GameObject("Opposition Attendance And Questions Board");
            attendanceBoard.transform.position = new Vector3(-23f, 2.1f, -18f);
            CreatePrimitiveChild("Attendance Public Screen", PrimitiveType.Cube, attendanceBoard.transform, Vector3.zero, new Vector3(9.2f, 4.2f, 0.28f), darkStone);
            for (int index = 0; index < 10; index++)
            {
                CreatePrimitiveChild($"Question Record {index + 1}", PrimitiveType.Cube, attendanceBoard.transform,
                    new Vector3(-3.4f + (index % 5) * 1.7f, 0.8f - (index / 5) * 1.55f, -0.18f),
                    new Vector3(1.0f, 0.68f, 0.06f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective attendance = AddObjective(
                attendanceBoard, "opposition-attendance", "Attendance and public-question record publish karein",
                "Fictional Council Accountability Office",
                "Attendance, researched questions, committee notes, amendment drafts and conflict recusals public hain. Walkout ko default performance metric nahi banaya gaya.",
                0, 0, 0, false);
            attendance.ConfigurePoliticalReward(0, 4, 2);
            attendance.ConfigureOppositionTermReward(10, 8, 8);
            objectives.Add(attendance);
            labels.Add("Attendance, questions, committees, amendments and recusals publish karein");

            GameObject serviceLead = CreatePerson("National Public Service Network Lead", new Vector3(-37f, 0f, -4f), volunteerDress, darkStone, skin, hair, false);
            CreateChapterNineCrowd("Opposition Public Service Volunteers", new Vector3(-37f, 0f, 0f), 12,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective service = AddObjective(
                serviceLead, "opposition-service-network", "Public service network five saal chalayein",
                "Public Service Network Lead",
                "Paperwork help, legal referral, school support, health navigation and disaster helpline government credit se compete kiye bina chal rahe hain.",
                0, 0, 0, false);
            service.ConfigurePoliticalReward(0, 10, 2);
            service.ConfigureOppositionTermReward(14, 6, 6);
            objectives.Add(service);
            labels.Add("Paperwork, referral, school, health and helpline service network chalayein");

            GameObject policyLab = new GameObject("Shadow Policy Evidence Lab");
            policyLab.transform.position = new Vector3(-35f, 2.1f, 15f);
            CreatePrimitiveChild("Shadow Policy Screen", PrimitiveType.Cube, policyLab.transform, Vector3.zero, new Vector3(9.4f, 4.2f, 0.28f), darkStone);
            for (int index = 0; index < 9; index++)
            {
                float height = 0.55f + (index % 3) * 0.36f;
                CreatePrimitiveChild($"Policy Correction Bar {index + 1}", PrimitiveType.Cube, policyLab.transform,
                    new Vector3(-3.2f + (index % 3) * 3.2f, -1.2f + (index / 3) * 1.3f + height * 0.5f, -0.18f),
                    new Vector3(1.7f, height, 0.06f), index % 2 == 0 ? yellow : teal);
            }
            MissionObjective policy = AddObjective(
                policyLab, "shadow-policy-lab", "Shadow policy alternatives publish karein",
                "Independent Policy Evidence Lab",
                "Government proposal par sourced critique ke saath costed alternative, implementation sequence and correction note publish hua. Sirf oppose karna policy nahi maana gaya.",
                0, 0, 0, false);
            policy.ConfigurePoliticalReward(0, 6, 2);
            policy.ConfigureOppositionTermReward(6, 8, 14);
            objectives.Add(policy);
            labels.Add("Sourced critique ke saath costed alternatives aur corrections publish karein");

            GameObject reliefLead = CreatePerson("Cross Party Relief Coordinator", new Vector3(-18f, 0f, 26f), volunteerDress, trousers, skin, hair, false);
            CreateChapterNineCrowd("Neutral Disaster Relief Cohort", new Vector3(-18f, 0f, 30f), 10,
                volunteerDress, white, teal, darkStone, skin, hair);
            MissionObjective relief = AddObjective(
                reliefLead, "cross-party-relief", "Disaster relief mein institution ke saath cooperate karein",
                "Neutral Relief Coordinator",
                "Fictional flood response mein government control room, local volunteers and opposition helpline shared needs list par kaam kar rahe hain. Branding relief se door rakhi gayi.",
                0, 0, 0, false);
            relief.ConfigurePoliticalReward(0, 8, -3);
            relief.ConfigureOppositionTermReward(10, 8, 6);
            objectives.Add(relief);
            labels.Add("Neutral flood relief, shared needs list and referral response coordinate karein");

            GameObject watchdog = CreatePerson("Evidence Watchdog Editor", new Vector3(0f, 0f, 27f), shirt, darkStone, skin, hair, false);
            MissionObjective watchdogObjective = AddObjective(
                watchdog, "evidence-watchdog", "Evidence-led watchdog report defend karein",
                "Independent Evidence Watchdog Editor",
                "Documents, response rights, source protection, corrections and legal review complete hain. Unverified accusation headline mein publish nahi hui.",
                0, 0, 0, false, 3);
            watchdogObjective.ConfigurePoliticalReward(0, 0, 4);
            watchdogObjective.ConfigureOppositionTermReward(8, 6, 10);
            objectives.Add(watchdogObjective);
            labels.Add("Documents, response rights, source protection and corrections defend karein");

            GameObject strategyStage = new GameObject("Responsible Opposition Strategy Stage");
            strategyStage.transform.position = new Vector3(19f, 1.2f, 22f);
            CreatePrimitiveChild("Opposition Strategy Platform", PrimitiveType.Cube, strategyStage.transform,
                Vector3.zero, new Vector3(9f, 1f, 5f), darkStone);
            CreatePrimitiveChild("Opposition Strategy Backdrop", PrimitiveType.Cube, strategyStage.transform,
                new Vector3(0f, 2f, 2.2f), new Vector3(9f, 3.4f, 0.24f), teal);
            CreateWorldLabel("Opposition Strategy Label", "SEVA  /  SAWAL  /  SUDHAAR",
                new Vector3(19f, 3.5f, 24.1f), Vector3.zero, yellow, strategyStage.transform.parent, 0.023f);
            MissionObjective strategy = AddObjective(
                strategyStage, "opposition-term-approach", "Opposition ka five-year approach chunein",
                "Independent Democratic Practice Council",
                "Issue-by-issue opposition slower headlines degi, lekin service, alliance trust and policy correction ko balance karegi. Constant confrontation attention de sakta hai, par institutions aur evidence record weak hoga.",
                0, 0, 0, false);
            strategy.ConfigurePoliticalReward(3, 8, -2);
            strategy.ConfigureOppositionTermReward(12, 14, 12);
            strategy.ConfigureDecision(
                "opposition-term-approach",
                "PAANCH SAAL KA RAASTA",
                "Har bill aur crisis par evidence ke hisaab se support, amend ya oppose karein, ya har waqt confrontation chunein?",
                "ISSUE-BY-ISSUE\nService +12 / Alliance +14",
                "CONSTANT CONFRONTATION\nAttention + Power",
                "Confrontation se rallies badi hui, lekin joint relief work, source corrections and regional partner trust ko nuksan hua.",
                0, 0, -10, -8, 8, 4, 15,
                riskyOppositionService: 22,
                riskyNationalAllianceRenewal: -8,
                riskyNationalPolicyCorrection: -6);
            objectives.Add(strategy);
            labels.Add("Issue-by-issue opposition ya constant confrontation mein decision lein");

            GameObject shanti = CreatePerson(
                "Shanti Public Learning Network Lead", new Vector3(35f, 0f, 15f), shantiDress, darkStone, skin, hair, true);
            MissionObjective learning = AddObjective(
                shanti, "public-learning-network", "Shanti ke saath public learning network badhayein",
                "Shanti",
                "Community classes mein English help, civic forms, digital safety, scholarship navigation and women-led learning circles chal rahe hain. Party membership compulsory nahi hai.",
                0, 0, 0, false);
            learning.ConfigurePoliticalReward(0, 6, 1);
            learning.ConfigureOppositionTermReward(8, 8, 8);
            objectives.Add(learning);
            labels.Add("Shanti ke learning circles, civic forms aur scholarship network badhayein");

            GameObject samrat = CreatePerson(
                "Constable Samrat Institutional Neutrality Liaison", new Vector3(38f, 0f, -4f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective neutrality = AddObjective(
                samrat, "institutional-neutrality", "Samrat ke saath lawful neutrality protocol verify karein",
                "Constable Samrat",
                "Missing-person help, crowd safety, evidence handover and emergency referrals neutral protocol par chalenge. Samrat campaign stage ya party decision mein shamil nahi hoga.",
                0, 0, 0, false, 1);
            neutrality.ConfigurePoliticalReward(0, 0, -2);
            neutrality.ConfigureOppositionTermReward(6, 6, 8);
            objectives.Add(neutrality);
            labels.Add("Neutral safety, evidence handover aur emergency referral protocol verify karein");

            GameObject partnerLead = CreatePerson(
                "Regional Partner Autonomy Convenor", new Vector3(34f, 0f, -20f), volunteerDress, trousers, skin, hair, false);
            CreateChapterNineCrowd("Regional Partner Delegates", new Vector3(34f, 0f, -25f), 12,
                volunteerDress, shirt, yellow, darkStone, skin, hair);
            MissionObjective partner = AddObjective(
                partnerLead, "regional-partner-autonomy", "Regional partners ka autonomy charter renew karein",
                "Regional Partner Convenor",
                "Local leadership, language, candidate consent, finance disclosure and opt-out clauses written charter mein protected hain. National office har decision impose nahi karega.",
                0, 0, 0, false);
            partner.ConfigurePoliticalReward(0, 8, 1);
            partner.ConfigureOppositionTermReward(6, 14, 6);
            objectives.Add(partner);
            labels.Add("Regional language, leadership, consent aur opt-out charter renew karein");

            GameObject reportLead = CreatePerson(
                "Five Year Public Report Editor", new Vector3(13f, 0f, -22f), shirt, darkStone, skin, hair, false);
            CreateChapterNineCrowd("Five Year Public Review Meeting", new Vector3(13f, 0f, -27f), 12,
                shirt, volunteerDress, teal, darkStone, skin, hair);
            MissionObjective report = AddObjective(
                reportLead, "five-year-public-report", "Five-year public report release karein",
                "Independent Public Report Editor",
                "Promises, attendance, service cases, policy corrections, donations, complaints and failed experiments ek public report mein documented hain. Mistakes hide nahi ki gayi.",
                0, 0, 0, false, 2);
            report.ConfigurePoliticalReward(0, 0, -2);
            report.ConfigureOppositionTermReward(8, 8, 10);
            objectives.Add(report);
            labels.Add("Promises, attendance, service, finance, complaints aur mistakes publish karein");

            GameObject reviewWall = new GameObject("National Comeback Review Wall");
            reviewWall.transform.position = new Vector3(0f, 2.7f, -10f);
            CreatePrimitiveChild("Comeback Review Screen", PrimitiveType.Cube, reviewWall.transform,
                Vector3.zero, new Vector3(13.2f, 5.4f, 0.32f), darkStone);
            for (int index = 0; index < 10; index++)
            {
                CreatePrimitiveChild($"Five Year Record Bar {index + 1}", PrimitiveType.Cube, reviewWall.transform,
                    new Vector3(-5f + index * 1.12f, -1.65f + (index % 5) * 0.34f, -0.20f),
                    new Vector3(0.72f, 1.15f + (index % 5) * 0.34f, 0.07f), index < 5 ? teal : index < 9 ? yellow : white);
            }
            CreateWorldLabel("Comeback Review Label", "FIVE-YEAR OPPOSITION REVIEW",
                new Vector3(0f, 5.55f, -10.22f), Vector3.zero, yellow, reviewWall.transform.parent, 0.024f);
            MissionObjective review = AddObjective(
                reviewWall, "national-comeback-review", "Independent comeback review dekhein",
                "Independent National Democratic Review",
                "Service record, renewed alliances, policy corrections, previous vote, readiness and pressure se second national campaign eligibility compute hogi.",
                0, 0, 0, false);
            review.ConfigureNationalComeback();
            objectives.Add(review);
            labels.Add("Independent five-year review aur second-campaign eligibility dekhein");

            mission.Configure(
                "Haar Ke Baad Himmat",
                objectives,
                labels,
                "CHAPTER 20 COMPLETE",
                "Five saal ki responsible opposition, public service aur policy correction ke baad second national campaign approved hai. Haar ant nahi thi; public record agla raasta bana.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "SEVA RECORD LIVE", "OPPOSITION MODEL LOCKED", "ALLIANCE RENEWED" },
                new List<string>
                {
                    "Result acceptance, council accountability and public service record active hain. Policy alternatives aur relief next hain.",
                    "Evidence watchdog aur five-year approach complete hain. Shanti, Samrat and regional partners next hain.",
                    "Public learning, neutral institutions and regional autonomy verified hain. Five-year report review unlock karega."
                });
            mission.ConfigureChapter(20, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 20 / HAAR KE BAAD HIMMAT",
                "Pehla national election haarne ke baad Azad paanch saal tak responsible opposition, seva aur policy alternatives ka public record banata hai.");
        }

        private static void CreateChapterTwentyEnvironment(
            Transform root, Material sand, Material stone, Material darkStone, Material teal,
            Material yellow, Material white, Material foliage, Material trunk)
        {
            CreateBox("Responsible Opposition Campus Ground", new Vector3(0f, -0.32f, 0f), new Vector3(108f, 0.64f, 78f), sand, root);
            CreateBox("Public Record Avenue", new Vector3(0f, 0.02f, -5f), new Vector3(12f, 0.08f, 70f), stone, root);
            CreateBox("Five Year Service Crossway", new Vector3(0f, 0.03f, 10f), new Vector3(92f, 0.08f, 7.5f), stone, root);

            CreateBox("Responsible Opposition Review Hall", new Vector3(0f, 5.2f, 33f), new Vector3(46f, 10.4f, 10f), darkStone, root);
            CreateBox("Five Year Public Record Display", new Vector3(0f, 5f, 27.82f), new Vector3(28f, 6.6f, 0.35f), teal, root);
            for (int index = 0; index < 20; index++)
            {
                int column = index % 10;
                int row = index / 10;
                CreateBox($"Public Record Tile {index + 1}",
                    new Vector3(-11.7f + column * 2.6f, 3.6f + row * 2.05f, 27.58f),
                    new Vector3(1.55f, 1.1f, 0.10f), index % 3 == 0 ? yellow : index % 3 == 1 ? white : teal, root);
            }
            CreateWorldLabel("Review Hall Sign", "FIVE YEARS  /  SEVA + SAWAL + SUDHAAR",
                new Vector3(0f, 8.65f, 27.54f), Vector3.zero, yellow, root, 0.026f);

            CreateBox("Council Record Pavilion", new Vector3(-41f, 3.2f, -13f), new Vector3(18f, 6.4f, 21f), teal, root);
            CreateWorldLabel("Council Record Sign", "COUNCIL RECORD",
                new Vector3(-31.88f, 4.25f, -13f), new Vector3(0f, -90f, 0f), yellow, root, 0.022f);
            CreateBox("Public Service Pavilion", new Vector3(-41f, 3.1f, 16f), new Vector3(18f, 6.2f, 20f), white, root);
            CreateWorldLabel("Public Service Sign", "PUBLIC SERVICE NETWORK",
                new Vector3(-31.88f, 4.2f, 16f), new Vector3(0f, -90f, 0f), teal, root, 0.020f);
            CreateBox("Shadow Policy Pavilion", new Vector3(41f, 3.1f, 16f), new Vector3(18f, 6.2f, 20f), teal, root);
            CreateWorldLabel("Shadow Policy Sign", "SHADOW POLICY + LEARNING",
                new Vector3(31.88f, 4.2f, 16f), new Vector3(0f, 90f, 0f), yellow, root, 0.019f);
            CreateBox("Alliance Renewal Pavilion", new Vector3(41f, 3.2f, -13f), new Vector3(18f, 6.4f, 21f), white, root);
            CreateWorldLabel("Alliance Renewal Sign", "ALLIANCE RENEWAL",
                new Vector3(31.88f, 4.25f, -13f), new Vector3(0f, 90f, 0f), teal, root, 0.021f);

            CreateBox("Public Report Plaza", new Vector3(10f, -0.07f, -25f), new Vector3(30f, 0.18f, 13f), white, root);
            CreateBox("Public Report Stage", new Vector3(10f, 0.65f, -34f), new Vector3(20f, 1.3f, 6f), darkStone, root);
            CreateBox("Comeback Review Backdrop", new Vector3(10f, 3.4f, -36.5f), new Vector3(20f, 5.5f, 0.35f), teal, root);
            CreateWorldLabel("Comeback Review Sign", "HAAR KE BAAD HIMMAT  /  PUBLIC RECORD",
                new Vector3(10f, 5.35f, -36.28f), Vector3.zero, yellow, root, 0.021f);

            for (int index = 0; index < 14; index++)
            {
                float flagX = -52f + index * 8f;
                if (Mathf.Abs(flagX) < 5f)
                {
                    continue;
                }
                CreateExpansionFlag($"Public Service Route Flag {index + 1}", new Vector3(flagX, 0f, -36f),
                    darkStone, index % 3 == 0 ? yellow : index % 3 == 1 ? teal : white, root);
            }
            for (int index = 0; index < 12; index++)
            {
                CreateStreetLamp($"Public Record Avenue Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -7.5f : 7.5f, 0f, -29f + index * 5.8f), darkStone, yellow, root);
            }

            CreateTree("Opposition Campus Neem North West", new Vector3(-51f, 0f, 35f), foliage, trunk, root);
            CreateTree("Opposition Campus Neem North East", new Vector3(51f, 0f, 35f), foliage, trunk, root);
            CreateTree("Opposition Campus Neem South West", new Vector3(-51f, 0f, -33f), foliage, trunk, root);
            CreateTree("Opposition Campus Neem South East", new Vector3(51f, 0f, -33f), foliage, trunk, root);
        }

        private static void CreateChapterTwentyLighting()
        {
            GameObject lightObject = new GameObject("Comeback Golden Hour Light");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.88f, 0.70f);
            sunlight.intensity = 1.12f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.50f;
            lightObject.transform.rotation = Quaternion.Euler(40f, -34f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.46f, 0.67f, 0.86f);
            RenderSettings.ambientEquatorColor = new Color(0.79f, 0.61f, 0.43f);
            RenderSettings.ambientGroundColor = new Color(0.24f, 0.24f, 0.21f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.77f, 0.80f, 0.83f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 92f;
            RenderSettings.fogEndDistance = 240f;
        }
    }
}

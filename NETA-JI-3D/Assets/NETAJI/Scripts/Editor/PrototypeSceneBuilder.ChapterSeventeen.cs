using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterSeventeenScene(
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
            GameObject environment = new GameObject("Fictional Five Year State Reform Campus");
            CreateChapterSeventeenEnvironment(
                environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 17 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                17, 100, 0, 100, 100, 65, 326, 89, 67, 85, 59, true,
                65, 87, 5, 85, true, 88, 93, 99, 91, true, 58, 92, 69, 58, true,
                78, 80, 90, 30, 78, true, 78, 90, 92, 89, true,
                80, 89, 98, 82, 6, true, 84, 86, 88, 87, true,
                78, 100, 90, 56, 27, true, 92, 100, 80, 88, true,
                93, 87, 92, 85, 87, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson(
                "Azad Chief Minister Full Term", new Vector3(0f, 0f, -29f), white, white, skin, hair, true);
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
            gameCamera.farClipPlane = 245f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 4.0f, -34f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterSeventeenMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterSeventeenLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterSeventeenScenePath);
        }

        private static void ConfigureChapterSeventeenMission(
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

            GameObject baselineLead = CreatePerson(
                "Independent State Outcome Statistician", new Vector3(0f, 0f, -23f), white, darkStone, skin, hair, false);
            MissionObjective baseline = AddObjective(
                baselineLead,
                "state-outcome-baseline",
                "Five-year outcome baseline verify karein",
                "Independent Outcome Statistician",
                "Health, learning, safety and livelihood baselines, sample limits and correction history frozen hain. Government apni convenience se denominator change nahi kar sakti.",
                0, 0, 0, false, 1);
            baseline.ConfigurePoliticalReward(0, 0, -8);
            baseline.ConfigureStateTermReward(8, 8, 8, 8);
            objectives.Add(baseline);
            labels.Add("Independent baseline, sample limits and correction history verify karein");

            GameObject primaryHealth = new GameObject("Primary Health Outcome Station");
            primaryHealth.transform.position = new Vector3(-24f, 1.2f, -15f);
            CreatePrimitiveChild("Primary Health Counter", PrimitiveType.Cube, primaryHealth.transform, Vector3.zero, new Vector3(7.2f, 1.4f, 2.5f), white);
            for (int index = 0; index < 8; index++)
            {
                CreatePrimitiveChild(
                    $"Clinic Coverage Tile {index + 1}", PrimitiveType.Cube, primaryHealth.transform,
                    new Vector3(-2.7f + (index % 4) * 1.8f, 1.02f + (index / 4) * 0.72f, 0f),
                    new Vector3(1.1f, 0.48f, 0.48f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective health = AddObjective(
                primaryHealth,
                "primary-health-outcomes",
                "Primary health outcome plan deliver karein",
                "State Primary Health Mission",
                "Fictional clinics mein essential medicine availability, doctor support, tele-consult referral and public stock audit measured hain. Footfall ko treatment outcome nahi maana gaya.",
                0, 0, 0, false);
            health.ConfigurePoliticalReward(0, 6, 2);
            health.ConfigureStateTermReward(18, 4, 4, 2);
            objectives.Add(health);
            labels.Add("Clinic access, medicine availability and referral outcomes deliver karein");

            GameObject maternalLead = CreatePerson(
                "Maternal Child Care Network Lead", new Vector3(-34f, 0f, -3f), shantiDress, darkStone, skin, hair, false);
            AddScarf(maternalLead.transform, shantiDress);
            MissionObjective maternal = AddObjective(
                maternalLead,
                "maternal-child-network",
                "Maternal-child referral network run karein",
                "Maternal And Child Care Lead",
                "High-risk pregnancy referral, newborn follow-up, nutrition support and transport delay review active hai. Private medical details aggregate report se excluded hain.",
                0, 0, 0, false);
            maternal.ConfigurePoliticalReward(0, 6, 2);
            maternal.ConfigureStateTermReward(16, 5, 10, 0);
            objectives.Add(maternal);
            labels.Add("High-risk maternal referral, newborn follow-up and nutrition network run karein");

            GameObject learningBoard = new GameObject("Learning Recovery Observatory");
            learningBoard.transform.position = new Vector3(-29f, 2.1f, 13f);
            CreatePrimitiveChild("Learning Observatory Screen", PrimitiveType.Cube, learningBoard.transform, Vector3.zero, new Vector3(8.6f, 4.2f, 0.28f), darkStone);
            for (int index = 0; index < 10; index++)
            {
                float barHeight = 0.45f + (index % 5) * 0.22f;
                CreatePrimitiveChild(
                    $"Learning Trend Bar {index + 1}", PrimitiveType.Cube, learningBoard.transform,
                    new Vector3(-3.2f + (index % 5) * 1.6f, -0.9f + (index / 5) * 1.7f + barHeight * 0.5f, -0.18f),
                    new Vector3(0.78f, barHeight, 0.06f), index % 2 == 0 ? yellow : teal);
            }
            MissionObjective learning = AddObjective(
                learningBoard,
                "learning-recovery",
                "Learning recovery mission scale karein",
                "Independent Learning Council",
                "Reading, maths, attendance, teacher coaching and accessible classroom outcomes cohort-wise measured hain. Exam pass percentage ke saath independent sample check bhi public hai.",
                0, 0, 0, false);
            learning.ConfigurePoliticalReward(0, 8, 1);
            learning.ConfigureStateTermReward(4, 20, 3, 4);
            objectives.Add(learning);
            labels.Add("Reading, maths, attendance and teacher-support outcomes scale karein");

            GameObject livelihoodLead = CreatePerson(
                "Local Livelihood Mission Lead", new Vector3(-18f, 0f, 23f), volunteerDress, darkStone, skin, hair, false);
            CreateChapterNineCrowd(
                "Skill And Local Enterprise Cohort", new Vector3(-18f, 0f, 26f), 10,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective livelihood = AddObjective(
                livelihoodLead,
                "local-livelihood-mission",
                "Local livelihood mission deliver karein",
                "Livelihood Mission Lead",
                "Apprenticeship completion, small-enterprise survival, fair credit, rural producer access and women-led workgroups ka verified outcome report ready hai. Registration ko job nahi gina gaya.",
                0, 0, 0, false);
            livelihood.ConfigurePoliticalReward(0, 8, 2);
            livelihood.ConfigureStateTermReward(2, 8, 3, 20);
            objectives.Add(livelihood);
            labels.Add("Skills, enterprise survival, fair credit and producer-market outcomes deliver karein");

            GameObject samrat = CreatePerson(
                "Constable Samrat Safety Reform Liaison", new Vector3(1f, 0f, 23f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective safety = AddObjective(
                samrat,
                "state-safety-outcomes",
                "Safety and case-follow-up reform karein",
                "Constable Samrat",
                "Emergency response, missing-person follow-up, survivor support, evidence chain and complaint appeal measured hain. Arrest count ko safety outcome ka shortcut nahi banaya gaya.",
                0, 0, 0, false);
            safety.ConfigurePoliticalReward(0, 6, 2);
            safety.ConfigureStateTermReward(2, 4, 20, 2);
            objectives.Add(safety);
            labels.Add("Samrat ke saath response, evidence, survivor support and appeal outcomes review karein");

            GameObject strategyStage = new GameObject("Five Year Reform Strategy Stage");
            strategyStage.transform.position = new Vector3(18f, 1.2f, 20f);
            CreatePrimitiveChild("Reform Strategy Platform", PrimitiveType.Cube, strategyStage.transform, Vector3.zero, new Vector3(8.4f, 1.0f, 4.8f), darkStone);
            CreatePrimitiveChild("Reform Strategy Backdrop", PrimitiveType.Cube, strategyStage.transform, new Vector3(0f, 2.0f, 2.1f), new Vector3(8.4f, 3.3f, 0.24f), teal);
            CreateWorldLabel(
                "Reform Strategy Label", "OUTCOMES BEFORE ANNOUNCEMENTS",
                new Vector3(18f, 3.45f, 22.0f), Vector3.zero, yellow, strategyStage.transform.parent, 0.021f);
            MissionObjective strategy = AddObjective(
                strategyStage,
                "state-reform-approach",
                "Five-year reform strategy chunein",
                "Independent State Reform Council",
                "Measured phased reforms Rs 10 and more coordination lenge, lekin health, learning, safety and livelihood capacity balanced rahegi.",
                0, -10, 0, false);
            strategy.ConfigurePoliticalReward(3, 10, 4);
            strategy.ConfigureStateTermReward(10, 12, 12, 10);
            strategy.ConfigureDecision(
                "state-reform-approach",
                "FIVE-YEAR REFORM MODEL",
                "Phased reforms balanced systems build karte hain. Simultaneous mega announcements fast coverage and jobs claims dete hain, par learning and safety capacity stretch ho sakti hai.",
                "MEASURED PHASED REFORMS\nLearning +12 / Safety +12",
                "SIMULTANEOUS MEGA ANNOUNCEMENTS\nHealth +20 / Jobs +22",
                "Mega announcements se clinics and projects fast expand hue, lekin trained staff, learning support and safety follow-up capacity peeche reh gayi. Final audit minimum outcomes check karega.",
                0, 0, -10, -8, 9, 4, 15,
                riskyStateHealthOutcome: 20,
                riskyStateLearningOutcome: -6,
                riskyStateSafetyOutcome: -10,
                riskyStateLivelihoodOutcome: 22);
            objectives.Add(strategy);
            labels.Add("Measured phased reforms ya simultaneous mega announcements mein decision lein");

            GameObject ruralBoard = new GameObject("Rural Services And Work Board");
            ruralBoard.transform.position = new Vector3(31f, 2.0f, 9f);
            CreatePrimitiveChild("Rural Outcome Screen", PrimitiveType.Cube, ruralBoard.transform, Vector3.zero, new Vector3(8.0f, 4.0f, 0.28f), darkStone);
            for (int index = 0; index < 12; index++)
            {
                CreatePrimitiveChild(
                    $"Rural Service Cluster {index + 1}", PrimitiveType.Cube, ruralBoard.transform,
                    new Vector3(-3f + (index % 6) * 1.2f, 0.78f - (index / 6) * 1.5f, -0.18f),
                    new Vector3(0.75f, 0.62f, 0.06f), index % 3 == 0 ? yellow : teal);
            }
            MissionObjective rural = AddObjective(
                ruralBoard,
                "rural-services-work",
                "Rural services-work compact deliver karein",
                "Rural Outcomes Commissioner",
                "Water reliability, clinic referral, school transport, local maintenance work and producer access fictional rural clusters mein verified completion and maintenance records se measured hain.",
                0, 0, 0, false);
            rural.ConfigurePoliticalReward(0, 7, 2);
            rural.ConfigureStateTermReward(10, 8, 6, 14);
            objectives.Add(rural);
            labels.Add("Rural water, referral, school transport, maintenance and producer outcomes deliver karein");

            GameObject urbanBoard = new GameObject("Urban Services Outcome Board");
            urbanBoard.transform.position = new Vector3(33f, 2.0f, -6f);
            CreatePrimitiveChild("Urban Outcome Screen", PrimitiveType.Cube, urbanBoard.transform, Vector3.zero, new Vector3(8.0f, 4.0f, 0.28f), darkStone);
            for (int index = 0; index < 8; index++)
            {
                CreatePrimitiveChild(
                    $"Urban Ward Trend {index + 1}", PrimitiveType.Cube, urbanBoard.transform,
                    new Vector3(-2.8f + (index % 4) * 1.85f, 0.72f - (index / 4) * 1.45f, -0.18f),
                    new Vector3(1.2f, 0.58f, 0.06f), index % 2 == 0 ? teal : white);
            }
            MissionObjective urban = AddObjective(
                urbanBoard,
                "urban-service-outcomes",
                "Urban service outcomes deliver karein",
                "Urban Services Commissioner",
                "Air alerts, waste collection, public transport reliability, safe streets, clinic access and vendor zones complaint-adjusted data ke saath public hain.",
                0, 0, 0, false);
            urban.ConfigurePoliticalReward(0, 7, 2);
            urban.ConfigureStateTermReward(12, 5, 8, 10);
            objectives.Add(urban);
            labels.Add("Transport, waste, air, safe-street, clinic and vendor-zone outcomes deliver karein");

            GameObject integrityDesk = new GameObject("State Integrity Enforcement Desk");
            integrityDesk.transform.position = new Vector3(25f, 1.05f, -19f);
            CreatePrimitiveChild("Integrity Audit Counter", PrimitiveType.Cube, integrityDesk.transform, Vector3.zero, new Vector3(6.8f, 1.25f, 2.3f), teal);
            CreatePrimitiveChild("Complaint Register", PrimitiveType.Cube, integrityDesk.transform, new Vector3(-1.5f, 0.78f, 0f), new Vector3(1.15f, 0.15f, 0.8f), white);
            CreatePrimitiveChild("Recovery Register", PrimitiveType.Cube, integrityDesk.transform, new Vector3(0f, 0.78f, 0f), new Vector3(1.15f, 0.15f, 0.8f), yellow);
            CreatePrimitiveChild("Appeal Register", PrimitiveType.Cube, integrityDesk.transform, new Vector3(1.5f, 0.78f, 0f), new Vector3(1.15f, 0.15f, 0.8f), white);
            MissionObjective integrity = AddObjective(
                integrityDesk,
                "state-integrity-enforcement",
                "Integrity and recovery audit karein",
                "Independent State Integrity Panel",
                "Complaint triage, conflict recusals, procurement recoveries, whistleblower safety and appeal outcomes sample-checked hain. Political loyalty investigation priority decide nahi karti.",
                0, 0, 0, false, 3);
            integrity.ConfigurePoliticalReward(0, 0, -4);
            integrity.ConfigureStateTermReward(5, 5, 10, 5);
            objectives.Add(integrity);
            labels.Add("Complaints, recusals, recoveries, whistleblower safety and appeals audit karein");

            GameObject reportLead = CreatePerson(
                "Five Year Public Report Moderator", new Vector3(7f, 0f, -21f), shirt, trousers, skin, hair, false);
            CreateChapterNineCrowd(
                "Five Year Public Accountability Hearing", new Vector3(7f, 0f, -24f), 12,
                shirt, volunteerDress, yellow, darkStone, skin, hair);
            MissionObjective report = AddObjective(
                reportLead,
                "five-year-public-report",
                "Five-year public report defend karein",
                "Independent Public Report Moderator",
                "Targets, misses, costs, corrections and unresolved gaps residents, opposition, press and frontline workers ke equal questions ke saamne published hain.",
                0, 0, 0, false);
            report.ConfigurePoliticalReward(0, 5, 1);
            report.ConfigureStateTermReward(6, 8, 8, 10);
            objectives.Add(report);
            labels.Add("Targets, misses, costs and corrections ka equal-question public report defend karein");

            GameObject reviewWall = new GameObject("Independent Five Year State Review Wall");
            reviewWall.transform.position = new Vector3(0f, 2.55f, -10f);
            CreatePrimitiveChild("Five Year Review Wall", PrimitiveType.Cube, reviewWall.transform, Vector3.zero, new Vector3(11.2f, 5.1f, 0.30f), darkStone);
            Material[] barColors = { teal, yellow, white, teal };
            float[] barHeights = { 2.4f, 2.0f, 2.3f, 1.9f };
            for (int index = 0; index < 4; index++)
            {
                CreatePrimitiveChild(
                    $"Final Outcome Bar {index + 1}", PrimitiveType.Cube, reviewWall.transform,
                    new Vector3(-3.6f + index * 2.4f, -0.75f + barHeights[index] * 0.5f, -0.19f),
                    new Vector3(1.15f, barHeights[index], 0.07f), barColors[index]);
            }
            CreateWorldLabel(
                "Five Year Review Label", "INDEPENDENT FIVE-YEAR STATE REVIEW",
                new Vector3(0f, 5.28f, -10.2f), Vector3.zero, yellow, reviewWall.transform.parent, 0.024f);
            MissionObjective review = AddObjective(
                reviewWall,
                "state-term-review",
                "Independent five-year review dekhein",
                "Independent State Outcomes Panel",
                "Health, learning, safety, livelihood, 100-day governance record, integrity and pressure ka computed full-term public audit ready hai.",
                0, 0, 0, false);
            review.ConfigureStateTermReview();
            objectives.Add(review);
            labels.Add("Independent panel par four outcomes and full-term public audit dekhein");

            mission.Configure(
                "Badlav Ke Paanch Saal",
                objectives,
                labels,
                "CHAPTER 17 COMPLETE",
                "Five-year public audit pass hua. Pradesh model ko doosre fictional regions ne study karna shuru kiya.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "HEALTH BASELINE MOVING", "BALANCED REFORM ACTIVE", "RURAL-URBAN AUDIT READY" },
                new List<string>
                {
                    "Baseline, primary health and maternal-child systems measured hain. Learning and livelihood next hain.",
                    "Learning, livelihood, safety and reform strategy active hain. Rural-urban execution next hai.",
                    "Rural, urban and integrity outcomes audited. Five-year public report final review unlock karega."
                });
            mission.ConfigureChapter(17, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 17 / BADLAV KE PAANCH SAAL",
                "Announcement ek din ka hota hai. Health, learning, safety aur rozgaar ka badlav paanch saal maangta hai.");
        }

        private static void CreateChapterSeventeenEnvironment(
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
            CreateBox("Five Year Outcomes Campus Ground", new Vector3(0f, -0.32f, 0f), new Vector3(104f, 0.64f, 74f), sand, root);
            CreateBox("Public Outcomes Avenue", new Vector3(0f, 0.02f, -4f), new Vector3(11f, 0.08f, 68f), stone, root);
            CreateBox("Four Outcomes Crossway", new Vector3(0f, 0.03f, 11f), new Vector3(88f, 0.08f, 7f), stone, root);

            CreateBox("State Outcomes Observatory", new Vector3(0f, 4.8f, 31f), new Vector3(42f, 9.6f, 9f), darkStone, root);
            CreateBox("Observatory Public Screen", new Vector3(0f, 4.5f, 26.35f), new Vector3(24f, 5.8f, 0.35f), teal, root);
            for (int index = 0; index < 4; index++)
            {
                CreateBox(
                    $"Observatory Outcome Light {index + 1}", new Vector3(-8.1f + index * 5.4f, 4.5f, 26.12f),
                    new Vector3(3.5f, 2.7f + index * 0.35f, 0.12f), index % 2 == 0 ? yellow : white, root);
            }
            CreateWorldLabel(
                "Outcomes Observatory Sign", "FIVE-YEAR PUBLIC OUTCOMES OBSERVATORY",
                new Vector3(0f, 7.35f, 26.08f), Vector3.zero, yellow, root, 0.028f);

            CreateBox("Health Outcome Pavilion", new Vector3(-39f, 3.2f, -5f), new Vector3(17f, 6.4f, 21f), white, root);
            CreateWorldLabel("Health Pavilion Sign", "HEALTH OUTCOMES", new Vector3(-30.38f, 4.3f, -5f), new Vector3(0f, -90f, 0f), teal, root, 0.022f);

            CreateBox("Learning Outcome Pavilion", new Vector3(-32f, 3.1f, 21f), new Vector3(25f, 6.2f, 12f), teal, root);
            CreateWorldLabel("Learning Pavilion Sign", "LEARNING RECOVERY", new Vector3(-32f, 4.25f, 14.88f), Vector3.zero, yellow, root, 0.022f);

            CreateBox("Safety Outcome Pavilion", new Vector3(35f, 3.1f, 19f), new Vector3(23f, 6.2f, 13f), white, root);
            CreateWorldLabel("Safety Pavilion Sign", "SAFETY  +  FOLLOW-UP", new Vector3(35f, 4.25f, 12.38f), Vector3.zero, teal, root, 0.021f);

            CreateBox("Livelihood Outcome Pavilion", new Vector3(39f, 3.2f, -7f), new Vector3(17f, 6.4f, 21f), teal, root);
            CreateWorldLabel("Livelihood Pavilion Sign", "LIVELIHOOD OUTCOMES", new Vector3(30.38f, 4.3f, -7f), new Vector3(0f, 90f, 0f), yellow, root, 0.021f);

            CreateBox("Public Term Review Plaza", new Vector3(6f, -0.07f, -22f), new Vector3(28f, 0.18f, 12f), white, root);

            for (int index = 0; index < 13; index++)
            {
                float flagX = -48f + index * 8f;
                if (Mathf.Abs(flagX) < 4f)
                {
                    continue;
                }
                CreateExpansionFlag(
                    $"Outcome Route Flag {index + 1}", new Vector3(flagX, 0f, -33f),
                    darkStone, index % 3 == 0 ? yellow : teal, root);
            }
            for (int index = 0; index < 12; index++)
            {
                CreateStreetLamp(
                    $"Outcomes Avenue Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -7f : 7f, 0f, -28f + index * 5.6f), darkStone, yellow, root);
            }

            CreateTree("Outcome Neem North West", new Vector3(-48f, 0f, 32f), foliage, trunk, root);
            CreateTree("Outcome Neem North East", new Vector3(48f, 0f, 32f), foliage, trunk, root);
            CreateTree("Outcome Neem South West", new Vector3(-48f, 0f, -30f), foliage, trunk, root);
            CreateTree("Outcome Neem South East", new Vector3(48f, 0f, -30f), foliage, trunk, root);
        }

        private static void CreateChapterSeventeenLighting()
        {
            GameObject lightObject = new GameObject("Five Year Review Sunset");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.88f, 0.68f);
            sunlight.intensity = 1.10f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.52f;
            lightObject.transform.rotation = Quaternion.Euler(39f, -41f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.45f, 0.66f, 0.84f);
            RenderSettings.ambientEquatorColor = new Color(0.76f, 0.57f, 0.38f);
            RenderSettings.ambientGroundColor = new Color(0.24f, 0.24f, 0.20f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.76f, 0.79f, 0.82f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 86f;
            RenderSettings.fogEndDistance = 230f;
        }
    }
}

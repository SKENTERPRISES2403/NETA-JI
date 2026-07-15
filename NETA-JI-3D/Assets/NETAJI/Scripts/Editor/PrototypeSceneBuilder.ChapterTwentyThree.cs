using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterTwentyThreeScene(
            Material sand, Material stone, Material darkStone, Material teal, Material yellow,
            Material white, Material shirt, Material trousers, Material skin, Material hair,
            Material shantiDress, Material volunteerDress, Material policeKhaki,
            Material foliage, Material trunk)
        {
            Scene chapterScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject environment = new GameObject("Fictional National Development Campus");
            CreateChapterTwentyThreeEnvironment(environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 23 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                23, 100, 0, 100,
                proof: 100, power: 85, team: 670, pressure: 78,
                support: 67, booth: 85, voteShare: 59, wardWin: true,
                delivery: 65, integrity: 87, budget: 5, governanceScore: 85, reviewPassed: true,
                assemblyReach: 88, coalitionUnity: 93, assemblyReadiness: 99, nominationScore: 91, assemblyNomination: true,
                constituencySupport: 58, campaignCompliance: 92, electionOperations: 69, assemblyVoteShare: 58, assemblyWin: true,
                legislativeEffectiveness: 78, constituencyService: 80, ethics: 90, mlaAllocation: 30, mlaPerformance: 78, mlaTermOnTrack: true,
                districtReach: 78, candidateQuality: 90, organizationDiscipline: 92, districtExpansionScore: 89, districtNetworkReady: true,
                stateCampaignReach: 80, candidateSlateIntegrity: 89, stateElectionOperations: 98, stateExpansionScore: 82, stateSeatsWon: 6, stateFootholdSecured: true,
                statePolicyCredibility: 84, stateCaucusUnity: 86, publicLeadership: 88, stateLeadershipScore: 87, stateLeaderSelected: true,
                statewideSupport: 78, statewideCampaignCompliance: 100, statewideElectionOperations: 90, statewideVoteShare: 56, stateAssemblySeatsWon: 27, chiefMinisterElected: true,
                chiefMinisterDelivery: 92, cabinetIntegrity: 100, stateFiscalDiscipline: 80, chiefMinisterGovernanceScore: 88, chiefMinisterHundredDayReviewPassed: true,
                stateHealthOutcome: 93, stateLearningOutcome: 87, stateSafetyOutcome: 92, stateLivelihoodOutcome: 85, stateTermScore: 87, stateTermReviewPassed: true,
                nationalOrganizationReach: 96, federalAllianceTrust: 100, nationalPolicyCredibility: 96, nationalReadinessScore: 90, nationalRegionsAligned: 18, nationalExpansionReady: true,
                nationalCampaignSupport: 84, nationalCampaignCompliance: 100, nationalElectionOperations: 92,
                firstNationalVoteShare: 49, firstNationalSeatsWon: 42, firstNationalElectionContested: true, firstNationalElectionWon: false,
                oppositionServiceRecord: 94, nationalAllianceRenewal: 92, nationalPolicyCorrection: 92, nationalComebackScore: 84, nationalComebackReady: true,
                secondNationalCampaignSupport: 76, secondNationalCampaignCompliance: 98, secondNationalElectionOperations: 76,
                secondNationalVoteShare: 55, secondNationalSeatsWon: 56, secondNationalElectionContested: true, primeMinisterElected: true,
                primeMinisterDelivery: 90, unionCabinetIntegrity: 96, nationalFiscalDiscipline: 84,
                institutionalTrust: 94, primeMinisterHundredDayScore: 88, primeMinisterHundredDayReviewPassed: true,
                nationalHealthIndex: 93, nationalLearningIndex: 95, nationalSafetyJusticeIndex: 92,
                nationalLivelihoodIndex: 90, nationalDevelopmentScore: 87, nationalDevelopmentReviewPassed: true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson(
                "Prime Minister Azad Development Review", new Vector3(0f, 0f, -38f), white, white, skin, hair, true);
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
            gameCamera.farClipPlane = 280f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 4.0f, -43f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterTwentyThreeMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterTwentyThreeLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterTwentyThreeScenePath);
        }

        private static void ConfigureChapterTwentyThreeMission(
            MissionController mission, Material shantiDress, Material volunteerDress,
            Material policeKhaki, Material teal, Material yellow, Material white,
            Material shirt, Material trousers, Material darkStone, Material skin, Material hair)
        {
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject baselineWall = new GameObject("National Baseline And Privacy Wall");
            baselineWall.transform.position = new Vector3(0f, 2.5f, -29f);
            CreatePrimitiveChild("Baseline Public Screen", PrimitiveType.Cube, baselineWall.transform,
                Vector3.zero, new Vector3(13f, 5f, 0.34f), darkStone);
            for (int index = 0; index < 12; index++)
            {
                CreatePrimitiveChild($"Baseline Tile {index + 1}", PrimitiveType.Cube, baselineWall.transform,
                    new Vector3(-4.8f + (index % 4) * 3.2f, 1.35f - (index / 4) * 1.25f, -0.20f),
                    new Vector3(2.25f, 0.76f, 0.07f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective baseline = AddObjective(
                baselineWall, "national-development-baseline", "Ten-year baseline aur privacy audit sign karein",
                "Independent National Statistics Panel",
                "Health, learning, safety, justice and livelihood baselines sampling limits, missing data, privacy rules and correction history ke saath public hain. Numbers ko slogan nahi banaya gaya.",
                0, 0, 0, false, 2);
            baseline.ConfigurePoliticalReward(0, 4, -2);
            baseline.ConfigureNationalDevelopmentReward(4, 6, 6, 4);
            objectives.Add(baseline);
            labels.Add("Baselines, missing data, privacy aur correction history audit karein");

            GameObject healthLead = CreatePerson(
                "Universal Health Access Lead", new Vector3(-39f, 0f, -18f), volunteerDress, trousers, skin, hair, false);
            CreateChapterNineCrowd("District Health Access Teams", new Vector3(-39f, 0f, -13f), 12,
                volunteerDress, white, teal, darkStone, skin, hair);
            MissionObjective health = AddObjective(
                healthLead, "national-health-reform", "Health access reform network review karein",
                "Universal Health Access Lead",
                "Primary care, maternal safety, essential medicines, emergency referral, rural staffing and negligence complaints district evidence se track hue. Coverage claim independent sample se match hai.",
                0, 0, 0, false);
            health.ConfigurePoliticalReward(0, 8, 0);
            health.ConfigureNationalDevelopmentReward(20, 4, 4, 4);
            objectives.Add(health);
            labels.Add("Primary care, maternal safety, medicines, referrals aur complaints review karein");

            GameObject learningLead = CreatePerson(
                "Learning And Literacy Mission Lead", new Vector3(-40f, 0f, 11f), shirt, darkStone, skin, hair, false);
            CreateChapterNineCrowd("Learning Fellows And Tutors", new Vector3(-40f, 0f, 16f), 12,
                shirt, volunteerDress, yellow, darkStone, skin, hair);
            MissionObjective learning = AddObjective(
                learningLead, "national-learning-reform", "95 learning-access target audit karein",
                "Independent Learning Mission Lead",
                "Foundational reading, adult learning, teacher support, libraries, scholarships and disability access ka fictional national index 95 tak pahucha. Enrollment ko learning outcome nahi maana gaya.",
                0, 0, 0, false);
            learning.ConfigurePoliticalReward(0, 8, 0);
            learning.ConfigureNationalDevelopmentReward(4, 22, 4, 4);
            objectives.Add(learning);
            labels.Add("Learning outcomes, adult literacy, teachers, libraries aur access audit karein");

            GameObject justicePanel = CreatePerson(
                "Safety And Justice Reform Auditor", new Vector3(-36f, 0f, 31f), shirt, trousers, skin, hair, false);
            MissionObjective justice = AddObjective(
                justicePanel, "national-safety-justice-reform", "Safety and justice safeguards verify karein",
                "Independent Safety And Justice Auditor",
                "Prevention, missing-person response, forensics, survivor support, legal aid, case tracking, police accountability and due process measure hue. Harshness ko justice ka shortcut nahi maana gaya.",
                0, 0, 0, false, 2);
            justice.ConfigurePoliticalReward(0, 8, 2);
            justice.ConfigureNationalDevelopmentReward(4, 4, 22, 4);
            objectives.Add(justice);
            labels.Add("Prevention, forensics, survivor support, legal aid aur due process verify karein");

            GameObject livelihoodLead = CreatePerson(
                "Livelihood And Enterprise Mission Lead", new Vector3(-12f, 0f, 38f), volunteerDress, trousers, skin, hair, false);
            CreateChapterNineCrowd("Workers And Small Enterprise Circle", new Vector3(-6f, 0f, 38f), 14,
                volunteerDress, shirt, teal, darkStone, skin, hair);
            MissionObjective livelihood = AddObjective(
                livelihoodLead, "national-livelihood-reform", "Livelihood outcome network review karein",
                "Livelihood And Enterprise Mission Lead",
                "Skills, apprenticeships, small-enterprise paperwork, payment reliability, worker safety, rural value chains and migrant access income-quality samples ke saath verified hain.",
                0, 0, 0, false);
            livelihood.ConfigurePoliticalReward(0, 8, 0);
            livelihood.ConfigureNationalDevelopmentReward(4, 4, 4, 20);
            objectives.Add(livelihood);
            labels.Add("Skills, apprenticeships, enterprises, payments aur worker safety review karein");

            GameObject strategyStage = new GameObject("National Development Strategy Stage");
            strategyStage.transform.position = new Vector3(19f, 1.1f, 34f);
            CreatePrimitiveChild("Development Strategy Platform", PrimitiveType.Cube, strategyStage.transform,
                Vector3.zero, new Vector3(10f, 1.1f, 5f), darkStone);
            CreatePrimitiveChild("Development Strategy Backdrop", PrimitiveType.Cube, strategyStage.transform,
                new Vector3(0f, 2f, 2.25f), new Vector3(10f, 3.5f, 0.26f), teal);
            CreateWorldLabel("Development Strategy Sign", "SABOOT  /  SUDHAAR  /  SABKA HAQ",
                new Vector3(19f, 3.55f, 36.38f), Vector3.zero, yellow, strategyStage.transform.parent, 0.021f);
            MissionObjective strategy = AddObjective(
                strategyStage, "national-development-approach", "Ten-year reform strategy chunein",
                "Independent Development Standards Council",
                "Evidence-led phased reform chaaron outcomes ko balance karega. Mega target sprint health and jobs headlines tez karega, lekin learning depth and justice safeguards ko weak karega.",
                0, 0, 0, false);
            strategy.ConfigurePoliticalReward(4, 10, 4);
            strategy.ConfigureNationalDevelopmentReward(10, 12, 10, 10);
            strategy.ConfigureDecision(
                "national-development-approach",
                "DAS SAAL KA BADLAV",
                "Evidence-led phased reform ya mega target sprint: final audit health ke saath learning, safety-justice and livelihood minimums bhi check karega.",
                "EVIDENCE-LED PHASED REFORM\nLearning +12 / Safety +10",
                "MEGA TARGET SPRINT\nHealth +17 / Jobs +20",
                "Large health and jobs targets fast hue, lekin learning depth, justice safeguards and independent corrections par pressure badha. Final audit four-outcome gate ke saath binding hai.",
                0, 0, -10, -10, 8, 8, 18);
            strategy.ConfigureNationalDevelopmentDecisionRewards(17, -7, -12, 20);
            objectives.Add(strategy);
            labels.Add("Evidence-led phased reform ya mega target sprint mein decision lein");

            GameObject financeConvenor = CreatePerson(
                "Federal Development Finance Convenor", new Vector3(42f, 0f, 27f), shirt, darkStone, skin, hair, false);
            CreateChapterNineCrowd("Fictional Federal Finance Delegates", new Vector3(42f, 0f, 32f), 12,
                volunteerDress, white, yellow, darkStone, skin, hair);
            MissionObjective finance = AddObjective(
                financeConvenor, "federal-development-finance", "Federal development finance compact renew karein",
                "Federal Development Finance Convenor",
                "Need formula, state flexibility, local matching, audit schedule, data consent, disaster reserve and opt-out process written hain. Credit-sharing bhi compact ka part hai.",
                0, 0, 0, false);
            finance.ConfigurePoliticalReward(0, 10, 1);
            finance.ConfigureNationalDevelopmentReward(8, 8, 8, 8);
            objectives.Add(finance);
            labels.Add("Need formula, state flexibility, audits, reserves aur opt-out compact renew karein");

            GameObject procurementWall = new GameObject("National Procurement Integrity Lab");
            procurementWall.transform.position = new Vector3(43f, 2.3f, 6f);
            CreatePrimitiveChild("Procurement Integrity Screen", PrimitiveType.Cube, procurementWall.transform,
                Vector3.zero, new Vector3(10f, 4.6f, 0.32f), darkStone);
            for (int index = 0; index < 10; index++)
            {
                CreatePrimitiveChild($"Procurement Audit File {index + 1}", PrimitiveType.Cube, procurementWall.transform,
                    new Vector3(-3.7f + (index % 5) * 1.85f, 1.1f - (index / 5) * 1.45f, -0.19f),
                    new Vector3(1.2f, 0.72f, 0.07f), index % 2 == 0 ? yellow : teal);
            }
            MissionObjective procurement = AddObjective(
                procurementWall, "national-procurement-audit", "Procurement and outcome audit publish karein",
                "Independent Procurement Integrity Panel",
                "Open tenders, beneficial ownership, delivery samples, blacklisting reasons, appeals, whistleblower referrals and recovered funds public record se match hain.",
                0, 0, 0, false, 4);
            procurement.ConfigurePoliticalReward(0, 4, -3);
            procurement.ConfigureNationalDevelopmentReward(8, 6, 10, 8);
            objectives.Add(procurement);
            labels.Add("Tenders, ownership, samples, appeals aur whistleblower referrals publish karein");

            GameObject resilienceLead = CreatePerson(
                "Climate Water And Energy Resilience Lead", new Vector3(43f, 0f, -17f), volunteerDress, trousers, skin, hair, false);
            MissionObjective resilience = AddObjective(
                resilienceLead, "national-resilience-mission", "Water, energy and climate resilience audit karein",
                "Climate Water And Energy Resilience Lead",
                "Clean energy reliability, heat plans, flood routes, river monitoring, farm water and disaster shelters ko health, schools, safety and local jobs outcomes se jointly review kiya gaya.",
                0, 0, 0, false);
            resilience.ConfigurePoliticalReward(0, 8, 0);
            resilience.ConfigureNationalDevelopmentReward(10, 8, 8, 10);
            objectives.Add(resilience);
            labels.Add("Energy, heat, flood, rivers, farm water aur shelters ka outcome audit karein");

            GameObject shanti = CreatePerson(
                "Shanti National Citizen Review Lead", new Vector3(24f, 0f, -29f), shantiDress, darkStone, skin, hair, true);
            CreateChapterNineCrowd("National Citizen Review Assembly", new Vector3(24f, 0f, -24f), 12,
                volunteerDress, white, teal, darkStone, skin, hair);
            MissionObjective citizens = AddObjective(
                shanti, "national-citizen-review", "Shanti ka citizen outcome review publish karein",
                "Shanti",
                "Household time, travel cost, forms, disability access, grievance closure and independent media questions se pata chala ki dashboard ke numbers zindagi mein mehsoos bhi ho rahe hain ya nahi.",
                0, 0, 0, false, 2);
            citizens.ConfigurePoliticalReward(0, 6, -2);
            citizens.ConfigureNationalDevelopmentReward(8, 10, 8, 6);
            objectives.Add(citizens);
            labels.Add("Household cost, forms, access, grievances aur media questions publish karein");

            GameObject samrat = CreatePerson(
                "Constable Samrat National Field Audit", new Vector3(0f, 0f, -16f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective fieldAudit = AddObjective(
                samrat, "national-field-safety-audit", "Samrat ka local field audit national panel ko dein",
                "Constable Samrat",
                "Response time, missing-person desk, evidence receipt, witness support and complaint escalation mein improvement hai, par weak districts bhi report mein hain. Sach chhupaya toh reform sirf poster ban jayega.",
                0, 0, 0, false);
            fieldAudit.ConfigurePoliticalReward(0, 6, -2);
            fieldAudit.ConfigureNationalDevelopmentReward(6, 4, 8, 4);
            objectives.Add(fieldAudit);
            labels.Add("Local response, evidence, witnesses, complaints aur weak districts report karein");

            GameObject reviewWall = new GameObject("Independent Ten Year Development Review Wall");
            reviewWall.transform.position = new Vector3(0f, 3f, 16f);
            CreatePrimitiveChild("Ten Year Development Review Screen", PrimitiveType.Cube, reviewWall.transform,
                Vector3.zero, new Vector3(15f, 6f, 0.38f), darkStone);
            Material[] meterMaterials = { teal, yellow, teal, yellow };
            for (int index = 0; index < 4; index++)
            {
                CreatePrimitiveChild($"Development Outcome Meter {index + 1}", PrimitiveType.Cube, reviewWall.transform,
                    new Vector3(-5.1f + index * 3.4f, -0.7f, -0.23f),
                    new Vector3(2.45f, 3.1f + index * 0.35f, 0.08f), meterMaterials[index]);
            }
            CreateWorldLabel("Development Review Sign", "HEALTH  /  LEARNING  /  JUSTICE  /  LIVELIHOOD",
                new Vector3(0f, 6.15f, 15.74f), Vector3.zero, yellow, reviewWall.transform.parent, 0.021f);
            MissionObjective review = AddObjective(
                reviewWall, "national-development-review", "Independent ten-year development review dekhein",
                "Independent National Development Review Panel",
                "Four outcomes, PM 100-day foundation, institutions, evidence quality and pressure se national development score compute hoga. Koi single headline baaki outcomes ko cancel nahi kar sakti.",
                0, 0, 0, false);
            review.ConfigureNationalDevelopmentReward(7, 7, 0, 8);
            review.ConfigureNationalDevelopmentReview();
            objectives.Add(review);
            labels.Add("Independent health, learning, justice and livelihood review dekhein");

            mission.Configure(
                "Desh Ka Badlav: Das Saal Ka Hisaab",
                objectives,
                labels,
                "CHAPTER 23 COMPLETE",
                "Ten-year national development review passed. Health, learning, safety-justice and livelihood outcomes next global-cooperation chapter ko unlock karte hain.");
            mission.ConfigureMilestones(
                new List<int> { 5, 8, 11 },
                new List<string> { "FOUR BASELINES VERIFIED", "FEDERAL DELIVERY LOCKED", "PUBLIC REVIEW READY" },
                new List<string>
                {
                    "Baselines and four development pillars verified hain. Long-term reform model next hai.",
                    "Strategy, federal finance and procurement integrity locked hain. Resilience and public review next hain.",
                    "Resilience, citizen review and field safety audit complete hain. Independent ten-year review unlocked."
                });
            mission.ConfigureChapter(23, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 23 / DESH KA BADLAV",
                "Das saal ke baad sawal speech ka nahi, evidence ka hai: sehat, padhai, nyay aur rozgaar aam zindagi mein kitna badla?");
        }

        private static void CreateChapterTwentyThreeEnvironment(
            Transform root, Material sand, Material stone, Material darkStone, Material teal,
            Material yellow, Material white, Material foliage, Material trunk)
        {
            CreateBox("National Development Campus Ground", new Vector3(0f, -0.32f, 0f), new Vector3(120f, 0.64f, 88f), sand, root);
            CreateBox("Evidence Avenue", new Vector3(0f, 0.02f, -4f), new Vector3(14f, 0.08f, 82f), stone, root);
            CreateBox("Four Outcome Crossway", new Vector3(0f, 0.03f, 17f), new Vector3(105f, 0.08f, 8f), stone, root);

            CreateBox("National Development Review Hall", new Vector3(0f, 5.6f, 39f), new Vector3(52f, 11.2f, 11f), darkStone, root);
            CreateBox("Review Hall Public Face", new Vector3(0f, 5.1f, 33.35f), new Vector3(45f, 9f, 0.42f), teal, root);
            for (int index = 0; index < 16; index++)
            {
                CreateBox($"Verified Outcome Light {index + 1}",
                    new Vector3(-18f + (index % 8) * 5.15f, 3.1f + (index / 8) * 3f, 33.05f),
                    new Vector3(2.6f, 1.55f, 0.10f), index % 2 == 0 ? yellow : white, root);
            }
            CreateWorldLabel("Development Hall Sign", "DAS SAAL  /  PUBLIC EVIDENCE REVIEW",
                new Vector3(0f, 9.6f, 32.86f), Vector3.zero, yellow, root, 0.028f);

            CreateBox("Health Access Hub", new Vector3(-50f, 3.3f, -18f), new Vector3(19f, 6.6f, 20f), white, root);
            CreateWorldLabel("Health Hub Sign", "HEALTH ACCESS",
                new Vector3(-40.35f, 4.4f, -18f), new Vector3(0f, -90f, 0f), teal, root, 0.022f);
            CreateBox("Learning Courtyard", new Vector3(-50f, 3.3f, 12f), new Vector3(19f, 6.6f, 20f), teal, root);
            CreateWorldLabel("Learning Hub Sign", "LEARNING 95",
                new Vector3(-40.35f, 4.4f, 12f), new Vector3(0f, -90f, 0f), yellow, root, 0.022f);
            CreateBox("Safety Justice Lab", new Vector3(50f, 3.3f, 21f), new Vector3(19f, 6.6f, 20f), white, root);
            CreateWorldLabel("Justice Lab Sign", "SAFETY + JUSTICE",
                new Vector3(40.35f, 4.4f, 21f), new Vector3(0f, 90f, 0f), teal, root, 0.021f);
            CreateBox("Livelihood Market Lab", new Vector3(50f, 3.3f, -12f), new Vector3(19f, 6.6f, 21f), teal, root);
            CreateWorldLabel("Livelihood Lab Sign", "SKILLS + LIVELIHOOD",
                new Vector3(40.35f, 4.4f, -12f), new Vector3(0f, 90f, 0f), yellow, root, 0.019f);

            CreateBox("Citizen Evidence Plaza", new Vector3(0f, -0.06f, 14f), new Vector3(34f, 0.18f, 21f), white, root);
            CreateBox("Decade Opening Stage", new Vector3(0f, 0.72f, -40f), new Vector3(26f, 1.44f, 7f), darkStone, root);
            CreateBox("Decade Stage Backdrop", new Vector3(0f, 3.7f, -43.1f), new Vector3(26f, 5.9f, 0.38f), teal, root);
            CreateWorldLabel("Decade Stage Sign", "BADLAV KA MATLAB  /  MEASURABLE LIFE",
                new Vector3(0f, 5.75f, -42.88f), Vector3.zero, yellow, root, 0.023f);

            for (int index = 0; index < 14; index++)
            {
                CreateStreetLamp($"Evidence Avenue Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -8.5f : 8.5f, 0f, -35f + index * 5.5f), darkStone, yellow, root);
            }
            for (int index = 0; index < 8; index++)
            {
                CreateExpansionFlag($"Development Outcome Flag {index + 1}",
                    new Vector3(-18f + index * 5.2f, 0f, -43f), darkStone,
                    index % 2 == 0 ? teal : yellow, root);
            }
            CreateTree("Development Neem North West", new Vector3(-57f, 0f, 40f), foliage, trunk, root);
            CreateTree("Development Neem North East", new Vector3(57f, 0f, 40f), foliage, trunk, root);
            CreateTree("Development Neem South West", new Vector3(-57f, 0f, -39f), foliage, trunk, root);
            CreateTree("Development Neem South East", new Vector3(57f, 0f, -39f), foliage, trunk, root);
        }

        private static void CreateChapterTwentyThreeLighting()
        {
            GameObject lightObject = new GameObject("Decade Review Clear Day Light");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.94f, 0.82f);
            sunlight.intensity = 1.12f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.50f;
            lightObject.transform.rotation = Quaternion.Euler(40f, -34f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.43f, 0.68f, 0.87f);
            RenderSettings.ambientEquatorColor = new Color(0.79f, 0.72f, 0.59f);
            RenderSettings.ambientGroundColor = new Color(0.22f, 0.28f, 0.24f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.74f, 0.83f, 0.87f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 105f;
            RenderSettings.fogEndDistance = 270f;
        }
    }
}

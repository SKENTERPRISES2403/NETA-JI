using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterTwentyTwoScene(
            Material sand, Material stone, Material darkStone, Material teal, Material yellow,
            Material white, Material shirt, Material trousers, Material skin, Material hair,
            Material shantiDress, Material volunteerDress, Material policeKhaki,
            Material foliage, Material trunk)
        {
            Scene chapterScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject environment = new GameObject("Fictional Union Governance Campus");
            CreateChapterTwentyTwoEnvironment(environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 22 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                22, 100, 0, 100,
                proof: 100, power: 81, team: 590, pressure: 80,
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
                institutionalTrust: 94, primeMinisterHundredDayScore: 88, primeMinisterHundredDayReviewPassed: true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson(
                "Prime Minister Azad", new Vector3(0f, 0f, -34f), white, white, skin, hair, true);
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
            gameCamera.farClipPlane = 260f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 4.0f, -39f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterTwentyTwoMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterTwentyTwoLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterTwentyTwoScenePath);
        }

        private static void ConfigureChapterTwentyTwoMission(
            MissionController mission, Material shantiDress, Material volunteerDress,
            Material policeKhaki, Material teal, Material yellow, Material white,
            Material shirt, Material trousers, Material darkStone, Material skin, Material hair)
        {
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject oathOfficer = CreatePerson(
                "Constitutional Transition Officer", new Vector3(0f, 0f, -27f), shirt, darkStone, skin, hair, false);
            MissionObjective oath = AddObjective(
                oathOfficer, "pm-oath-transition", "Oath aur transition ethics record sign karein",
                "Independent Constitutional Transition Officer",
                "Oath ke saath asset declaration, coalition agreement, record handover, caretaker decisions and constitutional limits public register mein signed hain.",
                0, 0, 0, false);
            oath.ConfigurePoliticalReward(0, 0, -3);
            oath.ConfigurePrimeMinisterGovernanceReward(4, 8, 4, 10);
            objectives.Add(oath);
            labels.Add("Oath, asset declaration, coalition agreement aur transition record sign karein");

            GameObject cabinetTable = new GameObject("Transparent Union Cabinet Table");
            cabinetTable.transform.position = new Vector3(-24f, 1.0f, -18f);
            CreatePrimitiveChild("Cabinet Round Table", PrimitiveType.Cylinder, cabinetTable.transform,
                Vector3.zero, new Vector3(4.8f, 0.45f, 4.8f), white);
            for (int index = 0; index < 8; index++)
            {
                float angle = index * Mathf.PI * 0.25f;
                CreatePrimitiveChild($"Cabinet Merit File {index + 1}", PrimitiveType.Cube, cabinetTable.transform,
                    new Vector3(Mathf.Sin(angle) * 3.2f, 0.65f, Mathf.Cos(angle) * 3.2f),
                    new Vector3(0.72f, 0.12f, 0.92f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective cabinet = AddObjective(
                cabinetTable, "union-cabinet-criteria", "Union Cabinet merit criteria publish karein",
                "Independent Cabinet Ethics Panel",
                "Skill, federal representation, conflict checks, attendance, disclosure and removal rules se fictional cabinet shortlist hui. Personal loyalty koi eligibility rule nahi hai.",
                0, 0, 0, false);
            cabinet.ConfigurePoliticalReward(0, 8, 0);
            cabinet.ConfigurePrimeMinisterGovernanceReward(4, 14, 4, 8);
            objectives.Add(cabinet);
            labels.Add("Merit, federal representation, conflicts aur removal rules publish karein");

            GameObject disclosureWall = new GameObject("Public Conflict Disclosure Wall");
            disclosureWall.transform.position = new Vector3(-42f, 2.2f, -3f);
            CreatePrimitiveChild("Disclosure Screen", PrimitiveType.Cube, disclosureWall.transform,
                Vector3.zero, new Vector3(9f, 4.4f, 0.30f), darkStone);
            for (int index = 0; index < 10; index++)
            {
                CreatePrimitiveChild($"Disclosure Card {index + 1}", PrimitiveType.Cube, disclosureWall.transform,
                    new Vector3(-3.3f + (index % 5) * 1.65f, 1.15f - (index / 5) * 1.55f, -0.18f),
                    new Vector3(1.08f, 0.72f, 0.06f), index % 3 == 0 ? yellow : teal);
            }
            MissionObjective disclosure = AddObjective(
                disclosureWall, "cabinet-conflict-disclosure", "Conflict aur procurement disclosures verify karein",
                "Independent Public Disclosure Auditor",
                "Ministers, advisers, major vendors, gifts, recusals and procurement interests searchable register mein hain. Missing entries ke liye correction deadline active hai.",
                0, 0, 0, false, 2);
            disclosure.ConfigurePoliticalReward(0, 0, -2);
            disclosure.ConfigurePrimeMinisterGovernanceReward(2, 12, 8, 8);
            objectives.Add(disclosure);
            labels.Add("Ministers, advisers, vendors, gifts aur recusals ka register verify karein");

            GameObject dashboard = new GameObject("National Hundred Day Public Dashboard");
            dashboard.transform.position = new Vector3(-39f, 2.7f, 19f);
            CreatePrimitiveChild("Hundred Day Dashboard Screen", PrimitiveType.Cube, dashboard.transform,
                Vector3.zero, new Vector3(12f, 5.4f, 0.34f), darkStone);
            for (int index = 0; index < 12; index++)
            {
                CreatePrimitiveChild($"National Milestone Tile {index + 1}", PrimitiveType.Cube, dashboard.transform,
                    new Vector3(-4.5f + (index % 4) * 3f, 1.55f - (index / 4) * 1.45f, -0.20f),
                    new Vector3(2.2f, 0.92f, 0.07f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective dashboardObjective = AddObjective(
                dashboard, "national-hundred-day-dashboard", "Public 100-day dashboard activate karein",
                "National Delivery Dashboard Auditor",
                "Har promise ka owner, baseline, budget, milestone, evidence link, delay reason and correction date visible hai. Green colour sirf verified completion par lagega.",
                0, 0, 0, false);
            dashboardObjective.ConfigurePoliticalReward(0, 4, 0);
            dashboardObjective.ConfigurePrimeMinisterGovernanceReward(10, 6, 10, 10);
            objectives.Add(dashboardObjective);
            labels.Add("Owners, baselines, budgets, evidence aur delay reasons wala dashboard activate karein");

            GameObject healthLead = CreatePerson(
                "National Health Mission Coordinator", new Vector3(-22f, 0f, 29f), volunteerDress, trousers, skin, hair, false);
            CreateChapterNineCrowd("Health Navigation Team", new Vector3(-22f, 0f, 34f), 10,
                volunteerDress, white, teal, darkStone, skin, hair);
            MissionObjective health = AddObjective(
                healthLead, "first-health-mission", "Health access mission launch karein",
                "National Health Mission Coordinator",
                "Emergency referral, maternal care, essential medicine stock, district helpline and negligence complaint tracking ko states ke consent ke saath launch kiya gaya.",
                0, 0, 0, false);
            health.ConfigurePoliticalReward(0, 6, 0);
            health.ConfigurePrimeMinisterGovernanceReward(12, 4, 8, 6);
            objectives.Add(health);
            labels.Add("Emergency, maternal care, medicines aur complaint tracking mission launch karein");

            GameObject learningLead = CreatePerson(
                "National Learning Mission Coordinator", new Vector3(0f, 0f, 31f), shirt, trousers, skin, hair, false);
            CreateChapterNineCrowd("Learning Access Fellows", new Vector3(0f, 0f, 36f), 10,
                shirt, volunteerDress, yellow, darkStone, skin, hair);
            MissionObjective learning = AddObjective(
                learningLead, "first-learning-mission", "Learning recovery mission approve karein",
                "National Learning Mission Coordinator",
                "Foundational learning, teacher support, scholarship forms, library hours, disability access and public progress samples ke liye consent-based state plan ready hai.",
                0, 0, 0, false);
            learning.ConfigurePoliticalReward(0, 6, 0);
            learning.ConfigurePrimeMinisterGovernanceReward(10, 4, 8, 6);
            objectives.Add(learning);
            labels.Add("Learning recovery, teachers, scholarships, libraries aur access plan approve karein");

            GameObject samrat = CreatePerson(
                "Constable Samrat Field Safety Review", new Vector3(23f, 0f, 29f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective safety = AddObjective(
                samrat, "neutral-citizen-safety-review", "Samrat ka neutral safety field note review karein",
                "Constable Samrat",
                "Missing-person response, evidence receipt, witness safety, emergency desk and complaint escalation ke field gaps bheje hain. Main constable hi hoon; party ya PM office ka agent nahi.",
                0, 0, 0, false);
            safety.ConfigurePoliticalReward(0, 6, -2);
            safety.ConfigurePrimeMinisterGovernanceReward(8, 6, 4, 12);
            objectives.Add(safety);
            labels.Add("Missing-person, evidence, witness safety aur complaint escalation note review karein");

            GameObject livelihoodLead = CreatePerson(
                "National Livelihood Mission Coordinator", new Vector3(41f, 0f, 18f), volunteerDress, trousers, skin, hair, false);
            MissionObjective livelihood = AddObjective(
                livelihoodLead, "first-livelihood-mission", "Livelihood and paperwork mission launch karein",
                "National Livelihood Mission Coordinator",
                "Apprenticeship matching, small-business paperwork, payment-delay tracking, migrant help and local skill desks ke measurable pilots states ke saath signed hain.",
                0, 0, 0, false);
            livelihood.ConfigurePoliticalReward(0, 6, 0);
            livelihood.ConfigurePrimeMinisterGovernanceReward(10, 4, 10, 6);
            objectives.Add(livelihood);
            labels.Add("Apprenticeship, small-business, payment aur migrant-help pilots launch karein");

            GameObject strategyStage = new GameObject("Prime Minister Governance Strategy Stage");
            strategyStage.transform.position = new Vector3(40f, 1.1f, -4f);
            CreatePrimitiveChild("Governance Strategy Platform", PrimitiveType.Cube, strategyStage.transform,
                Vector3.zero, new Vector3(10f, 1.1f, 5f), darkStone);
            CreatePrimitiveChild("Governance Strategy Backdrop", PrimitiveType.Cube, strategyStage.transform,
                new Vector3(0f, 2f, 2.25f), new Vector3(10f, 3.5f, 0.26f), teal);
            CreateWorldLabel("Governance Strategy Sign", "SAATH  /  SYSTEM  /  SEVA",
                new Vector3(40f, 3.55f, -1.62f), Vector3.zero, yellow, strategyStage.transform.parent, 0.023f);
            MissionObjective strategy = AddObjective(
                strategyStage, "pm-governance-approach", "First 100-day governance model chunein",
                "Independent Governance Standards Council",
                "Open federal cabinet delivery ko consultation, audit and institution strength ke saath balance karega. Central command blitz headlines tez karega, par cabinet, budget and institutions par pressure dalega.",
                0, 0, 0, false);
            strategy.ConfigurePoliticalReward(3, 8, 2);
            strategy.ConfigurePrimeMinisterGovernanceReward(10, 14, 10, 10);
            strategy.ConfigureDecision(
                "pm-governance-approach",
                "PM KE PEHLE 100 DIN",
                "Open federal cabinet ya central command blitz: review delivery ke saath cabinet, fiscal discipline and institutions ko independently score karega.",
                "OPEN FEDERAL CABINET\nIntegrity +14 / Institutions +10",
                "CENTRAL COMMAND BLITZ\nDelivery +16 / Power +8",
                "Headline orders se kuch delivery fast hui, lekin cabinet challenge, budget review and institutional confidence weak hue. Independent audit phir bhi binding hai.",
                0, 0, -8, -6, 8, 6, 16);
            strategy.ConfigurePrimeMinisterDecisionRewards(16, -14, -4, -12);
            objectives.Add(strategy);
            labels.Add("Open federal cabinet ya central command blitz mein decision lein");

            GameObject federalConvenor = CreatePerson(
                "Federal Council Convenor", new Vector3(24f, 0f, -20f), shirt, darkStone, skin, hair, false);
            CreateChapterNineCrowd("Fictional State Delegates", new Vector3(24f, 0f, -25f), 14,
                volunteerDress, shirt, teal, darkStone, skin, hair);
            MissionObjective federal = AddObjective(
                federalConvenor, "first-federal-council", "First federal council compact sign karein",
                "Federal Council Convenor",
                "Health, learning, safety and livelihood missions ke roles, finance, data consent, grievance path and opt-out clauses fictional state delegates ke saath written hain.",
                0, 0, 0, false);
            federal.ConfigurePoliticalReward(0, 8, 1);
            federal.ConfigurePrimeMinisterGovernanceReward(8, 8, 6, 8);
            objectives.Add(federal);
            labels.Add("State roles, finance, data consent, grievances aur opt-out compact sign karein");

            GameObject shanti = CreatePerson(
                "Shanti Citizen Access Review Lead", new Vector3(0f, 0f, -14f), shantiDress, darkStone, skin, hair, true);
            CreateChapterNineCrowd("Citizen And Opposition Review Circle", new Vector3(0f, 0f, -9f), 12,
                volunteerDress, white, yellow, darkStone, skin, hair);
            MissionObjective access = AddObjective(
                shanti, "citizen-opposition-review", "Citizen access aur opposition briefing complete karein",
                "Shanti",
                "Easy-read dashboard, local-language helpline, disability access, complaint sample and opposition briefing ready hain. Sarkar ko sirf applause nahi, uncomfortable questions bhi sunne honge.",
                0, 0, 0, false);
            access.ConfigurePoliticalReward(0, 4, -1);
            access.ConfigurePrimeMinisterGovernanceReward(6, 8, 6, 6);
            objectives.Add(access);
            labels.Add("Accessible citizen review, complaint sample aur opposition briefing complete karein");

            GameObject auditWall = new GameObject("Independent PM Hundred Day Review Wall");
            auditWall.transform.position = new Vector3(0f, 2.8f, 15f);
            CreatePrimitiveChild("PM Hundred Day Review Screen", PrimitiveType.Cube, auditWall.transform,
                Vector3.zero, new Vector3(14f, 5.6f, 0.36f), darkStone);
            string[] reviewLabels = { "DELIVERY", "CABINET", "FISCAL", "INSTITUTIONS" };
            for (int index = 0; index < reviewLabels.Length; index++)
            {
                CreatePrimitiveChild($"PM Review Meter {reviewLabels[index]}", PrimitiveType.Cube, auditWall.transform,
                    new Vector3(-4.8f + index * 3.2f, -0.7f, -0.22f),
                    new Vector3(2.35f, 2.9f + index * 0.25f, 0.08f), index % 2 == 0 ? teal : yellow);
            }
            CreateWorldLabel("PM Review Wall Sign", "INDEPENDENT 100-DAY PUBLIC REVIEW",
                new Vector3(0f, 5.7f, 14.76f), Vector3.zero, yellow, auditWall.transform.parent, 0.025f);
            MissionObjective review = AddObjective(
                auditWall, "pm-hundred-day-review", "Independent PM 100-day review dekhein",
                "Independent National Governance Auditor",
                "Delivery, cabinet integrity, fiscal discipline, institutional trust, public record and pressure se first 100-day score compute hoga. Majority se audit exemption nahi milti.",
                0, 0, 0, false);
            review.ConfigurePrimeMinisterGovernanceReward(6, 8, 6, 4);
            review.ConfigurePrimeMinisterHundredDayReview();
            objectives.Add(review);
            labels.Add("Independent delivery, cabinet, fiscal and institutional review dekhein");

            mission.Configure(
                "Pradhan Mantri Ke Pehle 100 Din",
                objectives,
                labels,
                "CHAPTER 22 COMPLETE",
                "Independent PM 100-day review passed. Delivery, cabinet integrity, fiscal discipline aur institutions ne national reform term unlock kiya.");
            mission.ConfigureMilestones(
                new List<int> { 4, 8, 11 },
                new List<string> { "TRANSITION OPEN", "MISSIONS LIVE", "FEDERAL REVIEW READY" },
                new List<string>
                {
                    "Oath, cabinet, disclosures and public dashboard verified hain. Four national service missions next hain.",
                    "Health, learning, safety and livelihood pilots live hain. Governance model and federal compact next hain.",
                    "Governance model, federal compact and citizen-opposition review complete hain. Independent 100-day audit unlocked."
                });
            mission.ConfigureChapter(22, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 22 / PRADHAN MANTRI KE PEHLE 100 DIN",
                "Janadesh mil gaya, lekin ab har order ka budget, owner, evidence aur constitutional limit hoga. Majority responsibility hai, shortcut pass nahi.");
        }

        private static void CreateChapterTwentyTwoEnvironment(
            Transform root, Material sand, Material stone, Material darkStone, Material teal,
            Material yellow, Material white, Material foliage, Material trunk)
        {
            CreateBox("Union Governance Campus Ground", new Vector3(0f, -0.32f, 0f), new Vector3(112f, 0.64f, 82f), sand, root);
            CreateBox("Constitution Avenue", new Vector3(0f, 0.02f, -4f), new Vector3(13f, 0.08f, 76f), stone, root);
            CreateBox("Public Mission Crossway", new Vector3(0f, 0.03f, 15f), new Vector3(98f, 0.08f, 8f), stone, root);

            CreateBox("Union Governance House", new Vector3(0f, 5.4f, 35f), new Vector3(50f, 10.8f, 11f), white, root);
            CreateBox("Governance House Front", new Vector3(0f, 5.0f, 29.35f), new Vector3(43f, 8.8f, 0.42f), darkStone, root);
            for (int index = 0; index < 8; index++)
            {
                CreateBox($"Governance House Pillar {index + 1}",
                    new Vector3(-17.5f + index * 5f, 4.3f, 28.8f), new Vector3(1.1f, 8.6f, 1.1f), white, root);
            }
            CreateWorldLabel("Governance House Sign", "SEVA  /  SAMVIDHAN  /  ZIMMEDARI",
                new Vector3(0f, 9.0f, 28.55f), Vector3.zero, yellow, root, 0.030f);

            CreateBox("Health Mission Pavilion", new Vector3(-46f, 3.2f, 23f), new Vector3(18f, 6.4f, 19f), teal, root);
            CreateWorldLabel("Health Pavilion Sign", "HEALTH ACCESS",
                new Vector3(-36.85f, 4.3f, 23f), new Vector3(0f, -90f, 0f), yellow, root, 0.022f);
            CreateBox("Learning Mission Pavilion", new Vector3(-46f, 3.2f, -10f), new Vector3(18f, 6.4f, 20f), white, root);
            CreateWorldLabel("Learning Pavilion Sign", "LEARNING ACCESS",
                new Vector3(-36.85f, 4.3f, -10f), new Vector3(0f, -90f, 0f), teal, root, 0.021f);
            CreateBox("Safety Mission Pavilion", new Vector3(46f, 3.2f, 23f), new Vector3(18f, 6.4f, 19f), white, root);
            CreateWorldLabel("Safety Pavilion Sign", "CITIZEN SAFETY",
                new Vector3(36.85f, 4.3f, 23f), new Vector3(0f, 90f, 0f), teal, root, 0.021f);
            CreateBox("Livelihood Mission Pavilion", new Vector3(46f, 3.2f, -10f), new Vector3(18f, 6.4f, 20f), teal, root);
            CreateWorldLabel("Livelihood Pavilion Sign", "LIVELIHOOD + FORMS",
                new Vector3(36.85f, 4.3f, -10f), new Vector3(0f, 90f, 0f), yellow, root, 0.019f);

            CreateBox("Public Review Plaza", new Vector3(0f, -0.06f, 14f), new Vector3(31f, 0.18f, 19f), white, root);
            CreateBox("Oath And Responsibility Stage", new Vector3(0f, 0.72f, -36f), new Vector3(24f, 1.44f, 7f), darkStone, root);
            CreateBox("Oath Stage Backdrop", new Vector3(0f, 3.6f, -39.1f), new Vector3(24f, 5.7f, 0.38f), teal, root);
            CreateWorldLabel("Oath Stage Sign", "JANADESH SE ZIMMEDARI TAK",
                new Vector3(0f, 5.55f, -38.88f), Vector3.zero, yellow, root, 0.026f);

            for (int index = 0; index < 14; index++)
            {
                CreateStreetLamp($"Constitution Avenue Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -8f : 8f, 0f, -32f + index * 5.2f), darkStone, yellow, root);
            }
            for (int index = 0; index < 6; index++)
            {
                CreateExpansionFlag($"Federal Service Flag {index + 1}",
                    new Vector3(-13f + index * 5.2f, 0f, -39f), darkStone,
                    index % 2 == 0 ? teal : yellow, root);
            }
            CreateTree("Governance Neem North West", new Vector3(-53f, 0f, 37f), foliage, trunk, root);
            CreateTree("Governance Neem North East", new Vector3(53f, 0f, 37f), foliage, trunk, root);
            CreateTree("Governance Neem South West", new Vector3(-53f, 0f, -36f), foliage, trunk, root);
            CreateTree("Governance Neem South East", new Vector3(53f, 0f, -36f), foliage, trunk, root);
        }

        private static void CreateChapterTwentyTwoLighting()
        {
            GameObject lightObject = new GameObject("First Hundred Days Morning Light");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.93f, 0.80f);
            sunlight.intensity = 1.10f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.48f;
            lightObject.transform.rotation = Quaternion.Euler(42f, -38f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.48f, 0.69f, 0.87f);
            RenderSettings.ambientEquatorColor = new Color(0.80f, 0.71f, 0.58f);
            RenderSettings.ambientGroundColor = new Color(0.24f, 0.27f, 0.24f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.76f, 0.82f, 0.86f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 98f;
            RenderSettings.fogEndDistance = 252f;
        }
    }
}

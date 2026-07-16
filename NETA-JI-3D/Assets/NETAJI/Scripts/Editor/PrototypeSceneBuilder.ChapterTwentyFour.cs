using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterTwentyFourScene(
            Material sand, Material stone, Material darkStone, Material teal, Material yellow,
            Material white, Material shirt, Material trousers, Material skin, Material hair,
            Material shantiDress, Material volunteerDress, Material policeKhaki,
            Material foliage, Material trunk)
        {
            Scene chapterScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject environment = new GameObject("Fictional Global Cooperation Campus");
            CreateChapterTwentyFourEnvironment(
                environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 24 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                24, 100, 0, 100,
                proof: 100, power: 89, team: 750, pressure: 72,
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
                nationalLivelihoodIndex: 90, nationalDevelopmentScore: 87, nationalDevelopmentReviewPassed: true,
                globalTradeTrust: 92, scienceInnovationLeadership: 100, peaceDefenseReadiness: 94,
                humanitarianClimateLeadership: 100, globalLeadershipScore: 96, vishwaGuruOutcomeEarned: true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson(
                "Prime Minister Azad Global Review", new Vector3(0f, 0f, -42f), white, white, skin, hair, true);
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
            gameCamera.farClipPlane = 300f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 4.0f, -47f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterTwentyFourMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterTwentyFourLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterTwentyFourScenePath);
        }

        private static void ConfigureChapterTwentyFourMission(
            MissionController mission, Material shantiDress, Material volunteerDress,
            Material policeKhaki, Material teal, Material yellow, Material white,
            Material shirt, Material trousers, Material darkStone, Material skin, Material hair)
        {
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject charter = new GameObject("Global Service Charter Wall");
            charter.transform.position = new Vector3(0f, 2.6f, -31f);
            CreatePrimitiveChild("Charter Screen", PrimitiveType.Cube, charter.transform,
                Vector3.zero, new Vector3(14f, 5.2f, 0.36f), darkStone);
            for (int index = 0; index < 8; index++)
            {
                CreatePrimitiveChild($"Charter Promise {index + 1}", PrimitiveType.Cube, charter.transform,
                    new Vector3(-4.8f + (index % 4) * 3.2f, 1.2f - (index / 4) * 1.7f, -0.21f),
                    new Vector3(2.25f, 0.9f, 0.07f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective charterObjective = AddObjective(
                charter, "global-service-charter", "Global service charter aur audit rules sign karein",
                "Independent Global Cooperation Council",
                "Trade, science, defensive peace aur humanitarian work ke targets public methodology, privacy limits, appeals aur correction history ke saath lock hue. Vishwa Guru title khud se nahi, review se earn hoga.",
                0, 0, 0, false, 2);
            charterObjective.ConfigurePoliticalReward(0, 4, -2);
            charterObjective.ConfigureGlobalLeadershipReward(8, 4, 4, 8);
            objectives.Add(charterObjective);
            labels.Add("Public targets, privacy, appeals aur independent review charter verify karein");

            GameObject tradeLead = CreatePerson(
                "Fair Trade Standards Lead", new Vector3(-43f, 0f, -20f), shirt, trousers, skin, hair, false);
            CreateChapterNineCrowd("Small Exporters And Worker Delegates", new Vector3(-43f, 0f, -14f), 14,
                volunteerDress, white, teal, darkStone, skin, hair);
            MissionObjective trade = AddObjective(
                tradeLead, "fair-trade-corridor", "Fair trade aur worker standards corridor review karein",
                "Fair Trade Standards Lead",
                "Small exporters, farmers, workers and consumers ke liye transparent contracts, product standards, timely payment, labour safety and dispute appeal ek hi audited corridor mein hain.",
                0, 0, 0, false);
            trade.ConfigurePoliticalReward(0, 8, 0);
            trade.ConfigureGlobalLeadershipReward(20, 4, 4, 4);
            objectives.Add(trade);
            labels.Add("Contracts, standards, payments, worker safety aur dispute appeals review karein");

            GameObject scienceLead = CreatePerson(
                "Open Science Mission Lead", new Vector3(-43f, 0f, 10f), volunteerDress, darkStone, skin, hair, false);
            CreateChapterNineCrowd("Researchers Students And Innovators", new Vector3(-43f, 0f, 16f), 14,
                shirt, volunteerDress, yellow, darkStone, skin, hair);
            MissionObjective science = AddObjective(
                scienceLead, "open-science-mission", "Open science aur innovation mission inspect karein",
                "Open Science Mission Lead",
                "Public research, scholarships, rural labs, health technology, space data and startup procurement reproducible evidence aur safety review ke saath share ho rahe hain. Patent headline akela innovation nahi hai.",
                0, 0, 0, false);
            science.ConfigurePoliticalReward(0, 8, 0);
            science.ConfigureGlobalLeadershipReward(4, 20, 4, 4);
            objectives.Add(science);
            labels.Add("Research, scholarships, rural labs, safety aur open evidence audit karein");

            GameObject peaceLead = CreatePerson(
                "Defensive Peace And Disaster Readiness Lead", new Vector3(-35f, 0f, 34f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(peaceLead.transform, policeKhaki, darkStone);
            MissionObjective peace = AddObjective(
                peaceLead, "defensive-peace-readiness", "Defensive peace and disaster readiness drill verify karein",
                "Defensive Peace And Disaster Readiness Lead",
                "Border safety fictional simulation, cyber defence, coast rescue, veterans care, civilian oversight and disaster logistics tested hain. Strength ka goal deterrence aur rescue hai, conquest nahi.",
                0, 0, 0, false, 2);
            peace.ConfigurePoliticalReward(0, 8, -1);
            peace.ConfigureGlobalLeadershipReward(4, 4, 20, 4);
            objectives.Add(peace);
            labels.Add("Cyber defence, rescue, oversight, veterans aur disaster logistics verify karein");

            GameObject humanitarianLead = CreatePerson(
                "Humanitarian Climate Network Lead", new Vector3(-9f, 0f, 41f), shantiDress, darkStone, skin, hair, true);
            CreateChapterNineCrowd("Relief Health And Climate Teams", new Vector3(-3f, 0f, 41f), 14,
                volunteerDress, white, teal, darkStone, skin, hair);
            MissionObjective humanitarian = AddObjective(
                humanitarianLead, "humanitarian-climate-network", "Humanitarian climate network ka field record dekhein",
                "Humanitarian Climate Network Lead",
                "Flood, heat, medicine, food, shelter and disability support need-based protocol se chala. Aid par party poster nahi laga; local partners ko credit aur affected families ko complaint channel mila.",
                0, 0, 0, false);
            humanitarian.ConfigurePoliticalReward(0, 8, -1);
            humanitarian.ConfigureGlobalLeadershipReward(4, 4, 4, 20);
            objectives.Add(humanitarian);
            labels.Add("Flood, heat, medicine, shelter, disability access aur complaint record dekhein");

            GameObject strategyStage = new GameObject("Global Leadership Strategy Stage");
            strategyStage.transform.position = new Vector3(21f, 1.1f, 35f);
            CreatePrimitiveChild("Global Strategy Platform", PrimitiveType.Cube, strategyStage.transform,
                Vector3.zero, new Vector3(11f, 1.1f, 5.5f), darkStone);
            CreatePrimitiveChild("Global Strategy Backdrop", PrimitiveType.Cube, strategyStage.transform,
                new Vector3(0f, 2f, 2.5f), new Vector3(11f, 3.7f, 0.28f), teal);
            CreateWorldLabel("Global Strategy Sign", "SEVA  /  SAATH  /  SAMAAN",
                new Vector3(21f, 3.65f, 37.64f), Vector3.zero, yellow, strategyStage.transform.parent, 0.022f);
            MissionObjective strategy = AddObjective(
                strategyStage, "global-leadership-approach", "India ka global leadership approach chunein",
                "Independent Global Cooperation Council",
                "Seva + Saath Compact slow headlines ke saath open science, fair trade, defensive peace and humanitarian trust balance karega.",
                0, 0, 0, false);
            strategy.ConfigurePoliticalReward(4, 10, 3);
            strategy.ConfigureGlobalLeadershipReward(10, 12, 12, 12);
            strategy.ConfigureDecision(
                "global-leadership-approach",
                "VISHWA MEIN INDIA KA RAASTA",
                "Leadership cooperation se earn karni hai ya spectacle se claim? Final independent review chaaron pillars ko separately check karega.",
                "SEVA + SAATH COMPACT\nTrust + Science + Peace",
                "SUPERPOWER SPECTACLE\nVisibility + Pressure",
                "Mega summit, giant screens aur loud claims ne visibility badhai, par science openness, defensive restraint aur humanitarian trust weak hua. Review title ko automatic nahi maanega.",
                0, 0, 0, 0, 8, 4, 14);
            strategy.ConfigureGlobalLeadershipDecisionRewards(28, -4, -8, -6);
            objectives.Add(strategy);
            labels.Add("Seva + Saath Compact ya Superpower Spectacle mein binding decision lein");

            GameObject technologyWall = new GameObject("Open Technology Standards Lab");
            technologyWall.transform.position = new Vector3(44f, 2.4f, 22f);
            CreatePrimitiveChild("Open Standards Screen", PrimitiveType.Cube, technologyWall.transform,
                Vector3.zero, new Vector3(11f, 4.8f, 0.34f), darkStone);
            for (int index = 0; index < 10; index++)
            {
                CreatePrimitiveChild($"Standards Module {index + 1}", PrimitiveType.Cube, technologyWall.transform,
                    new Vector3(-3.8f + (index % 5) * 1.9f, 1.1f - (index / 5) * 1.5f, -0.20f),
                    new Vector3(1.25f, 0.78f, 0.07f), index % 2 == 0 ? yellow : teal);
            }
            MissionObjective technology = AddObjective(
                technologyWall, "open-technology-standards", "Open technology standards aur safety audit publish karein",
                "Open Technology Standards Panel",
                "Interoperability, cyber safety, accessible design, research reproducibility, public procurement and bias testing ke standards public hain. Vendor lock-in ko innovation trophy nahi mili.",
                0, 0, 0, false, 3);
            technology.ConfigurePoliticalReward(0, 8, 0);
            technology.ConfigureGlobalLeadershipReward(8, 12, 4, 8);
            objectives.Add(technology);
            labels.Add("Interoperability, cyber safety, accessibility, bias aur procurement audit publish karein");

            GameObject climateLead = CreatePerson(
                "Climate Response Coalition Convenor", new Vector3(45f, 0f, -5f), volunteerDress, trousers, skin, hair, false);
            MissionObjective climate = AddObjective(
                climateLead, "climate-response-coalition", "Climate response coalition drill complete karein",
                "Climate Response Coalition Convenor",
                "Heat alerts, river data, clean energy reliability, resilient crops, emergency finance and local rescue teams common protocol par chale. Emission claim ko satellite aur ground samples cross-check karte hain.",
                0, 0, 0, false);
            climate.ConfigurePoliticalReward(0, 8, -1);
            climate.ConfigureGlobalLeadershipReward(6, 6, 6, 14);
            objectives.Add(climate);
            labels.Add("Heat, rivers, energy, crops, emergency finance aur rescue drill complete karein");

            GameObject exchangeLead = CreatePerson(
                "Knowledge And Culture Exchange Lead", new Vector3(42f, 0f, -29f), shirt, darkStone, skin, hair, false);
            CreateChapterNineCrowd("Students Artists And Language Fellows", new Vector3(36f, 0f, -29f), 12,
                shirt, volunteerDress, yellow, darkStone, skin, hair);
            MissionObjective exchange = AddObjective(
                exchangeLead, "knowledge-culture-exchange", "Knowledge and culture exchange programme review karein",
                "Knowledge And Culture Exchange Lead",
                "Students, teachers, artists, translators and researchers two-way exchange mein hain. Culture export ke saath sunna, credit dena aur local language access bhi programme ka result hai.",
                0, 0, 0, false);
            exchange.ConfigurePoliticalReward(0, 6, -1);
            exchange.ConfigureGlobalLeadershipReward(6, 10, 4, 8);
            objectives.Add(exchange);
            labels.Add("Student, teacher, artist, translation aur two-way exchange outcomes review karein");

            GameObject shanti = CreatePerson(
                "Shanti Global Citizen Accountability Lead", new Vector3(18f, 0f, -33f), shantiDress, darkStone, skin, hair, true);
            CreateChapterNineCrowd("Citizen And Diaspora Review Circle", new Vector3(18f, 0f, -27f), 12,
                volunteerDress, white, teal, darkStone, skin, hair);
            MissionObjective citizenReview = AddObjective(
                shanti, "global-citizen-accountability", "Shanti ka citizen accountability review publish karein",
                "Shanti",
                "Foreign headline se pehle dekho ki student, worker, traveller aur disaster volunteer ko fair treatment mila ya nahi. Helpline closures, consent, cost and grievance appeals raw sample ke saath public hain.",
                0, 0, 0, false, 2);
            citizenReview.ConfigurePoliticalReward(0, 6, -2);
            citizenReview.ConfigureGlobalLeadershipReward(6, 6, 10, 8);
            objectives.Add(citizenReview);
            labels.Add("Student, worker, traveller, volunteer, consent aur grievance data publish karein");

            GameObject samrat = CreatePerson(
                "Constable Samrat Ground Truth Delegate", new Vector3(-11f, 0f, -20f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective groundTruth = AddObjective(
                samrat, "global-ground-truth-audit", "Samrat ka local-to-global ground truth audit dein",
                "Constable Samrat",
                "Bada title tabhi sach hai jab local emergency call, missing-person response, medicine delivery aur witness protection ground par kaam kare. Weak cases bhi report mein hain; poster team ko thoda bura laga, auditor ko nahi.",
                0, 0, 0, false);
            groundTruth.ConfigurePoliticalReward(0, 6, -1);
            groundTruth.ConfigureGlobalLeadershipReward(8, 8, 8, 10);
            objectives.Add(groundTruth);
            labels.Add("Local emergency, medicine, missing-person aur witness ground truth audit dein");

            GameObject reviewWall = new GameObject("Independent Vishwa Guru Review Wall");
            reviewWall.transform.position = new Vector3(0f, 3.2f, 16f);
            CreatePrimitiveChild("Global Leadership Review Screen", PrimitiveType.Cube, reviewWall.transform,
                Vector3.zero, new Vector3(16f, 6.4f, 0.40f), darkStone);
            Material[] meterMaterials = { teal, yellow, teal, yellow };
            for (int index = 0; index < 4; index++)
            {
                CreatePrimitiveChild($"Global Leadership Meter {index + 1}", PrimitiveType.Cube, reviewWall.transform,
                    new Vector3(-5.2f + index * 3.45f, -0.75f, -0.24f),
                    new Vector3(2.5f, 3.5f + index * 0.25f, 0.08f), meterMaterials[index]);
            }
            CreateWorldLabel("Global Review Sign", "TRADE  /  SCIENCE  /  PEACE  /  HUMANITY",
                new Vector3(0f, 6.45f, 15.72f), Vector3.zero, yellow, reviewWall.transform.parent, 0.022f);
            MissionObjective finalReview = AddObjective(
                reviewWall, "global-leadership-review", "Independent global leadership review dekhein",
                "Independent Global Leadership Review Panel",
                "National development foundation, institutions, trade trust, science, defensive peace and humanitarian-climate leadership weighted score mein aayenge. Chaaron pillars 75+ aur final score 88+ ke bina title hold par rahega.",
                0, 0, 0, false);
            finalReview.ConfigureGlobalLeadershipReward(8, 10, 14, 6);
            finalReview.ConfigureGlobalLeadershipReview();
            objectives.Add(finalReview);
            labels.Add("Independent four-pillar Vishwa Guru review aur earned outcome dekhein");

            mission.Configure(
                "Seva Se Vishwa Guru",
                objectives,
                labels,
                "AZAD KA SAFAR COMPLETE",
                "India ne domination se nahi, audited seva, science, shanti aur saath se global leadership earn ki. NETA JI Prototype 1 story complete.");
            mission.ConfigureMilestones(
                new List<int> { 5, 9, 11 },
                new List<string> { "FOUR GLOBAL PILLARS READY", "COOPERATION NETWORK ACTIVE", "GROUND TRUTH VERIFIED" },
                new List<string>
                {
                    "Trade, science, defensive peace and humanitarian baselines ready hain. Leadership approach next hai.",
                    "Strategy, open technology, climate response and knowledge exchange locked hain. Citizen accountability next hai.",
                    "Citizen and local ground-truth audits public hain. Independent final review unlocked."
                });
            mission.ConfigureChapter(24, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 24 / SEVA SE VISHWA GURU",
                "Global leadership koi map capture nahi. Fair trade, open science, defensive peace aur humanitarian trust ko evidence se earn karna hai.");
        }

        private static void CreateChapterTwentyFourEnvironment(
            Transform root, Material sand, Material stone, Material darkStone, Material teal,
            Material yellow, Material white, Material foliage, Material trunk)
        {
            CreateBox("Global Cooperation Campus Ground", new Vector3(0f, -0.32f, 0f), new Vector3(126f, 0.64f, 94f), sand, root);
            CreateBox("Cooperation Avenue", new Vector3(0f, 0.02f, -4f), new Vector3(14f, 0.08f, 88f), stone, root);
            CreateBox("Four Pillar Crossway", new Vector3(0f, 0.03f, 17f), new Vector3(112f, 0.08f, 8f), stone, root);
            CreateBox("Global Review Hall", new Vector3(0f, 6f, 43f), new Vector3(56f, 12f, 10f), darkStone, root);
            CreateBox("Review Hall Public Face", new Vector3(0f, 5.4f, 37.85f), new Vector3(49f, 9.6f, 0.42f), teal, root);
            CreateWorldLabel("Global Hall Sign", "INDIA  /  SERVICE  /  GLOBAL TRUST",
                new Vector3(0f, 10.1f, 37.58f), Vector3.zero, yellow, root, 0.028f);

            CreateBox("Fair Trade Pavilion", new Vector3(-52f, 3.5f, -18f), new Vector3(19f, 7f, 21f), white, root);
            CreateWorldLabel("Trade Pavilion Sign", "FAIR TRADE",
                new Vector3(-42.3f, 4.8f, -18f), new Vector3(0f, -90f, 0f), teal, root, 0.023f);
            CreateBox("Open Science Pavilion", new Vector3(-52f, 3.5f, 13f), new Vector3(19f, 7f, 21f), teal, root);
            CreateWorldLabel("Science Pavilion Sign", "OPEN SCIENCE",
                new Vector3(-42.3f, 4.8f, 13f), new Vector3(0f, -90f, 0f), yellow, root, 0.023f);
            CreateBox("Peace And Rescue Pavilion", new Vector3(52f, 3.5f, 21f), new Vector3(19f, 7f, 21f), white, root);
            CreateWorldLabel("Peace Pavilion Sign", "PEACE + RESCUE",
                new Vector3(42.3f, 4.8f, 21f), new Vector3(0f, 90f, 0f), teal, root, 0.021f);
            CreateBox("Humanitarian Climate Pavilion", new Vector3(52f, 3.5f, -13f), new Vector3(19f, 7f, 22f), teal, root);
            CreateWorldLabel("Humanitarian Pavilion Sign", "HUMANITY + CLIMATE",
                new Vector3(42.3f, 4.8f, -13f), new Vector3(0f, 90f, 0f), yellow, root, 0.019f);

            CreateBox("Opening Stage", new Vector3(0f, 0.74f, -44f), new Vector3(28f, 1.48f, 7f), darkStone, root);
            CreateBox("Opening Stage Backdrop", new Vector3(0f, 3.8f, -47.15f), new Vector3(28f, 6f, 0.40f), teal, root);
            CreateWorldLabel("Opening Stage Sign", "VISHWA GURU  /  EARNED, NOT CLAIMED",
                new Vector3(0f, 5.9f, -46.92f), Vector3.zero, yellow, root, 0.023f);

            for (int index = 0; index < 16; index++)
            {
                CreateStreetLamp($"Cooperation Avenue Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -8.7f : 8.7f, 0f, -39f + index * 5.4f), darkStone, yellow, root);
            }
            for (int index = 0; index < 12; index++)
            {
                CreateExpansionFlag($"Cooperation Flag {index + 1}",
                    new Vector3(-28f + index * 5.1f, 0f, -47f), darkStone,
                    index % 3 == 0 ? white : index % 2 == 0 ? teal : yellow, root);
            }
            CreateTree("Global Neem North West", new Vector3(-59f, 0f, 42f), foliage, trunk, root);
            CreateTree("Global Neem North East", new Vector3(59f, 0f, 42f), foliage, trunk, root);
            CreateTree("Global Neem South West", new Vector3(-59f, 0f, -42f), foliage, trunk, root);
            CreateTree("Global Neem South East", new Vector3(59f, 0f, -42f), foliage, trunk, root);
        }

        private static void CreateChapterTwentyFourLighting()
        {
            GameObject lightObject = new GameObject("Global Cooperation Sunrise Light");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.94f, 0.82f);
            sunlight.intensity = 1.14f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.50f;
            lightObject.transform.rotation = Quaternion.Euler(39f, -31f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.45f, 0.70f, 0.89f);
            RenderSettings.ambientEquatorColor = new Color(0.80f, 0.74f, 0.61f);
            RenderSettings.ambientGroundColor = new Color(0.22f, 0.29f, 0.25f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.75f, 0.85f, 0.89f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 112f;
            RenderSettings.fogEndDistance = 290f;
        }
    }
}

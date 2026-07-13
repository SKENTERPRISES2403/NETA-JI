using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterThirteenScene(
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
            GameObject environment = new GameObject("UP-Inspired Fictional Multi-Seat Campaign Campus");
            CreateChapterThirteenEnvironment(
                environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 13 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                13, 100, 40, 100, 100, 52, 165, 99, 67, 85, 59, true,
                65, 87, 5, 85, true, 88, 93, 99, 91, true, 58, 92, 69, 58, true,
                78, 80, 90, 30, 78, true, 78, 90, 92, 89, true,
                80, 89, 98, 82, 6, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("MLA Azad State Organizer", new Vector3(0f, 0f, -25f), white, white, skin, hair, true);
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
            gameCamera.farClipPlane = 220f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.8f, -30f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterThirteenMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterThirteenLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterThirteenScenePath);
        }

        private static void ConfigureChapterThirteenMission(
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

            GameObject eligibilityLead = CreatePerson(
                "State Expansion Eligibility Lead", new Vector3(0f, 0f, -20f), white, darkStone, skin, hair, false);
            MissionObjective eligibility = AddObjective(
                eligibilityLead,
                "state-eligibility",
                "District mandate verify karein",
                "Independent Expansion Auditor",
                "MLA report, district score, candidate rules and public-fund separation verified. Eight fictional seats ka campaign tabhi khulega jab local service desks active rahenge.",
                0, 0, 0, false, 1);
            eligibility.ConfigurePoliticalReward(0, 0, -10);
            eligibility.ConfigureStateExpansionReward(4, 8, 10);
            objectives.Add(eligibility);
            labels.Add("Independent auditor se district mandate aur state-expansion eligibility verify karein");

            GameObject atlas = new GameObject("Eight Seat Public Issue Atlas");
            atlas.transform.position = new Vector3(-11f, 0.95f, -15f);
            CreatePrimitiveChild("Atlas Table", PrimitiveType.Cube, atlas.transform, Vector3.zero, new Vector3(6.8f, 0.30f, 4.8f), darkStone);
            for (int row = 0; row < 2; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    Material tileMaterial = (row + column) % 2 == 0 ? teal : yellow;
                    CreatePrimitiveChild(
                        $"Fictional Seat Tile {row * 4 + column + 1}",
                        PrimitiveType.Cube,
                        atlas.transform,
                        new Vector3(-2.35f + column * 1.55f, 0.23f, -1.15f + row * 2.3f),
                        new Vector3(1.25f, 0.12f, 1.75f),
                        tileMaterial);
                }
            }
            MissionObjective issueAtlas = AddObjective(
                atlas,
                "seat-issue-atlas",
                "Eight-seat issue atlas lock karein",
                "Public Data Team",
                "Eight fictional seats ke water, schools, clinics, jobs, transport and paperwork baselines same public format mein mapped hain. Real constituency result ya voter profile use nahi hua.",
                0, 0, 0, false);
            issueAtlas.ConfigurePoliticalReward(0, 0, -8);
            issueAtlas.ConfigureStateExpansionReward(12, 4, 6);
            objectives.Add(issueAtlas);
            labels.Add("Eight fictional seats ka comparable public-issue atlas lock karein");

            GameObject slateLead = CreatePerson(
                "Candidate Slate Ethics Lead", new Vector3(-23f, 0f, -7f), teal, darkStone, skin, hair, false);
            MissionObjective slate = AddObjective(
                slateLead,
                "state-slate-vetting",
                "Candidate slate vet karein",
                "Candidate Ethics Panel",
                "Service record, assets, liabilities, conflicts, criminal-case disclosure and public interview scored hain. Caste, religion, family identity and rumour selection se bahar hain.",
                0, 0, 0, false, 2);
            slate.ConfigurePoliticalReward(0, 0, 2);
            slate.ConfigureStateExpansionReward(5, 16, 8);
            objectives.Add(slate);
            labels.Add("Eight-seat fictional candidate slate ko published ethics criteria se vet karein");

            GameObject fundingDesk = new GameObject("Legal Small Donation Desk");
            fundingDesk.transform.position = new Vector3(-24f, 1.1f, 4f);
            CreatePrimitiveChild("Donation Counter", PrimitiveType.Cube, fundingDesk.transform, Vector3.zero, new Vector3(4.5f, 1.25f, 1.8f), teal);
            CreatePrimitiveChild("Receipt Stack", PrimitiveType.Cube, fundingDesk.transform, new Vector3(-0.9f, 0.74f, 0f), new Vector3(0.9f, 0.14f, 0.75f), white);
            CreatePrimitiveChild("Donor Cap Ledger", PrimitiveType.Cube, fundingDesk.transform, new Vector3(0.9f, 0.74f, 0f), new Vector3(0.9f, 0.14f, 0.75f), yellow);
            MissionObjective funding = AddObjective(
                fundingDesk,
                "state-fundraising",
                "Small-donor drive audit karein",
                "Independent Finance Volunteer",
                "Rs 150 campaign funds receipt-backed small donations se aaye. Donor cap, refund route and conflict register public hain; anonymous large money reject hua.",
                0, 150, 0, false, 1);
            funding.ConfigurePoliticalReward(0, 0, 1);
            funding.ConfigureStateExpansionReward(4, 8, 8);
            objectives.Add(funding);
            labels.Add("Receipt-backed small-donor campaign fund aur conflict register audit karein");

            GameObject trainingLead = CreatePerson(
                "Polling Volunteer Trainer", new Vector3(-17f, 0f, 14f), volunteerDress, darkStone, skin, hair, false);
            CreateChapterNineCrowd(
                "Multi Seat Volunteer Cohort", new Vector3(-17f, 0f, 17f), 8,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective training = AddObjective(
                trainingLead,
                "state-volunteer-training",
                "Volunteer cohort train karein",
                "Polling Volunteer Trainer",
                "Consent, shift limits, accessibility, queue help, incident log and no-hate protocol eight seat teams ne rehearse kiya. Volunteers public servants ko direct nahi karenge.",
                0, 0, 0, false);
            training.ConfigurePoliticalReward(0, 8, 2);
            training.ConfigureStateExpansionReward(8, 6, 12);
            objectives.Add(training);
            labels.Add("Eight-seat volunteer cohort ko lawful polling and safety protocol train karein");

            GameObject shanti = CreatePerson(
                "Shanti Public Debate Moderator", new Vector3(0f, 0f, 19f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            MissionObjective debate = AddObjective(
                shanti,
                "state-public-debate",
                "Public candidate debate karein",
                "Shanti",
                "Har candidate ko costed plan, conflict question and citizen follow-up ka same time mila. Slogans short rakhe, answers aur source links long rakhe gaye.",
                0, 0, 2, false);
            debate.ConfigurePoliticalReward(0, 3, 2);
            debate.ConfigureStateExpansionReward(6, 10, 6);
            objectives.Add(debate);
            labels.Add("Shanti ke saath equal-time public candidate debate complete karein");

            GameObject strategyStage = new GameObject("Multi Seat Campaign Strategy Stage");
            strategyStage.transform.position = new Vector3(16f, 1.15f, 15f);
            CreatePrimitiveChild("Strategy Platform", PrimitiveType.Cube, strategyStage.transform, Vector3.zero, new Vector3(7.2f, 1.0f, 4.2f), darkStone);
            CreatePrimitiveChild("Strategy Backdrop", PrimitiveType.Cube, strategyStage.transform, new Vector3(0f, 2f, 1.85f), new Vector3(7.2f, 3.1f, 0.22f), teal);
            CreateWorldLabel(
                "Strategy Stage Label", "FIELD FIRST  /  EIGHT FICTIONAL SEATS",
                new Vector3(16f, 3.35f, 16.74f), Vector3.zero, yellow, strategyStage.transform.parent, 0.019f);
            MissionObjective strategy = AddObjective(
                strategyStage,
                "state-expansion-strategy",
                "Multi-seat strategy chunein",
                "State Campaign Committee",
                "Phased field campaign seat-wise service evidence, candidate debate and logistics ko sequence karta hai. Rs 120 lagenge, par slate ownership and polling readiness strong rahenge.",
                0, -120, 0, false);
            strategy.ConfigurePoliticalReward(2, 6, 4);
            strategy.ConfigureStateExpansionReward(14, 14, 14);
            strategy.ConfigureDecision(
                "state-expansion-strategy",
                "EIGHT-SEAT CAMPAIGN STRATEGY",
                "Phased field work costlier aur slower hai. All-seat media blitz reach jaldi badhata hai, par candidate scrutiny, local ownership and polling operations weak ho sakte hain.",
                "PHASED FIELD CAMPAIGN\nIntegrity +14 / Ops +14",
                "ALL-SEAT MEDIA BLITZ\nReach +26 / Pressure +14",
                "Blitz ne launch fast kar diya, lekin two candidate files rushed, coordinators ko late notice mila and polling dry-runs cut hue. Public correction mandatory hai.",
                0, -30, -10, -5, 6, 2, 14,
                riskyStateCampaignReach: 26,
                riskyCandidateSlateIntegrity: -8,
                riskyStateElectionOperations: -10);
            objectives.Add(strategy);
            labels.Add("Phased field campaign ya all-seat media blitz mein strategic decision lein");

            GameObject safetyLead = CreatePerson(
                "Campaign Safety Counsel", new Vector3(24f, 0f, 6f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(safetyLead.transform, policeKhaki, darkStone);
            MissionObjective safety = AddObjective(
                safetyLead,
                "state-safety-response",
                "Intimidation evidence secure karein",
                "Independent Safety Counsel",
                "Fictional threat call ka original log, witness consent and legal complaint secure hua. Constable Samrat public-safety channel tak neutral raha; campaign decision mein police role nahi hai.",
                0, 0, 0, false, 2);
            safety.ConfigurePoliticalReward(0, 0, 4);
            safety.ConfigureStateExpansionReward(6, 5, 8);
            objectives.Add(safety);
            labels.Add("Threat report ko evidence-safe neutral public-safety channel se file karein");

            GameObject logisticsBoard = new GameObject("Eight Seat Polling Logistics Board");
            logisticsBoard.transform.position = new Vector3(25f, 1.7f, -4f);
            CreatePrimitiveChild("Logistics Screen", PrimitiveType.Cube, logisticsBoard.transform, Vector3.zero, new Vector3(5.8f, 3.2f, 0.24f), darkStone);
            for (int index = 0; index < 8; index++)
            {
                CreatePrimitiveChild(
                    $"Seat Logistics Light {index + 1}", PrimitiveType.Cube, logisticsBoard.transform,
                    new Vector3(-2.0f + (index % 4) * 1.35f, 0.65f - (index / 4) * 1.3f, -0.17f),
                    new Vector3(0.55f, 0.55f, 0.06f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective logistics = AddObjective(
                logisticsBoard,
                "state-polling-logistics",
                "Polling dry-run complete karein",
                "Election Operations Lead",
                "Route sheets, booth-agent training, accessibility checks, queue help and incident escalation eight fictional seats par timed dry-run se pass hue.",
                0, 0, 0, false);
            logistics.ConfigurePoliticalReward(0, 4, 2);
            logistics.ConfigureStateExpansionReward(10, 5, 12);
            objectives.Add(logistics);
            labels.Add("Eight seats ka polling logistics and accessibility dry-run pass karein");

            GameObject conventionLead = CreatePerson(
                "State Public Convention Moderator", new Vector3(15f, 0f, -15f), yellow, darkStone, skin, hair, false);
            CreateChapterNineCrowd(
                "Eight Seat Public Delegates", new Vector3(15f, 0f, -18f), 10,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective convention = AddObjective(
                conventionLead,
                "state-public-convention",
                "Eight-seat convention karein",
                "Public Convention Moderator",
                "Each seat reports solved cases, candidate score, funds and unresolved objections. Azad last speaks; rival questions receive the same response window.",
                0, 0, 0, false);
            convention.ConfigurePoliticalReward(3, 5, 2);
            convention.ConfigureStateExpansionReward(7, 5, 5);
            objectives.Add(convention);
            labels.Add("Eight-seat evidence-led convention aur objection session complete karein");

            GameObject complianceLead = CreatePerson(
                "State Campaign Compliance Lead", new Vector3(3f, 0f, -18f), white, darkStone, skin, hair, false);
            MissionObjective compliance = AddObjective(
                complianceLead,
                "state-compliance",
                "Final campaign compliance lock karein",
                "Independent Compliance Panel",
                "Candidate disclosures, donor receipts, consent logs, accessibility, complaints, media corrections and anti-hate training complete. Protected-trait voter profiles store nahi hue.",
                0, 0, 0, false, 2);
            compliance.ConfigurePoliticalReward(0, 0, 1);
            compliance.ConfigureStateExpansionReward(4, 8, 9);
            objectives.Add(compliance);
            labels.Add("Candidate, funding, consent and anti-hate compliance audit lock karein");

            GameObject resultWall = new GameObject("Multi Seat Result Wall");
            resultWall.transform.position = new Vector3(0f, 2.1f, -11f);
            CreatePrimitiveChild("Result Screen", PrimitiveType.Cube, resultWall.transform, Vector3.zero, new Vector3(7.4f, 4.0f, 0.28f), darkStone);
            for (int index = 0; index < 8; index++)
            {
                CreatePrimitiveChild(
                    $"Result Seat {index + 1}", PrimitiveType.Cube, resultWall.transform,
                    new Vector3(-2.45f + (index % 4) * 1.65f, 0.75f - (index / 4) * 1.55f, -0.18f),
                    new Vector3(0.95f, 0.72f, 0.06f), index < 6 ? teal : white);
            }
            CreateWorldLabel(
                "Result Wall Label", "MULTI-SEAT PUBLIC RESULT",
                new Vector3(0f, 4.45f, -11.18f), Vector3.zero, yellow, resultWall.transform.parent, 0.024f);
            MissionObjective result = AddObjective(
                resultWall,
                "state-result",
                "Multi-seat result dekhein",
                "Independent State Review Panel",
                "Reach, slate integrity, polling operations, district record and pressure ka computed eight-seat result ready hai.",
                0, 0, 0, false);
            result.ConfigureStateFoothold();
            objectives.Add(result);
            labels.Add("Independent panel par computed eight-seat result aur state foothold dekhein");

            mission.Configure(
                "Pradesh Ki Dastak",
                objectives,
                labels,
                "CHAPTER 13 COMPLETE",
                "Eight fictional seats ka transparent campaign complete. State foothold ab earned result par khada hai.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "EIGHT-SEAT BASELINE LOCKED", "CAMPAIGN STRATEGY ACTIVE", "POLLING SYSTEM READY" },
                new List<string>
                {
                    "District mandate, issue atlas and candidate slate verified. Ab clean funding, teams and debate ready karo.",
                    "Funding, training, debate and strategy complete. Safety evidence and polling operations next hain.",
                    "Safety response, polling dry-run and public convention complete. Final compliance result decide karega."
                });
            mission.ConfigureChapter(13, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 13 / PRADESH KI DASTAK",
                "Ek district se aage: eight fictional seats par clean candidates aur lawful operations prove karo.");
        }

        private static void CreateChapterThirteenEnvironment(
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
            CreateBox("State Campaign Ground", new Vector3(0f, -0.32f, 0f), new Vector3(92f, 0.64f, 66f), sand, root);
            CreateBox("Central Campaign Avenue", new Vector3(0f, 0.02f, -3f), new Vector3(9f, 0.08f, 60f), stone, root);
            CreateBox("Cross Seat Route", new Vector3(0f, 0.03f, 7f), new Vector3(76f, 0.08f, 7f), stone, root);

            CreateBox("State Campaign Command Centre", new Vector3(0f, 3.4f, 28f), new Vector3(28f, 6.8f, 8f), teal, root);
            CreateBox("Command Centre Awning", new Vector3(0f, 6.9f, 25.2f), new Vector3(31f, 0.42f, 3f), yellow, root);
            CreateWorldLabel(
                "Command Centre Sign", "INDIA HELPING PARTY  /  FICTIONAL STATE CAMPAIGN",
                new Vector3(0f, 4.75f, 23.92f), Vector3.zero, white, root, 0.024f);
            CreateWorldLabel(
                "Campaign Safety Sign", "SERVICE  /  DISCLOSURE  /  CONSENT  /  AUDIT",
                new Vector3(0f, 3.55f, 23.90f), Vector3.zero, yellow, root, 0.019f);

            CreateBox("Candidate Disclosure Hall", new Vector3(-31f, 3f, -5f), new Vector3(15f, 6f, 18f), white, root);
            CreateWorldLabel(
                "Disclosure Hall Sign", "CANDIDATE DISCLOSURE HALL",
                new Vector3(-23.38f, 4.1f, -5f), new Vector3(0f, -90f, 0f), teal, root, 0.020f);

            CreateBox("Volunteer Training Hangar", new Vector3(-27f, 2.7f, 19f), new Vector3(21f, 5.4f, 10f), darkStone, root);
            CreateWorldLabel(
                "Training Hangar Sign", "VOLUNTEERS  /  SHIFT  /  SAFETY",
                new Vector3(-27f, 3.8f, 13.88f), Vector3.zero, yellow, root, 0.020f);

            CreateBox("Polling Logistics Depot", new Vector3(31f, 3f, -3f), new Vector3(16f, 6f, 18f), teal, root);
            CreateWorldLabel(
                "Logistics Depot Sign", "POLLING LOGISTICS DEPOT",
                new Vector3(22.88f, 4.1f, -3f), new Vector3(0f, 90f, 0f), yellow, root, 0.020f);

            CreateBox("Public Convention Plaza", new Vector3(16f, -0.08f, -19f), new Vector3(29f, 0.18f, 10f), white, root);
            CreateBox("Campaign Debate Plaza", new Vector3(0f, -0.07f, 19f), new Vector3(24f, 0.18f, 10f), white, root);

            for (int index = 0; index < 8; index++)
            {
                float x = -30f + index * 8.5f;
                Material seatColor = index % 2 == 0 ? teal : yellow;
                CreateExpansionFlag($"Fictional Seat Flag {index + 1}", new Vector3(x, 0f, -28f), darkStone, seatColor, root);
                CreateWorldLabel(
                    $"Fictional Seat Label {index + 1}", $"SEAT {index + 1}",
                    new Vector3(x, 1.05f, -26.8f), Vector3.zero, darkStone, root, 0.015f);
            }

            for (int index = 0; index < 10; index++)
            {
                float z = -24f + index * 5.6f;
                CreateStreetLamp(
                    $"State Avenue Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -6f : 6f, 0f, z), darkStone, yellow, root);
            }

            CreateTree("State Campus Neem North West", new Vector3(-42f, 0f, 28f), foliage, trunk, root);
            CreateTree("State Campus Neem North East", new Vector3(42f, 0f, 28f), foliage, trunk, root);
            CreateTree("State Campus Neem South West", new Vector3(-42f, 0f, -25f), foliage, trunk, root);
            CreateTree("State Campus Neem South East", new Vector3(42f, 0f, -25f), foliage, trunk, root);
        }

        private static void CreateChapterThirteenLighting()
        {
            GameObject lightObject = new GameObject("State Campaign Morning Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.90f, 0.73f);
            sunlight.intensity = 1.08f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.48f;
            lightObject.transform.rotation = Quaternion.Euler(44f, -28f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.45f, 0.69f, 0.86f);
            RenderSettings.ambientEquatorColor = new Color(0.70f, 0.62f, 0.46f);
            RenderSettings.ambientGroundColor = new Color(0.23f, 0.25f, 0.20f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.73f, 0.82f, 0.87f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 76f;
            RenderSettings.fogEndDistance = 205f;
        }
    }
}

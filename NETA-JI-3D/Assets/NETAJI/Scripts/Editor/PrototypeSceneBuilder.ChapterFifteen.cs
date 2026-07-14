using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterFifteenScene(
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
            GameObject environment = new GameObject("Fictional Forty Seat State Election Campus");
            CreateChapterFifteenEnvironment(
                environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 15 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                15, 100, 30, 100, 100, 59, 218, 88, 67, 85, 59, true,
                65, 87, 5, 85, true, 88, 93, 99, 91, true, 58, 92, 69, 58, true,
                78, 80, 90, 30, 78, true, 78, 90, 92, 89, true,
                80, 89, 98, 82, 6, true, 84, 86, 88, 87, true,
                78, 100, 90, 56, 27, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad Fictional CM Candidate", new Vector3(0f, 0f, -26f), white, white, skin, hair, true);
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
            gameCamera.farClipPlane = 230f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.9f, -31f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterFifteenMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterFifteenLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterFifteenScenePath);
        }

        private static void ConfigureChapterFifteenMission(
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

            GameObject auditor = CreatePerson(
                "State Election Eligibility Auditor", new Vector3(0f, 0f, -21f), white, darkStone, skin, hair, false);
            MissionObjective eligibility = AddObjective(
                auditor,
                "state-election-eligibility",
                "CM mandate eligibility verify karein",
                "Independent Election Auditor",
                "State leader selection, six-seat foothold, district audit and funding separation verified. Forty fictional seats ka mandate campaign rules aur counted result se decide hoga.",
                0, 0, 0, false, 1);
            eligibility.ConfigurePoliticalReward(0, 0, -15);
            eligibility.ConfigureStateElectionReward(5, 10, 8);
            objectives.Add(eligibility);
            labels.Add("Independent auditor se state-election and CM-candidate eligibility verify karein");

            GameObject manifestoLead = CreatePerson(
                "State Manifesto Costing Lead", new Vector3(-12f, 0f, -16f), teal, darkStone, skin, hair, false);
            MissionObjective manifesto = AddObjective(
                manifestoLead,
                "state-manifesto",
                "Costed state manifesto lock karein",
                "Public Finance Research Lead",
                "Education, primary health, safety, jobs, agriculture, cities and local government promises ke costs, funding source, timeline and review metrics public hain.",
                0, 0, 0, false, 1);
            manifesto.ConfigurePoliticalReward(0, 0, -5);
            manifesto.ConfigureStateElectionReward(8, 10, 4);
            objectives.Add(manifesto);
            labels.Add("Seven public priorities ka costed fictional state manifesto publish karein");

            GameObject slateBoard = new GameObject("Forty Seat Candidate Disclosure Wall");
            slateBoard.transform.position = new Vector3(-25f, 2.0f, -7f);
            CreatePrimitiveChild("Slate Screen", PrimitiveType.Cube, slateBoard.transform, Vector3.zero, new Vector3(7.2f, 3.8f, 0.26f), darkStone);
            for (int index = 0; index < 10; index++)
            {
                CreatePrimitiveChild(
                    $"Slate File Group {index + 1}", PrimitiveType.Cube, slateBoard.transform,
                    new Vector3(-2.65f + (index % 5) * 1.32f, 0.72f - (index / 5) * 1.48f, -0.17f),
                    new Vector3(0.85f, 0.64f, 0.06f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective slate = AddObjective(
                slateBoard,
                "forty-seat-slate",
                "Forty-seat slate audit karein",
                "Candidate Disclosure Panel",
                "Forty fictional candidates ke service record, assets, liabilities, conflicts, cases and interviews published. Protected identity and family data scoring se excluded hain.",
                0, 0, 0, false, 2);
            slate.ConfigurePoliticalReward(0, 0, 2);
            slate.ConfigureStateElectionReward(8, 14, 8);
            objectives.Add(slate);
            labels.Add("Forty fictional candidates ka disclosure and service-record audit complete karein");

            GameObject fundingDesk = new GameObject("State Campaign Public Funding Desk");
            fundingDesk.transform.position = new Vector3(-25f, 1.1f, 5f);
            CreatePrimitiveChild("Funding Counter", PrimitiveType.Cube, fundingDesk.transform, Vector3.zero, new Vector3(5.2f, 1.25f, 1.9f), teal);
            CreatePrimitiveChild("Receipt Ledger", PrimitiveType.Cube, fundingDesk.transform, new Vector3(-1.1f, 0.76f, 0f), new Vector3(1.0f, 0.14f, 0.78f), white);
            CreatePrimitiveChild("Expense Ledger", PrimitiveType.Cube, fundingDesk.transform, new Vector3(1.1f, 0.76f, 0f), new Vector3(1.0f, 0.14f, 0.78f), yellow);
            MissionObjective funding = AddObjective(
                fundingDesk,
                "state-election-funding",
                "State campaign funding audit karein",
                "Independent Finance Volunteer",
                "Rs 200 campaign funds capped small donations se receipt-backed aaye. Media, travel and accessibility budgets seat-wise public hain; anonymous large funds reject hue.",
                0, 200, 0, false, 1);
            funding.ConfigurePoliticalReward(0, 0, 1);
            funding.ConfigureStateElectionReward(4, 8, 6);
            objectives.Add(funding);
            labels.Add("Receipt-backed state campaign funds and seat-wise expenses audit karein");

            GameObject shanti = CreatePerson(
                "Shanti Inclusive Ticket Forum", new Vector3(-18f, 0f, 16f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            CreateChapterNineCrowd(
                "Inclusive Candidate Forum", new Vector3(-18f, 0f, 19f), 8,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective forum = AddObjective(
                shanti,
                "inclusive-ticket-forum",
                "Inclusive candidate forum karein",
                "Shanti",
                "Women, youth and under-represented public-service leaders ko equal interview, safety support and written scoring mila. Token promise nahi; final reasons published hain.",
                0, 0, 0, false);
            forum.ConfigurePoliticalReward(0, 6, 2);
            forum.ConfigureStateElectionReward(8, 8, 6);
            objectives.Add(forum);
            labels.Add("Shanti ke saath inclusive and transparent candidate forum complete karein");

            GameObject yatraLead = CreatePerson(
                "State Jan Samvad Coordinator", new Vector3(-3f, 0f, 21f), volunteerDress, darkStone, skin, hair, false);
            CreateChapterNineCrowd(
                "Jan Samvad Yatra Volunteers", new Vector3(-3f, 0f, 24f), 10,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective yatra = AddObjective(
                yatraLead,
                "state-jan-samvad",
                "State jan-samvad yatra karein",
                "Jan Samvad Coordinator",
                "Fictional rural and urban clusters mein costed manifesto, local questions and grievance receipts discuss hue. Convoy limits and public-route permissions followed hain.",
                0, 0, 0, false);
            yatra.ConfigurePoliticalReward(0, 8, 3);
            yatra.ConfigureStateElectionReward(14, 5, 8);
            objectives.Add(yatra);
            labels.Add("Rural-urban jan-samvad yatra with grievance receipts complete karein");

            GameObject strategyStage = new GameObject("State Election Strategy Stage");
            strategyStage.transform.position = new Vector3(16f, 1.2f, 17f);
            CreatePrimitiveChild("Election Strategy Platform", PrimitiveType.Cube, strategyStage.transform, Vector3.zero, new Vector3(7.8f, 1.0f, 4.6f), darkStone);
            CreatePrimitiveChild("Election Strategy Backdrop", PrimitiveType.Cube, strategyStage.transform, new Vector3(0f, 2.05f, 2.05f), new Vector3(7.8f, 3.3f, 0.22f), teal);
            CreateWorldLabel(
                "Election Strategy Label", "FORTY SEATS  /  ISSUES BEFORE IMAGE",
                new Vector3(16f, 3.48f, 18.94f), Vector3.zero, yellow, strategyStage.transform.parent, 0.020f);
            MissionObjective strategy = AddObjective(
                strategyStage,
                "state-election-strategy",
                "State campaign strategy chunein",
                "State Election Committee",
                "Issue-led decentralized campaign local candidates, costed plans and booth dry-runs ko priority deta hai. Rs 180 lagenge, par rules and operations strong rahenge.",
                0, -180, 0, false);
            strategy.ConfigurePoliticalReward(3, 8, 5);
            strategy.ConfigureStateElectionReward(14, 16, 14);
            strategy.ConfigureDecision(
                "state-election-strategy",
                "STATE ELECTION CAMPAIGN",
                "Issue-led campaign costlier and slower hai. Personality-led mega rallies support jaldi badhati hain, par candidate scrutiny, expense control and polling operations weak ho sakte hain.",
                "ISSUE-LED LOCAL CAMPAIGN\nRules +16 / Ops +14",
                "PERSONALITY MEGA RALLIES\nSupport +24 / Power +8",
                "Mega rallies ne crowd aur reach badhayi, lekin candidate airtime, expense checks and two polling rehearsals cut hue. Final compliance correction mandatory hai.",
                0, -60, -10, -8, 8, 3, 14,
                riskyStatewideSupport: 24,
                riskyStatewideCampaignCompliance: -10,
                riskyStatewideElectionOperations: -8);
            objectives.Add(strategy);
            labels.Add("Issue-led local campaign ya personality mega rallies mein decision lein");

            GameObject debateLead = CreatePerson(
                "State Debate Fact Check Editor", new Vector3(26f, 0f, 8f), shirt, trousers, skin, hair, false);
            MissionObjective debate = AddObjective(
                debateLead,
                "state-election-debate",
                "State debate fact-check karein",
                "Independent Debate Editor",
                "Candidates ko equal time, published sources and correction window mila. Azad ke claim par bhi same live fact-check rule apply hua.",
                0, 0, 0, false, 2);
            debate.ConfigurePoliticalReward(0, 0, 3);
            debate.ConfigureStateElectionReward(6, 8, 4);
            objectives.Add(debate);
            labels.Add("Equal-time state debate and live claim correction complete karein");

            GameObject pollingBoard = new GameObject("Forty Seat Polling Operations Board");
            pollingBoard.transform.position = new Vector3(27f, 2.1f, -3f);
            CreatePrimitiveChild("Polling Screen", PrimitiveType.Cube, pollingBoard.transform, Vector3.zero, new Vector3(7.4f, 4.1f, 0.26f), darkStone);
            for (int index = 0; index < 12; index++)
            {
                CreatePrimitiveChild(
                    $"Polling Zone Light {index + 1}", PrimitiveType.Cube, pollingBoard.transform,
                    new Vector3(-2.65f + (index % 6) * 1.05f, 0.78f - (index / 6) * 1.58f, -0.17f),
                    new Vector3(0.62f, 0.62f, 0.06f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective polling = AddObjective(
                pollingBoard,
                "state-polling-operations",
                "Forty-seat polling rehearsal karein",
                "Election Operations Lead",
                "Booth agents, accessibility, queues, route sheets, incident escalation, relief shifts and counting records forty fictional seats par timed dry-run se pass hue.",
                0, 0, 0, false);
            polling.ConfigurePoliticalReward(0, 6, 2);
            polling.ConfigureStateElectionReward(5, 5, 14);
            objectives.Add(polling);
            labels.Add("Forty fictional seats ka polling and counting dry-run pass karein");

            GameObject samrat = CreatePerson(
                "Constable Samrat Election Safety Liaison", new Vector3(25f, 0f, -14f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective safety = AddObjective(
                samrat,
                "state-election-safety",
                "Hate and intimidation response lock karein",
                "Constable Samrat",
                "Public safety helpline, threat evidence, rally separation and rapid correction SOP ready hai. Police neutral rahegi; party workers enforcement nahi karenge.",
                0, 0, 0, false, 2);
            safety.ConfigurePoliticalReward(0, 0, 2);
            safety.ConfigureStateElectionReward(4, 10, 8);
            objectives.Add(safety);
            labels.Add("Samrat ke saath neutral hate, threat and crowd-safety protocol lock karein");

            GameObject complianceLead = CreatePerson(
                "State Election Compliance Officer", new Vector3(7f, 0f, -19f), white, darkStone, skin, hair, false);
            MissionObjective compliance = AddObjective(
                complianceLead,
                "state-election-compliance",
                "Silence and counting compliance lock karein",
                "Independent Compliance Officer",
                "Campaign silence, expense freeze, agent lists, accessibility, complaint appeals and counting-copy chain verified. Identity-based voter profiles store nahi hue.",
                0, 0, 0, false, 2);
            compliance.ConfigurePoliticalReward(0, 0, -4);
            compliance.ConfigureStateElectionReward(2, 7, 10);
            objectives.Add(compliance);
            labels.Add("Campaign silence, expense, agent and counting-copy compliance close karein");

            GameObject resultCentre = new GameObject("Forty Seat State Result Centre");
            resultCentre.transform.position = new Vector3(0f, 2.4f, -11f);
            CreatePrimitiveChild("State Result Wall", PrimitiveType.Cube, resultCentre.transform, Vector3.zero, new Vector3(9.6f, 4.8f, 0.28f), darkStone);
            for (int row = 0; row < 5; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    int seat = row * 8 + column;
                    Material resultColor = seat < 27 ? teal : seat < 33 ? yellow : white;
                    CreatePrimitiveChild(
                        $"State Result Seat {seat + 1}", PrimitiveType.Cube, resultCentre.transform,
                        new Vector3(-3.55f + column * 1.02f, 1.45f - row * 0.72f, -0.18f),
                        new Vector3(0.66f, 0.44f, 0.06f), resultColor);
                }
            }
            CreateWorldLabel(
                "State Result Centre Label", "40-SEAT FICTIONAL STATE RESULT",
                new Vector3(0f, 5.08f, -11.18f), Vector3.zero, yellow, resultCentre.transform.parent, 0.024f);
            MissionObjective result = AddObjective(
                resultCentre,
                "state-election-result",
                "State election result dekhein",
                "Independent State Election Panel",
                "Support, campaign rules, polling operations, leadership record and pressure ka computed forty-seat result ready hai.",
                0, 0, 0, false);
            result.ConfigureStateElection();
            objectives.Add(result);
            labels.Add("Independent panel par vote share, seats and CM mandate result dekhein");

            mission.Configure(
                "Pradesh Ka Janadesh",
                objectives,
                labels,
                "CHAPTER 15 COMPLETE",
                "Forty fictional seats counted. Majority mandate mila aur Azad CM-designate bana.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "STATE MANIFESTO LOCKED", "CAMPAIGN STRATEGY ACTIVE", "POLLING SYSTEM READY" },
                new List<string>
                {
                    "Eligibility, manifesto and candidate slate verified. Clean funding and public campaign next hain.",
                    "Funding, inclusion, jan-samvad and strategy complete. Debate and polling operations next hain.",
                    "Debate, polling and safety protocols complete. Final silence and counting compliance mandate decide karega."
                });
            mission.ConfigureChapter(15, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 15 / PRADESH KA JANADESH",
                "Forty fictional seats. Counted majority ke bina CM kursi nahi milegi.");
        }

        private static void CreateChapterFifteenEnvironment(
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
            CreateBox("State Election Campus Ground", new Vector3(0f, -0.32f, 0f), new Vector3(96f, 0.64f, 70f), sand, root);
            CreateBox("State Election Avenue", new Vector3(0f, 0.02f, -3f), new Vector3(10f, 0.08f, 64f), stone, root);
            CreateBox("Campaign Cross Route", new Vector3(0f, 0.03f, 10f), new Vector3(80f, 0.08f, 7f), stone, root);

            CreateBox("Fictional State Election Headquarters", new Vector3(0f, 3.7f, 31f), new Vector3(34f, 7.4f, 8f), teal, root);
            CreateBox("Election Headquarters Canopy", new Vector3(0f, 7.5f, 28.2f), new Vector3(37f, 0.44f, 3.2f), yellow, root);
            CreateWorldLabel(
                "Election Headquarters Sign", "FORTY-SEAT STATE ELECTION HEADQUARTERS",
                new Vector3(0f, 5.05f, 26.92f), Vector3.zero, white, root, 0.026f);
            CreateWorldLabel(
                "Election Headquarters Motto", "MANIFESTO  /  RULES  /  COUNT",
                new Vector3(0f, 3.80f, 26.90f), Vector3.zero, yellow, root, 0.021f);

            CreateBox("State Manifesto Lab", new Vector3(-34f, 3f, -4f), new Vector3(16f, 6f, 20f), white, root);
            CreateWorldLabel(
                "Manifesto Lab Sign", "COSTED STATE MANIFESTO",
                new Vector3(-25.88f, 4.1f, -4f), new Vector3(0f, -90f, 0f), teal, root, 0.020f);

            CreateBox("Candidate Disclosure Hall", new Vector3(-31f, 2.8f, 21f), new Vector3(22f, 5.6f, 11f), darkStone, root);
            CreateWorldLabel(
                "Candidate Hall Sign", "40 CANDIDATES  /  PUBLIC FILES",
                new Vector3(-31f, 3.9f, 15.38f), Vector3.zero, yellow, root, 0.020f);

            CreateBox("State Polling Depot", new Vector3(34f, 3.1f, -2f), new Vector3(17f, 6.2f, 20f), teal, root);
            CreateWorldLabel(
                "Polling Depot Sign", "POLLING AND COUNTING DEPOT",
                new Vector3(25.38f, 4.2f, -2f), new Vector3(0f, 90f, 0f), yellow, root, 0.020f);

            CreateBox("State Debate Plaza", new Vector3(17f, -0.08f, 19f), new Vector3(31f, 0.18f, 11f), white, root);
            CreateBox("State Counting Plaza", new Vector3(0f, -0.07f, -15f), new Vector3(27f, 0.18f, 10f), white, root);

            for (int index = 0; index < 12; index++)
            {
                float x = -42f + index * 7.6f;
                CreateExpansionFlag(
                    $"State Election Route Flag {index + 1}", new Vector3(x, 0f, -31f),
                    darkStone, index % 3 == 0 ? yellow : teal, root);
            }

            for (int index = 0; index < 11; index++)
            {
                CreateStreetLamp(
                    $"State Election Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -6.5f : 6.5f, 0f, -27f + index * 5.8f), darkStone, yellow, root);
            }

            CreateTree("Election Campus Neem North West", new Vector3(-45f, 0f, 30f), foliage, trunk, root);
            CreateTree("Election Campus Neem North East", new Vector3(45f, 0f, 30f), foliage, trunk, root);
            CreateTree("Election Campus Neem South West", new Vector3(-45f, 0f, -28f), foliage, trunk, root);
            CreateTree("Election Campus Neem South East", new Vector3(45f, 0f, -28f), foliage, trunk, root);
        }

        private static void CreateChapterFifteenLighting()
        {
            GameObject lightObject = new GameObject("State Election Day Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.91f, 0.74f);
            sunlight.intensity = 1.09f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.50f;
            lightObject.transform.rotation = Quaternion.Euler(45f, -32f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.44f, 0.69f, 0.86f);
            RenderSettings.ambientEquatorColor = new Color(0.72f, 0.61f, 0.45f);
            RenderSettings.ambientGroundColor = new Color(0.24f, 0.25f, 0.20f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.73f, 0.82f, 0.88f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 80f;
            RenderSettings.fogEndDistance = 218f;
        }
    }
}

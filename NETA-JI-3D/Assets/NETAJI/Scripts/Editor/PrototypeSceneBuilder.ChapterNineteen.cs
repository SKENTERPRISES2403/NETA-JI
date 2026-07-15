using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterNineteenScene(
            Material sand, Material stone, Material darkStone, Material teal, Material yellow,
            Material white, Material shirt, Material trousers, Material skin, Material hair,
            Material shantiDress, Material volunteerDress, Material policeKhaki,
            Material foliage, Material trunk)
        {
            Scene chapterScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject environment = new GameObject("Fictional First National Election Campus");
            CreateChapterNineteenEnvironment(environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 19 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                19, 100, 0, 100, 100, 72, 440, 93, 67, 85, 59, true,
                65, 87, 5, 85, true, 88, 93, 99, 91, true, 58, 92, 69, 58, true,
                78, 80, 90, 30, 78, true, 78, 90, 92, 89, true,
                80, 89, 98, 82, 6, true, 84, 86, 88, 87, true,
                78, 100, 90, 56, 27, true, 92, 100, 80, 88, true,
                93, 87, 92, 85, 87, true, 96, 100, 96, 90, 18, true,
                84, 100, 92, 49, 42, true, false);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson(
                "Azad First National Candidate", new Vector3(0f, 0f, -31f), white, white, skin, hair, true);
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

            ConfigureChapterNineteenMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterNineteenLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterNineteenScenePath);
        }

        private static void ConfigureChapterNineteenMission(
            MissionController mission, Material shantiDress, Material volunteerDress,
            Material policeKhaki, Material teal, Material yellow, Material white,
            Material shirt, Material trousers, Material darkStone, Material skin, Material hair)
        {
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject eligibility = CreatePerson(
                "National Election Eligibility Officer", new Vector3(0f, 0f, -24f), shirt, darkStone, skin, hair, false);
            MissionObjective eligibilityAudit = AddObjective(
                eligibility, "first-national-eligibility", "National election eligibility verify karein",
                "Independent Election Eligibility Officer",
                "Fictional 100-seat national council ke nomination, regional endorsements, public disclosures and audit deadlines verify hain. Result voters decide karenge.",
                0, 0, 0, false);
            eligibilityAudit.ConfigurePoliticalReward(0, 0, -6);
            eligibilityAudit.ConfigureFirstNationalCampaignReward(0, 8, 4);
            objectives.Add(eligibilityAudit);
            labels.Add("Nomination, endorsements, disclosures and deadlines verify karein");

            GameObject manifestoDesk = new GameObject("National Public Manifesto Desk");
            manifestoDesk.transform.position = new Vector3(-22f, 1.05f, -18f);
            CreatePrimitiveChild("Manifesto Counter", PrimitiveType.Cube, manifestoDesk.transform, Vector3.zero, new Vector3(7.6f, 1.3f, 2.7f), teal);
            CreatePrimitiveChild("Cost Register", PrimitiveType.Cube, manifestoDesk.transform, new Vector3(-1.8f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), white);
            CreatePrimitiveChild("Timeline Register", PrimitiveType.Cube, manifestoDesk.transform, new Vector3(0f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), yellow);
            CreatePrimitiveChild("Correction Register", PrimitiveType.Cube, manifestoDesk.transform, new Vector3(1.8f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), white);
            MissionObjective manifesto = AddObjective(
                manifestoDesk, "costed-national-manifesto", "Costed national manifesto publish karein",
                "National Public Policy Desk",
                "Health, learning, safety, livelihood and fiscal promises ke cost, sequence, limits and correction process public hain. Impossible guarantees remove ki gayi hain.",
                0, 0, 0, false);
            manifesto.ConfigurePoliticalReward(0, 8, 2);
            manifesto.ConfigureFirstNationalCampaignReward(8, 12, 5);
            objectives.Add(manifesto);
            labels.Add("Cost, timeline, limits and corrections ke saath manifesto publish karein");

            GameObject candidates = new GameObject("National Candidate Verification Wall");
            candidates.transform.position = new Vector3(-36f, 2.1f, -4f);
            CreatePrimitiveChild("Candidate Verification Screen", PrimitiveType.Cube, candidates.transform, Vector3.zero, new Vector3(9.4f, 4.2f, 0.28f), darkStone);
            for (int index = 0; index < 10; index++)
            {
                CreatePrimitiveChild($"Candidate File {index + 1}", PrimitiveType.Cube, candidates.transform,
                    new Vector3(-3.4f + (index % 5) * 1.7f, 0.8f - (index / 5) * 1.55f, -0.18f),
                    new Vector3(1.0f, 0.68f, 0.06f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective slate = AddObjective(
                candidates, "national-candidate-slate", "Candidate slate public-check karein",
                "Independent Candidate Verification Panel",
                "Disclosure, conflict, serious-case review, local interview and appeal records sample-checked hain. Fame ya loyalty se standard bypass nahi hua.",
                0, 0, 0, false, 2);
            slate.ConfigurePoliticalReward(0, 8, 2);
            slate.ConfigureFirstNationalCampaignReward(8, 12, 8);
            objectives.Add(slate);
            labels.Add("Candidate disclosure, conflict, interview and appeal records check karein");

            GameObject shanti = CreatePerson(
                "Shanti National Language Desk", new Vector3(-34f, 0f, 14f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            MissionObjective language = AddObjective(
                shanti, "national-language-access", "Shanti ke saath language-access media run karein",
                "Shanti",
                "Manifesto summaries regional languages, easy-read formats and sign support mein available hain. Misleading clipped translation ka correction channel live hai.",
                0, 0, 0, false);
            language.ConfigurePoliticalReward(0, 6, 1);
            language.ConfigureFirstNationalCampaignReward(10, 8, 6);
            objectives.Add(language);
            labels.Add("Multilingual, easy-read and accessible campaign media run karein");

            GameObject yatraLead = CreatePerson(
                "Ground Yatra Network Lead", new Vector3(-18f, 0f, 25f), volunteerDress, trousers, skin, hair, false);
            CreateChapterNineCrowd("Ground Yatra Volunteer Network", new Vector3(-18f, 0f, 29f), 12,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective yatra = AddObjective(
                yatraLead, "national-ground-yatra", "Issue-based national yatra run karein",
                "Ground Yatra Network Lead",
                "Small meetings, door-to-door listening, local issue cards and consent-based follow-up chal raha hai. Crowd size ko vote guarantee nahi maana gaya.",
                0, 0, 0, false);
            yatra.ConfigurePoliticalReward(0, 12, 3);
            yatra.ConfigureFirstNationalCampaignReward(16, 8, 10);
            objectives.Add(yatra);
            labels.Add("Small meetings, listening cards and consent-based follow-up yatra run karein");

            GameObject debate = CreatePerson(
                "Independent National Debate Moderator", new Vector3(0f, 0f, 27f), shirt, darkStone, skin, hair, false);
            MissionObjective debateObjective = AddObjective(
                debate, "national-public-debate", "Independent public debate attend karein",
                "Independent Debate Moderator",
                "Equal time, sourced claims, rebuttal and correction window ke saath fictional debate complete hai. Opponent ko enemy nahi, democratic rival kaha gaya.",
                0, 0, 0, false);
            debateObjective.ConfigurePoliticalReward(0, 4, 2);
            debateObjective.ConfigureFirstNationalCampaignReward(8, 10, 4);
            objectives.Add(debateObjective);
            labels.Add("Equal-time sourced debate aur correction window complete karein");

            GameObject strategyStage = new GameObject("First National Campaign Strategy Stage");
            strategyStage.transform.position = new Vector3(19f, 1.2f, 22f);
            CreatePrimitiveChild("National Strategy Platform", PrimitiveType.Cube, strategyStage.transform, Vector3.zero, new Vector3(9f, 1f, 5f), darkStone);
            CreatePrimitiveChild("National Strategy Backdrop", PrimitiveType.Cube, strategyStage.transform, new Vector3(0f, 2f, 2.2f), new Vector3(9f, 3.4f, 0.24f), teal);
            CreateWorldLabel("National Strategy Label", "ISSUES BEFORE SPECTACLE",
                new Vector3(19f, 3.5f, 24.1f), Vector3.zero, yellow, strategyStage.transform.parent, 0.023f);
            MissionObjective strategy = AddObjective(
                strategyStage, "first-national-campaign-approach", "National campaign strategy chunein",
                "Independent Campaign Standards Council",
                "Issue-first federation slower headlines degi, par compliance aur booth readiness balanced rakhegi. Spectacle convoy support spike de sakta hai, par rules aur evidence weaken ho sakte hain.",
                0, 0, 0, false);
            strategy.ConfigurePoliticalReward(3, 10, 4);
            strategy.ConfigureFirstNationalCampaignReward(14, 14, 14);
            strategy.ConfigureDecision(
                "first-national-campaign-approach",
                "FIRST NATIONAL CAMPAIGN",
                "Issue-first federation ya spectacle convoy: result sirf support se nahi, rules, operations aur national readiness se compute hoga.",
                "ISSUE-FIRST FEDERATION\nRules +14 / Ops +14",
                "SPECTACLE CONVOY\nSupport +30 / Power +8",
                "Convoy ne attention aur support badhaya, lekin fact-check corrections, local permissions and evidence depth par pressure aaya. Final count independent rahega.",
                0, 0, -8, -9, 8, 4, 13,
                riskyNationalCampaignSupport: 30,
                riskyNationalCampaignCompliance: -14,
                riskyNationalElectionOperations: 20);
            objectives.Add(strategy);
            labels.Add("Issue-first federation ya spectacle convoy mein decision lein");

            GameObject fundingDesk = new GameObject("National Election Funding Audit Desk");
            fundingDesk.transform.position = new Vector3(36f, 1.05f, 11f);
            CreatePrimitiveChild("Election Funding Counter", PrimitiveType.Cube, fundingDesk.transform, Vector3.zero, new Vector3(7.6f, 1.3f, 2.7f), white);
            CreatePrimitiveChild("Source Ledger", PrimitiveType.Cube, fundingDesk.transform, new Vector3(-1.8f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), teal);
            CreatePrimitiveChild("Media Ledger", PrimitiveType.Cube, fundingDesk.transform, new Vector3(0f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), yellow);
            CreatePrimitiveChild("Vendor Ledger", PrimitiveType.Cube, fundingDesk.transform, new Vector3(1.8f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), teal);
            MissionObjective funding = AddObjective(
                fundingDesk, "national-election-funding", "Election funding ledger audit karein",
                "Independent Campaign Finance Panel",
                "Sources, spending caps, media invoices, vendor conflicts and corrections public ledger se match hain. Hidden sponsorship reject hui.",
                0, 0, 0, false, 2);
            funding.ConfigurePoliticalReward(0, 0, -3);
            funding.ConfigureFirstNationalCampaignReward(4, 12, 8);
            objectives.Add(funding);
            labels.Add("Sources, caps, media invoices and vendor conflicts audit karein");

            GameObject boothLead = CreatePerson(
                "National Poll Operations Trainer", new Vector3(38f, 0f, -5f), volunteerDress, darkStone, skin, hair, false);
            CreateChapterNineCrowd("Polling Day Volunteer Cohort", new Vector3(38f, 0f, -1f), 10,
                volunteerDress, white, yellow, darkStone, skin, hair);
            MissionObjective booth = AddObjective(
                boothLead, "national-poll-operations", "Polling operations training complete karein",
                "National Poll Operations Trainer",
                "Queue help, accessibility, lawful observer conduct, incident logging and result-form verification train hue. Volunteers voter choice influence nahi karenge.",
                0, 0, 0, false);
            booth.ConfigurePoliticalReward(0, 10, 2);
            booth.ConfigureFirstNationalCampaignReward(8, 8, 16);
            objectives.Add(booth);
            labels.Add("Queue, accessibility, observer and result-form operations train karein");

            GameObject samrat = CreatePerson(
                "Constable Samrat National Poll Safety Liaison", new Vector3(31f, 0f, -20f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective safety = AddObjective(
                samrat, "national-poll-safety", "Samrat ke saath neutral poll-safety drill karein",
                "Constable Samrat",
                "Emergency routes, missing-person help, evidence handover and peaceful queue protection drill complete hai. Police political campaign ka part nahi hai.",
                0, 0, 0, false, 1);
            safety.ConfigurePoliticalReward(0, 0, -2);
            safety.ConfigureFirstNationalCampaignReward(4, 8, 8);
            objectives.Add(safety);
            labels.Add("Neutral emergency, queue and evidence-handover safety drill karein");

            GameObject closingLead = CreatePerson(
                "National Campaign Silence Moderator", new Vector3(13f, 0f, -22f), shirt, trousers, skin, hair, false);
            CreateChapterNineCrowd("National Campaign Closing Meeting", new Vector3(13f, 0f, -27f), 12,
                shirt, volunteerDress, teal, darkStone, skin, hair);
            MissionObjective closing = AddObjective(
                closingLead, "national-campaign-close", "Campaign silence protocol activate karein",
                "Independent Campaign Silence Moderator",
                "Final claims corrected, paid promotion paused, volunteer instructions frozen and result acceptance statement signed hai.",
                0, 0, 0, false);
            closing.ConfigurePoliticalReward(0, 6, 3);
            closing.ConfigureFirstNationalCampaignReward(4, 10, 9);
            objectives.Add(closing);
            labels.Add("Corrections, promotion pause and result-acceptance statement activate karein");

            GameObject countWall = new GameObject("First National Count Wall");
            countWall.transform.position = new Vector3(0f, 2.7f, -10f);
            CreatePrimitiveChild("National Count Screen", PrimitiveType.Cube, countWall.transform, Vector3.zero, new Vector3(13.2f, 5.4f, 0.32f), darkStone);
            for (int index = 0; index < 10; index++)
            {
                CreatePrimitiveChild($"Count Bar {index + 1}", PrimitiveType.Cube, countWall.transform,
                    new Vector3(-5f + index * 1.12f, -1.65f + (index % 5) * 0.34f, -0.20f),
                    new Vector3(0.72f, 1.15f + (index % 5) * 0.34f, 0.07f), index < 4 ? teal : index < 8 ? yellow : white);
            }
            CreateWorldLabel("First National Count Label", "FICTIONAL 100-SEAT NATIONAL COUNT",
                new Vector3(0f, 5.55f, -10.22f), Vector3.zero, yellow, countWall.transform.parent, 0.024f);
            MissionObjective count = AddObjective(
                countWall, "first-national-election-result", "Independent national count dekhein",
                "Independent National Count Panel",
                "Support, campaign rules, operations, readiness, alliance trust and pressure se fictional 100-seat result compute hoga. Haar bhi story progression hai.",
                0, 0, 0, false);
            count.ConfigureFirstNationalElection();
            objectives.Add(count);
            labels.Add("Independent 100-seat fictional national count aur result dekhein");

            mission.Configure(
                "Pehla Rashtriya Faisla",
                objectives,
                labels,
                "CHAPTER 19 COMPLETE",
                "49% vote aur 42/100 fictional seats. Pehla national mandate nahi mila. Azad result sweekar karke responsible opposition aur public service ka sankalp leta hai.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "CLEAN SLATE READY", "CAMPAIGN MODEL LOCKED", "POLL SYSTEM READY" },
                new List<string>
                {
                    "Eligibility, manifesto and candidate slate verified. Language access and yatra next hain.",
                    "Language, ground yatra, debate and campaign strategy ready. Funding and polling operations next hain.",
                    "Funding, poll operations and neutral safety verified. Silence protocol final count unlock karega."
                });
            mission.ConfigureChapter(19, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 19 / PEHLA RASHTRIYA FAISLA",
                "National stage par seva ka record zaroori hai, lekin pehli baar mein mandate milna guaranteed nahi.");
        }

        private static void CreateChapterNineteenEnvironment(
            Transform root, Material sand, Material stone, Material darkStone, Material teal,
            Material yellow, Material white, Material foliage, Material trunk)
        {
            CreateBox("National Election Campus Ground", new Vector3(0f, -0.32f, 0f), new Vector3(108f, 0.64f, 78f), sand, root);
            CreateBox("Democratic Campaign Avenue", new Vector3(0f, 0.02f, -5f), new Vector3(12f, 0.08f, 70f), stone, root);
            CreateBox("National Campaign Crossway", new Vector3(0f, 0.03f, 10f), new Vector3(92f, 0.08f, 7.5f), stone, root);

            CreateBox("Fictional National Counting Hall", new Vector3(0f, 5.2f, 33f), new Vector3(46f, 10.4f, 10f), darkStone, root);
            CreateBox("Hundred Seat Display", new Vector3(0f, 5f, 27.82f), new Vector3(28f, 6.6f, 0.35f), teal, root);
            for (int index = 0; index < 100; index++)
            {
                int column = index % 20;
                int row = index / 20;
                CreateBox($"Fictional Council Seat Light {index + 1}",
                    new Vector3(-12.8f + column * 1.35f, 2.8f + row * 1.05f, 27.58f),
                    new Vector3(0.78f, 0.62f, 0.10f), index < 42 ? yellow : index < 91 ? white : teal, root);
            }
            CreateWorldLabel("National Counting Hall Sign", "FICTIONAL 100-SEAT NATIONAL COUNCIL",
                new Vector3(0f, 8.65f, 27.54f), Vector3.zero, yellow, root, 0.028f);

            CreateBox("Manifesto And Media Pavilion", new Vector3(-41f, 3.2f, -13f), new Vector3(18f, 6.4f, 21f), teal, root);
            CreateWorldLabel("Manifesto Pavilion Sign", "MANIFESTO + MEDIA",
                new Vector3(-31.88f, 4.25f, -13f), new Vector3(0f, -90f, 0f), yellow, root, 0.022f);
            CreateBox("Candidate Verification Pavilion", new Vector3(-41f, 3.1f, 16f), new Vector3(18f, 6.2f, 20f), white, root);
            CreateWorldLabel("Candidate Pavilion Sign", "CANDIDATE VERIFICATION",
                new Vector3(-31.88f, 4.2f, 16f), new Vector3(0f, -90f, 0f), teal, root, 0.021f);
            CreateBox("Public Debate Pavilion", new Vector3(41f, 3.1f, 16f), new Vector3(18f, 6.2f, 20f), teal, root);
            CreateWorldLabel("Public Debate Sign", "PUBLIC DEBATE + FACT CHECK",
                new Vector3(31.88f, 4.2f, 16f), new Vector3(0f, 90f, 0f), yellow, root, 0.020f);
            CreateBox("Polling Operations Pavilion", new Vector3(41f, 3.2f, -13f), new Vector3(18f, 6.4f, 21f), white, root);
            CreateWorldLabel("Polling Operations Sign", "POLLING OPERATIONS",
                new Vector3(31.88f, 4.25f, -13f), new Vector3(0f, 90f, 0f), teal, root, 0.022f);

            CreateBox("Campaign Closing Plaza", new Vector3(10f, -0.07f, -25f), new Vector3(30f, 0.18f, 13f), white, root);
            CreateBox("Campaign Closing Stage", new Vector3(10f, 0.65f, -34f), new Vector3(20f, 1.3f, 6f), darkStone, root);
            CreateBox("Result Acceptance Backdrop", new Vector3(10f, 3.4f, -36.5f), new Vector3(20f, 5.5f, 0.35f), teal, root);
            CreateWorldLabel("Result Acceptance Sign", "RESULT ACCEPTANCE  /  SEVA JAARI",
                new Vector3(10f, 5.35f, -36.28f), Vector3.zero, yellow, root, 0.024f);

            for (int index = 0; index < 14; index++)
            {
                float flagX = -52f + index * 8f;
                if (Mathf.Abs(flagX) < 5f)
                {
                    continue;
                }
                CreateExpansionFlag($"Campaign Route Flag {index + 1}", new Vector3(flagX, 0f, -36f),
                    darkStone, index % 3 == 0 ? yellow : index % 3 == 1 ? teal : white, root);
            }
            for (int index = 0; index < 12; index++)
            {
                CreateStreetLamp($"Democratic Avenue Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -7.5f : 7.5f, 0f, -29f + index * 5.8f), darkStone, yellow, root);
            }

            CreateTree("Election Neem North West", new Vector3(-51f, 0f, 35f), foliage, trunk, root);
            CreateTree("Election Neem North East", new Vector3(51f, 0f, 35f), foliage, trunk, root);
            CreateTree("Election Neem South West", new Vector3(-51f, 0f, -33f), foliage, trunk, root);
            CreateTree("Election Neem South East", new Vector3(51f, 0f, -33f), foliage, trunk, root);
        }

        private static void CreateChapterNineteenLighting()
        {
            GameObject lightObject = new GameObject("National Count Evening Light");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.87f, 0.69f);
            sunlight.intensity = 1.10f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.50f;
            lightObject.transform.rotation = Quaternion.Euler(42f, -36f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.43f, 0.64f, 0.84f);
            RenderSettings.ambientEquatorColor = new Color(0.75f, 0.57f, 0.39f);
            RenderSettings.ambientGroundColor = new Color(0.23f, 0.23f, 0.21f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.74f, 0.78f, 0.82f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 90f;
            RenderSettings.fogEndDistance = 238f;
        }
    }
}

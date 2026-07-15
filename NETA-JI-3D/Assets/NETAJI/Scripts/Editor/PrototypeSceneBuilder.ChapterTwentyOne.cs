using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterTwentyOneScene(
            Material sand, Material stone, Material darkStone, Material teal, Material yellow,
            Material white, Material shirt, Material trousers, Material skin, Material hair,
            Material shantiDress, Material volunteerDress, Material policeKhaki,
            Material foliage, Material trunk)
        {
            Scene chapterScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject environment = new GameObject("Fictional Second National Election Campus");
            CreateChapterTwentyOneEnvironment(environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 21 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                21, 100, 0, 100,
                proof: 100, power: 78, team: 534, pressure: 85,
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
                secondNationalVoteShare: 55, secondNationalSeatsWon: 56, secondNationalElectionContested: true, primeMinisterElected: true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson(
                "Azad National Alliance Candidate", new Vector3(0f, 0f, -31f), white, white, skin, hair, true);
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

            ConfigureChapterTwentyOneMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterTwentyOneLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterTwentyOneScenePath);
        }

        private static void ConfigureChapterTwentyOneMission(
            MissionController mission, Material shantiDress, Material volunteerDress,
            Material policeKhaki, Material teal, Material yellow, Material white,
            Material shirt, Material trousers, Material darkStone, Material skin, Material hair)
        {
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject eligibility = CreatePerson(
                "Second National Eligibility Auditor", new Vector3(0f, 0f, -24f), shirt, darkStone, skin, hair, false);
            MissionObjective eligibilityAudit = AddObjective(
                eligibility, "second-national-eligibility", "Second national eligibility audit sign karein",
                "Independent National Eligibility Auditor",
                "First result acceptance, five-year service, alliance renewal, policy corrections, disclosures and deadlines verified hain. Comeback automatic entitlement nahi hai.",
                0, 0, 0, false);
            eligibilityAudit.ConfigurePoliticalReward(0, 0, -4);
            eligibilityAudit.ConfigureSecondNationalCampaignReward(4, 10, 4);
            objectives.Add(eligibilityAudit);
            labels.Add("Result acceptance, comeback record, disclosures aur deadlines audit karein");

            GameObject lessonsBoard = new GameObject("First Election Lessons Board");
            lessonsBoard.transform.position = new Vector3(-23f, 2.1f, -18f);
            CreatePrimitiveChild("Lessons Public Screen", PrimitiveType.Cube, lessonsBoard.transform,
                Vector3.zero, new Vector3(9.2f, 4.2f, 0.28f), darkStone);
            for (int index = 0; index < 8; index++)
            {
                CreatePrimitiveChild($"Correction Note {index + 1}", PrimitiveType.Cube, lessonsBoard.transform,
                    new Vector3(-3.2f + (index % 4) * 2.1f, 0.75f - (index / 4) * 1.55f, -0.18f),
                    new Vector3(1.25f, 0.72f, 0.06f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective lessons = AddObjective(
                lessonsBoard, "first-election-lessons", "First-election lessons public karein",
                "Independent Campaign Learning Panel",
                "Low-reach areas, unclear promises, volunteer gaps, correction delays and accessible-media failures public note mein documented hain. Blame list nahi banayi gayi.",
                0, 0, 0, false, 1);
            lessons.ConfigureSecondNationalCampaignReward(6, 6, 4);
            objectives.Add(lessons);
            labels.Add("First-election gaps, corrections aur measurable lessons publish karein");

            GameObject manifesto = new GameObject("Second National Costed Manifesto Desk");
            manifesto.transform.position = new Vector3(-37f, 1.2f, -4f);
            CreatePrimitiveChild("Manifesto Counter", PrimitiveType.Cube, manifesto.transform,
                Vector3.zero, new Vector3(8.6f, 1.2f, 3.2f), white);
            for (int index = 0; index < 5; index++)
            {
                CreatePrimitiveChild($"Manifesto Ledger {index + 1}", PrimitiveType.Cube, manifesto.transform,
                    new Vector3(-3f + index * 1.5f, 0.78f, 0f), new Vector3(1.0f, 0.14f, 0.9f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective manifestoObjective = AddObjective(
                manifesto, "second-national-manifesto", "Updated costed manifesto verify karein",
                "Independent Fiscal And Policy Panel",
                "Health, learning, safety, livelihood, federal finance and institutional reform promises ke targets, costs, funding source and review dates public hain.",
                0, 0, 0, false);
            manifestoObjective.ConfigurePoliticalReward(0, 4, 0);
            manifestoObjective.ConfigureSecondNationalCampaignReward(8, 8, 4);
            objectives.Add(manifestoObjective);
            labels.Add("Targets, costs, funding sources aur review dates wala manifesto verify karein");

            GameObject coalitionLead = CreatePerson(
                "Federal Coalition Contract Convenor", new Vector3(-35f, 0f, 15f), volunteerDress, trousers, skin, hair, false);
            CreateChapterNineCrowd("Federal Coalition Delegates", new Vector3(-35f, 0f, 20f), 12,
                volunteerDress, shirt, yellow, darkStone, skin, hair);
            MissionObjective coalition = AddObjective(
                coalitionLead, "federal-coalition-contract", "Federal coalition contract sign karein",
                "Federal Coalition Convenor",
                "Shared minimum programme ke saath regional autonomy, dissent procedure, cabinet criteria, finance disclosure and coalition exit rules written hain.",
                0, 0, 0, false);
            coalition.ConfigurePoliticalReward(0, 8, 1);
            coalition.ConfigureSecondNationalCampaignReward(10, 8, 6);
            objectives.Add(coalition);
            labels.Add("Regional autonomy, dissent, cabinet aur exit rules ka contract sign karein");

            GameObject shanti = CreatePerson(
                "Shanti Accessible Campaign Lead", new Vector3(-18f, 0f, 26f), shantiDress, darkStone, skin, hair, true);
            CreateChapterNineCrowd("Accessible Campaign Volunteers", new Vector3(-18f, 0f, 30f), 10,
                volunteerDress, white, teal, darkStone, skin, hair);
            MissionObjective access = AddObjective(
                shanti, "second-campaign-access", "Shanti ke accessible campaign desk ko approve karein",
                "Shanti",
                "Local-language summaries, easy-read cards, sign-supported clips, captioned audio, disability access and correction links rollout ke liye ready hain.",
                0, 0, 0, false);
            access.ConfigurePoliticalReward(0, 6, 0);
            access.ConfigureSecondNationalCampaignReward(8, 8, 6);
            objectives.Add(access);
            labels.Add("Local-language, easy-read, captioned aur accessible media approve karein");

            GameObject firewall = CreatePerson(
                "Public Service Campaign Firewall Auditor", new Vector3(0f, 0f, 27f), shirt, darkStone, skin, hair, false);
            MissionObjective firewallObjective = AddObjective(
                firewall, "service-vote-firewall", "Seva aur vote ke beech firewall verify karein",
                "Independent Public Service Ethics Auditor",
                "Helpers, relief lists, student records and health referrals campaign database se separated hain. Service beneficiary se vote, photo ya membership nahi maangi jayegi.",
                0, 0, 0, false);
            firewallObjective.ConfigurePoliticalReward(0, 0, -2);
            firewallObjective.ConfigureSecondNationalCampaignReward(4, 10, 6);
            objectives.Add(firewallObjective);
            labels.Add("Service records, beneficiary privacy aur campaign firewall verify karein");

            GameObject strategyStage = new GameObject("Second National Campaign Strategy Stage");
            strategyStage.transform.position = new Vector3(19f, 1.2f, 22f);
            CreatePrimitiveChild("Second Campaign Platform", PrimitiveType.Cube, strategyStage.transform,
                Vector3.zero, new Vector3(9f, 1f, 5f), darkStone);
            CreatePrimitiveChild("Second Campaign Backdrop", PrimitiveType.Cube, strategyStage.transform,
                new Vector3(0f, 2f, 2.2f), new Vector3(9f, 3.4f, 0.24f), teal);
            CreateWorldLabel("Second Campaign Strategy Label", "SUNO  /  SAMJHO  /  SAATH CHALO",
                new Vector3(19f, 3.5f, 24.1f), Vector3.zero, yellow, strategyStage.transform.parent, 0.021f);
            MissionObjective strategy = AddObjective(
                strategyStage, "second-national-campaign-approach", "Second national campaign strategy chunein",
                "Independent Campaign Standards Council",
                "Listening coalition support, compliance and polling preparation ko balance karegi. Massive comeback wave attention badhayegi, lekin corrections, permissions and partner trust par pressure dalegi.",
                0, 0, 0, false);
            strategy.ConfigurePoliticalReward(3, 8, 3);
            strategy.ConfigureSecondNationalCampaignReward(14, 12, 12);
            strategy.ConfigureDecision(
                "second-national-campaign-approach",
                "SECOND NATIONAL CAMPAIGN",
                "Listening coalition ya massive comeback wave: PM mandate vote ke saath rules, operations aur 51-seat majority se compute hoga.",
                "LISTENING COALITION\nRules +12 / Ops +12",
                "MASSIVE COMEBACK WAVE\nSupport +26 / Power +8",
                "Wave se attention aur rallies badhi, lekin fact-check corrections, local permissions and evidence depth weak hui. Independent count phir bhi binding rahega.",
                0, 0, -8, -6, 8, 4, 14,
                riskySecondNationalCampaignSupport: 26,
                riskySecondNationalCampaignCompliance: -22,
                riskySecondNationalElectionOperations: 0);
            objectives.Add(strategy);
            labels.Add("Listening coalition ya massive comeback wave mein decision lein");

            GameObject debate = CreatePerson(
                "Second National Debate Moderator", new Vector3(35f, 0f, 15f), shirt, darkStone, skin, hair, false);
            MissionObjective debateObjective = AddObjective(
                debate, "second-national-debate", "Independent PM-candidate debate attend karein",
                "Independent National Debate Moderator",
                "Equal time, sourced claims, coalition questions, rebuttal, correction window and public transcript ke saath fictional debate complete hai.",
                0, 0, 0, false);
            debateObjective.ConfigurePoliticalReward(0, 4, 2);
            debateObjective.ConfigureSecondNationalCampaignReward(8, 8, 4);
            objectives.Add(debateObjective);
            labels.Add("Equal-time debate, coalition questions aur correction window complete karein");

            GameObject fundingDesk = new GameObject("Second National Funding Audit Desk");
            fundingDesk.transform.position = new Vector3(38f, 1.05f, -4f);
            CreatePrimitiveChild("Second Campaign Funding Counter", PrimitiveType.Cube, fundingDesk.transform,
                Vector3.zero, new Vector3(7.6f, 1.3f, 2.7f), white);
            CreatePrimitiveChild("Donation Ledger", PrimitiveType.Cube, fundingDesk.transform,
                new Vector3(-1.8f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), teal);
            CreatePrimitiveChild("Media Ledger", PrimitiveType.Cube, fundingDesk.transform,
                new Vector3(0f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), yellow);
            CreatePrimitiveChild("Coalition Ledger", PrimitiveType.Cube, fundingDesk.transform,
                new Vector3(1.8f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), teal);
            MissionObjective funding = AddObjective(
                fundingDesk, "second-national-funding", "Second-campaign funding ledger audit karein",
                "Independent Campaign Finance Panel",
                "Donation sources, coalition shares, spending caps, media invoices, vendors and corrections public ledger se match hain. Hidden sponsorship reject hui.",
                0, 0, 0, false, 2);
            funding.ConfigurePoliticalReward(0, 0, -2);
            funding.ConfigureSecondNationalCampaignReward(4, 10, 6);
            objectives.Add(funding);
            labels.Add("Donations, coalition shares, caps, invoices aur vendors audit karein");

            GameObject samrat = CreatePerson(
                "Constable Samrat National Poll Safety Liaison Two", new Vector3(34f, 0f, -20f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            CreateChapterNineCrowd("Second Poll Operations Cohort", new Vector3(34f, 0f, -25f), 12,
                volunteerDress, white, yellow, darkStone, skin, hair);
            MissionObjective polling = AddObjective(
                samrat, "second-national-poll-operations", "Neutral polling operations drill complete karein",
                "Constable Samrat",
                "Accessibility, queue safety, lawful observers, incident records, emergency routes and result-form handover train hue. Police voter choice ya campaign mein shamil nahi hai.",
                0, 0, 0, false);
            polling.ConfigurePoliticalReward(0, 10, 1);
            polling.ConfigureSecondNationalCampaignReward(6, 8, 16);
            objectives.Add(polling);
            labels.Add("Accessibility, neutral safety, observers aur result-form operations train karein");

            GameObject silenceLead = CreatePerson(
                "Second Campaign Silence Moderator", new Vector3(13f, 0f, -22f), shirt, trousers, skin, hair, false);
            CreateChapterNineCrowd("Second Campaign Closing Meeting", new Vector3(13f, 0f, -27f), 12,
                shirt, volunteerDress, teal, darkStone, skin, hair);
            MissionObjective silence = AddObjective(
                silenceLead, "second-campaign-silence", "Campaign silence aur result protocol activate karein",
                "Independent Campaign Silence Moderator",
                "Final claims corrected, paid promotion paused, volunteer instructions frozen, coalition result statement signed and peaceful transition teams ready hain.",
                0, 0, 0, false);
            silence.ConfigurePoliticalReward(0, 4, -2);
            silence.ConfigureSecondNationalCampaignReward(4, 10, 8);
            objectives.Add(silence);
            labels.Add("Corrections, promotion pause, volunteer freeze aur result protocol activate karein");

            GameObject countWall = new GameObject("Second National Count And Mandate Wall");
            countWall.transform.position = new Vector3(0f, 2.7f, -10f);
            CreatePrimitiveChild("Second National Count Screen", PrimitiveType.Cube, countWall.transform,
                Vector3.zero, new Vector3(13.2f, 5.4f, 0.32f), darkStone);
            for (int index = 0; index < 10; index++)
            {
                CreatePrimitiveChild($"Second Count Bar {index + 1}", PrimitiveType.Cube, countWall.transform,
                    new Vector3(-5f + index * 1.12f, -1.65f + (index % 5) * 0.34f, -0.20f),
                    new Vector3(0.72f, 1.15f + (index % 5) * 0.34f, 0.07f), index < 6 ? teal : index < 9 ? yellow : white);
            }
            CreateWorldLabel("Second Count Label", "FICTIONAL 100-SEAT NATIONAL MANDATE",
                new Vector3(0f, 5.55f, -10.22f), Vector3.zero, yellow, countWall.transform.parent, 0.022f);
            MissionObjective count = AddObjective(
                countWall, "second-national-election-result", "Independent second national count dekhein",
                "Independent National Count Panel",
                "Comeback score, support, rules, operations, renewed alliances, policy corrections, public record and pressure se fictional 100-seat result compute hoga.",
                0, 0, 0, false);
            count.ConfigureSecondNationalElection();
            objectives.Add(count);
            labels.Add("Independent fictional 100-seat count aur PM mandate dekhein");

            mission.Configure(
                "Janata Ka Rashtriya Janadesh",
                objectives,
                labels,
                "CHAPTER 21 COMPLETE",
                "55% vote aur 56/100 fictional seats. Majority earned; elected coalition ne Azad ko Prime Minister-designate chuna. Agla chapter oath se pehle cabinet, ethics aur pehle 100 din ki taiyari hai.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "LESSONS APPLIED", "CAMPAIGN MODEL LOCKED", "POLL SYSTEM READY" },
                new List<string>
                {
                    "Eligibility, first-election lessons and updated manifesto verified hain. Coalition and accessibility next hain.",
                    "Coalition contract, accessibility, service firewall and campaign strategy ready hain. Debate and finance next hain.",
                    "Debate, finance and neutral polling operations verified hain. Silence protocol independent count unlock karega."
                });
            mission.ConfigureChapter(21, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 21 / JANATA KA RASHTRIYA JANADESH",
                "Paanch saal ki opposition service ke baad Azad dusri baar national mandate maangta hai. Is baar bhi majority earned hogi, scripted nahi.");
        }

        private static void CreateChapterTwentyOneEnvironment(
            Transform root, Material sand, Material stone, Material darkStone, Material teal,
            Material yellow, Material white, Material foliage, Material trunk)
        {
            CreateBox("Second National Election Campus Ground", new Vector3(0f, -0.32f, 0f), new Vector3(108f, 0.64f, 78f), sand, root);
            CreateBox("Mandate Avenue", new Vector3(0f, 0.02f, -5f), new Vector3(12f, 0.08f, 70f), stone, root);
            CreateBox("Federal Campaign Crossway", new Vector3(0f, 0.03f, 10f), new Vector3(92f, 0.08f, 7.5f), stone, root);

            CreateBox("Second National Counting Hall", new Vector3(0f, 5.2f, 33f), new Vector3(46f, 10.4f, 10f), darkStone, root);
            CreateBox("Second Hundred Seat Display", new Vector3(0f, 5f, 27.82f), new Vector3(28f, 6.6f, 0.35f), teal, root);
            for (int index = 0; index < 100; index++)
            {
                int column = index % 20;
                int row = index / 20;
                CreateBox($"Second Fictional Council Seat Light {index + 1}",
                    new Vector3(-12.8f + column * 1.35f, 2.8f + row * 1.05f, 27.58f),
                    new Vector3(0.78f, 0.62f, 0.10f), index < 56 ? yellow : index < 98 ? white : teal, root);
            }
            CreateWorldLabel("Second Counting Hall Sign", "56 / 100  /  MAJORITY EARNED",
                new Vector3(0f, 8.65f, 27.54f), Vector3.zero, yellow, root, 0.028f);

            CreateBox("Lessons And Manifesto Pavilion", new Vector3(-41f, 3.2f, -13f), new Vector3(18f, 6.4f, 21f), teal, root);
            CreateWorldLabel("Lessons Pavilion Sign", "LESSONS + MANIFESTO",
                new Vector3(-31.88f, 4.25f, -13f), new Vector3(0f, -90f, 0f), yellow, root, 0.021f);
            CreateBox("Federal Coalition Pavilion", new Vector3(-41f, 3.1f, 16f), new Vector3(18f, 6.2f, 20f), white, root);
            CreateWorldLabel("Federal Coalition Sign", "FEDERAL COALITION",
                new Vector3(-31.88f, 4.2f, 16f), new Vector3(0f, -90f, 0f), teal, root, 0.021f);
            CreateBox("Debate And Accessibility Pavilion", new Vector3(41f, 3.1f, 16f), new Vector3(18f, 6.2f, 20f), teal, root);
            CreateWorldLabel("Debate Pavilion Sign", "DEBATE + ACCESSIBILITY",
                new Vector3(31.88f, 4.2f, 16f), new Vector3(0f, 90f, 0f), yellow, root, 0.019f);
            CreateBox("Funding And Polling Pavilion", new Vector3(41f, 3.2f, -13f), new Vector3(18f, 6.4f, 21f), white, root);
            CreateWorldLabel("Polling Pavilion Sign", "FUNDING + POLLING",
                new Vector3(31.88f, 4.25f, -13f), new Vector3(0f, 90f, 0f), teal, root, 0.020f);

            CreateBox("National Mandate Plaza", new Vector3(10f, -0.07f, -25f), new Vector3(30f, 0.18f, 13f), white, root);
            CreateBox("Prime Minister Designate Stage", new Vector3(10f, 0.65f, -34f), new Vector3(20f, 1.3f, 6f), darkStone, root);
            CreateBox("Mandate Ceremony Backdrop", new Vector3(10f, 3.4f, -36.5f), new Vector3(20f, 5.5f, 0.35f), teal, root);
            CreateWorldLabel("Mandate Ceremony Sign", "JANADESH  /  SEVA KI ZIMMEDARI",
                new Vector3(10f, 5.35f, -36.28f), Vector3.zero, yellow, root, 0.023f);
            for (int index = 0; index < 7; index++)
            {
                CreateExpansionFlag($"Coalition Mandate Flag {index + 1}",
                    new Vector3(-2f + index * 4f, 0f, -37f), darkStone,
                    index % 3 == 0 ? teal : index % 3 == 1 ? yellow : white, root);
            }

            for (int index = 0; index < 12; index++)
            {
                CreateStreetLamp($"Mandate Avenue Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -7.5f : 7.5f, 0f, -29f + index * 5.8f), darkStone, yellow, root);
            }
            CreateTree("Mandate Neem North West", new Vector3(-51f, 0f, 35f), foliage, trunk, root);
            CreateTree("Mandate Neem North East", new Vector3(51f, 0f, 35f), foliage, trunk, root);
            CreateTree("Mandate Neem South West", new Vector3(-51f, 0f, -33f), foliage, trunk, root);
            CreateTree("Mandate Neem South East", new Vector3(51f, 0f, -33f), foliage, trunk, root);
        }

        private static void CreateChapterTwentyOneLighting()
        {
            GameObject lightObject = new GameObject("National Mandate Sunrise Light");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.90f, 0.73f);
            sunlight.intensity = 1.14f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.50f;
            lightObject.transform.rotation = Quaternion.Euler(39f, -32f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.49f, 0.70f, 0.88f);
            RenderSettings.ambientEquatorColor = new Color(0.82f, 0.65f, 0.46f);
            RenderSettings.ambientGroundColor = new Color(0.25f, 0.25f, 0.22f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.79f, 0.82f, 0.85f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 94f;
            RenderSettings.fogEndDistance = 242f;
        }
    }
}

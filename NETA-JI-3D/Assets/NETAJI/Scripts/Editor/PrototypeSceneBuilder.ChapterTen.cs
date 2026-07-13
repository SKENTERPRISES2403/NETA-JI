using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterTenScene(
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
            GameObject environment = new GameObject("Fictional Prayagraj Assembly Election District");
            CreateChapterTenEnvironment(
                environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 10 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                10, 100, 50, 100, 79, 40, 99, 56, 67, 85, 59, true,
                65, 87, 5, 85, true, 88, 93, 99, 91, true, 58, 92, 69, 58, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad MLA Candidate", new Vector3(0f, 0f, -21f), white, white, skin, hair, true);
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
            gameCamera.farClipPlane = 200f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.7f, -26f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterTenMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white, shirt, trousers, darkStone, skin, hair);
            CreateAssemblyElectionLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterTenScenePath);
        }

        private static void ConfigureChapterTenMission(
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

            GameObject returningTrainer = CreatePerson("Independent Returning Trainer", new Vector3(0f, 0f, -17f), white, darkStone, skin, hair, false);
            MissionObjective nomination = AddObjective(returningTrainer, "candidate-oath", "Candidacy pledge sign karein", "Independent Returning Trainer",
                "Nomination accepted. Lawful spending, truthful disclosures, no hate speech, no inducement aur polling-day silence rules written pledge ka hissa hain.",
                0, 0, 0, false);
            nomination.ConfigurePoliticalReward(0, 0, 1);
            nomination.ConfigureAssemblyCampaignReward(4, 8, 2);
            objectives.Add(nomination);
            labels.Add("Independent trainer ke saamne lawful candidacy pledge sign karein");

            GameObject economist = CreatePerson("Manifesto Costing Volunteer", new Vector3(-12f, 0f, -14f), teal, darkStone, skin, hair, false);
            MissionObjective manifesto = AddObjective(economist, "manifesto-costing", "Manifesto cost publish karein", "Manifesto Costing Volunteer",
                "Water, clinics, schools, jobs aur safety promises ke funding source, annual cost aur delivery date public sheet par hain. Free promise bina budget line ke nahi jayega.",
                0, -20, 0, false, 3);
            manifesto.ConfigureAssemblyCampaignReward(5, 12, 3);
            objectives.Add(manifesto);
            labels.Add("Five measurable promises ka cost aur funding source publish karein");

            GameObject ruralLead = CreatePerson("Rural Padyatra Lead", new Vector3(-21f, 0f, -7f), volunteerDress, darkStone, skin, hair, false);
            MissionObjective ruralRoute = AddObjective(ruralLead, "rural-padyatra", "Rural issue route complete karein", "Rural Padyatra Lead",
                "Canal schedule, crop claim, school transport aur health sub-centre ke signed issue logs ready hain. Vote ka wada nahi, follow-up docket har gaon ko milega.",
                0, 0, 0, false);
            ruralRoute.ConfigurePoliticalReward(0, 2, 1);
            ruralRoute.ConfigureAssemblyReward(4, 3, 2);
            ruralRoute.ConfigureAssemblyCampaignReward(8, 4, 3);
            objectives.Add(ruralRoute);
            labels.Add("Rural padyatra mein issue logs aur follow-up dockets complete karein");

            GameObject shanti = CreatePerson("Shanti Urban Town Hall Lead", new Vector3(-18f, 0f, 3f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            MissionObjective urbanHall = AddObjective(shanti, "urban-town-hall", "Urban town hall karein", "Shanti",
                "Jobs, rent paperwork, bus-stop safety aur clinic access par women aur youth ko pehla mic slot milega. Answer na ho to hum deadline ke saath pending bolenge.",
                0, 0, 0, false);
            urbanHall.ConfigurePoliticalReward(0, 2, 0);
            urbanHall.ConfigureAssemblyReward(4, 3, 2);
            urbanHall.ConfigureAssemblyCampaignReward(8, 4, 3);
            objectives.Add(urbanHall);
            labels.Add("Shanti ke saath women-youth led urban town hall complete karein");

            GameObject legalTrainer = CreatePerson("Campaign Law Trainer", new Vector3(-12f, 0f, 12f), white, darkStone, skin, hair, false);
            CreateChapterNineCrowd("Trained Volunteer Batch", new Vector3(-12f, 0f, 15f), 6, volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective training = AddObjective(legalTrainer, "volunteer-training", "Volunteer law drill karein", "Campaign Law Trainer",
                "Consent before photos, no cash or gifts, no protected-identity profiling, expense receipt same day, aur complaint escalation chain sab volunteers practice karenge.",
                0, 0, 0, false);
            training.ConfigurePoliticalReward(0, 5, 0);
            training.ConfigureAssemblyReward(0, 2, 4);
            training.ConfigureAssemblyCampaignReward(0, 10, 10);
            objectives.Add(training);
            labels.Add("Volunteer batch ko consent, spending aur anti-hate rules train karein");

            GameObject factChecker = new GameObject("Public Fact Check Screen");
            factChecker.transform.position = new Vector3(0f, 1.45f, 15f);
            CreatePrimitiveChild("Screen", PrimitiveType.Cube, factChecker.transform, Vector3.zero, new Vector3(4.6f, 2.6f, 0.22f), darkStone);
            CreatePrimitiveChild("Source Panel", PrimitiveType.Cube, factChecker.transform, new Vector3(-1.2f, 0f, -0.16f), new Vector3(1.5f, 1.6f, 0.05f), teal);
            CreatePrimitiveChild("Correction Panel", PrimitiveType.Cube, factChecker.transform, new Vector3(1.2f, 0f, -0.16f), new Vector3(1.5f, 1.6f, 0.05f), yellow);
            MissionObjective factCheck = AddObjective(factChecker, "rumour-fact-check", "Rumour fact-check publish karein", "Independent Local Journalist",
                "Edited clip ka full source, timestamp aur correction public hai. Opponent par personal attack nahi; false claim ko evidence se answer karke archive karenge.",
                0, 0, 0, false, 3);
            factCheck.ConfigurePoliticalReward(0, 0, 3);
            factCheck.ConfigureAssemblyCampaignReward(5, 6, 3);
            objectives.Add(factCheck);
            labels.Add("Edited rumour ka source-backed correction publish karein");

            GameObject debateModerator = CreatePerson("Assembly Debate Moderator", new Vector3(12f, 0f, 13f), shirt, trousers, skin, hair, false);
            MissionObjective debate = AddObjective(debateModerator, "assembly-debate", "Public debate complete karein", "Assembly Debate Moderator",
                "Each candidate gets equal time: ward record, jobs plan, health budget aur water timeline. Personal life aur identity attacks cut honge; unsourced number challenge hoga.",
                0, 0, 0, false);
            debate.ConfigurePoliticalReward(1, 0, 2);
            debate.ConfigureAssemblyReward(3, 3, 2);
            debate.ConfigureAssemblyCampaignReward(7, 6, 4);
            objectives.Add(debate);
            labels.Add("Equal-time public debate mein records aur costed plan defend karein");

            GameObject strategyStage = new GameObject("Final Campaign Strategy Stage");
            strategyStage.transform.position = new Vector3(20f, 1.1f, 7f);
            CreatePrimitiveChild("Stage", PrimitiveType.Cube, strategyStage.transform, Vector3.zero, new Vector3(6.2f, 1.0f, 3.6f), darkStone);
            CreatePrimitiveChild("Backdrop", PrimitiveType.Cube, strategyStage.transform, new Vector3(0f, 1.8f, 1.55f), new Vector3(6.2f, 2.7f, 0.20f), teal);
            CreateWorldLabel("Final Strategy Label", "ISSUES  /  RECEIPTS  /  RESPECT", new Vector3(20f, 3.0f, 8.44f), Vector3.zero, yellow, strategyStage.transform.parent, 0.020f);
            MissionObjective strategy = AddObjective(strategyStage, "assembly-campaign-strategy", "Final campaign strategy chunein", "Campaign Treasurer",
                "Door-to-door issue yatra smaller hai: trained teams, consent log aur daily expense upload. Reach steady rahegi, compliance aur polling operations strong honge.",
                0, -80, 0, false, 2);
            strategy.ConfigurePoliticalReward(1, 4, 3);
            strategy.ConfigureAssemblyReward(5, 5, 5);
            strategy.ConfigureAssemblyCampaignReward(12, 12, 10);
            strategy.ConfigureDecision(
                "assembly-campaign-strategy",
                "FINAL CAMPAIGN WEEK",
                "Rs 130 campaign funds bache hain. Door-to-door issue yatra disciplined hai; influencer-led mega convoy fast support deta hai par permissions, unity aur polling preparation weak kar sakta hai.",
                "ISSUE YATRA\nSupport +12 / Rules +12",
                "MEDIA CONVOY\nSupport +20 / Pressure +12",
                "Mega convoy trend karta hai, par two route permissions late, volunteer briefing thin aur expense evidence incomplete hai. Support badhta hai, future scrutiny bhi.",
                0, -130, -5, -3, 4, 1, 12, 0, 0, 0, 0, 0, 10, -5, -4, 20, -5, -4);
            objectives.Add(strategy);
            labels.Add("Disciplined issue yatra ya influencer-led mega convoy mein decision lein");

            GameObject auditDesk = new GameObject("Campaign Donation Audit Desk");
            auditDesk.transform.position = new Vector3(20f, 1f, -2f);
            CreatePrimitiveChild("Audit Desk", PrimitiveType.Cube, auditDesk.transform, Vector3.zero, new Vector3(3.4f, 1.0f, 1.4f), teal);
            CreatePrimitiveChild("Receipt Stack", PrimitiveType.Cube, auditDesk.transform, new Vector3(-0.65f, 0.62f, 0f), new Vector3(0.75f, 0.12f, 0.65f), white);
            CreatePrimitiveChild("Donation Register", PrimitiveType.Cube, auditDesk.transform, new Vector3(0.65f, 0.62f, 0f), new Vector3(0.75f, 0.12f, 0.65f), yellow);
            MissionObjective audit = AddObjective(auditDesk, "campaign-audit", "Campaign account audit karein", "Independent Campaign Auditor",
                "Every donation, print bill, vehicle permission aur digital invoice reconciled. Anonymous large donation reject aur correction note public register mein hai.",
                0, 0, 0, false, 3);
            audit.ConfigurePoliticalReward(0, 0, 2);
            audit.ConfigureAssemblyCampaignReward(2, 14, 2);
            objectives.Add(audit);
            labels.Add("Donation, print, vehicle aur digital spending audit close karein");

            GameObject accessibilityLead = CreatePerson("Polling Accessibility Lead", new Vector3(16f, 0f, -10f), white, darkStone, skin, hair, false);
            MissionObjective accessibility = AddObjective(accessibilityLead, "polling-accessibility", "Accessible polling plan verify karein", "Polling Accessibility Lead",
                "Ramp, shade, water, queue signs aur elderly assistance ready hain. Candidate volunteers boundary ke bahar; polling zone ke andar sirf neutral authorized staff.",
                0, 0, 0, false);
            accessibility.ConfigurePoliticalReward(0, 4, 0);
            accessibility.ConfigureAssemblyReward(2, 2, 5);
            accessibility.ConfigureAssemblyCampaignReward(4, 8, 15);
            objectives.Add(accessibility);
            labels.Add("Neutral polling zone ka accessibility aur queue plan verify karein");

            GameObject samrat = CreatePerson("Constable Samrat Polling Perimeter", new Vector3(7f, 0f, -14f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective pollingDay = AddObjective(samrat, "polling-day", "Polling-day protocol confirm karein", "Constable Samrat",
                "Police sirf queue safety aur lawful perimeter sambhalegi. Main kisi candidate desk par nahi aaunga; complaint authorized observer ko record number ke saath jayegi.",
                0, 0, 0, false);
            pollingDay.ConfigurePoliticalReward(0, 0, 2);
            pollingDay.ConfigureAssemblyCampaignReward(3, 8, 14);
            objectives.Add(pollingDay);
            labels.Add("Samrat se neutral polling perimeter aur complaint protocol confirm karein");

            GameObject countCentre = new GameObject("Independent Counting Centre Screen");
            countCentre.transform.position = new Vector3(28f, 1.7f, 11.5f);
            CreatePrimitiveChild("Counting Screen", PrimitiveType.Cube, countCentre.transform, Vector3.zero, new Vector3(5.6f, 3.1f, 0.24f), darkStone);
            for (int i = 0; i < 5; i++)
            {
                CreatePrimitiveChild($"Round Bar {i + 1}", PrimitiveType.Cube, countCentre.transform,
                    new Vector3(-1.8f + i * 0.9f, -0.45f + i * 0.16f, -0.17f),
                    new Vector3(0.48f, 0.65f + i * 0.24f, 0.06f), i % 2 == 0 ? teal : yellow);
            }
            CreateWorldLabel("Counting Centre Label", "AUTHORIZED ROUND TOTALS", new Vector3(28f, 2.55f, 11.32f), Vector3.zero, white, countCentre.transform.parent, 0.021f);
            MissionObjective result = AddObjective(countCentre, "assembly-result", "Final result dekhein", "Independent Counting Observer",
                "Authorized round totals reconciled. Candidate agents ko signed result copy mil rahi hai; final vote-share calculation ready hai.",
                0, 0, 0, false);
            result.ConfigureAssemblyElection();
            objectives.Add(result);
            labels.Add("Independent observer se computed Vidhansabha result receive karein");

            mission.Configure(
                "Janata Ka Mandate",
                objectives,
                labels,
                "CHAPTER 10 COMPLETE",
                "Constituency ne record, organization aur lawful campaign ko vote diya. Azad ka MLA safar ab shuru hota hai.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "COSTED MANIFESTO LIVE", "CAMPAIGN RULES LOCKED", "POLLING SYSTEM READY" },
                new List<string>
                {
                    "Nomination, costed manifesto aur rural issue route complete. Urban public dialogue next hai.",
                    "Town halls, volunteer training, fact-check aur debate verified. Final campaign strategy ab consequences set karegi.",
                    "Strategy and accounts audited. Accessible polling plan aur neutral perimeter ke baad counting hogi."
                });
            mission.ConfigureChapter(10, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 10 / JANATA KA MANDATE",
                "Candidate bano, lawful campaign chalao aur Vidhansabha ka result earn karo.");
        }

        private static void CreateChapterTenEnvironment(
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
            CreateBox("Election District Ground", new Vector3(0f, -0.3f, 0f), new Vector3(72f, 0.6f, 58f), sand, root);
            CreateBox("Election Boulevard", new Vector3(0f, 0.02f, -2f), new Vector3(12f, 0.10f, 50f), darkStone, root);
            CreateBox("Town Hall Road", new Vector3(-17f, 0.03f, 2f), new Vector3(22f, 0.10f, 7f), stone, root);
            CreateBox("Polling Access Road", new Vector3(17f, 0.03f, -10f), new Vector3(22f, 0.10f, 7f), stone, root);

            CreateBox("Manifesto Resource Centre", new Vector3(-24f, 2.7f, -15f), new Vector3(12f, 5.4f, 9f), teal, root);
            CreateWorldLabel("Manifesto Centre Sign", "COSTED PUBLIC MANIFESTO", new Vector3(-24f, 3.55f, -10.38f), Vector3.zero, yellow, root, 0.020f);
            CreateBox("Urban Town Hall", new Vector3(-27f, 3.1f, 4f), new Vector3(13f, 6.2f, 12f), white, root);
            CreateWorldLabel("Town Hall Sign", "OPEN TOWN HALL", new Vector3(-27f, 4f, -2.12f), Vector3.zero, teal, root, 0.024f);

            for (int row = 0; row < 4; row++)
            {
                CreateBox($"Rural Campaign Field {row + 1}", new Vector3(-28f, -0.02f, 13f + row * 3.7f), new Vector3(12f, 0.18f, 2.5f), row % 2 == 0 ? foliage : yellow, root);
            }
            CreateBox("Rural Canal", new Vector3(-20.8f, 0.02f, 18.5f), new Vector3(1.2f, 0.20f, 16f), teal, root);

            CreateBox("Volunteer Law Classroom", new Vector3(-15f, 2.4f, 21f), new Vector3(14f, 4.8f, 8f), yellow, root);
            CreateWorldLabel("Volunteer Classroom Sign", "CAMPAIGN LAW + CONSENT", new Vector3(-15f, 3.25f, 16.88f), Vector3.zero, darkStone, root, 0.019f);
            CreateBox("Fact Check Studio", new Vector3(0f, 2.5f, 24f), new Vector3(12f, 5f, 8f), teal, root);
            CreateWorldLabel("Fact Studio Sign", "SOURCE BEFORE SHARE", new Vector3(0f, 3.35f, 19.88f), Vector3.zero, yellow, root, 0.021f);

            CreateBox("Equal Time Debate Stage", new Vector3(13f, 0.75f, 17f), new Vector3(13f, 1.5f, 8f), darkStone, root);
            CreateBox("Debate Backdrop", new Vector3(13f, 3f, 20.8f), new Vector3(13f, 4.5f, 0.22f), teal, root);
            CreateWorldLabel("Debate Stage Sign", "EQUAL TIME  /  VERIFIED DATA", new Vector3(13f, 3.55f, 20.65f), Vector3.zero, yellow, root, 0.020f);

            CreateBox("Neutral Polling Centre", new Vector3(27f, 2.8f, -12f), new Vector3(13f, 5.6f, 13f), white, root);
            CreateWorldLabel("Polling Centre Sign", "NEUTRAL POLLING ZONE", new Vector3(27f, 3.7f, -5.38f), Vector3.zero, darkStone, root, 0.022f);
            CreateBox("Polling Ramp", new Vector3(20.5f, 0.38f, -9f), new Vector3(7f, 0.18f, 2.2f), stone, root).transform.rotation = Quaternion.Euler(0f, 0f, -5f);
            CreatePollingQueue(root, new Vector3(22f, 0f, -2f), white, darkStone);

            CreateBox("Counting Centre", new Vector3(28f, 3f, 17f), new Vector3(15f, 6f, 9f), stone, root);
            CreateWorldLabel("Counting Building Sign", "INDEPENDENT COUNTING CENTRE", new Vector3(28f, 4f, 12.38f), Vector3.zero, teal, root, 0.019f);

            for (int i = 0; i < 7; i++)
            {
                CreateStreetLamp($"Election Route Lamp {i + 1}", new Vector3(i % 2 == 0 ? -7f : 7f, 0f, -18f + i * 6f), darkStone, yellow, root);
            }
            CreateTree("Election Neem West", new Vector3(-33f, 0f, -2f), foliage, trunk, root);
            CreateTree("Election Neem East", new Vector3(34f, 0f, 5f), foliage, trunk, root);
            CreateExpansionFlag("Campaign Flag Teal", new Vector3(17f, 0f, 14f), darkStone, teal, root);
            CreateExpansionFlag("Campaign Flag Yellow", new Vector3(23f, 0f, 14f), darkStone, yellow, root);
        }

        private static void CreatePollingQueue(Transform root, Vector3 centre, Material neutral, Material dark)
        {
            GameObject queue = new GameObject("Neutral Polling Queue Rails");
            queue.transform.SetParent(root);
            queue.transform.position = centre;
            for (int i = 0; i < 5; i++)
            {
                CreatePrimitiveChild($"Queue Post {i + 1}", PrimitiveType.Cylinder, queue.transform,
                    new Vector3(-4f + i * 2f, 0.7f, 0f), new Vector3(0.10f, 0.7f, 0.10f), dark);
                if (i < 4)
                {
                    CreatePrimitiveChild($"Queue Rail {i + 1}", PrimitiveType.Cube, queue.transform,
                        new Vector3(-3f + i * 2f, 0.72f, 0f), new Vector3(1.9f, 0.08f, 0.08f), neutral);
                }
            }
        }

        private static void AddCandidateOutfit(
            Transform person,
            Material white,
            Material teal,
            Material yellow,
            Material dark)
        {
            CreatePrimitiveChild("Long Kurta Front", PrimitiveType.Cube, person, new Vector3(0f, 0.92f, 0.34f), new Vector3(0.68f, 0.92f, 0.12f), white);
            CreatePrimitiveChild("Party Stole Left", PrimitiveType.Cube, person, new Vector3(-0.23f, 1.12f, -0.48f), new Vector3(0.12f, 0.74f, 0.05f), teal);
            CreatePrimitiveChild("Party Stole Right", PrimitiveType.Cube, person, new Vector3(0.23f, 1.12f, -0.48f), new Vector3(0.12f, 0.74f, 0.05f), yellow);
            CreatePrimitiveChild("Kolhapuri Left", PrimitiveType.Cube, person, new Vector3(-0.18f, 0.07f, 0.16f), new Vector3(0.28f, 0.10f, 0.46f), dark);
            CreatePrimitiveChild("Kolhapuri Right", PrimitiveType.Cube, person, new Vector3(0.18f, 0.07f, 0.16f), new Vector3(0.28f, 0.10f, 0.46f), dark);
        }

        private static void CreateAssemblyElectionLighting()
        {
            GameObject lightObject = new GameObject("Election Morning Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.92f, 0.75f);
            sunlight.intensity = 1.06f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.50f;
            lightObject.transform.rotation = Quaternion.Euler(45f, -36f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.46f, 0.70f, 0.84f);
            RenderSettings.ambientEquatorColor = new Color(0.67f, 0.60f, 0.46f);
            RenderSettings.ambientGroundColor = new Color(0.23f, 0.24f, 0.20f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.71f, 0.80f, 0.84f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 64f;
            RenderSettings.fogEndDistance = 175f;
        }
    }
}

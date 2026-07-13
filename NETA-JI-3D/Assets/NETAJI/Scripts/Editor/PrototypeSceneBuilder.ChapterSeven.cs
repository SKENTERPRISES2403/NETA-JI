using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterSevenScene(
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
            GameObject environment = new GameObject("Fictional Prayagraj Ward Campaign");
            CreateBox("Ward Ground", new Vector3(0f, -0.3f, 0f), new Vector3(54f, 0.6f, 42f), stone, environment.transform);
            CreateBox("Campaign Lane", new Vector3(0f, 0.02f, -4f), new Vector3(10f, 0.08f, 34f), sand, environment.transform);
            CreateBox("IHP Campaign Office", new Vector3(-15f, 2.5f, -9f), new Vector3(14f, 5f, 10f), teal, environment.transform);
            CreateBox("Public Issue Hall", new Vector3(15f, 2.5f, -9f), new Vector3(14f, 5f, 10f), yellow, environment.transform);
            CreateBox("Polling Training Center", new Vector3(-15f, 2.5f, 10f), new Vector3(14f, 5f, 10f), white, environment.transform);
            CreateBox("Counting Center", new Vector3(15f, 2.5f, 10f), new Vector3(14f, 5f, 10f), darkStone, environment.transform);
            CreateWorldLabel("Campaign Office Sign", "INDIA HELPING PARTY  /  WARD OFFICE", new Vector3(-15f, 3.55f, -3.88f), Vector3.zero, yellow, environment.transform, 0.021f);
            CreateWorldLabel("Issue Hall Sign", "PUBLIC ISSUE DESK", new Vector3(15f, 3.55f, -3.88f), Vector3.zero, darkStone, environment.transform, 0.026f);
            CreateWorldLabel("Polling Sign", "BOOTH TRAINING  /  NO VOTE BUYING", new Vector3(-15f, 3.55f, 4.88f), Vector3.zero, teal, environment.transform, 0.019f);
            CreateWorldLabel("Counting Sign", "WARD COUNTING CENTER", new Vector3(15f, 3.55f, 4.88f), Vector3.zero, yellow, environment.transform, 0.023f);

            for (int i = 0; i < 6; i++)
            {
                float side = i % 2 == 0 ? -1f : 1f;
                float x = side * (9f + (i % 3) * 2.5f);
                float z = -1f + (i / 2) * 5f;
                CreateBox($"Ward House {i + 1}", new Vector3(x, 1.8f, z), new Vector3(4.5f, 3.6f, 4.5f), i % 3 == 0 ? teal : i % 3 == 1 ? sand : yellow, environment.transform);
            }
            CreateTree("Campaign Neem Left", new Vector3(-22f, 0f, 2f), foliage, trunk, environment.transform);
            CreateTree("Campaign Neem Right", new Vector3(22f, 0f, 2f), foliage, trunk, environment.transform);
            CreateStreetLamp("Campaign Lamp Left", new Vector3(-6f, 0f, -11f), darkStone, yellow, environment.transform);
            CreateStreetLamp("Campaign Lamp Right", new Vector3(6f, 0f, -11f), darkStone, yellow, environment.transform);

            GameObject systems = new GameObject("Chapter 7 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(7, 100, 350, 90, 42, 28, 55, 17, 62, 85, 59, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad", new Vector3(0f, 0f, -17f), shirt, trousers, skin, hair, true);
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
            gameCamera.farClipPlane = 170f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.6f, -22f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterSevenMission(
                mission, volunteerDress, policeKhaki, teal, yellow, white, darkStone, skin, hair);
            CreateElectionLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterSevenScenePath);
        }

        private static void ConfigureChapterSevenMission(
            MissionController mission,
            Material volunteerDress,
            Material policeKhaki,
            Material teal,
            Material yellow,
            Material white,
            Material darkStone,
            Material skin,
            Material hair)
        {
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject manager = CreatePerson("Ward Campaign Manager", new Vector3(-15f, 0f, -5f), volunteerDress, darkStone, skin, hair, false);
            MissionObjective managerObjective = AddObjective(manager, "baseline-survey", "Baseline survey lein", "Campaign Manager",
                "Ward mein paani, paperwork aur streetlight top issues hain. Support estimate zero se start hoga; har claim field sheet se verify hoga.",
                0, 0, 0, false);
            managerObjective.ConfigurePoliticalReward(1, 2, 0);
            managerObjective.ConfigureCampaignReward(5, 0);
            objectives.Add(managerObjective);
            labels.Add("Campaign manager se ward baseline survey lein");

            GameObject issueBoard = new GameObject("Ward Issue Priority Board");
            issueBoard.transform.position = new Vector3(15f, 1.25f, -5f);
            CreatePrimitiveChild("Issue Board", PrimitiveType.Cube, issueBoard.transform, Vector3.zero, new Vector3(3f, 1.8f, 0.18f), white);
            CreatePrimitiveChild("Water Issue", PrimitiveType.Cube, issueBoard.transform, new Vector3(-0.75f, 0.25f, -0.14f), new Vector3(0.65f, 0.45f, 0.05f), teal);
            CreatePrimitiveChild("Documents Issue", PrimitiveType.Cube, issueBoard.transform, new Vector3(0f, -0.25f, -0.14f), new Vector3(0.65f, 0.45f, 0.05f), yellow);
            CreatePrimitiveChild("Light Issue", PrimitiveType.Cube, issueBoard.transform, new Vector3(0.75f, 0.25f, -0.14f), new Vector3(0.65f, 0.45f, 0.05f), darkStone);
            MissionObjective issueObjective = AddObjective(issueBoard, "issue-priority", "Top ward issues lock karein", "Azad",
                "Manifesto mein wahi teen promises jayenge jinke cost, department aur first-100-days steps likhe ja saken.",
                0, 0, 0, false, 2);
            issueObjective.ConfigureCampaignReward(7, 0);
            objectives.Add(issueObjective);
            labels.Add("Verified survey se top three ward issues lock karein");

            GameObject laneA = CreatePerson("Lane A Resident", new Vector3(-7f, 0f, 0f), yellow, darkStone, skin, hair, false);
            MissionObjective laneAObjective = AddObjective(laneA, "canvass-a", "Lane A issue sheet bharein", "Resident",
                "Vote baad mein, pehle water complaint number likhiye. Pichhle teen applications ki copies yahan hain.",
                0, -50, 0, false);
            laneAObjective.ConfigurePoliticalReward(0, 2, 0);
            laneAObjective.ConfigureCampaignReward(6, 0);
            objectives.Add(laneAObjective);
            labels.Add("Lane A mein door-to-door issue sheet complete karein");

            GameObject laneB = CreatePerson("Lane B Resident", new Vector3(7f, 0f, 1f), teal, darkStone, skin, hair, false);
            MissionObjective laneBObjective = AddObjective(laneB, "canvass-b", "Lane B issue sheet bharein", "Resident",
                "Hamare senior citizens ko pension status desk chahiye. Koi gift nahi, bas weekly help schedule dikhaiye.",
                0, -50, 0, false);
            laneBObjective.ConfigurePoliticalReward(0, 2, 0);
            laneBObjective.ConfigureCampaignReward(6, 0);
            objectives.Add(laneBObjective);
            labels.Add("Lane B mein door-to-door issue sheet complete karein");

            GameObject boothTrainer = CreatePerson("Booth Trainer", new Vector3(-15f, 0f, 6f), volunteerDress, darkStone, skin, hair, false);
            MissionObjective boothObjective = AddObjective(boothTrainer, "booth-training", "Booth team training karein", "Booth Trainer",
                "Voter list help lawful hogi, secrecy absolute hogi, aur kisi ko vote dikhane ya gift lene ko nahi kaha jayega.",
                0, 0, 0, false);
            boothObjective.ConfigurePoliticalReward(1, 5, 0);
            boothObjective.ConfigureCampaignReward(0, 20);
            objectives.Add(boothObjective);
            labels.Add("Polling ethics aur voter-help booth team train karein");

            GameObject expenseLedger = new GameObject("Campaign Expense Ledger");
            expenseLedger.transform.position = new Vector3(-11f, 0.9f, 10f);
            CreatePrimitiveChild("Expense Book", PrimitiveType.Cube, expenseLedger.transform, Vector3.zero, new Vector3(0.95f, 0.16f, 1.2f), yellow);
            MissionObjective expenseObjective = AddObjective(expenseLedger, "expense-ledger", "Campaign receipts publish karein", "Party Treasurer",
                "Small donations Rs 300. Printing, travel aur meeting hall har line receipt ke saath public board par hai.",
                0, 300, 0, false, 2);
            expenseObjective.ConfigurePoliticalReward(0, 0, 1);
            expenseObjective.ConfigureCampaignReward(0, 5);
            objectives.Add(expenseObjective);
            labels.Add("Campaign donation aur expense ledger publish karein");

            GameObject rival = CreatePerson("Fictional Rival Arvind Rana", new Vector3(11f, 0f, -1f), white, darkStone, skin, hair, false);
            MissionObjective rivalObjective = AddObjective(rival, "rival-manifesto", "Rival manifesto compare karein", "Arvind Rana / Nagar Badlav Dal",
                "Main aapke model se sehmat nahi, par debate issues par hogi. Dono campaigns apna cost sheet public karenge.",
                0, 0, 0, false, 2);
            rivalObjective.ConfigurePoliticalReward(0, 0, 3);
            rivalObjective.ConfigureCampaignReward(5, 0);
            objectives.Add(rivalObjective);
            labels.Add("Fictional rival ke manifesto aur cost claims compare karein");

            GameObject rumour = new GameObject("Campaign Misinformation Packet");
            rumour.transform.position = new Vector3(3f, 0.2f, 4f);
            CreatePrimitiveChild("Rumour Print", PrimitiveType.Cube, rumour.transform, Vector3.zero, new Vector3(1.0f, 0.05f, 1.25f), darkStone);
            MissionObjective rumourObjective = AddObjective(rumour, "campaign-rumour", "Misinformation packet verify karein", "Fact Check Volunteer",
                "Edited photo aur original receipt side by side hain. Correction mein source links honge, abuse ya doxxing nahi.",
                0, 0, 0, false, 2);
            rumourObjective.ConfigurePoliticalReward(0, 0, 2);
            rumourObjective.ConfigureCampaignReward(3, 0);
            objectives.Add(rumourObjective);
            labels.Add("Campaign misinformation ka evidence packet verify karein");

            GameObject strategy = new GameObject("Campaign Strategy Board");
            strategy.transform.position = new Vector3(0f, 1f, 8f);
            CreatePrimitiveChild("Strategy Panel", PrimitiveType.Cube, strategy.transform, Vector3.zero, new Vector3(3.2f, 1.7f, 0.18f), darkStone);
            MissionObjective strategyObjective = AddObjective(strategy, "campaign-strategy", "Final campaign strategy chunein", "Azad",
                "Chhoti issue meetings aur door-to-door fact sheets. Kam shor, zyada booth discipline.",
                0, -200, 2, false, 2);
            strategyObjective.ConfigurePoliticalReward(2, 5, 2);
            strategyObjective.ConfigureCampaignReward(12, 10);
            strategyObjective.ConfigureDecision(
                "campaign-strategy",
                "FINAL CAMPAIGN STRATEGY",
                "Ground meetings costly aur slow hain. Mega rally tez support la sakti hai, par booth discipline aur pressure ko hurt karegi.",
                "GROUND CAMPAIGN\nSupport +12 / Booth +10",
                "MEGA RALLY\nSupport +16 / Pressure +10",
                "Campaign mega rally par shift hota hai. Crowd bada hai, par expense scrutiny aur booth coordination weak hoti hai.",
                0, -50, -3, -2, 4, 2, 10, 16, -5);
            objectives.Add(strategyObjective);
            labels.Add("Ground campaign ya mega rally strategy chunein");

            GameObject debate = CreatePerson("Public Debate Moderator", new Vector3(0f, 0f, 3f), policeKhaki, darkStone, skin, hair, false);
            MissionObjective debateObjective = AddObjective(debate, "public-debate", "Public issue debate complete karein", "Independent Moderator",
                "Time equal raha, personal allegations roki gayi, aur written answers public record mein upload honge.",
                0, 0, 2, false);
            debateObjective.ConfigurePoliticalReward(2, 0, 1);
            debateObjective.ConfigureCampaignReward(8, 0);
            objectives.Add(debateObjective);
            labels.Add("Fictional rival ke saath moderated public debate complete karein");

            GameObject pollingChecklist = new GameObject("Polling Day Checklist");
            pollingChecklist.transform.position = new Vector3(-15f, 1f, 11f);
            CreatePrimitiveChild("Checklist Board", PrimitiveType.Cube, pollingChecklist.transform, Vector3.zero, new Vector3(2.8f, 1.7f, 0.18f), white);
            MissionObjective pollingObjective = AddObjective(pollingChecklist, "polling-checklist", "Polling checklist lock karein", "Booth Trainer",
                "Agents, helpline, transport disclosure, complaint log aur voter-secrecy briefing complete. Gifts aur intimidation zero.",
                0, -100, 2, false, 1);
            pollingObjective.ConfigurePoliticalReward(4, 4, 0);
            pollingObjective.ConfigureCampaignReward(10, 50);
            objectives.Add(pollingObjective);
            labels.Add("Polling-day booth readiness checklist complete karein");

            GameObject resultBoard = new GameObject("Ward Election Result Board");
            resultBoard.transform.position = new Vector3(15f, 1.4f, 6f);
            CreatePrimitiveChild("Counting Screen", PrimitiveType.Cube, resultBoard.transform, Vector3.zero, new Vector3(4.2f, 2.4f, 0.20f), darkStone);
            CreateWorldLabel("Counting Ready", "COUNTING  /  RESULT", new Vector3(15f, 1.65f, 5.84f), Vector3.zero, yellow, resultBoard.transform.parent, 0.025f);
            MissionObjective resultObjective = AddObjective(resultBoard, "ward-result", "Ward result dekhein", "Returning Officer",
                "Counting complete.", 0, 0, 0, false);
            resultObjective.ConfigureWardElectionResult();
            objectives.Add(resultObjective);
            labels.Add("Counting center par computed ward election result dekhein");

            mission.Configure(
                "Ward Ka Faisla",
                objectives,
                labels,
                "CHAPTER 7 COMPLETE",
                "First democratic mandate recorded. Local governance responsibility begins now.");
            mission.ConfigureMilestones(
                new List<int> { 5, 8, 11 },
                new List<string> { "GROUND SURVEY COMPLETE", "CAMPAIGN TEST", "POLLING READY" },
                new List<string>
                {
                    "Issues aur two-lane canvass complete hain. Ab booth team aur expense records ready karo.",
                    "Rival claims aur misinformation documented hain. Final campaign strategy choose karo.",
                    "Polling systems ready hain. Counting center par result ka wait hai."
                });
            mission.ConfigureChapter(7, "Chapter08");
            mission.ConfigureIntro(
                "CHAPTER 7 / WARD KA FAISLA",
                "First local election: issues, expenses, booths aur public mandate.");
        }

        private static void CreateElectionLighting()
        {
            GameObject lightObject = new GameObject("Election Day Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.88f, 0.72f);
            sunlight.intensity = 1.04f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.48f;
            lightObject.transform.rotation = Quaternion.Euler(38f, -30f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.50f, 0.68f, 0.78f);
            RenderSettings.ambientEquatorColor = new Color(0.58f, 0.52f, 0.42f);
            RenderSettings.ambientGroundColor = new Color(0.20f, 0.22f, 0.20f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.67f, 0.74f, 0.76f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 48f;
            RenderSettings.fogEndDistance = 140f;
        }
    }
}

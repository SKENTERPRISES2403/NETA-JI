using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterFourteenScene(
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
            GameObject environment = new GameObject("Fictional State Policy and Leadership Campus");
            CreateChapterFourteenEnvironment(
                environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 14 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                14, 100, 10, 100, 100, 56, 190, 92, 67, 85, 59, true,
                65, 87, 5, 85, true, 88, 93, 99, 91, true, 58, 92, 69, 58, true,
                78, 80, 90, 30, 78, true, 78, 90, 92, 89, true,
                80, 89, 98, 82, 6, true, 84, 86, 88, 87, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad State Leader Candidate", new Vector3(0f, 0f, -24f), white, white, skin, hair, true);
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
            cameraObject.transform.position = new Vector3(0f, 3.8f, -29f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterFourteenMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterFourteenLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterFourteenScenePath);
        }

        private static void ConfigureChapterFourteenMission(
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
                "State Leadership Mandate Auditor", new Vector3(0f, 0f, -19f), white, darkStone, skin, hair, false);
            MissionObjective mandate = AddObjective(
                auditor,
                "leadership-mandate",
                "State foothold verify karein",
                "Independent Mandate Auditor",
                "Six of eight fictional seats, district readiness, MLA report and campaign compliance verified. Leadership claim result se nahi, open caucus process se decide hoga.",
                0, 0, 0, false, 1);
            mandate.ConfigurePoliticalReward(0, 0, -10);
            mandate.ConfigureStateLeadershipReward(5, 8, 6);
            objectives.Add(mandate);
            labels.Add("Independent auditor se multi-seat mandate aur leadership eligibility verify karein");

            GameObject charterTable = new GameObject("Caucus Ethics Charter Table");
            charterTable.transform.position = new Vector3(-11f, 1.0f, -14f);
            CreatePrimitiveChild("Round Charter Table", PrimitiveType.Cylinder, charterTable.transform, Vector3.zero, new Vector3(4.2f, 0.22f, 4.2f), darkStone);
            for (int index = 0; index < 6; index++)
            {
                float angle = index * Mathf.PI * 2f / 6f;
                CreatePrimitiveChild(
                    $"Caucus Charter Seat {index + 1}", PrimitiveType.Cube, charterTable.transform,
                    new Vector3(Mathf.Cos(angle) * 3.2f, -0.35f, Mathf.Sin(angle) * 3.2f),
                    new Vector3(0.9f, 0.8f, 0.9f), index % 2 == 0 ? teal : yellow);
            }
            MissionObjective charter = AddObjective(
                charterTable,
                "caucus-charter",
                "Caucus ethics charter sign karein",
                "Six-Seat Caucus Secretary",
                "Attendance, dissent note, conflict disclosure, whip limits and anti-defection legal review published. Agreement issue discipline par hai, personal loyalty par nahi.",
                0, 0, 0, false, 2);
            charter.ConfigurePoliticalReward(0, 0, -5);
            charter.ConfigureStateLeadershipReward(6, 12, 4);
            objectives.Add(charter);
            labels.Add("Six-seat caucus ka ethics, dissent and conflict charter publish karein");

            GameObject budgetLead = CreatePerson(
                "Shadow Budget Research Lead", new Vector3(-24f, 0f, -5f), teal, darkStone, skin, hair, false);
            MissionObjective budget = AddObjective(
                budgetLead,
                "shadow-budget",
                "Costed shadow budget banayein",
                "Public Finance Research Lead",
                "Education, primary health, safety, jobs and local government proposals ke cost, funding source and trade-offs public spreadsheet mein hain. Unfunded promise remove hua.",
                0, 0, 0, false, 2);
            budget.ConfigureStateLeadershipReward(15, 5, 6);
            objectives.Add(budget);
            labels.Add("Five priority areas ka costed fictional shadow budget publish karein");

            GameObject hearingLead = CreatePerson(
                "Regional Hearing Moderator", new Vector3(-23f, 0f, 8f), volunteerDress, darkStone, skin, hair, false);
            CreateChapterNineCrowd(
                "Regional Hearing Citizens", new Vector3(-23f, 0f, 11f), 8,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective hearings = AddObjective(
                hearingLead,
                "regional-hearings",
                "Regional public hearings karein",
                "Independent Hearing Moderator",
                "Rural, urban, women, youth, workers, traders and disability groups ko equal speaking slots mile. Personal data masked; unresolved objections public tracker par hain.",
                0, 0, 0, false);
            hearings.ConfigurePoliticalReward(0, 5, 2);
            hearings.ConfigureStateLeadershipReward(8, 5, 14);
            objectives.Add(hearings);
            labels.Add("Seven public groups ke equal-time regional hearings complete karein");

            GameObject shanti = CreatePerson(
                "Shanti State Leadership Council", new Vector3(-10f, 0f, 19f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            CreateChapterNineCrowd(
                "Women Youth Policy Council", new Vector3(-10f, 0f, 22f), 7,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective council = AddObjective(
                shanti,
                "leadership-council",
                "Policy leadership council train karein",
                "Shanti",
                "Council members budget reading, bill notes, public speaking, harassment reporting and complaint appeals practice karenge. Token photo nahi; written recommendations track hongi.",
                0, 0, 0, false);
            council.ConfigurePoliticalReward(0, 6, 2);
            council.ConfigureStateLeadershipReward(6, 10, 10);
            objectives.Add(council);
            labels.Add("Shanti ke saath women-youth policy leadership council train karein");

            GameObject samrat = CreatePerson(
                "Constable Samrat Safety Liaison", new Vector3(7f, 0f, 19f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective emergency = AddObjective(
                samrat,
                "state-emergency-plan",
                "Non-partisan emergency plan review karein",
                "Constable Samrat",
                "Flood, heatwave and crowd-safety coordination public SOP par hai. Police and administration remain neutral; party volunteers sirf relief, helpline and verified information support karenge.",
                0, 0, 0, false);
            emergency.ConfigurePoliticalReward(0, 4, 2);
            emergency.ConfigureStateLeadershipReward(10, 6, 8);
            objectives.Add(emergency);
            labels.Add("Samrat ke saath neutral emergency and crowd-safety coordination review karein");

            GameObject selectionStage = new GameObject("State Legislative Leader Selection Stage");
            selectionStage.transform.position = new Vector3(17f, 1.2f, 15f);
            CreatePrimitiveChild("Selection Platform", PrimitiveType.Cube, selectionStage.transform, Vector3.zero, new Vector3(7.4f, 1.0f, 4.4f), darkStone);
            CreatePrimitiveChild("Selection Backdrop", PrimitiveType.Cube, selectionStage.transform, new Vector3(0f, 2f, 1.95f), new Vector3(7.4f, 3.2f, 0.22f), teal);
            CreateWorldLabel(
                "Leader Selection Label", "OPEN RECORD  /  SECRET BALLOT",
                new Vector3(17f, 3.4f, 16.84f), Vector3.zero, yellow, selectionStage.transform.parent, 0.021f);
            MissionObjective selection = AddObjective(
                selectionStage,
                "state-leadership-strategy",
                "Leadership process chunein",
                "Independent Caucus Returning Officer",
                "Open caucus election mein candidate statement, published endorsements, secret ballot and independent count hoga. Rs 30 process cost hai, par legitimacy durable rahegi.",
                0, -30, 0, false);
            selection.ConfigurePoliticalReward(2, 5, 4);
            selection.ConfigureStateLeadershipReward(12, 16, 14);
            selection.ConfigureDecision(
                "state-leadership-strategy",
                "STATE LEGISLATIVE LEADER",
                "Open caucus election slower hai. Closed loyalty deal numbers jaldi lock karta hai, par policy record, public legitimacy and evidence discipline weak ho sakte hain.",
                "OPEN CAUCUS BALLOT\nUnity +16 / Public +14",
                "CLOSED LOYALTY DEAL\nPower +6 / Pressure +14",
                "Closed deal ne votes lock kiye, lekin policy conditions informal hain, two endorsements undisclosed rahe and public hearing objections unanswered hain.",
                0, 0, -10, -5, 6, 2, 14,
                riskyStatePolicyCredibility: -8,
                riskyStateCaucusUnity: 12,
                riskyPublicLeadership: -4);
            objectives.Add(selection);
            labels.Add("Open caucus ballot ya closed loyalty deal mein leadership decision lein");

            GameObject researchDesk = new GameObject("Legislative Research Library Desk");
            researchDesk.transform.position = new Vector3(25f, 1.2f, 6f);
            CreatePrimitiveChild("Research Counter", PrimitiveType.Cube, researchDesk.transform, Vector3.zero, new Vector3(4.8f, 1.2f, 1.8f), teal);
            for (int index = 0; index < 5; index++)
            {
                CreatePrimitiveChild(
                    $"Policy File {index + 1}", PrimitiveType.Cube, researchDesk.transform,
                    new Vector3(-1.6f + index * 0.8f, 0.72f, 0f), new Vector3(0.52f, 0.16f, 0.75f), index % 2 == 0 ? white : yellow);
            }
            MissionObjective research = AddObjective(
                researchDesk,
                "legislative-research",
                "Policy research cell lock karein",
                "Legislative Research Coordinator",
                "Bills, budget notes, committee briefs and dissent drafts use common evidence template. Caucus members source challenge kar sakte hain; correction log permanent hai.",
                0, 0, 0, false);
            research.ConfigurePoliticalReward(0, 0, -4);
            research.ConfigureStateLeadershipReward(8, 4, 5);
            objectives.Add(research);
            labels.Add("Evidence-led legislative research and correction workflow activate karein");

            GameObject mediaLead = CreatePerson(
                "State Fact Check Editor", new Vector3(25f, 0f, -4f), shirt, trousers, skin, hair, false);
            MissionObjective media = AddObjective(
                mediaLead,
                "leadership-fact-check",
                "Leadership rumour correct karein",
                "Independent Fact Check Editor",
                "Fake resignation quote ka full video, timestamp and correction released. Abuse brigade nahi; rival response and archived source same page par hain.",
                0, 0, 0, false, 1);
            media.ConfigurePoliticalReward(0, 0, 3);
            media.ConfigureStateLeadershipReward(4, 4, 8);
            objectives.Add(media);
            labels.Add("Fictional leadership rumour ko source-backed correction se answer karein");

            GameObject conventionLead = CreatePerson(
                "State Policy Convention Chair", new Vector3(16f, 0f, -15f), yellow, darkStone, skin, hair, false);
            CreateChapterNineCrowd(
                "State Policy Convention Delegates", new Vector3(16f, 0f, -18f), 10,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective convention = AddObjective(
                conventionLead,
                "state-policy-convention",
                "State policy convention karein",
                "Convention Chair",
                "Six elected members, candidates, civil experts and citizens report policy costs, dissent and pending work. Leadership applause se nahi, recorded answers se judge hogi.",
                0, 0, 0, false);
            convention.ConfigurePoliticalReward(2, 5, 2);
            convention.ConfigureStateLeadershipReward(5, 8, 5);
            objectives.Add(convention);
            labels.Add("Evidence-led state policy convention and dissent session complete karein");

            GameObject ethicsLead = CreatePerson(
                "State Leadership Ethics Reviewer", new Vector3(4f, 0f, -17f), white, darkStone, skin, hair, false);
            MissionObjective ethics = AddObjective(
                ethicsLead,
                "state-leadership-ethics",
                "Leadership ethics audit karein",
                "Independent Ethics Review Panel",
                "Assets, gifts, meetings, donors, conflicts, endorsements, complaints and vote count audited. Protected identity, private family data and retaliation are prohibited.",
                0, 0, 0, false, 2);
            ethics.ConfigurePoliticalReward(0, 0, -3);
            ethics.ConfigureStateLeadershipReward(5, 8, 8);
            objectives.Add(ethics);
            labels.Add("Assets, endorsements, conflicts and caucus vote ka final ethics audit close karein");

            GameObject reviewDais = new GameObject("State Leadership Review Dais");
            reviewDais.transform.position = new Vector3(0f, 2.0f, -10f);
            CreatePrimitiveChild("Leadership Review Screen", PrimitiveType.Cube, reviewDais.transform, Vector3.zero, new Vector3(7.2f, 3.8f, 0.26f), darkStone);
            CreatePrimitiveChild("Policy Gauge", PrimitiveType.Cube, reviewDais.transform, new Vector3(-2.1f, -0.30f, -0.17f), new Vector3(0.8f, 1.6f, 0.06f), teal);
            CreatePrimitiveChild("Unity Gauge", PrimitiveType.Cube, reviewDais.transform, new Vector3(0f, 0.02f, -0.17f), new Vector3(0.8f, 2.25f, 0.06f), white);
            CreatePrimitiveChild("Public Gauge", PrimitiveType.Cube, reviewDais.transform, new Vector3(2.1f, 0.22f, -0.17f), new Vector3(0.8f, 2.65f, 0.06f), yellow);
            CreateWorldLabel(
                "Leadership Review Label", "STATE LEADERSHIP REVIEW",
                new Vector3(0f, 4.15f, -10.18f), Vector3.zero, yellow, reviewDais.transform.parent, 0.025f);
            MissionObjective review = AddObjective(
                reviewDais,
                "state-leadership-review",
                "Leadership result dekhein",
                "Independent State Leadership Panel",
                "Policy credibility, caucus unity, public leadership, multi-seat record and pressure ka computed result ready hai.",
                0, 0, 0, false);
            review.ConfigureStateLeadership();
            objectives.Add(review);
            labels.Add("Independent panel par computed state-leadership selection dekhein");

            mission.Configure(
                "Pradesh Ka Netrutva",
                objectives,
                labels,
                "CHAPTER 14 COMPLETE",
                "Policy, caucus and public record reviewed. Azad ki state leadership ab process se earned hai.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "POLICY BASELINE READY", "LEADERSHIP PROCESS LOCKED", "PUBLIC RECORD COMPLETE" },
                new List<string>
                {
                    "Mandate, caucus charter and costed budget ready. Ab public hearings and leadership bench build karo.",
                    "Hearings, council, emergency plan and leadership decision complete. Research and public record next hain.",
                    "Research, fact-check and state convention complete. Final ethics audit selection decide karega."
                });
            mission.ConfigureChapter(14, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 14 / PRADESH KA NETRUTVA",
                "Six seats ka foothold mila hai. Ab policy, unity aur public trust se leadership earn karo.");
        }

        private static void CreateChapterFourteenEnvironment(
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
            CreateBox("State Policy Campus Ground", new Vector3(0f, -0.32f, 0f), new Vector3(88f, 0.64f, 64f), sand, root);
            CreateBox("Leadership Avenue", new Vector3(0f, 0.02f, -2f), new Vector3(9f, 0.08f, 58f), stone, root);
            CreateBox("Policy Crosswalk", new Vector3(0f, 0.03f, 9f), new Vector3(72f, 0.08f, 7f), stone, root);

            CreateBox("Fictional State Policy Hall", new Vector3(0f, 3.5f, 28f), new Vector3(30f, 7f, 8f), teal, root);
            CreateBox("Policy Hall Canopy", new Vector3(0f, 7.1f, 25.2f), new Vector3(33f, 0.42f, 3.2f), yellow, root);
            CreateWorldLabel(
                "Policy Hall Sign", "STATE POLICY AND PUBLIC LEADERSHIP CAMPUS",
                new Vector3(0f, 4.85f, 23.92f), Vector3.zero, white, root, 0.025f);
            CreateWorldLabel(
                "Policy Hall Motto", "EVIDENCE  /  DISSENT  /  ACCOUNTABILITY",
                new Vector3(0f, 3.65f, 23.90f), Vector3.zero, yellow, root, 0.020f);

            CreateBox("Shadow Budget Lab", new Vector3(-31f, 3f, -4f), new Vector3(15f, 6f, 18f), white, root);
            CreateWorldLabel(
                "Budget Lab Sign", "COSTED POLICY LAB",
                new Vector3(-23.38f, 4.1f, -4f), new Vector3(0f, -90f, 0f), teal, root, 0.021f);

            CreateBox("Public Hearing Pavilion", new Vector3(-28f, 2.7f, 19f), new Vector3(20f, 5.4f, 10f), darkStone, root);
            CreateWorldLabel(
                "Hearing Pavilion Sign", "PUBLIC HEARING PAVILION",
                new Vector3(-28f, 3.8f, 13.88f), Vector3.zero, yellow, root, 0.021f);

            CreateBox("Legislative Research Library", new Vector3(31f, 3f, 3f), new Vector3(16f, 6f, 18f), teal, root);
            CreateWorldLabel(
                "Research Library Sign", "BILLS  /  BUDGET  /  SOURCES",
                new Vector3(22.88f, 4.1f, 3f), new Vector3(0f, 90f, 0f), yellow, root, 0.019f);

            CreateBox("State Convention Plaza", new Vector3(16f, -0.08f, -19f), new Vector3(30f, 0.18f, 10f), white, root);
            CreateBox("Leadership Council Plaza", new Vector3(-8f, -0.07f, 20f), new Vector3(24f, 0.18f, 10f), white, root);

            for (int index = 0; index < 8; index++)
            {
                float x = -30f + index * 8.5f;
                Material seatColor = index < 6 ? teal : white;
                CreateExpansionFlag($"Leadership Mandate Flag {index + 1}", new Vector3(x, 0f, -28f), darkStone, seatColor, root);
                CreateWorldLabel(
                    $"Leadership Mandate Label {index + 1}", index < 6 ? "WON" : "OPPOSITION",
                    new Vector3(x, 1.05f, -26.8f), Vector3.zero, darkStone, root, 0.013f);
            }

            for (int index = 0; index < 10; index++)
            {
                CreateStreetLamp(
                    $"Policy Avenue Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -6f : 6f, 0f, -24f + index * 5.6f), darkStone, yellow, root);
            }

            CreateTree("Policy Campus Neem North West", new Vector3(-41f, 0f, 27f), foliage, trunk, root);
            CreateTree("Policy Campus Neem North East", new Vector3(41f, 0f, 27f), foliage, trunk, root);
            CreateTree("Policy Campus Neem South West", new Vector3(-41f, 0f, -24f), foliage, trunk, root);
            CreateTree("Policy Campus Neem South East", new Vector3(41f, 0f, -24f), foliage, trunk, root);
        }

        private static void CreateChapterFourteenLighting()
        {
            GameObject lightObject = new GameObject("State Policy Afternoon Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.88f, 0.70f);
            sunlight.intensity = 1.07f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.49f;
            lightObject.transform.rotation = Quaternion.Euler(48f, -38f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.43f, 0.67f, 0.84f);
            RenderSettings.ambientEquatorColor = new Color(0.71f, 0.60f, 0.44f);
            RenderSettings.ambientGroundColor = new Color(0.24f, 0.24f, 0.19f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.72f, 0.81f, 0.86f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 74f;
            RenderSettings.fogEndDistance = 205f;
        }
    }
}

using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterElevenScene(
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
            GameObject environment = new GameObject("Fictional State Assembly And Constituency Campus");
            CreateChapterElevenEnvironment(
                environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 11 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                11, 100, 50, 100, 96, 43, 114, 76, 67, 85, 59, true,
                65, 87, 5, 85, true, 88, 93, 99, 91, true, 58, 92, 69, 58, true,
                78, 80, 90, 30, 78, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("MLA Azad", new Vector3(0f, 0f, -21f), white, white, skin, hair, true);
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
            gameCamera.farClipPlane = 205f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.7f, -26f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterElevenMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white, shirt, trousers, darkStone, skin, hair);
            CreateLegislativeLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterElevenScenePath);
        }

        private static void ConfigureChapterElevenMission(
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

            GameObject registrar = CreatePerson("Fictional Assembly Registrar", new Vector3(0f, 0f, -17f), white, darkStone, skin, hair, false);
            MissionObjective oath = AddObjective(registrar, "mla-oath", "Public oath record karein", "Fictional Assembly Registrar",
                "Constitutional duty, truthful disclosure, attendance aur conflict-of-interest register accepted. Oath public archive mein same day upload hogi.",
                0, 0, 0, false);
            oath.ConfigurePoliticalReward(0, 0, 1);
            oath.ConfigureLegislativeReward(6, 2, 12, 0);
            objectives.Add(oath);
            labels.Add("Fictional assembly registrar ke saamne public duty oath record karein");

            GameObject officeSecretary = CreatePerson("Constituency Office Secretary", new Vector3(-12f, 0f, -12f), teal, darkStone, skin, hair, false);
            MissionObjective office = AddObjective(officeSecretary, "constituency-office", "Constituency office kholein", "Constituency Office Secretary",
                "Rs 120 lakh fictional public allocation ledger open hai. Personal, party aur public accounts separate; every case receives token, owner and due date.",
                0, 0, 0, false);
            office.ConfigurePoliticalReward(0, 4, 0);
            office.ConfigureLegislativeReward(4, 10, 8, 120);
            objectives.Add(office);
            labels.Add("Token-based constituency office aur separate public allocation ledger kholein");

            GameObject dashboard = new GameObject("Constituency Case Dashboard");
            dashboard.transform.position = new Vector3(-20f, 1.45f, -4f);
            CreatePrimitiveChild("Dashboard", PrimitiveType.Cube, dashboard.transform, Vector3.zero, new Vector3(4.4f, 2.6f, 0.22f), teal);
            for (int i = 0; i < 4; i++)
            {
                CreatePrimitiveChild($"Case Column {i + 1}", PrimitiveType.Cube, dashboard.transform,
                    new Vector3(-1.35f + i * 0.9f, -0.25f + i * 0.14f, -0.16f),
                    new Vector3(0.52f, 1.0f + i * 0.22f, 0.05f), i % 2 == 0 ? white : yellow);
            }
            MissionObjective caseDashboard = AddObjective(dashboard, "case-dashboard", "Public case dashboard live karein", "Citizen Data Volunteer",
                "Pension, hospital, school, land-copy aur safety cases anonymized ID ke saath live hain. Rs 5 lakh setup invoice and privacy review attached hai.",
                0, 0, 0, false, 2);
            caseDashboard.ConfigurePoliticalReward(0, 3, 0);
            caseDashboard.ConfigureLegislativeReward(4, 10, 8, -5);
            objectives.Add(caseDashboard);
            labels.Add("Privacy-safe constituency case dashboard aur deadlines publish karein");

            GameObject questionClerk = CreatePerson("Legislative Question Clerk", new Vector3(-18f, 0f, 7f), shirt, trousers, skin, hair, false);
            MissionObjective hospitalQuestion = AddObjective(questionClerk, "hospital-question", "Hospital question file karein", "Legislative Question Clerk",
                "Medicine tender delay, stock-out days, inspection date aur corrective deadline four-part question mein cited hain. Real person allegation nahi; verified fictional records only.",
                0, 0, 0, false, 3);
            hospitalQuestion.ConfigurePoliticalReward(0, 0, 3);
            hospitalQuestion.ConfigureLegislativeReward(10, 4, 6, 0);
            objectives.Add(hospitalQuestion);
            labels.Add("Evidence-backed hospital procurement question assembly desk par file karein");

            GameObject committeeClerk = CreatePerson("Public Accounts Committee Clerk", new Vector3(-10f, 0f, 14f), white, darkStone, skin, hair, false);
            MissionObjective committee = AddObjective(committeeClerk, "committee-hearing", "Committee evidence submit karein", "Committee Clerk",
                "Ward dashboard, medicine batches, vendor invoices aur citizen statements indexed hain. Committee hearing public summary dega; confidential identities masked rahengi.",
                0, 0, 0, false, 3);
            committee.ConfigurePoliticalReward(2, 0, 3);
            committee.ConfigureLegislativeReward(12, 3, 8, 0);
            objectives.Add(committee);
            labels.Add("Committee ko indexed evidence aur privacy-safe citizen statements dein");

            GameObject budgetAnalyst = CreatePerson("Education Transport Budget Analyst", new Vector3(0f, 0f, 17f), yellow, darkStone, skin, hair, false);
            MissionObjective budget = AddObjective(budgetAnalyst, "budget-priority", "Education transport budget lock karein", "Budget Analyst",
                "Rural student bus, girls' safe-stop lighting aur disability access ke Rs 15 lakh line items costed hain. Outcome and quarterly utilization public rahenge.",
                0, 0, 0, false);
            budget.ConfigurePoliticalReward(0, 0, 2);
            budget.ConfigureLegislativeReward(8, 8, 5, -15);
            objectives.Add(budget);
            labels.Add("Student transport aur safe-stop allocation with outcomes approve karein");

            GameObject worksBoard = new GameObject("Public Works Selection Board");
            worksBoard.transform.position = new Vector3(12f, 1.45f, 14f);
            CreatePrimitiveChild("Works Board", PrimitiveType.Cube, worksBoard.transform, Vector3.zero, new Vector3(4.8f, 2.8f, 0.22f), darkStone);
            CreatePrimitiveChild("Open Score Sheet", PrimitiveType.Cube, worksBoard.transform, new Vector3(-1.2f, 0f, -0.16f), new Vector3(1.5f, 1.7f, 0.05f), teal);
            CreatePrimitiveChild("Recommendation Sheet", PrimitiveType.Cube, worksBoard.transform, new Vector3(1.2f, 0f, -0.16f), new Vector3(1.5f, 1.7f, 0.05f), yellow);
            MissionObjective works = AddObjective(worksBoard, "legislative-strategy", "Public works process chunein", "Constituency Finance Officer",
                "Open scorecard se school repair, drain and clinic access projects need, cost and readiness par rank honge. Rs 40 lakh release public measurement ke baad.",
                0, 0, 0, false, 3);
            works.ConfigurePoliticalReward(1, 4, 4);
            works.ConfigureLegislativeReward(7, 10, 15, -40);
            works.ConfigureDecision(
                "legislative-strategy",
                "CONSTITUENCY WORKS ALLOCATION",
                "Open scoring slower but auditable hai. A closed recommendation list faster delivery promise karti hai, par beneficiary reasons aur conflict checks incomplete hain.",
                "OPEN SCORECARD\nEthics +15 / Fund -40L",
                "CLOSED LIST\nSpeed +14 / Pressure +14",
                "Recommendation list se approvals tez hote hain, lekin Rs 55 lakh commitment ka conflict trail weak aur public objections unanswered rehte hain.",
                0, 0, -5, -4, 5, 1, 14,
                riskyLegislativeEffectiveness: 14,
                riskyConstituencyService: 14,
                riskyEthics: -15,
                riskyMlaAllocation: -55);
            objectives.Add(works);
            labels.Add("Open project scorecard ya closed recommendation list mein decision lein");

            GameObject samrat = CreatePerson("Constable Samrat Flood Liaison", new Vector3(20f, 0f, 7f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective flood = AddObjective(samrat, "flood-response", "Flood response coordinate karein", "Constable Samrat",
                "Evacuation route, shelters and missing-person desk police coordinate karegi. MLA office Rs 20 lakh relief logistics, public stock register aur independent beneficiary check sambhalega.",
                0, 0, 0, false);
            flood.ConfigurePoliticalReward(0, 4, 2);
            flood.ConfigureLegislativeReward(6, 12, 5, -20);
            objectives.Add(flood);
            labels.Add("Samrat ke saath non-partisan flood shelter and relief system activate karein");

            GameObject ethicsDesk = new GameObject("Protected Disclosure Desk");
            ethicsDesk.transform.position = new Vector3(20f, 1.1f, -2f);
            CreatePrimitiveChild("Secure Desk", PrimitiveType.Cube, ethicsDesk.transform, Vector3.zero, new Vector3(3.4f, 1.1f, 1.4f), teal);
            CreatePrimitiveChild("Sealed File", PrimitiveType.Cube, ethicsDesk.transform, new Vector3(0f, 0.68f, 0f), new Vector3(0.9f, 0.12f, 0.7f), yellow);
            MissionObjective ethicsChannel = AddObjective(ethicsDesk, "protected-disclosure", "Protected disclosure seal karein", "Independent Ethics Counsel",
                "Fictional contractor kickback evidence hash, witness protection request and conflict register sealed hain. Rs 5 lakh legal support public head se traceable hai.",
                0, 0, 0, false, 4);
            ethicsChannel.ConfigurePoliticalReward(0, 0, 3);
            ethicsChannel.ConfigureLegislativeReward(8, 5, 12, -5);
            objectives.Add(ethicsChannel);
            labels.Add("Whistleblower evidence ko independent protected channel mein seal karein");

            GameObject attendance = new GameObject("Attendance And Voting Record");
            attendance.transform.position = new Vector3(14f, 1.35f, -10f);
            CreatePrimitiveChild("Attendance Screen", PrimitiveType.Cube, attendance.transform, Vector3.zero, new Vector3(4.2f, 2.4f, 0.22f), darkStone);
            for (int i = 0; i < 5; i++)
            {
                CreatePrimitiveChild($"Attendance Tick {i + 1}", PrimitiveType.Cube, attendance.transform,
                    new Vector3(-1.45f + i * 0.72f, 0f, -0.16f), new Vector3(0.42f, 1.35f, 0.05f), i % 2 == 0 ? teal : yellow);
            }
            MissionObjective record = AddObjective(attendance, "attendance-record", "Attendance record publish karein", "Legislative Records Officer",
                "Session attendance, questions, committee work, votes and declared conflicts machine-readable file mein public hain. Absence reason bhi attached hai.",
                0, 0, 0, false, 2);
            record.ConfigureLegislativeReward(8, 4, 6, 0);
            objectives.Add(record);
            labels.Add("Attendance, questions, votes aur conflict declarations publish karein");

            GameObject shanti = CreatePerson("Shanti Citizen Report Lead", new Vector3(6f, 0f, -14f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            CreateChapterNineCrowd("Constituency Report Card Residents", new Vector3(7f, 0f, -17f), 6, volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective reportCard = AddObjective(shanti, "mla-report-card", "Citizen report card karein", "Shanti",
                "Closed, pending and rejected cases read out honge. Rs 5 lakh review, audit and accessibility cost disclosed hai; opposition residents ko equal question time milega.",
                0, 0, 0, false);
            reportCard.ConfigurePoliticalReward(0, 0, 2);
            reportCard.ConfigureLegislativeReward(5, 12, 5, -5);
            objectives.Add(reportCard);
            labels.Add("Shanti ke saath open constituency report card and opposition Q&A karein");

            GameObject reviewBoard = new GameObject("Public MLA Performance Board");
            reviewBoard.transform.position = new Vector3(0f, 1.7f, -10f);
            CreatePrimitiveChild("Performance Screen", PrimitiveType.Cube, reviewBoard.transform, Vector3.zero, new Vector3(5.6f, 3.1f, 0.24f), darkStone);
            CreatePrimitiveChild("Legislative Gauge", PrimitiveType.Cube, reviewBoard.transform, new Vector3(-1.55f, -0.4f, -0.17f), new Vector3(0.7f, 1.25f, 0.06f), teal);
            CreatePrimitiveChild("Service Gauge", PrimitiveType.Cube, reviewBoard.transform, new Vector3(0f, -0.1f, -0.17f), new Vector3(0.7f, 1.85f, 0.06f), yellow);
            CreatePrimitiveChild("Ethics Gauge", PrimitiveType.Cube, reviewBoard.transform, new Vector3(1.55f, 0.15f, -0.17f), new Vector3(0.7f, 2.35f, 0.06f), white);
            CreateWorldLabel("MLA Review Label", "PUBLIC MLA PERFORMANCE REVIEW", new Vector3(0f, 2.55f, -10.18f), Vector3.zero, yellow, reviewBoard.transform.parent, 0.020f);
            MissionObjective review = AddObjective(reviewBoard, "mla-performance", "MLA performance score dekhein", "Independent Citizen Review Panel",
                "Legislative work, constituency service, ethics, public allocation and pressure ka computed review ready hai.",
                0, 0, 0, false);
            review.ConfigureMlaPerformance();
            objectives.Add(review);
            labels.Add("Independent panel par computed MLA performance review dekhein");

            mission.Configure(
                "Janata Ka MLA",
                objectives,
                labels,
                "CHAPTER 11 COMPLETE",
                "Azad ne seat ko seva office aur accountable legislative record mein badla. Agla challenge district bhar ka network hai.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "CONSTITUENCY OFFICE LIVE", "LEGISLATIVE RECORD ACTIVE", "PUBLIC FUNDS TRACEABLE" },
                new List<string>
                {
                    "Oath, public allocation and case dashboard live. Ab ground evidence assembly process mein le jaana hai.",
                    "Question, committee, budget and works decision recorded. Emergency response and ethics channel next hain.",
                    "Flood response, disclosure channel and attendance record complete. Citizen report card final review lock karega."
                });
            mission.ConfigureChapter(11, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 11 / JANATA KA MLA",
                "Seat ko power nahi, measurable constituency service aur legislative duty mein badlo.");
        }

        private static void CreateChapterElevenEnvironment(
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
            CreateBox("Legislative Campus Ground", new Vector3(0f, -0.3f, 0f), new Vector3(72f, 0.6f, 58f), sand, root);
            CreateBox("Public Duty Boulevard", new Vector3(0f, 0.02f, -2f), new Vector3(12f, 0.10f, 50f), darkStone, root);
            CreateBox("Constituency Service Road", new Vector3(-19f, 0.03f, -3f), new Vector3(24f, 0.10f, 7f), stone, root);
            CreateBox("Emergency Service Road", new Vector3(19f, 0.03f, 3f), new Vector3(24f, 0.10f, 7f), stone, root);

            CreateBox("Fictional Assembly Main Hall", new Vector3(0f, 5f, 27f), new Vector3(34f, 10f, 12f), white, root);
            CreateBox("Assembly Roof", new Vector3(0f, 10.3f, 27f), new Vector3(38f, 0.8f, 15f), teal, root);
            for (int i = 0; i < 7; i++)
            {
                CreatePrimitiveChild($"Assembly Column {i + 1}", PrimitiveType.Cylinder, root,
                    new Vector3(-12f + i * 4f, 4.2f, 20.6f), new Vector3(0.55f, 4.2f, 0.55f), stone, true);
            }
            CreateWorldLabel("Assembly Hall Sign", "FICTIONAL STATE ASSEMBLY  /  PUBLIC DUTY", new Vector3(0f, 8f, 20.45f), Vector3.zero, teal, root, 0.024f);

            CreateBox("Constituency Seva Office", new Vector3(-28f, 3f, -9f), new Vector3(13f, 6f, 14f), teal, root);
            CreateWorldLabel("Seva Office Sign", "MLA CONSTITUENCY SEVA OFFICE", new Vector3(-28f, 4f, -1.88f), Vector3.zero, yellow, root, 0.020f);
            CreateBox("Committee Evidence Room", new Vector3(-25f, 2.7f, 12f), new Vector3(15f, 5.4f, 12f), stone, root);
            CreateWorldLabel("Committee Room Sign", "PUBLIC EVIDENCE COMMITTEE", new Vector3(-25f, 3.6f, 5.88f), Vector3.zero, teal, root, 0.020f);

            CreateBox("Flood Control Room", new Vector3(28f, 2.8f, 8f), new Vector3(13f, 5.6f, 13f), yellow, root);
            CreateWorldLabel("Flood Room Sign", "FLOOD CONTROL + RELIEF LOG", new Vector3(28f, 3.7f, 1.38f), Vector3.zero, darkStone, root, 0.019f);
            CreateBox("Ethics And Records Annex", new Vector3(28f, 2.8f, -10f), new Vector3(13f, 5.6f, 13f), white, root);
            CreateWorldLabel("Ethics Annex Sign", "ETHICS + PUBLIC RECORDS", new Vector3(28f, 3.7f, -3.38f), Vector3.zero, teal, root, 0.021f);

            CreateBox("Citizen Review Plaza", new Vector3(7f, -0.08f, -18f), new Vector3(24f, 0.18f, 8f), white, root);
            CreateExpansionFlag("Public Duty Flag Teal", new Vector3(-7f, 0f, 18f), darkStone, teal, root);
            CreateExpansionFlag("Public Duty Flag Yellow", new Vector3(7f, 0f, 18f), darkStone, yellow, root);
            for (int i = 0; i < 8; i++)
            {
                CreateStreetLamp($"Legislative Campus Lamp {i + 1}", new Vector3(i % 2 == 0 ? -7f : 7f, 0f, -20f + i * 5.5f), darkStone, yellow, root);
            }
            CreateTree("Assembly Neem West", new Vector3(-34f, 0f, 20f), foliage, trunk, root);
            CreateTree("Assembly Neem East", new Vector3(34f, 0f, 22f), foliage, trunk, root);
            CreateTree("Seva Office Neem", new Vector3(-34f, 0f, -20f), foliage, trunk, root);
        }

        private static void CreateLegislativeLighting()
        {
            GameObject lightObject = new GameObject("Legislative Campus Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.93f, 0.78f);
            sunlight.intensity = 1.05f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.50f;
            lightObject.transform.rotation = Quaternion.Euler(44f, -33f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.47f, 0.69f, 0.83f);
            RenderSettings.ambientEquatorColor = new Color(0.66f, 0.61f, 0.48f);
            RenderSettings.ambientGroundColor = new Color(0.23f, 0.24f, 0.21f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.72f, 0.80f, 0.84f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 66f;
            RenderSettings.fogEndDistance = 180f;
        }
    }
}

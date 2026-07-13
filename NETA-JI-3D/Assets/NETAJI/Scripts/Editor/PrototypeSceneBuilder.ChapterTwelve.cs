using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterTwelveScene(
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
            GameObject environment = new GameObject("Fictional Prayagraj District Organization Campus");
            CreateChapterTwelveEnvironment(
                environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 12 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                12, 100, 10, 100, 100, 47, 139, 97, 67, 85, 59, true,
                65, 87, 5, 85, true, 88, 93, 99, 91, true, 58, 92, 69, 58, true,
                78, 80, 90, 30, 78, true, 78, 90, 92, 89, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("MLA Azad District Organizer", new Vector3(0f, 0f, -22f), white, white, skin, hair, true);
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
            gameCamera.farClipPlane = 210f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.7f, -27f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterTwelveMission(
                mission, shantiDress, volunteerDress, teal, yellow, white, shirt, trousers, darkStone, skin, hair);
            CreateDistrictLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterTwelveScenePath);
        }

        private static void ConfigureChapterTwelveMission(
            MissionController mission,
            Material shantiDress,
            Material volunteerDress,
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

            GameObject auditor = CreatePerson("District Expansion Auditor", new Vector3(0f, 0f, -18f), white, darkStone, skin, hair, false);
            MissionObjective eligibility = AddObjective(auditor, "district-eligibility", "MLA record verify karein", "District Expansion Auditor",
                "Assembly mandate, MLA report card, public fund balance and ethics threshold verified. District expansion tabhi chalega jab local office service continue rahe.",
                0, 0, 0, false, 1);
            eligibility.ConfigurePoliticalReward(0, 0, 1);
            eligibility.ConfigureDistrictReward(4, 8, 10);
            objectives.Add(eligibility);
            labels.Add("Independent auditor se MLA record aur expansion eligibility verify karein");

            GameObject issueMap = new GameObject("Fictional District Issue Map");
            issueMap.transform.position = new Vector3(-10f, 0.9f, -14f);
            CreatePrimitiveChild("Map Table", PrimitiveType.Cube, issueMap.transform, Vector3.zero, new Vector3(5.2f, 0.28f, 3.8f), darkStone);
            for (int row = 0; row < 2; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    CreatePrimitiveChild($"Fictional Zone {row + 1}-{column + 1}", PrimitiveType.Cube, issueMap.transform,
                        new Vector3(-1.55f + column * 1.55f, 0.22f, -0.85f + row * 1.7f),
                        new Vector3(1.25f, 0.12f, 1.25f), (row + column) % 2 == 0 ? teal : yellow);
                }
            }
            MissionObjective baseline = AddObjective(issueMap, "district-baseline", "District issue map lock karein", "District Data Volunteer",
                "Six fictional zones ke water, schools, clinics, transport and paperwork baselines same format mein mapped hain. Real boundary ya election claim use nahi hua.",
                0, 0, 0, false);
            baseline.ConfigureDistrictReward(10, 4, 6);
            objectives.Add(baseline);
            labels.Add("Six fictional zones ka comparable public-issue baseline lock karein");

            GameObject ethicsPanel = CreatePerson("Candidate Ethics Panel Lead", new Vector3(-20f, 0f, -7f), teal, darkStone, skin, hair, false);
            MissionObjective vetting = AddObjective(ethicsPanel, "candidate-vetting", "Candidate files vet karein", "Candidate Ethics Panel Lead",
                "Service record, assets, liabilities, conflicts, criminal-case disclosure and public interview scored hain. Rumour, family identity and protected traits scoring se bahar hain.",
                0, 0, 0, false, 2);
            vetting.ConfigurePoliticalReward(0, 0, 2);
            vetting.ConfigureDistrictReward(5, 15, 8);
            objectives.Add(vetting);
            labels.Add("Independent criteria se fictional candidate disclosures and service record vet karein");

            GameObject shanti = CreatePerson("Shanti Leadership Cohort Lead", new Vector3(-20f, 0f, 4f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            CreateChapterNineCrowd("Women Youth Leadership Cohort", new Vector3(-20f, 0f, 7f), 6, volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective cohort = AddObjective(shanti, "leadership-cohort", "Leadership cohort train karein", "Shanti",
                "Women and youth leaders public speaking, complaint handling, budget reading and safety protocol practice karenge. Ticket promise nahi; evaluation transparent rahega.",
                0, 0, 0, false);
            cohort.ConfigurePoliticalReward(0, 5, 1);
            cohort.ConfigureDistrictReward(8, 10, 8);
            objectives.Add(cohort);
            labels.Add("Shanti ke saath women-youth public leadership cohort train karein");

            GameObject ruralCoordinator = CreatePerson("Rural Block Coordinator", new Vector3(-13f, 0f, 13f), volunteerDress, darkStone, skin, hair, false);
            MissionObjective rural = AddObjective(ruralCoordinator, "rural-blocks", "Rural block network banayein", "Rural Block Coordinator",
                "Irrigation, health sub-centre, school route and land-copy desks shared service calendar par hain. Coordinator selection attendance and solved cases se hoga.",
                0, 0, 0, false);
            rural.ConfigurePoliticalReward(0, 4, 2);
            rural.ConfigureDistrictReward(10, 6, 8);
            objectives.Add(rural);
            labels.Add("Rural blocks ke issue desks aur accountable coordinators connect karein");

            GameObject urbanCoordinator = CreatePerson("Urban Zone Coordinator", new Vector3(0f, 0f, 17f), shirt, trousers, skin, hair, false);
            MissionObjective urban = AddObjective(urbanCoordinator, "urban-zones", "Urban zone network banayein", "Urban Zone Coordinator",
                "Water complaints, jobs desk, rent paperwork and safe transport route common dashboard par hain. Volunteer roster consent and shift limits follow karega.",
                0, 0, 0, false);
            urban.ConfigurePoliticalReward(0, 4, 2);
            urban.ConfigureDistrictReward(10, 6, 8);
            objectives.Add(urban);
            labels.Add("Urban zones ko shared complaint and volunteer dashboard par jodein");

            GameObject selectionStage = new GameObject("District Candidate Selection Stage");
            selectionStage.transform.position = new Vector3(13f, 1.2f, 14f);
            CreatePrimitiveChild("Stage", PrimitiveType.Cube, selectionStage.transform, Vector3.zero, new Vector3(6.2f, 1.0f, 3.8f), darkStone);
            CreatePrimitiveChild("Backdrop", PrimitiveType.Cube, selectionStage.transform, new Vector3(0f, 1.9f, 1.65f), new Vector3(6.2f, 2.9f, 0.20f), teal);
            CreateWorldLabel("Selection Stage Label", "SERVICE  /  DISCLOSURE  /  DEBATE", new Vector3(13f, 3.12f, 15.54f), Vector3.zero, yellow, selectionStage.transform.parent, 0.019f);
            MissionObjective selection = AddObjective(selectionStage, "district-strategy", "Candidate selection process chunein", "District Election Committee",
                "Open local primary mein published shortlist, public debate and conflict review hoga. Rs 40 organization funds lagenge, par quality and discipline strong rahenge.",
                0, -40, 0, false, 1);
            selection.ConfigurePoliticalReward(2, 6, 4);
            selection.ConfigureDistrictReward(12, 16, 16);
            selection.ConfigureDecision(
                "district-strategy",
                "DISTRICT CANDIDATE SELECTION",
                "Open local selection slower and costlier hai. Closed appointment fast launch deta hai, par local evidence, team ownership and discipline weaker ho sakte hain.",
                "OPEN LOCAL PRIMARY\nQuality +16 / Discipline +16",
                "CLOSED APPOINTMENT\nReach +24 / Pressure +14",
                "Closed appointments jaldi announce hote hain, lekin two service records incomplete, local objections pending and coordinators decision se disconnected feel karte hain.",
                0, -10, -10, -5, 6, 2, 14,
                riskyDistrictReach: 24,
                riskyCandidateQuality: -9,
                riskyOrganizationDiscipline: -8);
            objectives.Add(selection);
            labels.Add("Open local primary ya centralized closed appointment mein decision lein");

            GameObject fundingDesk = new GameObject("District Funding Audit Desk");
            fundingDesk.transform.position = new Vector3(21f, 1.1f, 6f);
            CreatePrimitiveChild("Audit Counter", PrimitiveType.Cube, fundingDesk.transform, Vector3.zero, new Vector3(3.8f, 1.1f, 1.5f), teal);
            CreatePrimitiveChild("Donation Ledger", PrimitiveType.Cube, fundingDesk.transform, new Vector3(-0.7f, 0.68f, 0f), new Vector3(0.8f, 0.12f, 0.7f), white);
            CreatePrimitiveChild("Expense Ledger", PrimitiveType.Cube, fundingDesk.transform, new Vector3(0.7f, 0.68f, 0f), new Vector3(0.8f, 0.12f, 0.7f), yellow);
            MissionObjective funding = AddObjective(fundingDesk, "district-funding", "District funding audit karein", "Independent Finance Volunteer",
                "Zone donations, travel, training and media bills reconciled. Anonymous large funds rejected; donor cap and conflict register public hain.",
                0, 0, 0, false, 2);
            funding.ConfigurePoliticalReward(0, 0, 2);
            funding.ConfigureDistrictReward(4, 10, 12);
            objectives.Add(funding);
            labels.Add("Zone-wise donations and organization expenses ka public audit close karein");

            GameObject mediaDesk = new GameObject("District Fact Check Studio");
            mediaDesk.transform.position = new Vector3(21f, 1.5f, -3f);
            CreatePrimitiveChild("Fact Screen", PrimitiveType.Cube, mediaDesk.transform, Vector3.zero, new Vector3(4.2f, 2.7f, 0.22f), darkStone);
            CreatePrimitiveChild("Source Card", PrimitiveType.Cube, mediaDesk.transform, new Vector3(-1.05f, 0f, -0.16f), new Vector3(1.25f, 1.6f, 0.05f), teal);
            CreatePrimitiveChild("Correction Card", PrimitiveType.Cube, mediaDesk.transform, new Vector3(1.05f, 0f, -0.16f), new Vector3(1.25f, 1.6f, 0.05f), yellow);
            MissionObjective media = AddObjective(mediaDesk, "district-fact-check", "District rumour correct karein", "Independent Media Trainer",
                "Fake candidate quote ka full recording and correction all zone groups ko gaya. Abuse brigade nahi; archive, source and calm response protocol follow hua.",
                0, 0, 0, false, 1);
            media.ConfigurePoliticalReward(0, 0, 4);
            media.ConfigureDistrictReward(6, 4, 5);
            objectives.Add(media);
            labels.Add("Fictional candidate rumour ko source-backed district correction se answer karein");

            GameObject conventionLead = CreatePerson("District Public Convention Moderator", new Vector3(14f, 0f, -11f), yellow, darkStone, skin, hair, false);
            CreateChapterNineCrowd("District Convention Delegates", new Vector3(14f, 0f, -14f), 8, volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective convention = AddObjective(conventionLead, "district-convention", "District convention karein", "Convention Moderator",
                "Each zone reports solved cases, candidate score and pending objections. Azad last bolega; local coordinators and citizens get equal response time.",
                0, 0, 0, false);
            convention.ConfigurePoliticalReward(2, 6, 2);
            convention.ConfigureDistrictReward(7, 6, 5);
            objectives.Add(convention);
            labels.Add("All zones ka evidence-led public convention and objection session complete karein");

            GameObject compliance = CreatePerson("District Compliance Counsel", new Vector3(5f, 0f, -16f), white, darkStone, skin, hair, false);
            MissionObjective complianceCheck = AddObjective(compliance, "district-compliance", "District compliance lock karein", "District Compliance Counsel",
                "Candidate disclosures, consent logs, funding audit, complaint appeals and anti-hate training complete. Protected-identity voter profiles store nahi kiye gaye.",
                0, 0, 0, false, 2);
            complianceCheck.ConfigurePoliticalReward(0, 0, 1);
            complianceCheck.ConfigureDistrictReward(2, 5, 6);
            objectives.Add(complianceCheck);
            labels.Add("Candidate, consent, funding and anti-hate compliance checklist lock karein");

            GameObject reviewBoard = new GameObject("District Network Review Board");
            reviewBoard.transform.position = new Vector3(0f, 1.7f, -11f);
            CreatePrimitiveChild("District Review Screen", PrimitiveType.Cube, reviewBoard.transform, Vector3.zero, new Vector3(5.8f, 3.2f, 0.24f), darkStone);
            CreatePrimitiveChild("Reach Gauge", PrimitiveType.Cube, reviewBoard.transform, new Vector3(-1.6f, -0.35f, -0.17f), new Vector3(0.72f, 1.35f, 0.06f), teal);
            CreatePrimitiveChild("Quality Gauge", PrimitiveType.Cube, reviewBoard.transform, new Vector3(0f, -0.05f, -0.17f), new Vector3(0.72f, 1.95f, 0.06f), white);
            CreatePrimitiveChild("Discipline Gauge", PrimitiveType.Cube, reviewBoard.transform, new Vector3(1.6f, 0.15f, -0.17f), new Vector3(0.72f, 2.35f, 0.06f), yellow);
            CreateWorldLabel("District Review Label", "DISTRICT NETWORK REVIEW", new Vector3(0f, 2.55f, -11.18f), Vector3.zero, yellow, reviewBoard.transform.parent, 0.022f);
            MissionObjective review = AddObjective(reviewBoard, "district-review", "District readiness score dekhein", "Independent District Review Panel",
                "Reach, candidate quality, organization discipline, MLA performance and pressure ka computed review ready hai.",
                0, 0, 0, false);
            review.ConfigureDistrictExpansion();
            objectives.Add(review);
            labels.Add("Independent panel par computed district expansion score dekhein");

            mission.Configure(
                "Zila Sangathan",
                objectives,
                labels,
                "CHAPTER 12 COMPLETE",
                "Prayagraj district network ab verified candidates, issue desks and public rules par khada hai. Multi-seat journey ready hai.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "DISTRICT BASELINE LOCKED", "RURAL-URBAN NETWORK ACTIVE", "CANDIDATE SYSTEM AUDITED" },
                new List<string>
                {
                    "MLA record, issue map and candidate criteria verified. Ab leadership bench and zone coordinators build karo.",
                    "Leadership cohort, rural-urban desks and selection decision complete. Funding and information discipline next hai.",
                    "Funding, fact-check and public convention complete. Final compliance district readiness decide karega."
                });
            mission.ConfigureChapter(12, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 12 / ZILA SANGATHAN",
                "Ek seat se aage badho: clean candidates aur disciplined district network earn karo.");
        }

        private static void CreateChapterTwelveEnvironment(
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
            CreateBox("District Campus Ground", new Vector3(0f, -0.3f, 0f), new Vector3(76f, 0.6f, 60f), sand, root);
            CreateBox("District Network Boulevard", new Vector3(0f, 0.02f, -2f), new Vector3(12f, 0.10f, 52f), darkStone, root);
            CreateBox("Rural Urban Connector", new Vector3(0f, 0.03f, 12f), new Vector3(58f, 0.10f, 7f), stone, root);
            CreateBox("Candidate Selection Road", new Vector3(0f, 0.03f, -11f), new Vector3(52f, 0.10f, 7f), stone, root);

            CreateBox("District Data Centre", new Vector3(-28f, 3f, -13f), new Vector3(14f, 6f, 12f), teal, root);
            CreateWorldLabel("Data Centre Sign", "DISTRICT ISSUE + DATA CENTRE", new Vector3(-28f, 4f, -6.88f), Vector3.zero, yellow, root, 0.020f);
            CreateBox("Candidate Vetting Hall", new Vector3(-29f, 3f, 3f), new Vector3(14f, 6f, 13f), white, root);
            CreateWorldLabel("Vetting Hall Sign", "CANDIDATE ETHICS + DISCLOSURE", new Vector3(-29f, 4f, -3.62f), Vector3.zero, teal, root, 0.019f);
            CreateBox("Leadership Academy", new Vector3(-25f, 2.7f, 20f), new Vector3(18f, 5.4f, 10f), yellow, root);
            CreateWorldLabel("Leadership Academy Sign", "PUBLIC LEADERSHIP ACADEMY", new Vector3(-25f, 3.6f, 14.88f), Vector3.zero, darkStone, root, 0.020f);

            for (int row = 0; row < 4; row++)
            {
                CreateBox($"Rural Block Field {row + 1}", new Vector3(29f, -0.02f, 17f + row * 3.2f), new Vector3(14f, 0.18f, 2.2f), row % 2 == 0 ? foliage : yellow, root);
            }
            CreateBox("Rural Block Canal", new Vector3(21f, 0.03f, 21f), new Vector3(1.2f, 0.20f, 15f), teal, root);

            CreateBox("District Finance Office", new Vector3(30f, 2.8f, 3f), new Vector3(14f, 5.6f, 12f), teal, root);
            CreateWorldLabel("Finance Office Sign", "DISTRICT FUNDING + AUDIT", new Vector3(30f, 3.7f, -3.12f), Vector3.zero, yellow, root, 0.020f);
            CreateBox("District Media Lab", new Vector3(29f, 2.8f, -14f), new Vector3(14f, 5.6f, 12f), darkStone, root);
            CreateWorldLabel("Media Lab Sign", "SOURCE BEFORE SHARE", new Vector3(29f, 3.7f, -7.88f), Vector3.zero, yellow, root, 0.022f);

            CreateBox("District Convention Plaza", new Vector3(14f, -0.08f, -18f), new Vector3(25f, 0.18f, 8f), white, root);
            CreateExpansionFlag("District Flag Teal", new Vector3(-7f, 0f, 19f), darkStone, teal, root);
            CreateExpansionFlag("District Flag Yellow", new Vector3(7f, 0f, 19f), darkStone, yellow, root);
            for (int i = 0; i < 8; i++)
            {
                CreateStreetLamp($"District Campus Lamp {i + 1}", new Vector3(i % 2 == 0 ? -7f : 7f, 0f, -21f + i * 6f), darkStone, yellow, root);
            }
            CreateTree("District Neem North West", new Vector3(-36f, 0f, 25f), foliage, trunk, root);
            CreateTree("District Neem North East", new Vector3(36f, 0f, 25f), foliage, trunk, root);
            CreateTree("District Neem South West", new Vector3(-35f, 0f, -23f), foliage, trunk, root);
        }

        private static void CreateDistrictLighting()
        {
            GameObject lightObject = new GameObject("District Campus Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.92f, 0.75f);
            sunlight.intensity = 1.06f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.50f;
            lightObject.transform.rotation = Quaternion.Euler(46f, -35f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.46f, 0.70f, 0.84f);
            RenderSettings.ambientEquatorColor = new Color(0.67f, 0.60f, 0.46f);
            RenderSettings.ambientGroundColor = new Color(0.22f, 0.24f, 0.20f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.71f, 0.80f, 0.84f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 68f;
            RenderSettings.fogEndDistance = 185f;
        }
    }
}

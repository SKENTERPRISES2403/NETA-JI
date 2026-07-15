using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterEighteenScene(
            Material sand, Material stone, Material darkStone, Material teal, Material yellow,
            Material white, Material shirt, Material trousers, Material skin, Material hair,
            Material shantiDress, Material volunteerDress, Material policeKhaki,
            Material foliage, Material trunk)
        {
            Scene chapterScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject environment = new GameObject("Fictional National Federation Campus");
            CreateChapterEighteenEnvironment(
                environment.transform, sand, stone, darkStone, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Chapter 18 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(
                18, 100, 0, 100, 100, 69, 376, 85, 67, 85, 59, true,
                65, 87, 5, 85, true, 88, 93, 99, 91, true, 58, 92, 69, 58, true,
                78, 80, 90, 30, 78, true, 78, 90, 92, 89, true,
                80, 89, 98, 82, 6, true, 84, 86, 88, 87, true,
                78, 100, 90, 56, 27, true, 92, 100, 80, 88, true,
                93, 87, 92, 85, 87, true, 96, 100, 96, 90, 18, true);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson(
                "Azad National Federation Organizer", new Vector3(0f, 0f, -31f), white, white, skin, hair, true);
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

            ConfigureChapterEighteenMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white,
                shirt, trousers, darkStone, skin, hair);
            CreateChapterEighteenLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterEighteenScenePath);
        }

        private static void ConfigureChapterEighteenMission(
            MissionController mission, Material shantiDress, Material volunteerDress,
            Material policeKhaki, Material teal, Material yellow, Material white,
            Material shirt, Material trousers, Material darkStone, Material skin, Material hair)
        {
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject eligibilityLead = CreatePerson(
                "National Eligibility Auditor", new Vector3(0f, 0f, -24f), shirt, darkStone, skin, hair, false);
            MissionObjective eligibility = AddObjective(
                eligibilityLead, "national-eligibility-audit", "National expansion eligibility verify karein",
                "Independent Eligibility Auditor",
                "Five-year state outcomes, public disclosures and correction records verify ho gaye. National network state performance ko chhupane ke liye nahi banega.",
                0, 0, 0, false, 1);
            eligibility.ConfigurePoliticalReward(0, 0, -10);
            eligibility.ConfigureNationalExpansionReward(6, 8, 6);
            objectives.Add(eligibility);
            labels.Add("State outcomes, disclosures and correction record verify karein");

            GameObject constitutionDesk = new GameObject("Federal Organization Constitution Desk");
            constitutionDesk.transform.position = new Vector3(-22f, 1.05f, -18f);
            CreatePrimitiveChild("Federal Rules Counter", PrimitiveType.Cube, constitutionDesk.transform, Vector3.zero, new Vector3(7.4f, 1.3f, 2.7f), teal);
            CreatePrimitiveChild("Rights Register", PrimitiveType.Cube, constitutionDesk.transform, new Vector3(-1.8f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), white);
            CreatePrimitiveChild("Duties Register", PrimitiveType.Cube, constitutionDesk.transform, new Vector3(0f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), yellow);
            CreatePrimitiveChild("Appeal Register", PrimitiveType.Cube, constitutionDesk.transform, new Vector3(1.8f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), white);
            MissionObjective constitution = AddObjective(
                constitutionDesk, "federal-organization-rules", "Federal organization constitution banayein",
                "Fictional Federal Rules Council",
                "Regional autonomy, transparent membership, internal elections, removal appeal and audit powers written hain. Kisi real party ya leader ka representation nahi hai.",
                0, 0, 0, false, 1);
            constitution.ConfigureNationalExpansionReward(8, 10, 8);
            objectives.Add(constitution);
            labels.Add("Regional autonomy, membership, elections and appeal rules publish karein");

            GameObject regionLead = CreatePerson(
                "Regional Listening Network Lead", new Vector3(-36f, 0f, -5f), volunteerDress, darkStone, skin, hair, false);
            CreateChapterNineCrowd(
                "Fictional Regional Listening Delegates", new Vector3(-36f, 0f, -1f), 12,
                volunteerDress, teal, yellow, darkStone, skin, hair);
            MissionObjective listening = AddObjective(
                regionLead, "regional-listening-network", "18-region listening yatra run karein",
                "Regional Listening Network Lead",
                "Fictional regions ki language access, local priorities and organizer feedback published minutes ke saath collect hui. Ek template sab jagah force nahi hua.",
                0, 0, 0, false);
            listening.ConfigurePoliticalReward(0, 10, 2);
            listening.ConfigureNationalExpansionReward(16, 10, 6);
            objectives.Add(listening);
            labels.Add("18 fictional regions mein local priorities aur language access listen karein");

            GameObject ethicsBoard = new GameObject("National Candidate Ethics Board");
            ethicsBoard.transform.position = new Vector3(-34f, 2.1f, 14f);
            CreatePrimitiveChild("Candidate Ethics Screen", PrimitiveType.Cube, ethicsBoard.transform, Vector3.zero, new Vector3(9.2f, 4.2f, 0.28f), darkStone);
            for (int index = 0; index < 8; index++)
            {
                CreatePrimitiveChild(
                    $"Candidate Verification Tile {index + 1}", PrimitiveType.Cube, ethicsBoard.transform,
                    new Vector3(-3.3f + (index % 4) * 2.2f, 0.8f - (index / 4) * 1.55f, -0.18f),
                    new Vector3(1.25f, 0.66f, 0.06f), index % 2 == 0 ? yellow : teal);
            }
            MissionObjective ethics = AddObjective(
                ethicsBoard, "national-candidate-ethics", "Candidate ethics standard lock karein",
                "Independent Candidate Ethics Board",
                "Asset disclosure, conflict checks, serious-case review, public interview and removal appeal sab fictional candidates par equal apply honge.",
                0, 0, 0, false, 2);
            ethics.ConfigureNationalExpansionReward(6, 10, 10);
            objectives.Add(ethics);
            labels.Add("Disclosure, conflict check, interview and removal appeal standard lock karein");

            GameObject trainingLead = CreatePerson(
                "Volunteer Training Federation Lead", new Vector3(-18f, 0f, 25f), volunteerDress, trousers, skin, hair, false);
            CreateChapterNineCrowd(
                "National Volunteer Training Cohort", new Vector3(-18f, 0f, 29f), 10,
                volunteerDress, white, teal, darkStone, skin, hair);
            MissionObjective training = AddObjective(
                trainingLead, "national-volunteer-training", "Volunteer training federation build karein",
                "Volunteer Training Federation Lead",
                "Fact-check, consent, crowd safety, grievance routing and local language conduct modules train hue. Supporter ko unchecked authority nahi mili.",
                0, 0, 0, false);
            training.ConfigurePoliticalReward(0, 12, 2);
            training.ConfigureNationalExpansionReward(12, 6, 6);
            objectives.Add(training);
            labels.Add("Fact-check, consent, safety and grievance training federation build karein");

            GameObject shanti = CreatePerson(
                "Shanti Language Access Coordinator", new Vector3(0f, 0f, 27f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            MissionObjective languageAccess = AddObjective(
                shanti, "language-access-network", "Shanti ke saath language-access charter banayein",
                "Shanti",
                "English aur Hinglish ke saath local-language summaries, sign support and accessible meetings ka charter ready hai. Translation ko decoration nahi, participation maana gaya.",
                0, 0, 0, false);
            languageAccess.ConfigurePoliticalReward(0, 8, 2);
            languageAccess.ConfigureNationalExpansionReward(8, 8, 8);
            objectives.Add(languageAccess);
            labels.Add("Shanti ke saath multilingual and accessible participation charter banayein");

            GameObject strategyStage = new GameObject("National Expansion Strategy Stage");
            strategyStage.transform.position = new Vector3(18f, 1.2f, 22f);
            CreatePrimitiveChild("Federation Strategy Platform", PrimitiveType.Cube, strategyStage.transform, Vector3.zero, new Vector3(9.0f, 1.0f, 5.0f), darkStone);
            CreatePrimitiveChild("Federation Strategy Backdrop", PrimitiveType.Cube, strategyStage.transform, new Vector3(0f, 2.0f, 2.2f), new Vector3(9.0f, 3.4f, 0.24f), teal);
            CreateWorldLabel(
                "Federation Strategy Label", "LISTEN  -  ALIGN  -  EARN TRUST",
                new Vector3(18f, 3.5f, 24.1f), Vector3.zero, yellow, strategyStage.transform.parent, 0.022f);
            MissionObjective strategy = AddObjective(
                strategyStage, "national-expansion-approach", "National expansion strategy chunein",
                "Independent Federation Council",
                "Patient federation model regional trust aur policy credibility build karega. Fast headline wave reach badha sakti hai, lekin alliances aur policy depth kamzor pad sakte hain.",
                0, 0, 0, false);
            strategy.ConfigurePoliticalReward(4, 12, 4);
            strategy.ConfigureNationalExpansionReward(14, 16, 14);
            strategy.ConfigureDecision(
                "national-expansion-approach",
                "NATIONAL FEDERATION MODEL",
                "Regional autonomy ke saath patient federation ya centralized headline wave: dono reach la sakte hain, par final review trust aur policy minimum bhi check karega.",
                "PATIENT FEDERATION\nAlliance +16 / Policy +14",
                "CENTRALIZED HEADLINE WAVE\nReach +24 / Power +10",
                "Headline wave ne visibility jaldi badhai, lekin regional partners aur policy teams ko decision space kam mila. Independent readiness review minimum trust check karega.",
                0, 0, -10, -8, 10, 5, 15,
                riskyNationalOrganizationReach: 24,
                riskyFederalAllianceTrust: -10,
                riskyNationalPolicyCredibility: -6);
            objectives.Add(strategy);
            labels.Add("Patient federation ya centralized headline wave mein decision lein");

            GameObject policyBoard = new GameObject("National Policy Federation Lab");
            policyBoard.transform.position = new Vector3(35f, 2.1f, 12f);
            CreatePrimitiveChild("Policy Federation Screen", PrimitiveType.Cube, policyBoard.transform, Vector3.zero, new Vector3(9.4f, 4.2f, 0.28f), darkStone);
            for (int index = 0; index < 9; index++)
            {
                float barHeight = 0.55f + (index % 3) * 0.4f;
                CreatePrimitiveChild(
                    $"Policy Evidence Bar {index + 1}", PrimitiveType.Cube, policyBoard.transform,
                    new Vector3(-3.2f + (index % 3) * 3.2f, -1.15f + (index / 3) * 1.3f + barHeight * 0.5f, -0.18f),
                    new Vector3(1.7f, barHeight, 0.06f), index % 2 == 0 ? yellow : teal);
            }
            MissionObjective policy = AddObjective(
                policyBoard, "national-policy-federation", "National policy federation publish karein",
                "National Policy Evidence Lab",
                "Health, learning, safety, livelihoods and fiscal options regional evidence notes ke saath publish hue. Local adaptation allowed hai, outcome definitions common rahenge.",
                0, 0, 0, false);
            policy.ConfigureNationalExpansionReward(8, 8, 14);
            objectives.Add(policy);
            labels.Add("Regional evidence ke saath health, learning, safety and livelihood platform publish karein");

            GameObject fundingDesk = new GameObject("Clean National Funding Desk");
            fundingDesk.transform.position = new Vector3(38f, 1.05f, -5f);
            CreatePrimitiveChild("Clean Funding Counter", PrimitiveType.Cube, fundingDesk.transform, Vector3.zero, new Vector3(7.6f, 1.3f, 2.7f), white);
            CreatePrimitiveChild("Donation Ledger", PrimitiveType.Cube, fundingDesk.transform, new Vector3(-1.8f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), teal);
            CreatePrimitiveChild("Expense Ledger", PrimitiveType.Cube, fundingDesk.transform, new Vector3(0f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), yellow);
            CreatePrimitiveChild("Audit Ledger", PrimitiveType.Cube, fundingDesk.transform, new Vector3(1.8f, 0.82f, 0f), new Vector3(1.3f, 0.16f, 0.9f), teal);
            MissionObjective funding = AddObjective(
                fundingDesk, "clean-national-funding", "Clean funding protocol activate karein",
                "Independent Campaign Finance Panel",
                "Donation caps, source checks, public expense ledger, vendor conflicts and correction trail activate hain. Hidden influence ko funding shortcut nahi milega.",
                0, 0, 0, false, 3);
            funding.ConfigurePoliticalReward(0, 0, -4);
            funding.ConfigureNationalExpansionReward(5, 8, 8);
            objectives.Add(funding);
            labels.Add("Donation caps, source checks, expense ledger and vendor conflicts audit karein");

            GameObject samrat = CreatePerson(
                "Constable Samrat Convention Safety Liaison", new Vector3(31f, 0f, -20f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective safety = AddObjective(
                samrat, "national-event-safety", "Samrat ke saath convention safety drill karein",
                "Constable Samrat",
                "Entry flow, emergency lane, missing-person desk, volunteer boundaries and evidence handover drill complete hai. Peaceful dissent ke liye separate safe space bhi marked hai.",
                0, 0, 0, false, 1);
            safety.ConfigurePoliticalReward(0, 0, -2);
            safety.ConfigureNationalExpansionReward(5, 7, 6);
            objectives.Add(safety);
            labels.Add("Samrat ke saath event safety, emergency and missing-person drill karein");

            GameObject conventionLead = CreatePerson(
                "National Convention Moderator", new Vector3(13f, 0f, -22f), shirt, trousers, skin, hair, false);
            CreateChapterNineCrowd(
                "National Federation Convention", new Vector3(13f, 0f, -27f), 14,
                shirt, volunteerDress, yellow, darkStone, skin, hair);
            MissionObjective convention = AddObjective(
                conventionLead, "national-federation-convention", "Open national convention conduct karein",
                "Independent Convention Moderator",
                "Regional votes, dissent notes, policy amendments and elected coordination roles open record mein decide hue. Stage par fictional organization hai, real political endorsement nahi.",
                0, 0, 0, false);
            convention.ConfigurePoliticalReward(0, 8, 2);
            convention.ConfigureNationalExpansionReward(8, 9, 10);
            objectives.Add(convention);
            labels.Add("Regional votes, dissent notes and policy amendments ke saath convention conduct karein");

            GameObject reviewWall = new GameObject("Independent National Readiness Review Wall");
            reviewWall.transform.position = new Vector3(0f, 2.55f, -10f);
            CreatePrimitiveChild("National Readiness Review Wall", PrimitiveType.Cube, reviewWall.transform, Vector3.zero, new Vector3(12.2f, 5.1f, 0.30f), darkStone);
            Material[] reviewColors = { teal, yellow, white };
            float[] reviewHeights = { 2.45f, 2.25f, 2.05f };
            for (int index = 0; index < 3; index++)
            {
                CreatePrimitiveChild(
                    $"National Readiness Bar {index + 1}", PrimitiveType.Cube, reviewWall.transform,
                    new Vector3(-3.2f + index * 3.2f, -0.78f + reviewHeights[index] * 0.5f, -0.19f),
                    new Vector3(1.35f, reviewHeights[index], 0.07f), reviewColors[index]);
            }
            CreateWorldLabel(
                "National Readiness Label", "INDEPENDENT NATIONAL READINESS REVIEW",
                new Vector3(0f, 5.28f, -10.2f), Vector3.zero, yellow, reviewWall.transform.parent, 0.023f);
            MissionObjective review = AddObjective(
                reviewWall, "national-readiness-review", "Independent national readiness review dekhein",
                "Independent National Readiness Panel",
                "Organization reach, federal alliance trust, national policy credibility, state record and opposition pressure ka computed public review ready hai.",
                0, 0, 0, false);
            review.ConfigureNationalExpansion();
            objectives.Add(review);
            labels.Add("Reach, alliance trust, policy credibility and regional alignment ka review dekhein");

            mission.Configure(
                "Desh Bhar Ka Saath",
                objectives,
                labels,
                "CHAPTER 18 COMPLETE",
                "18 fictional regional chapters align hue. National election ki taiyari ab publicity nahi, accountable federation ke roop mein shuru hogi.");
            mission.ConfigureMilestones(
                new List<int> { 3, 7, 10 },
                new List<string> { "FEDERAL RULES READY", "NATIONAL STRATEGY LOCKED", "TRUST NETWORK ACTIVE" },
                new List<string>
                {
                    "Eligibility, federal rules and regional listening verified. Ethics and training next hain.",
                    "Candidate ethics, volunteer training, language access and expansion strategy active hain. Policy and funding next hai.",
                    "Policy, clean funding and convention safety verified. Open convention final review unlock karega."
                });
            mission.ConfigureChapter(18, string.Empty);
            mission.ConfigureIntro(
                "CHAPTER 18 / DESH BHAR KA SAATH",
                "Ek state ka model national tab banta hai jab doosre regions usse copy nahi, apni awaaz ke saath shape karein.");
        }

        private static void CreateChapterEighteenEnvironment(
            Transform root, Material sand, Material stone, Material darkStone, Material teal,
            Material yellow, Material white, Material foliage, Material trunk)
        {
            CreateBox("Federation Campus Ground", new Vector3(0f, -0.32f, 0f), new Vector3(108f, 0.64f, 78f), sand, root);
            CreateBox("National Listening Avenue", new Vector3(0f, 0.02f, -5f), new Vector3(12f, 0.08f, 70f), stone, root);
            CreateBox("Regional Federation Crossway", new Vector3(0f, 0.03f, 10f), new Vector3(92f, 0.08f, 7.5f), stone, root);

            CreateBox("National Federation Hall", new Vector3(0f, 5.2f, 33f), new Vector3(44f, 10.4f, 10f), darkStone, root);
            CreateBox("Federation Hall Screen", new Vector3(0f, 4.8f, 27.82f), new Vector3(27f, 6.2f, 0.35f), teal, root);
            CreateWorldLabel(
                "Federation Hall Sign", "FICTIONAL NATIONAL FEDERATION",
                new Vector3(0f, 7.85f, 27.58f), Vector3.zero, yellow, root, 0.030f);

            for (int index = 0; index < 18; index++)
            {
                float angle = index * Mathf.PI * 2f / 18f;
                float radius = 16.5f;
                Vector3 position = new Vector3(Mathf.Sin(angle) * radius, 2.8f, 7f + Mathf.Cos(angle) * radius);
                CreateBox(
                    $"Fictional Regional Chapter Light {index + 1}", position,
                    new Vector3(1.15f, 5.6f, 1.15f), index % 3 == 0 ? yellow : index % 3 == 1 ? teal : white, root);
                CreateBox(
                    $"Fictional Regional Chapter Base {index + 1}", new Vector3(position.x, 0.28f, position.z),
                    new Vector3(2.2f, 0.56f, 2.2f), darkStone, root);
            }

            CreateBox("Federal Rules Pavilion", new Vector3(-40f, 3.2f, -13f), new Vector3(18f, 6.4f, 21f), teal, root);
            CreateWorldLabel(
                "Federal Rules Sign", "FEDERAL RULES + APPEALS",
                new Vector3(-30.88f, 4.25f, -13f), new Vector3(0f, -90f, 0f), yellow, root, 0.021f);

            CreateBox("Regional Listening Pavilion", new Vector3(-41f, 3.1f, 16f), new Vector3(17f, 6.2f, 20f), white, root);
            CreateWorldLabel(
                "Regional Listening Sign", "REGIONAL LISTENING",
                new Vector3(-32.38f, 4.2f, 16f), new Vector3(0f, -90f, 0f), teal, root, 0.022f);

            CreateBox("Policy Federation Pavilion", new Vector3(41f, 3.1f, 16f), new Vector3(17f, 6.2f, 20f), teal, root);
            CreateWorldLabel(
                "Policy Federation Sign", "POLICY FEDERATION",
                new Vector3(32.38f, 4.2f, 16f), new Vector3(0f, 90f, 0f), yellow, root, 0.022f);

            CreateBox("Clean Funding Safety Pavilion", new Vector3(41f, 3.2f, -13f), new Vector3(18f, 6.4f, 21f), white, root);
            CreateWorldLabel(
                "Funding Safety Sign", "CLEAN FUNDING + SAFETY",
                new Vector3(31.88f, 4.25f, -13f), new Vector3(0f, 90f, 0f), teal, root, 0.021f);

            CreateBox("Open National Convention Plaza", new Vector3(10f, -0.07f, -25f), new Vector3(30f, 0.18f, 13f), white, root);
            CreateBox("National Convention Stage", new Vector3(10f, 0.65f, -34f), new Vector3(20f, 1.3f, 6f), darkStone, root);
            CreateBox("Convention Backdrop", new Vector3(10f, 3.4f, -36.5f), new Vector3(20f, 5.5f, 0.35f), teal, root);
            CreateWorldLabel(
                "Open Convention Sign", "OPEN FEDERATION CONVENTION",
                new Vector3(10f, 5.35f, -36.28f), Vector3.zero, yellow, root, 0.025f);

            for (int index = 0; index < 14; index++)
            {
                float flagX = -52f + index * 8f;
                if (Mathf.Abs(flagX) < 5f)
                {
                    continue;
                }
                CreateExpansionFlag(
                    $"Federation Route Flag {index + 1}", new Vector3(flagX, 0f, -36f),
                    darkStone, index % 3 == 0 ? yellow : index % 3 == 1 ? teal : white, root);
            }

            for (int index = 0; index < 12; index++)
            {
                CreateStreetLamp(
                    $"Federation Avenue Lamp {index + 1}",
                    new Vector3(index % 2 == 0 ? -7.5f : 7.5f, 0f, -29f + index * 5.8f), darkStone, yellow, root);
            }

            CreateTree("Federation Neem North West", new Vector3(-51f, 0f, 35f), foliage, trunk, root);
            CreateTree("Federation Neem North East", new Vector3(51f, 0f, 35f), foliage, trunk, root);
            CreateTree("Federation Neem South West", new Vector3(-51f, 0f, -33f), foliage, trunk, root);
            CreateTree("Federation Neem South East", new Vector3(51f, 0f, -33f), foliage, trunk, root);
        }

        private static void CreateChapterEighteenLighting()
        {
            GameObject lightObject = new GameObject("Federation Convention Golden Light");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.90f, 0.72f);
            sunlight.intensity = 1.12f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.50f;
            lightObject.transform.rotation = Quaternion.Euler(40f, -38f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.43f, 0.67f, 0.87f);
            RenderSettings.ambientEquatorColor = new Color(0.76f, 0.60f, 0.40f);
            RenderSettings.ambientGroundColor = new Color(0.23f, 0.24f, 0.21f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.75f, 0.80f, 0.84f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 90f;
            RenderSettings.fogEndDistance = 235f;
        }
    }
}

using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterFiveScene(
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
            GameObject environment = new GameObject("Fictional Community Hospital");
            CreateBox("Hospital Floor", new Vector3(0f, -0.3f, 0f), new Vector3(48f, 0.6f, 38f), stone, environment.transform);
            CreateBox("Main Corridor", new Vector3(0f, 0.02f, 0f), new Vector3(9f, 0.08f, 34f), white, environment.transform);
            CreateBox("Reception Wing", new Vector3(-13f, 2.5f, -9f), new Vector3(14f, 5f, 10f), teal, environment.transform);
            CreateBox("Emergency Ward", new Vector3(13f, 2.5f, -9f), new Vector3(14f, 5f, 10f), white, environment.transform);
            CreateBox("Pharmacy Store", new Vector3(-13f, 2.5f, 8f), new Vector3(14f, 5f, 10f), yellow, environment.transform);
            CreateBox("Records Office", new Vector3(13f, 2.5f, 8f), new Vector3(14f, 5f, 10f), sand, environment.transform);
            CreateBox("Recovery Garden", new Vector3(0f, 0.02f, 16f), new Vector3(20f, 0.08f, 6f), teal, environment.transform);
            CreateWorldLabel("Hospital Name", "SANJEEVANI COMMUNITY HOSPITAL", new Vector3(0f, 4.4f, -16.8f), Vector3.zero, teal, environment.transform, 0.030f);
            CreateWorldLabel("Emergency Sign", "EMERGENCY  /  MATERNITY", new Vector3(13f, 3.5f, -3.88f), Vector3.zero, darkStone, environment.transform, 0.024f);
            CreateWorldLabel("Pharmacy Sign", "PHARMACY STOCK", new Vector3(-13f, 3.5f, 2.88f), Vector3.zero, darkStone, environment.transform, 0.024f);
            CreateWorldLabel("Records Sign", "RECORDS  /  TENDER FILES", new Vector3(13f, 3.5f, 2.88f), Vector3.zero, darkStone, environment.transform, 0.021f);
            CreateBench("Waiting Bench A", new Vector3(-5.5f, 0f, -6f), 0f, darkStone, teal, environment.transform);
            CreateBench("Waiting Bench B", new Vector3(5.5f, 0f, 5f), 180f, darkStone, yellow, environment.transform);
            CreateTree("Recovery Neem A", new Vector3(-7f, 0f, 16f), foliage, trunk, environment.transform);
            CreateTree("Recovery Neem B", new Vector3(7f, 0f, 16f), foliage, trunk, environment.transform);

            GameObject systems = new GameObject("Chapter 5 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(5, 97, 150, 71, 17);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad", new Vector3(0f, 0f, -14f), shirt, trousers, skin, hair, true);
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
            gameCamera.farClipPlane = 150f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.6f, -19f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterFiveMission(
                mission, shantiDress, volunteerDress, policeKhaki, teal, yellow, white, darkStone, skin, hair);
            CreateHospitalLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterFiveScenePath);
        }

        private static void ConfigureChapterFiveMission(
            MissionController mission,
            Material shantiDress,
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

            GameObject shantiArrival = CreatePerson("Shanti Emergency Arrival", new Vector3(-5f, 0f, -11f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shantiArrival.transform, shantiDress);
            objectives.Add(AddObjective(shantiArrival, "shanti-emergency", "Shanti ko sambhalein", "Shanti",
                "Azad, chakkar aa raha hai aur dard badh raha hai. Sandhya safe hai... ab hospital chalo.",
                0, 0, 0, false));
            labels.Add("Shanti ki emergency condition samjhein");

            GameObject ambulanceDesk = new GameObject("Emergency Transport Desk");
            ambulanceDesk.transform.position = new Vector3(-10f, 0.8f, -8f);
            CreatePrimitiveChild("Transport Phone", PrimitiveType.Cube, ambulanceDesk.transform, Vector3.zero, new Vector3(0.45f, 0.72f, 0.24f), darkStone);
            objectives.Add(AddObjective(ambulanceDesk, "transport", "Emergency transport confirm karein", "Azad",
                "Ambulance route clear hai. Advance receipt le li; har payment ka record rakhenge.",
                1, -200, 1, false));
            labels.Add("Emergency transport aur receipt confirm karein");

            GameObject nurse = CreatePerson("Triage Nurse", new Vector3(10f, 0f, -11f), white, teal, skin, hair, false);
            objectives.Add(AddObjective(nurse, "triage", "Triage nurse ko history dein", "Triage Nurse",
                "Vitals unstable the, ab monitor par hain. Pregnancy file aur current medicines ki list turant chahiye.",
                1, -50, 1, false));
            labels.Add("Triage desk par Shanti ki medical history dein");

            GameObject consent = new GameObject("Emergency Consent Form");
            consent.transform.position = new Vector3(6f, 0.85f, -6f);
            CreatePrimitiveChild("Form", PrimitiveType.Cube, consent.transform, Vector3.zero, new Vector3(0.78f, 0.08f, 1.0f), white);
            CreatePrimitiveChild("Pen", PrimitiveType.Cylinder, consent.transform, new Vector3(0.38f, 0.12f, 0f), new Vector3(0.05f, 0.42f, 0.05f), teal).transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            objectives.Add(AddObjective(consent, "consent", "Consent form padhein", "Azad",
                "Procedure, risk aur cost padhe bina sign nahi. Doctor ne questions clear kiye; copy mere paas rahegi.",
                1, 0, 1, false));
            labels.Add("Emergency consent ko padhkar signed copy lein");

            GameObject donor = CreatePerson("Helpers Hand Donor Coordinator", new Vector3(-4f, 0f, -2f), volunteerDress, darkStone, skin, hair, false);
            objectives.Add(AddObjective(donor, "donor-network", "Donor network activate karein", "Helpers Hand Coordinator",
                "Do verified donors blood bank pahunch rahe hain. Kisi se paise nahi liye jayenge; screening hospital karega.",
                2, -100, 1, false));
            labels.Add("Helpers Hand verified donor network activate karein");

            GameObject stockRegister = new GameObject("Pharmacy Stock Register");
            stockRegister.transform.position = new Vector3(-12f, 0.9f, 4f);
            CreatePrimitiveChild("Stock Book", PrimitiveType.Cube, stockRegister.transform, Vector3.zero, new Vector3(0.9f, 0.14f, 1.1f), yellow);
            objectives.Add(AddObjective(stockRegister, "stock-register", "Medicine stock register dekhein", "Pharmacist",
                "Emergency injection shelf par nahi thi, jabki register mein available likhi hai. Issue time aur signature ka photo le lo.",
                1, 0, 1, false, 2));
            labels.Add("Pharmacy stock register aur issue time verify karein");

            GameObject batchLabel = new GameObject("Medicine Batch Label");
            batchLabel.transform.position = new Vector3(-15f, 1f, 8f);
            CreatePrimitiveChild("Medicine Box", PrimitiveType.Cube, batchLabel.transform, Vector3.zero, new Vector3(1.2f, 0.65f, 0.72f), white);
            CreatePrimitiveChild("Batch Strip", PrimitiveType.Cube, batchLabel.transform, new Vector3(0f, 0.05f, -0.40f), new Vector3(0.82f, 0.20f, 0.05f), shantiDress);
            objectives.Add(AddObjective(batchLabel, "batch-label", "Batch label preserve karein", "Azad",
                "Supply label aur invoice batch match nahi karte. Box seal karke doctor aur Samrat dono ko dikhayenge.",
                1, 0, 1, false, 3));
            labels.Add("Medicine batch label aur invoice mismatch preserve karein");

            GameObject doctor = CreatePerson("Duty Doctor", new Vector3(11f, 0f, -5f), white, darkStone, skin, hair, false);
            objectives.Add(AddObjective(doctor, "doctor-update", "Doctor se Shanti ka update lein", "Duty Doctor",
                "Shanti ab stable hai. Humein afsos hai, pregnancy ko nahi bacha sake. Ab unki recovery aur sachchi case review dono zaroori hain.",
                0, 0, 0, false));
            labels.Add("Duty doctor se private medical update lein");

            GameObject strategy = new GameObject("Hospital Case Strategy Board");
            strategy.transform.position = new Vector3(0f, 1f, 2f);
            CreatePrimitiveChild("Case Board", PrimitiveType.Cube, strategy.transform, Vector3.zero, new Vector3(2.8f, 1.5f, 0.18f), darkStone);
            CreatePrimitiveChild("Verified File", PrimitiveType.Cube, strategy.transform, new Vector3(-0.62f, 0f, -0.13f), new Vector3(0.75f, 0.9f, 0.05f), teal);
            CreatePrimitiveChild("Phone Post", PrimitiveType.Cube, strategy.transform, new Vector3(0.62f, 0f, -0.13f), new Vector3(0.55f, 0.9f, 0.05f), shantiDress);
            MissionObjective decision = AddObjective(strategy, "hospital-decision", "Case strategy chunein", "Azad",
                "Pehle batch, stock aur tender file verify karenge. Sach dheere chale, par adhoora nahi.",
                2, -100, 3, false, 5);
            decision.ConfigureDecision(
                "hospital-approach",
                "CASE STRATEGY",
                "Supplier ka naam abhi post karna tez pressure banayega, par unverified claim case ko kamzor kar sakta hai.",
                "VERIFIED CASE FILE\nRs -100 / Proof +5",
                "ABHI PUBLIC POST\nTrust +4 / Rep -4",
                "Azad supplier list turant post karta hai. Public pressure badhta hai, par incomplete proof se official case weak hota hai.",
                4, 0, -4, -3);
            objectives.Add(decision);
            labels.Add("Verified case file ya instant public post chunein");

            GameObject tender = new GameObject("Tender File Copy");
            tender.transform.position = new Vector3(12f, 0.9f, 5f);
            CreatePrimitiveChild("Tender Folder", PrimitiveType.Cube, tender.transform, Vector3.zero, new Vector3(0.9f, 0.16f, 1.15f), teal);
            objectives.Add(AddObjective(tender, "tender-copy", "Tender copy receive karein", "Records Clerk",
                "Certified copy number aur dispatch date yahan hai. Personal data cover karke case file mein lagaiye.",
                1, -50, 2, false, 4));
            labels.Add("Records office se certified tender copy lein");

            GameObject samrat = CreatePerson("Constable Samrat", new Vector3(7f, 0f, 8f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            objectives.Add(AddObjective(samrat, "evidence-chain", "Samrat ko sealed evidence dein", "Constable Samrat",
                "Batch box, stock photo aur tender copy receive ho gaye. Main seizure memo aur complaint number banwata hoon.",
                1, 0, 2, false, 3));
            labels.Add("Samrat ke saath healthcare evidence chain complete karein");

            GameObject shantiRecovery = CreatePerson("Shanti Recovery", new Vector3(0f, 0f, 15f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shantiRecovery.transform, shantiDress);
            objectives.Add(AddObjective(shantiRecovery, "recovery-promise", "Shanti ke saath baithen", "Shanti",
                "Humne apna bachcha khoya hai, par main tumhare saath hoon. Charity se jaan bachti hai; system badlega toh har ghar bachega.",
                3, 0, 2, false));
            labels.Add("Recovery garden mein Shanti ke saath agla kadam tay karein");

            mission.Configure(
                "Dawa Ka Sach",
                objectives,
                labels,
                "CHAPTER 5 COMPLETE",
                "Shanti stable hai. Verified healthcare case file ne Azad ke system-change sankalp ko janam diya.");
            mission.ConfigureMilestones(
                new List<int> { 5, 8, 11 },
                new List<string> { "EMERGENCY SUPPORT READY", "DIFFICULT TRUTH", "CASE FILE SEALED" },
                new List<string>
                {
                    "Transport, triage, consent aur donors ready hain. Ab pharmacy records verify karo.",
                    "Shanti stable hai, par family ne pregnancy kho di. Gusse ko verified case mein badalna hoga.",
                    "Tender aur medicine evidence sealed hai. Recovery garden mein Shanti intezar kar rahi hai."
                });
            mission.ConfigureChapter(5, "Chapter06");
            mission.ConfigureIntro(
                "CHAPTER 5 / DAWA KA SACH",
                "Rescue ke baad ek nayi emergency. Shanti ki jaan, documents aur sach tino sambhalo.");
        }

        private static void CreateHospitalLighting()
        {
            GameObject lightObject = new GameObject("Hospital Morning Light");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(0.86f, 0.92f, 1f);
            sunlight.intensity = 0.94f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.42f;
            lightObject.transform.rotation = Quaternion.Euler(38f, -18f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.50f, 0.66f, 0.78f);
            RenderSettings.ambientEquatorColor = new Color(0.54f, 0.58f, 0.58f);
            RenderSettings.ambientGroundColor = new Color(0.20f, 0.23f, 0.24f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.66f, 0.72f, 0.76f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 42f;
            RenderSettings.fogEndDistance = 120f;
        }
    }
}

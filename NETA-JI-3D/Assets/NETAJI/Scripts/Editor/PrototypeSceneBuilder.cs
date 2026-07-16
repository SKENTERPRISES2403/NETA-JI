using System;
using System.Collections.Generic;
using System.IO;
using NetaJi.Prototype;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private const string ScenePath = "Assets/NETAJI/Scenes/Prototype01.unity";
        private const string ChapterTwoScenePath = "Assets/NETAJI/Scenes/Chapter02.unity";
        private const string ChapterThreeScenePath = "Assets/NETAJI/Scenes/Chapter03.unity";
        private const string ChapterFourScenePath = "Assets/NETAJI/Scenes/Chapter04.unity";
        private const string ChapterFiveScenePath = "Assets/NETAJI/Scenes/Chapter05.unity";
        private const string ChapterSixScenePath = "Assets/NETAJI/Scenes/Chapter06.unity";
        private const string ChapterSevenScenePath = "Assets/NETAJI/Scenes/Chapter07.unity";
        private const string ChapterEightScenePath = "Assets/NETAJI/Scenes/Chapter08.unity";
        private const string ChapterNineScenePath = "Assets/NETAJI/Scenes/Chapter09.unity";
        private const string ChapterTenScenePath = "Assets/NETAJI/Scenes/Chapter10.unity";
        private const string ChapterElevenScenePath = "Assets/NETAJI/Scenes/Chapter11.unity";
        private const string ChapterTwelveScenePath = "Assets/NETAJI/Scenes/Chapter12.unity";
        private const string ChapterThirteenScenePath = "Assets/NETAJI/Scenes/Chapter13.unity";
        private const string ChapterFourteenScenePath = "Assets/NETAJI/Scenes/Chapter14.unity";
        private const string ChapterFifteenScenePath = "Assets/NETAJI/Scenes/Chapter15.unity";
        private const string ChapterSixteenScenePath = "Assets/NETAJI/Scenes/Chapter16.unity";
        private const string ChapterSeventeenScenePath = "Assets/NETAJI/Scenes/Chapter17.unity";
        private const string ChapterEighteenScenePath = "Assets/NETAJI/Scenes/Chapter18.unity";
        private const string ChapterNineteenScenePath = "Assets/NETAJI/Scenes/Chapter19.unity";
        private const string ChapterTwentyScenePath = "Assets/NETAJI/Scenes/Chapter20.unity";
        private const string ChapterTwentyOneScenePath = "Assets/NETAJI/Scenes/Chapter21.unity";
        private const string ChapterTwentyTwoScenePath = "Assets/NETAJI/Scenes/Chapter22.unity";
        private const string ChapterTwentyThreeScenePath = "Assets/NETAJI/Scenes/Chapter23.unity";
        private const string ChapterTwentyFourScenePath = "Assets/NETAJI/Scenes/Chapter24.unity";
        private const string MenuScenePath = "Assets/NETAJI/Scenes/MainMenu.unity";
        private const string PrologueScenePath = "Assets/NETAJI/Scenes/Prologue.unity";
        private const string FreeRoamScenePath = "Assets/NETAJI/Scenes/FreeRoam.unity";
        private const string MaterialPath = "Assets/NETAJI/Materials";
        private static readonly string[] BuildScenes = { MenuScenePath, PrologueScenePath, FreeRoamScenePath, ScenePath, ChapterTwoScenePath, ChapterThreeScenePath, ChapterFourScenePath, ChapterFiveScenePath, ChapterSixScenePath, ChapterSevenScenePath, ChapterEightScenePath, ChapterNineScenePath, ChapterTenScenePath, ChapterElevenScenePath, ChapterTwelveScenePath, ChapterThirteenScenePath, ChapterFourteenScenePath, ChapterFifteenScenePath, ChapterSixteenScenePath, ChapterSeventeenScenePath, ChapterEighteenScenePath, ChapterNineteenScenePath, ChapterTwentyScenePath, ChapterTwentyOneScenePath, ChapterTwentyTwoScenePath, ChapterTwentyThreeScenePath, ChapterTwentyFourScenePath };

        [MenuItem("NETA JI/Build Prototype Scene")]
        public static void Build()
        {
            EnsureFolder("Assets/NETAJI/Scenes");
            EnsureFolder(MaterialPath);

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            ConfigureProject();

            Material sand = CreateMaterial("GhatSand", new Color(0.64f, 0.55f, 0.40f), 0.08f);
            Material stone = CreateMaterial("GhatStone", new Color(0.50f, 0.52f, 0.49f), 0.14f);
            Material darkStone = CreateMaterial("DarkStone", new Color(0.22f, 0.25f, 0.24f), 0.12f);
            Material water = CreateMaterial("RiverWater", new Color(0.11f, 0.42f, 0.54f), 0.42f);
            Material teal = CreateMaterial("HelpersTeal", new Color(0.04f, 0.36f, 0.38f), 0.18f);
            Material yellow = CreateMaterial("HelpersYellow", new Color(0.93f, 0.64f, 0.12f), 0.12f);
            Material shirt = CreateMaterial("AzadShirt", new Color(0.67f, 0.81f, 0.84f), 0.06f);
            Material trousers = CreateMaterial("AzadTrousers", new Color(0.12f, 0.20f, 0.28f), 0.08f);
            Material skin = CreateMaterial("Skin", new Color(0.52f, 0.31f, 0.21f), 0.02f);
            Material hair = CreateMaterial("Hair", new Color(0.05f, 0.04f, 0.035f), 0.01f);
            Material white = CreateMaterial("EyeWhite", new Color(0.92f, 0.94f, 0.90f), 0.04f);
            Material shantiDress = CreateMaterial("ShantiDress", new Color(0.76f, 0.20f, 0.27f), 0.08f);
            Material sandhyaDress = CreateMaterial("SandhyaDress", new Color(0.18f, 0.48f, 0.76f), 0.08f);
            Material policeKhaki = CreateMaterial("PoliceKhaki", new Color(0.58f, 0.47f, 0.30f), 0.06f);
            Material volunteerDress = CreateMaterial("VolunteerDress", new Color(0.90f, 0.74f, 0.24f), 0.08f);
            Material foliage = CreateMaterial("Foliage", new Color(0.16f, 0.39f, 0.22f), 0.03f);
            Material trunk = CreateMaterial("Trunk", new Color(0.28f, 0.16f, 0.08f), 0.03f);
            Material litter = CreateMaterial("Litter", new Color(0.72f, 0.28f, 0.16f), 0.02f);

            GameObject environment = new GameObject("Daraganj Ghat Greybox");
            CreateEnvironment(environment.transform, sand, stone, darkStone, water, teal, yellow, white, foliage, trunk);

            GameObject systems = new GameObject("Prototype Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(1, 35, 950, 16);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad", new Vector3(0f, 0f, -5f), shirt, trousers, skin, hair, true);
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
            gameCamera.farClipPlane = 180f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.6f, -10f);
            controller.SetCamera(cameraObject.transform);

            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject shanti = CreatePerson("Shanti", new Vector3(2.8f, 0f, -0.8f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            MissionObjective shantiObjective = AddObjective(
                shanti,
                "talk-shanti",
                "Shanti se baat karein",
                "Shanti",
                "Azad, aaj teen jagah kachra jama hai. Main volunteers ko gloves de rahi hoon, tum pehla round dekh lo.",
                1,
                0,
                0,
                false);
            objectives.Add(shantiObjective);
            labels.Add("Shanti se safai route samjhein");

            MissionObjective litterOne = CreateLitterObjective(
                "Litter Cluster A",
                new Vector3(-4.5f, -0.05f, 10.2f),
                litter,
                "clean-a",
                "Pehla kachra uthayein",
                "Azad",
                "Plastic alag rakho, baaki wet waste bag mein. Safai photo ke liye nahi, aadat ke liye honi chahiye.");
            objectives.Add(litterOne);
            labels.Add("Ghat ka pehla litter cluster saaf karein");

            MissionObjective litterTwo = CreateLitterObjective(
                "Litter Cluster B",
                new Vector3(0.4f, -0.40f, 13.2f),
                litter,
                "clean-b",
                "Doosra kachra uthayein",
                "Local Volunteer",
                "Bhaiya, yahan roz dustbin bhar jaata hai. Kal ward office mein replacement ki application bhi denge.");
            objectives.Add(litterTwo);
            labels.Add("Doosra litter cluster saaf karein");

            MissionObjective litterThree = CreateLitterObjective(
                "Litter Cluster C",
                new Vector3(4.8f, -0.76f, 16.2f),
                litter,
                "clean-c",
                "Teesra kachra uthayein",
                "Azad",
                "Bas aakhri bag. Is baar shopkeepers ke saath weekly rota bhi banana hoga.");
            objectives.Add(litterThree);
            labels.Add("Aakhri litter cluster saaf karein");

            GameObject coordinator = CreatePerson("Volunteer Coordinator", new Vector3(7f, 0f, 9.2f), volunteerDress, darkStone, skin, hair, false);
            CreatePrimitiveChild("Helpers Hand Badge", PrimitiveType.Cube, coordinator.transform, new Vector3(0f, 1.25f, 0.48f), new Vector3(0.20f, 0.20f, 0.04f), teal);
            MissionObjective coordinatorObjective = AddObjective(
                coordinator,
                "report-complete",
                "Volunteer ko report dein",
                "Volunteer Coordinator",
                "Kaam ho gaya, Azad bhaiya. Agle Sunday do aur galiyon ko jodte hain. Chhota kaam hai, par log dekh rahe hain.",
                7,
                -50,
                3,
                false);
            objectives.Add(coordinatorObjective);
            labels.Add("Volunteer coordinator ko update dein");

            GameObject sandhya = CreatePerson("Sandhya", new Vector3(-8f, 0f, -9.4f), sandhyaDress, darkStone, skin, hair, false);
            AddPigtails(sandhya.transform, hair);
            sandhya.transform.localScale = Vector3.one * 0.72f;
            MissionObjective sandhyaObjective = AddObjective(
                sandhya,
                "talk-sandhya",
                "Sandhya se baat karein",
                "Sandhya",
                "Papa, Mumma ne ghar ki diary mez par rakhi hai. Aur maine Helpers Hand ke bachchon ke liye apni purani copies bhi nikaal di hain.",
                1,
                0,
                1,
                false);
            objectives.Add(sandhyaObjective);
            labels.Add("Ghar par Sandhya se milen");

            GameObject ledger = new GameObject("Household Ledger");
            ledger.transform.position = new Vector3(-7.1f, 0.72f, -10.5f);
            CreatePrimitiveChild("Ledger", PrimitiveType.Cube, ledger.transform, Vector3.zero, new Vector3(0.48f, 0.12f, 0.62f), yellow);
            MissionObjective ledgerObjective = AddObjective(
                ledger,
                "read-ledger",
                "Ghar ki diary dekhein",
                "Shanti ka Note",
                "Aaj ki tuition fees Rs 250. Rs 150 ghar ke liye, Rs 100 NGO photocopies ke liye. Hisaab saaf rahega toh hausla bhi saaf rahega.",
                0,
                250,
                0,
                false);
            objectives.Add(ledgerObjective);
            labels.Add("Shanti ki household diary padhein");

            GameObject samrat = CreatePerson("Constable Samrat", new Vector3(8.2f, 0f, -7f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            MissionObjective samratObjective = AddObjective(
                samrat,
                "talk-samrat",
                "Constable Samrat se baat karein",
                "Constable Samrat",
                "Azad, budhiya amma ki pension file ka verification sahi hai. Main record room se entry nikalwa deta hoon; tum NGO se application aaj hi jama kara do.",
                3,
                0,
                2,
                false);
            objectives.Add(samratObjective);
            labels.Add("Constable Samrat se pension file verify karayein");

            GameObject ngoFolder = new GameObject("Helpers Hand Pension Folder");
            ngoFolder.transform.position = new Vector3(8.2f, 1.08f, -1f);
            CreatePrimitiveChild("Application File", PrimitiveType.Cube, ngoFolder.transform, Vector3.zero, new Vector3(0.52f, 0.08f, 0.68f), teal);
            MissionObjective ngoObjective = AddObjective(
                ngoFolder,
                "submit-pension-file",
                "Pension file jama karein",
                "Azad",
                "Verification lag gaya. Ab receipt amma ko deni hai aur saat din baad status check karna hai. Madad tab poori hoti hai jab kaam daftar se bahar aa jaye.",
                5,
                -100,
                3,
                false);
            objectives.Add(ngoObjective);
            labels.Add("Helpers Hand desk par pension application jama karein");

            mission.Configure(
                "Ravivaar Ki Seva: Ghat Se Ghar Tak",
                objectives,
                labels,
                "CHAPTER COMPLETE",
                "Safai, ghar aur pension file: Azad ka Sunday poora hua.");
            mission.ConfigureMilestones(
                new List<int> { 5, 7 },
                new List<string> { "GHAT ROUTE COMPLETE", "COMMUNITY CASE" },
                new List<string>
                {
                    "Safai report ho gayi. Ab ghar par Sandhya intezar kar rahi hai.",
                    "Pension verification ready hai. Constable Samrat se milna hoga."
                });
            mission.ConfigureChapter(1, "Chapter02");
            mission.ConfigureIntro("AZAD / 31 / SOCIAL WORKER", "Daraganj ka beta. Helpers Hand ka field volunteer.");

            CreateLighting();
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene, ScenePath);
            BuildChapterTwoScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterThreeScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                sandhyaDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterFourScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                sandhyaDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterFiveScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterSixScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterSevenScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterEightScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterNineScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterTenScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterElevenScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterTwelveScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterThirteenScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterFourteenScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterFifteenScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterSixteenScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterSeventeenScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterEighteenScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterNineteenScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterTwentyScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterTwentyOneScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterTwentyTwoScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterTwentyThreeScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildChapterTwentyFourScene(
                sand,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            BuildPrologueScene(
                sand, stone, darkStone, water, teal, yellow, white, shirt, trousers,
                skin, hair, shantiDress, sandhyaDress, policeKhaki, volunteerDress, foliage, trunk);
            BuildFreeRoamScene(
                sand, stone, darkStone, water, teal, yellow, white, shirt, trousers,
                skin, hair, shantiDress, sandhyaDress, policeKhaki, volunteerDress, foliage, trunk);
            BuildMainMenuScene(
                sand,
                stone,
                darkStone,
                water,
                teal,
                yellow,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                sandhyaDress,
                foliage,
                trunk);
            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(MenuScenePath, true),
                new EditorBuildSettingsScene(PrologueScenePath, true),
                new EditorBuildSettingsScene(FreeRoamScenePath, true),
                new EditorBuildSettingsScene(ScenePath, true),
                new EditorBuildSettingsScene(ChapterTwoScenePath, true),
                new EditorBuildSettingsScene(ChapterThreeScenePath, true),
                new EditorBuildSettingsScene(ChapterFourScenePath, true),
                new EditorBuildSettingsScene(ChapterFiveScenePath, true),
                new EditorBuildSettingsScene(ChapterSixScenePath, true),
                new EditorBuildSettingsScene(ChapterSevenScenePath, true),
                new EditorBuildSettingsScene(ChapterEightScenePath, true),
                new EditorBuildSettingsScene(ChapterNineScenePath, true),
                new EditorBuildSettingsScene(ChapterTenScenePath, true),
                new EditorBuildSettingsScene(ChapterElevenScenePath, true),
                new EditorBuildSettingsScene(ChapterTwelveScenePath, true),
                new EditorBuildSettingsScene(ChapterThirteenScenePath, true),
                new EditorBuildSettingsScene(ChapterFourteenScenePath, true),
                new EditorBuildSettingsScene(ChapterFifteenScenePath, true),
                new EditorBuildSettingsScene(ChapterSixteenScenePath, true),
                new EditorBuildSettingsScene(ChapterSeventeenScenePath, true),
                new EditorBuildSettingsScene(ChapterEighteenScenePath, true),
                new EditorBuildSettingsScene(ChapterNineteenScenePath, true),
                new EditorBuildSettingsScene(ChapterTwentyScenePath, true),
                new EditorBuildSettingsScene(ChapterTwentyOneScenePath, true),
                new EditorBuildSettingsScene(ChapterTwentyTwoScenePath, true),
                new EditorBuildSettingsScene(ChapterTwentyThreeScenePath, true),
                new EditorBuildSettingsScene(ChapterTwentyFourScenePath, true)
            };
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            Selection.activeGameObject = GameObject.Find("Azad");
            Debug.Log($"NETA JI open-world scenes and story chapters generated through {ChapterTwentyFourScenePath}");
        }

        private static void BuildChapterTwoScene(
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
            GameObject environment = new GameObject("Allahpur Evening Learning Courtyard");
            CreateBox("Learning Courtyard", new Vector3(0f, -0.3f, 0f), new Vector3(42f, 0.6f, 34f), stone, environment.transform);
            CreateBox("Courtyard Path", new Vector3(0f, 0.02f, -6f), new Vector3(7f, 0.08f, 22f), sand, environment.transform);
            CreateBox("Community Hall", new Vector3(0f, 2.5f, 15.5f), new Vector3(20f, 5f, 6f), teal, environment.transform);
            CreateBox("Class Blackboard", new Vector3(0f, 2.35f, 12.42f), new Vector3(7.6f, 2.55f, 0.18f), darkStone, environment.transform);
            CreateWorldLabel("Class Title", "HELPERS HAND EVENING CLASS", new Vector3(0f, 4.42f, 12.30f), Vector3.zero, yellow, environment.transform, 0.027f);
            CreateWorldLabel("Blackboard Lesson", "FORM  /  ENGLISH  /  MATHS", new Vector3(0f, 2.45f, 12.30f), Vector3.zero, white, environment.transform, 0.019f);

            for (int i = 0; i < 6; i++)
            {
                float side = i % 2 == 0 ? -1f : 1f;
                float x = side * (13f + (i % 3) * 2f);
                float z = -7f + (i / 2) * 7f;
                Material wall = i % 3 == 0 ? teal : i % 3 == 1 ? stone : shantiDress;
                CreateBox($"Allahpur Home {i + 1}", new Vector3(x, 1.9f, z), new Vector3(5f, 3.8f, 5f), wall, environment.transform);
            }

            CreateTree("Courtyard Neem Left", new Vector3(-10f, 0f, 5f), foliage, trunk, environment.transform);
            CreateTree("Courtyard Neem Right", new Vector3(10.5f, 0f, 7f), foliage, trunk, environment.transform);
            CreateBench("Parent Bench", new Vector3(-9f, 0f, -3f), 12f, darkStone, teal, environment.transform);
            CreateStreetLamp("Courtyard Solar Lamp", new Vector3(12f, 0f, 3.5f), darkStone, yellow, environment.transform);
            CreateStreetLamp("Courtyard Entry Lamp", new Vector3(-12f, 0f, -8f), darkStone, yellow, environment.transform);

            GameObject systems = new GameObject("Chapter 2 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(2, 58, 1050, 30);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad", new Vector3(0f, 0f, -9f), shirt, trousers, skin, hair, true);
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
            gameCamera.farClipPlane = 160f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetTarget(azad.transform);
            orbitCamera.SetCollisionMask(~(1 << 2));
            cameraObject.transform.position = new Vector3(0f, 3.6f, -14f);
            controller.SetCamera(cameraObject.transform);

            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject shanti = CreatePerson("Shanti", new Vector3(-5f, 0f, 4f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            objectives.Add(AddObjective(
                shanti,
                "class-plan",
                "Shanti se class plan lein",
                "Shanti",
                "Azad, aaj admission forms bhi hain aur chhote bachchon ki English class bhi. Pehle do desks aur books ready kara do, main attendance bana rahi hoon.",
                1,
                0,
                0,
                false));
            labels.Add("Shanti se evening class ka plan samjhein");

            GameObject deskA = CreateStudyDesk("Study Desk A", new Vector3(-4.5f, 0f, 3f), darkStone, yellow);
            objectives.Add(AddObjective(deskA, "desk-a", "Pehla desk lagayein", "Azad", "Desk seedha rahe, bachchon ko likhte waqt jagah milni chahiye.", 1, -25, 1, false));
            labels.Add("Pehla study desk arrange karein");

            GameObject deskB = CreateStudyDesk("Study Desk B", new Vector3(0f, 0f, 3f), darkStone, yellow);
            objectives.Add(AddObjective(deskB, "desk-b", "Doosra desk lagayein", "Volunteer", "Is desk par admission form help hogi. Pens aur documents ke clips bhi rakh dete hain.", 1, -25, 1, false));
            labels.Add("Doosra study desk arrange karein");

            GameObject books = new GameObject("Donated Book Crate");
            books.transform.position = new Vector3(5f, 0.42f, 4f);
            CreatePrimitiveChild("Crate", PrimitiveType.Cube, books.transform, Vector3.zero, new Vector3(1.25f, 0.78f, 0.92f), darkStone);
            for (int i = 0; i < 5; i++)
            {
                CreatePrimitiveChild($"Book {i + 1}", PrimitiveType.Cube, books.transform, new Vector3(-0.38f + i * 0.19f, 0.48f + (i % 2) * 0.08f, 0f), new Vector3(0.16f, 0.55f, 0.60f), i % 2 == 0 ? teal : yellow).transform.localRotation = Quaternion.Euler(0f, 0f, -8f + i * 4f);
            }
            objectives.Add(AddObjective(books, "books", "Donated books sort karein", "Azad", "Class 3 se 8 tak alag bundles. Naam likh denge, par kisi bachche ko kitab ke bina wapas nahi bhejna.", 2, 0, 1, false));
            labels.Add("Donated books ko class-wise sort karein");

            GameObject raju = CreatePerson("Student Raju", new Vector3(-2f, 0f, 7f), volunteerDress, darkStone, skin, hair, false);
            raju.transform.localScale = Vector3.one * 0.82f;
            objectives.Add(AddObjective(
                raju,
                "raju-form",
                "Raju ka admission form dekhein",
                "Raju",
                "Bhaiya, address proof mein kiraye ka paper hai. School wale bol rahe the guardian signature bhi chahiye.",
                4,
                0,
                2,
                false));
            labels.Add("Raju ka admission form complete karayein");

            GameObject samrat = CreatePerson("Constable Samrat", new Vector3(9f, 0f, -1f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            objectives.Add(AddObjective(
                samrat,
                "samrat-safety",
                "Samrat se safety update lein",
                "Constable Samrat",
                "Gali ki main light band hai. Complaint number mil gaya hai; tab tak solar lamp class ke exit par shift kara do. Bachche andhere mein nahi jayenge.",
                2,
                0,
                1,
                false));
            labels.Add("Constable Samrat se streetlight update lein");

            GameObject solarSwitch = new GameObject("Portable Solar Lamp Control");
            solarSwitch.transform.position = new Vector3(12f, 0.92f, 3.5f);
            CreatePrimitiveChild("Battery", PrimitiveType.Cube, solarSwitch.transform, Vector3.zero, new Vector3(0.72f, 0.65f, 0.52f), teal);
            CreatePrimitiveChild("Switch", PrimitiveType.Cube, solarSwitch.transform, new Vector3(0f, 0.18f, 0.28f), new Vector3(0.18f, 0.18f, 0.08f), yellow);
            objectives.Add(AddObjective(
                solarSwitch,
                "solar-lamp",
                "Solar lamp activate karein",
                "Azad",
                "Temporary light chal gayi. Complaint receipt notice board par laga denge, permanent repair ka follow-up kal hoga.",
                3,
                -150,
                2,
                false));
            labels.Add("Exit ke paas temporary solar lamp activate karein");

            GameObject teachingPoint = new GameObject("Teaching Point");
            teachingPoint.transform.position = new Vector3(0f, 1.2f, 11.6f);
            CreatePrimitiveChild("Chalk Box", PrimitiveType.Cube, teachingPoint.transform, Vector3.zero, new Vector3(0.72f, 0.18f, 0.28f), white);
            objectives.Add(AddObjective(
                teachingPoint,
                "teach-class",
                "Evening class shuru karein",
                "Azad",
                "Aaj ka pehla lesson: form bharte waqt har box padhna hai, bina samjhe sign nahi karna. Phir English reading karenge.",
                5,
                0,
                3,
                false));
            labels.Add("Blackboard par evening class shuru karein");

            GameObject coordinator = CreatePerson("Helpers Hand Coordinator", new Vector3(7f, 0f, 7f), volunteerDress, darkStone, skin, hair, false);
            CreatePrimitiveChild("Helpers Hand Badge", PrimitiveType.Cube, coordinator.transform, new Vector3(0f, 1.25f, 0.48f), new Vector3(0.20f, 0.20f, 0.04f), teal);
            objectives.Add(AddObjective(
                coordinator,
                "class-report",
                "Coordinator ko report dein",
                "Helpers Hand Coordinator",
                "Attendance 23, admission forms 4, books 17. Shanti ne tuition fund ka hisaab bhi note kar diya. Agli class Wednesday ko.",
                4,
                300,
                3,
                false));
            labels.Add("Helpers Hand coordinator ko class report dein");

            mission.Configure(
                "Shaam Ki Paathshala",
                objectives,
                labels,
                "CHAPTER 2 COMPLETE",
                "Forms, books, safety aur class: mohalla school roshan hua.");
            mission.ConfigureMilestones(
                new List<int> { 4, 7 },
                new List<string> { "ADMISSION HELP", "CLASS READY" },
                new List<string>
                {
                    "Desks aur books ready hain. Ab Raju ka admission form dekhna hai.",
                    "Safe exit light active hai. Evening class shuru ki ja sakti hai."
                });
            mission.ConfigureChapter(2, "Chapter03");
            mission.ConfigureIntro("CHAPTER 2 / SHAAM KI PAATHSHALA", "Allahpur courtyard. Forms, books aur safe evening class.");

            CreateEveningLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterTwoScenePath);
        }

        private static void BuildMainMenuScene(
            Material sand,
            Material stone,
            Material darkStone,
            Material water,
            Material teal,
            Material yellow,
            Material shirt,
            Material trousers,
            Material skin,
            Material hair,
            Material shantiDress,
            Material sandhyaDress,
            Material foliage,
            Material trunk)
        {
            Scene menuScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject world = new GameObject("Prayagraj Menu Backdrop");

            CreateBox("Menu Plaza", new Vector3(0f, -0.3f, 3f), new Vector3(46f, 0.6f, 28f), sand, world.transform);
            for (int i = 0; i < 6; i++)
            {
                CreateBox(
                    $"Menu Ghat Step {i + 1}",
                    new Vector3(0f, -0.4f - i * 0.30f, 13f + i * 2.5f),
                    new Vector3(40f, 0.45f, 2.7f),
                    stone,
                    world.transform);
            }

            CreateBox("Menu River", new Vector3(0f, -2.2f, 31f), new Vector3(90f, 0.22f, 22f), water, world.transform);
            CreateBoat("Menu Boat", new Vector3(10f, -1.86f, 29f), -14f, darkStone, yellow, world.transform);

            for (int i = 0; i < 8; i++)
            {
                float x = -18f + i * 5.2f;
                float height = 4f + (i % 4) * 0.9f;
                CreateBox($"Menu Skyline {i + 1}", new Vector3(x, height * 0.5f - 0.1f, 41f), new Vector3(4.5f, height, 7f), i % 2 == 0 ? teal : yellow, world.transform);
            }

            CreateTree("Menu Neem Left", new Vector3(-10f, 0f, 4f), foliage, trunk, world.transform);
            CreateTree("Menu Neem Right", new Vector3(14f, 0f, 6f), foliage, trunk, world.transform);
            CreateStreetLamp("Menu Lamp Left", new Vector3(-4f, 0f, 5f), darkStone, yellow, world.transform);
            CreateStreetLamp("Menu Lamp Right", new Vector3(11f, 0f, 6f), darkStone, yellow, world.transform);
            CreateBench("Menu Bench", new Vector3(12f, 0f, 1f), -18f, darkStone, teal, world.transform);

            GameObject azad = CreatePerson("Menu Azad", new Vector3(4.8f, 0f, 3f), shirt, trousers, skin, hair, true);
            azad.transform.rotation = Quaternion.Euler(0f, -18f, 0f);
            GameObject shanti = CreatePerson("Menu Shanti", new Vector3(7.2f, 0f, 4.2f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            GameObject sandhya = CreatePerson("Menu Sandhya", new Vector3(6.2f, 0f, 2.6f), sandhyaDress, darkStone, skin, hair, false);
            AddPigtails(sandhya.transform, hair);
            sandhya.transform.localScale = Vector3.one * 0.72f;

            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            Camera camera = cameraObject.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.Skybox;
            camera.fieldOfView = 58f;
            camera.nearClipPlane = 0.12f;
            camera.farClipPlane = 180f;
            camera.allowHDR = false;
            camera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();
            cameraObject.transform.position = new Vector3(0f, 3.1f, -9.5f);
            cameraObject.transform.LookAt(new Vector3(5.5f, 1.2f, 4f));

            GameObject systems = new GameObject("Main Menu Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<MainMenuController>();
            CreateLighting();

            EditorSceneManager.MarkSceneDirty(menuScene);
            EditorSceneManager.SaveScene(menuScene, MenuScenePath);
        }

        public static void BuildFromCommandLine()
        {
            try
            {
                Build();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                EditorApplication.Exit(1);
            }
        }

        [MenuItem("NETA JI/Build Windows Prototype")]
        public static void BuildWindowsPrototype()
        {
            Build();
            Directory.CreateDirectory("Builds/Windows");
            BuildPlayerOptions options = new BuildPlayerOptions
            {
                scenes = BuildScenes,
                locationPathName = "Builds/Windows/NETA-JI-Prototype.exe",
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.None
            };
            BuildPipeline.BuildPlayer(options);
        }

        [MenuItem("NETA JI/Build Android Prototype")]
        public static void BuildAndroidPrototype()
        {
            Build();
            Directory.CreateDirectory("Builds/Android");
            BuildPlayerOptions options = new BuildPlayerOptions
            {
                scenes = BuildScenes,
                locationPathName = "Builds/Android/NETA-JI-Prototype.apk",
                target = BuildTarget.Android,
                options = BuildOptions.None
            };
            BuildPipeline.BuildPlayer(options);
        }

        private static void ConfigureProject()
        {
            PlayerSettings.companyName = "SK Enterprises";
            PlayerSettings.productName = "NETA JI";
            PlayerSettings.bundleVersion = "0.34.0";
            PlayerSettings.colorSpace = ColorSpace.Gamma;
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
            PlayerSettings.allowedAutorotateToPortrait = false;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.allowedAutorotateToLandscapeLeft = true;
            PlayerSettings.allowedAutorotateToLandscapeRight = true;
            PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, "com.skenterprises.netaji.prototype");
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel26;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
            PlayerSettings.Android.bundleVersionCode = 34;
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetManagedStrippingLevel(NamedBuildTarget.Android, ManagedStrippingLevel.Medium);
            QualitySettings.vSyncCount = 0;
            QualitySettings.shadowDistance = 24f;
            QualitySettings.antiAliasing = 0;
        }

        private static void CreateEnvironment(
            Transform root,
            Material sand,
            Material stone,
            Material darkStone,
            Material water,
            Material teal,
            Material yellow,
            Material white,
            Material foliage,
            Material trunk)
        {
            CreateBox("Upper Plaza", new Vector3(0f, -0.3f, -4f), new Vector3(42f, 0.6f, 26f), sand, root);
            CreateBox("Stone Lane", new Vector3(0f, 0.02f, -12f), new Vector3(8f, 0.08f, 20f), stone, root);

            for (int i = 0; i < 8; i++)
            {
                float y = -0.25f - i * 0.36f;
                float z = 9.5f + i * 3f;
                CreateBox($"Ghat Step {i + 1}", new Vector3(0f, y, z), new Vector3(34f, 0.5f, 3.25f), stone, root);
            }

            CreateBox("River", new Vector3(0f, -3.18f, 38f), new Vector3(90f, 0.24f, 24f), water, root);
            CreateBox("Left Ghat Parapet", new Vector3(-18f, 0.42f, 5f), new Vector3(0.75f, 0.85f, 48f), darkStone, root);
            CreateBox("Right Ghat Parapet", new Vector3(18f, 0.42f, 5f), new Vector3(0.75f, 0.85f, 48f), darkStone, root);

            CreateBox("Far River Bank", new Vector3(0f, -3f, 55f), new Vector3(90f, 1f, 14f), sand, root);
            for (int i = 0; i < 5; i++)
            {
                CreateTree($"Far Bank Tree {i + 1}", new Vector3(-24f + i * 12f, -2.5f, 53f + (i % 2) * 2f), foliage, trunk, root);
            }

            CreateBoat("Wooden Boat A", new Vector3(-10f, -2.82f, 38f), 12f, darkStone, yellow, root);
            CreateBoat("Wooden Boat B", new Vector3(12f, -2.82f, 44f), -18f, teal, sand, root);

            for (int i = 0; i < 7; i++)
            {
                float x = -15f + i * 5f;
                float height = 4.2f + (i % 3) * 0.9f;
                Material wall = i % 2 == 0 ? teal : yellow;
                CreateBox($"Local Building {i + 1}", new Vector3(x, height * 0.5f - 0.05f, -18f), new Vector3(4.3f, height, 8f), wall, root);
                CreateBox($"Building Shade {i + 1}", new Vector3(x, 2.25f, -13.8f), new Vector3(3.1f, 0.22f, 1.6f), darkStone, root);
            }

            for (int i = 0; i < 6; i++)
            {
                float side = i % 2 == 0 ? -1f : 1f;
                float x = side * (11.5f + (i % 3) * 1.5f);
                float z = -8f + (i / 2) * 7f;
                CreateTree($"Neem Tree {i + 1}", new Vector3(x, 0f, z), foliage, trunk, root);
            }

            CreateBox("Helpers Hand Table", new Vector3(8.2f, 0.45f, -1f), new Vector3(2.6f, 0.16f, 1.2f), teal, root);
            CreateBox("Donation Box", new Vector3(8.2f, 0.86f, -1f), new Vector3(0.62f, 0.66f, 0.48f), yellow, root);
            CreateBox("Helpers Hand Backboard", new Vector3(8.2f, 1.46f, -1.72f), new Vector3(4.1f, 1.75f, 0.18f), teal, root);
            CreateWorldLabel("Helpers Hand Sign", "HELPERS HAND", new Vector3(8.2f, 1.72f, -1.60f), Vector3.zero, yellow, root, 0.028f);
            CreateWorldLabel("Helpers Hand Service", "PENSION  /  EDUCATION  /  SEVA", new Vector3(8.2f, 1.28f, -1.60f), Vector3.zero, white, root, 0.013f);

            CreateBox("Azad Home Facade", new Vector3(-8f, 2f, -12.8f), new Vector3(5.4f, 4f, 3.6f), sand, root);
            CreateBox("Azad Home Door", new Vector3(-8f, 1.05f, -10.95f), new Vector3(1.15f, 2.1f, 0.12f), darkStone, root);
            CreateBox("Azad Home Awning", new Vector3(-8f, 2.45f, -10.55f), new Vector3(2.8f, 0.16f, 1.05f), teal, root);
            CreateBox("Home Ledger Table", new Vector3(-7.1f, 0.48f, -10.5f), new Vector3(1.5f, 0.12f, 0.9f), darkStone, root);
            CreateWorldLabel("Azad Home Nameplate", "AZAD  /  SHANTI", new Vector3(-8f, 2.82f, -10.88f), Vector3.zero, darkStone, root, 0.020f);

            CreateBox("Daraganj Sign Board", new Vector3(-11.7f, 1.78f, 4.5f), new Vector3(6.2f, 2.15f, 0.20f), darkStone, root);
            CreateWorldLabel("Daraganj Sign", "DARAGANJ GHAT", new Vector3(-11.7f, 2.02f, 4.37f), Vector3.zero, yellow, root, 0.030f);
            CreateWorldLabel("Daraganj Subsign", "PRAYAGRAJ", new Vector3(-11.7f, 1.47f, 4.37f), Vector3.zero, white, root, 0.018f);

            CreateBench("Public Bench Left", new Vector3(-10.5f, 0f, -1f), 18f, darkStone, yellow, root);
            CreateBench("Public Bench Right", new Vector3(11f, 0f, 3f), -12f, darkStone, teal, root);
            CreateStreetLamp("Ghat Lamp A", new Vector3(-14.5f, 0f, -5f), darkStone, yellow, root);
            CreateStreetLamp("Ghat Lamp B", new Vector3(14.5f, 0f, -3f), darkStone, yellow, root);
            CreateStreetLamp("Ghat Lamp C", new Vector3(-15f, -0.45f, 14f), darkStone, yellow, root);
            CreateStreetLamp("Ghat Lamp D", new Vector3(15f, -0.45f, 14f), darkStone, yellow, root);

            for (int i = 0; i < 4; i++)
            {
                CreateBox($"Safety Bollard {i + 1}", new Vector3(-6f + i * 4f, 0.45f, 5.2f), new Vector3(0.3f, 0.9f, 0.3f), darkStone, root);
            }
        }

        private static void CreateBoat(
            string name,
            Vector3 position,
            float yaw,
            Material hull,
            Material trim,
            Transform parent)
        {
            GameObject boat = new GameObject(name);
            boat.transform.SetParent(parent);
            boat.transform.position = position;
            boat.transform.rotation = Quaternion.Euler(0f, yaw, 0f);

            CreatePrimitiveChild("Hull", PrimitiveType.Cube, boat.transform, Vector3.zero, new Vector3(3.7f, 0.34f, 1.2f), hull);
            CreatePrimitiveChild("Bow", PrimitiveType.Cube, boat.transform, new Vector3(1.85f, 0.1f, 0f), new Vector3(0.9f, 0.3f, 0.85f), hull);
            CreatePrimitiveChild("Bench Front", PrimitiveType.Cube, boat.transform, new Vector3(0.8f, 0.32f, 0f), new Vector3(0.18f, 0.12f, 1.05f), trim);
            CreatePrimitiveChild("Bench Rear", PrimitiveType.Cube, boat.transform, new Vector3(-0.8f, 0.32f, 0f), new Vector3(0.18f, 0.12f, 1.05f), trim);
            WorldMotion motion = boat.AddComponent<WorldMotion>();
            motion.Configure(WorldMotionKind.Float, 0.11f, 1.25f, Vector3.up);
        }

        private static MissionObjective CreateLitterObjective(
            string name,
            Vector3 position,
            Material material,
            string id,
            string prompt,
            string speaker,
            string dialogue)
        {
            GameObject root = new GameObject(name);
            root.transform.position = position;
            BoxCollider trigger = root.AddComponent<BoxCollider>();
            trigger.isTrigger = true;
            trigger.size = new Vector3(1.6f, 1.2f, 1.6f);

            for (int i = 0; i < 5; i++)
            {
                GameObject piece = CreatePrimitiveChild(
                    $"Discarded Item {i + 1}",
                    PrimitiveType.Cube,
                    root.transform,
                    new Vector3((i - 2) * 0.24f, 0.08f + (i % 2) * 0.08f, (i % 3) * 0.16f),
                    new Vector3(0.34f, 0.12f, 0.22f),
                    material);
                piece.transform.localRotation = Quaternion.Euler(0f, i * 31f, i * 8f);
            }

            return AddObjective(root, id, prompt, speaker, dialogue, 2, 0, 1, true);
        }

        private static MissionObjective AddObjective(
            GameObject target,
            string id,
            string prompt,
            string speaker,
            string dialogue,
            int trust,
            int money,
            int reputation,
            bool hideWhenDone,
            int proof = 0)
        {
            Collider existingCollider = target.GetComponent<Collider>();
            if (existingCollider == null)
            {
                CapsuleCollider trigger = target.AddComponent<CapsuleCollider>();
                trigger.center = new Vector3(0f, 0.9f, 0f);
                trigger.height = 2.1f;
                trigger.radius = 0.55f;
                trigger.isTrigger = true;
            }
            else
            {
                existingCollider.isTrigger = true;
            }

            MissionObjective objective = target.AddComponent<MissionObjective>();
            objective.Configure(id, prompt, speaker, dialogue, trust, money, reputation, hideWhenDone, proof);
            target.AddComponent<ObjectiveBeacon>();
            return objective;
        }

        private static GameObject CreatePerson(
            string name,
            Vector3 position,
            Material top,
            Material lower,
            Material skin,
            Material hair,
            bool player)
        {
            GameObject root = new GameObject(name);
            root.transform.position = position;

            CreatePrimitiveChild("Torso", PrimitiveType.Capsule, root.transform, new Vector3(0f, 1.1f, 0f), new Vector3(0.72f, 0.56f, 0.52f), top);
            CreatePrimitiveChild("Leg Left", PrimitiveType.Capsule, root.transform, new Vector3(-0.18f, 0.42f, 0f), new Vector3(0.24f, 0.46f, 0.24f), lower);
            CreatePrimitiveChild("Leg Right", PrimitiveType.Capsule, root.transform, new Vector3(0.18f, 0.42f, 0f), new Vector3(0.24f, 0.46f, 0.24f), lower);
            CreatePrimitiveChild("Arm Left", PrimitiveType.Capsule, root.transform, new Vector3(-0.43f, 1.08f, 0f), new Vector3(0.17f, 0.42f, 0.17f), top);
            CreatePrimitiveChild("Arm Right", PrimitiveType.Capsule, root.transform, new Vector3(0.43f, 1.08f, 0f), new Vector3(0.17f, 0.42f, 0.17f), top);
            CreatePrimitiveChild("Head", PrimitiveType.Sphere, root.transform, new Vector3(0f, 1.78f, 0f), new Vector3(0.42f, 0.48f, 0.42f), skin);
            CreatePrimitiveChild("Hair", PrimitiveType.Sphere, root.transform, new Vector3(0f, 1.96f, -0.015f), new Vector3(0.43f, 0.19f, 0.43f), hair);
            CreatePrimitiveChild("Shoulder Bag", PrimitiveType.Cube, root.transform, new Vector3(0.43f, 0.72f, -0.16f), new Vector3(0.20f, 0.40f, 0.32f), player ? lower : top);
            if (player)
            {
                GameObject bagStrap = CreatePrimitiveChild(
                    "Shoulder Bag Strap", PrimitiveType.Cube, root.transform,
                    new Vector3(0f, 1.23f, 0.34f), new Vector3(0.07f, 0.92f, 0.05f), lower);
                bagStrap.transform.localRotation = Quaternion.Euler(0f, 0f, -27f);
            }
            CreatePrimitiveChild("Belt", PrimitiveType.Cube, root.transform, new Vector3(0f, 0.78f, 0f), new Vector3(0.68f, 0.10f, 0.54f), lower);
            CreatePrimitiveChild("Shoe Left", PrimitiveType.Cube, root.transform, new Vector3(-0.18f, 0.08f, 0.10f), new Vector3(0.25f, 0.15f, 0.42f), lower);
            CreatePrimitiveChild("Shoe Right", PrimitiveType.Cube, root.transform, new Vector3(0.18f, 0.08f, 0.10f), new Vector3(0.25f, 0.15f, 0.42f), lower);
            CreatePrimitiveChild("Hand Left", PrimitiveType.Sphere, root.transform, new Vector3(-0.43f, 0.72f, 0f), new Vector3(0.18f, 0.20f, 0.18f), skin);
            CreatePrimitiveChild("Hand Right", PrimitiveType.Sphere, root.transform, new Vector3(0.43f, 0.72f, 0f), new Vector3(0.18f, 0.20f, 0.18f), skin);
            CreatePrimitiveChild("Eye Left", PrimitiveType.Sphere, root.transform, new Vector3(-0.12f, 1.82f, 0.215f), new Vector3(0.055f, 0.065f, 0.035f), hair);
            CreatePrimitiveChild("Eye Right", PrimitiveType.Sphere, root.transform, new Vector3(0.12f, 1.82f, 0.215f), new Vector3(0.055f, 0.065f, 0.035f), hair);
            CreatePrimitiveChild("Nose", PrimitiveType.Sphere, root.transform, new Vector3(0f, 1.70f, 0.225f), new Vector3(0.075f, 0.11f, 0.07f), skin);
            CreatePrimitiveChild("Mouth", PrimitiveType.Cube, root.transform, new Vector3(0f, 1.59f, 0.22f), new Vector3(0.13f, 0.028f, 0.025f), hair);
            CreatePrimitiveChild("Collar Left", PrimitiveType.Cube, root.transform, new Vector3(-0.14f, 1.45f, 0.285f), new Vector3(0.20f, 0.18f, 0.04f), top).transform.localRotation = Quaternion.Euler(0f, 0f, -24f);
            CreatePrimitiveChild("Collar Right", PrimitiveType.Cube, root.transform, new Vector3(0.14f, 1.45f, 0.285f), new Vector3(0.20f, 0.18f, 0.04f), top).transform.localRotation = Quaternion.Euler(0f, 0f, 24f);
            if (player)
            {
                CreatePrimitiveChild("Moustache", PrimitiveType.Cube, root.transform, new Vector3(0f, 1.64f, 0.23f), new Vector3(0.19f, 0.038f, 0.028f), hair);
                CreatePrimitiveChild("Wrist Watch", PrimitiveType.Cylinder, root.transform, new Vector3(-0.43f, 0.77f, 0f), new Vector3(0.10f, 0.04f, 0.10f), lower).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            }
            return root;
        }

        private static void CreateTree(string name, Vector3 position, Material foliage, Material trunk, Transform parent)
        {
            GameObject root = new GameObject(name);
            root.transform.SetParent(parent);
            root.transform.position = position;
            CreatePrimitiveChild("Trunk", PrimitiveType.Cylinder, root.transform, new Vector3(0f, 1.45f, 0f), new Vector3(0.34f, 1.45f, 0.34f), trunk, true);
            CreatePrimitiveChild("Canopy A", PrimitiveType.Sphere, root.transform, new Vector3(0f, 3.25f, 0f), new Vector3(2.2f, 1.35f, 2.1f), foliage);
            CreatePrimitiveChild("Canopy B", PrimitiveType.Sphere, root.transform, new Vector3(0.8f, 3.1f, 0.1f), new Vector3(1.35f, 1.05f, 1.35f), foliage);
            WorldMotion motion = root.AddComponent<WorldMotion>();
            motion.Configure(WorldMotionKind.Sway, 1.4f, 0.72f, Vector3.forward);
        }

        private static void AddScarf(Transform person, Material material)
        {
            CreatePrimitiveChild("Dupatta Left", PrimitiveType.Cube, person, new Vector3(-0.38f, 1.06f, 0.08f), new Vector3(0.12f, 0.78f, 0.48f), material).transform.localRotation = Quaternion.Euler(0f, 0f, -5f);
            CreatePrimitiveChild("Dupatta Right", PrimitiveType.Cube, person, new Vector3(0.38f, 1.06f, 0.08f), new Vector3(0.12f, 0.78f, 0.48f), material).transform.localRotation = Quaternion.Euler(0f, 0f, 5f);
        }

        private static void AddPigtails(Transform person, Material hair)
        {
            CreatePrimitiveChild("Pigtail Left", PrimitiveType.Sphere, person, new Vector3(-0.38f, 1.82f, -0.02f), new Vector3(0.25f, 0.38f, 0.25f), hair);
            CreatePrimitiveChild("Pigtail Right", PrimitiveType.Sphere, person, new Vector3(0.38f, 1.82f, -0.02f), new Vector3(0.25f, 0.38f, 0.25f), hair);
        }

        private static void AddPoliceDetails(Transform person, Material khaki, Material dark)
        {
            CreatePrimitiveChild("Police Cap", PrimitiveType.Cube, person, new Vector3(0f, 2.08f, 0f), new Vector3(0.58f, 0.16f, 0.55f), khaki);
            CreatePrimitiveChild("Cap Brim", PrimitiveType.Cube, person, new Vector3(0f, 2.02f, 0.30f), new Vector3(0.52f, 0.06f, 0.30f), dark);
            CreatePrimitiveChild("Name Badge", PrimitiveType.Cube, person, new Vector3(0.22f, 1.25f, 0.48f), new Vector3(0.26f, 0.10f, 0.04f), dark);
        }

        private static void CreateBench(string name, Vector3 position, float yaw, Material frame, Material seat, Transform parent)
        {
            GameObject bench = new GameObject(name);
            bench.transform.SetParent(parent);
            bench.transform.position = position;
            bench.transform.rotation = Quaternion.Euler(0f, yaw, 0f);
            CreatePrimitiveChild("Seat", PrimitiveType.Cube, bench.transform, new Vector3(0f, 0.65f, 0f), new Vector3(2.6f, 0.18f, 0.72f), seat);
            CreatePrimitiveChild("Back", PrimitiveType.Cube, bench.transform, new Vector3(0f, 1.05f, -0.31f), new Vector3(2.6f, 0.62f, 0.14f), seat);
            CreatePrimitiveChild("Leg Left", PrimitiveType.Cube, bench.transform, new Vector3(-0.95f, 0.32f, 0f), new Vector3(0.16f, 0.64f, 0.56f), frame);
            CreatePrimitiveChild("Leg Right", PrimitiveType.Cube, bench.transform, new Vector3(0.95f, 0.32f, 0f), new Vector3(0.16f, 0.64f, 0.56f), frame);
        }

        private static GameObject CreateStudyDesk(string name, Vector3 position, Material frame, Material surface)
        {
            GameObject desk = new GameObject(name);
            desk.transform.position = position;
            CreatePrimitiveChild("Writing Surface", PrimitiveType.Cube, desk.transform, new Vector3(0f, 0.72f, 0f), new Vector3(2.4f, 0.16f, 0.85f), surface);
            CreatePrimitiveChild("Frame Left", PrimitiveType.Cube, desk.transform, new Vector3(-0.92f, 0.36f, 0f), new Vector3(0.14f, 0.72f, 0.64f), frame);
            CreatePrimitiveChild("Frame Right", PrimitiveType.Cube, desk.transform, new Vector3(0.92f, 0.36f, 0f), new Vector3(0.14f, 0.72f, 0.64f), frame);
            CreatePrimitiveChild("Notebook", PrimitiveType.Cube, desk.transform, new Vector3(0f, 0.84f, 0f), new Vector3(0.62f, 0.05f, 0.48f), frame);
            return desk;
        }

        private static void CreateStreetLamp(string name, Vector3 position, Material pole, Material lamp, Transform parent)
        {
            GameObject root = new GameObject(name);
            root.transform.SetParent(parent);
            root.transform.position = position;
            CreatePrimitiveChild("Pole", PrimitiveType.Cylinder, root.transform, new Vector3(0f, 1.9f, 0f), new Vector3(0.11f, 1.9f, 0.11f), pole);
            CreatePrimitiveChild("Lamp", PrimitiveType.Sphere, root.transform, new Vector3(0f, 3.95f, 0f), new Vector3(0.36f, 0.30f, 0.36f), lamp);
            CreatePrimitiveChild("Cap", PrimitiveType.Cube, root.transform, new Vector3(0f, 4.23f, 0f), new Vector3(0.52f, 0.10f, 0.52f), pole);
        }

        private static void CreateWorldLabel(string name, string value, Vector3 position, Vector3 rotation, Material material, Transform parent, float size)
        {
            GameObject label = new GameObject(name);
            label.transform.SetParent(parent);
            label.transform.position = position;
            label.transform.rotation = Quaternion.Euler(rotation);
            TextMesh text = label.AddComponent<TextMesh>();
            text.text = value;
            text.anchor = TextAnchor.MiddleCenter;
            text.alignment = TextAlignment.Center;
            text.fontSize = 72;
            text.characterSize = size;
            text.fontStyle = FontStyle.Bold;
            text.color = material.color;
        }

        private static GameObject CreateBox(string name, Vector3 position, Vector3 scale, Material material, Transform parent)
        {
            GameObject item = GameObject.CreatePrimitive(PrimitiveType.Cube);
            item.name = name;
            item.transform.SetParent(parent);
            item.transform.position = position;
            item.transform.localScale = scale;
            item.GetComponent<Renderer>().sharedMaterial = material;
            return item;
        }

        private static GameObject CreatePrimitiveChild(
            string name,
            PrimitiveType primitive,
            Transform parent,
            Vector3 localPosition,
            Vector3 localScale,
            Material material,
            bool keepCollider = false)
        {
            GameObject item = GameObject.CreatePrimitive(primitive);
            item.name = name;
            item.transform.SetParent(parent);
            item.transform.localPosition = localPosition;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localScale = localScale;
            item.GetComponent<Renderer>().sharedMaterial = material;

            if (!keepCollider)
            {
                Collider collider = item.GetComponent<Collider>();
                if (collider != null)
                {
                    Object.DestroyImmediate(collider);
                }
            }

            return item;
        }

        private static void CreateLighting()
        {
            GameObject lightObject = new GameObject("Warm Morning Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.88f, 0.70f);
            sunlight.intensity = 1.05f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.56f;
            lightObject.transform.rotation = Quaternion.Euler(44f, -32f, 0f);

            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.48f, 0.66f, 0.75f);
            RenderSettings.ambientEquatorColor = new Color(0.56f, 0.51f, 0.39f);
            RenderSettings.ambientGroundColor = new Color(0.21f, 0.22f, 0.19f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.64f, 0.73f, 0.75f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 45f;
            RenderSettings.fogEndDistance = 140f;
        }

        private static void CreateEveningLighting()
        {
            GameObject lightObject = new GameObject("Evening Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(0.76f, 0.84f, 1f);
            sunlight.intensity = 0.88f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.48f;
            lightObject.transform.rotation = Quaternion.Euler(34f, 24f, 0f);

            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.22f, 0.34f, 0.52f);
            RenderSettings.ambientEquatorColor = new Color(0.31f, 0.39f, 0.50f);
            RenderSettings.ambientGroundColor = new Color(0.11f, 0.16f, 0.23f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.28f, 0.37f, 0.52f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 42f;
            RenderSettings.fogEndDistance = 120f;
        }

        private static Material CreateMaterial(string name, Color color, float smoothness)
        {
            string path = $"{MaterialPath}/{name}.mat";
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (material == null)
            {
                Shader shader = Shader.Find("Standard");
                if (shader == null)
                {
                    throw new InvalidOperationException("Built-in Standard shader was not found.");
                }

                material = new Material(shader) { name = name };
                AssetDatabase.CreateAsset(material, path);
            }

            material.color = color;
            if (material.HasProperty("_Glossiness"))
            {
                material.SetFloat("_Glossiness", smoothness);
            }
            EditorUtility.SetDirty(material);
            return material;
        }

        private static void EnsureFolder(string path)
        {
            string[] parts = path.Split('/');
            string current = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                string next = $"{current}/{parts[i]}";
                if (!AssetDatabase.IsValidFolder(next))
                {
                    AssetDatabase.CreateFolder(current, parts[i]);
                }
                current = next;
            }
        }

        private static void SetLayerRecursively(GameObject root, int layer)
        {
            root.layer = layer;
            foreach (Transform child in root.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }
}

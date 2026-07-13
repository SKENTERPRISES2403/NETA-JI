using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildChapterThreeScene(
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
            Material sandhyaDress,
            Material volunteerDress,
            Material policeKhaki,
            Material foliage,
            Material trunk)
        {
            Scene chapterScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject environment = new GameObject("Daraganj Night Search Lanes");
            CreateBox("Neighborhood Ground", new Vector3(0f, -0.3f, 0f), new Vector3(48f, 0.6f, 38f), stone, environment.transform);
            CreateBox("Main Search Lane", new Vector3(0f, 0.02f, 0f), new Vector3(8f, 0.08f, 36f), darkStone, environment.transform);
            CreateBox("Cross Lane", new Vector3(0f, 0.025f, 4f), new Vector3(40f, 0.08f, 6f), darkStone, environment.transform);
            CreateBox("Left Footpath", new Vector3(-6.1f, 0f, 0f), new Vector3(4.1f, 0.15f, 36f), sand, environment.transform);
            CreateBox("Right Footpath", new Vector3(6.1f, 0f, 0f), new Vector3(4.1f, 0.15f, 36f), sand, environment.transform);

            CreateBox("Azad Home Night", new Vector3(-12f, 2.2f, -13.8f), new Vector3(8f, 4.4f, 5f), sand, environment.transform);
            CreateBox("Azad Home Door Night", new Vector3(-12f, 1.15f, -11.2f), new Vector3(1.35f, 2.3f, 0.16f), teal, environment.transform);
            CreateWorldLabel("Home Nameplate Night", "AZAD  /  SHANTI  /  SANDHYA", new Vector3(-12f, 3.1f, -11.08f), Vector3.zero, darkStone, environment.transform, 0.019f);

            CreateBox("Tea Stall", new Vector3(12.5f, 1.5f, -6f), new Vector3(7f, 3f, 4.5f), teal, environment.transform);
            CreateBox("Tea Counter", new Vector3(12.5f, 0.75f, -3.55f), new Vector3(5.4f, 1.5f, 0.75f), darkStone, environment.transform);
            CreateWorldLabel("Tea Stall Sign", "GUPTA CHAI  /  CCTV", new Vector3(12.5f, 2.45f, -3.68f), Vector3.zero, yellow, environment.transform, 0.024f);
            CreateWorldLabel("Tea Stall Joke", "CHAI THANDI  /  CLUE GARAM", new Vector3(12.5f, 1.92f, -3.68f), Vector3.zero, white, environment.transform, 0.014f);

            CreateBox("Helpers Hand Search Desk", new Vector3(-13f, 0.70f, 7f), new Vector3(5.4f, 1.4f, 2.2f), teal, environment.transform);
            CreateBox("Search Desk Board", new Vector3(-13f, 2.15f, 8f), new Vector3(7f, 2.2f, 0.18f), darkStone, environment.transform);
            CreateWorldLabel("Search Desk Title", "HELPERS HAND  /  SEARCH DESK", new Vector3(-13f, 2.42f, 7.88f), Vector3.zero, yellow, environment.transform, 0.022f);
            CreateWorldLabel("Search Desk Rule", "VERIFY  /  COORDINATE  /  NO RUMOURS", new Vector3(-13f, 1.82f, 7.88f), Vector3.zero, white, environment.transform, 0.013f);

            CreateBox("Police Help Point", new Vector3(12.5f, 1.8f, 12.5f), new Vector3(8f, 3.6f, 5.5f), policeKhaki, environment.transform);
            CreateBox("Police Help Counter", new Vector3(12.5f, 0.82f, 9.55f), new Vector3(5.5f, 1.5f, 0.8f), darkStone, environment.transform);
            CreateWorldLabel("Police Point Sign", "NIGHT HELP POINT", new Vector3(12.5f, 2.65f, 9.64f), Vector3.zero, darkStone, environment.transform, 0.025f);

            for (int i = 0; i < 8; i++)
            {
                float side = i % 2 == 0 ? -1f : 1f;
                float x = side * (16f + (i % 4) * 1.2f);
                float z = -10f + (i / 2) * 7f;
                Material wall = i % 3 == 0 ? teal : i % 3 == 1 ? sand : shantiDress;
                CreateBox($"Search Lane Home {i + 1}", new Vector3(x, 2.1f, z), new Vector3(5f, 4.2f, 5f), wall, environment.transform);
            }

            CreateTree("Search Neem Left", new Vector3(-8.5f, 0f, -2f), foliage, trunk, environment.transform);
            CreateTree("Search Neem Right", new Vector3(8.5f, 0f, 5f), foliage, trunk, environment.transform);
            CreateSearchLamps(environment.transform, darkStone, yellow);

            GameObject systems = new GameObject("Chapter 3 Systems");
            systems.AddComponent<GameSession>();
            systems.AddComponent<PrototypeInput>();
            systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<PrototypeHud>();
            PrototypeAutomation automation = systems.AddComponent<PrototypeAutomation>();
            automation.Configure(3, 67, 900, 40);
            MissionController mission = systems.AddComponent<MissionController>();

            GameObject azad = CreatePerson("Azad", new Vector3(0f, 0f, -15f), shirt, trousers, skin, hair, true);
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
            cameraObject.transform.position = new Vector3(0f, 3.6f, -20f);
            controller.SetCamera(cameraObject.transform);

            ConfigureChapterThreeMission(
                mission,
                shantiDress,
                sandhyaDress,
                volunteerDress,
                policeKhaki,
                teal,
                yellow,
                white,
                darkStone,
                skin,
                hair);

            CreateNightSearchLighting();
            EditorSceneManager.MarkSceneDirty(chapterScene);
            EditorSceneManager.SaveScene(chapterScene, ChapterThreeScenePath);
        }

        private static void CreateSearchLamps(Transform parent, Material pole, Material lamp)
        {
            Vector3[] positions =
            {
                new Vector3(-6.8f, 0f, -9f),
                new Vector3(6.8f, 0f, 1f),
                new Vector3(-6.8f, 0f, 12f)
            };
            for (int i = 0; i < positions.Length; i++)
            {
                CreateStreetLamp($"Search Lamp {i + 1}", positions[i], pole, lamp, parent);
                GameObject glow = new GameObject($"Search Lamp Glow {i + 1}");
                glow.transform.SetParent(parent);
                glow.transform.position = positions[i] + Vector3.up * 3.85f;
                Light point = glow.AddComponent<Light>();
                point.type = LightType.Point;
                point.color = new Color(1f, 0.72f, 0.30f);
                point.intensity = 1.3f;
                point.range = 11f;
                point.shadows = LightShadows.None;
            }
        }

        private static void ConfigureChapterThreeMission(
            MissionController mission,
            Material shantiDress,
            Material sandhyaDress,
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

            GameObject shanti = CreatePerson("Shanti", new Vector3(-12f, 0f, -10.2f), shantiDress, darkStone, skin, hair, false);
            AddScarf(shanti.transform, shantiDress);
            objectives.Add(AddObjective(shanti, "missing-alert", "Shanti se baat karein", "Shanti",
                "Azad, Sandhya library se abhi tak nahi lauti. Uska phone watch bhi unreachable hai. Ghabrana nahi hai, par ek minute bhi waste nahi karna.",
                0, 0, 0, false));
            labels.Add("Ghar par Shanti se poori baat samjhein");

            GameObject ribbon = new GameObject("Sandhya Blue Ribbon");
            ribbon.transform.position = new Vector3(-5.5f, 0.18f, -5f);
            CreatePrimitiveChild("Ribbon Left", PrimitiveType.Cube, ribbon.transform, new Vector3(-0.13f, 0f, 0f), new Vector3(0.12f, 0.05f, 0.62f), sandhyaDress).transform.localRotation = Quaternion.Euler(0f, 24f, 0f);
            CreatePrimitiveChild("Ribbon Right", PrimitiveType.Cube, ribbon.transform, new Vector3(0.13f, 0f, 0f), new Vector3(0.12f, 0.05f, 0.62f), sandhyaDress).transform.localRotation = Quaternion.Euler(0f, -24f, 0f);
            objectives.Add(AddObjective(ribbon, "blue-ribbon", "Neela ribbon dekhein", "Azad",
                "Yeh Sandhya ke school ribbon jaisa hai. Pehle witness se confirm karte hain, phir Samrat ko exact location denge.",
                1, 0, 1, false));
            labels.Add("Gali mein mile blue ribbon ko inspect karein");

            GameObject meera = CreatePerson("Neighbor Meera", new Vector3(-1.5f, 0f, -1f), volunteerDress, darkStone, skin, hair, false);
            objectives.Add(AddObjective(meera, "last-seen", "Meera ji se last-seen poochhein", "Meera",
                "Maine use stationery shop ke paas dekha tha. Ek safed van do baar lane mein ghoomi thi. Number poora nahi, bas aakhri mein 27 yaad hai.",
                1, 0, 1, false));
            labels.Add("Neighbor Meera se last-seen detail lein");

            GameObject teaOwner = CreatePerson("Tea Stall Owner", new Vector3(10.5f, 0f, -2.7f), yellow, darkStone, skin, hair, false);
            objectives.Add(AddObjective(teaOwner, "cctv-access", "Chai stall camera poochhein", "Gupta Ji",
                "Camera chalu tha, lekin backup unit ka switch trip ho gaya. Aaj chai se zyada recording kaam aayegi; switch counter ke peeche hai.",
                0, 0, 1, false));
            labels.Add("Tea stall owner se CCTV access lein");

            GameObject cameraSwitch = new GameObject("CCTV Backup Switch");
            cameraSwitch.transform.position = new Vector3(14.5f, 1f, -3.15f);
            CreatePrimitiveChild("Switch Box", PrimitiveType.Cube, cameraSwitch.transform, Vector3.zero, new Vector3(0.65f, 0.85f, 0.30f), darkStone);
            CreatePrimitiveChild("Switch Lever", PrimitiveType.Cube, cameraSwitch.transform, new Vector3(0f, 0.05f, 0.20f), new Vector3(0.16f, 0.42f, 0.10f), yellow).transform.localRotation = Quaternion.Euler(0f, 0f, -18f);
            objectives.Add(AddObjective(cameraSwitch, "cctv-power", "CCTV backup chalu karein", "Azad",
                "Backup aa gaya. Recording ko phone par copy karne ke liye data pack aur card reader lena hoga.",
                0, -50, 1, false));
            labels.Add("CCTV backup power restore karein");

            GameObject cctvScreen = new GameObject("CCTV Evidence Screen");
            cctvScreen.transform.position = new Vector3(12.5f, 1.35f, -3.15f);
            CreatePrimitiveChild("Monitor", PrimitiveType.Cube, cctvScreen.transform, Vector3.zero, new Vector3(1.7f, 1.05f, 0.22f), darkStone);
            CreatePrimitiveChild("Footage", PrimitiveType.Cube, cctvScreen.transform, new Vector3(0f, 0f, -0.13f), new Vector3(1.42f, 0.78f, 0.05f), teal);
            objectives.Add(AddObjective(cctvScreen, "cctv-clue", "CCTV footage dekhein", "Azad",
                "Safed van, 8:17 PM. Plate ka hissa clear hai: ...27. Sandhya frame mein hai, aur van old godown wali road par mudti hai.",
                1, 0, 0, false));
            labels.Add("CCTV recording se vehicle clue nikalein");

            GameObject coordinator = CreatePerson("Search Coordinator", new Vector3(-10.5f, 0f, 5f), volunteerDress, darkStone, skin, hair, false);
            CreatePrimitiveChild("Helpers Hand Badge", PrimitiveType.Cube, coordinator.transform, new Vector3(0f, 1.25f, 0.48f), new Vector3(0.20f, 0.20f, 0.04f), teal);
            objectives.Add(AddObjective(coordinator, "search-network", "Volunteer search activate karein", "Helpers Hand Coordinator",
                "Do verified teams nikal rahe hain. Photo sirf team phones par rahegi, public rumour nahi. Shanti ke paas ek volunteer rukega.",
                1, 0, 1, false));
            labels.Add("Helpers Hand ka verified search network activate karein");

            GameObject samrat = CreatePerson("Constable Samrat", new Vector3(10.5f, 0f, 8.8f), policeKhaki, darkStone, skin, hair, false);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            objectives.Add(AddObjective(samrat, "samrat-response", "Samrat ko evidence dein", "Constable Samrat",
                "Main control room aur patrol ko partial plate bhej raha hoon. Tum facts likhwao; pressure ya phone aaye toh seedha mere saamne record hoga.",
                1, 0, 1, false));
            labels.Add("Constable Samrat ko ribbon aur CCTV evidence dein");

            GameObject incidentBoard = new GameObject("Incident Evidence Board");
            incidentBoard.transform.position = new Vector3(14f, 1.35f, 9.2f);
            CreatePrimitiveChild("Board", PrimitiveType.Cube, incidentBoard.transform, Vector3.zero, new Vector3(2.9f, 1.9f, 0.18f), white);
            CreatePrimitiveChild("Photo Marker", PrimitiveType.Cube, incidentBoard.transform, new Vector3(-0.72f, 0.35f, -0.14f), new Vector3(0.62f, 0.65f, 0.04f), sandhyaDress);
            CreatePrimitiveChild("Vehicle Note", PrimitiveType.Cube, incidentBoard.transform, new Vector3(0.65f, 0.25f, -0.14f), new Vector3(0.82f, 0.42f, 0.04f), yellow);
            CreatePrimitiveChild("Route Note", PrimitiveType.Cube, incidentBoard.transform, new Vector3(0f, -0.48f, -0.14f), new Vector3(1.65f, 0.34f, 0.04f), teal);
            objectives.Add(AddObjective(incidentBoard, "incident-record", "Incident details verify karein", "Azad",
                "Time, route, ribbon, partial plate aur witness statement sab match kar rahe hain. Andaza nahi, sirf verified facts aage jayenge.",
                2, 0, 2, false));
            labels.Add("Police help point par incident record verify karein");

            GameObject routeMap = new GameObject("Old Godown Route Map");
            routeMap.transform.position = new Vector3(-13f, 1.2f, 6.85f);
            CreatePrimitiveChild("Map Board", PrimitiveType.Cube, routeMap.transform, Vector3.zero, new Vector3(2.4f, 1.35f, 0.12f), white);
            CreatePrimitiveChild("Route A", PrimitiveType.Cube, routeMap.transform, new Vector3(-0.45f, 0.15f, -0.09f), new Vector3(0.95f, 0.10f, 0.04f), teal).transform.localRotation = Quaternion.Euler(0f, 0f, 24f);
            CreatePrimitiveChild("Route B", PrimitiveType.Cube, routeMap.transform, new Vector3(0.42f, -0.14f, -0.09f), new Vector3(1.05f, 0.10f, 0.04f), teal).transform.localRotation = Quaternion.Euler(0f, 0f, -18f);
            CreatePrimitiveChild("Godown Marker", PrimitiveType.Sphere, routeMap.transform, new Vector3(0.92f, -0.42f, -0.12f), new Vector3(0.20f, 0.20f, 0.08f), yellow);
            objectives.Add(AddObjective(routeMap, "route-plan", "Search route confirm karein", "Search Coordinator",
                "Team A main road se, Team B river-side lane se. Koi akela godown ke andar nahi jayega; location milte hi Samrat ko update hoga.",
                1, 0, 1, false));
            labels.Add("Volunteer teams ke liye safe search route confirm karein");

            GameObject phone = new GameObject("Unknown Caller Phone");
            phone.transform.position = new Vector3(-12.7f, 1.15f, -10.4f);
            CreatePrimitiveChild("Phone Body", PrimitiveType.Cube, phone.transform, Vector3.zero, new Vector3(0.34f, 0.62f, 0.12f), darkStone);
            CreatePrimitiveChild("Incoming Call", PrimitiveType.Cube, phone.transform, new Vector3(0f, 0.02f, -0.08f), new Vector3(0.24f, 0.38f, 0.04f), shantiDress);
            objectives.Add(AddObjective(phone, "extortion-call", "Incoming call record karein", "Unknown Caller",
                "Rs 50 lakh tayyar rakho. Agla message ka wait karo. Phone band mat karna.",
                1, -100, 1, false));
            labels.Add("Ghar par aaye unknown call ko Samrat ke saath record karein");

            mission.Configure(
                "Sandhya Kahan Hai?",
                objectives,
                labels,
                "CHAPTER 3 COMPLETE",
                "Search network active hai. Samrat ke paas van route aur recorded call dono hain.");
            mission.ConfigureMilestones(
                new List<int> { 3, 6, 9 },
                new List<string> { "LAST-SEEN CONFIRMED", "CAMERA CLUE", "RESPONSE ACTIVE" },
                new List<string>
                {
                    "Meera ne white van aur partial plate yaad ki. Tea stall camera check karna hai.",
                    "Footage ne old godown route confirm kiya. Helpers Hand aur Samrat ko evidence dena hoga.",
                    "Verified search route ready hai. Ghar ka phone baj raha hai."
                });
            mission.ConfigureChapter(3, "Chapter04");
            mission.ConfigureIntro(
                "CHAPTER 3 / GHAR KA SANNATA",
                "Sandhya ghar nahi lauti. Har clue verify karo, har minute sambhal kar chalo.");
        }

        private static void CreateNightSearchLighting()
        {
            GameObject lightObject = new GameObject("Cool Night Key Light");
            Light moonlight = lightObject.AddComponent<Light>();
            moonlight.type = LightType.Directional;
            moonlight.color = new Color(0.55f, 0.68f, 1f);
            moonlight.intensity = 0.68f;
            moonlight.shadows = LightShadows.Hard;
            moonlight.shadowStrength = 0.45f;
            lightObject.transform.rotation = Quaternion.Euler(42f, -26f, 0f);

            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.10f, 0.16f, 0.29f);
            RenderSettings.ambientEquatorColor = new Color(0.16f, 0.22f, 0.31f);
            RenderSettings.ambientGroundColor = new Color(0.05f, 0.08f, 0.12f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.12f, 0.18f, 0.28f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 38f;
            RenderSettings.fogEndDistance = 105f;
        }
    }
}

using System.Collections.Generic;
using NetaJi.Prototype;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void BuildPrologueScene(
            Material sand, Material stone, Material darkStone, Material water,
            Material teal, Material yellow, Material white, Material shirt, Material trousers,
            Material skin, Material hair, Material shantiDress, Material sandhyaDress,
            Material policeKhaki, Material volunteerDress, Material foliage, Material trunk)
        {
            Scene prologueScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            Material warmWall = CreateMaterial("PrologueWarmWall", new Color(0.74f, 0.49f, 0.31f), 0.08f);
            Material hospitalBlue = CreateMaterial("PrologueHospitalBlue", new Color(0.52f, 0.72f, 0.76f), 0.20f);
            Material hospitalFloor = CreateMaterial("PrologueHospitalFloor", new Color(0.55f, 0.62f, 0.62f), 0.10f);
            Material monitorGreen = CreateMaterial("PrologueMonitorGreen", new Color(0.17f, 0.86f, 0.48f), 0.34f);
            Material yamunaWater = CreateMaterial("PrologueYamunaWater", new Color(0.05f, 0.28f, 0.48f), 0.62f);
            Material vanBlack = CreateMaterial("PrologueVanBlack", new Color(0.035f, 0.045f, 0.05f), 0.34f);
            Material rescueRed = CreateMaterial("PrologueRescueRed", new Color(0.72f, 0.08f, 0.06f), 0.30f);

            List<GameObject> roots = new List<GameObject>();
            List<Transform> anchors = new List<Transform>();
            List<Transform> focuses = new List<Transform>();

            GameObject family = CreateCinematicRoot("01 Family And Seva");
            roots.Add(family);
            CreateBox("Family Courtyard", new Vector3(0f, -0.25f, 0f), new Vector3(24f, 0.5f, 18f), sand, family.transform);
            CreateBox("Family Home", new Vector3(5f, 2.6f, 5.5f), new Vector3(10f, 5.2f, 5f), warmWall, family.transform);
            CreateBox("Family Roof", new Vector3(5f, 5.34f, 5.5f), new Vector3(10.7f, 0.30f, 5.7f), darkStone, family.transform);
            CreateBox("Family Front Door", new Vector3(5f, 1.35f, 2.94f), new Vector3(1.7f, 2.7f, 0.18f), teal, family.transform);
            CreateBox("Family Window Left", new Vector3(2.4f, 2.65f, 2.93f), new Vector3(1.6f, 1.35f, 0.16f), hospitalBlue, family.transform);
            CreateBox("Family Window Right", new Vector3(7.6f, 2.65f, 2.93f), new Vector3(1.6f, 1.35f, 0.16f), hospitalBlue, family.transform);
            CreateBox("Family Porch Step", new Vector3(5f, 0.18f, 2.35f), new Vector3(4.3f, 0.32f, 1.1f), stone, family.transform);
            CreateBox("Father Memory Frame", new Vector3(8.45f, 3.75f, 2.88f), new Vector3(1.15f, 1.35f, 0.15f), darkStone, family.transform);
            CreateBox("Father Uniform Portrait", new Vector3(8.45f, 3.75f, 2.78f), new Vector3(0.82f, 1.02f, 0.06f), policeKhaki, family.transform);
            CreatePrimitiveChild("Service Medal", PrimitiveType.Sphere, family.transform, new Vector3(8.45f, 3.48f, 2.70f), new Vector3(0.18f, 0.18f, 0.06f), yellow);
            CreateStudyDesk("Sandhya Study Table", new Vector3(0f, 0f, 2.4f), darkStone, yellow).transform.SetParent(family.transform, true);
            CreatePrimitiveChild("English Book", PrimitiveType.Cube, family.transform, new Vector3(-0.25f, 0.91f, 2.38f), new Vector3(0.55f, 0.06f, 0.42f), teal);
            CreatePrimitiveChild("Sandhya Crayon Box", PrimitiveType.Cube, family.transform, new Vector3(0.42f, 0.91f, 2.38f), new Vector3(0.36f, 0.09f, 0.24f), rescueRed);
            CreatePrimitiveChild("Sandhya Toy Ball", PrimitiveType.Sphere, family.transform, new Vector3(-1.1f, 0.23f, 1.9f), new Vector3(0.38f, 0.38f, 0.38f), yellow);
            GameObject familyAzad = ParentPerson(CreatePerson("Azad", new Vector3(-1.65f, 0f, 0.55f), shirt, trousers, skin, hair, true), family.transform);
            GameObject familyShanti = ParentPerson(CreatePerson("Shanti", new Vector3(1.65f, 0f, 0.70f), shantiDress, darkStone, skin, hair, false), family.transform);
            AddScarf(familyShanti.transform, shantiDress);
            GameObject familySandhya = ParentPerson(CreatePerson("Sandhya", new Vector3(0f, 0f, 0.55f), sandhyaDress, darkStone, skin, hair, false), family.transform);
            AddPigtails(familySandhya.transform, hair);
            familySandhya.transform.localScale = Vector3.one * 0.72f;
            familyAzad.transform.rotation = Quaternion.Euler(0f, 22f, 0f);
            familyShanti.transform.rotation = Quaternion.Euler(0f, -38f, 0f);
            CreateCinematicCameraPoints(family.transform, new Vector3(0f, 2.25f, -5.7f), new Vector3(0f, 1.05f, 0.85f), anchors, focuses);

            GameObject service = CreateCinematicRoot("02 Helpers Hand Ghat Service");
            roots.Add(service);
            CreateBox("Clean Ghat Plaza", new Vector3(0f, -0.25f, 0f), new Vector3(30f, 0.5f, 18f), stone, service.transform);
            for (int index = 0; index < 5; index++)
            {
                CreateBox($"Clean Ghat Step {index + 1}", new Vector3(0f, -0.45f - index * 0.28f, 8f + index * 2.2f), new Vector3(28f, 0.45f, 2.4f), stone, service.transform);
            }
            CreateBox("Clean Ganga", new Vector3(0f, -2.0f, 20f), new Vector3(48f, 0.3f, 15f), water, service.transform);
            GameObject serviceAzad = ParentPerson(CreatePerson("Azad Serving", new Vector3(-3.5f, 0f, -1f), shirt, trousers, skin, hair, true), service.transform);
            CinematicActorMotion azadWalk = serviceAzad.AddComponent<CinematicActorMotion>();
            azadWalk.Configure(new Vector3(1.5f, 0f, 3f), 0.2f, 3.5f);
            GameObject serviceShanti = ParentPerson(CreatePerson("Shanti Serving", new Vector3(2f, 0f, 0f), shantiDress, darkStone, skin, hair, false), service.transform);
            AddScarf(serviceShanti.transform, shantiDress);
            GameObject elder = ParentPerson(CreatePerson("Elder Receiving Help", new Vector3(2.8f, 0f, 2.8f), white, darkStone, skin, hair, false), service.transform);
            elder.transform.localScale = Vector3.one * 0.92f;
            CreatePrimitiveChild("Elder Walking Cane", PrimitiveType.Cylinder, service.transform, new Vector3(3.25f, 0.68f, 2.75f), new Vector3(0.045f, 0.68f, 0.045f), darkStone);
            for (int index = 0; index < 5; index++)
            {
                CreatePrimitiveChild($"Seva Supply Box {index + 1}", PrimitiveType.Cube, service.transform,
                    new Vector3(-5.2f + index * 1.15f, 0.34f, 1.8f + (index % 2) * 0.7f),
                    new Vector3(0.82f, 0.68f, 0.72f), index % 2 == 0 ? teal : yellow);
            }
            for (int index = 0; index < 4; index++)
            {
                Transform broom = CreatePrimitiveChild($"Ghat Broom {index + 1}", PrimitiveType.Cylinder, service.transform,
                    new Vector3(-6.8f + index * 3.1f, 0.75f, 5.4f), new Vector3(0.045f, 0.75f, 0.045f), darkStone).transform;
                broom.localRotation = Quaternion.Euler(0f, 0f, index % 2 == 0 ? -14f : 14f);
            }
            GameObject sevaBoat = new GameObject("Ghat Seva Boat");
            sevaBoat.transform.SetParent(service.transform);
            sevaBoat.transform.localPosition = new Vector3(6f, -1.45f, 19f);
            CreatePrimitiveChild("Boat Hull", PrimitiveType.Cube, sevaBoat.transform, Vector3.zero, new Vector3(4.2f, 0.48f, 1.4f), darkStone);
            CreatePrimitiveChild("Boat Trim", PrimitiveType.Cube, sevaBoat.transform, new Vector3(0f, 0.32f, 0f), new Vector3(3.6f, 0.16f, 1.1f), yellow);
            for (int index = 0; index < 6; index++)
            {
                GameObject volunteer = ParentPerson(CreatePerson(
                    $"Ghat Volunteer {index + 1}", new Vector3(-8f + index * 3f, 0f, 4f + index % 2),
                    index % 2 == 0 ? volunteerDress : yellow, darkStone, skin, hair, false), service.transform);
                volunteer.transform.localScale = Vector3.one * 0.92f;
            }
            CreateCinematicCameraPoints(service.transform, new Vector3(-8.2f, 3.15f, -5.6f), new Vector3(0f, 1.0f, 3.0f), anchors, focuses);

            GameObject abduction = CreateCinematicRoot("03 Sandhya Abduction");
            roots.Add(abduction);
            CreateBox("School Lane", new Vector3(0f, -0.25f, 0f), new Vector3(30f, 0.5f, 18f), stone, abduction.transform);
            CreateBox("School Gate Left", new Vector3(-5f, 2f, 5f), new Vector3(1f, 4f, 0.8f), teal, abduction.transform);
            CreateBox("School Gate Right", new Vector3(5f, 2f, 5f), new Vector3(1f, 4f, 0.8f), teal, abduction.transform);
            CreateBox("School Wall", new Vector3(0f, 3f, 8f), new Vector3(20f, 6f, 1f), warmWall, abduction.transform);
            GameObject laneSandhya = ParentPerson(CreatePerson("Sandhya Waiting", new Vector3(2.4f, 0f, 1.4f), sandhyaDress, darkStone, skin, hair, false), abduction.transform);
            AddPigtails(laneSandhya.transform, hair);
            laneSandhya.transform.localScale = Vector3.one * 0.72f;
            CinematicActorMotion sandhyaMotion = laneSandhya.AddComponent<CinematicActorMotion>();
            sandhyaMotion.Configure(new Vector3(-1.1f, 0f, 0.8f), 0.55f, 2.0f);
            CreatePrimitiveChild("Dropped School Bag", PrimitiveType.Cube, abduction.transform,
                new Vector3(2.3f, 0.18f, 0.8f), new Vector3(0.55f, 0.30f, 0.42f), yellow);
            CreatePrimitiveChild("Dropped Hair Ribbon", PrimitiveType.Cube, abduction.transform,
                new Vector3(1.75f, 0.06f, 0.72f), new Vector3(0.32f, 0.035f, 0.12f), rescueRed);
            GameObject watcherOne = ParentPerson(CreatePerson("Fictional Gang Lookout One", new Vector3(-2.8f, 0f, 0.4f), vanBlack, vanBlack, skin, hair, false), abduction.transform);
            GameObject watcherTwo = ParentPerson(CreatePerson("Fictional Gang Lookout Two", new Vector3(-4.3f, 0f, -0.2f), darkStone, vanBlack, skin, hair, false), abduction.transform);
            watcherOne.transform.rotation = Quaternion.Euler(0f, 78f, 0f);
            watcherTwo.transform.rotation = Quaternion.Euler(0f, 62f, 0f);
            GameObject concernedTeacher = ParentPerson(CreatePerson("Concerned School Teacher", new Vector3(4.8f, 0f, 4.1f), white, darkStone, skin, hair, false), abduction.transform);
            concernedTeacher.transform.rotation = Quaternion.Euler(0f, -155f, 0f);
            GameObject van = new GameObject("Unmarked Fictional Van");
            van.transform.SetParent(abduction.transform);
            van.transform.localPosition = new Vector3(-7.5f, 0f, -0.5f);
            CreatePrimitiveChild("Van Body", PrimitiveType.Cube, van.transform, new Vector3(0f, 1.0f, 0f), new Vector3(4.8f, 1.8f, 2.1f), vanBlack);
            CreatePrimitiveChild("Van Cabin", PrimitiveType.Cube, van.transform, new Vector3(1.5f, 1.55f, 0f), new Vector3(1.6f, 1.1f, 2f), vanBlack);
            for (int index = 0; index < 4; index++)
            {
                CreatePrimitiveChild($"Van Wheel {index + 1}", PrimitiveType.Cylinder, van.transform,
                    new Vector3(index < 2 ? -1.5f : 1.5f, 0.38f, index % 2 == 0 ? -1.05f : 1.05f),
                    new Vector3(0.42f, 0.18f, 0.42f), darkStone).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            }
            CinematicActorMotion vanMotion = van.AddComponent<CinematicActorMotion>();
            vanMotion.Configure(new Vector3(8.5f, 0f, -0.5f), 0.25f, 2.6f);
            CreateCinematicCameraPoints(abduction.transform, new Vector3(6.2f, 2.45f, -5.2f), new Vector3(0.2f, 0.9f, 0.8f), anchors, focuses);

            GameObject rescue = CreateCinematicRoot("04 Rescue And Reunion");
            roots.Add(rescue);
            CreateBox("Rescue Yard", new Vector3(0f, -0.25f, 0f), new Vector3(28f, 0.5f, 20f), darkStone, rescue.transform);
            CreateBox("Fictional Storage Building", new Vector3(0f, 3f, 7f), new Vector3(18f, 6f, 4f), warmWall, rescue.transform);
            GameObject rescueAzad = ParentPerson(CreatePerson("Azad Reunion", new Vector3(-0.9f, 0f, 0f), shirt, trousers, skin, hair, true), rescue.transform);
            GameObject rescueSandhya = ParentPerson(CreatePerson("Sandhya Safe", new Vector3(0.4f, 0f, 0.2f), sandhyaDress, darkStone, skin, hair, false), rescue.transform);
            AddPigtails(rescueSandhya.transform, hair);
            rescueSandhya.transform.localScale = Vector3.one * 0.72f;
            rescueAzad.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            rescueSandhya.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            GameObject rescueShanti = ParentPerson(CreatePerson("Shanti At Reunion", new Vector3(1.8f, 0f, 0.25f), shantiDress, darkStone, skin, hair, false), rescue.transform);
            AddScarf(rescueShanti.transform, shantiDress);
            rescueShanti.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            GameObject rescueSamrat = ParentPerson(CreatePerson("Constable Samrat", new Vector3(-4f, 0f, 1f), policeKhaki, darkStone, skin, hair, false), rescue.transform);
            AddPoliceDetails(rescueSamrat.transform, policeKhaki, darkStone);
            GameObject policeJeep = new GameObject("Samrat Police Jeep");
            policeJeep.transform.SetParent(rescue.transform);
            policeJeep.transform.localPosition = new Vector3(-6.6f, 0f, -0.8f);
            CreatePrimitiveChild("Jeep Body", PrimitiveType.Cube, policeJeep.transform, new Vector3(0f, 0.82f, 0f), new Vector3(4.4f, 1.15f, 2.0f), white);
            CreatePrimitiveChild("Jeep Cabin", PrimitiveType.Cube, policeJeep.transform, new Vector3(0.7f, 1.48f, 0f), new Vector3(2.2f, 0.75f, 1.8f), policeKhaki);
            for (int index = 0; index < 4; index++)
            {
                CreatePrimitiveChild($"Jeep Wheel {index + 1}", PrimitiveType.Cylinder, policeJeep.transform,
                    new Vector3(index < 2 ? -1.45f : 1.45f, 0.36f, index % 2 == 0 ? -1.02f : 1.02f),
                    new Vector3(0.38f, 0.16f, 0.38f), darkStone).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            }
            CreatePrimitiveChild("Police Beacon Red", PrimitiveType.Sphere, rescue.transform, new Vector3(-7f, 2.2f, -1f), new Vector3(0.35f, 0.20f, 0.35f), rescueRed)
                .AddComponent<WorldMotion>().Configure(WorldMotionKind.Turn, 90f, 2f, Vector3.up);
            CreatePrimitiveChild("Police Beacon Blue", PrimitiveType.Sphere, rescue.transform, new Vector3(-6.2f, 2.2f, -1f), new Vector3(0.35f, 0.20f, 0.35f), hospitalBlue)
                .AddComponent<WorldMotion>().Configure(WorldMotionKind.Turn, 90f, 2.3f, Vector3.up);
            CreateCinematicCameraPoints(rescue.transform, new Vector3(0f, 2.30f, -5.0f), new Vector3(-0.1f, 0.95f, 0.35f), anchors, focuses);

            GameObject hospital = CreateCinematicRoot("05 Hospital Loss");
            roots.Add(hospital);
            CreateBox("Hospital Floor", new Vector3(0f, -0.25f, 0f), new Vector3(24f, 0.5f, 18f), hospitalFloor, hospital.transform);
            CreateBox("Hospital Back Wall", new Vector3(0f, 3.5f, 7f), new Vector3(24f, 7f, 0.6f), hospitalBlue, hospital.transform);
            CreateBox("Hospital Left Wall", new Vector3(-11.7f, 3.5f, 0f), new Vector3(0.6f, 7f, 14f), hospitalBlue, hospital.transform);
            CreateBox("Hospital Ceiling Light", new Vector3(0f, 5.8f, 1.5f), new Vector3(7f, 0.12f, 1.2f), white, hospital.transform);
            CreateBox("Hospital Bed", new Vector3(1.8f, 0.72f, 2f), new Vector3(4.5f, 0.55f, 2.2f), white, hospital.transform);
            CreateBox("Hospital Pillow", new Vector3(0.1f, 1.10f, 2f), new Vector3(0.85f, 0.20f, 1.25f), white, hospital.transform);
            CreateBox("Hospital Blanket", new Vector3(2.15f, 1.08f, 2f), new Vector3(2.2f, 0.12f, 1.85f), hospitalBlue, hospital.transform);
            GameObject hospitalShanti = ParentPerson(CreatePerson("Shanti Recovering", new Vector3(0.2f, 1.05f, 2f), shantiDress, darkStone, skin, hair, false), hospital.transform);
            hospitalShanti.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            GameObject hospitalAzad = ParentPerson(CreatePerson("Azad At Bedside", new Vector3(-2.5f, 0f, 0.8f), shirt, trousers, skin, hair, true), hospital.transform);
            hospitalAzad.transform.rotation = Quaternion.Euler(0f, 55f, 0f);
            GameObject doctor = ParentPerson(CreatePerson("Honest Doctor", new Vector3(4.0f, 0f, 0.8f), white, darkStone, skin, hair, false), hospital.transform);
            doctor.transform.rotation = Quaternion.Euler(0f, -42f, 0f);
            GameObject monitor = new GameObject("Maternal Care Monitor");
            monitor.transform.SetParent(hospital.transform);
            monitor.transform.localPosition = new Vector3(4.5f, 2.25f, 3.8f);
            CreatePrimitiveChild("Monitor Screen", PrimitiveType.Cube, monitor.transform, Vector3.zero, new Vector3(1.65f, 1.15f, 0.14f), darkStone);
            for (int index = 0; index < 5; index++)
            {
                CreatePrimitiveChild($"Monitor Pulse {index + 1}", PrimitiveType.Cube, monitor.transform,
                    new Vector3(-0.58f + index * 0.29f, (index == 2 ? 0.20f : 0f), -0.09f),
                    new Vector3(0.22f, index == 2 ? 0.38f : 0.06f, 0.025f), monitorGreen);
            }
            CreatePrimitiveChild("Monitor Stand", PrimitiveType.Cylinder, hospital.transform, new Vector3(4.5f, 1.05f, 3.8f), new Vector3(0.07f, 1.05f, 0.07f), darkStone);
            CreatePrimitiveChild("IV Stand", PrimitiveType.Cylinder, hospital.transform, new Vector3(-0.3f, 1.65f, 3.5f), new Vector3(0.055f, 1.65f, 0.055f), darkStone);
            CreatePrimitiveChild("IV Bag", PrimitiveType.Cube, hospital.transform, new Vector3(-0.3f, 3.0f, 3.5f), new Vector3(0.42f, 0.62f, 0.12f), white);
            GameObject cradle = new GameObject("Empty Cradle");
            cradle.transform.SetParent(hospital.transform);
            cradle.transform.localPosition = new Vector3(4.9f, 0f, -0.35f);
            CreatePrimitiveChild("Cradle Base", PrimitiveType.Cube, cradle.transform, new Vector3(0f, 0.65f, 0f), new Vector3(1.6f, 0.12f, 0.9f), white);
            CreatePrimitiveChild("Cradle Rail Left", PrimitiveType.Cube, cradle.transform, new Vector3(-0.75f, 1.05f, 0f), new Vector3(0.08f, 0.8f, 0.9f), darkStone);
            CreatePrimitiveChild("Cradle Rail Right", PrimitiveType.Cube, cradle.transform, new Vector3(0.75f, 1.05f, 0f), new Vector3(0.08f, 0.8f, 0.9f), darkStone);
            GameObject cradleLightObject = new GameObject("Cradle Soft Light");
            cradleLightObject.transform.SetParent(hospital.transform);
            cradleLightObject.transform.localPosition = new Vector3(4.9f, 3.8f, -0.35f);
            Light cradleLight = cradleLightObject.AddComponent<Light>();
            cradleLight.type = LightType.Point;
            cradleLight.color = new Color(0.62f, 0.82f, 0.92f);
            cradleLight.intensity = 1.1f;
            cradleLight.range = 7f;
            CreateCinematicCameraPoints(hospital.transform, new Vector3(-5.6f, 2.75f, -4.25f), new Vector3(1.65f, 1.1f, 2.0f), anchors, focuses);

            GameObject recovery = CreateCinematicRoot("06 Family And Community Support");
            roots.Add(recovery);
            CreateBox("Recovery Park", new Vector3(0f, -0.25f, 0f), new Vector3(30f, 0.5f, 22f), sand, recovery.transform);
            CreateBox("Recovery Walking Path", new Vector3(0f, -0.04f, 1f), new Vector3(22f, 0.10f, 4.2f), stone, recovery.transform);
            CreateTree("Recovery Neem", new Vector3(-6f, 0f, 3f), foliage, trunk, recovery.transform);
            CreateBench("Recovery Bench", new Vector3(0f, 0f, 1.2f), 0f, darkStone, teal, recovery.transform);
            GameObject recoveryAzad = ParentPerson(CreatePerson("Azad Recovering", new Vector3(0f, 0f, 0f), shirt, trousers, skin, hair, true), recovery.transform);
            GameObject recoveryShanti = ParentPerson(CreatePerson("Shanti Supporting", new Vector3(1.4f, 0f, 0.5f), shantiDress, darkStone, skin, hair, false), recovery.transform);
            AddScarf(recoveryShanti.transform, shantiDress);
            GameObject recoverySandhya = ParentPerson(CreatePerson("Sandhya Supporting", new Vector3(-1.3f, 0f, 0.5f), sandhyaDress, darkStone, skin, hair, false), recovery.transform);
            AddPigtails(recoverySandhya.transform, hair);
            recoverySandhya.transform.localScale = Vector3.one * 0.72f;
            recoveryAzad.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            for (int index = 0; index < 8; index++)
            {
                ParentPerson(CreatePerson($"Community Supporter {index + 1}",
                    new Vector3(-7f + index * 2f, 0f, 4.6f + index % 2),
                    index % 2 == 0 ? volunteerDress : teal, darkStone, skin, hair, false), recovery.transform);
                CreatePrimitiveChild($"Recovery Flower {index + 1}", PrimitiveType.Sphere, recovery.transform,
                    new Vector3(-8f + index * 2.2f, 0.18f, -2.0f + (index % 2) * 0.6f),
                    new Vector3(0.24f, 0.24f, 0.24f), index % 2 == 0 ? yellow : rescueRed);
            }
            CreateCinematicCameraPoints(recovery.transform, new Vector3(-6.8f, 2.65f, -4.9f), new Vector3(0f, 1.0f, 1.2f), anchors, focuses);

            GameObject resolve = CreateCinematicRoot("07 Sangam Resolve");
            roots.Add(resolve);
            CreateBox("Resolve Ghat Plaza", new Vector3(0f, -0.25f, 0f), new Vector3(36f, 0.5f, 24f), stone, resolve.transform);
            for (int index = 0; index < 6; index++)
            {
                CreateBox($"Resolve Ghat Step {index + 1}", new Vector3(0f, -0.4f - index * 0.25f, 9f + index * 2.2f), new Vector3(34f, 0.42f, 2.4f), stone, resolve.transform);
            }
            CreateBox("Resolve Ganga", new Vector3(0f, -2.0f, 23f), new Vector3(56f, 0.3f, 20f), water, resolve.transform);
            CreateBox("Resolve Yamuna", new Vector3(12f, -1.92f, 22f), new Vector3(30f, 0.26f, 18f), yamunaWater, resolve.transform);
            GameObject resolveAzad = ParentPerson(CreatePerson("Azad Resolve", new Vector3(0f, 0f, 3.8f), shirt, trousers, skin, hair, true), resolve.transform);
            resolveAzad.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            GameObject resolveShanti = ParentPerson(CreatePerson("Shanti Resolve", new Vector3(-1.55f, 0f, 3.15f), shantiDress, darkStone, skin, hair, false), resolve.transform);
            AddScarf(resolveShanti.transform, shantiDress);
            GameObject resolveSandhya = ParentPerson(CreatePerson("Sandhya Resolve", new Vector3(1.55f, 0f, 3.20f), sandhyaDress, darkStone, skin, hair, false), resolve.transform);
            AddPigtails(resolveSandhya.transform, hair);
            resolveSandhya.transform.localScale = Vector3.one * 0.72f;
            for (int index = 0; index < 12; index++)
            {
                ParentPerson(CreatePerson($"Helpers Hand Citizen {index + 1}",
                    new Vector3(-10.5f + (index % 6) * 4.2f, 0f, 7.0f + (index / 6) * 2.6f),
                    index % 3 == 0 ? yellow : index % 2 == 0 ? teal : volunteerDress,
                    darkStone, skin, hair, false), resolve.transform);
            }
            GameObject resolveFlag = new GameObject("Helpers Hand Resolve Flag");
            resolveFlag.transform.SetParent(resolve.transform);
            resolveFlag.transform.localPosition = new Vector3(0f, 0f, 8.3f);
            CreatePrimitiveChild("Resolve Flag Pole", PrimitiveType.Cylinder, resolveFlag.transform, new Vector3(0f, 3.0f, 0f), new Vector3(0.08f, 3f, 0.08f), darkStone);
            CreatePrimitiveChild("Resolve Flag Teal", PrimitiveType.Cube, resolveFlag.transform, new Vector3(1.05f, 5.0f, 0f), new Vector3(2.0f, 0.72f, 0.08f), teal)
                .AddComponent<WorldMotion>().Configure(WorldMotionKind.Sway, 5f, 1.4f, Vector3.up);
            CreatePrimitiveChild("Resolve Flag Yellow", PrimitiveType.Cube, resolveFlag.transform, new Vector3(1.05f, 4.32f, 0f), new Vector3(2.0f, 0.62f, 0.08f), yellow)
                .AddComponent<WorldMotion>().Configure(WorldMotionKind.Sway, 4f, 1.55f, Vector3.up);
            for (int index = 0; index < 9; index++)
            {
                CreatePrimitiveChild($"Resolve Diya {index + 1}", PrimitiveType.Sphere, resolve.transform,
                    new Vector3(-6f + index * 1.5f, 0.12f, 5.7f), new Vector3(0.20f, 0.12f, 0.20f), yellow)
                    .AddComponent<WorldMotion>().Configure(WorldMotionKind.Float, 0.03f, 2.2f + index * 0.08f, Vector3.up);
            }
            CreateCinematicCameraPoints(resolve.transform, new Vector3(5.8f, 2.75f, -3.3f), new Vector3(0f, 1.1f, 4.0f), anchors, focuses);

            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            Camera cinematicCamera = cameraObject.AddComponent<Camera>();
            cinematicCamera.clearFlags = CameraClearFlags.Skybox;
            cinematicCamera.fieldOfView = 56f;
            cinematicCamera.nearClipPlane = 0.12f;
            cinematicCamera.farClipPlane = 180f;
            cinematicCamera.allowHDR = false;
            cinematicCamera.allowMSAA = false;
            cameraObject.AddComponent<AudioListener>();

            GameObject directorObject = new GameObject("Visual Backstory Director");
            PrologueCinematic director = directorObject.AddComponent<PrologueCinematic>();
            director.Configure(
                cinematicCamera,
                "Prototype01",
                roots.ToArray(),
                anchors.ToArray(),
                focuses.ToArray(),
                new[] { 3.2f, 3.2f, 3.0f, 3.0f, 3.8f, 3.0f, 4.2f });
            for (int index = 1; index < roots.Count; index++)
            {
                roots[index].SetActive(false);
            }
            CreateOpenWorldLighting();
            EditorSceneManager.MarkSceneDirty(prologueScene);
            EditorSceneManager.SaveScene(prologueScene, PrologueScenePath);
        }

        private static void BuildFreeRoamScene(
            Material sand, Material stone, Material darkStone, Material water,
            Material teal, Material yellow, Material white, Material shirt, Material trousers,
            Material skin, Material hair, Material shantiDress, Material sandhyaDress,
            Material policeKhaki, Material volunteerDress, Material foliage, Material trunk)
        {
            Scene freeRoamScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            Material road = CreateMaterial("OpenWorldRoad", new Color(0.16f, 0.19f, 0.20f), 0.10f);
            Material roadLine = CreateMaterial("OpenWorldRoadLine", new Color(0.92f, 0.79f, 0.22f), 0.08f);
            Material grass = CreateMaterial("OpenWorldGrass", new Color(0.29f, 0.48f, 0.27f), 0.04f);
            Material cleanWater = CreateMaterial("CleanGangaWater", new Color(0.07f, 0.48f, 0.66f), 0.68f);
            Material brick = CreateMaterial("OpenWorldBrick", new Color(0.58f, 0.25f, 0.17f), 0.10f);
            Material cream = CreateMaterial("OpenWorldCream", new Color(0.84f, 0.79f, 0.64f), 0.14f);
            Material glass = CreateMaterial("OpenWorldGlass", new Color(0.18f, 0.44f, 0.56f), 0.75f);
            Material marketRed = CreateMaterial("MarketRed", new Color(0.70f, 0.13f, 0.10f), 0.12f);
            Material carRed = CreateMaterial("VehicleRed", new Color(0.67f, 0.06f, 0.05f), 0.48f);
            Material scooterBlue = CreateMaterial("ScooterBlue", new Color(0.05f, 0.31f, 0.57f), 0.46f);
            Material tyre = CreateMaterial("VehicleTyre", new Color(0.025f, 0.028f, 0.03f), 0.03f);

            GameObject world = new GameObject("Connected Prayagraj Free Roam World");
            CreateOpenWorldGround(world.transform, grass, sand, stone, road, roadLine, darkStone, cleanWater, white);
            CreateOpenWorldDistricts(world.transform, brick, cream, teal, yellow, white, glass, darkStone, foliage, trunk);
            CreateOpenWorldGhatsAndMarket(world.transform, stone, darkStone, cleanWater, teal, yellow, white, marketRed, foliage, trunk);
            CreateOpenWorldStreetLife(world.transform, shirt, trousers, shantiDress, volunteerDress, teal, yellow, darkStone, skin, hair);

            CreateDrivableCar("Azad Compact Car", new Vector3(0f, 0.08f, -72f), Quaternion.identity,
                carRed, glass, tyre, darkStone, yellow, shirt, trousers, skin, hair, world.transform);
            CreateDrivableScooter("Helpers Hand Scooter", new Vector3(-52f, 0.08f, 0f), Quaternion.Euler(0f, 90f, 0f),
                scooterBlue, tyre, darkStone, yellow, shirt, trousers, skin, hair, world.transform);
            CreateParkedTraffic(world.transform, carRed, scooterBlue, glass, tyre, darkStone, cream);
            CreateAmbientTraffic(world.transform, marketRed, teal, yellow, cream, glass, tyre, darkStone);

            GameObject systems = new GameObject("Free Roam Systems");
            systems.AddComponent<GameSession>();
            PrototypeInput freeRoamInput = systems.AddComponent<PrototypeInput>();
            freeRoamInput.SetActionLabel("USE");
            PrototypeAudio freeRoamAudio = systems.AddComponent<PrototypeAudio>();
            systems.AddComponent<FreeRoamAutomation>();

            GameObject azad = CreatePerson("Azad Free Roam", new Vector3(104f, 0f, -48f), shirt, trousers, skin, hair, true);
            AzadController controller = azad.AddComponent<AzadController>();
            CharacterController characterController = azad.GetComponent<CharacterController>();
            characterController.center = new Vector3(0f, 0.9f, 0f);
            characterController.height = 1.8f;
            characterController.radius = 0.34f;
            characterController.stepOffset = 0.42f;
            characterController.slopeLimit = 48f;
            azad.AddComponent<ProceduralWalker>();
            SetLayerRecursively(azad, 2);
            freeRoamAudio.ConfigureOpenWorld(azad.transform);

            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            Camera gameCamera = cameraObject.AddComponent<Camera>();
            gameCamera.clearFlags = CameraClearFlags.Skybox;
            gameCamera.fieldOfView = 62f;
            gameCamera.nearClipPlane = 0.12f;
            gameCamera.farClipPlane = 650f;
            gameCamera.allowHDR = false;
            gameCamera.allowMSAA = true;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCamera orbitCamera = cameraObject.AddComponent<ThirdPersonCamera>();
            orbitCamera.SetCollisionMask(~(1 << 2));
            orbitCamera.SetTarget(azad.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
            cameraObject.transform.position = new Vector3(104f, 4.2f, -55f);
            controller.SetCamera(cameraObject.transform);
            OpenWorldPresentation presentation = systems.AddComponent<OpenWorldPresentation>();
            presentation.Configure(gameCamera);

            FreeRoamMapHud mapHud = systems.AddComponent<FreeRoamMapHud>();
            mapHud.Configure(
                azad.transform,
                new Vector2(-230f, -230f),
                new Vector2(230f, 230f),
                new[]
                {
                    new Vector3(135f, 0f, -35f), new Vector3(-145f, 0f, 112f),
                    new Vector3(-28f, 0f, 116f), new Vector3(-128f, 0f, 24f),
                    new Vector3(-145f, 0f, -130f), new Vector3(28f, 0f, -72f),
                    new Vector3(78f, 0f, 74f), new Vector3(82f, 0f, -142f),
                    new Vector3(34f, 0f, -182f)
                },
                new[]
                {
                    "Daraganj Ghats", "Allahabad Univ.", "High Court", "Azad Park",
                    "Allahpur", "Loknath Market", "Sangam Mall", "Seva Hospital", "Story Mission"
                });
            StoryHubController storyHub = CreateStoryMissionMarker(world.transform, controller, teal, yellow, darkStone);
            GameObject chapterOneRoot = CreateOpenWorldChapterOneMission(
                world.transform,
                teal,
                yellow,
                darkStone,
                shirt,
                trousers,
                skin,
                hair,
                shantiDress,
                sandhyaDress,
                policeKhaki,
                volunteerDress,
                marketRed);
            GameObject chapterTwoRoot = CreateOpenWorldChapterTwoMission(
                world.transform,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                skin,
                hair,
                shantiDress,
                policeKhaki,
                volunteerDress,
                foliage,
                trunk);
            GameObject chapterThreeRoot = CreateOpenWorldChapterThreeMission(
                world.transform,
                darkStone,
                teal,
                yellow,
                white,
                marketRed,
                skin,
                hair,
                shantiDress,
                sandhyaDress,
                policeKhaki,
                volunteerDress);
            GameObject chapterFourRoot = CreateOpenWorldChapterFourMission(
                world.transform,
                stone,
                darkStone,
                teal,
                yellow,
                white,
                marketRed,
                skin,
                hair,
                shantiDress,
                sandhyaDress,
                volunteerDress,
                policeKhaki,
                foliage,
                trunk);
            OpenWorldMissionDirector missionDirector = systems.AddComponent<OpenWorldMissionDirector>();
            missionDirector.Configure(
                controller,
                storyHub,
                chapterOneRoot,
                chapterTwoRoot,
                chapterThreeRoot,
                chapterFourRoot);
            CreateOpenWorldLighting();

            EditorSceneManager.MarkSceneDirty(freeRoamScene);
            EditorSceneManager.SaveScene(freeRoamScene, FreeRoamScenePath);
        }

        private static StoryHubController CreateStoryMissionMarker(
            Transform parent, AzadController player, Material teal, Material yellow, Material darkStone)
        {
            GameObject marker = new GameObject("Helpers Hand Story Mission");
            marker.transform.SetParent(parent);
            marker.transform.position = new Vector3(34f, 0f, -182f);
            SphereCollider trigger = marker.AddComponent<SphereCollider>();
            trigger.isTrigger = true;
            trigger.center = new Vector3(0f, 1f, 0f);
            trigger.radius = 2.4f;

            CreatePrimitiveChild("Mission Ground Ring", PrimitiveType.Cylinder, marker.transform,
                new Vector3(0f, 0.08f, 0f), new Vector3(2.3f, 0.08f, 2.3f), teal);
            CreatePrimitiveChild("Mission Inner Ring", PrimitiveType.Cylinder, marker.transform,
                new Vector3(0f, 0.14f, 0f), new Vector3(1.65f, 0.07f, 1.65f), yellow);
            CreatePrimitiveChild("Mission Beacon Stem", PrimitiveType.Cube, marker.transform,
                new Vector3(0f, 2.0f, 0f), new Vector3(0.34f, 2.5f, 0.34f), yellow)
                .AddComponent<WorldMotion>().Configure(WorldMotionKind.Float, 0.16f, 1.8f, Vector3.up);
            CreatePrimitiveChild("Mission Beacon Dot", PrimitiveType.Sphere, marker.transform,
                new Vector3(0f, 3.65f, 0f), new Vector3(0.75f, 0.75f, 0.75f), yellow)
                .AddComponent<WorldMotion>().Configure(WorldMotionKind.Float, 0.22f, 1.55f, Vector3.up);
            CreatePrimitiveChild("Mission Direction Base", PrimitiveType.Cube, marker.transform,
                new Vector3(0f, 0.32f, 0f), new Vector3(2.8f, 0.18f, 0.42f), darkStone);
            CreateWorldLabel("Story Mission Sign", "STORY MISSION", new Vector3(34f, 4.85f, -182f),
                Vector3.zero, yellow, marker.transform, 0.028f);

            StoryHubController hub = marker.AddComponent<StoryHubController>();
            hub.Configure(player, new Vector3(34f, 0f, -188f), Vector3.zero);
            return hub;
        }

        private static GameObject CreateOpenWorldChapterOneMission(
            Transform parent,
            Material teal,
            Material yellow,
            Material darkStone,
            Material shirt,
            Material trousers,
            Material skin,
            Material hair,
            Material shantiDress,
            Material sandhyaDress,
            Material policeKhaki,
            Material volunteerDress,
            Material litter)
        {
            GameObject root = new GameObject("Open World Chapter 01 - Ravivaar Ki Seva");
            root.transform.SetParent(parent);
            root.AddComponent<OpenWorldMissionHud>();
            MissionController mission = root.AddComponent<MissionController>();
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            GameObject shanti = ParentPerson(CreatePerson(
                "Mission Shanti", new Vector3(42f, 0f, -181f),
                shantiDress, darkStone, skin, hair, false), root.transform);
            AddScarf(shanti.transform, shantiDress);
            objectives.Add(AddObjective(
                shanti,
                "talk-shanti",
                "Shanti se seva route samjhein",
                "Shanti",
                "Azad, Daraganj ke teen spots par kachra jama hai. Main gloves aur bags de rahi hoon; tum ghat route dekh lo.",
                1,
                0,
                0,
                false));
            labels.Add("Helpers Hand par Shanti se safai route samjhein");

            MissionObjective litterOne = CreateLitterObjective(
                "Open World Litter Cluster A",
                new Vector3(120.5f, 0.05f, -145f),
                litter,
                "clean-a",
                "Pehla ghat spot saaf karein",
                "Azad",
                "Plastic alag rakho, wet waste alag. Safai photo ke liye nahi, roz ki aadat ke liye honi chahiye.");
            litterOne.transform.SetParent(root.transform, true);
            objectives.Add(litterOne);
            labels.Add("Daraganj ghat ka pehla litter spot saaf karein");

            MissionObjective litterTwo = CreateLitterObjective(
                "Open World Litter Cluster B",
                new Vector3(120.5f, 0.05f, -38f),
                litter,
                "clean-b",
                "Doosra ghat spot saaf karein",
                "Local Volunteer",
                "Azad bhaiya, yahan dustbin jaldi bhar jaata hai. Kal replacement ki written application bhi denge.");
            litterTwo.transform.SetParent(root.transform, true);
            objectives.Add(litterTwo);
            labels.Add("Daraganj promenade ka doosra litter spot saaf karein");

            MissionObjective litterThree = CreateLitterObjective(
                "Open World Litter Cluster C",
                new Vector3(120.5f, 0.05f, 78f),
                litter,
                "clean-c",
                "Teesra ghat spot saaf karein",
                "Azad",
                "Aakhri bag bhi ho gaya. Ab stall owners ke saath weekly safai rota banana hoga.");
            litterThree.transform.SetParent(root.transform, true);
            objectives.Add(litterThree);
            labels.Add("Ghat route ka aakhri litter spot saaf karein");

            GameObject coordinator = ParentPerson(CreatePerson(
                "Mission Volunteer Coordinator", new Vector3(111f, 0f, 132f),
                volunteerDress, darkStone, skin, hair, false), root.transform);
            CreatePrimitiveChild("Helpers Hand Badge", PrimitiveType.Cube, coordinator.transform,
                new Vector3(0f, 1.25f, 0.48f), new Vector3(0.20f, 0.20f, 0.04f), teal);
            objectives.Add(AddObjective(
                coordinator,
                "report-complete",
                "Volunteer coordinator ko report dein",
                "Volunteer Coordinator",
                "Route complete hai, Azad bhaiya. Agle Sunday do galiyan aur jodenge. Chhota kaam hai, lekin log saath aa rahe hain.",
                7,
                -50,
                3,
                false));
            labels.Add("Daraganj ke volunteer coordinator ko update dein");

            GameObject sandhya = ParentPerson(CreatePerson(
                "Mission Sandhya", new Vector3(-152f, 0f, -162f),
                sandhyaDress, darkStone, skin, hair, false), root.transform);
            AddPigtails(sandhya.transform, hair);
            sandhya.transform.localScale = Vector3.one * 0.72f;
            objectives.Add(AddObjective(
                sandhya,
                "talk-sandhya",
                "Sandhya se baat karein",
                "Sandhya",
                "Papa, Mumma ne ghar ki diary bahar mez par rakhi hai. Maine Helpers Hand ke bachchon ke liye copies bhi nikaal di hain.",
                1,
                0,
                1,
                false));
            labels.Add("Allahpur ghar par Sandhya se milen");

            CreateBox("Mission Home Ledger Table", new Vector3(-148.5f, 0.44f, -162f),
                new Vector3(1.5f, 0.82f, 1.0f), darkStone, root.transform);
            GameObject ledger = CreateBox("Mission Household Ledger", new Vector3(-148.5f, 0.92f, -162f),
                new Vector3(0.48f, 0.10f, 0.62f), yellow, root.transform);
            objectives.Add(AddObjective(
                ledger,
                "read-ledger",
                "Ghar ki diary dekhein",
                "Shanti ka Note",
                "Aaj ki tuition fees Rs 250. Rs 150 ghar ke liye, Rs 100 NGO photocopies ke liye. Hisaab saaf rahega toh hausla bhi saaf rahega.",
                0,
                250,
                0,
                false));
            labels.Add("Shanti ki household diary padhein");

            GameObject samrat = ParentPerson(CreatePerson(
                "Mission Constable Samrat", new Vector3(35f, 0f, 130f),
                policeKhaki, darkStone, skin, hair, false), root.transform);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            objectives.Add(AddObjective(
                samrat,
                "talk-samrat",
                "Constable Samrat se baat karein",
                "Constable Samrat",
                "Azad, amma ki pension file ka verification sahi hai. Main record entry nikalwa deta hoon; tum NGO se application aaj jama kara do.",
                3,
                0,
                2,
                false));
            labels.Add("Public Records Office par Samrat se file verify karayein");

            CreateBox("Mission Helpers Hand Desk", new Vector3(40f, 0.46f, -181f),
                new Vector3(2.2f, 0.86f, 1.2f), teal, root.transform);
            GameObject pensionFolder = CreateBox("Mission Pension Folder", new Vector3(40f, 0.96f, -181f),
                new Vector3(0.52f, 0.08f, 0.68f), yellow, root.transform);
            objectives.Add(AddObjective(
                pensionFolder,
                "submit-pension-file",
                "Pension file Helpers Hand me jama karein",
                "Azad",
                "Verification lag gaya. Ab receipt amma ko deni hai aur saat din baad status check karna hai. Madad tab poori hoti hai jab kaam daftar se bahar aa jaye.",
                5,
                -100,
                3,
                false));
            labels.Add("Helpers Hand desk par pension application jama karein");

            mission.Configure(
                "Ravivaar Ki Seva: Prayagraj Route",
                objectives,
                labels,
                "CHAPTER 1 COMPLETE",
                "Daraganj safai, Allahpur ghar aur pension file: Azad ka Sunday poora hua.");
            mission.ConfigureMilestones(
                new List<int> { 5, 7 },
                new List<string> { "GHAT ROUTE COMPLETE", "COMMUNITY CASE" },
                new List<string>
                {
                    "Safai report ho gayi. Ab Allahpur ghar par Sandhya intezar kar rahi hai.",
                    "Household diary dekh li. Ab Public Records Office par Samrat se milna hai."
                });
            mission.ConfigureChapter(1, "Chapter02");
            mission.ConfigureIntro(
                "AZAD / 31 / SOCIAL WORKER",
                "Helpers Hand se shuru hua Sunday route ab poore Prayagraj me chalega.");
            return root;
        }

        private static GameObject CreateOpenWorldChapterTwoMission(
            Transform parent,
            Material stone,
            Material darkStone,
            Material teal,
            Material yellow,
            Material white,
            Material skin,
            Material hair,
            Material shantiDress,
            Material policeKhaki,
            Material volunteerDress,
            Material foliage,
            Material trunk)
        {
            GameObject root = new GameObject("Open World Chapter 02 - Shaam Ki Paathshala");
            root.transform.SetParent(parent);
            root.AddComponent<OpenWorldMissionHud>();
            root.AddComponent<OpenWorldMissionAtmosphere>().ConfigureEvening();
            MissionController mission = root.AddComponent<MissionController>();
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            CreateBox("Allahpur Mission Learning Courtyard", new Vector3(-165f, -0.10f, -63f),
                new Vector3(38f, 0.22f, 34f), stone, root.transform);
            CreateBox("Allahpur Mission Community Hall", new Vector3(-165f, 2.5f, -44f),
                new Vector3(22f, 5f, 7f), teal, root.transform);
            CreateBox("Allahpur Mission Blackboard", new Vector3(-165f, 2.35f, -47.55f),
                new Vector3(8.4f, 2.55f, 0.18f), darkStone, root.transform);
            CreateWorldLabel("Allahpur Class Title", "HELPERS HAND EVENING CLASS",
                new Vector3(-165f, 4.45f, -47.42f), Vector3.zero, yellow, root.transform, 0.025f);
            CreateWorldLabel("Allahpur Blackboard Lesson", "FORM  /  ENGLISH  /  MATHS",
                new Vector3(-165f, 2.45f, -47.42f), Vector3.zero, white, root.transform, 0.017f);
            CreateTree("Mission Courtyard Neem", new Vector3(-178f, 0f, -54f), foliage, trunk, root.transform);
            CreateStreetLamp("Mission Courtyard Solar Lamp", new Vector3(-152f, 0f, -61f),
                darkStone, yellow, root.transform);
            CreateStreetLamp("Mission Courtyard Entry Lamp", new Vector3(-178f, 0f, -74f),
                darkStone, yellow, root.transform);
            CreateMissionPointLight("Courtyard Entry Warm Light", new Vector3(-178f, 4.1f, -74f), root.transform);
            CreateMissionPointLight("Courtyard Desk Warm Light", new Vector3(-158f, 4.2f, -61f), root.transform);
            CreateMissionPointLight("Blackboard Warm Light", new Vector3(-165f, 4.4f, -49f), root.transform);

            Vector3[] learnerPositions =
            {
                new Vector3(-176f, 0f, -59f), new Vector3(-173f, 0f, -53f),
                new Vector3(-163f, 0f, -56f), new Vector3(-157f, 0f, -53f),
                new Vector3(-175f, 0f, -49f), new Vector3(-156f, 0f, -48f)
            };
            for (int index = 0; index < learnerPositions.Length; index++)
            {
                Material top = index % 3 == 0 ? volunteerDress : index % 3 == 1 ? teal : yellow;
                GameObject learner = ParentPerson(CreatePerson(
                    $"Evening Class Learner {index + 1}", learnerPositions[index],
                    top, darkStone, skin, hair, false), root.transform);
                learner.transform.localScale = Vector3.one * (0.66f + (index % 2) * 0.06f);
                foreach (Collider learnerCollider in learner.GetComponentsInChildren<Collider>(true))
                {
                    learnerCollider.enabled = false;
                }
            }

            GameObject waitingParent = ParentPerson(CreatePerson(
                "Evening Class Waiting Parent", new Vector3(-180f, 0f, -67f),
                shantiDress, darkStone, skin, hair, false), root.transform);
            foreach (Collider parentCollider in waitingParent.GetComponentsInChildren<Collider>(true))
            {
                parentCollider.enabled = false;
            }

            GameObject shanti = ParentPerson(CreatePerson(
                "Mission 02 Shanti", new Vector3(-171f, 0f, -69f),
                shantiDress, darkStone, skin, hair, false), root.transform);
            AddScarf(shanti.transform, shantiDress);
            objectives.Add(AddObjective(
                shanti,
                "class-plan",
                "Shanti se class plan lein",
                "Shanti",
                "Azad, aaj admission forms bhi hain aur chhote bachchon ki English class bhi. Pehle do desks aur books ready kara do; main attendance bana rahi hoon.",
                1,
                0,
                0,
                false));
            labels.Add("Allahpur courtyard me Shanti se class plan samjhein");

            GameObject deskA = CreateStudyDesk(
                "Mission Study Desk A", new Vector3(-171f, 0f, -63f), darkStone, yellow);
            deskA.transform.SetParent(root.transform, true);
            objectives.Add(AddObjective(
                deskA, "desk-a", "Pehla desk lagayein", "Azad",
                "Desk seedha rahe, bachchon ko likhte waqt jagah milni chahiye.",
                1, -25, 1, false));
            labels.Add("Pehla study desk arrange karein");

            GameObject deskB = CreateStudyDesk(
                "Mission Study Desk B", new Vector3(-165f, 0f, -63f), darkStone, yellow);
            deskB.transform.SetParent(root.transform, true);
            objectives.Add(AddObjective(
                deskB, "desk-b", "Doosra desk lagayein", "Volunteer",
                "Is desk par admission form help hogi. Pens aur documents ke clips bhi rakh dete hain.",
                1, -25, 1, false));
            labels.Add("Doosra study desk arrange karein");

            GameObject books = new GameObject("Mission Donated Book Crate");
            books.transform.SetParent(root.transform);
            books.transform.position = new Vector3(-159f, 0.42f, -63f);
            CreatePrimitiveChild("Crate", PrimitiveType.Cube, books.transform, Vector3.zero,
                new Vector3(1.25f, 0.78f, 0.92f), darkStone);
            for (int index = 0; index < 5; index++)
            {
                Transform book = CreatePrimitiveChild(
                    $"Book {index + 1}", PrimitiveType.Cube, books.transform,
                    new Vector3(-0.38f + index * 0.19f, 0.48f + (index % 2) * 0.08f, 0f),
                    new Vector3(0.16f, 0.55f, 0.60f), index % 2 == 0 ? teal : yellow).transform;
                book.localRotation = Quaternion.Euler(0f, 0f, -8f + index * 4f);
            }
            objectives.Add(AddObjective(
                books, "books", "Donated books sort karein", "Azad",
                "Class 3 se 8 tak alag bundles. Naam likh denge, par kisi bachche ko kitab ke bina wapas nahi bhejna.",
                2, 0, 1, false));
            labels.Add("Donated books ko class-wise sort karein");

            GameObject raju = ParentPerson(CreatePerson(
                "Mission Student Raju", new Vector3(-168f, 0f, -56f),
                volunteerDress, darkStone, skin, hair, false), root.transform);
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

            GameObject samrat = ParentPerson(CreatePerson(
                "Mission 02 Constable Samrat", new Vector3(-155f, 0f, -69f),
                policeKhaki, darkStone, skin, hair, false), root.transform);
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

            GameObject solarSwitch = new GameObject("Mission Portable Solar Lamp Control");
            solarSwitch.transform.SetParent(root.transform);
            solarSwitch.transform.position = new Vector3(-152f, 0.92f, -61f);
            CreatePrimitiveChild("Battery", PrimitiveType.Cube, solarSwitch.transform, Vector3.zero,
                new Vector3(0.72f, 0.65f, 0.52f), teal);
            CreatePrimitiveChild("Switch", PrimitiveType.Cube, solarSwitch.transform,
                new Vector3(0f, 0.18f, 0.28f), new Vector3(0.18f, 0.18f, 0.08f), yellow);
            objectives.Add(AddObjective(
                solarSwitch,
                "solar-lamp",
                "Solar lamp activate karein",
                "Azad",
                "Temporary light chal gayi. Complaint receipt notice board par laga denge; permanent repair ka follow-up kal hoga.",
                3,
                -150,
                2,
                false));
            labels.Add("Class exit ke paas temporary solar lamp activate karein");

            GameObject teachingPoint = new GameObject("Mission Evening Teaching Point");
            teachingPoint.transform.SetParent(root.transform);
            teachingPoint.transform.position = new Vector3(-165f, 1.2f, -48.4f);
            CreatePrimitiveChild("Chalk Box", PrimitiveType.Cube, teachingPoint.transform, Vector3.zero,
                new Vector3(0.72f, 0.18f, 0.28f), white);
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

            GameObject coordinator = ParentPerson(CreatePerson(
                "Mission Helpers Hand Coordinator", new Vector3(-158f, 0f, -55f),
                volunteerDress, darkStone, skin, hair, false), root.transform);
            CreatePrimitiveChild("Helpers Hand Badge", PrimitiveType.Cube, coordinator.transform,
                new Vector3(0f, 1.25f, 0.48f), new Vector3(0.20f, 0.20f, 0.04f), teal);
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
                "Shaam Ki Paathshala: Allahpur Courtyard",
                objectives,
                labels,
                "CHAPTER 2 COMPLETE",
                "Forms, books, safety aur class: Allahpur ki paathshala roshan hui.");
            mission.ConfigureMilestones(
                new List<int> { 4, 7 },
                new List<string> { "ADMISSION HELP", "CLASS READY" },
                new List<string>
                {
                    "Desks aur books ready hain. Ab Raju ka admission form dekhna hai.",
                    "Safe exit light active hai. Evening class shuru ki ja sakti hai."
                });
            mission.ConfigureChapter(2, "Chapter03");
            mission.ConfigureIntro(
                "CHAPTER 2 / SHAAM KI PAATHSHALA",
                "Allahpur courtyard me forms, books aur safe evening class taiyaar karni hai.");
            return root;
        }

        private static GameObject CreateOpenWorldChapterThreeMission(
            Transform parent,
            Material darkStone,
            Material teal,
            Material yellow,
            Material white,
            Material alertRed,
            Material skin,
            Material hair,
            Material shantiDress,
            Material sandhyaDress,
            Material policeKhaki,
            Material volunteerDress)
        {
            GameObject root = new GameObject("Open World Chapter 03 - Sandhya Kahan Hai");
            root.transform.SetParent(parent);
            root.AddComponent<OpenWorldMissionHud>();
            root.AddComponent<OpenWorldMissionAtmosphere>().ConfigureNightSearch();
            MissionController mission = root.AddComponent<MissionController>();
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            CreateBox("Mission Allahpur Home Porch", new Vector3(-152f, 0.14f, -162f),
                new Vector3(10f, 0.25f, 7f), darkStone, root.transform);
            CreateWorldLabel("Mission Home Search Sign", "AZAD  /  SHANTI  /  SANDHYA",
                new Vector3(-152f, 3.7f, -166f), Vector3.zero, yellow, root.transform, 0.021f);
            CreateStreetLamp("Mission Home Search Lamp", new Vector3(-143f, 0f, -158f),
                darkStone, yellow, root.transform);
            CreateMissionPointLight(
                "Mission Home Warm Light", new Vector3(-143f, 4.1f, -158f), root.transform,
                new Color(1f, 0.62f, 0.25f), 2.5f, 18f);

            CreateBox("Mission Gupta Chai Stall", new Vector3(-92f, 1.55f, -154f),
                new Vector3(8f, 3.1f, 5f), teal, root.transform);
            CreateBox("Mission Gupta Chai Counter", new Vector3(-92f, 0.74f, -156.75f),
                new Vector3(6.2f, 1.35f, 0.85f), darkStone, root.transform);
            CreateBox("Mission Gupta Chai Canopy", new Vector3(-92f, 3.25f, -153.3f),
                new Vector3(8.6f, 0.28f, 6.0f), alertRed, root.transform);
            CreateWorldLabel("Mission Gupta Chai Sign", "GUPTA CHAI  /  CCTV",
                new Vector3(-92f, 2.6f, -156.88f), Vector3.zero, yellow, root.transform, 0.024f);
            CreateWorldLabel("Mission Gupta Chai Line", "CHAI THANDI  /  CLUE GARAM",
                new Vector3(-92f, 2.05f, -156.90f), Vector3.zero, white, root.transform, 0.014f);
            CreateMissionPointLight(
                "Mission CCTV Stall Light", new Vector3(-92f, 4.25f, -157f), root.transform,
                new Color(1f, 0.67f, 0.30f), 2.4f, 17f);

            CreateBox("Mission Helpers Search Desk", new Vector3(52f, 0.65f, -184f),
                new Vector3(8.8f, 1.3f, 3.1f), teal, root.transform);
            CreateBox("Mission Helpers Search Board", new Vector3(52f, 2.15f, -185.65f),
                new Vector3(9.5f, 2.1f, 0.18f), darkStone, root.transform);
            CreateWorldLabel("Mission Helpers Search Title", "HELPERS HAND  /  VERIFIED SEARCH",
                new Vector3(52f, 2.48f, -185.78f), Vector3.zero, yellow, root.transform, 0.019f);
            CreateWorldLabel("Mission Helpers Search Rule", "NO RUMOURS  /  TEAM ROUTES ONLY",
                new Vector3(52f, 1.85f, -185.79f), Vector3.zero, white, root.transform, 0.013f);
            CreateMissionPointLight(
                "Mission Search Desk Light", new Vector3(52f, 4.2f, -188f), root.transform,
                new Color(1f, 0.63f, 0.25f), 2.6f, 20f);

            CreateBox("Mission Neutral Police Help Desk", new Vector3(69f, 0.70f, -184f),
                new Vector3(10f, 1.4f, 3.2f), policeKhaki, root.transform);
            CreateBox("Mission Police Help Board", new Vector3(69f, 2.25f, -185.7f),
                new Vector3(10.8f, 2.3f, 0.18f), darkStone, root.transform);
            CreateWorldLabel("Mission Police Help Title", "NEUTRAL NIGHT HELP DESK",
                new Vector3(69f, 2.58f, -185.84f), Vector3.zero, white, root.transform, 0.021f);
            CreateWorldLabel("Mission Police Help Rule", "RECORD  /  VERIFY  /  RESPOND",
                new Vector3(69f, 1.92f, -185.85f), Vector3.zero, yellow, root.transform, 0.014f);
            CreatePrimitiveChild("Mission Police Beacon Red", PrimitiveType.Sphere, root.transform,
                new Vector3(66.8f, 3.55f, -185.5f), new Vector3(0.38f, 0.20f, 0.38f), alertRed)
                .AddComponent<WorldMotion>().Configure(WorldMotionKind.Float, 0.08f, 2.2f, Vector3.up);
            CreatePrimitiveChild("Mission Police Beacon Blue", PrimitiveType.Sphere, root.transform,
                new Vector3(71.2f, 3.55f, -185.5f), new Vector3(0.38f, 0.20f, 0.38f), teal)
                .AddComponent<WorldMotion>().Configure(WorldMotionKind.Float, 0.08f, 2.5f, Vector3.up);
            CreateMissionPointLight(
                "Mission Police Red Light", new Vector3(66.8f, 3.6f, -186f), root.transform,
                new Color(1f, 0.16f, 0.10f), 1.35f, 12f);
            CreateMissionPointLight(
                "Mission Police Blue Light", new Vector3(71.2f, 3.6f, -186f), root.transform,
                new Color(0.14f, 0.42f, 1f), 1.35f, 12f);

            Vector3[] searchLightPositions =
            {
                new Vector3(-126f, 4.0f, -140f), new Vector3(-98f, 4.0f, -142f),
                new Vector3(-111f, 4.0f, -125f), new Vector3(-75f, 4.0f, -135f),
                new Vector3(18f, 4.0f, -136f), new Vector3(46f, 4.0f, -178f)
            };
            for (int index = 0; index < searchLightPositions.Length; index++)
            {
                CreateMissionPointLight(
                    $"Mission Search Pool {index + 1}", searchLightPositions[index], root.transform,
                    new Color(1f, 0.70f, 0.34f), 1.65f, 15f);
            }

            Vector3[] volunteerPositions =
            {
                new Vector3(-132f, 0f, -141f), new Vector3(-101f, 0f, -139f),
                new Vector3(-84f, 0f, -146f), new Vector3(-68f, 0f, -133f),
                new Vector3(45f, 0f, -188f), new Vector3(58f, 0f, -188f)
            };
            for (int index = 0; index < volunteerPositions.Length; index++)
            {
                Material top = index % 3 == 0 ? volunteerDress : index % 3 == 1 ? teal : yellow;
                GameObject volunteer = ParentPerson(CreatePerson(
                    $"Mission Search Volunteer {index + 1}", volunteerPositions[index],
                    top, darkStone, skin, hair, false), root.transform);
                volunteer.transform.localScale = Vector3.one * 0.92f;
                Transform torch = CreatePrimitiveChild(
                    "Search Torch", PrimitiveType.Cylinder, volunteer.transform,
                    new Vector3(0.42f, 1.0f, 0.22f), new Vector3(0.055f, 0.22f, 0.055f), yellow).transform;
                torch.localRotation = Quaternion.Euler(72f, 0f, 20f);
                foreach (Collider volunteerCollider in volunteer.GetComponentsInChildren<Collider>(true))
                {
                    volunteerCollider.enabled = false;
                }
            }

            GameObject shanti = ParentPerson(CreatePerson(
                "Mission 03 Shanti", new Vector3(-152f, 0f, -162f),
                shantiDress, darkStone, skin, hair, false), root.transform);
            AddScarf(shanti.transform, shantiDress);
            objectives.Add(AddObjective(
                shanti, "missing-alert", "Shanti se poori baat samjhein", "Shanti",
                "Azad, Sandhya library se abhi tak nahi lauti. Uska phone watch bhi unreachable hai. Ghabrana nahi hai, par ek minute bhi waste nahi karna.",
                0, 0, 0, false));
            labels.Add("Allahpur ghar par Shanti se missing alert samjhein");

            GameObject ribbon = new GameObject("Mission Sandhya Blue Ribbon");
            ribbon.transform.SetParent(root.transform);
            ribbon.transform.position = new Vector3(-126f, 0.18f, -140f);
            CreatePrimitiveChild("Ribbon Clue Marker", PrimitiveType.Cylinder, ribbon.transform,
                new Vector3(0f, -0.11f, 0f), new Vector3(0.72f, 0.025f, 0.72f), darkStone);
            CreatePrimitiveChild("Ribbon Left", PrimitiveType.Cube, ribbon.transform,
                new Vector3(-0.13f, 0f, 0f), new Vector3(0.12f, 0.05f, 0.62f), sandhyaDress)
                .transform.localRotation = Quaternion.Euler(0f, 24f, 0f);
            CreatePrimitiveChild("Ribbon Right", PrimitiveType.Cube, ribbon.transform,
                new Vector3(0.13f, 0f, 0f), new Vector3(0.12f, 0.05f, 0.62f), sandhyaDress)
                .transform.localRotation = Quaternion.Euler(0f, -24f, 0f);
            foreach (Collider ribbonCollider in ribbon.GetComponentsInChildren<Collider>(true))
            {
                ribbonCollider.enabled = false;
            }
            objectives.Add(AddObjective(
                ribbon, "blue-ribbon", "Neela ribbon inspect karein", "Azad",
                "Yeh Sandhya ke school ribbon jaisa hai. Pehle witness se confirm karte hain, phir Samrat ko exact location denge.",
                1, 0, 1, false));
            labels.Add("Allahpur lane me mile blue ribbon ko inspect karein");

            GameObject meera = ParentPerson(CreatePerson(
                "Mission Neighbor Meera", new Vector3(-98f, 0f, -142f),
                volunteerDress, darkStone, skin, hair, false), root.transform);
            objectives.Add(AddObjective(
                meera, "last-seen", "Meera ji se last-seen poochhein", "Meera",
                "Maine use stationery shop ke paas dekha tha. Ek safed van do baar lane mein ghoomi thi. Number poora nahi, bas aakhri mein 27 yaad hai.",
                1, 0, 1, false));
            labels.Add("Neighbor Meera se verified last-seen detail lein");

            GameObject teaOwner = ParentPerson(CreatePerson(
                "Mission Gupta Chai Owner", new Vector3(-95f, 0f, -158.4f),
                yellow, darkStone, skin, hair, false), root.transform);
            objectives.Add(AddObjective(
                teaOwner, "cctv-access", "Gupta ji se CCTV access lein", "Gupta Ji",
                "Camera chalu tha, lekin backup unit ka switch trip ho gaya. Aaj chai se zyada recording kaam aayegi; switch counter ke peeche hai.",
                0, 0, 1, false));
            labels.Add("Gupta Chai ke camera ka access lein");

            GameObject cameraSwitch = new GameObject("Mission CCTV Backup Switch");
            cameraSwitch.transform.SetParent(root.transform);
            cameraSwitch.transform.position = new Vector3(-88.7f, 1.0f, -156.8f);
            CreatePrimitiveChild("Switch Box", PrimitiveType.Cube, cameraSwitch.transform,
                Vector3.zero, new Vector3(0.65f, 0.85f, 0.30f), darkStone);
            CreatePrimitiveChild("Switch Lever", PrimitiveType.Cube, cameraSwitch.transform,
                new Vector3(0f, 0.05f, 0.20f), new Vector3(0.16f, 0.42f, 0.10f), yellow)
                .transform.localRotation = Quaternion.Euler(0f, 0f, -18f);
            objectives.Add(AddObjective(
                cameraSwitch, "cctv-power", "CCTV backup chalu karein", "Azad",
                "Backup aa gaya. Recording ko phone par copy karne ke liye data pack aur card reader lena hoga.",
                0, -50, 1, false));
            labels.Add("Tea stall ka CCTV backup power restore karein");

            GameObject cctvScreen = new GameObject("Mission CCTV Evidence Screen");
            cctvScreen.transform.SetParent(root.transform);
            cctvScreen.transform.position = new Vector3(-92f, 1.45f, -156.82f);
            cctvScreen.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            CreatePrimitiveChild("Monitor", PrimitiveType.Cube, cctvScreen.transform,
                Vector3.zero, new Vector3(2.0f, 1.25f, 0.22f), darkStone);
            CreatePrimitiveChild("Footage", PrimitiveType.Cube, cctvScreen.transform,
                new Vector3(0f, 0f, -0.13f), new Vector3(1.70f, 0.96f, 0.05f), teal);
            CreatePrimitiveChild("White Van Frame", PrimitiveType.Cube, cctvScreen.transform,
                new Vector3(-0.15f, -0.08f, -0.18f), new Vector3(0.9f, 0.38f, 0.04f), white);
            CreatePrimitiveChild("Plate 27", PrimitiveType.Cube, cctvScreen.transform,
                new Vector3(0.32f, -0.24f, -0.22f), new Vector3(0.26f, 0.10f, 0.02f), yellow);
            objectives.Add(AddObjective(
                cctvScreen, "cctv-clue", "CCTV footage verify karein", "Azad",
                "Safed van, 8:17 PM. Plate ka hissa clear hai: ...27. Sandhya frame mein hai, aur van old godown wali road par mudti hai.",
                1, 0, 0, false));
            labels.Add("CCTV recording se vehicle aur route clue nikalein");

            GameObject coordinator = ParentPerson(CreatePerson(
                "Mission Search Coordinator", new Vector3(48f, 0f, -187f),
                volunteerDress, darkStone, skin, hair, false), root.transform);
            CreatePrimitiveChild("Helpers Hand Badge", PrimitiveType.Cube, coordinator.transform,
                new Vector3(0f, 1.25f, 0.48f), new Vector3(0.20f, 0.20f, 0.04f), teal);
            objectives.Add(AddObjective(
                coordinator, "search-network", "Verified search teams activate karein", "Helpers Hand Coordinator",
                "Do verified teams nikal rahe hain. Photo sirf team phones par rahegi, public rumour nahi. Shanti ke paas ek volunteer rukega.",
                1, 0, 1, false));
            labels.Add("Helpers Hand ka verified search network activate karein");

            GameObject samrat = ParentPerson(CreatePerson(
                "Mission 03 Constable Samrat", new Vector3(66f, 0f, -187f),
                policeKhaki, darkStone, skin, hair, false), root.transform);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            objectives.Add(AddObjective(
                samrat, "samrat-response", "Samrat ko evidence dein", "Constable Samrat",
                "Main control room aur patrol ko partial plate bhej raha hoon. Tum facts likhwao; pressure ya phone aaye toh seedha mere saamne record hoga.",
                1, 0, 1, false));
            labels.Add("Neutral help desk par Samrat ko ribbon aur CCTV evidence dein");

            GameObject incidentBoard = new GameObject("Mission Incident Evidence Board");
            incidentBoard.transform.SetParent(root.transform);
            incidentBoard.transform.position = new Vector3(72f, 1.35f, -186.0f);
            incidentBoard.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            CreatePrimitiveChild("Board", PrimitiveType.Cube, incidentBoard.transform,
                Vector3.zero, new Vector3(2.9f, 1.9f, 0.18f), white);
            CreatePrimitiveChild("Photo Marker", PrimitiveType.Cube, incidentBoard.transform,
                new Vector3(-0.72f, 0.35f, -0.14f), new Vector3(0.62f, 0.65f, 0.04f), sandhyaDress);
            CreatePrimitiveChild("Vehicle Note", PrimitiveType.Cube, incidentBoard.transform,
                new Vector3(0.65f, 0.25f, -0.14f), new Vector3(0.82f, 0.42f, 0.04f), yellow);
            CreatePrimitiveChild("Route Note", PrimitiveType.Cube, incidentBoard.transform,
                new Vector3(0f, -0.48f, -0.14f), new Vector3(1.65f, 0.34f, 0.04f), teal);
            objectives.Add(AddObjective(
                incidentBoard, "incident-record", "Incident record verify karein", "Azad",
                "Time, route, ribbon, partial plate aur witness statement sab match kar rahe hain. Andaza nahi, sirf verified facts aage jayenge.",
                2, 0, 2, false));
            labels.Add("Police help desk par incident record verify karein");

            GameObject routeMap = new GameObject("Mission Old Godown Route Map");
            routeMap.transform.SetParent(root.transform);
            routeMap.transform.position = new Vector3(54f, 1.25f, -186.0f);
            routeMap.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            CreatePrimitiveChild("Map Board", PrimitiveType.Cube, routeMap.transform,
                Vector3.zero, new Vector3(2.6f, 1.5f, 0.12f), white);
            CreatePrimitiveChild("Route A", PrimitiveType.Cube, routeMap.transform,
                new Vector3(-0.45f, 0.15f, -0.09f), new Vector3(1.05f, 0.10f, 0.04f), teal)
                .transform.localRotation = Quaternion.Euler(0f, 0f, 24f);
            CreatePrimitiveChild("Route B", PrimitiveType.Cube, routeMap.transform,
                new Vector3(0.42f, -0.14f, -0.09f), new Vector3(1.12f, 0.10f, 0.04f), teal)
                .transform.localRotation = Quaternion.Euler(0f, 0f, -18f);
            CreatePrimitiveChild("Godown Marker", PrimitiveType.Sphere, routeMap.transform,
                new Vector3(0.92f, -0.42f, -0.12f), new Vector3(0.20f, 0.20f, 0.08f), yellow);
            objectives.Add(AddObjective(
                routeMap, "route-plan", "Safe search route confirm karein", "Search Coordinator",
                "Team A main road se, Team B river-side lane se. Koi akela godown ke andar nahi jayega; location milte hi Samrat ko update hoga.",
                1, 0, 1, false));
            labels.Add("Volunteer teams ke liye safe search route confirm karein");

            GameObject phone = new GameObject("Mission Unknown Caller Phone");
            phone.transform.SetParent(root.transform);
            phone.transform.position = new Vector3(-148.5f, 1.15f, -162f);
            CreatePrimitiveChild("Phone Body", PrimitiveType.Cube, phone.transform,
                Vector3.zero, new Vector3(0.34f, 0.62f, 0.12f), darkStone);
            CreatePrimitiveChild("Incoming Call", PrimitiveType.Cube, phone.transform,
                new Vector3(0f, 0.02f, -0.08f), new Vector3(0.24f, 0.38f, 0.04f), alertRed)
                .AddComponent<WorldMotion>().Configure(WorldMotionKind.Float, 0.05f, 3.8f, Vector3.up);
            objectives.Add(AddObjective(
                phone, "extortion-call", "Incoming call Samrat ke saath record karein", "Unknown Caller",
                "Rs 50 lakh tayyar rakho. Agla message ka wait karo. Phone band mat karna.",
                1, -100, 1, false));
            labels.Add("Allahpur ghar par unknown call ko police ke saath record karein");

            mission.Configure(
                "Sandhya Kahan Hai?: Night Search",
                objectives,
                labels,
                "CHAPTER 3 COMPLETE",
                "Verified search active hai. Samrat ke paas van route aur recorded call dono hain.");
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
            return root;
        }

        private static GameObject CreateOpenWorldChapterFourMission(
            Transform parent,
            Material stone,
            Material darkStone,
            Material teal,
            Material yellow,
            Material white,
            Material alertRed,
            Material skin,
            Material hair,
            Material shantiDress,
            Material sandhyaDress,
            Material volunteerDress,
            Material policeKhaki,
            Material foliage,
            Material trunk)
        {
            GameObject root = new GameObject("Open World Chapter 04 - Operation Umeed");
            root.transform.SetParent(parent);
            root.AddComponent<OpenWorldMissionHud>();
            root.AddComponent<OpenWorldMissionAtmosphere>().ConfigureDawnRescue();
            MissionController mission = root.AddComponent<MissionController>();
            List<MissionObjective> objectives = new List<MissionObjective>();
            List<string> labels = new List<string>();

            CreateBox("Mission Godown Yard", new Vector3(75f, -0.10f, 190f),
                new Vector3(54f, 0.22f, 42f), stone, root.transform);
            CreateBox("Mission Godown Access Road", new Vector3(75f, 0.02f, 158f),
                new Vector3(11f, 0.10f, 26f), darkStone, root.transform);
            CreateBox("Mission Godown Floor", new Vector3(75f, 0.04f, 201f),
                new Vector3(25f, 0.12f, 14f), darkStone, root.transform);
            CreateBox("Mission Godown Back Wall", new Vector3(75f, 3.6f, 207.8f),
                new Vector3(25f, 7.2f, 0.7f), darkStone, root.transform);
            CreateBox("Mission Godown Left Wall", new Vector3(62.8f, 3.6f, 201f),
                new Vector3(0.7f, 7.2f, 14f), darkStone, root.transform);
            CreateBox("Mission Godown Right Wall", new Vector3(87.2f, 3.6f, 201f),
                new Vector3(0.7f, 7.2f, 14f), darkStone, root.transform);
            CreateBox("Mission Godown Roof", new Vector3(75f, 7.2f, 201f),
                new Vector3(25.5f, 0.45f, 14.5f), teal, root.transform);
            CreateBox("Mission Godown Front Left", new Vector3(66f, 3.6f, 194.1f),
                new Vector3(6f, 7.2f, 0.7f), darkStone, root.transform);
            CreateBox("Mission Godown Front Right", new Vector3(84f, 3.6f, 194.1f),
                new Vector3(6f, 7.2f, 0.7f), darkStone, root.transform);
            CreateWorldLabel("Mission Godown Sign", "OLD RIVERSIDE STORAGE  /  FICTIONAL",
                new Vector3(75f, 5.8f, 193.7f), Vector3.zero, yellow, root.transform, 0.022f);

            CreateBox("Mission Family Safe Tent Floor", new Vector3(56f, 0.06f, 176f),
                new Vector3(12f, 0.12f, 9f), teal, root.transform);
            CreateBox("Mission Family Safe Tent Back", new Vector3(56f, 1.7f, 180.4f),
                new Vector3(12f, 3.4f, 0.35f), teal, root.transform);
            CreateBox("Mission Family Safe Tent Roof", new Vector3(56f, 3.35f, 176f),
                new Vector3(12.5f, 0.25f, 9.5f), white, root.transform);
            CreateWorldLabel("Mission Family Safe Point Sign", "FAMILY SAFE POINT",
                new Vector3(56f, 2.5f, 180.18f), Vector3.zero, yellow, root.transform, 0.022f);

            CreateBox("Mission Police Barricade Left", new Vector3(69f, 0.65f, 176f),
                new Vector3(8f, 1.3f, 0.35f), policeKhaki, root.transform);
            CreateBox("Mission Police Barricade Right", new Vector3(81f, 0.65f, 176f),
                new Vector3(8f, 1.3f, 0.35f), policeKhaki, root.transform);
            CreateWorldLabel("Mission Barricade Text", "POLICE  /  KEEP CLEAR",
                new Vector3(75f, 1.38f, 175.80f), Vector3.zero, darkStone, root.transform, 0.018f);

            GameObject van = new GameObject("Mission Suspect White Van");
            van.transform.SetParent(root.transform);
            van.transform.position = new Vector3(86f, 0f, 184f);
            CreatePrimitiveChild("Van Body", PrimitiveType.Cube, van.transform,
                new Vector3(0f, 1.25f, 0f), new Vector3(4.8f, 2.5f, 2.2f), white);
            CreatePrimitiveChild("Van Cabin", PrimitiveType.Cube, van.transform,
                new Vector3(1.55f, 1.4f, 0f), new Vector3(1.8f, 2.1f, 2.1f), white);
            CreatePrimitiveChild("Van Side Scratch", PrimitiveType.Cube, van.transform,
                new Vector3(-0.6f, 1.2f, -1.13f), new Vector3(1.4f, 0.10f, 0.04f), alertRed)
                .transform.localRotation = Quaternion.Euler(0f, 0f, -16f);
            CreatePrimitiveChild("Van Plate 27", PrimitiveType.Cube, van.transform,
                new Vector3(-2.42f, 0.92f, 0f), new Vector3(0.04f, 0.34f, 0.86f), yellow);
            for (int index = 0; index < 4; index++)
            {
                float x = index < 2 ? -1.45f : 1.45f;
                float z = index % 2 == 0 ? -1.05f : 1.05f;
                CreatePrimitiveChild($"Mission Van Wheel {index + 1}", PrimitiveType.Cylinder, van.transform,
                    new Vector3(x, 0.45f, z), new Vector3(0.42f, 0.18f, 0.42f), darkStone)
                    .transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            }

            for (int index = 0; index < 5; index++)
            {
                CreateBox($"Mission Storage Crate {index + 1}",
                    new Vector3(66f + index * 4.2f, 0.7f, 205f),
                    new Vector3(2.2f, 1.4f, 2.2f), index % 2 == 0 ? stone : teal, root.transform);
            }
            CreateTree("Mission Godown Neem", new Vector3(54f, 0f, 194f), foliage, trunk, root.transform);
            CreateStreetLamp("Mission Perimeter Lamp Left", new Vector3(65f, 0f, 173f),
                darkStone, yellow, root.transform);
            CreateStreetLamp("Mission Perimeter Lamp Right", new Vector3(85f, 0f, 173f),
                darkStone, yellow, root.transform);
            CreateMissionPointLight(
                "Mission Perimeter Warm Light Left", new Vector3(65f, 4.2f, 173f), root.transform,
                new Color(1f, 0.64f, 0.28f), 2.2f, 19f);
            CreateMissionPointLight(
                "Mission Perimeter Warm Light Right", new Vector3(85f, 4.2f, 173f), root.transform,
                new Color(1f, 0.64f, 0.28f), 2.2f, 19f);
            CreateMissionPointLight(
                "Mission Godown Interior Light", new Vector3(75f, 5.8f, 202f), root.transform,
                new Color(0.72f, 0.82f, 1f), 1.8f, 20f);

            Vector3[] backupPositions =
            {
                new Vector3(64f, 0f, 179f), new Vector3(69f, 0f, 180f),
                new Vector3(80f, 0f, 180f), new Vector3(84f, 0f, 179f)
            };
            for (int index = 0; index < backupPositions.Length; index++)
            {
                GameObject officer = ParentPerson(CreatePerson(
                    $"Mission Backup Officer {index + 1}", backupPositions[index],
                    policeKhaki, darkStone, skin, hair, false), root.transform);
                AddPoliceDetails(officer.transform, policeKhaki, darkStone);
                foreach (Collider officerCollider in officer.GetComponentsInChildren<Collider>(true))
                {
                    officerCollider.enabled = false;
                }
            }
            GameObject medic = ParentPerson(CreatePerson(
                "Mission Medical Volunteer", new Vector3(59f, 0f, 174f),
                white, darkStone, skin, hair, false), root.transform);
            CreatePrimitiveChild("Medical Satchel", PrimitiveType.Cube, medic.transform,
                new Vector3(0.48f, 0.92f, 0f), new Vector3(0.22f, 0.55f, 0.48f), alertRed);
            foreach (Collider medicCollider in medic.GetComponentsInChildren<Collider>(true))
            {
                medicCollider.enabled = false;
            }

            GameObject samrat = ParentPerson(CreatePerson(
                "Mission 04 Constable Samrat", new Vector3(70f, 0f, 173f),
                policeKhaki, darkStone, skin, hair, false), root.transform);
            AddPoliceDetails(samrat.transform, policeKhaki, darkStone);
            objectives.Add(AddObjective(
                samrat, "rescue-brief", "Samrat se rescue brief lein", "Constable Samrat",
                "Van mil gayi hai. Backup paanch minute door hai. Pehle plate verify karo aur watch receiver ka signal lock karo; bina plan koi andar nahi jayega.",
                1, 0, 1, false));
            labels.Add("Riverside perimeter par Samrat se rescue briefing lein");

            objectives.Add(AddObjective(
                van, "verify-van", "Van ki plate verify karein", "Azad",
                "Plate ka aakhri number 27 hai aur side par wahi scratch hai jo CCTV mein tha. Evidence photo Samrat ko bhej diya.",
                1, 0, 1, false));
            labels.Add("CCTV clue se suspect van verify karein");

            GameObject receiver = new GameObject("Mission Sandhya Watch Receiver");
            receiver.transform.SetParent(root.transform);
            receiver.transform.position = new Vector3(81f, 0.95f, 173f);
            CreatePrimitiveChild("Receiver Body", PrimitiveType.Cube, receiver.transform,
                Vector3.zero, new Vector3(0.78f, 0.58f, 0.32f), darkStone);
            CreatePrimitiveChild("Signal Screen", PrimitiveType.Cube, receiver.transform,
                new Vector3(0f, 0.04f, -0.19f), new Vector3(0.55f, 0.30f, 0.05f), teal);
            CreatePrimitiveChild("Antenna", PrimitiveType.Cylinder, receiver.transform,
                new Vector3(0.28f, 0.52f, 0f), new Vector3(0.05f, 0.34f, 0.05f), yellow);
            objectives.Add(AddObjective(
                receiver, "watch-signal", "Watch signal lock karein", "Volunteer",
                "Weak signal main godown se aa raha hai. Receiver ka live location police tablet par share ho gaya.",
                1, -50, 1, false));
            labels.Add("Sandhya ki watch ka short-range signal lock karein");

            GameObject briefingTable = new GameObject("Mission Rescue Approach Board");
            briefingTable.transform.SetParent(root.transform);
            briefingTable.transform.position = new Vector3(75f, 1.15f, 173f);
            CreatePrimitiveChild("Plan Board", PrimitiveType.Cube, briefingTable.transform,
                Vector3.zero, new Vector3(2.8f, 1.55f, 0.18f), white);
            CreatePrimitiveChild("Safe Route", PrimitiveType.Cube, briefingTable.transform,
                new Vector3(-0.5f, 0.16f, -0.12f), new Vector3(1.15f, 0.10f, 0.04f), teal)
                .transform.localRotation = Quaternion.Euler(0f, 0f, 22f);
            CreatePrimitiveChild("Risk Route", PrimitiveType.Cube, briefingTable.transform,
                new Vector3(0.52f, -0.20f, -0.12f), new Vector3(1.05f, 0.10f, 0.04f), alertRed)
                .transform.localRotation = Quaternion.Euler(0f, 0f, -26f);
            MissionObjective decision = AddObjective(
                briefingTable, "rescue-decision", "Rescue approach chunein", "Azad",
                "Samrat, perimeter aur backup ke saath chalenge. Sandhya ki safety mere ego se badi hai.",
                3, -100, 3, false);
            decision.ConfigureDecision(
                "rescue-approach",
                "RESCUE APPROACH",
                "Backup bas kuch minute door hai. Coordinated entry safer hai; solo entry tez lag sakti hai, par Sandhya aur team dono ko risk hoga.",
                "SAMRAT KA PLAN\nFunds -100 / Trust +3",
                "AKELA SIDE GATE\nTrust -4 / Rep -3",
                "Azad akela side gate tak badhta hai. Alarm trigger hota hai aur Samrat ko team jaldi move karni padti hai.",
                -4, 0, -3);
            objectives.Add(decision);
            labels.Add("Coordinated police entry ya risky solo route mein decision lein");

            GameObject gate = new GameObject("Mission Police Controlled Gate");
            gate.transform.SetParent(root.transform);
            gate.transform.position = new Vector3(75f, 0f, 194f);
            CreatePrimitiveChild("Gate Left Panel", PrimitiveType.Cube, gate.transform,
                new Vector3(-2.0f, 2.0f, 0f), new Vector3(3.8f, 4.0f, 0.28f), teal);
            CreatePrimitiveChild("Gate Right Panel", PrimitiveType.Cube, gate.transform,
                new Vector3(2.0f, 2.0f, 0f), new Vector3(3.8f, 4.0f, 0.28f), teal);
            CreatePrimitiveChild("Gate Lock", PrimitiveType.Cube, gate.transform,
                new Vector3(0f, 1.4f, -0.24f), new Vector3(0.85f, 1.1f, 0.34f), darkStone);
            CreatePrimitiveChild("Police Key", PrimitiveType.Cube, gate.transform,
                new Vector3(0f, 1.6f, -0.46f), new Vector3(0.20f, 0.50f, 0.08f), yellow);
            objectives.Add(AddObjective(
                gate, "open-gate", "Police gate unlock karein", "Constable Samrat",
                "Lock khul gaya. Shield team pehle, Azad mere peeche. Kisi ko hero banne ki zarurat nahi.",
                1, 0, 1, true));
            labels.Add("Samrat ke saath police-controlled gate safely unlock karein");

            GameObject radio = new GameObject("Mission Patrol Signal Radio");
            radio.transform.SetParent(root.transform);
            radio.transform.position = new Vector3(69f, 0.95f, 190f);
            CreatePrimitiveChild("Radio", PrimitiveType.Cube, radio.transform,
                Vector3.zero, new Vector3(0.72f, 0.78f, 0.34f), policeKhaki);
            CreatePrimitiveChild("Transmit Key", PrimitiveType.Cube, radio.transform,
                new Vector3(0f, 0.12f, -0.21f), new Vector3(0.28f, 0.22f, 0.06f), yellow);
            objectives.Add(AddObjective(
                radio, "patrol-signal", "Entry signal bhejein", "Police Radio",
                "Control, child located zone confirmed. Medical team aur family safe point standby par rahein.",
                1, 0, 1, false));
            labels.Add("Patrol aur medical team ko entry signal bhejein");

            GameObject sandhya = ParentPerson(CreatePerson(
                "Mission 04 Sandhya", new Vector3(75f, 0f, 202f),
                sandhyaDress, darkStone, skin, hair, false), root.transform);
            AddPigtails(sandhya.transform, hair);
            sandhya.transform.localScale = Vector3.one * 0.72f;
            objectives.Add(AddObjective(
                sandhya, "reach-sandhya", "Sandhya tak pahunchein", "Sandhya",
                "Papa... mujhe pata tha aap aaoge. Samrat uncle bhi aaye hain na?",
                3, 0, 2, false));
            labels.Add("Police team ke saath godown mein Sandhya tak pahunchein");

            GameObject firstAid = new GameObject("Mission Child First Aid Kit");
            firstAid.transform.SetParent(root.transform);
            firstAid.transform.position = new Vector3(78f, 0.65f, 201f);
            CreatePrimitiveChild("Medical Case", PrimitiveType.Cube, firstAid.transform,
                Vector3.zero, new Vector3(1.15f, 0.68f, 0.72f), white);
            CreatePrimitiveChild("Medical Mark Horizontal", PrimitiveType.Cube, firstAid.transform,
                new Vector3(0f, 0f, -0.39f), new Vector3(0.48f, 0.15f, 0.05f), alertRed);
            CreatePrimitiveChild("Medical Mark Vertical", PrimitiveType.Cube, firstAid.transform,
                new Vector3(0f, 0f, -0.40f), new Vector3(0.15f, 0.48f, 0.05f), alertRed);
            objectives.Add(AddObjective(
                firstAid, "first-aid", "First aid aur paani dein", "Azad",
                "Sandhya hosh mein hai. Paani dheere, blanket pehle. Medical team bahar poora check karegi.",
                1, -100, 1, false));
            labels.Add("Sandhya ko basic first aid aur paani dein");

            GameObject ledger = new GameObject("Mission Fictional Payment Ledger");
            ledger.transform.SetParent(root.transform);
            ledger.transform.position = new Vector3(83f, 0.82f, 202f);
            CreatePrimitiveChild("Ledger", PrimitiveType.Cube, ledger.transform,
                Vector3.zero, new Vector3(0.78f, 0.14f, 1.0f), yellow);
            CreatePrimitiveChild("Evidence Bag", PrimitiveType.Cube, ledger.transform,
                new Vector3(0f, 0.18f, 0f), new Vector3(1.1f, 0.05f, 1.35f), white);
            objectives.Add(AddObjective(
                ledger, "secure-ledger", "Payment ledger secure karein", "Constable Samrat",
                "Is fictional register ko haath mat lagao; evidence bag mein seal hoga. Ismein aur extortion cases ki entries ho sakti hain.",
                1, 0, 1, true));
            labels.Add("Samrat ke liye fictional ledger evidence secure karein");

            GameObject handover = new GameObject("Mission Evidence Handover Point");
            handover.transform.SetParent(root.transform);
            handover.transform.position = new Vector3(72f, 0.8f, 179f);
            CreatePrimitiveChild("Evidence Crate", PrimitiveType.Cube, handover.transform,
                Vector3.zero, new Vector3(1.4f, 0.75f, 1.0f), policeKhaki);
            objectives.Add(AddObjective(
                handover, "evidence-handover", "Evidence handover complete karein", "Constable Samrat",
                "Van, call recording, watch signal aur ledger chain mein hain. Ab case facts par chalega, gusse par nahi.",
                1, 0, 2, false));
            labels.Add("Police evidence handover record complete karein");

            GameObject shanti = ParentPerson(CreatePerson(
                "Mission 04 Shanti", new Vector3(56f, 0f, 176f),
                shantiDress, darkStone, skin, hair, false), root.transform);
            AddScarf(shanti.transform, shantiDress);
            objectives.Add(AddObjective(
                shanti, "family-reunion", "Family safe point par Shanti se milen", "Shanti",
                "Sandhya ghar aa gayi. Aaj Samrat aur mohalla saath tha; kal humein aisa system banana hai jahan kisi maa ko akela na padna pade.",
                2, 0, 2, false));
            labels.Add("Family safe point par Shanti se milen");

            mission.Configure(
                "Operation Umeed: Riverside Perimeter",
                objectives,
                labels,
                "CHAPTER 4 COMPLETE",
                "Sandhya safe hai. Evidence police custody mein hai aur family saath hai.");
            mission.ConfigureMilestones(
                new List<int> { 3, 6, 8 },
                new List<string> { "LOCATION CONFIRMED", "ENTRY READY", "SANDHYA SAFE" },
                new List<string>
                {
                    "Van aur watch signal match karte hain. Ab rescue approach decide karna hai.",
                    "Gate aur patrol signal ready hain. Samrat ki team ke saath Sandhya tak pahuncho.",
                    "Sandhya mil gayi hai. First aid ke baad evidence secure karna hoga."
                });
            mission.ConfigureChapter(4, "Chapter05");
            mission.ConfigureIntro(
                "CHAPTER 4 / OPERATION UMEED",
                "Riverside godown. Sandhya ki safety pehle; gussa aur jaldbazi baad mein.");
            return root;
        }

        private static void CreateMissionPointLight(string name, Vector3 position, Transform parent)
        {
            CreateMissionPointLight(
                name, position, parent, new Color(1f, 0.62f, 0.28f), 2.1f, 18f);
        }

        private static void CreateMissionPointLight(
            string name,
            Vector3 position,
            Transform parent,
            Color color,
            float intensity,
            float range)
        {
            GameObject lightObject = new GameObject(name);
            lightObject.transform.SetParent(parent);
            lightObject.transform.position = position;
            Light light = lightObject.AddComponent<Light>();
            light.type = LightType.Point;
            light.color = color;
            light.intensity = intensity;
            light.range = range;
            light.shadows = LightShadows.None;
        }

        private static void CreateOpenWorldGround(
            Transform root, Material grass, Material sand, Material stone, Material road,
            Material roadLine, Material darkStone, Material water, Material crosswalk)
        {
            CreateBox("City Ground", new Vector3(-48f, -0.42f, 0f), new Vector3(364f, 0.84f, 460f), grass, root);
            CreateBox("Central East West Road", new Vector3(-42f, 0.02f, 0f), new Vector3(374f, 0.10f, 24f), road, root);
            CreateBox("Central North South Road", new Vector3(0f, 0.025f, -40f), new Vector3(24f, 0.11f, 374f), road, root);
            CreateBox("University Civic Road", new Vector3(-32f, 0.03f, 101f), new Vector3(354f, 0.11f, 19f), road, root);
            CreateBox("Allahpur Ring Road", new Vector3(-30f, 0.03f, -125f), new Vector3(340f, 0.11f, 19f), road, root);
            CreateBox("Allahpur Connector", new Vector3(-111f, 0.035f, -48f), new Vector3(19f, 0.12f, 320f), road, root);
            CreateBox("Commercial Connector", new Vector3(79f, 0.035f, -40f), new Vector3(19f, 0.12f, 370f), road, root);
            CreateBox("Ghat Road", new Vector3(126f, 0.04f, -20f), new Vector3(15f, 0.12f, 380f), road, root);

            for (int index = -8; index <= 8; index++)
            {
                CreateBox($"Central Road Dash {index + 9}", new Vector3(index * 20f, 0.10f, 0f), new Vector3(8f, 0.025f, 0.24f), roadLine, root);
                CreateBox($"Vertical Road Dash {index + 9}", new Vector3(0f, 0.10f, index * 20f), new Vector3(0.24f, 0.025f, 8f), roadLine, root);
            }

            CreateBox("Ganga River", new Vector3(188f, -2.45f, 0f), new Vector3(86f, 0.35f, 460f), water, root);
            CreateBox("Far River Bank", new Vector3(235f, -1.6f, 0f), new Vector3(12f, 2.2f, 460f), sand, root);
            CreateBox("West Boundary", new Vector3(-230f, 0.65f, 0f), new Vector3(1.2f, 1.3f, 460f), darkStone, root);
            CreateBox("North Boundary", new Vector3(0f, 0.65f, 230f), new Vector3(460f, 1.3f, 1.2f), darkStone, root);
            CreateBox("South Boundary", new Vector3(0f, 0.65f, -230f), new Vector3(460f, 1.3f, 1.2f), darkStone, root);
            CreateOpenWorldRoadDetails(root, stone, roadLine, crosswalk, darkStone);
        }

        private static void CreateOpenWorldRoadDetails(
            Transform root, Material pavement, Material marking, Material crosswalk, Material darkStone)
        {
            CreatePrimitiveChild("Central North Footpath", PrimitiveType.Cube, root,
                new Vector3(-42f, 0.09f, 13.4f), new Vector3(374f, 0.14f, 2.8f), pavement);
            CreatePrimitiveChild("Central South Footpath", PrimitiveType.Cube, root,
                new Vector3(-42f, 0.09f, -13.4f), new Vector3(374f, 0.14f, 2.8f), pavement);
            CreatePrimitiveChild("Central West Footpath", PrimitiveType.Cube, root,
                new Vector3(-13.4f, 0.09f, -40f), new Vector3(2.8f, 0.14f, 374f), pavement);
            CreatePrimitiveChild("Central East Footpath", PrimitiveType.Cube, root,
                new Vector3(13.4f, 0.09f, -40f), new Vector3(2.8f, 0.14f, 374f), pavement);

            CreatePrimitiveChild("University North Footpath", PrimitiveType.Cube, root,
                new Vector3(-32f, 0.085f, 111.6f), new Vector3(354f, 0.13f, 2.4f), pavement);
            CreatePrimitiveChild("University South Footpath", PrimitiveType.Cube, root,
                new Vector3(-32f, 0.085f, 90.4f), new Vector3(354f, 0.13f, 2.4f), pavement);
            CreatePrimitiveChild("Allahpur North Footpath", PrimitiveType.Cube, root,
                new Vector3(-30f, 0.085f, -114.4f), new Vector3(340f, 0.13f, 2.4f), pavement);
            CreatePrimitiveChild("Allahpur South Footpath", PrimitiveType.Cube, root,
                new Vector3(-30f, 0.085f, -135.6f), new Vector3(340f, 0.13f, 2.4f), pavement);
            CreatePrimitiveChild("Allahpur Connector West Footpath", PrimitiveType.Cube, root,
                new Vector3(-121.6f, 0.085f, -48f), new Vector3(2.4f, 0.13f, 320f), pavement);
            CreatePrimitiveChild("Allahpur Connector East Footpath", PrimitiveType.Cube, root,
                new Vector3(-100.4f, 0.085f, -48f), new Vector3(2.4f, 0.13f, 320f), pavement);
            CreatePrimitiveChild("Commercial Connector West Footpath", PrimitiveType.Cube, root,
                new Vector3(68.4f, 0.085f, -40f), new Vector3(2.4f, 0.13f, 370f), pavement);
            CreatePrimitiveChild("Commercial Connector East Footpath", PrimitiveType.Cube, root,
                new Vector3(89.6f, 0.085f, -40f), new Vector3(2.4f, 0.13f, 370f), pavement);
            CreatePrimitiveChild("Ghat Road West Footpath", PrimitiveType.Cube, root,
                new Vector3(117.4f, 0.085f, -20f), new Vector3(2.0f, 0.13f, 380f), pavement);

            for (int index = -8; index <= 7; index++)
            {
                float offset = index * 20f;
                CreatePrimitiveChild($"University Lane Dash {index + 9}", PrimitiveType.Cube, root,
                    new Vector3(offset - 32f, 0.105f, 101f), new Vector3(8f, 0.024f, 0.22f), marking);
                CreatePrimitiveChild($"Allahpur Lane Dash {index + 9}", PrimitiveType.Cube, root,
                    new Vector3(offset - 30f, 0.105f, -125f), new Vector3(8f, 0.024f, 0.22f), marking);
                CreatePrimitiveChild($"Allahpur Connector Dash {index + 9}", PrimitiveType.Cube, root,
                    new Vector3(-111f, 0.105f, offset - 48f), new Vector3(0.22f, 0.024f, 8f), marking);
                CreatePrimitiveChild($"Commercial Connector Dash {index + 9}", PrimitiveType.Cube, root,
                    new Vector3(79f, 0.105f, offset - 40f), new Vector3(0.22f, 0.024f, 8f), marking);
                CreatePrimitiveChild($"Ghat Road Dash {index + 9}", PrimitiveType.Cube, root,
                    new Vector3(126f, 0.105f, offset - 20f), new Vector3(0.22f, 0.024f, 8f), marking);
            }

            CreateCrosswalk("Allahpur Junction", new Vector3(-111f, 0f, 0f), true, root, crosswalk);
            CreateCrosswalk("Central Junction", Vector3.zero, true, root, crosswalk);
            CreateCrosswalk("Commercial Junction", new Vector3(79f, 0f, 0f), true, root, crosswalk);
            CreateCrosswalk("University Junction", new Vector3(0f, 0f, 101f), false, root, crosswalk);
            CreateCrosswalk("South Junction", new Vector3(0f, 0f, -125f), false, root, crosswalk);

            for (int index = 0; index < 12; index++)
            {
                float x = -198f + index * 32f;
                CreatePrimitiveChild($"Central Safety Bollard {index + 1}", PrimitiveType.Cylinder, root,
                    new Vector3(x, 0.42f, index % 2 == 0 ? 14.2f : -14.2f),
                    new Vector3(0.13f, 0.42f, 0.13f), darkStone);
            }
        }

        private static void CreateCrosswalk(
            string name, Vector3 center, bool spansNorthSouth, Transform root, Material marking)
        {
            for (int stripe = -3; stripe <= 3; stripe++)
            {
                Vector3 position = center + (spansNorthSouth
                    ? new Vector3(stripe * 1.45f, 0.13f, 0f)
                    : new Vector3(0f, 0.13f, stripe * 1.45f));
                Vector3 size = spansNorthSouth
                    ? new Vector3(0.74f, 0.025f, 17f)
                    : new Vector3(17f, 0.025f, 0.74f);
                CreatePrimitiveChild($"{name} Stripe {stripe + 4}", PrimitiveType.Cube, root,
                    position, size, marking);
            }
        }

        private static void CreateOpenWorldDistricts(
            Transform root, Material brick, Material cream, Material teal, Material yellow,
            Material white, Material glass, Material darkStone, Material foliage, Material trunk)
        {
            CreateDetailedBuilding("Allahabad University Arts Block", new Vector3(-152f, 0f, 132f), new Vector3(62f, 14f, 28f), brick, cream, glass, root, 3);
            CreateDetailedBuilding("University Library", new Vector3(-143f, 0f, 77f), new Vector3(46f, 11f, 25f), cream, teal, glass, root, 2);
            CreateWorldLabel("University Sign", "ALLAHABAD UNIVERSITY", new Vector3(-152f, 8.8f, 117.7f), Vector3.zero, white, root, 0.030f);

            CreateDetailedBuilding("High Court Inspired Civic Hall", new Vector3(-30f, 0f, 137f), new Vector3(72f, 15f, 30f), white, darkStone, glass, root, 3);
            CreateWorldLabel("High Court District Sign", "HIGH COURT DISTRICT", new Vector3(-30f, 9.4f, 121.7f), Vector3.zero, darkStone, root, 0.028f);
            CreateDetailedBuilding("Public Records Office", new Vector3(35f, 0f, 147f), new Vector3(34f, 10f, 26f), cream, teal, glass, root, 2);

            CreateBox("Azad Park Lawn", new Vector3(-135f, -0.10f, 25f), new Vector3(68f, 0.22f, 68f), foliage, root);
            for (int index = 0; index < 18; index++)
            {
                float angle = index * Mathf.PI * 2f / 18f;
                CreateTree($"Azad Park Tree {index + 1}",
                    new Vector3(-135f + Mathf.Cos(angle) * 27f, 0f, 25f + Mathf.Sin(angle) * 27f), foliage, trunk, root);
            }
            for (int index = 0; index < 6; index++)
            {
                CreateBench($"Azad Park Bench {index + 1}", new Vector3(-156f + index * 8f, 0f, 12f), 0f, darkStone, yellow, root);
            }
            CreateWorldLabel("Azad Park Sign", "CHANDRASHEKHAR AZAD PARK", new Vector3(-135f, 3.2f, -8.6f), Vector3.zero, yellow, root, 0.025f);

            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 5; column++)
                {
                    float x = -196f + column * 22f;
                    float z = -176f + row * 27f;
                    CreateDetailedBuilding($"Allahpur Home {row + 1}-{column + 1}", new Vector3(x, 0f, z),
                        new Vector3(16f, 7f + (column % 2) * 1.5f, 18f),
                        column % 3 == 0 ? cream : column % 3 == 1 ? teal : brick,
                        yellow, glass, root, 2);
                }
            }
            CreateWorldLabel("Allahpur Sign", "ALLAHPUR", new Vector3(-149f, 4f, -111.2f), Vector3.zero, yellow, root, 0.030f);

            CreateDetailedBuilding("Sangam Mall", new Vector3(78f, 0f, 71f), new Vector3(58f, 18f, 44f), cream, teal, glass, root, 4);
            CreateWorldLabel("Sangam Mall Sign", "SANGAM MALL", new Vector3(78f, 10.5f, 48.6f), Vector3.zero, yellow, root, 0.034f);
            CreateDetailedBuilding("Commercial Office A", new Vector3(35f, 0f, 68f), new Vector3(24f, 13f, 32f), white, darkStone, glass, root, 3);
            CreateDetailedBuilding("Commercial Office B", new Vector3(112f, 0f, 72f), new Vector3(20f, 12f, 34f), brick, yellow, glass, root, 3);

            CreateDetailedBuilding("Seva Multi Speciality Hospital", new Vector3(82f, 0f, -157f), new Vector3(62f, 14f, 34f), white, teal, glass, root, 3);
            CreateWorldLabel("Hospital Sign", "SEVA HOSPITAL", new Vector3(82f, 8.7f, -174.3f), new Vector3(0f, 180f, 0f), teal, root, 0.032f);
            CreateDetailedBuilding("Helpers Hand Office", new Vector3(34f, 0f, -163f), new Vector3(28f, 9f, 27f), teal, yellow, glass, root, 2);
            CreateWorldLabel("Helpers Hand Open World Sign", "HELPERS HAND", new Vector3(34f, 5.8f, -176.7f), new Vector3(0f, 180f, 0f), yellow, root, 0.027f);
        }

        private static void CreateOpenWorldGhatsAndMarket(
            Transform root, Material stone, Material darkStone, Material water,
            Material teal, Material yellow, Material white, Material marketRed,
            Material foliage, Material trunk)
        {
            CreateBox("Daraganj Ghat Promenade", new Vector3(112f, -0.10f, -20f), new Vector3(22f, 0.24f, 370f), stone, root);
            for (int index = 0; index < 10; index++)
            {
                float stepY = -0.05f - index * 0.25f;
                float stepX = 136f + index * 3.6f;
                CreateBox($"Daraganj Ghat Step {index + 1}",
                    new Vector3(stepX, stepY, -20f),
                    new Vector3(4.0f, 0.42f, 360f), stone, root);
                CreatePrimitiveChild($"Daraganj Ghat Step Edge {index + 1}", PrimitiveType.Cube, root,
                    new Vector3(stepX + 1.82f, stepY + 0.225f, -20f),
                    new Vector3(0.16f, 0.045f, 360f), darkStone);
            }
            for (int index = 0; index < 18; index++)
            {
                CreateStreetLamp($"Ghat Promenade Lamp {index + 1}",
                    new Vector3(116f, 0f, -198f + index * 23f), darkStone, yellow, root);
            }
            for (int index = 0; index < 10; index++)
            {
                CreateBoat($"Clean Ganga Boat {index + 1}",
                    new Vector3(180f + (index % 2) * 15f, -2.05f, -176f + index * 38f),
                    index % 2 == 0 ? 8f : -12f, index % 2 == 0 ? darkStone : teal, yellow, root);
            }
            for (int index = 0; index < 16; index++)
            {
                GameObject glint = CreatePrimitiveChild($"Ganga Sun Glint {index + 1}", PrimitiveType.Cube, root,
                    new Vector3(165f + (index % 3) * 16f, -2.23f, -210f + index * 28f),
                    new Vector3(7f + (index % 4) * 1.8f, 0.025f, 0.16f), white);
                glint.transform.localRotation = Quaternion.Euler(0f, -8f + (index % 5) * 4f, 0f);
                glint.AddComponent<WorldMotion>().Configure(
                    WorldMotionKind.Float, 0.025f, 0.75f + index * 0.035f, Vector3.up);
            }
            CreateWorldLabel("Daraganj Ghat Open World Sign", "DARAGANJ GHATS", new Vector3(113f, 5.2f, -45f), new Vector3(0f, 90f, 0f), yellow, root, 0.035f);

            for (int index = 0; index < 8; index++)
            {
                float z = -172f + index * 43f;
                CreateFoodStall($"Ghat Food Stall {index + 1}", new Vector3(103f, 0f, z),
                    index % 2 == 0 ? teal : marketRed, yellow, white, darkStone, root,
                    index % 4 == 0 ? "CHAI" : index % 4 == 1 ? "KACHORI" : index % 4 == 2 ? "LASSI" : "FRUIT");
            }
            for (int index = 0; index < 6; index++)
            {
                Vector3 shadePosition = new Vector3(108.5f, 0f, -148f + index * 58f);
                GameObject shade = new GameObject($"Ghat Shade {index + 1}");
                shade.transform.SetParent(root);
                shade.transform.position = shadePosition;
                CreatePrimitiveChild("Shade Pole", PrimitiveType.Cylinder, shade.transform,
                    new Vector3(0f, 1.55f, 0f), new Vector3(0.08f, 1.55f, 0.08f), darkStone);
                CreatePrimitiveChild("Shade Canopy", PrimitiveType.Sphere, shade.transform,
                    new Vector3(0f, 3.05f, 0f), new Vector3(2.25f, 0.34f, 2.25f),
                    index % 2 == 0 ? teal : marketRed);
                CreatePrimitiveChild("Shade Table", PrimitiveType.Cylinder, shade.transform,
                    new Vector3(0f, 0.82f, 0f), new Vector3(0.85f, 0.12f, 0.85f), yellow);
                CreateBench($"Ghat Rest Bench {index + 1}", shadePosition + new Vector3(-3.2f, 0f, 0f),
                    90f, darkStone, index % 2 == 0 ? teal : marketRed, root);
            }

            CreateBox("Loknath Market Plaza", new Vector3(32f, -0.08f, -69f), new Vector3(68f, 0.18f, 70f), stone, root);
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    CreateFoodStall($"Loknath Stall {row + 1}-{column + 1}",
                        new Vector3(10f + column * 15f, 0f, -94f + row * 24f),
                        (row + column) % 2 == 0 ? marketRed : teal,
                        yellow, white, darkStone, root,
                        (row + column) % 3 == 0 ? "CHAAT" : (row + column) % 3 == 1 ? "SAMOSA" : "MITHAI");
                }
            }
            CreateWorldLabel("Loknath Market Sign", "LOKNATH MARKET", new Vector3(32f, 4.8f, -105.5f), Vector3.zero, yellow, root, 0.032f);

            for (int index = 0; index < 12; index++)
            {
                CreateTree($"Riverfront Tree {index + 1}", new Vector3(93f, 0f, -205f + index * 36f), foliage, trunk, root);
            }
        }

        private static void CreateOpenWorldStreetLife(
            Transform root, Material shirt, Material trousers, Material shantiDress,
            Material volunteerDress, Material teal, Material yellow, Material darkStone,
            Material skin, Material hair)
        {
            Vector3[] zones =
            {
                new Vector3(-145f, 0f, 98f), new Vector3(-130f, 0f, 23f),
                new Vector3(-148f, 0f, -124f), new Vector3(27f, 0f, -67f),
                new Vector3(73f, 0f, 42f), new Vector3(108f, 0f, -28f),
                new Vector3(108f, 0f, -142f), new Vector3(108f, 0f, 128f),
                new Vector3(45f, 0f, -178f)
            };
            for (int zone = 0; zone < zones.Length; zone++)
            {
                for (int index = 0; index < 4; index++)
                {
                    Vector3 start = zones[zone] + new Vector3(-7f + index * 4.5f, 0f, -4f + index % 2 * 4f);
                    GameObject citizen = CreatePerson($"Free Roam Citizen {zone + 1}-{index + 1}", start,
                        index % 3 == 0 ? shantiDress : index % 2 == 0 ? teal : volunteerDress,
                        index % 2 == 0 ? trousers : darkStone, skin, hair, false);
                    citizen.transform.SetParent(root, true);
                    citizen.transform.localScale = Vector3.one * (0.90f + (index % 2) * 0.08f);
                    CinematicActorMotion walker = citizen.AddComponent<CinematicActorMotion>();
                    walker.Configure(citizen.transform.localPosition + new Vector3(10f - index * 2f, 0f, 7f - index), index * 0.3f, 7f + index, true);
                }
            }
        }

        private static void CreateDetailedBuilding(
            string name, Vector3 position, Vector3 size, Material wall, Material accent,
            Material glass, Transform parent, int storeys)
        {
            GameObject building = new GameObject(name);
            building.transform.SetParent(parent);
            building.transform.position = position;
            CreatePrimitiveChild("Foundation Plinth", PrimitiveType.Cube, building.transform,
                new Vector3(0f, 0.18f, 0f), new Vector3(size.x + 1.2f, 0.36f, size.z + 1.2f), accent);
            CreatePrimitiveChild("Main Structure", PrimitiveType.Cube, building.transform,
                new Vector3(0f, size.y * 0.5f, 0f), size, wall, true);
            CreatePrimitiveChild("Roof Line", PrimitiveType.Cube, building.transform,
                new Vector3(0f, size.y + 0.32f, 0f), new Vector3(size.x + 0.6f, 0.64f, size.z + 0.6f), accent);
            CreatePrimitiveChild("Entrance", PrimitiveType.Cube, building.transform,
                new Vector3(0f, 1.8f, -size.z * 0.5f - 0.06f), new Vector3(Mathf.Min(4.5f, size.x * 0.22f), 3.6f, 0.14f), accent);
            CreatePrimitiveChild("Entrance Step", PrimitiveType.Cube, building.transform,
                new Vector3(0f, 0.18f, -size.z * 0.5f - 0.92f),
                new Vector3(Mathf.Min(5.8f, size.x * 0.30f), 0.34f, 1.65f), wall);
            CreatePrimitiveChild("Entrance Canopy", PrimitiveType.Cube, building.transform,
                new Vector3(0f, 3.65f, -size.z * 0.5f - 0.68f),
                new Vector3(Mathf.Min(6.4f, size.x * 0.34f), 0.22f, 1.42f), accent);
            int columns = Mathf.Clamp(Mathf.RoundToInt(size.x / 7f), 3, 9);
            int safeStoreys = Mathf.Clamp(storeys, 1, 5);
            for (int floor = 0; floor < safeStoreys; floor++)
            {
                float y = 3.0f + floor * Mathf.Max(2.5f, (size.y - 2f) / safeStoreys);
                for (int column = 0; column < columns; column++)
                {
                    float x = Mathf.Lerp(-size.x * 0.40f, size.x * 0.40f, columns == 1 ? 0.5f : column / (float)(columns - 1));
                    CreatePrimitiveChild($"Front Window {floor + 1}-{column + 1}", PrimitiveType.Cube, building.transform,
                        new Vector3(x, Mathf.Min(y, size.y - 1f), -size.z * 0.5f - 0.08f),
                        new Vector3(Mathf.Max(1.5f, size.x / columns * 0.52f), 1.35f, 0.10f), glass);
                }

                int sideWindows = Mathf.Clamp(Mathf.RoundToInt(size.z / 10f), 2, 4);
                for (int sideWindow = 0; sideWindow < sideWindows; sideWindow++)
                {
                    float z = Mathf.Lerp(-size.z * 0.34f, size.z * 0.34f,
                        sideWindows == 1 ? 0.5f : sideWindow / (float)(sideWindows - 1));
                    Vector3 sideSize = new Vector3(0.10f, 1.28f, Mathf.Max(1.4f, size.z / sideWindows * 0.42f));
                    CreatePrimitiveChild($"Left Window {floor + 1}-{sideWindow + 1}", PrimitiveType.Cube,
                        building.transform, new Vector3(-size.x * 0.5f - 0.08f, Mathf.Min(y, size.y - 1f), z), sideSize, glass);
                    CreatePrimitiveChild($"Right Window {floor + 1}-{sideWindow + 1}", PrimitiveType.Cube,
                        building.transform, new Vector3(size.x * 0.5f + 0.08f, Mathf.Min(y, size.y - 1f), z), sideSize, glass);
                }

                if (floor > 0 && floor % 2 == 1)
                {
                    float balconyWidth = Mathf.Min(12f, size.x * 0.56f);
                    CreatePrimitiveChild($"Balcony Slab {floor + 1}", PrimitiveType.Cube, building.transform,
                        new Vector3(0f, Mathf.Min(y - 0.95f, size.y - 1.4f), -size.z * 0.5f - 0.58f),
                        new Vector3(balconyWidth, 0.16f, 1.25f), accent);
                    CreatePrimitiveChild($"Balcony Rail {floor + 1}", PrimitiveType.Cube, building.transform,
                        new Vector3(0f, Mathf.Min(y - 0.46f, size.y - 0.9f), -size.z * 0.5f - 1.14f),
                        new Vector3(balconyWidth, 0.72f, 0.10f), accent);
                }
            }

            for (int floorLine = 1; floorLine < safeStoreys; floorLine++)
            {
                float y = size.y * floorLine / safeStoreys;
                CreatePrimitiveChild($"Facade Floor Band {floorLine}", PrimitiveType.Cube, building.transform,
                    new Vector3(0f, y, 0f), new Vector3(size.x + 0.32f, 0.15f, size.z + 0.32f), accent);
            }

            float parapetY = size.y + 0.88f;
            CreatePrimitiveChild("Roof Parapet Front", PrimitiveType.Cube, building.transform,
                new Vector3(0f, parapetY, -size.z * 0.5f), new Vector3(size.x + 0.4f, 0.92f, 0.28f), accent);
            CreatePrimitiveChild("Roof Parapet Rear", PrimitiveType.Cube, building.transform,
                new Vector3(0f, parapetY, size.z * 0.5f), new Vector3(size.x + 0.4f, 0.92f, 0.28f), accent);
            CreatePrimitiveChild("Roof Parapet Left", PrimitiveType.Cube, building.transform,
                new Vector3(-size.x * 0.5f, parapetY, 0f), new Vector3(0.28f, 0.92f, size.z), accent);
            CreatePrimitiveChild("Roof Parapet Right", PrimitiveType.Cube, building.transform,
                new Vector3(size.x * 0.5f, parapetY, 0f), new Vector3(0.28f, 0.92f, size.z), accent);

            if (size.x <= 20f)
            {
                CreatePrimitiveChild("Rooftop Water Tank", PrimitiveType.Cylinder, building.transform,
                    new Vector3(size.x * 0.24f, size.y + 2.0f, size.z * 0.14f),
                    new Vector3(1.15f, 0.95f, 1.15f), accent);
                CreatePrimitiveChild("Water Tank Lid", PrimitiveType.Cylinder, building.transform,
                    new Vector3(size.x * 0.24f, size.y + 3.0f, size.z * 0.14f),
                    new Vector3(1.22f, 0.08f, 1.22f), wall);
            }
            else
            {
                for (int panel = 0; panel < 2; panel++)
                {
                    GameObject solarPanel = CreatePrimitiveChild($"Rooftop Solar Panel {panel + 1}", PrimitiveType.Cube,
                        building.transform, new Vector3(-3.2f + panel * 6.4f, size.y + 1.46f, 1.4f),
                        new Vector3(5.2f, 0.12f, 2.6f), glass);
                    solarPanel.transform.localRotation = Quaternion.Euler(12f, 0f, 0f);
                }
            }

            StaticEditorFlags flags = StaticEditorFlags.BatchingStatic
                | StaticEditorFlags.OccluderStatic
                | StaticEditorFlags.OccludeeStatic;
            foreach (Transform item in building.GetComponentsInChildren<Transform>(true))
            {
                GameObjectUtility.SetStaticEditorFlags(item.gameObject, flags);
            }
        }

        private static void CreateFoodStall(
            string name, Vector3 position, Material canopy, Material trim, Material counter,
            Material frame, Transform parent, string label)
        {
            GameObject stall = new GameObject(name);
            stall.transform.SetParent(parent);
            stall.transform.position = position;
            CreatePrimitiveChild("Counter", PrimitiveType.Cube, stall.transform, new Vector3(0f, 0.8f, 0f), new Vector3(4.8f, 1.6f, 2.4f), counter, true);
            CreatePrimitiveChild("Counter Front", PrimitiveType.Cube, stall.transform,
                new Vector3(0f, 0.82f, -1.23f), new Vector3(4.15f, 1.12f, 0.10f), canopy);
            CreatePrimitiveChild("Display Shelf", PrimitiveType.Cube, stall.transform,
                new Vector3(0f, 1.66f, -0.30f), new Vector3(4.25f, 0.12f, 1.05f), frame);
            CreatePrimitiveChild("Canopy", PrimitiveType.Cube, stall.transform, new Vector3(0f, 2.8f, 0f), new Vector3(5.4f, 0.28f, 3.2f), canopy);
            CreatePrimitiveChild("Post Left", PrimitiveType.Cube, stall.transform, new Vector3(-2.2f, 1.6f, 1.1f), new Vector3(0.16f, 2.8f, 0.16f), frame);
            CreatePrimitiveChild("Post Right", PrimitiveType.Cube, stall.transform, new Vector3(2.2f, 1.6f, 1.1f), new Vector3(0.16f, 2.8f, 0.16f), frame);
            for (int item = 0; item < 4; item++)
            {
                float x = -1.45f + item * 0.96f;
                CreatePrimitiveChild($"Serving Plate {item + 1}", PrimitiveType.Cylinder, stall.transform,
                    new Vector3(x, 1.78f, -0.32f), new Vector3(0.38f, 0.045f, 0.38f), counter);
                CreatePrimitiveChild($"Food Display {item + 1}", PrimitiveType.Sphere, stall.transform,
                    new Vector3(x, 1.91f, -0.32f), new Vector3(0.27f, 0.16f, 0.27f),
                    item % 2 == 0 ? trim : canopy);
            }
            CreatePrimitiveChild("Side Supply Crate", PrimitiveType.Cube, stall.transform,
                new Vector3(2.75f, 0.42f, 0.65f), new Vector3(0.72f, 0.82f, 0.72f), frame);
            CreateWorldLabel(name + " Sign", label, position + new Vector3(0f, 2.85f, -1.65f), Vector3.zero, trim, parent, 0.021f);
        }

        private static void CreateDrivableCar(
            string name, Vector3 position, Quaternion rotation, Material bodyMaterial,
            Material glass, Material tyre, Material dark, Material light,
            Material shirt, Material trousers, Material skin, Material hair, Transform parent)
        {
            GameObject car = new GameObject(name);
            car.transform.SetParent(parent);
            car.transform.SetPositionAndRotation(position, rotation);
            Rigidbody body = car.AddComponent<Rigidbody>();
            body.mass = 1050f;
            body.linearDamping = 0.08f;
            body.angularDamping = 0.6f;
            body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            BoxCollider bodyCollider = car.AddComponent<BoxCollider>();
            bodyCollider.center = new Vector3(0f, 0.82f, 0f);
            bodyCollider.size = new Vector3(1.9f, 1.25f, 4.1f);
            CreatePrimitiveChild("Car Lower Body", PrimitiveType.Cube, car.transform, new Vector3(0f, 0.72f, 0f), new Vector3(1.9f, 0.62f, 4.2f), bodyMaterial);
            CreatePrimitiveChild("Car Cabin", PrimitiveType.Cube, car.transform, new Vector3(0f, 1.30f, 0.25f), new Vector3(1.65f, 0.68f, 2.25f), bodyMaterial);
            CreatePrimitiveChild("Front Windscreen", PrimitiveType.Cube, car.transform, new Vector3(0f, 1.35f, 1.40f), new Vector3(1.50f, 0.52f, 0.10f), glass).transform.localRotation = Quaternion.Euler(-18f, 0f, 0f);
            CreatePrimitiveChild("Rear Windscreen", PrimitiveType.Cube, car.transform, new Vector3(0f, 1.35f, -0.90f), new Vector3(1.50f, 0.52f, 0.10f), glass).transform.localRotation = Quaternion.Euler(18f, 0f, 0f);
            CreatePrimitiveChild("Roof", PrimitiveType.Cube, car.transform, new Vector3(0f, 1.68f, 0.24f), new Vector3(1.60f, 0.13f, 2.12f), bodyMaterial);
            CreatePrimitiveChild("Side Window Left", PrimitiveType.Cube, car.transform, new Vector3(-0.84f, 1.36f, 0.25f), new Vector3(0.07f, 0.48f, 1.55f), glass);
            CreatePrimitiveChild("Side Window Right", PrimitiveType.Cube, car.transform, new Vector3(0.84f, 1.36f, 0.25f), new Vector3(0.07f, 0.48f, 1.55f), glass);
            CreatePrimitiveChild("Front Bumper", PrimitiveType.Cube, car.transform, new Vector3(0f, 0.52f, 2.16f), new Vector3(1.78f, 0.18f, 0.18f), dark);
            CreatePrimitiveChild("Rear Bumper", PrimitiveType.Cube, car.transform, new Vector3(0f, 0.52f, -2.16f), new Vector3(1.78f, 0.18f, 0.18f), dark);
            CreatePrimitiveChild("Front Grille", PrimitiveType.Cube, car.transform, new Vector3(0f, 0.74f, 2.15f), new Vector3(0.68f, 0.22f, 0.09f), dark);
            CreatePrimitiveChild("Headlight Left", PrimitiveType.Cube, car.transform, new Vector3(-0.62f, 0.82f, 2.13f), new Vector3(0.46f, 0.22f, 0.08f), light);
            CreatePrimitiveChild("Headlight Right", PrimitiveType.Cube, car.transform, new Vector3(0.62f, 0.82f, 2.13f), new Vector3(0.46f, 0.22f, 0.08f), light);

            Vector3[] positions =
            {
                new Vector3(-0.93f, 0.40f, 1.38f), new Vector3(0.93f, 0.40f, 1.38f),
                new Vector3(-0.93f, 0.40f, -1.35f), new Vector3(0.93f, 0.40f, -1.35f)
            };
            WheelCollider[] wheels = new WheelCollider[4];
            Transform[] visuals = new Transform[4];
            for (int index = 0; index < 4; index++)
            {
                wheels[index] = CreateVehicleWheelCollider(car.transform, $"Car Wheel Collider {index + 1}", positions[index], 0.38f, 28f);
                visuals[index] = CreatePrimitiveChild($"Car Wheel Visual {index + 1}", PrimitiveType.Cylinder, car.transform,
                    positions[index], new Vector3(0.38f, 0.18f, 0.38f), tyre).transform;
                visuals[index].localRotation = Quaternion.Euler(0f, 0f, 90f);
            }
            Transform seat = CreateVehicleAnchor(car.transform, "Driver Seat", new Vector3(-0.38f, 1.15f, 0.25f));
            Transform exit = CreateVehicleAnchor(car.transform, "Driver Exit", new Vector3(-2.75f, 0.05f, -0.35f));
            Transform camera = CreateVehicleAnchor(car.transform, "Vehicle Camera Target", new Vector3(0f, 1.25f, 0f));
            GameObject rider = CreateVehicleRider("Azad Car Rider", car.transform, new Vector3(-0.38f, 0.05f, 0.25f),
                0.76f, false, shirt, trousers, skin, hair);
            FreeRoamVehicle controller = car.AddComponent<FreeRoamVehicle>();
            controller.Configure(name, seat, exit, camera, new[] { wheels[2], wheels[3] }, new[] { wheels[0], wheels[1] },
                wheels, visuals, rider, 1250f, 1800f, 27f, 78f, 8.2f);
            SetLayerRecursively(car, 2);
            foreach (WheelCollider wheel in wheels)
            {
                wheel.gameObject.layer = 0;
            }
        }

        private static void CreateDrivableScooter(
            string name, Vector3 position, Quaternion rotation, Material bodyMaterial,
            Material tyre, Material dark, Material light,
            Material shirt, Material trousers, Material skin, Material hair, Transform parent)
        {
            GameObject scooter = new GameObject(name);
            scooter.transform.SetParent(parent);
            scooter.transform.SetPositionAndRotation(position, rotation);
            Rigidbody body = scooter.AddComponent<Rigidbody>();
            body.mass = 180f;
            body.linearDamping = 0.10f;
            body.angularDamping = 0.8f;
            body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            BoxCollider bodyCollider = scooter.AddComponent<BoxCollider>();
            bodyCollider.center = new Vector3(0f, 0.85f, 0f);
            bodyCollider.size = new Vector3(0.78f, 1.35f, 2.25f);
            CreatePrimitiveChild("Scooter Floor", PrimitiveType.Cube, scooter.transform, new Vector3(0f, 0.46f, 0f), new Vector3(0.58f, 0.18f, 1.35f), bodyMaterial);
            CreatePrimitiveChild("Scooter Front", PrimitiveType.Capsule, scooter.transform, new Vector3(0f, 0.92f, 0.62f), new Vector3(0.62f, 0.62f, 0.48f), bodyMaterial);
            CreatePrimitiveChild("Scooter Seat", PrimitiveType.Cube, scooter.transform, new Vector3(0f, 0.88f, -0.42f), new Vector3(0.58f, 0.18f, 0.95f), dark);
            CreatePrimitiveChild("Scooter Handle", PrimitiveType.Cube, scooter.transform, new Vector3(0f, 1.48f, 0.65f), new Vector3(0.88f, 0.10f, 0.10f), dark);
            CreatePrimitiveChild("Scooter Headlight", PrimitiveType.Sphere, scooter.transform, new Vector3(0f, 1.26f, 0.94f), new Vector3(0.28f, 0.24f, 0.16f), light);
            CreatePrimitiveChild("Scooter Rear Light", PrimitiveType.Cube, scooter.transform, new Vector3(0f, 0.88f, -0.93f), new Vector3(0.30f, 0.18f, 0.10f), light);
            Transform mirrorLeft = CreatePrimitiveChild("Scooter Mirror Left", PrimitiveType.Sphere, scooter.transform, new Vector3(-0.47f, 1.72f, 0.68f), new Vector3(0.18f, 0.13f, 0.08f), dark).transform;
            Transform mirrorRight = CreatePrimitiveChild("Scooter Mirror Right", PrimitiveType.Sphere, scooter.transform, new Vector3(0.47f, 1.72f, 0.68f), new Vector3(0.18f, 0.13f, 0.08f), dark).transform;
            CreatePrimitiveChild("Scooter Mirror Stem Left", PrimitiveType.Cylinder, scooter.transform, new Vector3(-0.34f, 1.60f, 0.67f), new Vector3(0.025f, 0.18f, 0.025f), dark).transform.localRotation = Quaternion.Euler(0f, 0f, -38f);
            CreatePrimitiveChild("Scooter Mirror Stem Right", PrimitiveType.Cylinder, scooter.transform, new Vector3(0.34f, 1.60f, 0.67f), new Vector3(0.025f, 0.18f, 0.025f), dark).transform.localRotation = Quaternion.Euler(0f, 0f, 38f);

            Vector3 frontPosition = new Vector3(0f, 0.36f, 0.92f);
            Vector3 rearPosition = new Vector3(0f, 0.36f, -0.92f);
            WheelCollider front = CreateVehicleWheelCollider(scooter.transform, "Scooter Front Wheel Collider", frontPosition, 0.34f, 14f);
            WheelCollider rear = CreateVehicleWheelCollider(scooter.transform, "Scooter Rear Wheel Collider", rearPosition, 0.34f, 16f);
            Transform frontVisual = CreatePrimitiveChild("Scooter Front Wheel Visual", PrimitiveType.Cylinder, scooter.transform,
                frontPosition, new Vector3(0.34f, 0.12f, 0.34f), tyre).transform;
            Transform rearVisual = CreatePrimitiveChild("Scooter Rear Wheel Visual", PrimitiveType.Cylinder, scooter.transform,
                rearPosition, new Vector3(0.34f, 0.12f, 0.34f), tyre).transform;
            frontVisual.localRotation = Quaternion.Euler(0f, 0f, 90f);
            rearVisual.localRotation = Quaternion.Euler(0f, 0f, 90f);
            Transform seat = CreateVehicleAnchor(scooter.transform, "Scooter Rider Seat", new Vector3(0f, 1.18f, -0.25f));
            Transform exit = CreateVehicleAnchor(scooter.transform, "Scooter Exit", new Vector3(-1.85f, 0.05f, -0.35f));
            Transform camera = CreateVehicleAnchor(scooter.transform, "Scooter Camera Target", new Vector3(0f, 1.15f, 0f));
            GameObject rider = CreateVehicleRider("Azad Scooter Rider", scooter.transform, new Vector3(0f, 0.30f, -0.38f),
                0.80f, true, shirt, trousers, skin, hair);
            FreeRoamVehicle controller = scooter.AddComponent<FreeRoamVehicle>();
            controller.Configure(name, seat, exit, camera, new[] { rear }, new[] { front }, new[] { front, rear },
                new[] { frontVisual, rearVisual }, rider, 330f, 620f, 24f, 54f, 6.8f);
            SetLayerRecursively(scooter, 2);
            front.gameObject.layer = 0;
            rear.gameObject.layer = 0;
        }

        private static GameObject CreateVehicleRider(
            string name, Transform parent, Vector3 localPosition, float scale, bool scooterPose,
            Material shirt, Material trousers, Material skin, Material hair)
        {
            GameObject rider = CreatePerson(name, Vector3.zero, shirt, trousers, skin, hair, true);
            rider.transform.SetParent(parent, false);
            rider.transform.localPosition = localPosition;
            rider.transform.localRotation = Quaternion.identity;
            rider.transform.localScale = Vector3.one * scale;
            SetChildActive(rider.transform, "Shoulder Bag", false);
            SetChildActive(rider.transform, "Shoulder Bag Strap", false);

            if (scooterPose)
            {
                PoseRiderLimb(rider.transform, "Leg Left", new Vector3(-0.18f, 0.72f, 0.02f), new Vector3(78f, 0f, 0f));
                PoseRiderLimb(rider.transform, "Leg Right", new Vector3(0.18f, 0.72f, 0.02f), new Vector3(78f, 0f, 0f));
                PoseRiderLimb(rider.transform, "Arm Left", new Vector3(-0.37f, 1.13f, 0.75f), new Vector3(72f, 0f, -8f));
                PoseRiderLimb(rider.transform, "Arm Right", new Vector3(0.37f, 1.13f, 0.75f), new Vector3(72f, 0f, 8f));
                PoseRiderLimb(rider.transform, "Hand Left", new Vector3(-0.38f, 1.45f, 1.25f), Vector3.zero);
                PoseRiderLimb(rider.transform, "Hand Right", new Vector3(0.38f, 1.45f, 1.25f), Vector3.zero);
                PoseRiderLimb(rider.transform, "Shoe Left", new Vector3(-0.18f, 0.32f, 0.72f), Vector3.zero);
                PoseRiderLimb(rider.transform, "Shoe Right", new Vector3(0.18f, 0.32f, 0.72f), Vector3.zero);
            }

            SetLayerRecursively(rider, 2);
            rider.SetActive(false);
            return rider;
        }

        private static void PoseRiderLimb(Transform rider, string name, Vector3 position, Vector3 rotation)
        {
            Transform limb = rider.Find(name);
            if (limb == null)
            {
                return;
            }
            limb.localPosition = position;
            limb.localRotation = Quaternion.Euler(rotation);
        }

        private static void SetChildActive(Transform parent, string name, bool value)
        {
            Transform child = parent.Find(name);
            if (child != null)
            {
                child.gameObject.SetActive(value);
            }
        }

        private static WheelCollider CreateVehicleWheelCollider(
            Transform parent, string name, Vector3 localPosition, float radius, float mass)
        {
            GameObject wheelObject = new GameObject(name);
            wheelObject.transform.SetParent(parent);
            wheelObject.transform.localPosition = localPosition;
            WheelCollider wheel = wheelObject.AddComponent<WheelCollider>();
            wheel.radius = radius;
            wheel.mass = mass;
            wheel.suspensionDistance = 0.16f;
            JointSpring spring = wheel.suspensionSpring;
            spring.spring = 28000f;
            spring.damper = 4200f;
            spring.targetPosition = 0.52f;
            wheel.suspensionSpring = spring;
            WheelFrictionCurve forwardFriction = wheel.forwardFriction;
            forwardFriction.extremumSlip = 0.36f;
            forwardFriction.extremumValue = 1.0f;
            forwardFriction.asymptoteSlip = 0.78f;
            forwardFriction.asymptoteValue = 0.58f;
            forwardFriction.stiffness = 1.35f;
            wheel.forwardFriction = forwardFriction;
            WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
            sidewaysFriction.extremumSlip = 0.25f;
            sidewaysFriction.extremumValue = 1.0f;
            sidewaysFriction.asymptoteSlip = 0.52f;
            sidewaysFriction.asymptoteValue = 0.72f;
            sidewaysFriction.stiffness = 1.65f;
            wheel.sidewaysFriction = sidewaysFriction;
            return wheel;
        }

        private static Transform CreateVehicleAnchor(Transform parent, string name, Vector3 localPosition)
        {
            GameObject anchor = new GameObject(name);
            anchor.transform.SetParent(parent);
            anchor.transform.localPosition = localPosition;
            anchor.transform.localRotation = Quaternion.identity;
            return anchor.transform;
        }

        private static void CreateParkedTraffic(
            Transform root, Material car, Material scooter, Material glass,
            Material tyre, Material dark, Material cream)
        {
            Vector3[] carPositions =
            {
                new Vector3(-72f, 0.35f, 8f), new Vector3(42f, 0.35f, 109f),
                new Vector3(88f, 0.35f, -113f), new Vector3(-118f, 0.35f, -91f)
            };
            for (int index = 0; index < carPositions.Length; index++)
            {
                GameObject parked = new GameObject($"Parked City Car {index + 1}");
                parked.transform.SetParent(root);
                parked.transform.position = carPositions[index];
                parked.transform.rotation = Quaternion.Euler(0f, index % 2 == 0 ? 90f : 0f, 0f);
                CreatePrimitiveChild("Body", PrimitiveType.Cube, parked.transform, new Vector3(0f, 0.45f, 0f), new Vector3(1.8f, 0.55f, 3.8f), index % 2 == 0 ? car : cream, true);
                CreatePrimitiveChild("Cabin", PrimitiveType.Cube, parked.transform, new Vector3(0f, 0.95f, 0.15f), new Vector3(1.5f, 0.55f, 1.9f), glass);
                for (int wheel = 0; wheel < 4; wheel++)
                {
                    CreatePrimitiveChild($"Wheel {wheel + 1}", PrimitiveType.Cylinder, parked.transform,
                        new Vector3(wheel < 2 ? -0.9f : 0.9f, 0.18f, wheel % 2 == 0 ? -1.2f : 1.2f),
                        new Vector3(0.30f, 0.14f, 0.30f), tyre).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
                }
            }
            for (int index = 0; index < 5; index++)
            {
                GameObject bike = new GameObject($"Parked Bike {index + 1}");
                bike.transform.SetParent(root);
                bike.transform.position = new Vector3(92f + index * 2f, 0f, 28f);
                CreatePrimitiveChild("Bike Frame", PrimitiveType.Cube, bike.transform, new Vector3(0f, 0.55f, 0f), new Vector3(0.25f, 0.25f, 1.3f), scooter);
                CreatePrimitiveChild("Bike Seat", PrimitiveType.Cube, bike.transform, new Vector3(0f, 0.86f, -0.3f), new Vector3(0.38f, 0.12f, 0.55f), dark);
                CreatePrimitiveChild("Bike Wheel Front", PrimitiveType.Cylinder, bike.transform, new Vector3(0f, 0.36f, 0.75f), new Vector3(0.32f, 0.10f, 0.32f), tyre).transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
                CreatePrimitiveChild("Bike Wheel Rear", PrimitiveType.Cylinder, bike.transform, new Vector3(0f, 0.36f, -0.75f), new Vector3(0.32f, 0.10f, 0.32f), tyre).transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            }
        }

        private static void CreateAmbientTraffic(
            Transform root, Material red, Material teal, Material yellow, Material cream,
            Material glass, Material tyre, Material dark)
        {
            CreateAmbientAuto("Prayagraj Auto 1", new Vector3(-208f, 0f, -4.2f),
                new Vector3(135f, 0f, -4.2f), 0f, 32f, yellow, dark, teal, glass, tyre, root);
            CreateAmbientAuto("Prayagraj Auto 2", new Vector3(135f, 0f, 4.2f),
                new Vector3(-208f, 0f, 4.2f), 2.4f, 36f, teal, dark, yellow, glass, tyre, root);
            CreateAmbientAuto("Prayagraj Auto 3", new Vector3(-4.2f, 0f, -208f),
                new Vector3(-4.2f, 0f, 164f), 1.2f, 35f, yellow, dark, red, glass, tyre, root);
            CreateAmbientAuto("Prayagraj Auto 4", new Vector3(75f, 0f, 164f),
                new Vector3(75f, 0f, -208f), 4.1f, 39f, red, dark, cream, glass, tyre, root);
            CreateAmbientBus("Prayagraj City Bus", new Vector3(-202f, 0f, 97f),
                new Vector3(132f, 0f, 97f), 5.5f, 44f, cream, red, glass, tyre, dark, root);
            CreateAmbientBus("Allahpur Seva Bus", new Vector3(132f, 0f, -129f),
                new Vector3(-205f, 0f, -129f), 9f, 48f, teal, yellow, glass, tyre, dark, root);
        }

        private static void CreateAmbientAuto(
            string name, Vector3 start, Vector3 destination, float delay, float duration,
            Material body, Material canopy, Material accent, Material glass, Material tyre, Transform root)
        {
            GameObject auto = new GameObject(name);
            auto.transform.SetParent(root);
            auto.transform.localPosition = start;
            CreatePrimitiveChild("Auto Lower Body", PrimitiveType.Cube, auto.transform,
                new Vector3(0f, 0.58f, 0f), new Vector3(1.35f, 0.55f, 2.15f), body);
            CreatePrimitiveChild("Auto Cabin", PrimitiveType.Cube, auto.transform,
                new Vector3(0f, 1.25f, -0.18f), new Vector3(1.28f, 1.10f, 1.55f), canopy);
            CreatePrimitiveChild("Auto Roof", PrimitiveType.Cube, auto.transform,
                new Vector3(0f, 1.90f, -0.18f), new Vector3(1.52f, 0.16f, 1.82f), accent);
            CreatePrimitiveChild("Auto Windscreen", PrimitiveType.Cube, auto.transform,
                new Vector3(0f, 1.38f, 0.62f), new Vector3(1.08f, 0.58f, 0.08f), glass);
            CreatePrimitiveChild("Auto Headlight", PrimitiveType.Sphere, auto.transform,
                new Vector3(0f, 0.72f, 1.10f), new Vector3(0.24f, 0.20f, 0.12f), accent);
            Vector3[] wheels =
            {
                new Vector3(0f, 0.35f, 0.82f),
                new Vector3(-0.68f, 0.35f, -0.72f),
                new Vector3(0.68f, 0.35f, -0.72f)
            };
            for (int index = 0; index < wheels.Length; index++)
            {
                Transform wheel = CreatePrimitiveChild($"Auto Wheel {index + 1}", PrimitiveType.Cylinder,
                    auto.transform, wheels[index], new Vector3(0.30f, 0.12f, 0.30f), tyre).transform;
                wheel.localRotation = Quaternion.Euler(0f, 0f, 90f);
            }
            auto.AddComponent<CinematicActorMotion>().Configure(destination, delay, duration, true);
        }

        private static void CreateAmbientBus(
            string name, Vector3 start, Vector3 destination, float delay, float duration,
            Material body, Material accent, Material glass, Material tyre, Material dark, Transform root)
        {
            GameObject bus = new GameObject(name);
            bus.transform.SetParent(root);
            bus.transform.localPosition = start;
            CreatePrimitiveChild("Bus Body", PrimitiveType.Cube, bus.transform,
                new Vector3(0f, 1.15f, 0f), new Vector3(2.25f, 1.85f, 5.8f), body);
            CreatePrimitiveChild("Bus Lower Trim", PrimitiveType.Cube, bus.transform,
                new Vector3(0f, 0.48f, 0f), new Vector3(2.35f, 0.34f, 5.92f), accent);
            CreatePrimitiveChild("Bus Windscreen", PrimitiveType.Cube, bus.transform,
                new Vector3(0f, 1.48f, 2.94f), new Vector3(1.90f, 0.88f, 0.10f), glass);
            for (int side = -1; side <= 1; side += 2)
            {
                for (int window = 0; window < 4; window++)
                {
                    CreatePrimitiveChild($"Bus Window {side}-{window + 1}", PrimitiveType.Cube, bus.transform,
                        new Vector3(side * 1.14f, 1.48f, -1.75f + window * 1.15f),
                        new Vector3(0.08f, 0.72f, 0.88f), glass);
                }
            }
            Vector3[] wheels =
            {
                new Vector3(-1.17f, 0.38f, 1.85f), new Vector3(1.17f, 0.38f, 1.85f),
                new Vector3(-1.17f, 0.38f, -1.85f), new Vector3(1.17f, 0.38f, -1.85f)
            };
            for (int index = 0; index < wheels.Length; index++)
            {
                Transform wheel = CreatePrimitiveChild($"Bus Wheel {index + 1}", PrimitiveType.Cylinder,
                    bus.transform, wheels[index], new Vector3(0.42f, 0.16f, 0.42f), tyre).transform;
                wheel.localRotation = Quaternion.Euler(0f, 0f, 90f);
            }
            CreatePrimitiveChild("Bus Front Grille", PrimitiveType.Cube, bus.transform,
                new Vector3(0f, 0.76f, 2.96f), new Vector3(1.25f, 0.36f, 0.08f), dark);
            bus.AddComponent<CinematicActorMotion>().Configure(destination, delay, duration, true);
        }

        private static GameObject CreateCinematicRoot(string name)
        {
            return new GameObject(name);
        }

        private static GameObject ParentPerson(GameObject person, Transform parent)
        {
            person.transform.SetParent(parent, true);
            return person;
        }

        private static void CreateCinematicCameraPoints(
            Transform root, Vector3 cameraPosition, Vector3 focusPosition,
            List<Transform> anchors, List<Transform> focuses)
        {
            GameObject anchor = new GameObject("Camera Anchor");
            anchor.transform.SetParent(root);
            anchor.transform.localPosition = cameraPosition;
            GameObject focus = new GameObject("Camera Focus");
            focus.transform.SetParent(root);
            focus.transform.localPosition = focusPosition;
            anchors.Add(anchor.transform);
            focuses.Add(focus.transform);
        }

        private static void CreateOpenWorldLighting()
        {
            GameObject lightObject = new GameObject("Prayagraj Clear Morning Sun");
            Light sunlight = lightObject.AddComponent<Light>();
            sunlight.type = LightType.Directional;
            sunlight.color = new Color(1f, 0.94f, 0.82f);
            sunlight.intensity = 1.08f;
            sunlight.shadows = LightShadows.Hard;
            sunlight.shadowStrength = 0.58f;
            lightObject.transform.rotation = Quaternion.Euler(42f, -34f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.36f, 0.58f, 0.76f);
            RenderSettings.ambientEquatorColor = new Color(0.62f, 0.60f, 0.50f);
            RenderSettings.ambientGroundColor = new Color(0.19f, 0.24f, 0.18f);
            RenderSettings.ambientIntensity = 0.88f;
            RenderSettings.reflectionIntensity = 0.58f;
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.67f, 0.79f, 0.86f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 205f;
            RenderSettings.fogEndDistance = 560f;
        }
    }
}

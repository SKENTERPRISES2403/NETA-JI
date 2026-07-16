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
            CreateStoryMissionMarker(world.transform, controller, teal, yellow, darkStone);
            CreateOpenWorldLighting();

            EditorSceneManager.MarkSceneDirty(freeRoamScene);
            EditorSceneManager.SaveScene(freeRoamScene, FreeRoamScenePath);
        }

        private static void CreateStoryMissionMarker(
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
                Vector3.zero, yellow, parent, 0.028f);

            StoryHubController hub = marker.AddComponent<StoryHubController>();
            hub.Configure(player, new Vector3(34f, 0f, -188f), Vector3.zero);
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

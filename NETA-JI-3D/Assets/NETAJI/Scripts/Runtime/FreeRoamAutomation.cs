using System.Collections;
using System.IO;
using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class FreeRoamAutomation : MonoBehaviour
    {
        private void Start()
        {
            string[] arguments = System.Environment.GetCommandLineArgs();
            if (System.Array.IndexOf(arguments, "-storyHubSmoke") >= 0)
            {
                StartCoroutine(RunOpenWorldChapterOneSmoke(arguments, true));
                return;
            }
            if (System.Array.IndexOf(arguments, "-openWorldChapterOneSmoke") >= 0)
            {
                StartCoroutine(RunOpenWorldChapterOneSmoke(arguments, false));
                return;
            }
            if (System.Array.IndexOf(arguments, "-freeRoamSmoke") >= 0)
            {
                StartCoroutine(RunSmoke(arguments));
            }
        }

        private IEnumerator RunSmoke(string[] arguments)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            GameSession.Instance?.ResetProgress();
            yield return new WaitForSeconds(2.2f);

            AzadController player = FindFirstObjectByType<AzadController>();
            FreeRoamMapHud map = FindFirstObjectByType<FreeRoamMapHud>();
            StoryHubController storyHub = FindFirstObjectByType<StoryHubController>();
            OpenWorldPresentation presentation = FindFirstObjectByType<OpenWorldPresentation>();
            PrototypeAudio audio = FindFirstObjectByType<PrototypeAudio>();
            ThirdPersonCamera orbitCamera = FindFirstObjectByType<ThirdPersonCamera>();
            FreeRoamVehicle[] vehicles = FindObjectsByType<FreeRoamVehicle>(FindObjectsSortMode.None);
            string[] ambientNames =
            {
                "Prayagraj Auto 1", "Prayagraj Auto 2", "Prayagraj Auto 3",
                "Prayagraj Auto 4", "Prayagraj City Bus", "Allahpur Seva Bus"
            };
            int ambientVehicles = 0;
            GameObject trackedAmbientVehicle = null;
            foreach (string ambientName in ambientNames)
            {
                GameObject ambientVehicle = GameObject.Find(ambientName);
                if (ambientVehicle != null)
                {
                    ambientVehicles++;
                    trackedAmbientVehicle ??= ambientVehicle;
                }
            }
            bool requiredSystems = player != null && map != null && storyHub != null
                && presentation != null && presentation.IsApplied
                && audio != null && audio.IsLocationAware
                && orbitCamera != null
                && vehicles.Length >= 2 && ambientVehicles == ambientNames.Length;
            if (!requiredSystems)
            {
                Debug.LogError(
                    $"FREE_ROAM_SMOKE_FAILED: player={player != null}, map={map != null}, "
                    + $"story={storyHub != null}, presentation={presentation?.IsApplied ?? false}, "
                    + $"audio={audio?.IsLocationAware ?? false}, camera={orbitCamera != null}, "
                    + $"vehicles={vehicles.Length}, ambient={ambientVehicles}.");
                Application.Quit(3);
                yield break;
            }
            Vector3 ambientStart = trackedAmbientVehicle.transform.position;

            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-walk.png"));
            yield return new WaitForSeconds(0.8f);
            map.SetFullMapOpen(true);
            yield return new WaitForSeconds(0.25f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-map.png"));
            yield return new WaitForSeconds(0.7f);
            map.SetFullMapOpen(false);

            player.Teleport(new Vector3(124f, 0f, -42f), Quaternion.Euler(0f, 90f, 0f));
            orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
            orbitCamera.SetOrbit(90f, 30f, true);
            yield return new WaitForSeconds(0.75f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-ghat.png"));
            yield return new WaitForSeconds(0.45f);
            player.Teleport(new Vector3(27f, 0f, -101f), Quaternion.identity);
            orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
            yield return new WaitForSeconds(0.75f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-market.png"));
            yield return new WaitForSeconds(0.45f);
            player.Teleport(new Vector3(-100f, 0f, 86f), Quaternion.Euler(0f, -45f, 0f));
            orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
            yield return new WaitForSeconds(0.75f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-civic.png"));
            yield return new WaitForSeconds(0.45f);

            player.Teleport(storyHub.transform.position + new Vector3(0f, 0f, -3.2f), Quaternion.identity);
            orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-story-beacon.png"));
            yield return new WaitForSeconds(0.55f);
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            bool storyReady = storyHub.ConfirmationOpen && storyHub.ActiveChapter == 1 && storyHub.CompletedChapters == 0;
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-story-mission.png"));
            yield return new WaitForSeconds(0.55f);
            storyHub.CancelForAutomation();
            yield return new WaitForSeconds(0.35f);

            FreeRoamVehicle car = System.Array.Find(vehicles, item => item.VehicleName.Contains("Compact Car"));
            FreeRoamVehicle scooter = System.Array.Find(vehicles, item => item.VehicleName.Contains("Scooter"));
            if (car == null || scooter == null)
            {
                Debug.LogError("FREE_ROAM_SMOKE_FAILED: named car or scooter was not found.");
                Application.Quit(3);
                yield break;
            }
            Vector3 startPosition = car.transform.position;
            player.Teleport(startPosition + car.transform.right * -2.2f, car.transform.rotation);
            yield return new WaitForSeconds(0.4f);
            car.Interact(player);
            yield return new WaitForFixedUpdate();
            Debug.Log($"FREE_ROAM_CAR_CONTACT: driving={car.IsDriving}, rider={car.RiderVisible}, playerRenderers={player.VisibleRendererCount}, collision={player.CollisionEnabled}, wheels={car.GroundedWheelCount}/{car.WheelCount}, grounded={car.HasGroundContact}.");
            car.SetAutomationInput(0.82f, 0f);
            yield return new WaitForSeconds(3.8f);
            Debug.Log($"FREE_ROAM_CAR_DRIVE: driving={car.IsDriving}, rider={car.RiderVisible}, playerRenderers={player.VisibleRendererCount}, speed={car.SpeedKph:F1}kph.");
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-drive.png"));
            yield return new WaitForSeconds(0.6f);
            car.StopAutomationInput();
            float distance = Vector3.Distance(startPosition, car.transform.position);
            float carSpeed = car.SpeedKph;
            car.ExitForAutomation();
            yield return new WaitForSeconds(0.6f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-exit.png"));
            yield return new WaitForSeconds(0.5f);

            Vector3 scooterStart = scooter.transform.position;
            player.Teleport(scooterStart + scooter.transform.right * -1.8f, scooter.transform.rotation);
            yield return new WaitForSeconds(0.35f);
            scooter.Interact(player);
            yield return new WaitForFixedUpdate();
            Debug.Log($"FREE_ROAM_SCOOTER_CONTACT: driving={scooter.IsDriving}, rider={scooter.RiderVisible}, playerRenderers={player.VisibleRendererCount}, collision={player.CollisionEnabled}, wheels={scooter.GroundedWheelCount}/{scooter.WheelCount}, grounded={scooter.HasGroundContact}.");
            scooter.SetAutomationInput(0.86f, -0.08f);
            yield return new WaitForSeconds(3.2f);
            Debug.Log($"FREE_ROAM_SCOOTER_DRIVE: driving={scooter.IsDriving}, rider={scooter.RiderVisible}, playerRenderers={player.VisibleRendererCount}, speed={scooter.SpeedKph:F1}kph.");
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-scooter.png"));
            yield return new WaitForSeconds(0.55f);
            scooter.StopAutomationInput();
            float scooterDistance = Vector3.Distance(scooterStart, scooter.transform.position);
            float scooterSpeed = scooter.SpeedKph;
            scooter.ExitForAutomation();
            yield return new WaitForSeconds(0.45f);

            float ambientDistance = Vector3.Distance(ambientStart, trackedAmbientVehicle.transform.position);
            bool passed = storyReady
                && presentation.IsApplied
                && audio.IsLocationAware
                && ambientDistance >= 1f
                && distance >= 4f
                && scooterDistance >= 3f;
            Debug.Log(passed
                ? $"FREE_ROAM_SMOKE_PASSED: story=chapter{storyHub.ActiveChapter}, vehicles={vehicles.Length}, "
                    + $"ambient={ambientVehicles}/{ambientDistance:F1}m, msaa={presentation.AppliedAntiAliasing}, "
                    + $"audio=location-aware, car={distance:F1}m/{carSpeed:F1}kph, "
                    + $"scooter={scooterDistance:F1}m/{scooterSpeed:F1}kph, map=live."
                : $"FREE_ROAM_SMOKE_FAILED: story={storyReady}, presentation={presentation.IsApplied}, "
                    + $"audio={audio.IsLocationAware}, ambient={ambientDistance:F1}m, "
                    + $"car={distance:F1}m/{carSpeed:F1}kph, scooter={scooterDistance:F1}m/{scooterSpeed:F1}kph.");
            Application.Quit(passed ? 0 : 3);
        }

        private IEnumerator RunOpenWorldChapterOneSmoke(string[] arguments, bool storyHubAlias)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            GameSession.Instance?.ResetProgress();
            yield return new WaitForSeconds(1.5f);

            AzadController player = FindFirstObjectByType<AzadController>();
            StoryHubController storyHub = FindFirstObjectByType<StoryHubController>();
            OpenWorldMissionDirector director = FindFirstObjectByType<OpenWorldMissionDirector>();
            ThirdPersonCamera orbitCamera = FindFirstObjectByType<ThirdPersonCamera>();
            FreeRoamMapHud map = FindFirstObjectByType<FreeRoamMapHud>();
            if (player == null || storyHub == null || director == null || orbitCamera == null || map == null || GameSession.Instance == null)
            {
                Debug.LogError(
                    $"OPEN_WORLD_CHAPTER_ONE_SMOKE_FAILED: player={player != null}, hub={storyHub != null}, "
                    + $"director={director != null}, camera={orbitCamera != null}, map={map != null}, save={GameSession.Instance != null}.");
                Application.Quit(6);
                yield break;
            }

            player.Teleport(storyHub.transform.position + new Vector3(0f, 0f, -3.2f), Quaternion.identity);
            orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter01-confirm.png"));
            bool confirmationReady = storyHub.ConfirmationOpen && storyHub.ActiveChapter == 1;
            yield return new WaitForSeconds(0.35f);
            storyHub.StartMissionForAutomation();
            yield return new WaitForSeconds(0.8f);

            MissionController mission = director.ActiveMission;
            string activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            bool startedInWorld = confirmationReady && director.MissionActive && mission != null && activeScene == "FreeRoam";
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter01-start.png"));
            if (!startedInWorld)
            {
                Debug.LogError(
                    $"OPEN_WORLD_CHAPTER_ONE_SMOKE_FAILED: confirmation={confirmationReady}, active={director.MissionActive}, "
                    + $"mission={mission != null}, scene={activeScene}.");
                Application.Quit(6);
                yield break;
            }

            map.SetFullMapOpen(true);
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter01-map-route.png"));
            yield return new WaitForSeconds(0.35f);
            map.SetFullMapOpen(false);
            yield return new WaitForSeconds(3.1f);

            int completedSteps = 0;
            while (!mission.IsComplete && completedSteps < 12)
            {
                MissionObjective objective = mission.CurrentObjectiveItem;
                if (objective == null)
                {
                    break;
                }

                player.Teleport(objective.transform.position + new Vector3(0f, 0f, -1.35f), Quaternion.identity);
                orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
                yield return new WaitForSeconds(0.35f);
                if (completedSteps == 1)
                {
                    orbitCamera.SetOrbit(90f, 28f, true);
                    yield return new WaitForSeconds(0.25f);
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter01-ghat.png"));
                }
                else if (completedSteps == 5)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter01-allahpur.png"));
                }
                else if (completedSteps == 7)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter01-records.png"));
                }

                objective.Interact(player);
                completedSteps++;
                yield return new WaitForSeconds(0.35f);
            }

            PlayerProgress progress = GameSession.Instance.Progress;
            bool completed = mission.IsComplete
                && completedSteps == 9
                && progress.chapterOneComplete
                && progress.publicTrust == 35
                && progress.money == 950
                && progress.reputation == 16
                && storyHub.ActiveChapter == 2
                && storyHub.CompletedChapters == 1
                && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
            yield return new WaitForSeconds(8.4f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter01-complete.png"));
            yield return new WaitForSeconds(0.45f);

            director.ExitCompletedMission();
            yield return new WaitForSeconds(0.6f);
            bool returnedToHub = completed
                && !director.MissionActive
                && Vector3.Distance(player.transform.position, storyHub.ReturnSpawn) < 1.5f;
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            bool chapterTwoReady = returnedToHub
                && storyHub.ConfirmationOpen
                && storyHub.ActiveChapter == 2
                && storyHub.CompletedChapters == 1;
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter02-ready.png"));
            yield return new WaitForSeconds(0.35f);
            storyHub.CancelForAutomation();

            bool passed = chapterTwoReady;
            string passMessage = storyHubAlias
                ? "STORY_HUB_SMOKE_PASSED: Chapter 1 ran inside connected Prayagraj, saved completion, returned to Helpers Hand, and advanced to Chapter 2."
                : $"OPEN_WORLD_CHAPTER_ONE_SMOKE_PASSED: steps={completedSteps}, trust={progress.publicTrust}, funds={progress.money}, rep={progress.reputation}, scene=FreeRoam, next=Chapter02.";
            Debug.Log(passed
                ? passMessage
                : $"OPEN_WORLD_CHAPTER_ONE_SMOKE_FAILED: steps={completedSteps}, complete={completed}, returned={returnedToHub}, "
                    + $"next={chapterTwoReady}, trust={progress.publicTrust}, funds={progress.money}, rep={progress.reputation}.");
            StoryHubFlow.AutomationStage = 0;
            Application.Quit(passed ? 0 : 6);
        }

        private IEnumerator RunStoryHubLaunchSmoke(string[] arguments)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            yield return new WaitForSeconds(1.5f);

            AzadController player = FindFirstObjectByType<AzadController>();
            StoryHubController storyHub = FindFirstObjectByType<StoryHubController>();
            if (player == null || storyHub == null || GameSession.Instance == null)
            {
                Debug.LogError($"STORY_HUB_SMOKE_FAILED: launch systems player={player != null}, hub={storyHub != null}, save={GameSession.Instance != null}.");
                Application.Quit(5);
                yield break;
            }

            GameSession.Instance.ResetProgress();
            yield return new WaitForSeconds(0.35f);
            player.Teleport(storyHub.transform.position + new Vector3(0f, 0f, -3.2f), Quaternion.identity);
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            bool launchReady = storyHub.ConfirmationOpen
                && storyHub.ActiveChapter == 1
                && storyHub.CompletedChapters == 0;
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "story-hub-launch.png"));
            yield return new WaitForSeconds(0.65f);
            if (!launchReady)
            {
                Debug.LogError($"STORY_HUB_SMOKE_FAILED: launchReady={launchReady}, chapter={storyHub.ActiveChapter}, completed={storyHub.CompletedChapters}.");
                Application.Quit(5);
                yield break;
            }

            StoryHubFlow.AutomationStage = 1;
            storyHub.StartMissionForAutomation();
        }

        private IEnumerator RunStoryHubReturnSmoke(string[] arguments)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            yield return new WaitForSeconds(1.7f);

            AzadController player = FindFirstObjectByType<AzadController>();
            StoryHubController storyHub = FindFirstObjectByType<StoryHubController>();
            PlayerProgress progress = GameSession.Instance?.Progress;
            bool returned = player != null && storyHub != null && progress != null
                && Vector3.Distance(player.transform.position, storyHub.ReturnSpawn) < 1.5f
                && progress.chapterOneComplete
                && storyHub.ActiveChapter == 2
                && storyHub.CompletedChapters == 1;
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "story-hub-return.png"));
            yield return new WaitForSeconds(0.65f);

            if (returned)
            {
                storyHub.OpenForAutomation(player);
                yield return new WaitForSeconds(0.30f);
                ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "story-hub-chapter02.png"));
                yield return new WaitForSeconds(0.55f);
                storyHub.CancelForAutomation();
            }

            Debug.Log(returned
                ? "STORY_HUB_SMOKE_PASSED: hub launched Chapter 1, saved completion, returned near Helpers Hand, and advanced to Chapter 2."
                : $"STORY_HUB_SMOKE_FAILED: returned={returned}, player={player != null}, hub={storyHub != null}, complete={progress?.chapterOneComplete ?? false}, chapter={storyHub?.ActiveChapter ?? 0}, completed={storyHub?.CompletedChapters ?? 0}.");
            StoryHubFlow.AutomationStage = 0;
            Application.Quit(returned ? 0 : 5);
        }

        private static string ReadArgument(string[] arguments, string name)
        {
            int index = System.Array.IndexOf(arguments, name);
            return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : null;
        }
    }
}

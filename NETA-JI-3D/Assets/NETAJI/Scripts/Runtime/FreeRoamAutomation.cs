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
                StartCoroutine(RunOpenWorldChapterOneSmoke(arguments, true, false));
                return;
            }
            if (System.Array.IndexOf(arguments, "-openWorldChapterOneSmoke") >= 0)
            {
                StartCoroutine(RunOpenWorldChapterOneSmoke(arguments, false, false));
                return;
            }
            if (System.Array.IndexOf(arguments, "-openWorldContinuitySmoke") >= 0)
            {
                StartCoroutine(RunOpenWorldChapterOneSmoke(arguments, false, true));
                return;
            }
            if (System.Array.IndexOf(arguments, "-openWorldChapterTwoSmoke") >= 0)
            {
                StartCoroutine(RunOpenWorldChapterTwoSmoke(arguments, false));
                return;
            }
            if (System.Array.IndexOf(arguments, "-openWorldChapterTwoContinuitySmoke") >= 0)
            {
                StartCoroutine(RunOpenWorldChapterTwoSmoke(arguments, true));
                return;
            }
            if (System.Array.IndexOf(arguments, "-openWorldChapterThreeSmoke") >= 0)
            {
                StartCoroutine(RunOpenWorldChapterThreeSmoke(arguments, false));
                return;
            }
            if (System.Array.IndexOf(arguments, "-openWorldChapterThreeContinuitySmoke") >= 0)
            {
                StartCoroutine(RunOpenWorldChapterThreeSmoke(arguments, true));
                return;
            }
            if (System.Array.IndexOf(arguments, "-openWorldChapterFourSmoke") >= 0)
            {
                StartCoroutine(RunOpenWorldChapterFourSmoke(arguments, false));
                return;
            }
            if (System.Array.IndexOf(arguments, "-openWorldChapterFourContinuitySmoke") >= 0)
            {
                StartCoroutine(RunOpenWorldChapterFourSmoke(arguments, true));
                return;
            }
            if (System.Array.IndexOf(arguments, "-openWorldChapterFiveSmoke") >= 0)
            {
                StartCoroutine(RunOpenWorldChapterFiveSmoke(arguments));
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

        private IEnumerator RunOpenWorldChapterOneSmoke(
            string[] arguments,
            bool storyHubAlias,
            bool continueIntoNextChapter)
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

            yield return new WaitForSeconds(0.35f);
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

            if (continueIntoNextChapter)
            {
                bool continued = completed && director.ContinueToNextChapter();
                yield return new WaitForSeconds(0.8f);
                MissionController nextMission = director.ActiveMission;
                OpenWorldMissionAtmosphere atmosphere = FindFirstObjectByType<OpenWorldMissionAtmosphere>();
                bool continuityPassed = continued
                    && director.MissionActive
                    && nextMission != null
                    && nextMission.ChapterNumber == 2
                    && atmosphere != null
                    && atmosphere.IsApplied
                    && storyHub.ActiveChapter == 2
                    && storyHub.CompletedChapters == 1
                    && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
                ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter02-continuous-start.png"));
                yield return new WaitForSeconds(0.45f);
                Debug.Log(continuityPassed
                    ? "OPEN_WORLD_CONTINUITY_SMOKE_PASSED: Chapter 1 advanced directly into Chapter 2 inside FreeRoam with evening atmosphere active."
                    : $"OPEN_WORLD_CONTINUITY_SMOKE_FAILED: continued={continued}, active={director.MissionActive}, "
                        + $"chapter={nextMission?.ChapterNumber ?? 0}, atmosphere={atmosphere?.IsApplied ?? false}, "
                        + $"hubChapter={storyHub.ActiveChapter}, completed={storyHub.CompletedChapters}, "
                        + $"scene={UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}.");
                Application.Quit(continuityPassed ? 0 : 8);
                yield break;
            }

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

        private IEnumerator RunOpenWorldChapterTwoSmoke(string[] arguments, bool continueIntoNextChapter)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            GameSession.Instance?.ResetProgress();
            GameSession.Instance?.ApplyReward(23, 100, 12);
            GameSession.Instance?.CompleteChapter(1);
            yield return new WaitForSeconds(1.5f);

            AzadController player = FindFirstObjectByType<AzadController>();
            StoryHubController storyHub = FindFirstObjectByType<StoryHubController>();
            OpenWorldMissionDirector director = FindFirstObjectByType<OpenWorldMissionDirector>();
            ThirdPersonCamera orbitCamera = FindFirstObjectByType<ThirdPersonCamera>();
            FreeRoamMapHud map = FindFirstObjectByType<FreeRoamMapHud>();
            bool systemsReady = player != null && storyHub != null && director != null
                && orbitCamera != null && map != null && GameSession.Instance != null
                && director.CanHostChapter(2) && storyHub.ActiveChapter == 2;
            if (!systemsReady)
            {
                Debug.LogError(
                    $"OPEN_WORLD_CHAPTER_TWO_SMOKE_FAILED: player={player != null}, hub={storyHub != null}, "
                    + $"director={director != null}, camera={orbitCamera != null}, map={map != null}, "
                    + $"hosted={director?.CanHostChapter(2) ?? false}, activeChapter={storyHub?.ActiveChapter ?? 0}.");
                Application.Quit(7);
                yield break;
            }

            player.Teleport(storyHub.transform.position + new Vector3(0f, 0f, -3.2f), Quaternion.identity);
            orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter02-confirm.png"));
            bool confirmationReady = storyHub.ConfirmationOpen && storyHub.ActiveChapter == 2;
            yield return new WaitForSeconds(0.35f);
            storyHub.StartMissionForAutomation();
            yield return new WaitForSeconds(0.8f);

            MissionController mission = director.ActiveMission;
            OpenWorldMissionAtmosphere atmosphere = FindFirstObjectByType<OpenWorldMissionAtmosphere>();
            bool startedInWorld = confirmationReady
                && director.MissionActive
                && mission != null
                && mission.ChapterNumber == 2
                && atmosphere != null
                && atmosphere.IsApplied
                && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter02-start.png"));
            if (!startedInWorld)
            {
                Debug.LogError(
                    $"OPEN_WORLD_CHAPTER_TWO_SMOKE_FAILED: confirmation={confirmationReady}, active={director.MissionActive}, "
                    + $"chapter={mission?.ChapterNumber ?? 0}, atmosphere={atmosphere?.IsApplied ?? false}, "
                    + $"scene={UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}.");
                Application.Quit(7);
                yield break;
            }

            yield return new WaitForSeconds(0.35f);
            map.SetFullMapOpen(true);
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter02-map-route.png"));
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
                if (completedSteps == 0)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter02-courtyard.png"));
                }
                else if (completedSteps == 4)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter02-admission.png"));
                }
                else if (completedSteps == 6)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter02-safety.png"));
                }
                else if (completedSteps == 7)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter02-class.png"));
                }

                objective.Interact(player);
                completedSteps++;
                yield return new WaitForSeconds(0.35f);
            }

            PlayerProgress progress = GameSession.Instance.Progress;
            bool completed = mission.IsComplete
                && completedSteps == 9
                && progress.chapterTwoComplete
                && progress.publicTrust == 58
                && progress.money == 1050
                && progress.reputation == 30
                && storyHub.ActiveChapter == 3
                && storyHub.CompletedChapters == 2
                && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
            yield return new WaitForSeconds(8.4f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter02-complete.png"));
            yield return new WaitForSeconds(0.45f);

            if (continueIntoNextChapter)
            {
                bool continued = completed && director.ContinueToNextChapter();
                yield return new WaitForSeconds(0.8f);
                MissionController nextMission = director.ActiveMission;
                OpenWorldMissionAtmosphere nextAtmosphere = FindFirstObjectByType<OpenWorldMissionAtmosphere>();
                bool continuityPassed = continued
                    && director.MissionActive
                    && nextMission != null
                    && nextMission.ChapterNumber == 3
                    && nextAtmosphere != null
                    && nextAtmosphere.IsNightSearch
                    && storyHub.ActiveChapter == 3
                    && storyHub.CompletedChapters == 2
                    && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
                ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-continuous-start.png"));
                yield return new WaitForSeconds(0.45f);
                Debug.Log(continuityPassed
                    ? "OPEN_WORLD_CHAPTER_TWO_CONTINUITY_SMOKE_PASSED: Chapter 2 advanced directly into Chapter 3 inside FreeRoam with night search active."
                    : $"OPEN_WORLD_CHAPTER_TWO_CONTINUITY_SMOKE_FAILED: continued={continued}, active={director.MissionActive}, "
                        + $"chapter={nextMission?.ChapterNumber ?? 0}, night={nextAtmosphere?.IsNightSearch ?? false}, "
                        + $"hubChapter={storyHub.ActiveChapter}, completed={storyHub.CompletedChapters}, "
                        + $"scene={UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}.");
                Application.Quit(continuityPassed ? 0 : 9);
                yield break;
            }

            director.ExitCompletedMission();
            yield return new WaitForSeconds(0.6f);
            bool atmosphereRestored = atmosphere != null && !atmosphere.IsApplied;
            bool returnedToHub = completed
                && atmosphereRestored
                && !director.MissionActive
                && Vector3.Distance(player.transform.position, storyHub.ReturnSpawn) < 1.5f;
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            bool chapterThreeReady = returnedToHub
                && storyHub.ConfirmationOpen
                && storyHub.ActiveChapter == 3
                && storyHub.CompletedChapters == 2;
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-ready.png"));
            yield return new WaitForSeconds(0.35f);
            storyHub.CancelForAutomation();

            Debug.Log(chapterThreeReady
                ? $"OPEN_WORLD_CHAPTER_TWO_SMOKE_PASSED: steps={completedSteps}, trust={progress.publicTrust}, funds={progress.money}, rep={progress.reputation}, atmosphere=restored, scene=FreeRoam, next=Chapter03."
                : $"OPEN_WORLD_CHAPTER_TWO_SMOKE_FAILED: steps={completedSteps}, complete={completed}, returned={returnedToHub}, "
                    + $"atmosphere={atmosphereRestored}, next={chapterThreeReady}, trust={progress.publicTrust}, "
                    + $"funds={progress.money}, rep={progress.reputation}.");
            Application.Quit(chapterThreeReady ? 0 : 7);
        }

        private IEnumerator RunOpenWorldChapterThreeSmoke(string[] arguments, bool continueIntoNextChapter)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            GameSession.Instance?.ResetProgress();
            GameSession.Instance?.ApplyReward(23, 100, 12);
            GameSession.Instance?.CompleteChapter(1);
            GameSession.Instance?.ApplyReward(23, 100, 14);
            GameSession.Instance?.CompleteChapter(2);
            yield return new WaitForSeconds(1.5f);

            AzadController player = FindFirstObjectByType<AzadController>();
            StoryHubController storyHub = FindFirstObjectByType<StoryHubController>();
            OpenWorldMissionDirector director = FindFirstObjectByType<OpenWorldMissionDirector>();
            ThirdPersonCamera orbitCamera = FindFirstObjectByType<ThirdPersonCamera>();
            FreeRoamMapHud map = FindFirstObjectByType<FreeRoamMapHud>();
            bool systemsReady = player != null && storyHub != null && director != null
                && orbitCamera != null && map != null && GameSession.Instance != null
                && director.CanHostChapter(3) && storyHub.ActiveChapter == 3;
            if (!systemsReady)
            {
                Debug.LogError(
                    $"OPEN_WORLD_CHAPTER_THREE_SMOKE_FAILED: player={player != null}, hub={storyHub != null}, "
                    + $"director={director != null}, camera={orbitCamera != null}, map={map != null}, "
                    + $"hosted={director?.CanHostChapter(3) ?? false}, activeChapter={storyHub?.ActiveChapter ?? 0}.");
                Application.Quit(9);
                yield break;
            }

            player.Teleport(storyHub.transform.position + new Vector3(0f, 0f, -3.2f), Quaternion.identity);
            orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-confirm.png"));
            bool confirmationReady = storyHub.ConfirmationOpen && storyHub.ActiveChapter == 3;
            yield return new WaitForSeconds(0.35f);
            storyHub.StartMissionForAutomation();
            yield return new WaitForSeconds(0.8f);

            MissionController mission = director.ActiveMission;
            OpenWorldMissionAtmosphere atmosphere = FindFirstObjectByType<OpenWorldMissionAtmosphere>();
            bool startedInWorld = confirmationReady
                && director.MissionActive
                && mission != null
                && mission.ChapterNumber == 3
                && atmosphere != null
                && atmosphere.IsNightSearch
                && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-start.png"));
            if (!startedInWorld)
            {
                Debug.LogError(
                    $"OPEN_WORLD_CHAPTER_THREE_SMOKE_FAILED: confirmation={confirmationReady}, active={director.MissionActive}, "
                    + $"chapter={mission?.ChapterNumber ?? 0}, night={atmosphere?.IsNightSearch ?? false}, "
                    + $"scene={UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}.");
                Application.Quit(9);
                yield break;
            }

            yield return new WaitForSeconds(0.35f);
            map.SetFullMapOpen(true);
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-map-route.png"));
            yield return new WaitForSeconds(0.35f);
            map.SetFullMapOpen(false);
            yield return new WaitForSeconds(3.1f);

            int completedSteps = 0;
            while (!mission.IsComplete && completedSteps < 14)
            {
                MissionObjective objective = mission.CurrentObjectiveItem;
                if (objective == null)
                {
                    break;
                }

                player.Teleport(objective.transform.position + new Vector3(0f, 0f, -1.35f), Quaternion.identity);
                orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
                orbitCamera.SetOrbit(completedSteps % 2 == 0 ? -16f : 16f, 20f, true);
                yield return new WaitForSeconds(0.40f);
                if (completedSteps == 0)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-home-alert.png"));
                }
                else if (completedSteps == 1)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-ribbon.png"));
                }
                else if (completedSteps == 3)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-cctv-stall.png"));
                }
                else if (completedSteps == 5)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-footage.png"));
                }
                else if (completedSteps == 6)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-search-desk.png"));
                }
                else if (completedSteps == 7)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-police-response.png"));
                }
                else if (completedSteps == 10)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-call.png"));
                }

                objective.Interact(player);
                completedSteps++;
                yield return new WaitForSeconds(
                    completedSteps == 3 || completedSteps == 6 || completedSteps == 9 ? 6.0f : 0.35f);
            }

            PlayerProgress progress = GameSession.Instance.Progress;
            bool completed = mission.IsComplete
                && completedSteps == 11
                && progress.chapterThreeComplete
                && progress.publicTrust == 67
                && progress.money == 900
                && progress.reputation == 40
                && storyHub.ActiveChapter == 4
                && storyHub.CompletedChapters == 3
                && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
            yield return new WaitForSeconds(8.4f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter03-complete.png"));
            yield return new WaitForSeconds(0.45f);

            if (continueIntoNextChapter)
            {
                bool continued = completed && director.ContinueToNextChapter();
                yield return new WaitForSeconds(0.8f);
                MissionController nextMission = director.ActiveMission;
                OpenWorldMissionAtmosphere nextAtmosphere = FindFirstObjectByType<OpenWorldMissionAtmosphere>();
                bool continuityPassed = continued
                    && director.MissionActive
                    && nextMission != null
                    && nextMission.ChapterNumber == 4
                    && nextAtmosphere != null
                    && nextAtmosphere.IsDawnRescue
                    && storyHub.ActiveChapter == 4
                    && storyHub.CompletedChapters == 3
                    && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
                ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-continuous-start.png"));
                yield return new WaitForSeconds(0.45f);
                Debug.Log(continuityPassed
                    ? "OPEN_WORLD_CHAPTER_THREE_CONTINUITY_SMOKE_PASSED: Chapter 3 advanced directly into Chapter 4 inside FreeRoam with dawn rescue active."
                    : $"OPEN_WORLD_CHAPTER_THREE_CONTINUITY_SMOKE_FAILED: continued={continued}, active={director.MissionActive}, "
                        + $"chapter={nextMission?.ChapterNumber ?? 0}, dawn={nextAtmosphere?.IsDawnRescue ?? false}, "
                        + $"hubChapter={storyHub.ActiveChapter}, completed={storyHub.CompletedChapters}, "
                        + $"scene={UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}.");
                Application.Quit(continuityPassed ? 0 : 10);
                yield break;
            }

            director.ExitCompletedMission();
            yield return new WaitForSeconds(0.6f);
            bool atmosphereRestored = atmosphere != null && !atmosphere.IsApplied;
            bool returnedToHub = completed
                && atmosphereRestored
                && !director.MissionActive
                && Vector3.Distance(player.transform.position, storyHub.ReturnSpawn) < 1.5f;
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            bool chapterFourReady = returnedToHub
                && storyHub.ConfirmationOpen
                && storyHub.ActiveChapter == 4
                && storyHub.CompletedChapters == 3;
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-ready.png"));
            yield return new WaitForSeconds(0.35f);
            storyHub.CancelForAutomation();

            Debug.Log(chapterFourReady
                ? $"OPEN_WORLD_CHAPTER_THREE_SMOKE_PASSED: steps={completedSteps}, trust={progress.publicTrust}, funds={progress.money}, rep={progress.reputation}, night=restored, scene=FreeRoam, next=Chapter04."
                : $"OPEN_WORLD_CHAPTER_THREE_SMOKE_FAILED: steps={completedSteps}, complete={completed}, returned={returnedToHub}, "
                    + $"nightRestored={atmosphereRestored}, next={chapterFourReady}, trust={progress.publicTrust}, "
                    + $"funds={progress.money}, rep={progress.reputation}.");
            Application.Quit(chapterFourReady ? 0 : 9);
        }

        private IEnumerator RunOpenWorldChapterFourSmoke(string[] arguments, bool continueIntoNextChapter)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            GameSession.Instance?.ResetProgress();
            GameSession.Instance?.ApplyReward(23, 100, 12);
            GameSession.Instance?.CompleteChapter(1);
            GameSession.Instance?.ApplyReward(23, 100, 14);
            GameSession.Instance?.CompleteChapter(2);
            GameSession.Instance?.ApplyReward(9, -150, 10);
            GameSession.Instance?.CompleteChapter(3);
            yield return new WaitForSeconds(1.5f);

            AzadController player = FindFirstObjectByType<AzadController>();
            StoryHubController storyHub = FindFirstObjectByType<StoryHubController>();
            OpenWorldMissionDirector director = FindFirstObjectByType<OpenWorldMissionDirector>();
            ThirdPersonCamera orbitCamera = FindFirstObjectByType<ThirdPersonCamera>();
            FreeRoamMapHud map = FindFirstObjectByType<FreeRoamMapHud>();
            bool systemsReady = player != null && storyHub != null && director != null
                && orbitCamera != null && map != null && GameSession.Instance != null
                && director.CanHostChapter(4) && storyHub.ActiveChapter == 4;
            if (!systemsReady)
            {
                Debug.LogError(
                    $"OPEN_WORLD_CHAPTER_FOUR_SMOKE_FAILED: player={player != null}, hub={storyHub != null}, "
                    + $"director={director != null}, camera={orbitCamera != null}, map={map != null}, "
                    + $"hosted={director?.CanHostChapter(4) ?? false}, activeChapter={storyHub?.ActiveChapter ?? 0}.");
                Application.Quit(11);
                yield break;
            }

            player.Teleport(storyHub.transform.position + new Vector3(0f, 0f, -3.2f), Quaternion.identity);
            orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-confirm.png"));
            bool confirmationReady = storyHub.ConfirmationOpen && storyHub.ActiveChapter == 4;
            yield return new WaitForSeconds(0.35f);
            storyHub.StartMissionForAutomation();
            yield return new WaitForSeconds(0.8f);

            MissionController mission = director.ActiveMission;
            OpenWorldMissionAtmosphere atmosphere = FindFirstObjectByType<OpenWorldMissionAtmosphere>();
            bool startedInWorld = confirmationReady
                && director.MissionActive
                && mission != null
                && mission.ChapterNumber == 4
                && atmosphere != null
                && atmosphere.IsDawnRescue
                && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-start.png"));
            if (!startedInWorld)
            {
                Debug.LogError(
                    $"OPEN_WORLD_CHAPTER_FOUR_SMOKE_FAILED: confirmation={confirmationReady}, active={director.MissionActive}, "
                    + $"chapter={mission?.ChapterNumber ?? 0}, dawn={atmosphere?.IsDawnRescue ?? false}, "
                    + $"scene={UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}.");
                Application.Quit(11);
                yield break;
            }

            yield return new WaitForSeconds(0.35f);
            map.SetFullMapOpen(true);
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-map-route.png"));
            yield return new WaitForSeconds(0.35f);
            map.SetFullMapOpen(false);
            yield return new WaitForSeconds(3.1f);

            int completedSteps = 0;
            bool decisionOpened = false;
            while (!mission.IsComplete && completedSteps < 14)
            {
                MissionObjective objective = mission.CurrentObjectiveItem;
                if (objective == null)
                {
                    break;
                }

                player.Teleport(objective.transform.position + new Vector3(0f, 0f, -1.35f), Quaternion.identity);
                orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
                orbitCamera.SetOrbit(completedSteps % 2 == 0 ? -18f : 18f, 19f, true);
                yield return new WaitForSeconds(0.40f);
                if (completedSteps == 0)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-perimeter.png"));
                }
                else if (completedSteps == 1)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-van.png"));
                }
                else if (completedSteps == 2)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-signal.png"));
                }
                else if (completedSteps == 4)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-gate.png"));
                }
                else if (completedSteps == 6)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-sandhya.png"));
                }
                else if (completedSteps == 7)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-first-aid.png"));
                }
                else if (completedSteps == 8)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-ledger.png"));
                }
                else if (completedSteps == 9)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-evidence.png"));
                }
                else if (completedSteps == 10)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-reunion.png"));
                }

                if (completedSteps == 3)
                {
                    objective.Interact(player);
                    yield return new WaitForSeconds(0.35f);
                    decisionOpened = MissionPresentation.IsDecisionOpen;
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-decision.png"));
                    yield return new WaitForEndOfFrame();
                    if (!decisionOpened)
                    {
                        Debug.LogError("OPEN_WORLD_CHAPTER_FOUR_SMOKE_FAILED: rescue decision did not open.");
                        Application.Quit(11);
                        yield break;
                    }
                    objective.ResolveDecisionForAutomation(1);
                }
                else
                {
                    objective.Interact(player);
                }

                completedSteps++;
                yield return new WaitForSeconds(
                    completedSteps == 3 || completedSteps == 6 || completedSteps == 8 ? 6.0f : 0.35f);
            }

            PlayerProgress progress = GameSession.Instance.Progress;
            bool completed = mission.IsComplete
                && completedSteps == 11
                && decisionOpened
                && progress.chapterFourComplete
                && progress.publicTrust == 83
                && progress.money == 650
                && progress.reputation == 56
                && storyHub.ActiveChapter == 5
                && storyHub.CompletedChapters == 4
                && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
            yield return new WaitForSeconds(8.4f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter04-complete.png"));
            yield return new WaitForSeconds(0.45f);

            if (continueIntoNextChapter)
            {
                bool continued = completed && director.ContinueToNextChapter();
                yield return new WaitForSeconds(0.8f);
                MissionController nextMission = director.ActiveMission;
                OpenWorldMissionAtmosphere nextAtmosphere = FindFirstObjectByType<OpenWorldMissionAtmosphere>();
                bool continuityPassed = continued
                    && director.MissionActive
                    && nextMission != null
                    && nextMission.ChapterNumber == 5
                    && nextAtmosphere != null
                    && nextAtmosphere.IsHospitalMorning
                    && storyHub.ActiveChapter == 5
                    && storyHub.CompletedChapters == 4
                    && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
                ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-continuous-start.png"));
                yield return new WaitForSeconds(0.45f);
                Debug.Log(continuityPassed
                    ? "OPEN_WORLD_CHAPTER_FOUR_CONTINUITY_SMOKE_PASSED: Chapter 4 advanced directly into Chapter 5 inside FreeRoam with hospital morning active."
                    : $"OPEN_WORLD_CHAPTER_FOUR_CONTINUITY_SMOKE_FAILED: continued={continued}, active={director.MissionActive}, "
                        + $"chapter={nextMission?.ChapterNumber ?? 0}, morning={nextAtmosphere?.IsHospitalMorning ?? false}, "
                        + $"hubChapter={storyHub.ActiveChapter}, completed={storyHub.CompletedChapters}, "
                        + $"scene={UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}.");
                Application.Quit(continuityPassed ? 0 : 12);
                yield break;
            }

            director.ExitCompletedMission();
            yield return new WaitForSeconds(0.6f);
            bool atmosphereRestored = atmosphere != null && !atmosphere.IsApplied;
            bool returnedToHub = completed
                && atmosphereRestored
                && !director.MissionActive
                && Vector3.Distance(player.transform.position, storyHub.ReturnSpawn) < 1.5f;
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            bool chapterFiveReady = returnedToHub
                && storyHub.ConfirmationOpen
                && storyHub.ActiveChapter == 5
                && storyHub.CompletedChapters == 4;
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-ready.png"));
            yield return new WaitForSeconds(0.35f);
            storyHub.CancelForAutomation();

            Debug.Log(chapterFiveReady
                ? $"OPEN_WORLD_CHAPTER_FOUR_SMOKE_PASSED: steps={completedSteps}, trust={progress.publicTrust}, funds={progress.money}, rep={progress.reputation}, dawn=restored, scene=FreeRoam, next=Chapter05."
                : $"OPEN_WORLD_CHAPTER_FOUR_SMOKE_FAILED: steps={completedSteps}, complete={completed}, returned={returnedToHub}, "
                    + $"dawnRestored={atmosphereRestored}, next={chapterFiveReady}, decision={decisionOpened}, "
                    + $"trust={progress.publicTrust}, funds={progress.money}, rep={progress.reputation}.");
            Application.Quit(chapterFiveReady ? 0 : 11);
        }

        private IEnumerator RunOpenWorldChapterFiveSmoke(string[] arguments)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            GameSession.Instance?.ResetProgress();
            GameSession.Instance?.ApplyReward(23, 100, 12);
            GameSession.Instance?.CompleteChapter(1);
            GameSession.Instance?.ApplyReward(23, 100, 14);
            GameSession.Instance?.CompleteChapter(2);
            GameSession.Instance?.ApplyReward(9, -150, 10);
            GameSession.Instance?.CompleteChapter(3);
            GameSession.Instance?.ApplyReward(16, -250, 16);
            GameSession.Instance?.CompleteChapter(4);
            yield return new WaitForSeconds(1.5f);

            AzadController player = FindFirstObjectByType<AzadController>();
            StoryHubController storyHub = FindFirstObjectByType<StoryHubController>();
            OpenWorldMissionDirector director = FindFirstObjectByType<OpenWorldMissionDirector>();
            ThirdPersonCamera orbitCamera = FindFirstObjectByType<ThirdPersonCamera>();
            FreeRoamMapHud map = FindFirstObjectByType<FreeRoamMapHud>();
            bool systemsReady = player != null && storyHub != null && director != null
                && orbitCamera != null && map != null && GameSession.Instance != null
                && director.CanHostChapter(5) && storyHub.ActiveChapter == 5;
            if (!systemsReady)
            {
                Debug.LogError(
                    $"OPEN_WORLD_CHAPTER_FIVE_SMOKE_FAILED: player={player != null}, hub={storyHub != null}, "
                    + $"director={director != null}, camera={orbitCamera != null}, map={map != null}, "
                    + $"hosted={director?.CanHostChapter(5) ?? false}, activeChapter={storyHub?.ActiveChapter ?? 0}.");
                Application.Quit(13);
                yield break;
            }

            player.Teleport(storyHub.transform.position + new Vector3(0f, 0f, -3.2f), Quaternion.identity);
            orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-confirm.png"));
            bool confirmationReady = storyHub.ConfirmationOpen && storyHub.ActiveChapter == 5;
            yield return new WaitForSeconds(0.35f);
            storyHub.StartMissionForAutomation();
            yield return new WaitForSeconds(0.8f);

            MissionController mission = director.ActiveMission;
            OpenWorldMissionAtmosphere atmosphere = FindFirstObjectByType<OpenWorldMissionAtmosphere>();
            bool startedInWorld = confirmationReady
                && director.MissionActive
                && mission != null
                && mission.ChapterNumber == 5
                && atmosphere != null
                && atmosphere.IsHospitalMorning
                && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-start.png"));
            if (!startedInWorld)
            {
                Debug.LogError(
                    $"OPEN_WORLD_CHAPTER_FIVE_SMOKE_FAILED: confirmation={confirmationReady}, active={director.MissionActive}, "
                    + $"chapter={mission?.ChapterNumber ?? 0}, morning={atmosphere?.IsHospitalMorning ?? false}, "
                    + $"scene={UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}.");
                Application.Quit(13);
                yield break;
            }

            yield return new WaitForSeconds(0.35f);
            map.SetFullMapOpen(true);
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-map-route.png"));
            yield return new WaitForSeconds(0.35f);
            map.SetFullMapOpen(false);
            yield return new WaitForSeconds(3.1f);

            int completedSteps = 0;
            bool decisionOpened = false;
            while (!mission.IsComplete && completedSteps < 15)
            {
                MissionObjective objective = mission.CurrentObjectiveItem;
                if (objective == null)
                {
                    break;
                }

                player.Teleport(objective.transform.position + new Vector3(0f, 0f, -1.35f), Quaternion.identity);
                orbitCamera.SetTarget(player.transform, new Vector3(0f, 1.45f, 0f), 6.2f, true);
                orbitCamera.SetOrbit(completedSteps % 2 == 0 ? -17f : 17f, 19f, true);
                yield return new WaitForSeconds(0.40f);
                if (completedSteps == 0)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-emergency.png"));
                }
                else if (completedSteps == 1)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-transport.png"));
                }
                else if (completedSteps == 2)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-triage.png"));
                }
                else if (completedSteps == 4)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-donors.png"));
                }
                else if (completedSteps == 5)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-stock.png"));
                }
                else if (completedSteps == 6)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-batch.png"));
                }
                else if (completedSteps == 7)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-doctor.png"));
                }
                else if (completedSteps == 9)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-tender.png"));
                }
                else if (completedSteps == 10)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-evidence.png"));
                }
                else if (completedSteps == 11)
                {
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-recovery.png"));
                }

                if (completedSteps == 8)
                {
                    objective.Interact(player);
                    yield return new WaitForSeconds(0.35f);
                    decisionOpened = MissionPresentation.IsDecisionOpen;
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-decision.png"));
                    yield return new WaitForEndOfFrame();
                    if (!decisionOpened)
                    {
                        Debug.LogError("OPEN_WORLD_CHAPTER_FIVE_SMOKE_FAILED: hospital strategy decision did not open.");
                        Application.Quit(13);
                        yield break;
                    }
                    objective.ResolveDecisionForAutomation(1);
                }
                else
                {
                    objective.Interact(player);
                }

                completedSteps++;
                yield return new WaitForSeconds(
                    completedSteps == 5 || completedSteps == 8 || completedSteps == 11 ? 6.0f : 0.35f);
            }

            PlayerProgress progress = GameSession.Instance.Progress;
            bool completed = mission.IsComplete
                && completedSteps == 12
                && decisionOpened
                && progress.chapterFiveComplete
                && progress.publicTrust == 97
                && progress.money == 150
                && progress.reputation == 71
                && progress.caseProof == 17
                && storyHub.ActiveChapter == 6
                && storyHub.CompletedChapters == 5
                && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FreeRoam";
            yield return new WaitForSeconds(8.4f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter05-complete.png"));
            yield return new WaitForSeconds(0.45f);

            director.ExitCompletedMission();
            yield return new WaitForSeconds(0.6f);
            bool atmosphereRestored = atmosphere != null && !atmosphere.IsApplied;
            bool returnedToHub = completed
                && atmosphereRestored
                && !director.MissionActive
                && Vector3.Distance(player.transform.position, storyHub.ReturnSpawn) < 1.5f;
            storyHub.OpenForAutomation(player);
            yield return new WaitForSeconds(0.35f);
            bool chapterSixReady = returnedToHub
                && storyHub.ConfirmationOpen
                && storyHub.ActiveChapter == 6
                && storyHub.CompletedChapters == 5;
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "open-world-chapter06-ready.png"));
            yield return new WaitForSeconds(0.35f);
            storyHub.CancelForAutomation();

            Debug.Log(chapterSixReady
                ? $"OPEN_WORLD_CHAPTER_FIVE_SMOKE_PASSED: steps={completedSteps}, trust={progress.publicTrust}, funds={progress.money}, rep={progress.reputation}, proof={progress.caseProof}, morning=restored, scene=FreeRoam, next=Chapter06."
                : $"OPEN_WORLD_CHAPTER_FIVE_SMOKE_FAILED: steps={completedSteps}, complete={completed}, returned={returnedToHub}, "
                    + $"morningRestored={atmosphereRestored}, next={chapterSixReady}, decision={decisionOpened}, "
                    + $"trust={progress.publicTrust}, funds={progress.money}, rep={progress.reputation}, proof={progress.caseProof}.");
            Application.Quit(chapterSixReady ? 0 : 13);
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

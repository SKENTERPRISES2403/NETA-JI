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
            if (System.Array.IndexOf(arguments, "-freeRoamSmoke") >= 0)
            {
                StartCoroutine(RunSmoke(arguments));
            }
        }

        private IEnumerator RunSmoke(string[] arguments)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            yield return new WaitForSeconds(2.2f);

            AzadController player = FindFirstObjectByType<AzadController>();
            FreeRoamMapHud map = FindFirstObjectByType<FreeRoamMapHud>();
            FreeRoamVehicle[] vehicles = FindObjectsByType<FreeRoamVehicle>(FindObjectsSortMode.None);
            bool requiredSystems = player != null && map != null && vehicles.Length >= 2;
            if (!requiredSystems)
            {
                Debug.LogError($"FREE_ROAM_SMOKE_FAILED: player={player != null}, map={map != null}, vehicles={vehicles.Length}.");
                Application.Quit(3);
                yield break;
            }

            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-walk.png"));
            yield return new WaitForSeconds(0.8f);
            map.SetFullMapOpen(true);
            yield return new WaitForSeconds(0.25f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "free-roam-map.png"));
            yield return new WaitForSeconds(0.7f);
            map.SetFullMapOpen(false);

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

            bool passed = distance >= 4f && scooterDistance >= 3f;
            Debug.Log(passed
                ? $"FREE_ROAM_SMOKE_PASSED: vehicles={vehicles.Length}, car={distance:F1}m/{carSpeed:F1}kph, scooter={scooterDistance:F1}m/{scooterSpeed:F1}kph, map=live."
                : $"FREE_ROAM_SMOKE_FAILED: car={distance:F1}m/{carSpeed:F1}kph, scooter={scooterDistance:F1}m/{scooterSpeed:F1}kph.");
            Application.Quit(passed ? 0 : 3);
        }

        private static string ReadArgument(string[] arguments, string name)
        {
            int index = System.Array.IndexOf(arguments, name);
            return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : null;
        }
    }
}

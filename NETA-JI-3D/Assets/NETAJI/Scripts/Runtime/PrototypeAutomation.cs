using System.Collections;
using System.IO;
using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class PrototypeAutomation : MonoBehaviour
    {
        private void Start()
        {
            string[] arguments = System.Environment.GetCommandLineArgs();
            if (System.Array.IndexOf(arguments, "-prototypeSmoke") >= 0)
            {
                StartCoroutine(RunSmoke(arguments));
            }
        }

        private IEnumerator RunSmoke(string[] arguments)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);

            yield return new WaitForSeconds(1.2f);
            MissionController mission = MissionController.Instance;
            AzadController player = FindFirstObjectByType<AzadController>();
            if (mission == null || player == null)
            {
                Debug.LogError("PROTOTYPE_SMOKE_FAILED: required runtime systems were not found.");
                Application.Quit(2);
                yield break;
            }

            mission.ResetMission();
            string startPath = Path.Combine(outputDirectory, "prototype-start.png");
            ScreenCapture.CaptureScreenshot(startPath);
            yield return new WaitForSeconds(0.8f);

            int guard = 0;
            while (!mission.IsComplete && guard++ < 12)
            {
                MissionObjective objective = mission.CurrentObjectiveItem;
                if (objective == null)
                {
                    break;
                }

                player.transform.position = objective.transform.position + new Vector3(0f, 0.1f, -1.1f);
                objective.Interact(player);
                yield return new WaitForSeconds(0.45f);
            }

            yield return new WaitForSeconds(0.9f);
            string finalPath = Path.Combine(outputDirectory, "prototype-final.png");
            ScreenCapture.CaptureScreenshot(finalPath);
            yield return new WaitForSeconds(1f);

            PlayerProgress progress = GameSession.Instance.Progress;
            bool passed = mission.IsComplete && progress.publicTrust == 26 && progress.money == 800 && progress.reputation == 10;
            Debug.Log(passed
                ? $"PROTOTYPE_SMOKE_PASSED: trust={progress.publicTrust}, money={progress.money}, reputation={progress.reputation}"
                : $"PROTOTYPE_SMOKE_FAILED: complete={mission.IsComplete}, trust={progress.publicTrust}, money={progress.money}, reputation={progress.reputation}");
            Application.Quit(passed ? 0 : 3);
        }

        private static string ReadArgument(string[] arguments, string name)
        {
            int index = System.Array.IndexOf(arguments, name);
            return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : null;
        }
    }
}

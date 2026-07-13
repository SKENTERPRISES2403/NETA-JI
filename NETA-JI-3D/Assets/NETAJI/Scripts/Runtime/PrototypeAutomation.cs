using System.Collections;
using System.IO;
using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class PrototypeAutomation : MonoBehaviour
    {
        [SerializeField] private int chapterNumber = 1;
        [SerializeField] private int expectedTrust = 35;
        [SerializeField] private int expectedMoney = 950;
        [SerializeField] private int expectedReputation = 16;
        [SerializeField] private int expectedProof;
        [SerializeField] private int expectedPower;
        [SerializeField] private int expectedVolunteers;
        [SerializeField] private int expectedPressure;

        public void Configure(
            int chapter,
            int trust,
            int money,
            int reputation,
            int proof = 0,
            int power = 0,
            int team = 0,
            int pressure = 0)
        {
            chapterNumber = Mathf.Max(1, chapter);
            expectedTrust = trust;
            expectedMoney = money;
            expectedReputation = reputation;
            expectedProof = proof;
            expectedPower = power;
            expectedVolunteers = team;
            expectedPressure = pressure;
        }

        private void Start()
        {
            string[] arguments = System.Environment.GetCommandLineArgs();
            string smokeArgument = chapterNumber == 1 ? "-prototypeSmoke" : $"-chapter{chapterNumber}Smoke";
            if (System.Array.IndexOf(arguments, smokeArgument) >= 0
                || (chapterNumber == 4 && System.Array.IndexOf(arguments, "-riskyDecisionSmoke") >= 0)
                || (chapterNumber == 5 && System.Array.IndexOf(arguments, "-riskyHospitalSmoke") >= 0)
                || (chapterNumber == 6 && System.Array.IndexOf(arguments, "-riskyOppositionSmoke") >= 0))
            {
                StartCoroutine(RunSmoke(arguments));
            }
        }

        private IEnumerator RunSmoke(string[] arguments)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            bool riskyDecision = (chapterNumber == 4
                    && System.Array.IndexOf(arguments, "-riskyDecisionSmoke") >= 0)
                || (chapterNumber == 5
                    && System.Array.IndexOf(arguments, "-riskyHospitalSmoke") >= 0)
                || (chapterNumber == 6
                    && System.Array.IndexOf(arguments, "-riskyOppositionSmoke") >= 0);

            yield return new WaitForSeconds(1.2f);
            MissionController mission = MissionController.Instance;
            AzadController player = FindFirstObjectByType<AzadController>();
            if (mission == null || player == null)
            {
                Debug.LogError("PROTOTYPE_SMOKE_FAILED: required runtime systems were not found.");
                Application.Quit(2);
                yield break;
            }

            if (chapterNumber == 1)
            {
                mission.ResetMission(true);
            }
            else if (chapterNumber == 2)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(23, 100, 12);
                GameSession.Instance.CompleteChapter(1);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 3)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(46, 200, 26);
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 4)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(55, 50, 36);
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 5)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(71, -200, 52);
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                GameSession.Instance.CompleteChapter(4);
                mission.ResetMission(false);
            }
            else
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(85, -700, 67, 17);
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                GameSession.Instance.CompleteChapter(4);
                GameSession.Instance.CompleteChapter(5);
                mission.ResetMission(false);
            }

            string filePrefix = chapterNumber == 1
                ? "prototype"
                : riskyDecision ? $"chapter-{chapterNumber}-risky" : $"chapter-{chapterNumber}";
            string startPath = Path.Combine(outputDirectory, filePrefix + "-start.png");
            ScreenCapture.CaptureScreenshot(startPath);
            yield return new WaitForSeconds(0.8f);

            int guard = 0;
            while (!mission.IsComplete && guard++ < 16)
            {
                MissionObjective objective = mission.CurrentObjectiveItem;
                if (objective == null)
                {
                    break;
                }

                player.transform.position = objective.transform.position + new Vector3(0f, 0.1f, -1.1f);
                objective.Interact(player);
                if (objective.RequiresDecision)
                {
                    yield return new WaitForSeconds(0.25f);
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, filePrefix + "-decision.png"));
                    yield return new WaitForSeconds(0.55f);
                    objective.ResolveDecisionForAutomation(riskyDecision ? 2 : 1);
                }
                yield return new WaitForSeconds(0.45f);
            }

            if (chapterNumber >= 3)
            {
                player.transform.position = new Vector3(0f, 0.1f, -4f);
                player.transform.rotation = Quaternion.identity;
                yield return new WaitForSeconds(0.8f);
            }

            yield return new WaitForSeconds(0.9f);
            string finalPath = Path.Combine(outputDirectory, filePrefix + "-final.png");
            ScreenCapture.CaptureScreenshot(finalPath);
            yield return new WaitForSeconds(1f);

            yield return new WaitForSeconds(6.5f);
            string actionsPath = Path.Combine(outputDirectory, filePrefix + "-actions.png");
            ScreenCapture.CaptureScreenshot(actionsPath);
            yield return new WaitForSeconds(0.6f);

            PlayerProgress progress = GameSession.Instance.Progress;
            int requiredTrust = chapterNumber == 4 && riskyDecision ? 76
                : chapterNumber == 5 && riskyDecision ? 99 : expectedTrust;
            int requiredMoney = chapterNumber == 4 && riskyDecision ? 750
                : chapterNumber == 5 && riskyDecision ? 250 : expectedMoney;
            int requiredReputation = chapterNumber == 4 && riskyDecision ? 50
                : chapterNumber == 5 && riskyDecision ? 64 : expectedReputation;
            int requiredProof = chapterNumber == 5 && riskyDecision ? 9 : expectedProof;
            if (chapterNumber == 6 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 550;
                requiredReputation = 77;
                requiredProof = 26;
            }
            int requiredDecision = riskyDecision ? 2 : 1;
            int requiredPower = chapterNumber == 6 && riskyDecision ? 20 : expectedPower;
            int requiredVolunteers = chapterNumber == 6 && riskyDecision ? 32 : expectedVolunteers;
            int requiredPressure = chapterNumber == 6 && riskyDecision ? 17 : expectedPressure;
            bool passed = mission.IsComplete
                && progress.publicTrust == requiredTrust
                && progress.money == requiredMoney
                && progress.reputation == requiredReputation
                && progress.caseProof == requiredProof
                && progress.politicalPower == requiredPower
                && progress.volunteers == requiredVolunteers
                && progress.oppositionPressure == requiredPressure
                && (chapterNumber != 4 || progress.rescueApproach == requiredDecision)
                && (chapterNumber != 5 || progress.hospitalApproach == requiredDecision)
                && (chapterNumber != 6 || progress.oppositionResponse == requiredDecision)
                && (PrototypeHud.Instance == null || !PrototypeHud.Instance.IsDecisionOpen);
            string marker = chapterNumber == 1
                ? "PROTOTYPE"
                : riskyDecision ? $"CHAPTER_{chapterNumber}_RISKY" : $"CHAPTER_{chapterNumber}";
            Debug.Log(passed
                ? $"{marker}_SMOKE_PASSED: trust={progress.publicTrust}, money={progress.money}, reputation={progress.reputation}"
                : $"{marker}_SMOKE_FAILED: complete={mission.IsComplete}, trust={progress.publicTrust}, money={progress.money}, reputation={progress.reputation}");
            Application.Quit(passed ? 0 : 3);
        }

        private static string ReadArgument(string[] arguments, string name)
        {
            int index = System.Array.IndexOf(arguments, name);
            return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : null;
        }
    }
}

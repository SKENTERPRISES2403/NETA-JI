using System.Collections.Generic;
using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class MissionController : MonoBehaviour
    {
        public static MissionController Instance { get; private set; }

        [SerializeField] private string missionTitle = "Ravivaar Ki Seva";
        [SerializeField] private string completionTitle = "SEVA COMPLETE";
        [SerializeField] private string completionMessage = "Ghat saaf hua. Public Trust badha.";
        [SerializeField] private int chapterNumber = 1;
        [SerializeField] private string nextChapterScene = string.Empty;
        [SerializeField] private string introTitle = "AZAD / 31 / SOCIAL WORKER";
        [SerializeField] private string introMessage = "Daraganj ka beta. Helpers Hand ka field volunteer.";
        [SerializeField] private List<MissionObjective> objectives = new List<MissionObjective>();
        [SerializeField] private List<string> objectiveLabels = new List<string>();
        [SerializeField] private List<int> milestoneSteps = new List<int>();
        [SerializeField] private List<string> milestoneTitles = new List<string>();
        [SerializeField] private List<string> milestoneMessages = new List<string>();
        private int currentIndex;

        public string MissionTitle => missionTitle;
        public string CurrentObjective => currentIndex < objectiveLabels.Count ? objectiveLabels[currentIndex] : "Mission complete";
        public MissionObjective CurrentObjectiveItem => currentIndex < objectives.Count ? objectives[currentIndex] : null;
        public bool IsComplete => currentIndex >= objectives.Count;
        public int ChapterNumber => chapterNumber;

        public void Configure(
            string title,
            List<MissionObjective> steps,
            List<string> labels,
            string completedTitle,
            string completedMessage)
        {
            missionTitle = title;
            objectives = steps;
            objectiveLabels = labels;
            completionTitle = completedTitle;
            completionMessage = completedMessage;
        }

        public void ConfigureMilestones(List<int> steps, List<string> titles, List<string> messages)
        {
            milestoneSteps = steps;
            milestoneTitles = titles;
            milestoneMessages = messages;
        }

        public void ConfigureChapter(int number, string nextScene)
        {
            chapterNumber = Mathf.Max(1, number);
            nextChapterScene = nextScene ?? string.Empty;
        }

        public void ConfigureIntro(string title, string message)
        {
            introTitle = title;
            introMessage = message;
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            currentIndex = Mathf.Clamp(GameSession.Instance?.GetMissionStep(chapterNumber) ?? 0, 0, objectives.Count);
            GameSession.Instance?.SetLastPlayedChapter(chapterNumber);
            for (int i = 0; i < currentIndex && i < objectives.Count; i++)
            {
                objectives[i]?.RestoreAsCompleted();
            }

            PrototypeHud.Instance?.RefreshMission();
            if (IsComplete)
            {
                PrototypeHud.Instance?.ShowChapterActions(nextChapterScene);
                return;
            }
            if (currentIndex == 0)
            {
                PrototypeHud.Instance?.ShowBanner(introTitle, introMessage);
            }
        }

        public bool IsCurrent(MissionObjective objective)
        {
            return !IsComplete && objectives[currentIndex] == objective;
        }

        public void Complete(MissionObjective objective)
        {
            if (!IsCurrent(objective))
            {
                return;
            }

            currentIndex++;
            GameSession.Instance?.SetMissionStep(chapterNumber, currentIndex);
            PrototypeHud.Instance?.RefreshMission();

            if (IsComplete)
            {
                GameSession.Instance?.CompleteChapter(chapterNumber);
                PrototypeAudio.Instance?.PlayCompletion();
                PrototypeHud.Instance?.ShowBanner(completionTitle, completionMessage);
                PrototypeHud.Instance?.ShowChapterActions(nextChapterScene);
                return;
            }

            for (int i = 0; i < milestoneSteps.Count; i++)
            {
                if (milestoneSteps[i] != currentIndex)
                {
                    continue;
                }

                string title = i < milestoneTitles.Count ? milestoneTitles[i] : "MISSION UPDATE";
                string message = i < milestoneMessages.Count ? milestoneMessages[i] : CurrentObjective;
                PrototypeAudio.Instance?.PlayMilestone();
                PrototypeHud.Instance?.ShowBanner(title, message);
                break;
            }
        }

        public void ResetMission(bool resetAllProgress = true)
        {
            currentIndex = 0;
            foreach (MissionObjective objective in objectives)
            {
                objective?.ResetObjective();
            }

            if (resetAllProgress)
            {
                GameSession.Instance?.ResetProgress();
            }
            else
            {
                GameSession.Instance?.ResetChapter(chapterNumber);
            }
            PrototypeHud.Instance?.HideChapterActions();
            PrototypeHud.Instance?.RefreshMission();
            PrototypeHud.Instance?.ShowBanner(introTitle, introMessage);
        }
    }
}

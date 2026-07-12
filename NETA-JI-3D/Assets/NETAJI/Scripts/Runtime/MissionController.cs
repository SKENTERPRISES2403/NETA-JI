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
        [SerializeField] private List<MissionObjective> objectives = new List<MissionObjective>();
        [SerializeField] private List<string> objectiveLabels = new List<string>();
        private int currentIndex;

        public string MissionTitle => missionTitle;
        public string CurrentObjective => currentIndex < objectiveLabels.Count ? objectiveLabels[currentIndex] : "Mission complete";
        public MissionObjective CurrentObjectiveItem => currentIndex < objectives.Count ? objectives[currentIndex] : null;
        public bool IsComplete => currentIndex >= objectives.Count;

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

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            currentIndex = Mathf.Clamp(GameSession.Instance?.Progress.missionStep ?? 0, 0, objectives.Count);
            for (int i = 0; i < currentIndex && i < objectives.Count; i++)
            {
                objectives[i]?.RestoreAsCompleted();
            }

            PrototypeHud.Instance?.RefreshMission();
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
            GameSession.Instance?.SetMissionStep(currentIndex);
            PrototypeHud.Instance?.RefreshMission();

            if (IsComplete)
            {
                PrototypeHud.Instance?.ShowBanner(completionTitle, completionMessage);
            }
        }

        public void ResetMission()
        {
            currentIndex = 0;
            foreach (MissionObjective objective in objectives)
            {
                objective?.ResetObjective();
            }

            GameSession.Instance?.ResetProgress();
            PrototypeHud.Instance?.RefreshMission();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class MissionController : MonoBehaviour
    {
        public static MissionController Instance { get; private set; }

        [SerializeField] private string missionTitle = "Ravivaar Ki Seva";
        [SerializeField] private List<MissionObjective> objectives = new List<MissionObjective>();
        [SerializeField] private List<string> objectiveLabels = new List<string>();
        private int currentIndex;

        public string MissionTitle => missionTitle;
        public string CurrentObjective => currentIndex < objectiveLabels.Count ? objectiveLabels[currentIndex] : "Mission complete";
        public MissionObjective CurrentObjectiveItem => currentIndex < objectives.Count ? objectives[currentIndex] : null;
        public bool IsComplete => currentIndex >= objectives.Count;

        public void Configure(string title, List<MissionObjective> steps, List<string> labels)
        {
            missionTitle = title;
            objectives = steps;
            objectiveLabels = labels;
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
                PrototypeHud.Instance?.ShowBanner("SEVA COMPLETE", "Ghat saaf hua. Public Trust badha.");
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

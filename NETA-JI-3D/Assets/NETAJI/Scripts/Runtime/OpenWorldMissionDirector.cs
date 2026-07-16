using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class OpenWorldMissionDirector : MonoBehaviour
    {
        public static OpenWorldMissionDirector Instance { get; private set; }

        [SerializeField] private AzadController player;
        [SerializeField] private StoryHubController storyHub;
        [SerializeField] private GameObject[] chapterRoots;

        private bool missionActive;
        private GameObject activeRoot;

        public AzadController Player => player;
        public bool MissionActive => missionActive;
        public MissionController ActiveMission => missionActive && activeRoot != null
            ? activeRoot.GetComponent<MissionController>()
            : null;

        public void Configure(
            AzadController playerValue,
            StoryHubController storyHubValue,
            params GameObject[] chapterRootValues)
        {
            player = playerValue;
            storyHub = storyHubValue;
            chapterRoots = chapterRootValues;
            DeactivateAllRoots();
        }

        private void DeactivateAllRoots()
        {
            if (chapterRoots == null)
            {
                return;
            }

            foreach (GameObject chapterRoot in chapterRoots)
            {
                chapterRoot?.SetActive(false);
            }
        }

        private void Awake()
        {
            Instance = this;
            DeactivateAllRoots();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public bool TryStartChapter(int chapter)
        {
            if (missionActive || !CanHostChapter(chapter))
            {
                return false;
            }

            missionActive = true;
            activeRoot = chapterRoots[chapter - 1];
            storyHub?.SetMissionLocked(true);
            FreeRoamMapHud.Instance?.SetInteractionPrompt(string.Empty);
            activeRoot.SetActive(true);
            player?.SetControlEnabled(true);
            GameSession.Instance?.SetLastPlayedChapter(chapter);
            return true;
        }

        public bool ContinueToNextChapter()
        {
            MissionController mission = ActiveMission;
            int nextChapter = mission != null ? mission.ChapterNumber + 1 : 0;
            if (mission == null || !mission.IsComplete || !CanHostChapter(nextChapter))
            {
                return false;
            }

            GameObject completedRoot = activeRoot;
            missionActive = false;
            activeRoot = null;
            completedRoot.SetActive(false);
            FreeRoamMapHud.Instance?.ClearMissionTarget();
            storyHub?.RefreshFromProgress();
            return TryStartChapter(nextChapter);
        }

        public bool CanHostChapter(int chapter)
        {
            int index = chapter - 1;
            return chapterRoots != null
                && index >= 0
                && index < chapterRoots.Length
                && chapterRoots[index] != null;
        }

        public void ExitCompletedMission()
        {
            MissionController mission = ActiveMission;
            if (mission == null || !mission.IsComplete)
            {
                return;
            }

            missionActive = false;
            GameObject completedRoot = activeRoot;
            activeRoot = null;
            completedRoot.SetActive(false);
            FreeRoamMapHud.Instance?.ClearMissionTarget();
            storyHub?.SetMissionLocked(false);
            storyHub?.RefreshFromProgress();
            if (player != null && storyHub != null)
            {
                player.Teleport(storyHub.ReturnSpawn, Quaternion.identity);
            }
            player?.SetControlEnabled(true);
        }
    }
}

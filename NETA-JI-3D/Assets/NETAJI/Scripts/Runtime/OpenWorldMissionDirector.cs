using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class OpenWorldMissionDirector : MonoBehaviour
    {
        public static OpenWorldMissionDirector Instance { get; private set; }

        [SerializeField] private AzadController player;
        [SerializeField] private StoryHubController storyHub;
        [SerializeField] private GameObject chapterOneRoot;

        private bool missionActive;

        public AzadController Player => player;
        public bool MissionActive => missionActive;
        public MissionController ActiveMission => missionActive && chapterOneRoot != null
            ? chapterOneRoot.GetComponent<MissionController>()
            : null;

        public void Configure(
            AzadController playerValue,
            StoryHubController storyHubValue,
            GameObject chapterOneRootValue)
        {
            player = playerValue;
            storyHub = storyHubValue;
            chapterOneRoot = chapterOneRootValue;
            if (chapterOneRoot != null)
            {
                chapterOneRoot.SetActive(false);
            }
        }

        private void Awake()
        {
            Instance = this;
            if (chapterOneRoot != null)
            {
                chapterOneRoot.SetActive(false);
            }
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
            if (missionActive || chapter != 1 || chapterOneRoot == null)
            {
                return false;
            }

            missionActive = true;
            storyHub?.SetMissionLocked(true);
            FreeRoamMapHud.Instance?.SetInteractionPrompt(string.Empty);
            chapterOneRoot.SetActive(true);
            player?.SetControlEnabled(true);
            GameSession.Instance?.SetLastPlayedChapter(chapter);
            return true;
        }

        public void ExitCompletedMission()
        {
            MissionController mission = ActiveMission;
            if (mission == null || !mission.IsComplete)
            {
                return;
            }

            missionActive = false;
            chapterOneRoot.SetActive(false);
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

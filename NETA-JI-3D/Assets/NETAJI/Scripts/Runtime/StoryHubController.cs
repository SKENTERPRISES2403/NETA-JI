using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype
{
    public static class StoryHubFlow
    {
        private const string ReturnToMissionKey = "neta-ji-return-to-story-hub";
        public static int AutomationStage { get; set; }

        public static void OpenAtMissionMarker()
        {
            PlayerPrefs.SetInt(ReturnToMissionKey, 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("FreeRoam");
        }

        public static bool ConsumeMissionMarkerArrival()
        {
            bool requested = PlayerPrefs.GetInt(ReturnToMissionKey, 0) == 1;
            if (requested)
            {
                PlayerPrefs.DeleteKey(ReturnToMissionKey);
                PlayerPrefs.Save();
            }
            return requested;
        }
    }

    [RequireComponent(typeof(Collider))]
    public sealed class StoryHubController : MonoBehaviour, IInteractable
    {
        public static StoryHubController Instance { get; private set; }

        [SerializeField] private AzadController player;
        [SerializeField] private Vector3 returnSpawn = new Vector3(34f, 0f, -188f);
        [SerializeField] private Vector3 returnFacing = Vector3.zero;

        private Texture2D overlayTexture;
        private Texture2D panelTexture;
        private Texture2D primaryTexture;
        private Texture2D secondaryTexture;
        private bool confirmationOpen;
        private bool loading;
        private int activeChapter = 1;
        private int completedChapters;
        private bool campaignComplete;

        public string Prompt => campaignComplete
            ? "Final chapter aur Vishwa Guru review dobara dekhein"
            : $"CH {activeChapter:00}  {StoryChapterCatalog.GetTitle(activeChapter)}";
        public bool CanInteract => !confirmationOpen && !loading;
        public bool ConfirmationOpen => confirmationOpen;
        public int ActiveChapter => activeChapter;
        public int CompletedChapters => completedChapters;
        public bool CampaignComplete => campaignComplete;
        public Vector3 ReturnSpawn => returnSpawn;

        public void Configure(AzadController playerValue, Vector3 spawnPosition, Vector3 facingEuler)
        {
            player = playerValue;
            returnSpawn = spawnPosition;
            returnFacing = facingEuler;
        }

        private void Awake()
        {
            Instance = this;
            overlayTexture = MakeTexture(new Color(0.005f, 0.018f, 0.024f, 0.82f));
            panelTexture = MakeTexture(new Color(0.01f, 0.055f, 0.065f, 0.985f));
            primaryTexture = MakeTexture(new Color(0.94f, 0.63f, 0.12f, 1f));
            secondaryTexture = MakeTexture(new Color(0.02f, 0.28f, 0.29f, 1f));
        }

        private void Start()
        {
            if (player == null)
            {
                player = FindFirstObjectByType<AzadController>();
            }
            if (GameSession.Instance != null)
            {
                GameSession.Instance.ProgressChanged += RefreshStatus;
            }
            RefreshStatus();

            if (player != null && StoryHubFlow.ConsumeMissionMarkerArrival())
            {
                player.Teleport(returnSpawn, Quaternion.Euler(returnFacing));
            }
        }

        private void OnDestroy()
        {
            if (GameSession.Instance != null)
            {
                GameSession.Instance.ProgressChanged -= RefreshStatus;
            }
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Update()
        {
            if (confirmationOpen && Input.GetKeyDown(KeyCode.Escape))
            {
                CloseConfirmation();
            }
            else if (confirmationOpen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
            {
                BeginMission();
            }
        }

        public void Interact(AzadController interactingPlayer)
        {
            if (!CanInteract)
            {
                return;
            }

            player = interactingPlayer != null ? interactingPlayer : player;
            confirmationOpen = true;
            player?.SetControlEnabled(false);
            FreeRoamMapHud.Instance?.SetInteractionPrompt(string.Empty);
        }

        public void OpenForAutomation(AzadController automationPlayer)
        {
            Interact(automationPlayer);
        }

        public void CancelForAutomation()
        {
            CloseConfirmation();
        }

        public void StartMissionForAutomation()
        {
            BeginMission();
        }

        private void RefreshStatus()
        {
            PlayerProgress progress = GameSession.Instance?.Progress;
            if (progress == null)
            {
                return;
            }

            completedChapters = 0;
            int firstIncomplete = 0;
            int unlocked = Mathf.Clamp(progress.highestUnlockedChapter, 1, StoryChapterCatalog.Count);
            for (int chapter = 1; chapter <= StoryChapterCatalog.Count; chapter++)
            {
                bool complete = GameSession.Instance.IsChapterComplete(chapter);
                if (complete)
                {
                    completedChapters++;
                }
                else if (firstIncomplete == 0 && chapter <= unlocked)
                {
                    firstIncomplete = chapter;
                }
            }

            campaignComplete = completedChapters >= StoryChapterCatalog.Count;
            activeChapter = campaignComplete
                ? StoryChapterCatalog.Count
                : firstIncomplete > 0
                    ? firstIncomplete
                    : Mathf.Clamp(progress.lastPlayedChapter, 1, unlocked);
            FreeRoamMapHud.Instance?.SetStoryStatus(
                activeChapter,
                completedChapters,
                StoryChapterCatalog.GetTitle(activeChapter),
                campaignComplete);
        }

        private void BeginMission()
        {
            if (loading)
            {
                return;
            }

            loading = true;
            confirmationOpen = false;
            GameSession.Instance?.SetLastPlayedChapter(activeChapter);
            SceneManager.LoadScene(StoryChapterCatalog.GetSceneName(activeChapter));
        }

        private void CloseConfirmation()
        {
            confirmationOpen = false;
            player?.SetControlEnabled(true);
        }

        private void OnGUI()
        {
            if (!confirmationOpen)
            {
                return;
            }

            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), overlayTexture);
            float width = Mathf.Min(680f, Screen.width - 32f);
            float height = Mathf.Min(280f, Screen.height - 28f);
            Rect panel = new Rect((Screen.width - width) * 0.5f, (Screen.height - height) * 0.5f, width, height);
            GUI.DrawTexture(panel, panelTexture);

            int baseSize = Mathf.Clamp(Mathf.RoundToInt(Screen.height * 0.045f), 16, 25);
            GUIStyle eyebrowStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.Max(12, baseSize - 5),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft
            };
            eyebrowStyle.normal.textColor = new Color(0.55f, 0.88f, 0.82f);
            GUIStyle titleStyle = new GUIStyle(eyebrowStyle)
            {
                fontSize = baseSize + 2,
                wordWrap = true
            };
            titleStyle.normal.textColor = new Color(1f, 0.76f, 0.20f);
            GUIStyle bodyStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.Max(13, baseSize - 4),
                wordWrap = true,
                alignment = TextAnchor.UpperLeft
            };
            bodyStyle.normal.textColor = Color.white;

            GUI.Label(new Rect(panel.x + 24f, panel.y + 16f, panel.width - 48f, 26f),
                campaignComplete ? "AZAD KA SAFAR COMPLETE" : $"STORY MISSION  /  CHAPTER {activeChapter:00} OF {StoryChapterCatalog.Count}", eyebrowStyle);
            GUI.Label(new Rect(panel.x + 24f, panel.y + 45f, panel.width - 48f, 42f), StoryChapterCatalog.GetTitle(activeChapter), titleStyle);
            string message = campaignComplete
                ? "Saare 24 chapters complete hain. Final public leadership review dobara khel sakte hain."
                : $"{completedChapters} chapters complete. Is mission se Azad ka agla seva chapter continue hoga.";
            GUI.Label(new Rect(panel.x + 24f, panel.y + 92f, panel.width - 48f, 62f), message, bodyStyle);

            float gap = 14f;
            float buttonWidth = (panel.width - 48f - gap) * 0.5f;
            float buttonHeight = 56f;
            float buttonY = panel.yMax - buttonHeight - 22f;
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = Mathf.Max(13, baseSize - 3),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            buttonStyle.normal.textColor = new Color(0.02f, 0.10f, 0.11f);
            buttonStyle.normal.background = primaryTexture;
            buttonStyle.hover.background = primaryTexture;
            Rect startRect = new Rect(panel.x + 24f, buttonY, buttonWidth, buttonHeight);
            if (GUI.Button(startRect, campaignComplete ? "FINAL CHAPTER REPLAY" : "MISSION SHURU", buttonStyle))
            {
                BeginMission();
            }

            GUIStyle cancelStyle = new GUIStyle(buttonStyle);
            cancelStyle.normal.textColor = Color.white;
            cancelStyle.normal.background = secondaryTexture;
            cancelStyle.hover.background = secondaryTexture;
            Rect cancelRect = new Rect(startRect.xMax + gap, buttonY, buttonWidth, buttonHeight);
            if (GUI.Button(cancelRect, "ABHI NAHI", cancelStyle))
            {
                CloseConfirmation();
            }
        }

        private static Texture2D MakeTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }
    }
}

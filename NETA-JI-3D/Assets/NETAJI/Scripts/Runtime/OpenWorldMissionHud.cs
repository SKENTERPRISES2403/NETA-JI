using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype
{
    public sealed class OpenWorldMissionHud : MonoBehaviour, IMissionPresentation
    {
        public static OpenWorldMissionHud Instance { get; private set; }

        private string interactionPrompt = string.Empty;
        private string dialogueSpeaker = string.Empty;
        private string dialogueText = string.Empty;
        private string bannerTitle = string.Empty;
        private string bannerSubtitle = string.Empty;
        private string nextChapterScene = string.Empty;
        private string decisionTitle = string.Empty;
        private string decisionMessage = string.Empty;
        private string decisionLeft = string.Empty;
        private string decisionRight = string.Empty;
        private float dialogueUntil;
        private float bannerUntil;
        private bool showChapterActions;
        private Action<int> decisionCallback;
        private Texture2D whiteTexture;
        private GUIStyle titleStyle;
        private GUIStyle bodyStyle;
        private GUIStyle smallStyle;
        private GUIStyle statStyle;

        public bool IsDecisionOpen => decisionCallback != null;
        public bool ChapterActionsVisible => showChapterActions;

        private void Awake()
        {
            whiteTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            whiteTexture.SetPixel(0, 0, Color.white);
            whiteTexture.Apply();
        }

        private void OnEnable()
        {
            Instance = this;
            MissionPresentation.Register(this);
        }

        private void OnDisable()
        {
            MissionPresentation.Unregister(this);
            FreeRoamMapHud.Instance?.ClearMissionTarget();
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void SetInteractionPrompt(string value)
        {
            interactionPrompt = value ?? string.Empty;
        }

        public void ShowDialogue(string speaker, string text)
        {
            dialogueSpeaker = speaker ?? string.Empty;
            dialogueText = text ?? string.Empty;
            dialogueUntil = Time.unscaledTime + Mathf.Clamp(3f + dialogueText.Length * 0.035f, 4f, 8f);
        }

        public void ShowBanner(string title, string subtitle)
        {
            bannerTitle = title ?? string.Empty;
            bannerSubtitle = subtitle ?? string.Empty;
            bannerUntil = Time.unscaledTime + 4.5f;
        }

        public void RefreshMission()
        {
            MissionObjective current = MissionController.Instance?.CurrentObjectiveItem;
            FreeRoamMapHud.Instance?.SetMissionTarget(current != null ? current.transform : null,
                MissionController.Instance?.CurrentObjective ?? string.Empty);
        }

        public void ShowChapterActions(string nextScene)
        {
            nextChapterScene = nextScene ?? string.Empty;
            showChapterActions = true;
            FreeRoamMapHud.Instance?.ClearMissionTarget();
        }

        public void HideChapterActions()
        {
            nextChapterScene = string.Empty;
            showChapterActions = false;
        }

        public void ShowDecision(
            string title,
            string message,
            string leftOption,
            string rightOption,
            Action<int> callback)
        {
            decisionTitle = title ?? string.Empty;
            decisionMessage = message ?? string.Empty;
            decisionLeft = leftOption ?? string.Empty;
            decisionRight = rightOption ?? string.Empty;
            decisionCallback = callback;
            interactionPrompt = string.Empty;
        }

        public void SelectDecisionForAutomation(int option)
        {
            if (IsDecisionOpen)
            {
                ResolveDecision(Mathf.Clamp(option, 1, 2));
            }
        }

        private void OnGUI()
        {
            if (FreeRoamMapHud.Instance?.IsFullMapOpen == true
                || StoryHubController.Instance?.ConfirmationOpen == true)
            {
                return;
            }

            EnsureStyles();
            DrawMissionCard();
            DrawBaseStats();

            if (!string.IsNullOrWhiteSpace(interactionPrompt) && Time.unscaledTime >= dialogueUntil && !IsDecisionOpen)
            {
                float width = Mathf.Min(480f, Screen.width - 42f);
                Rect prompt = new Rect((Screen.width - width) * 0.5f, Screen.height - 66f, width, 42f);
                DrawPanel(prompt, new Color(0.02f, 0.30f, 0.32f, 0.96f));
                GUI.Label(prompt, $"USE  /  {interactionPrompt}", statStyle);
            }

            if (Time.unscaledTime < dialogueUntil)
            {
                DrawDialogue();
            }

            if (Time.unscaledTime < bannerUntil && !IsDecisionOpen)
            {
                DrawBanner();
            }

            if (showChapterActions && Time.unscaledTime >= dialogueUntil && Time.unscaledTime >= bannerUntil)
            {
                DrawChapterActions();
            }

            if (IsDecisionOpen)
            {
                DrawDecision();
            }
        }

        private void DrawMissionCard()
        {
            float width = Mathf.Clamp(Screen.width * 0.39f, 292f, 390f);
            float height = Screen.height < 480f ? 94f : 108f;
            Rect card = new Rect(Screen.width - width - 16f, 14f, width, height);
            DrawPanel(card, new Color(0.01f, 0.055f, 0.065f, 0.97f));

            string mission = MissionController.Instance?.MissionTitle ?? "Prayagraj Seva Route";
            string objective = MissionController.Instance?.CurrentObjective ?? "Mission taiyaar ho raha hai";
            GUIStyle missionTitleStyle = new GUIStyle(titleStyle)
            {
                fontSize = mission.Length > 32 ? Mathf.Max(11, titleStyle.fontSize - 4) : titleStyle.fontSize,
                wordWrap = false,
                clipping = TextClipping.Clip
            };
            GUI.Label(new Rect(card.x + 14f, card.y + 8f, card.width - 28f, 22f), "STORY MISSION  /  PRAYAGRAJ", smallStyle);
            GUI.Label(new Rect(card.x + 14f, card.y + 29f, card.width - 28f, 27f), mission, missionTitleStyle);
            GUI.Label(new Rect(card.x + 14f, card.y + 54f, card.width - 28f, 24f), objective, bodyStyle);
            string route = GetRouteHint();
            if (!string.IsNullOrEmpty(route))
            {
                GUI.Label(new Rect(card.x + 14f, card.yMax - 24f, card.width - 28f, 19f), route, smallStyle);
            }
        }

        private void DrawBaseStats()
        {
            PlayerProgress progress = GameSession.Instance?.Progress;
            if (progress == null)
            {
                return;
            }

            float width = Mathf.Clamp(Screen.width * 0.39f, 292f, 390f);
            float top = 14f + (Screen.height < 480f ? 94f : 108f) + 8f;
            Rect stats = new Rect(Screen.width - width - 16f, top, width, 38f);
            DrawPanel(stats, new Color(0.01f, 0.055f, 0.065f, 0.95f));
            GUI.Label(stats,
                $"TRUST {progress.publicTrust}%   Rs {progress.money}   REP {progress.reputation}   PROOF {progress.caseProof}",
                statStyle);
        }

        private void DrawDialogue()
        {
            float width = Mathf.Min(720f, Screen.width - 36f);
            float height = Screen.height < 480f ? 92f : 108f;
            Rect panel = new Rect((Screen.width - width) * 0.5f, Screen.height - height - 22f, width, height);
            DrawPanel(panel, new Color(0.008f, 0.035f, 0.047f, 0.985f));
            GUI.Label(new Rect(panel.x + 18f, panel.y + 10f, panel.width - 36f, 24f), dialogueSpeaker.ToUpperInvariant(), titleStyle);
            GUI.Label(new Rect(panel.x + 18f, panel.y + 36f, panel.width - 36f, panel.height - 43f), dialogueText, bodyStyle);
        }

        private void DrawBanner()
        {
            float width = Mathf.Min(Screen.height < 480f ? 500f : 560f, Screen.width - 44f);
            float height = Screen.height < 480f ? 104f : 124f;
            float y = Screen.height < 480f
                ? Mathf.Max(164f, Screen.height - 92f - height - 28f)
                : Screen.height * 0.18f;
            Rect panel = new Rect((Screen.width - width) * 0.5f, y, width, height);
            DrawPanel(panel, new Color(0.94f, 0.64f, 0.12f, 0.98f));
            GUIStyle darkTitle = new GUIStyle(titleStyle) { alignment = TextAnchor.MiddleCenter };
            darkTitle.normal.textColor = new Color(0.02f, 0.10f, 0.11f);
            GUIStyle darkBody = new GUIStyle(bodyStyle) { alignment = TextAnchor.MiddleCenter };
            darkBody.normal.textColor = darkTitle.normal.textColor;
            GUI.Label(new Rect(panel.x + 14f, panel.y + 10f, panel.width - 28f, 32f), bannerTitle, darkTitle);
            GUI.Label(new Rect(panel.x + 14f, panel.y + 43f, panel.width - 28f, panel.height - 50f), bannerSubtitle, darkBody);
        }

        private void DrawChapterActions()
        {
            bool hasNext = !string.IsNullOrWhiteSpace(nextChapterScene);
            int count = hasNext ? 2 : 1;
            float gap = 12f;
            float buttonWidth = Mathf.Clamp((Screen.width - 44f - gap) / count, 210f, 300f);
            float totalWidth = buttonWidth * count + gap * (count - 1);
            float x = (Screen.width - totalWidth) * 0.5f;
            float y = Screen.height - 70f;

            Rect roam = new Rect(x, y, buttonWidth, 50f);
            DrawPanel(roam, new Color(0.02f, 0.30f, 0.32f, 0.98f));
            if (GUI.Button(roam, "PRAYAGRAJ ME GHUMEIN", statStyle))
            {
                OpenWorldMissionDirector.Instance?.ExitCompletedMission();
            }

            if (hasNext)
            {
                Rect next = new Rect(roam.xMax + gap, y, buttonWidth, 50f);
                DrawPanel(next, new Color(0.94f, 0.64f, 0.12f, 0.98f));
                GUIStyle darkButton = new GUIStyle(statStyle);
                darkButton.normal.textColor = new Color(0.02f, 0.10f, 0.11f);
                int nextChapter = (MissionController.Instance?.ChapterNumber ?? 1) + 1;
                if (GUI.Button(next, $"CHAPTER {nextChapter} SHURU", darkButton))
                {
                    if (OpenWorldMissionDirector.Instance?.ContinueToNextChapter() != true)
                    {
                        SceneManager.LoadScene(nextChapterScene);
                    }
                }
            }
        }

        private void DrawDecision()
        {
            DrawPanel(new Rect(0f, 0f, Screen.width, Screen.height), new Color(0.005f, 0.018f, 0.024f, 0.78f));
            float width = Mathf.Min(680f, Screen.width - 36f);
            float height = Mathf.Min(230f, Screen.height - 36f);
            Rect panel = new Rect((Screen.width - width) * 0.5f, (Screen.height - height) * 0.5f, width, height);
            DrawPanel(panel, new Color(0.01f, 0.045f, 0.06f, 0.99f));
            GUI.Label(new Rect(panel.x + 22f, panel.y + 15f, panel.width - 44f, 30f), decisionTitle, titleStyle);
            GUI.Label(new Rect(panel.x + 22f, panel.y + 49f, panel.width - 44f, 72f), decisionMessage, bodyStyle);
            float buttonWidth = (panel.width - 60f) * 0.5f;
            Rect left = new Rect(panel.x + 22f, panel.yMax - 72f, buttonWidth, 50f);
            Rect right = new Rect(left.xMax + 16f, left.y, buttonWidth, 50f);
            if (GUI.Button(left, decisionLeft, statStyle))
            {
                ResolveDecision(1);
            }
            if (GUI.Button(right, decisionRight, statStyle))
            {
                ResolveDecision(2);
            }
        }

        private void ResolveDecision(int option)
        {
            Action<int> callback = decisionCallback;
            decisionCallback = null;
            callback?.Invoke(option);
        }

        private string GetRouteHint()
        {
            MissionObjective objective = MissionController.Instance?.CurrentObjectiveItem;
            AzadController player = OpenWorldMissionDirector.Instance?.Player;
            Camera camera = Camera.main;
            if (objective == null || player == null || camera == null)
            {
                return string.Empty;
            }

            Vector3 localTarget = camera.transform.InverseTransformPoint(objective.transform.position);
            string direction = localTarget.z < -1.5f ? "BEHIND"
                : localTarget.x < -1.5f ? "LEFT"
                : localTarget.x > 1.5f ? "RIGHT"
                : "AHEAD";
            float distance = Vector3.Distance(player.transform.position, objective.transform.position);
            return $"ROUTE  {direction}  /  {distance:0} m  /  MAP MARKER ACTIVE";
        }

        private void EnsureStyles()
        {
            if (titleStyle != null)
            {
                return;
            }

            int baseSize = Mathf.Clamp(Mathf.RoundToInt(Screen.height / 45f), 15, 23);
            titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = baseSize,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                clipping = TextClipping.Clip
            };
            titleStyle.normal.textColor = new Color(1f, 0.77f, 0.20f);
            bodyStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.Max(13, baseSize - 3),
                alignment = TextAnchor.UpperLeft,
                wordWrap = true,
                clipping = TextClipping.Clip
            };
            bodyStyle.normal.textColor = Color.white;
            smallStyle = new GUIStyle(bodyStyle)
            {
                fontSize = Mathf.Max(11, baseSize - 5),
                fontStyle = FontStyle.Bold,
                wordWrap = false
            };
            smallStyle.normal.textColor = new Color(0.56f, 0.88f, 0.82f);
            statStyle = new GUIStyle(bodyStyle)
            {
                fontSize = Mathf.Max(12, baseSize - 4),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = false
            };
        }

        private void DrawPanel(Rect rect, Color color)
        {
            Color previous = GUI.color;
            GUI.color = color;
            GUI.DrawTexture(rect, whiteTexture, ScaleMode.StretchToFill);
            GUI.color = previous;
        }
    }
}

using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class PrototypeHud : MonoBehaviour
    {
        public static PrototypeHud Instance { get; private set; }

        private string interactionPrompt = string.Empty;
        private string dialogueSpeaker = string.Empty;
        private string dialogueText = string.Empty;
        private string bannerTitle = string.Empty;
        private string bannerSubtitle = string.Empty;
        private float dialogueUntil;
        private float bannerUntil;
        private GUIStyle titleStyle;
        private GUIStyle bodyStyle;
        private GUIStyle smallStyle;
        private GUIStyle statStyle;
        private Texture2D whiteTexture;

        private void Awake()
        {
            Instance = this;
            whiteTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            whiteTexture.SetPixel(0, 0, Color.white);
            whiteTexture.Apply();
        }

        public void SetInteractionPrompt(string value)
        {
            interactionPrompt = value;
        }

        public void ShowDialogue(string speaker, string text)
        {
            dialogueSpeaker = speaker;
            dialogueText = text;
            dialogueUntil = Time.unscaledTime + Mathf.Clamp(3f + text.Length * 0.035f, 4f, 8f);
        }

        public void ShowBanner(string title, string subtitle)
        {
            bannerTitle = title;
            bannerSubtitle = subtitle;
            bannerUntil = Time.unscaledTime + 4.5f;
        }

        public void RefreshMission()
        {
        }

        private void OnGUI()
        {
            EnsureStyles();
            DrawPanel(new Rect(18f, 16f, Mathf.Min(440f, Screen.width * 0.52f), 104f), new Color(0.015f, 0.08f, 0.10f, 0.88f));
            GUI.Label(new Rect(34f, 26f, 380f, 30f), "NETA JI  /  PRAYAGRAJ", titleStyle);
            string mission = MissionController.Instance != null ? MissionController.Instance.MissionTitle : "Prototype 1";
            string objective = MissionController.Instance != null ? MissionController.Instance.CurrentObjective : "Loading seva route...";
            GUI.Label(new Rect(34f, 58f, Mathf.Min(400f, Screen.width * 0.47f), 22f), mission, smallStyle);
            GUI.Label(new Rect(34f, 80f, Mathf.Min(400f, Screen.width * 0.47f), 30f), objective, bodyStyle);

            PlayerProgress progress = GameSession.Instance?.Progress;
            if (progress != null)
            {
                float width = Mathf.Min(420f, Screen.width * 0.42f);
                Rect statsRect = new Rect(Screen.width - width - 18f, 16f, width, 52f);
                DrawPanel(statsRect, new Color(0.015f, 0.08f, 0.10f, 0.84f));
                GUI.Label(new Rect(statsRect.x + 16f, statsRect.y + 14f, statsRect.width - 32f, 28f),
                    $"TRUST {progress.publicTrust}%     FUNDS Rs {progress.money}     REP {progress.reputation}", statStyle);
            }

            if (!string.IsNullOrWhiteSpace(interactionPrompt) && Time.unscaledTime >= dialogueUntil)
            {
                float width = Mathf.Min(460f, Screen.width - 48f);
                Rect promptRect = new Rect((Screen.width - width) * 0.5f, Screen.height - 94f, width, 48f);
                DrawPanel(promptRect, new Color(0.02f, 0.30f, 0.32f, 0.93f));
                GUI.Label(promptRect, $"E / HELP   {interactionPrompt}", statStyle);
            }

            if (Time.unscaledTime < dialogueUntil)
            {
                float width = Mathf.Min(720f, Screen.width - 36f);
                Rect dialogueRect = new Rect((Screen.width - width) * 0.5f, Screen.height - 188f, width, 100f);
                DrawPanel(dialogueRect, new Color(0.015f, 0.05f, 0.07f, 0.96f));
                GUI.Label(new Rect(dialogueRect.x + 18f, dialogueRect.y + 12f, dialogueRect.width - 36f, 24f), dialogueSpeaker.ToUpperInvariant(), titleStyle);
                GUI.Label(new Rect(dialogueRect.x + 18f, dialogueRect.y + 40f, dialogueRect.width - 36f, 50f), dialogueText, bodyStyle);
            }

            if (Time.unscaledTime < bannerUntil)
            {
                float width = Mathf.Min(560f, Screen.width - 44f);
                Rect bannerRect = new Rect((Screen.width - width) * 0.5f, Screen.height * 0.18f, width, 104f);
                DrawPanel(bannerRect, new Color(0.93f, 0.61f, 0.10f, 0.96f));
                GUIStyle darkTitle = new GUIStyle(titleStyle) { alignment = TextAnchor.MiddleCenter };
                darkTitle.normal.textColor = new Color(0.03f, 0.11f, 0.12f);
                GUIStyle darkBody = new GUIStyle(bodyStyle) { alignment = TextAnchor.MiddleCenter };
                darkBody.normal.textColor = new Color(0.03f, 0.11f, 0.12f);
                GUI.Label(new Rect(bannerRect.x + 12f, bannerRect.y + 16f, bannerRect.width - 24f, 34f), bannerTitle, darkTitle);
                GUI.Label(new Rect(bannerRect.x + 12f, bannerRect.y + 53f, bannerRect.width - 24f, 36f), bannerSubtitle, darkBody);
            }
        }

        private void EnsureStyles()
        {
            if (titleStyle != null)
            {
                return;
            }

            int baseSize = Mathf.Clamp(Mathf.RoundToInt(Screen.height / 45f), 16, 24);
            titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = baseSize,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft
            };
            titleStyle.normal.textColor = new Color(1f, 0.77f, 0.20f);

            bodyStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.Max(14, baseSize - 4),
                wordWrap = true,
                alignment = TextAnchor.UpperLeft
            };
            bodyStyle.normal.textColor = Color.white;

            smallStyle = new GUIStyle(bodyStyle)
            {
                fontSize = Mathf.Max(12, baseSize - 6),
                fontStyle = FontStyle.Bold
            };
            smallStyle.normal.textColor = new Color(0.56f, 0.88f, 0.82f);

            statStyle = new GUIStyle(bodyStyle)
            {
                fontSize = Mathf.Max(13, baseSize - 5),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
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

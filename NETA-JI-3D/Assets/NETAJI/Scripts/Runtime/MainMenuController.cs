using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetaJi.Prototype
{
    public sealed class MainMenuController : MonoBehaviour
    {
        [SerializeField] private string gameplaySceneName = "Prototype01";
        [SerializeField] private string chapterTwoSceneName = "Chapter02";
        [SerializeField] private string chapterThreeSceneName = "Chapter03";
        [SerializeField] private string chapterFourSceneName = "Chapter04";
        [SerializeField] private string chapterFiveSceneName = "Chapter05";
        [SerializeField] private string chapterSixSceneName = "Chapter06";
        [SerializeField] private string chapterSevenSceneName = "Chapter07";
        [SerializeField] private string chapterEightSceneName = "Chapter08";
        [SerializeField] private string chapterNineSceneName = "Chapter09";
        [SerializeField] private string chapterTenSceneName = "Chapter10";
        [SerializeField] private string chapterElevenSceneName = "Chapter11";

        private Texture2D whiteTexture;
        private Texture2D primaryTexture;
        private Texture2D secondaryTexture;
        private GUIStyle titleStyle;
        private GUIStyle subtitleStyle;
        private GUIStyle bodyStyle;
        private GUIStyle buttonStyle;
        private GUIStyle secondaryButtonStyle;
        private bool storyOpen;
        private bool chaptersOpen;

        private void Awake()
        {
            whiteTexture = MakeTexture(Color.white);
            primaryTexture = MakeTexture(new Color(0.93f, 0.61f, 0.10f, 0.96f));
            secondaryTexture = MakeTexture(new Color(0.02f, 0.25f, 0.27f, 0.94f));
            Application.targetFrameRate = 60;
            Application.runInBackground = true;
        }

        private void Start()
        {
            string[] arguments = Environment.GetCommandLineArgs();
            if (Array.IndexOf(arguments, "-menuSmoke") >= 0)
            {
                StartCoroutine(RunMenuSmoke(arguments));
                return;
            }

            if (Array.IndexOf(arguments, "-prototypeSmoke") >= 0)
            {
                SceneManager.LoadScene(gameplaySceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter2Smoke") >= 0)
            {
                SceneManager.LoadScene(chapterTwoSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter3Smoke") >= 0)
            {
                SceneManager.LoadScene(chapterThreeSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter4Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyDecisionSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterFourSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter5Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyHospitalSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterFiveSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter6Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyOppositionSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterSixSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter7Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyCampaignSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterSevenSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter8Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyGovernanceSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterEightSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter9Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyExpansionSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterNineSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter10Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyAssemblyCampaignSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterTenSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter11Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyLegislativeSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterElevenSceneName);
            }
        }

        private IEnumerator RunMenuSmoke(string[] arguments)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            if (GameSession.Instance != null)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(46, 200, 26);
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                GameSession.Instance.CompleteChapter(4);
                GameSession.Instance.CompleteChapter(5);
                GameSession.Instance.CompleteChapter(6);
                GameSession.Instance.CompleteChapter(7);
                GameSession.Instance.CompleteChapter(8);
                GameSession.Instance.CompleteChapter(9);
                GameSession.Instance.CompleteChapter(10);
            }
            yield return new WaitForSeconds(1.2f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "menu-start.png"));
            yield return new WaitForSeconds(0.8f);
            chaptersOpen = true;
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "menu-chapters.png"));
            yield return new WaitForSeconds(0.8f);
            chaptersOpen = false;
            storyOpen = true;
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "menu-story.png"));
            yield return new WaitForSeconds(1f);
            Debug.Log("MENU_SMOKE_PASSED");
            Application.Quit(0);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                storyOpen = false;
                chaptersOpen = false;
            }
        }

        private void OnGUI()
        {
            EnsureStyles();
            float scale = Mathf.Clamp(Screen.height / 720f, 0.68f, 1.15f);
            float titleX = 40f * scale;
            float titleY = 38f * scale;

            DrawPanel(new Rect(0f, 0f, Screen.width * 0.57f, Screen.height), new Color(0.01f, 0.055f, 0.065f, 0.84f));
            GUI.Label(new Rect(titleX, titleY, Screen.width * 0.52f, 78f * scale), "NETA JI", titleStyle);
            GUI.Label(new Rect(titleX, titleY + 72f * scale, Screen.width * 0.50f, 36f * scale), "RISE OF A LEADER", subtitleStyle);
            GUI.Label(
                new Rect(titleX, titleY + 132f * scale, Screen.width * 0.48f, 100f * scale),
                "Prayagraj. Ek social worker. Chhote kaam, saaf niyat, aur system badalne ka lamba safar.",
                bodyStyle);

            float buttonWidth = Mathf.Clamp(Screen.width * 0.27f, 210f, 320f);
            float buttonHeight = Mathf.Clamp(Screen.height * 0.105f, 46f, 64f);
            float buttonX = Screen.width - buttonWidth - 34f * scale;
            float buttonY = Screen.height * 0.36f;
            float nextButtonY = buttonY;

            if (GameSession.HasSave)
            {
                if (GUI.Button(new Rect(buttonX, nextButtonY, buttonWidth, buttonHeight), "SEVA JAARI RAKHEIN", buttonStyle))
                {
                    LoadChapter(GameSession.LastPlayedChapter, false);
                }
                nextButtonY += buttonHeight + 12f;
            }

            if (GUI.Button(new Rect(buttonX, nextButtonY, buttonWidth, buttonHeight), "NAYA SAFAR", buttonStyle))
            {
                LoadChapter(1, true);
            }
            nextButtonY += buttonHeight + 12f;

            if (GUI.Button(new Rect(buttonX, nextButtonY, buttonWidth, buttonHeight), "CHAPTER CHUNEIN", secondaryButtonStyle))
            {
                chaptersOpen = true;
            }

            GUI.Label(
                new Rect(titleX, Screen.height - 52f * scale, Screen.width * 0.52f, 34f * scale),
                "Fictional story  /  No real party or leader  /  Prototype " + Application.version,
                subtitleStyle);

            if (storyOpen)
            {
                DrawStoryPanel(scale);
            }
            else if (chaptersOpen)
            {
                DrawChapterPanel();
            }
        }

        private void DrawChapterPanel()
        {
            float width = Mathf.Min(826f, Screen.width - 18f);
            float height = Mathf.Min(360f, Screen.height - 18f);
            Rect panel = new Rect((Screen.width - width) * 0.5f, (Screen.height - height) * 0.5f, width, height);
            DrawPanel(panel, new Color(0.01f, 0.055f, 0.065f, 0.98f));
            GUI.Label(new Rect(panel.x + 24f, panel.y + 18f, panel.width - 48f, 46f), "AZAD KA SAFAR", subtitleStyle);

            const float cardGap = 10f;
            float cardWidth = (panel.width - 48f - cardGap * 5f) / 6f;
            float cardHeight = 70f;
            float firstX = panel.x + 24f;
            float firstY = panel.y + 62f;
            DrawChapterCard(new Rect(firstX, firstY, cardWidth, cardHeight), 1, "GHAT SE GHAR TAK");
            DrawChapterCard(new Rect(firstX + cardWidth + cardGap, firstY, cardWidth, cardHeight), 2, "SHAAM KI PAATHSHALA");
            DrawChapterCard(new Rect(firstX + (cardWidth + cardGap) * 2f, firstY, cardWidth, cardHeight), 3, "SANDHYA KAHAN HAI");
            DrawChapterCard(new Rect(firstX + (cardWidth + cardGap) * 3f, firstY, cardWidth, cardHeight), 4, "OPERATION UMEED");
            DrawChapterCard(new Rect(firstX + (cardWidth + cardGap) * 4f, firstY, cardWidth, cardHeight), 5, "DAWA KA SACH");
            DrawChapterCard(new Rect(firstX + (cardWidth + cardGap) * 5f, firstY, cardWidth, cardHeight), 6, "SEVA SE SIYASAT");

            float secondY = firstY + cardHeight + 12f;
            float secondRowWidth = cardWidth * 5f + cardGap * 4f;
            float secondX = panel.x + (panel.width - secondRowWidth) * 0.5f;
            DrawChapterCard(new Rect(secondX, secondY, cardWidth, cardHeight), 7, "WARD KA FAISLA");
            DrawChapterCard(new Rect(secondX + cardWidth + cardGap, secondY, cardWidth, cardHeight), 8, "PEHLE 100 DIN");
            DrawChapterCard(new Rect(secondX + (cardWidth + cardGap) * 2f, secondY, cardWidth, cardHeight), 9, "VIDHANSABHA KI RAAH");
            DrawChapterCard(new Rect(secondX + (cardWidth + cardGap) * 3f, secondY, cardWidth, cardHeight), 10, "JANATA KA MANDATE");
            DrawChapterCard(new Rect(secondX + (cardWidth + cardGap) * 4f, secondY, cardWidth, cardHeight), 11, "JANATA KA MLA");

            GUI.Label(
                new Rect(panel.x + 24f, panel.y + 224f, panel.width - 48f, 44f),
                "Har chapter ke decisions aur seva rewards same local profile mein save hote hain.",
                bodyStyle);
            if (GUI.Button(new Rect(panel.x + 24f, panel.y + panel.height - 62f, 190f, 42f), "AZAD KI KAHANI", secondaryButtonStyle))
            {
                chaptersOpen = false;
                storyOpen = true;
            }
            if (GUI.Button(new Rect(panel.x + panel.width - 154f, panel.y + panel.height - 62f, 130f, 42f), "WAPAS", secondaryButtonStyle))
            {
                chaptersOpen = false;
            }
        }

        private void DrawChapterCard(Rect rect, int chapterNumber, string title)
        {
            if (GameSession.HighestUnlockedChapter >= chapterNumber)
            {
                if (GUI.Button(rect, $"CHAPTER {chapterNumber}\n{title}", buttonStyle))
                {
                    LoadChapter(chapterNumber, false);
                }
                return;
            }

            DrawPanel(rect, new Color(0.12f, 0.16f, 0.17f, 0.96f));
            GUI.Label(rect, $"CHAPTER {chapterNumber}\nLOCKED", secondaryButtonStyle);
        }

        private void DrawStoryPanel(float scale)
        {
            float width = Mathf.Min(700f, Screen.width - 42f);
            float height = Mathf.Min(320f, Screen.height - 36f);
            Rect panel = new Rect((Screen.width - width) * 0.5f, (Screen.height - height) * 0.5f, width, height);
            DrawPanel(panel, new Color(0.01f, 0.055f, 0.065f, 0.98f));
            GUI.Label(new Rect(panel.x + 24f, panel.y + 20f, panel.width - 48f, 54f), "AZAD  /  31  /  SOCIAL WORKER", subtitleStyle);
            GUI.Label(
                new Rect(panel.x + 24f, panel.y + 78f, panel.width - 48f, panel.height - 150f),
                "Daraganj ka Azad apne shaheed fauji pita se imaandari aur discipline seekh kar bada hua. Shanti, Sandhya aur Helpers Hand ke saath woh paperwork, education, safai aur public service mein logon ki madad karta hai. Yeh kahani siyasat se pehle seva ko samajhne se shuru hoti hai.",
                bodyStyle);
            if (GUI.Button(new Rect(panel.x + panel.width - 154f, panel.y + panel.height - 62f, 130f, 42f), "WAPAS", secondaryButtonStyle))
            {
                storyOpen = false;
            }
        }

        private void LoadChapter(int chapterNumber, bool reset)
        {
            if (reset)
            {
                GameSession.DeleteSave();
            }

            string sceneName = gameplaySceneName;
            if (chapterNumber == 2)
            {
                sceneName = chapterTwoSceneName;
            }
            else if (chapterNumber == 3)
            {
                sceneName = chapterThreeSceneName;
            }
            else if (chapterNumber == 4)
            {
                sceneName = chapterFourSceneName;
            }
            else if (chapterNumber == 5)
            {
                sceneName = chapterFiveSceneName;
            }
            else if (chapterNumber == 6)
            {
                sceneName = chapterSixSceneName;
            }
            else if (chapterNumber == 7)
            {
                sceneName = chapterSevenSceneName;
            }
            else if (chapterNumber == 8)
            {
                sceneName = chapterEightSceneName;
            }
            else if (chapterNumber == 9)
            {
                sceneName = chapterNineSceneName;
            }
            else if (chapterNumber == 10)
            {
                sceneName = chapterTenSceneName;
            }
            else if (chapterNumber == 11)
            {
                sceneName = chapterElevenSceneName;
            }
            SceneManager.LoadScene(sceneName);
        }

        private void EnsureStyles()
        {
            if (titleStyle != null)
            {
                return;
            }

            int titleSize = Mathf.Clamp(Mathf.RoundToInt(Screen.height * 0.105f), 38, 74);
            titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = titleSize,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft
            };
            titleStyle.normal.textColor = new Color(1f, 0.73f, 0.16f);

            subtitleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.Clamp(titleSize / 3, 13, 22),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft
            };
            subtitleStyle.normal.textColor = new Color(0.61f, 0.88f, 0.82f);

            bodyStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.Clamp(titleSize / 3, 14, 22),
                wordWrap = true,
                alignment = TextAnchor.UpperLeft
            };
            bodyStyle.normal.textColor = Color.white;

            buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = Mathf.Clamp(titleSize / 3, 14, 21),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true
            };
            buttonStyle.normal.background = primaryTexture;
            buttonStyle.hover.background = primaryTexture;
            buttonStyle.active.background = secondaryTexture;
            buttonStyle.normal.textColor = new Color(0.02f, 0.10f, 0.11f);

            secondaryButtonStyle = new GUIStyle(buttonStyle);
            secondaryButtonStyle.normal.background = secondaryTexture;
            secondaryButtonStyle.hover.background = secondaryTexture;
            secondaryButtonStyle.active.background = primaryTexture;
            secondaryButtonStyle.normal.textColor = Color.white;
        }

        private void DrawPanel(Rect rect, Color color)
        {
            Color previous = GUI.color;
            GUI.color = color;
            GUI.DrawTexture(rect, whiteTexture, ScaleMode.StretchToFill);
            GUI.color = previous;
        }

        private static Texture2D MakeTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }

        private static string ReadArgument(string[] arguments, string name)
        {
            int index = Array.IndexOf(arguments, name);
            return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : null;
        }
    }
}

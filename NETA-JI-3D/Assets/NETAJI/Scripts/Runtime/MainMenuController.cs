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
        }

        private IEnumerator RunMenuSmoke(string[] arguments)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
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
            float width = Mathf.Min(700f, Screen.width - 42f);
            float height = Mathf.Min(340f, Screen.height - 30f);
            Rect panel = new Rect((Screen.width - width) * 0.5f, (Screen.height - height) * 0.5f, width, height);
            DrawPanel(panel, new Color(0.01f, 0.055f, 0.065f, 0.98f));
            GUI.Label(new Rect(panel.x + 24f, panel.y + 18f, panel.width - 48f, 46f), "AZAD KA SAFAR", subtitleStyle);

            float cardWidth = (panel.width - 64f) * 0.5f;
            Rect chapterOneRect = new Rect(panel.x + 24f, panel.y + 74f, cardWidth, 92f);
            if (GUI.Button(chapterOneRect, "CHAPTER 1\nGHAT SE GHAR TAK", buttonStyle))
            {
                LoadChapter(1, false);
            }

            Rect chapterTwoRect = new Rect(chapterOneRect.xMax + 16f, chapterOneRect.y, cardWidth, 92f);
            if (GameSession.HighestUnlockedChapter >= 2)
            {
                if (GUI.Button(chapterTwoRect, "CHAPTER 2\nSHAAM KI PAATHSHALA", buttonStyle))
                {
                    LoadChapter(2, false);
                }
            }
            else
            {
                DrawPanel(chapterTwoRect, new Color(0.12f, 0.16f, 0.17f, 0.96f));
                GUI.Label(chapterTwoRect, "CHAPTER 2\nLOCKED - COMPLETE CHAPTER 1", secondaryButtonStyle);
            }

            GUI.Label(
                new Rect(panel.x + 24f, panel.y + 184f, panel.width - 48f, 58f),
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

            SceneManager.LoadScene(chapterNumber == 2 ? chapterTwoSceneName : gameplaySceneName);
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
                alignment = TextAnchor.MiddleCenter
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

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
        [SerializeField] private string chapterTwelveSceneName = "Chapter12";
        [SerializeField] private string chapterThirteenSceneName = "Chapter13";
        [SerializeField] private string chapterFourteenSceneName = "Chapter14";
        [SerializeField] private string chapterFifteenSceneName = "Chapter15";
        [SerializeField] private string chapterSixteenSceneName = "Chapter16";
        [SerializeField] private string chapterSeventeenSceneName = "Chapter17";
        [SerializeField] private string chapterEighteenSceneName = "Chapter18";
        [SerializeField] private string chapterNineteenSceneName = "Chapter19";
        [SerializeField] private string chapterTwentySceneName = "Chapter20";
        [SerializeField] private string chapterTwentyOneSceneName = "Chapter21";
        [SerializeField] private string chapterTwentyTwoSceneName = "Chapter22";
        [SerializeField] private string chapterTwentyThreeSceneName = "Chapter23";

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
            else if (Array.IndexOf(arguments, "-chapter12Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyDistrictSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterTwelveSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter13Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyStateExpansionSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterThirteenSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter14Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyStateLeadershipSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterFourteenSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter15Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyStateElectionSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterFifteenSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter16Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyCmGovernanceSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterSixteenSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter17Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyStateReformSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterSeventeenSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter18Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyNationalExpansionSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterEighteenSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter19Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyFirstNationalElectionSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterNineteenSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter20Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyOppositionTermSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterTwentySceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter21Smoke") >= 0
                || Array.IndexOf(arguments, "-riskySecondNationalElectionSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterTwentyOneSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter22Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyPrimeMinisterGovernanceSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterTwentyTwoSceneName);
            }
            else if (Array.IndexOf(arguments, "-chapter23Smoke") >= 0
                || Array.IndexOf(arguments, "-riskyNationalDevelopmentSmoke") >= 0)
            {
                SceneManager.LoadScene(chapterTwentyThreeSceneName);
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
                GameSession.Instance.CompleteChapter(11);
                GameSession.Instance.CompleteChapter(12);
                GameSession.Instance.CompleteChapter(13);
                GameSession.Instance.CompleteChapter(14);
                GameSession.Instance.CompleteChapter(15);
                GameSession.Instance.CompleteChapter(16);
                GameSession.Instance.CompleteChapter(17);
                GameSession.Instance.CompleteChapter(18);
                GameSession.Instance.CompleteChapter(19);
                GameSession.Instance.CompleteChapter(20);
                GameSession.Instance.CompleteChapter(21);
                GameSession.Instance.CompleteChapter(22);
            }
            yield return new WaitForSeconds(1.2f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "menu-start.png"));
            yield return new WaitForSeconds(0.8f);
            chaptersOpen = true;
            yield return new WaitForSeconds(0.35f);
            ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, "menu-chapters.png"));
            yield return new WaitForSeconds(0.8f);
            chaptersOpen = false;
            storyOpen = true;
            yield return new WaitForSeconds(0.35f);
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
            float width = Mathf.Min(1020f, Screen.width - 18f);
            float height = Mathf.Min(520f, Screen.height - 18f);
            Rect panel = new Rect((Screen.width - width) * 0.5f, (Screen.height - height) * 0.5f, width, height);
            DrawPanel(panel, new Color(0.01f, 0.055f, 0.065f, 0.98f));
            GUI.Label(new Rect(panel.x + 24f, panel.y + 18f, panel.width - 48f, 46f), "AZAD KA SAFAR", subtitleStyle);

            string[] chapterTitles =
            {
                "GHAT SE GHAR TAK", "SHAAM KI PAATHSHALA", "SANDHYA KAHAN HAI", "OPERATION UMEED",
                "DAWA KA SACH", "SEVA SE SIYASAT", "WARD KA FAISLA", "PEHLE 100 DIN",
                "VIDHANSABHA KI RAAH", "JANATA KA MANDATE", "JANATA KA MLA", "ZILA SANGATHAN",
                "PRADESH KI DASTAK", "PRADESH KA NETRUTVA", "PRADESH KA JANADESH",
                "CM KE PEHLE 100 DIN", "BADLAV KE PAANCH SAAL", "DESH BHAR KA SAATH",
                "RASHTRIYA CHUNAV", "HAAR KE BAAD HIMMAT", "PM KA JANADESH", "PM KE PEHLE 100 DIN",
                "DESH KA BADLAV"
            };
            float cardGap = 10f;
            bool compactGrid = chapterTitles.Length > 15;
            int cardColumns = chapterTitles.Length == 21 ? 7 : chapterTitles.Length > 21 ? 6 : chapterTitles.Length > 16 ? 5 : compactGrid ? 4 : chapterTitles.Length > 14 ? 5 : 7;
            float cardHeight = compactGrid
                ? chapterTitles.Length > 20
                    ? panel.height >= 470f ? 72f : 64f
                    : panel.height >= 470f ? 64f : 48f
                : chapterTitles.Length > 14 ? 55f : 70f;
            float rowGap = compactGrid
                ? chapterTitles.Length > 20
                    ? panel.height >= 470f ? 8f : 6f
                    : panel.height >= 470f ? 8f : 6f
                : chapterTitles.Length > 14 ? 8f : 12f;
            float cardWidth = (panel.width - 48f - cardGap * (cardColumns - 1)) / cardColumns;
            float firstX = panel.x + 24f;
            float firstY = panel.y + (compactGrid ? 56f : 62f);
            for (int index = 0; index < chapterTitles.Length; index++)
            {
                int column = index % cardColumns;
                int row = index / cardColumns;
                Rect cardRect = new Rect(
                    firstX + column * (cardWidth + cardGap),
                    firstY + row * (cardHeight + rowGap),
                    cardWidth,
                    cardHeight);
                DrawChapterCard(cardRect, index + 1, chapterTitles[index]);
            }

            int cardRows = Mathf.CeilToInt(chapterTitles.Length / (float)cardColumns);
            float infoY = firstY + cardRows * (cardHeight + rowGap) + 2f;
            GUI.Label(
                new Rect(panel.x + 24f, infoY, panel.width - 48f, 36f),
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
            GUIStyle chapterStyle = new GUIStyle(buttonStyle)
            {
                fontSize = Mathf.Clamp(
                    Mathf.RoundToInt(Mathf.Min(rect.width * 0.08f, rect.height * 0.25f)),
                    11,
                    Mathf.Min(16, buttonStyle.fontSize))
            };
            if (GameSession.HighestUnlockedChapter >= chapterNumber)
            {
                if (GUI.Button(rect, $"CH {chapterNumber:00}\n{title}", chapterStyle))
                {
                    LoadChapter(chapterNumber, false);
                }
                return;
            }

            DrawPanel(rect, new Color(0.12f, 0.16f, 0.17f, 0.96f));
            GUIStyle lockedStyle = new GUIStyle(chapterStyle);
            lockedStyle.normal.textColor = Color.white;
            GUI.Label(rect, $"CHAPTER {chapterNumber}\nLOCKED", lockedStyle);
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
            else if (chapterNumber == 12)
            {
                sceneName = chapterTwelveSceneName;
            }
            else if (chapterNumber == 13)
            {
                sceneName = chapterThirteenSceneName;
            }
            else if (chapterNumber == 14)
            {
                sceneName = chapterFourteenSceneName;
            }
            else if (chapterNumber == 15)
            {
                sceneName = chapterFifteenSceneName;
            }
            else if (chapterNumber == 16)
            {
                sceneName = chapterSixteenSceneName;
            }
            else if (chapterNumber == 17)
            {
                sceneName = chapterSeventeenSceneName;
            }
            else if (chapterNumber == 18)
            {
                sceneName = chapterEighteenSceneName;
            }
            else if (chapterNumber == 19)
            {
                sceneName = chapterNineteenSceneName;
            }
            else if (chapterNumber == 20)
            {
                sceneName = chapterTwentySceneName;
            }
            else if (chapterNumber == 21)
            {
                sceneName = chapterTwentyOneSceneName;
            }
            else if (chapterNumber == 22)
            {
                sceneName = chapterTwentyTwoSceneName;
            }
            else if (chapterNumber == 23)
            {
                sceneName = chapterTwentyThreeSceneName;
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

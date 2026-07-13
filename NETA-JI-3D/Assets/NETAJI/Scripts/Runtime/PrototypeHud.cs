using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private bool showChapterActions;
        private string nextChapterScene = string.Empty;
        private string decisionTitle = string.Empty;
        private string decisionMessage = string.Empty;
        private string decisionLeft = string.Empty;
        private string decisionRight = string.Empty;
        private Action<int> decisionCallback;
        private GUIStyle titleStyle;
        private GUIStyle bodyStyle;
        private GUIStyle smallStyle;
        private GUIStyle statStyle;
        private Texture2D whiteTexture;
        private AzadController routePlayer;
        private Camera routeCamera;
        public bool IsDecisionOpen => decisionCallback != null;

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

        public void ShowChapterActions(string nextScene)
        {
            nextChapterScene = nextScene ?? string.Empty;
            showChapterActions = true;
        }

        public void HideChapterActions()
        {
            showChapterActions = false;
            nextChapterScene = string.Empty;
        }

        public void ShowDecision(string title, string message, string leftOption, string rightOption, Action<int> callback)
        {
            decisionTitle = title;
            decisionMessage = message;
            decisionLeft = leftOption;
            decisionRight = rightOption;
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
            EnsureStyles();
            DrawPanel(new Rect(18f, 16f, Mathf.Min(440f, Screen.width * 0.52f), 124f), new Color(0.015f, 0.08f, 0.10f, 0.97f));
            GUI.Label(new Rect(34f, 26f, 380f, 30f), "NETA JI  /  PRAYAGRAJ", titleStyle);
            string mission = MissionController.Instance != null ? MissionController.Instance.MissionTitle : "Prototype 1";
            string objective = MissionController.Instance != null ? MissionController.Instance.CurrentObjective : "Loading seva route...";
            GUI.Label(new Rect(34f, 58f, Mathf.Min(400f, Screen.width * 0.47f), 22f), mission, smallStyle);
            GUI.Label(new Rect(34f, 80f, Mathf.Min(400f, Screen.width * 0.47f), 30f), objective, bodyStyle);
            string routeHint = GetRouteHint();
            if (!string.IsNullOrEmpty(routeHint))
            {
                GUI.Label(new Rect(34f, 105f, Mathf.Min(400f, Screen.width * 0.47f), 22f), routeHint, smallStyle);
            }

            PlayerProgress progress = GameSession.Instance?.Progress;
            if (progress != null)
            {
                float width = Mathf.Min(420f, Screen.width * 0.42f);
                bool showPolitics = progress.politicalPower > 0 || progress.volunteers > 0 || progress.oppositionPressure > 0;
                bool showCampaign = progress.wardSupport > 0 || progress.boothReadiness > 0 || progress.wardVoteShare > 0;
                bool showGovernance = progress.serviceDelivery > 0 || progress.fiscalIntegrity > 0
                    || progress.wardBudgetLakhs != 0 || progress.governanceScore > 0;
                bool showExpansion = progress.assemblyReach > 0 || progress.coalitionUnity > 0
                    || progress.assemblyReadiness > 0 || progress.nominationScore > 0;
                bool showAssemblyElection = progress.constituencySupport > 0 || progress.campaignCompliance > 0
                    || progress.electionOperations > 0 || progress.assemblyVoteShare > 0;
                bool showLegislature = progress.legislativeEffectiveness > 0 || progress.constituencyService > 0
                    || progress.ethicsRecord > 0 || progress.mlaAllocationLakhs != 0 || progress.mlaPerformanceScore > 0;
                bool showDistrict = progress.districtReach > 0 || progress.candidateQuality > 0
                    || progress.organizationDiscipline > 0 || progress.districtExpansionScore > 0;
                float statsHeight = showDistrict ? 130f : showLegislature ? 158f : showAssemblyElection ? 186f : showExpansion ? 158f
                    : showGovernance ? 158f : showCampaign ? 102f : showPolitics ? 76f : 52f;
                Rect statsRect = new Rect(Screen.width - width - 18f, 16f, width, statsHeight);
                DrawPanel(statsRect, new Color(0.015f, 0.08f, 0.10f, 0.96f));
                GUI.Label(new Rect(statsRect.x + 10f, statsRect.y + 8f, statsRect.width - 20f, 26f),
                    $"TRUST {progress.publicTrust}%  Rs {progress.money}  REP {progress.reputation}  PROOF {progress.caseProof}", statStyle);
                if (showPolitics)
                {
                    GUI.Label(new Rect(statsRect.x + 10f, statsRect.y + 38f, statsRect.width - 20f, 26f),
                        $"POWER {progress.politicalPower}  TEAM {progress.volunteers}  PRESSURE {progress.oppositionPressure}", statStyle);
                }
                if (showCampaign && !showExpansion)
                {
                    string vote = progress.wardVoteShare > 0 ? progress.wardVoteShare + "%" : "--";
                    GUI.Label(new Rect(statsRect.x + 10f, statsRect.y + 66f, statsRect.width - 20f, 26f),
                        $"WARD {progress.wardSupport}  BOOTH {progress.boothReadiness}  VOTE {vote}", statStyle);
                }
                if (showGovernance && !showLegislature)
                {
                    string review = progress.governanceScore > 0 ? progress.governanceScore.ToString() : "--";
                    float governanceY = showExpansion ? 66f : 94f;
                    GUI.Label(new Rect(statsRect.x + 10f, statsRect.y + governanceY, statsRect.width - 20f, 26f),
                        $"DELIVERY {progress.serviceDelivery}  INTEGRITY {progress.fiscalIntegrity}", statStyle);
                    GUI.Label(new Rect(statsRect.x + 10f, statsRect.y + governanceY + 28f, statsRect.width - 20f, 26f),
                        $"BUDGET Rs {progress.wardBudgetLakhs}L  REVIEW {review}/100", statStyle);
                }
                if (showExpansion && !showDistrict)
                {
                    string nomination = progress.nominationScore > 0 ? progress.nominationScore.ToString() : "--";
                    float expansionY = showLegislature ? 66f : 122f;
                    GUI.Label(new Rect(statsRect.x + 10f, statsRect.y + expansionY, statsRect.width - 20f, 26f),
                        $"REACH {progress.assemblyReach}  UNITY {progress.coalitionUnity}  READY {progress.assemblyReadiness}  NOM {nomination}", statStyle);
                }
                if (showAssemblyElection && !showDistrict)
                {
                    string vote = progress.assemblyVoteShare > 0 ? progress.assemblyVoteShare + "%" : "--";
                    float electionY = showLegislature ? 94f : 150f;
                    GUI.Label(new Rect(statsRect.x + 10f, statsRect.y + electionY, statsRect.width - 20f, 26f),
                        $"SUP {progress.constituencySupport}  RULES {progress.campaignCompliance}  OPS {progress.electionOperations}  VOTE {vote}", statStyle);
                }
                if (showLegislature)
                {
                    string performance = progress.mlaPerformanceScore > 0 ? progress.mlaPerformanceScore.ToString() : "--";
                    float legislatureY = showDistrict ? 66f : 122f;
                    GUI.Label(new Rect(statsRect.x + 10f, statsRect.y + legislatureY, statsRect.width - 20f, 26f),
                        $"MLA L{progress.legislativeEffectiveness} S{progress.constituencyService} E{progress.ethicsRecord}  FUND Rs{progress.mlaAllocationLakhs}L  SCORE {performance}", statStyle);
                }
                if (showDistrict)
                {
                    string districtScore = progress.districtExpansionScore > 0 ? progress.districtExpansionScore.ToString() : "--";
                    GUI.Label(new Rect(statsRect.x + 10f, statsRect.y + 94f, statsRect.width - 20f, 26f),
                        $"DIST R{progress.districtReach} Q{progress.candidateQuality} D{progress.organizationDiscipline}  SCORE {districtScore}", statStyle);
                }
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

            if (Time.unscaledTime < bannerUntil && !IsDecisionOpen)
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

            if (showChapterActions && Time.unscaledTime >= dialogueUntil && Time.unscaledTime >= bannerUntil)
            {
                DrawChapterActions();
            }

            if (IsDecisionOpen)
            {
                DrawDecision();
            }
        }

        private void DrawDecision()
        {
            float width = Mathf.Min(680f, Screen.width - 36f);
            float height = Mathf.Min(230f, Screen.height - 42f);
            Rect panel = new Rect((Screen.width - width) * 0.5f, (Screen.height - height) * 0.5f, width, height);
            DrawPanel(panel, new Color(0.01f, 0.045f, 0.06f, 0.985f));
            GUI.Label(new Rect(panel.x + 22f, panel.y + 16f, panel.width - 44f, 30f), decisionTitle, titleStyle);
            GUI.Label(new Rect(panel.x + 22f, panel.y + 50f, panel.width - 44f, 68f), decisionMessage, bodyStyle);

            float buttonWidth = (panel.width - 60f) * 0.5f;
            Rect leftRect = new Rect(panel.x + 22f, panel.y + panel.height - 76f, buttonWidth, 54f);
            Rect rightRect = new Rect(leftRect.xMax + 16f, leftRect.y, buttonWidth, 54f);
            DrawPanel(leftRect, new Color(0.93f, 0.61f, 0.10f, 0.98f));
            GUIStyle safeStyle = new GUIStyle(statStyle);
            safeStyle.normal.textColor = new Color(0.02f, 0.10f, 0.11f);
            if (GUI.Button(leftRect, decisionLeft, safeStyle))
            {
                ResolveDecision(1);
            }

            DrawPanel(rightRect, new Color(0.33f, 0.15f, 0.16f, 0.98f));
            if (GUI.Button(rightRect, decisionRight, statStyle))
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

        private void DrawChapterActions()
        {
            float width = string.IsNullOrEmpty(nextChapterScene) ? 190f : 390f;
            float height = 52f;
            float x = (Screen.width - width) * 0.5f;
            float y = Screen.height - height - 24f;
            if (!string.IsNullOrEmpty(nextChapterScene))
            {
                Rect nextRect = new Rect(x, y, 190f, height);
                DrawPanel(nextRect, new Color(0.93f, 0.61f, 0.10f, 0.96f));
                GUIStyle nextStyle = new GUIStyle(statStyle);
                nextStyle.normal.textColor = new Color(0.02f, 0.10f, 0.11f);
                if (GUI.Button(nextRect, "NEXT CHAPTER", nextStyle))
                {
                    SceneManager.LoadScene(nextChapterScene);
                }
                x += 200f;
            }

            Rect menuRect = new Rect(x, y, 190f, height);
            DrawPanel(menuRect, new Color(0.02f, 0.25f, 0.27f, 0.96f));
            if (GUI.Button(menuRect, "MAIN MENU", statStyle))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        private string GetRouteHint()
        {
            MissionObjective objective = MissionController.Instance?.CurrentObjectiveItem;
            if (routePlayer == null)
            {
                routePlayer = FindFirstObjectByType<AzadController>();
            }
            if (routeCamera == null)
            {
                routeCamera = Camera.main;
            }
            if (objective == null || routePlayer == null || routeCamera == null)
            {
                return string.Empty;
            }

            Vector3 localTarget = routeCamera.transform.InverseTransformPoint(objective.transform.position);
            string direction;
            if (localTarget.z < -1.5f)
            {
                direction = "BEHIND";
            }
            else if (localTarget.x < -1.5f)
            {
                direction = "LEFT";
            }
            else if (localTarget.x > 1.5f)
            {
                direction = "RIGHT";
            }
            else
            {
                direction = "AHEAD";
            }

            float distance = Vector3.Distance(routePlayer.transform.position, objective.transform.position);
            return $"ROUTE  {direction}  /  {distance:0} m";
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

using System.Collections.Generic;
using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class FreeRoamMapHud : MonoBehaviour
    {
        public static FreeRoamMapHud Instance { get; private set; }

        [SerializeField] private Transform trackedTarget;
        [SerializeField] private Vector2 worldMin = new Vector2(-230f, -230f);
        [SerializeField] private Vector2 worldMax = new Vector2(230f, 230f);
        [SerializeField] private Vector3[] poiPositions;
        [SerializeField] private string[] poiNames;

        private Texture2D groundTexture;
        private Texture2D riverTexture;
        private Texture2D roadTexture;
        private Texture2D panelTexture;
        private Texture2D poiTexture;
        private Texture2D playerArrow;
        private bool fullMapOpen;
        private string interactionPrompt = string.Empty;
        private string activeVehicleName = string.Empty;
        private float activeVehicleSpeed;
        private int storyChapter;
        private int storyCompleted;
        private string storyTitle = string.Empty;
        private bool storyCampaignComplete;

        public void SetInteractionPrompt(string value)
        {
            interactionPrompt = value ?? string.Empty;
        }

        public void SetFullMapOpen(bool value)
        {
            fullMapOpen = value;
        }

        public void SetVehicleStatus(string vehicleName, float speedKph)
        {
            string value = vehicleName ?? string.Empty;
            activeVehicleName = value.Contains("Scooter") ? "SCOOTER" : value.Contains("Car") ? "CAR" : value.ToUpperInvariant();
            activeVehicleSpeed = Mathf.Max(0f, speedKph);
        }

        public void ClearVehicleStatus()
        {
            activeVehicleName = string.Empty;
            activeVehicleSpeed = 0f;
        }

        public void SetStoryStatus(int chapter, int completed, string title, bool campaignComplete)
        {
            storyChapter = Mathf.Clamp(chapter, 1, StoryChapterCatalog.Count);
            storyCompleted = Mathf.Clamp(completed, 0, StoryChapterCatalog.Count);
            storyTitle = title ?? string.Empty;
            storyCampaignComplete = campaignComplete;
        }

        public void Configure(
            Transform target,
            Vector2 minimum,
            Vector2 maximum,
            Vector3[] positions,
            string[] names)
        {
            trackedTarget = target;
            worldMin = minimum;
            worldMax = maximum;
            poiPositions = positions;
            poiNames = names;
        }

        private void Awake()
        {
            Instance = this;
            groundTexture = MakeTexture(new Color(0.70f, 0.75f, 0.62f, 1f));
            riverTexture = MakeTexture(new Color(0.10f, 0.48f, 0.65f, 1f));
            roadTexture = MakeTexture(new Color(0.24f, 0.28f, 0.29f, 1f));
            panelTexture = MakeTexture(new Color(0.01f, 0.055f, 0.065f, 0.96f));
            poiTexture = MakeTexture(new Color(0.95f, 0.66f, 0.12f, 1f));
            playerArrow = MakeArrowTexture();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                fullMapOpen = !fullMapOpen;
            }
            if (fullMapOpen && Input.GetKeyDown(KeyCode.Escape))
            {
                fullMapOpen = false;
            }
        }

        private void OnGUI()
        {
            if (StoryHubController.Instance != null && StoryHubController.Instance.ConfirmationOpen)
            {
                return;
            }

            if (trackedTarget == null)
            {
                return;
            }

            if (fullMapOpen)
            {
                DrawFullMap();
                return;
            }

            float width = Mathf.Clamp(Screen.width * 0.22f, 176f, 242f);
            float height = width * 0.67f;
            Rect panel = new Rect(16f, 14f, width, height + 34f);
            GUI.DrawTexture(panel, panelTexture);
            Rect mapRect = new Rect(panel.x + 8f, panel.y + 8f, panel.width - 16f, height - 8f);
            DrawMap(mapRect, false);

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = Mathf.Clamp(Mathf.RoundToInt(Screen.height * 0.024f), 11, 16),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            buttonStyle.normal.textColor = Color.white;
            if (GUI.Button(new Rect(panel.x + 8f, panel.yMax - 30f, panel.width - 16f, 24f), "MAP", buttonStyle))
            {
                fullMapOpen = true;
            }

            if (!string.IsNullOrWhiteSpace(activeVehicleName))
            {
                DrawVehicleStatus();
            }

            if (!string.IsNullOrWhiteSpace(storyTitle))
            {
                DrawStoryStatus();
            }

            if (!string.IsNullOrWhiteSpace(interactionPrompt))
            {
                float promptWidth = Mathf.Min(440f, Screen.width - 42f);
                Rect prompt = new Rect((Screen.width - promptWidth) * 0.5f, Screen.height - 72f, promptWidth, 44f);
                GUI.DrawTexture(prompt, panelTexture);
                GUIStyle promptStyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = Mathf.Clamp(Mathf.RoundToInt(Screen.height * 0.03f), 13, 19),
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter
                };
                promptStyle.normal.textColor = Color.white;
                GUI.Label(prompt, $"USE  /  {interactionPrompt}", promptStyle);
            }
        }

        private void DrawFullMap()
        {
            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), panelTexture);
            float height = Mathf.Min(Screen.height - 60f, (Screen.width - 56f) / 1.48f);
            float width = height * 1.48f;
            Rect mapRect = new Rect((Screen.width - width) * 0.5f, (Screen.height - height) * 0.5f, width, height);
            DrawMap(mapRect, true);

            GUIStyle closeStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = Mathf.Clamp(Mathf.RoundToInt(Screen.height * 0.03f), 13, 20),
                fontStyle = FontStyle.Bold
            };
            if (GUI.Button(new Rect(mapRect.xMax - 94f, mapRect.y + 12f, 78f, 36f), "CLOSE", closeStyle))
            {
                fullMapOpen = false;
            }
        }

        private void DrawVehicleStatus()
        {
            float width = Mathf.Clamp(Screen.width * 0.17f, 132f, 190f);
            float height = Mathf.Clamp(Screen.height * 0.16f, 58f, 82f);
            Rect panel = new Rect(Screen.width - width - 18f, 16f, width, height);
            GUI.DrawTexture(panel, panelTexture);
            GUIStyle speedStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.Clamp(Mathf.RoundToInt(height * 0.42f), 24, 36),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperCenter
            };
            speedStyle.normal.textColor = new Color(0.96f, 0.73f, 0.18f);
            GUI.Label(new Rect(panel.x, panel.y + 1f, panel.width, height * 0.62f), Mathf.RoundToInt(activeVehicleSpeed).ToString("00"), speedStyle);

            GUIStyle unitStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.Clamp(Mathf.RoundToInt(height * 0.18f), 10, 14),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.LowerCenter,
                clipping = TextClipping.Clip
            };
            unitStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(panel.x + 6f, panel.y + height * 0.48f, panel.width - 12f, height * 0.43f), "KM/H  |  " + activeVehicleName, unitStyle);
        }

        private void DrawStoryStatus()
        {
            float width = Mathf.Clamp(Screen.width * 0.32f, 240f, 310f);
            float height = Mathf.Clamp(Screen.height * 0.21f, 80f, 98f);
            float y = string.IsNullOrWhiteSpace(activeVehicleName)
                ? 16f
                : 16f + Mathf.Clamp(Screen.height * 0.16f, 58f, 82f) + 10f;
            Rect panel = new Rect(Screen.width - width - 18f, y, width, height);
            GUI.DrawTexture(panel, panelTexture);

            GUIStyle chapterStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.Clamp(Mathf.RoundToInt(height * 0.20f), 11, 15),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                wordWrap = false,
                clipping = TextClipping.Clip
            };
            chapterStyle.normal.textColor = new Color(0.58f, 0.88f, 0.82f);
            GUI.Label(new Rect(panel.x + 12f, panel.y + 5f, panel.width - 24f, 22f),
                storyCampaignComplete ? "STORY COMPLETE" : $"STORY  /  CH {storyChapter:00} OF {StoryChapterCatalog.Count}", chapterStyle);

            GUIStyle titleStyle = new GUIStyle(chapterStyle)
            {
                fontSize = Mathf.Clamp(Mathf.RoundToInt(height * 0.22f), 12, 17),
                clipping = TextClipping.Clip
            };
            titleStyle.normal.textColor = new Color(1f, 0.76f, 0.20f);
            GUI.Label(new Rect(panel.x + 12f, panel.y + 27f, panel.width - 24f, 25f), storyTitle, titleStyle);

            GUIStyle progressStyle = new GUIStyle(chapterStyle)
            {
                fontSize = Mathf.Clamp(Mathf.RoundToInt(height * 0.16f), 10, 13)
            };
            progressStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(panel.x + 12f, panel.yMax - 29f, panel.width - 24f, 22f),
                storyCampaignComplete ? "FINAL REVIEW AVAILABLE" : $"{storyCompleted}/{StoryChapterCatalog.Count} COMPLETE  |  HELPERS HAND", progressStyle);
        }

        private void DrawMap(Rect mapRect, bool showLabels)
        {
            GUI.DrawTexture(mapRect, groundTexture);
            DrawWorldRect(mapRect, new Rect(145f, -230f, 85f, 460f), riverTexture);
            DrawWorldRect(mapRect, new Rect(-225f, -12f, 370f, 24f), roadTexture);
            DrawWorldRect(mapRect, new Rect(-12f, -225f, 24f, 370f), roadTexture);
            DrawWorldRect(mapRect, new Rect(-205f, 92f, 350f, 18f), roadTexture);
            DrawWorldRect(mapRect, new Rect(-195f, -125f, 340f, 18f), roadTexture);
            DrawWorldRect(mapRect, new Rect(-120f, -205f, 18f, 320f), roadTexture);
            DrawWorldRect(mapRect, new Rect(70f, -205f, 18f, 350f), roadTexture);
            DrawWorldRect(mapRect, new Rect(126f, -205f, 14f, 350f), roadTexture);

            int count = Mathf.Min(poiPositions?.Length ?? 0, poiNames?.Length ?? 0);
            GUIStyle poiStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.Clamp(Mathf.RoundToInt(mapRect.height * 0.030f), 11, 17),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                clipping = TextClipping.Clip
            };
            poiStyle.normal.textColor = new Color(0.02f, 0.10f, 0.12f);
            List<Rect> occupiedLabels = new List<Rect>();
            for (int i = 0; i < count; i++)
            {
                Vector2 point = WorldToMap(mapRect, poiPositions[i]);
                float size = showLabels ? 12f : 7f;
                GUI.DrawTexture(new Rect(point.x - size * 0.5f, point.y - size * 0.5f, size, size), poiTexture);
                if (showLabels)
                {
                    float labelWidth = Mathf.Clamp(mapRect.width * 0.31f, 104f, 152f);
                    float labelHeight = 22f;
                    float labelX = point.x + 8f;
                    if (labelX + labelWidth > mapRect.xMax - 5f)
                    {
                        labelX = point.x - labelWidth - 8f;
                    }
                    labelX = Mathf.Clamp(labelX, mapRect.x + 5f, mapRect.xMax - labelWidth - 5f);
                    Rect labelRect = new Rect(labelX, point.y - labelHeight * 0.5f, labelWidth, labelHeight);
                    for (int attempt = 0; attempt < 6; attempt++)
                    {
                        bool overlaps = false;
                        foreach (Rect occupied in occupiedLabels)
                        {
                            if (occupied.Overlaps(labelRect))
                            {
                                overlaps = true;
                                break;
                            }
                        }
                        if (!overlaps)
                        {
                            break;
                        }
                        labelRect.y += labelHeight - 2f;
                    }
                    labelRect.y = Mathf.Clamp(labelRect.y, mapRect.y + 4f, mapRect.yMax - labelHeight - 4f);
                    occupiedLabels.Add(labelRect);
                    GUI.Label(labelRect, poiNames[i], poiStyle);
                }
            }

            Vector2 playerPoint = WorldToMap(mapRect, trackedTarget.position);
            float markerSize = showLabels ? 30f : 19f;
            Matrix4x4 previous = GUI.matrix;
            GUIUtility.RotateAroundPivot(trackedTarget.eulerAngles.y, playerPoint);
            GUI.DrawTexture(
                new Rect(playerPoint.x - markerSize * 0.5f, playerPoint.y - markerSize * 0.5f, markerSize, markerSize),
                playerArrow);
            GUI.matrix = previous;
        }

        private void DrawWorldRect(Rect mapRect, Rect worldRect, Texture2D texture)
        {
            Vector2 topLeft = WorldToMap(mapRect, new Vector3(worldRect.xMin, 0f, worldRect.yMax));
            Vector2 bottomRight = WorldToMap(mapRect, new Vector3(worldRect.xMax, 0f, worldRect.yMin));
            GUI.DrawTexture(
                new Rect(topLeft.x, topLeft.y, bottomRight.x - topLeft.x, bottomRight.y - topLeft.y),
                texture);
        }

        private Vector2 WorldToMap(Rect mapRect, Vector3 worldPosition)
        {
            float x = Mathf.InverseLerp(worldMin.x, worldMax.x, worldPosition.x);
            float y = Mathf.InverseLerp(worldMin.y, worldMax.y, worldPosition.z);
            return new Vector2(mapRect.x + x * mapRect.width, mapRect.yMax - y * mapRect.height);
        }

        private static Texture2D MakeTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }

        private static Texture2D MakeArrowTexture()
        {
            const int size = 20;
            Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            Color clear = new Color(0f, 0f, 0f, 0f);
            Color fill = new Color(0.92f, 0.18f, 0.13f, 1f);
            for (int y = 0; y < size; y++)
            {
                float halfWidth = Mathf.Lerp(1f, size * 0.43f, y / (float)(size - 1));
                for (int x = 0; x < size; x++)
                {
                    texture.SetPixel(x, y, Mathf.Abs(x - size * 0.5f) <= halfWidth ? fill : clear);
                }
            }
            texture.Apply();
            return texture;
        }
    }
}

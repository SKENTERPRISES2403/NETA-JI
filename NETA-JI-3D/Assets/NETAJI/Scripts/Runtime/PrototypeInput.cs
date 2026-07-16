using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class PrototypeInput : MonoBehaviour
    {
        public static PrototypeInput Instance { get; private set; }

        [SerializeField] private float joystickRadius = 78f;
        [SerializeField] private float mouseLookSensitivity = 0.16f;
        [SerializeField] private float touchLookSensitivity = 0.12f;
        [SerializeField] private bool showTouchControlsInEditor = true;

        private int moveFingerId = -1;
        private int lookFingerId = -1;
        private Vector2 moveOrigin;
        private Vector2 movePosition;
        private Vector2 previousLookPosition;
        private Vector2 touchMove;
        private Vector2 lookDelta;
        private bool interactQueued;
        private Texture2D whiteTexture;
        private Texture2D helpButtonTexture;
        private Texture2D helpButtonActiveTexture;
        private string actionLabel = "HELP";

        public Vector2 Move
        {
            get
            {
                Vector2 keyboard = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                keyboard = Vector2.ClampMagnitude(keyboard, 1f);
                return touchMove.sqrMagnitude > keyboard.sqrMagnitude ? touchMove : keyboard;
            }
        }

        public Vector2 LookDelta => lookDelta;
        public bool RunHeld => Input.GetKey(KeyCode.LeftShift) || touchMove.magnitude > 0.88f;

        public void SetActionLabel(string value)
        {
            actionLabel = string.IsNullOrWhiteSpace(value) ? "USE" : value.Trim().ToUpperInvariant();
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            whiteTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            whiteTexture.SetPixel(0, 0, Color.white);
            whiteTexture.Apply();
            helpButtonTexture = MakeTintedTexture(new Color(0.04f, 0.34f, 0.37f, 0.90f));
            helpButtonActiveTexture = MakeTintedTexture(new Color(0.96f, 0.63f, 0.12f, 0.95f));
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }

        private void Update()
        {
            lookDelta = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
            {
                interactQueued = true;
            }

            if (Input.GetMouseButton(1))
            {
                lookDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseLookSensitivity * 100f;
            }

            HandleTouches();
        }

        private void HandleTouches()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                Vector2 position = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    if (IsInteractArea(position))
                    {
                        interactQueued = true;
                        continue;
                    }

                    if (position.x < Screen.width * 0.5f && moveFingerId < 0)
                    {
                        moveFingerId = touch.fingerId;
                        moveOrigin = position;
                        movePosition = position;
                    }
                    else if (lookFingerId < 0)
                    {
                        lookFingerId = touch.fingerId;
                        previousLookPosition = position;
                    }
                }

                if (touch.fingerId == moveFingerId)
                {
                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        moveFingerId = -1;
                        touchMove = Vector2.zero;
                    }
                    else
                    {
                        movePosition = position;
                        touchMove = Vector2.ClampMagnitude((position - moveOrigin) / joystickRadius, 1f);
                    }
                }

                if (touch.fingerId == lookFingerId)
                {
                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        lookFingerId = -1;
                    }
                    else
                    {
                        Vector2 frameDelta = position - previousLookPosition;
                        previousLookPosition = position;
                        lookDelta += frameDelta * touchLookSensitivity;
                    }
                }
            }
        }

        public bool ConsumeInteract()
        {
            if (!interactQueued)
            {
                return false;
            }

            interactQueued = false;
            return true;
        }

        private bool IsInteractArea(Vector2 position)
        {
            float size = Mathf.Clamp(Screen.width * 0.18f, 76f, 120f);
            return position.x > Screen.width - size * 1.45f && position.y < size * 1.55f;
        }

        private void OnGUI()
        {
            bool showTouchControls = Application.isMobilePlatform;
#if UNITY_EDITOR
            showTouchControls |= showTouchControlsInEditor;
#endif
            if (!showTouchControls)
            {
                return;
            }

            float scale = Mathf.Clamp(Screen.width / 390f, 0.85f, 1.35f);
            float size = 104f * scale;
            Vector2 basePosition = moveFingerId >= 0 ? moveOrigin : new Vector2(82f * scale, 92f * scale);
            Vector2 knobPosition = moveFingerId >= 0 ? movePosition : basePosition;
            knobPosition = basePosition + Vector2.ClampMagnitude(knobPosition - basePosition, size * 0.36f);

            DrawDisc(basePosition, size, new Color(0.02f, 0.10f, 0.12f, 0.34f));
            DrawDisc(knobPosition, size * 0.42f, new Color(0.96f, 0.73f, 0.18f, 0.72f));

            float buttonSize = 88f * scale;
            Rect helpRect = new Rect(Screen.width - buttonSize - 24f * scale, Screen.height - buttonSize - 24f * scale, buttonSize, buttonSize);
            GUIStyle style = new GUIStyle(GUI.skin.button)
            {
                fontSize = Mathf.RoundToInt(15f * scale),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            style.normal.textColor = Color.white;
            style.normal.background = helpButtonTexture;
            style.active.background = helpButtonActiveTexture;

            if (GUI.Button(helpRect, actionLabel, style))
            {
                interactQueued = true;
            }
        }

        private void DrawDisc(Vector2 screenPosition, float size, Color color)
        {
            Color previous = GUI.color;
            GUI.color = color;
            Rect rect = new Rect(screenPosition.x - size * 0.5f, Screen.height - screenPosition.y - size * 0.5f, size, size);
            GUI.DrawTexture(rect, whiteTexture, ScaleMode.StretchToFill);
            GUI.color = previous;
        }

        private Texture2D MakeTintedTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }
    }
}

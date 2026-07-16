using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

namespace NetaJi.Prototype
{
    public sealed class PrologueCinematic : MonoBehaviour
    {
        private const int SampleRate = 22050;

        [SerializeField] private Camera cinematicCamera;
        [SerializeField] private string nextSceneName = "Prototype01";
        [SerializeField] private GameObject[] shotRoots;
        [SerializeField] private Transform[] cameraAnchors;
        [SerializeField] private Transform[] focusTargets;
        [SerializeField] private float[] shotDurations;
        [SerializeField] private float transitionDuration = 0.75f;

        private int shotIndex;
        private float shotStartedAt;
        private Texture2D darkTexture;
        private Texture2D buttonTexture;
        private AudioSource scoreSource;
        private bool smokeMode;
        private string smokeOutputDirectory;

        public void Configure(
            Camera cameraValue,
            string nextScene,
            GameObject[] roots,
            Transform[] anchors,
            Transform[] targets,
            float[] durations)
        {
            cinematicCamera = cameraValue;
            nextSceneName = nextScene;
            shotRoots = roots;
            cameraAnchors = anchors;
            focusTargets = targets;
            shotDurations = durations;
        }

        private void Awake()
        {
            darkTexture = MakeTexture(new Color(0.01f, 0.02f, 0.025f, 0.96f));
            buttonTexture = MakeTexture(new Color(0.02f, 0.25f, 0.27f, 0.92f));
            scoreSource = gameObject.AddComponent<AudioSource>();
            scoreSource.loop = true;
            scoreSource.volume = 0.32f;
            scoreSource.spatialBlend = 0f;
            scoreSource.clip = CreateScore();
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
            string[] arguments = System.Environment.GetCommandLineArgs();
            smokeMode = System.Array.IndexOf(arguments, "-prologueSmoke") >= 0;
            if (smokeMode)
            {
                smokeOutputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
                Directory.CreateDirectory(smokeOutputDirectory);
                for (int i = 0; i < shotDurations.Length; i++)
                {
                    shotDurations[i] = 1.6f;
                }
            }
            SetShot(0, true);
            scoreSource.Play();
            if (smokeMode)
            {
                StartCoroutine(RunSmoke());
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                Finish();
                return;
            }

            if (shotRoots == null || shotRoots.Length == 0 || cinematicCamera == null)
            {
                Finish();
                return;
            }

            float duration = GetDuration(shotIndex);
            float elapsed = Time.time - shotStartedAt;
            if (elapsed >= duration)
            {
                if (shotIndex + 1 >= shotRoots.Length)
                {
                    if (!smokeMode)
                    {
                        Finish();
                    }
                }
                else
                {
                    SetShot(shotIndex + 1, false);
                }
                return;
            }

            Transform anchor = cameraAnchors[shotIndex];
            Transform focus = focusTargets[shotIndex];
            float blend = Mathf.Clamp01(elapsed / transitionDuration);
            blend = blend * blend * (3f - 2f * blend);
            float shotProgress = Mathf.Clamp01(elapsed / duration);
            Vector3 towardFocus = (focus.position - anchor.position).normalized;
            Vector3 cameraDrift = towardFocus * (shotProgress * 0.32f)
                + Vector3.right * (Mathf.Sin(shotProgress * Mathf.PI) * 0.10f);
            Vector3 desiredPosition = anchor.position + cameraDrift;
            cinematicCamera.transform.position = Vector3.Lerp(cinematicCamera.transform.position, desiredPosition, blend);
            Quaternion desiredRotation = Quaternion.LookRotation(focus.position - cinematicCamera.transform.position, Vector3.up);
            cinematicCamera.transform.rotation = Quaternion.Slerp(cinematicCamera.transform.rotation, desiredRotation, blend);
        }

        private void SetShot(int index, bool snap)
        {
            shotIndex = Mathf.Clamp(index, 0, shotRoots.Length - 1);
            for (int i = 0; i < shotRoots.Length; i++)
            {
                if (shotRoots[i] != null)
                {
                    shotRoots[i].SetActive(i == shotIndex);
                }
            }
            Transform anchor = cameraAnchors[shotIndex];
            Transform focus = focusTargets[shotIndex];
            if (snap || shotIndex > 0)
            {
                cinematicCamera.transform.position = anchor.position;
                cinematicCamera.transform.rotation = Quaternion.LookRotation(focus.position - anchor.position, Vector3.up);
            }
            shotStartedAt = Time.time;
        }

        private float GetDuration(int index)
        {
            return shotDurations != null && index < shotDurations.Length ? Mathf.Max(1f, shotDurations[index]) : 4f;
        }

        private void Finish()
        {
            if (!string.IsNullOrWhiteSpace(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }

        private IEnumerator RunSmoke()
        {
            string[] names = { "family", "service", "abduction", "rescue", "crisis", "recovery", "resolve" };
            for (int index = 0; index < names.Length; index++)
            {
                SetShot(index, true);
                yield return new WaitForSeconds(0.85f);
                ScreenCapture.CaptureScreenshot(Path.Combine(smokeOutputDirectory, $"prologue-{names[index]}.png"));
                yield return new WaitForSeconds(0.30f);
            }
            bool passed = shotRoots != null && shotRoots.Length == 7
                && cinematicCamera != null && shotIndex >= 6;
            Debug.Log(passed
                ? "PROLOGUE_SMOKE_PASSED: seven visual shots rendered without exposition text."
                : $"PROLOGUE_SMOKE_FAILED: roots={shotRoots?.Length ?? 0}, shot={shotIndex}.");
            Application.Quit(passed ? 0 : 3);
        }

        private static string ReadArgument(string[] arguments, string name)
        {
            int index = System.Array.IndexOf(arguments, name);
            return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : null;
        }

        private void OnGUI()
        {
            float barHeight = Mathf.Clamp(Screen.height * 0.085f, 24f, 58f);
            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, barHeight), darkTexture);
            GUI.DrawTexture(new Rect(0f, Screen.height - barHeight, Screen.width, barHeight), darkTexture);
            float buttonWidth = Mathf.Clamp(Screen.width * 0.13f, 90f, 150f);
            Rect skipRect = new Rect(Screen.width - buttonWidth - 18f, 12f, buttonWidth, Mathf.Max(34f, barHeight - 20f));
            GUIStyle skipStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = Mathf.Clamp(Mathf.RoundToInt(Screen.height * 0.025f), 12, 18),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            skipStyle.normal.background = buttonTexture;
            skipStyle.hover.background = buttonTexture;
            skipStyle.normal.textColor = Color.white;
            if (GUI.Button(skipRect, "SKIP", skipStyle))
            {
                Finish();
            }
        }

        private static Texture2D MakeTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }

        private static AudioClip CreateScore()
        {
            const float length = 32f;
            int sampleCount = Mathf.CeilToInt(SampleRate * length);
            float[] samples = new float[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                float t = i / (float)SampleRate;
                float story = t / length;
                float baseFrequency = story < 0.28f ? 196f : story < 0.68f ? 146.8f : 220f;
                float pulse = 0.55f + 0.45f * Mathf.Sin(t * Mathf.PI * 0.48f);
                float resolve = Mathf.SmoothStep(0.25f, 1f, Mathf.InverseLerp(0.64f, 1f, story));
                float sample = Mathf.Sin(t * Mathf.PI * 2f * baseFrequency) * 0.055f;
                sample += Mathf.Sin(t * Mathf.PI * 2f * baseFrequency * 1.5f) * 0.025f;
                sample += Mathf.Sin(t * Mathf.PI * 2f * 293.7f) * resolve * 0.025f;
                samples[i] = sample * pulse;
            }
            AudioClip clip = AudioClip.Create("Azad Visual Prologue Score", sampleCount, 1, SampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }
    }
}

using UnityEngine;
using UnityEngine.Rendering;

namespace NetaJi.Prototype
{
    public sealed class OpenWorldMissionAtmosphere : MonoBehaviour
    {
        [SerializeField] private bool evening;

        private AmbientMode previousAmbientMode;
        private Color previousAmbientSky;
        private Color previousAmbientEquator;
        private Color previousAmbientGround;
        private bool previousFog;
        private FogMode previousFogMode;
        private Color previousFogColor;
        private float previousFogDensity;
        private Material previousSkybox;
        private Material eveningSkybox;
        private Light sun;
        private Color previousSunColor;
        private float previousSunIntensity;
        private Quaternion previousSunRotation;
        private bool applied;

        public bool IsApplied => applied;

        public void ConfigureEvening()
        {
            evening = true;
        }

        private void OnEnable()
        {
            if (!evening)
            {
                return;
            }

            previousAmbientMode = RenderSettings.ambientMode;
            previousAmbientSky = RenderSettings.ambientSkyColor;
            previousAmbientEquator = RenderSettings.ambientEquatorColor;
            previousAmbientGround = RenderSettings.ambientGroundColor;
            previousFog = RenderSettings.fog;
            previousFogMode = RenderSettings.fogMode;
            previousFogColor = RenderSettings.fogColor;
            previousFogDensity = RenderSettings.fogDensity;
            previousSkybox = RenderSettings.skybox;

            RenderSettings.ambientMode = AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.28f, 0.32f, 0.48f);
            RenderSettings.ambientEquatorColor = new Color(0.16f, 0.20f, 0.31f);
            RenderSettings.ambientGroundColor = new Color(0.08f, 0.10f, 0.16f);
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogColor = new Color(0.18f, 0.24f, 0.35f);
            RenderSettings.fogDensity = 0.0018f;
            if (previousSkybox != null)
            {
                eveningSkybox = new Material(previousSkybox);
                if (eveningSkybox.HasProperty("_SkyTint"))
                {
                    eveningSkybox.SetColor("_SkyTint", new Color(0.16f, 0.24f, 0.43f));
                }
                if (eveningSkybox.HasProperty("_GroundColor"))
                {
                    eveningSkybox.SetColor("_GroundColor", new Color(0.045f, 0.06f, 0.11f));
                }
                if (eveningSkybox.HasProperty("_Exposure"))
                {
                    eveningSkybox.SetFloat("_Exposure", 0.42f);
                }
                RenderSettings.skybox = eveningSkybox;
                DynamicGI.UpdateEnvironment();
            }

            foreach (Light candidate in FindObjectsByType<Light>(FindObjectsSortMode.None))
            {
                if (candidate.type == LightType.Directional)
                {
                    sun = candidate;
                    break;
                }
            }

            if (sun != null)
            {
                previousSunColor = sun.color;
                previousSunIntensity = sun.intensity;
                previousSunRotation = sun.transform.rotation;
                sun.color = new Color(1f, 0.66f, 0.38f);
                sun.intensity = 0.72f;
                sun.transform.rotation = Quaternion.Euler(18f, -35f, 0f);
            }

            applied = true;
        }

        private void OnDisable()
        {
            if (!applied)
            {
                return;
            }

            RenderSettings.ambientMode = previousAmbientMode;
            RenderSettings.ambientSkyColor = previousAmbientSky;
            RenderSettings.ambientEquatorColor = previousAmbientEquator;
            RenderSettings.ambientGroundColor = previousAmbientGround;
            RenderSettings.fog = previousFog;
            RenderSettings.fogMode = previousFogMode;
            RenderSettings.fogColor = previousFogColor;
            RenderSettings.fogDensity = previousFogDensity;
            RenderSettings.skybox = previousSkybox;
            DynamicGI.UpdateEnvironment();
            if (sun != null)
            {
                sun.color = previousSunColor;
                sun.intensity = previousSunIntensity;
                sun.transform.rotation = previousSunRotation;
            }

            if (eveningSkybox != null)
            {
                Destroy(eveningSkybox);
                eveningSkybox = null;
            }

            applied = false;
        }
    }
}

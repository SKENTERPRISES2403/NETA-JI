using UnityEngine;
using UnityEngine.Rendering;

namespace NetaJi.Prototype
{
    public sealed class OpenWorldMissionAtmosphere : MonoBehaviour
    {
        [SerializeField] private bool evening;
        [SerializeField] private bool nightSearch;
        [SerializeField] private bool dawnRescue;

        private AmbientMode previousAmbientMode;
        private Color previousAmbientSky;
        private Color previousAmbientEquator;
        private Color previousAmbientGround;
        private bool previousFog;
        private FogMode previousFogMode;
        private Color previousFogColor;
        private float previousFogDensity;
        private Material previousSkybox;
        private Material missionSkybox;
        private Light sun;
        private Color previousSunColor;
        private float previousSunIntensity;
        private Quaternion previousSunRotation;
        private bool applied;

        public bool IsApplied => applied;
        public bool IsNightSearch => applied && nightSearch;
        public bool IsDawnRescue => applied && dawnRescue;

        public void ConfigureEvening()
        {
            evening = true;
            nightSearch = false;
            dawnRescue = false;
        }

        public void ConfigureNightSearch()
        {
            evening = false;
            nightSearch = true;
            dawnRescue = false;
        }

        public void ConfigureDawnRescue()
        {
            evening = false;
            nightSearch = false;
            dawnRescue = true;
        }

        private void OnEnable()
        {
            if (!evening && !nightSearch && !dawnRescue)
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
            RenderSettings.ambientSkyColor = nightSearch
                ? new Color(0.13f, 0.18f, 0.30f)
                : dawnRescue ? new Color(0.34f, 0.43f, 0.60f)
                : new Color(0.28f, 0.32f, 0.48f);
            RenderSettings.ambientEquatorColor = nightSearch
                ? new Color(0.10f, 0.14f, 0.22f)
                : dawnRescue ? new Color(0.48f, 0.34f, 0.27f)
                : new Color(0.16f, 0.20f, 0.31f);
            RenderSettings.ambientGroundColor = nightSearch
                ? new Color(0.035f, 0.05f, 0.09f)
                : dawnRescue ? new Color(0.12f, 0.13f, 0.15f)
                : new Color(0.08f, 0.10f, 0.16f);
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogColor = nightSearch
                ? new Color(0.07f, 0.11f, 0.20f)
                : dawnRescue ? new Color(0.28f, 0.34f, 0.45f)
                : new Color(0.18f, 0.24f, 0.35f);
            RenderSettings.fogDensity = nightSearch ? 0.0026f : dawnRescue ? 0.0015f : 0.0018f;
            if (previousSkybox != null)
            {
                missionSkybox = new Material(previousSkybox);
                if (missionSkybox.HasProperty("_SkyTint"))
                {
                    missionSkybox.SetColor("_SkyTint", nightSearch
                        ? new Color(0.035f, 0.08f, 0.18f)
                        : dawnRescue ? new Color(0.40f, 0.48f, 0.64f)
                        : new Color(0.16f, 0.24f, 0.43f));
                }
                if (missionSkybox.HasProperty("_GroundColor"))
                {
                    missionSkybox.SetColor("_GroundColor", nightSearch
                        ? new Color(0.012f, 0.02f, 0.045f)
                        : dawnRescue ? new Color(0.18f, 0.13f, 0.11f)
                        : new Color(0.045f, 0.06f, 0.11f));
                }
                if (missionSkybox.HasProperty("_Exposure"))
                {
                    missionSkybox.SetFloat("_Exposure", nightSearch ? 0.20f : dawnRescue ? 0.56f : 0.42f);
                }
                RenderSettings.skybox = missionSkybox;
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
                sun.color = nightSearch
                    ? new Color(0.58f, 0.70f, 1f)
                    : dawnRescue ? new Color(1f, 0.72f, 0.48f)
                    : new Color(1f, 0.66f, 0.38f);
                sun.intensity = nightSearch ? 0.52f : dawnRescue ? 0.88f : 0.72f;
                sun.transform.rotation = nightSearch
                    ? Quaternion.Euler(40f, -24f, 0f)
                    : dawnRescue ? Quaternion.Euler(27f, -35f, 0f)
                    : Quaternion.Euler(18f, -35f, 0f);
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

            if (missionSkybox != null)
            {
                Destroy(missionSkybox);
                missionSkybox = null;
            }

            applied = false;
        }
    }
}

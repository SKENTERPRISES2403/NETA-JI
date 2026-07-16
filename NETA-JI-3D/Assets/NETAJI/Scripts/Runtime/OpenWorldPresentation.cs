using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class OpenWorldPresentation : MonoBehaviour
    {
        [SerializeField] private Camera targetCamera;

        public bool IsApplied { get; private set; }
        public bool IsLowSpecMobile { get; private set; }
        public int AppliedAntiAliasing { get; private set; }
        public float AppliedShadowDistance { get; private set; }

        public void Configure(Camera cameraValue)
        {
            targetCamera = cameraValue;
        }

        private void Awake()
        {
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }

            bool mobile = Application.isMobilePlatform;
            int memoryMb = SystemInfo.systemMemorySize;
            int graphicsMemoryMb = SystemInfo.graphicsMemorySize;
            IsLowSpecMobile = mobile
                && ((memoryMb > 0 && memoryMb < 3500)
                    || (graphicsMemoryMb > 0 && graphicsMemoryMb < 800));

            AppliedAntiAliasing = IsLowSpecMobile ? 0 : 2;
            AppliedShadowDistance = IsLowSpecMobile ? 22f : mobile ? 38f : 72f;
            QualitySettings.antiAliasing = AppliedAntiAliasing;
            QualitySettings.shadowDistance = AppliedShadowDistance;
            QualitySettings.shadowCascades = IsLowSpecMobile ? 0 : mobile ? 2 : 4;
            QualitySettings.anisotropicFiltering = IsLowSpecMobile
                ? AnisotropicFiltering.Disable
                : AnisotropicFiltering.Enable;
            QualitySettings.vSyncCount = mobile ? 0 : 1;
            Application.targetFrameRate = 60;

            if (targetCamera != null)
            {
                targetCamera.allowMSAA = AppliedAntiAliasing > 0;
            }

            IsApplied = true;
            Debug.Log(
                $"OPEN_WORLD_PRESENTATION_READY: mobile={mobile}, lowSpec={IsLowSpecMobile}, "
                + $"msaa={AppliedAntiAliasing}, shadows={AppliedShadowDistance:F0}m, "
                + $"memory={memoryMb}MB, graphicsMemory={graphicsMemoryMb}MB.");
        }
    }
}

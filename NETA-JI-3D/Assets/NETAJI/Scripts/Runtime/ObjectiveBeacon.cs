using UnityEngine;

namespace NetaJi.Prototype
{
    [RequireComponent(typeof(MissionObjective))]
    public sealed class ObjectiveBeacon : MonoBehaviour
    {
        private MissionObjective objective;
        private Transform marker;
        private Renderer markerRenderer;
        private Vector3 baseLocalPosition;

        private void Awake()
        {
            objective = GetComponent<MissionObjective>();
            GameObject markerObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            markerObject.name = "Current Objective Marker";
            markerObject.transform.SetParent(transform, false);
            markerObject.transform.localScale = new Vector3(0.16f, 0.06f, 0.16f);
            baseLocalPosition = new Vector3(0f, 2.55f, 0f);
            markerObject.transform.localPosition = baseLocalPosition;
            markerRenderer = markerObject.GetComponent<Renderer>();

            Collider markerCollider = markerObject.GetComponent<Collider>();
            if (markerCollider != null)
            {
                Destroy(markerCollider);
            }

            Shader shader = Shader.Find("Standard");
            Material material = new Material(shader);
            material.color = new Color(1f, 0.68f, 0.08f);
            if (material.HasProperty("_EmissionColor"))
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", new Color(0.45f, 0.16f, 0.01f));
            }
            markerRenderer.material = material;
            marker = markerObject.transform;
        }

        private void Update()
        {
            if (markerRenderer == null || objective == null)
            {
                return;
            }

            markerRenderer.enabled = objective.CanInteract;
            if (!markerRenderer.enabled)
            {
                return;
            }

            marker.localPosition = baseLocalPosition + Vector3.up * (Mathf.Sin(Time.time * 3.4f) * 0.15f);
            marker.Rotate(0f, 95f * Time.deltaTime, 0f, Space.Self);
        }
    }
}


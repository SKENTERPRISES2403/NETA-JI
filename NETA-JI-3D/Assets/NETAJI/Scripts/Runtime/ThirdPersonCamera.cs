using UnityEngine;

namespace NetaJi.Prototype
{
    [RequireComponent(typeof(Camera))]
    public sealed class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 targetOffset = new Vector3(0f, 1.45f, 0f);
        [SerializeField] private float distance = 5.2f;
        [SerializeField] private float minDistance = 1.2f;
        [SerializeField] private float pitch = 18f;
        [SerializeField] private float yaw;
        [SerializeField] private float positionSharpness = 16f;
        [SerializeField] private float collisionRadius = 0.28f;
        [SerializeField] private LayerMask collisionMask = ~0;

        private Vector3 smoothedPosition;

        public void SetTarget(Transform value)
        {
            target = value;
        }

        public void SetCollisionMask(LayerMask value)
        {
            collisionMask = value;
        }

        private void Start()
        {
            smoothedPosition = transform.position;
        }

        private void LateUpdate()
        {
            if (target == null || PrototypeInput.Instance == null)
            {
                return;
            }

            Vector2 look = PrototypeInput.Instance.LookDelta;
            yaw += look.x;
            pitch = Mathf.Clamp(pitch - look.y, -10f, 62f);

            Quaternion orbitRotation = Quaternion.Euler(pitch, yaw, 0f);
            Vector3 focus = target.position + targetOffset;
            Vector3 direction = orbitRotation * Vector3.back;
            float resolvedDistance = distance;

            if (Physics.SphereCast(focus, collisionRadius, direction, out RaycastHit hit, distance, collisionMask, QueryTriggerInteraction.Ignore))
            {
                resolvedDistance = Mathf.Clamp(hit.distance - 0.12f, minDistance, distance);
            }

            Vector3 desiredPosition = focus + direction * resolvedDistance;
            smoothedPosition = Vector3.Lerp(smoothedPosition, desiredPosition, 1f - Mathf.Exp(-positionSharpness * Time.deltaTime));
            transform.position = smoothedPosition;
            transform.rotation = Quaternion.LookRotation(focus - transform.position, Vector3.up);
        }
    }
}

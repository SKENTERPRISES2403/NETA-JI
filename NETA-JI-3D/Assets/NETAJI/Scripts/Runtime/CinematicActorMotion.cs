using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class CinematicActorMotion : MonoBehaviour
    {
        [SerializeField] private Vector3 localDestination;
        [SerializeField] private float delay;
        [SerializeField] private float duration = 4f;
        [SerializeField] private bool pingPong;

        private Vector3 startPosition;
        private float startedAt;

        public void Configure(Vector3 destination, float startDelay, float travelDuration, bool shouldPingPong = false)
        {
            localDestination = destination;
            delay = Mathf.Max(0f, startDelay);
            duration = Mathf.Max(0.2f, travelDuration);
            pingPong = shouldPingPong;
        }

        private void OnEnable()
        {
            startPosition = transform.localPosition;
            startedAt = Time.time;
        }

        private void Update()
        {
            float elapsed = Time.time - startedAt - delay;
            if (elapsed <= 0f)
            {
                return;
            }

            float raw = elapsed / duration;
            float progress = pingPong ? Mathf.PingPong(raw, 1f) : Mathf.Clamp01(raw);
            float eased = progress * progress * (3f - 2f * progress);
            Vector3 next = Vector3.Lerp(startPosition, localDestination, eased);
            Vector3 direction = next - transform.localPosition;
            transform.localPosition = next;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.0001f)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(direction.normalized, Vector3.up),
                    1f - Mathf.Exp(-8f * Time.deltaTime));
            }
        }
    }
}

using UnityEngine;

namespace NetaJi.Prototype
{
    public enum WorldMotionKind
    {
        Float,
        Sway,
        Turn
    }

    public sealed class WorldMotion : MonoBehaviour
    {
        [SerializeField] private WorldMotionKind motionKind;
        [SerializeField] private float amount = 0.12f;
        [SerializeField] private float speed = 1f;
        [SerializeField] private Vector3 axis = Vector3.up;

        private Vector3 startPosition;
        private Quaternion startRotation;
        private float phase;

        public void Configure(WorldMotionKind kind, float motionAmount, float motionSpeed, Vector3 motionAxis)
        {
            motionKind = kind;
            amount = motionAmount;
            speed = motionSpeed;
            axis = motionAxis.normalized;
        }

        private void Start()
        {
            startPosition = transform.localPosition;
            startRotation = transform.localRotation;
            phase = Mathf.Abs(GetInstanceID() % 97) * 0.13f;
        }

        private void Update()
        {
            float wave = Mathf.Sin(Time.time * speed + phase);
            switch (motionKind)
            {
                case WorldMotionKind.Float:
                    transform.localPosition = startPosition + axis * (wave * amount);
                    break;
                case WorldMotionKind.Sway:
                    transform.localRotation = startRotation * Quaternion.AngleAxis(wave * amount, axis);
                    break;
                case WorldMotionKind.Turn:
                    transform.localRotation = startRotation * Quaternion.AngleAxis(Time.time * speed * amount, axis);
                    break;
            }
        }
    }
}

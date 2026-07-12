using UnityEngine;

namespace NetaJi.Prototype
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class ProceduralWalker : MonoBehaviour
    {
        [SerializeField] private float strideDegrees = 24f;
        [SerializeField] private float strideFrequency = 7.5f;
        [SerializeField] private float bodyBob = 0.025f;

        private CharacterController characterController;
        private Transform torso;
        private Transform leftLeg;
        private Transform rightLeg;
        private Transform leftArm;
        private Transform rightArm;
        private Vector3 torsoBasePosition;
        private float phase;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            torso = transform.Find("Torso");
            leftLeg = transform.Find("Leg Left");
            rightLeg = transform.Find("Leg Right");
            leftArm = transform.Find("Arm Left");
            rightArm = transform.Find("Arm Right");
            if (torso != null)
            {
                torsoBasePosition = torso.localPosition;
            }
        }

        private void LateUpdate()
        {
            Vector3 planarVelocity = characterController.velocity;
            planarVelocity.y = 0f;
            float speed = planarVelocity.magnitude;
            float movementBlend = Mathf.Clamp01(speed / 2.2f);
            phase += speed * strideFrequency * Time.deltaTime;
            float swing = Mathf.Sin(phase) * strideDegrees * movementBlend;

            RotateLimb(leftLeg, swing);
            RotateLimb(rightLeg, -swing);
            RotateLimb(leftArm, -swing * 0.72f);
            RotateLimb(rightArm, swing * 0.72f);

            if (torso != null)
            {
                Vector3 bobbed = torsoBasePosition + Vector3.up * (Mathf.Abs(Mathf.Sin(phase)) * bodyBob * movementBlend);
                torso.localPosition = Vector3.Lerp(torso.localPosition, bobbed, 1f - Mathf.Exp(-18f * Time.deltaTime));
            }
        }

        private static void RotateLimb(Transform limb, float angle)
        {
            if (limb == null)
            {
                return;
            }

            Quaternion target = Quaternion.Euler(angle, 0f, 0f);
            limb.localRotation = Quaternion.Slerp(limb.localRotation, target, 1f - Mathf.Exp(-16f * Time.deltaTime));
        }
    }
}


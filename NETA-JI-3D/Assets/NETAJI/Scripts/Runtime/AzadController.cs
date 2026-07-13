using UnityEngine;

namespace NetaJi.Prototype
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class AzadController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float walkSpeed = 3.1f;
        [SerializeField] private float runSpeed = 5.1f;
        [SerializeField] private float acceleration = 12f;
        [SerializeField] private float rotationSharpness = 13f;
        [SerializeField] private float gravity = -22f;
        [SerializeField] private float interactionRadius = 2.25f;
        [SerializeField] private LayerMask interactionMask = ~0;

        private readonly Collider[] nearby = new Collider[24];
        private CharacterController characterController;
        private Vector3 planarVelocity;
        private float verticalVelocity;
        private float nextFootstepAt;
        private IInteractable focusedInteractable;

        public void SetCamera(Transform value)
        {
            cameraTransform = value;
        }

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (PrototypeInput.Instance == null || cameraTransform == null)
            {
                return;
            }
            if (PrototypeHud.Instance != null && PrototypeHud.Instance.IsDecisionOpen)
            {
                planarVelocity = Vector3.zero;
                PrototypeHud.Instance.SetInteractionPrompt(string.Empty);
                return;
            }

            Vector2 input = PrototypeInput.Instance.Move;
            Vector3 forward = cameraTransform.forward;
            forward.y = 0f;
            forward.Normalize();
            Vector3 right = cameraTransform.right;
            right.y = 0f;
            right.Normalize();

            Vector3 desiredDirection = Vector3.ClampMagnitude(forward * input.y + right * input.x, 1f);
            float targetSpeed = PrototypeInput.Instance.RunHeld ? runSpeed : walkSpeed;
            Vector3 desiredVelocity = desiredDirection * targetSpeed;
            planarVelocity = Vector3.MoveTowards(planarVelocity, desiredVelocity, acceleration * Time.deltaTime);

            if (desiredDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f - Mathf.Exp(-rotationSharpness * Time.deltaTime));
            }

            if (characterController.isGrounded && verticalVelocity < 0f)
            {
                verticalVelocity = -2f;
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;
            }

            characterController.Move((planarVelocity + Vector3.up * verticalVelocity) * Time.deltaTime);
            UpdateFootsteps();
            RefreshFocusedInteractable();

            if (PrototypeInput.Instance.ConsumeInteract() && focusedInteractable != null && focusedInteractable.CanInteract)
            {
                focusedInteractable.Interact(this);
            }
        }

        private void UpdateFootsteps()
        {
            if (!characterController.isGrounded || planarVelocity.sqrMagnitude < 0.4f || Time.time < nextFootstepAt)
            {
                return;
            }

            bool running = PrototypeInput.Instance != null && PrototypeInput.Instance.RunHeld;
            PrototypeAudio.Instance?.PlayFootstep(running);
            nextFootstepAt = Time.time + (running ? 0.28f : 0.43f);
        }

        private void RefreshFocusedInteractable()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position + Vector3.up, interactionRadius, nearby, interactionMask, QueryTriggerInteraction.Collide);
            float bestDistance = float.MaxValue;
            IInteractable best = null;

            for (int i = 0; i < count; i++)
            {
                IInteractable candidate = nearby[i].GetComponentInParent<IInteractable>();
                if (candidate == null || !candidate.CanInteract)
                {
                    continue;
                }

                float distance = (nearby[i].transform.position - transform.position).sqrMagnitude;
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    best = candidate;
                }
            }

            focusedInteractable = best;
            PrototypeHud.Instance?.SetInteractionPrompt(best?.Prompt ?? string.Empty);
        }
    }
}

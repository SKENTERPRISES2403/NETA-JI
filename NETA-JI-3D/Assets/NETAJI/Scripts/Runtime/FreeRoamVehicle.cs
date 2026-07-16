using UnityEngine;

namespace NetaJi.Prototype
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class FreeRoamVehicle : MonoBehaviour, IInteractable
    {
        [SerializeField] private string vehicleName = "Vehicle";
        [SerializeField] private Transform seat;
        [SerializeField] private Transform exitPoint;
        [SerializeField] private Transform cameraTarget;
        [SerializeField] private WheelCollider[] motorWheels;
        [SerializeField] private WheelCollider[] steeringWheels;
        [SerializeField] private WheelCollider[] allWheels;
        [SerializeField] private Transform[] wheelVisuals;
        [SerializeField] private GameObject riderVisual;
        [SerializeField] private float motorTorque = 820f;
        [SerializeField] private float brakeTorque = 1200f;
        [SerializeField] private float maxSteerAngle = 28f;
        [SerializeField] private float maxSpeedKph = 68f;
        [SerializeField] private float cameraDistance = 7.5f;

        private Rigidbody vehicleBody;
        private AzadController driver;
        private ThirdPersonCamera orbitCamera;
        private bool isDriving;
        private bool automationInputEnabled;
        private Vector2 automationInput;
        private int groundedWheelCount;
        private bool chassisGrounded;

        public string VehicleName => vehicleName;
        public string Prompt => $"{vehicleName} chalayein";
        public bool CanInteract => !isDriving;
        public bool IsDriving => isDriving;
        public int WheelCount => allWheels?.Length ?? 0;
        public int GroundedWheelCount => groundedWheelCount;
        public bool HasGroundContact => groundedWheelCount > 0 || chassisGrounded;
        public float SpeedKph => vehicleBody != null ? vehicleBody.linearVelocity.magnitude * 3.6f : 0f;
        public bool RiderVisible => riderVisual != null && riderVisual.activeSelf;

        public void Configure(
            string displayName,
            Transform seatTransform,
            Transform exitTransform,
            Transform cameraTransform,
            WheelCollider[] motors,
            WheelCollider[] steering,
            WheelCollider[] wheels,
            Transform[] visuals,
            GameObject rider,
            float torque,
            float braking,
            float steer,
            float speed,
            float followDistance)
        {
            vehicleName = displayName;
            seat = seatTransform;
            exitPoint = exitTransform;
            cameraTarget = cameraTransform;
            motorWheels = motors;
            steeringWheels = steering;
            allWheels = wheels;
            wheelVisuals = visuals;
            riderVisual = rider;
            motorTorque = torque;
            brakeTorque = braking;
            maxSteerAngle = steer;
            maxSpeedKph = speed;
            cameraDistance = followDistance;
            if (riderVisual != null)
            {
                riderVisual.SetActive(false);
            }
        }

        private void Awake()
        {
            vehicleBody = GetComponent<Rigidbody>();
            vehicleBody.centerOfMass = new Vector3(0f, -0.45f, 0f);
            vehicleBody.maxAngularVelocity = 5f;
            if (riderVisual != null)
            {
                riderVisual.SetActive(false);
            }
        }

        public void Interact(AzadController player)
        {
            if (!CanInteract || player == null)
            {
                return;
            }
            driver = player;
            isDriving = true;
            orbitCamera = FindFirstObjectByType<ThirdPersonCamera>();
            player.SetControlEnabled(false);
            player.SetVisible(false);
            player.SetCollisionEnabled(false);
            player.Teleport(seat.position, seat.rotation);
            if (riderVisual != null)
            {
                riderVisual.SetActive(true);
            }
            orbitCamera?.SetTarget(cameraTarget != null ? cameraTarget : transform, new Vector3(0f, 1.5f, 0f), cameraDistance, true);
            orbitCamera?.SetHeadingFollow(true);
            PrototypeInput.Instance?.SetActionLabel("EXIT");
            FreeRoamMapHud.Instance?.SetInteractionPrompt(string.Empty);
            FreeRoamMapHud.Instance?.SetVehicleStatus(vehicleName, 0f);
        }

        public void SetAutomationInput(float throttle, float steering)
        {
            automationInputEnabled = true;
            automationInput = new Vector2(Mathf.Clamp(steering, -1f, 1f), Mathf.Clamp(throttle, -1f, 1f));
        }

        public void StopAutomationInput()
        {
            automationInputEnabled = false;
            automationInput = Vector2.zero;
        }

        public void ExitForAutomation()
        {
            ExitVehicle();
        }

        private void Update()
        {
            if (!isDriving || driver == null)
            {
                return;
            }

            driver.transform.SetPositionAndRotation(seat.position, seat.rotation);
            FreeRoamMapHud.Instance?.SetVehicleStatus(vehicleName, SpeedKph);
            if (PrototypeInput.Instance != null && PrototypeInput.Instance.ConsumeInteract())
            {
                ExitVehicle();
            }
        }

        private void FixedUpdate()
        {
            if (vehicleBody == null)
            {
                return;
            }

            PrototypeInput input = PrototypeInput.Instance;
            Vector2 movement = automationInputEnabled
                ? automationInput
                : input != null ? input.Move : Vector2.zero;
            float throttle = isDriving ? movement.y : 0f;
            float steering = isDriving ? movement.x : 0f;
            float speedKph = SpeedKph;
            float availableTorque = speedKph < maxSpeedKph || throttle * Vector3.Dot(vehicleBody.linearVelocity, transform.forward) < 0f
                ? throttle * motorTorque
                : 0f;
            foreach (WheelCollider wheel in motorWheels)
            {
                if (wheel != null)
                {
                    wheel.motorTorque = availableTorque;
                }
            }
            foreach (WheelCollider wheel in steeringWheels)
            {
                if (wheel != null)
                {
                    wheel.steerAngle = steering * maxSteerAngle;
                }
            }

            groundedWheelCount = 0;
            foreach (WheelCollider wheel in allWheels)
            {
                if (wheel != null && wheel.isGrounded)
                {
                    groundedWheelCount++;
                }
            }

            int groundMask = ~(1 << 2);
            chassisGrounded = Physics.SphereCast(
                transform.position + Vector3.up * 0.55f,
                0.16f,
                Vector3.down,
                out _,
                1.15f,
                groundMask,
                QueryTriggerInteraction.Ignore);

            bool accelerating = isDriving && HasGroundContact && Mathf.Abs(throttle) > 0.02f && speedKph < maxSpeedKph;
            if (accelerating)
            {
                Vector3 driveForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
                vehicleBody.AddForce(driveForward * (throttle * motorTorque * 2.4f), ForceMode.Force);
                vehicleBody.AddForce(Vector3.down * vehicleBody.mass * 2.1f, ForceMode.Force);

                float steeringAuthority = Mathf.Clamp01(speedKph / 9f);
                vehicleBody.AddTorque(
                    Vector3.up * (steering * maxSteerAngle * vehicleBody.mass * 0.10f * steeringAuthority),
                    ForceMode.Force);
            }

            bool braking = !isDriving || Mathf.Abs(throttle) < 0.05f && speedKph > 1f;
            foreach (WheelCollider wheel in allWheels)
            {
                if (wheel != null)
                {
                    wheel.brakeTorque = braking ? brakeTorque * (isDriving ? 0.12f : 1f) : 0f;
                }
            }
        }

        private void LateUpdate()
        {
            int count = Mathf.Min(allWheels?.Length ?? 0, wheelVisuals?.Length ?? 0);
            for (int i = 0; i < count; i++)
            {
                if (allWheels[i] == null || wheelVisuals[i] == null)
                {
                    continue;
                }
                allWheels[i].GetWorldPose(out Vector3 position, out Quaternion rotation);
                wheelVisuals[i].SetPositionAndRotation(position, rotation * Quaternion.Euler(0f, 0f, 90f));
            }
        }

        private void ExitVehicle()
        {
            if (driver == null)
            {
                return;
            }

            AzadController exitingDriver = driver;
            driver = null;
            isDriving = false;
            if (riderVisual != null)
            {
                riderVisual.SetActive(false);
            }
            exitingDriver.Teleport(exitPoint.position, exitPoint.rotation);
            exitingDriver.SetVisible(true);
            exitingDriver.SetCollisionEnabled(true);
            exitingDriver.SetControlEnabled(true);
            orbitCamera?.RestorePedestrianTarget(exitingDriver.transform, true);
            PrototypeInput.Instance?.SetActionLabel("USE");
            FreeRoamMapHud.Instance?.SetInteractionPrompt(string.Empty);
            FreeRoamMapHud.Instance?.ClearVehicleStatus();
        }
    }
}

using UnityEngine;

namespace BarthaSzabolcs.IsometricAiming
{
    [RequireComponent(typeof(Rigidbody))]
    public class IsometricAiming : MonoBehaviour
    {
        [SerializeField] private LayerMask groundMask;
        private Camera mainCamera;
        private Rigidbody rb;

        [SerializeField] private float rotationSpeed = 10f;

        private bool isAimingEnabled = true;

        private void Awake()
        {
            mainCamera = Camera.main;
            rb = GetComponent<Rigidbody>();

            // Trava rotação nos eixos indesejados
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        private void FixedUpdate()
        {
            if (!isAimingEnabled) return;

            Aim();
        }

        private void Aim()
        {
            var (success, position) = GetMousePosition();

            if (!success || position == Vector3.zero)
                return;

            Vector3 direction = position - transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            }
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
            {
                return (true, hitInfo.point);
            }

            return (false, Vector3.zero);
        }

        public void SetAiming(bool state)
        {
            isAimingEnabled = state;
        }
    }
}

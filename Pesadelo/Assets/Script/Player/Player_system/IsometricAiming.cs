using UnityEngine;

namespace BarthaSzabolcs.IsometricAiming
{
    public class IsometricAiming : MonoBehaviour
    {
        [SerializeField] private LayerMask groundMask;
        private Camera mainCamera;
        private PlayerMovement playerMovement;

        private bool isAimingEnabled = true; // Nova variável de controle

        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!isAimingEnabled) return;

            Aim(); // Sempre mira, inclusive no dash
        }
        private void Aim()
        {
            var (success, position) = GetMousePosition();

            // Falha no Raycast OU posição inválida
            if (!success || position == Vector3.zero)
            {
                return;
            }

            var direction = position - transform.position;
            direction.y = 0;

            // Só rotaciona se o vetor for válido
            if (direction.sqrMagnitude > 0.01f)
            {
                transform.forward = direction;
            }
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
            {
                return (true, hitInfo.point);
            }
            else
            {
                return (false, Vector3.zero);
            }
        }

        // Função pública para ativar/desativar mira
        public void SetAiming(bool state)
        {
            isAimingEnabled = state;
        }
    }
}

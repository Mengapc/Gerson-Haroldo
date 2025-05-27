using UnityEngine;

namespace BarthaSzabolcs.IsometricAiming
{
    public class IsometricAiming : MonoBehaviour
    {
        [SerializeField] private LayerMask groundMask;
        private Camera mainCamera;

        private bool isAimingEnabled = true; // Nova variável de controle

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (isAimingEnabled) // Só mira se estiver ativado
            {
                Aim();
            }
        }

        private void Aim()
        {
            var (success, position) = GetMousePosition();
            if (!success)
            {
                return; // Interrompe a execução se não acertar o chão
            }

            var direction = position - transform.position;
            direction.y = 0;

            // Garante que não vai tentar normalizar ou aplicar direção nula
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
        public void SetAiming()
        {
            isAimingEnabled = !isAimingEnabled;
        }
    }
}

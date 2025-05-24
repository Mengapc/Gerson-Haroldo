using UnityEngine;

public class CameraTargetFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float followDistance = 2f;
    public float smoothSpeed = 5f;

    private Vector3 lastPlayerPosition;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        lastPlayerPosition = playerTransform.position;
    }

    void LateUpdate()
    {
        Vector3 movementDirection = playerTransform.position - lastPlayerPosition;

        // Se estiver parado, usa a última direção
        if (movementDirection.magnitude > 0.01f)
        {
            movementDirection.Normalize();
        }

        Vector3 targetPos = playerTransform.position + movementDirection * followDistance;
        targetPos.y = playerTransform.position.y; // Mantém altura igual ao jogador

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 1f / smoothSpeed);

        lastPlayerPosition = playerTransform.position;
    }
}
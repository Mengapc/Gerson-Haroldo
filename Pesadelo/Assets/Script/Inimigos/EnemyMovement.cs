using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;  // Referência ao jogador
    public float moveSpeed = 1.5f;  // Velocidade do inimigo
    public float detectionRange = 5f;  // Distância para detectar o jogador
    public float rotationSpeed = 5f;  // Velocidade de rotação do inimigo

    private void Update()
    {
        // Verifica a distância entre o inimigo e o jogador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Se o jogador estiver a 5 ou menos unidades de distância
        if (distanceToPlayer <= detectionRange)
        {
            // O inimigo olha para o jogador
            RotateTowardsPlayer();

            // O inimigo se move em direção ao jogador
            MoveTowardsPlayer();
        }
    }

    private void RotateTowardsPlayer()
    {
        // Cria um vetor que aponta na direção do jogador
        Vector3 direction = (player.position - transform.position).normalized;

        // Calcula a rotação necessária para que o inimigo olhe para o jogador
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Aplica a rotação ao inimigo suavemente
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowardsPlayer()
    {
        // Cria um vetor de direção para o movimento
        Vector3 direction = (player.position - transform.position).normalized;

        // Move o inimigo em direção ao jogador
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}

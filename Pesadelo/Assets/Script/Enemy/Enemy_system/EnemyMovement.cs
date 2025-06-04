using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;  // Referência ao jogador
    public bool canMove = true;
    public float moveSpeed = 1.5f;  // Velocidade do inimigo
    public float detectionRange = 5f;  // Distância para detectar o jogador
    public float rotationSpeed = 5f;  // Velocidade de rotação do inimigo

    private void Start()
    {
        // Busca automaticamente o objeto com a tag "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Nenhum objeto com a tag 'Player' foi encontrado na cena.");
        }
    }

    private void Update()
    {
        if (player == null || !canMove)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            RotateTowardsPlayer();
            MoveTowardsPlayer();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    public void SetCanMove(bool state)
    {
        canMove = state;
    }
}

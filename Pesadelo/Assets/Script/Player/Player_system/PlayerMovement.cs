using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;                // Velocidade normal
    public float dashSpeed = 6f;             // Velocidade do dash
    public float dashDuration = 0.2f;        // Duração do dash em segundos
    public float dashCooldown = 1f;          // Tempo de recarga do dash

    public GameObject player;
    public string spawnRoomName = "Spawn(Clone)";

    private Vector3 movementDirection;
    private bool isPlayerMoving = true;
    private GameObject lastSpawnRoom = null;

    private bool isDashing = false;
    private float dashTime = 0f;
    private float dashCooldownTimer = 0f; // Timer para o cooldown
    public bool IsDashing => isDashing;
    public Vector3 CurrentMovementDirection => movementDirection;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isPlayerMoving) return;

        movementDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
            movementDirection += Vector3.right;
        if (Input.GetKey(KeyCode.A))
            movementDirection += Vector3.left;
        if (Input.GetKey(KeyCode.W))
            movementDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            movementDirection += Vector3.back;

        movementDirection = movementDirection.normalized;

        // Atualiza cooldown do dash
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.fixedDeltaTime;

        // Inicia dash se permitido
        if (Input.GetKeyDown(KeyCode.LeftShift) && movementDirection != Vector3.zero && !isDashing && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        float currentSpeed = isDashing ? dashSpeed : speed;

        if (isDashing)
        {
            dashTime -= Time.fixedDeltaTime;
            if (dashTime <= 0)
                isDashing = false;
        }

        rb.MovePosition(rb.position + movementDirection * currentSpeed * Time.fixedDeltaTime);
    }

    // Função para realizar o spawn do jogador
    public void Spawn()
    {
        GameObject currentSpawnRoom = GameObject.Find(spawnRoomName);

        if (currentSpawnRoom == null)
        {
            Debug.LogError("Spawn room não encontrado!");
            return;
        }

        // Só faz o teleport se a sala de spawn mudou
        if (currentSpawnRoom != lastSpawnRoom)
        {
            Vector3 spawnPosition = currentSpawnRoom.transform.position;
            player.transform.position = spawnPosition + Vector3.up;
            lastSpawnRoom = currentSpawnRoom; // Atualiza a referência
            Debug.Log("Teleportado para nova sala de spawn: " + spawnPosition);
        }
        else
        {
            Debug.Log("Spawn room ainda é o mesmo, teleport cancelado.");
        }
    }

    // Função para bloquear a movimentação após o spawn, se necessário
    public void BlockMovement()
    {
        isPlayerMoving = !isPlayerMoving;
    }
}
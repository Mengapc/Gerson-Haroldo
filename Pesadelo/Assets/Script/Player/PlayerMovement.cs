using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public GameObject player; // O seu player
    public string spawnRoomName = "Spawn(Clone)"; // Nome da sala de spawn

    // Variáveis para movimentação
    private Vector3 movementDirection;
    private bool isPlayerMoving = true; // Controla se o player pode se mover
    private GameObject lastSpawnRoom = null;  // Referência ao último spawn usado

    void Update()
    {
        // Se o player não estiver se movendo (spawn ocorreu), ele não pode andar
        if (isPlayerMoving)
        {
            movementDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.D))
            {
                movementDirection += Vector3.right * speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movementDirection += Vector3.left * speed;
            }
            if (Input.GetKey(KeyCode.W))
            {
                movementDirection += Vector3.forward * speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementDirection += Vector3.back * speed;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 2.75f;
            }
            else
            {
                speed = 2.0f;
            }

            // Aplica o movimento ao jogador
            transform.position += movementDirection * speed * Time.deltaTime;
        }
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
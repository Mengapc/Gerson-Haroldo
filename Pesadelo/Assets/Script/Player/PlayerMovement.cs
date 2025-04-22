using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.0f;
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
                movementDirection += Vector3.right;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movementDirection += Vector3.left;
            }
            if (Input.GetKey(KeyCode.W))
            {
                movementDirection += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementDirection += Vector3.back;
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
        Vector3 spawnPosition = currentSpawnRoom.GetComponent<Renderer>().bounds.center;
        player.transform.position = spawnPosition;
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
        isPlayerMoving = false;
    }
}

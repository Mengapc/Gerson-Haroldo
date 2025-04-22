using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public GameObject player; // O seu player
    public string spawnRoomName = "Spawn(Clone)"; // Nome da sala de spawn

    // Variáveis para movimentação
    private Vector3 movementDirection;

    bool isSpawning = false;

	public void Spawn()
	{
		isSpawning = true;
		// Encontre a sala de spawn
		GameObject spawnRoom = GameObject.Find(spawnRoomName);

		// Pega a posição central da sala de spawn
		Vector3 spawnPosition = spawnRoom.GetComponent<Renderer>().bounds.center;

		// Define a posição do player na posição central da sala
		player.transform.position = spawnPosition;

		// Coloca o jogador em uma "posição segura" (sem movimento por enquanto)
		player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero; // Se estiver usando Rigidbody
	}

	void Update()
	{
		if (isSpawning) return; // Ignora movimento caso esteja spawnando

		// O código de movimentação normal aqui
		movementDirection = Vector3.zero;
		if (Input.GetKey(KeyCode.D)) movementDirection += Vector3.right;
		if (Input.GetKey(KeyCode.A)) movementDirection += Vector3.left;
		if (Input.GetKey(KeyCode.W)) movementDirection += Vector3.forward;
		if (Input.GetKey(KeyCode.S)) movementDirection += Vector3.back;

		transform.position += movementDirection * speed * Time.deltaTime;
	}
}
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player; // O seu player
    public string spawnRoomName = "Spawn"; // Nome da sala de spawn
    
    public void Spawn()
    {
        // Encontre a sala de spawn
        GameObject spawnRoom = GameObject.Find(spawnRoomName);

        if (spawnRoom != null)
        {
            // Pega a posição central da sala
            Vector3 spawnPosition = spawnRoom.GetComponent<Renderer>().bounds.center;

            // Define a posição do player na posição central da sala
            player.transform.position = spawnPosition;
        }
        else
        {
            Debug.LogError("Sala de spawn não encontrada!");
        }
    }
}
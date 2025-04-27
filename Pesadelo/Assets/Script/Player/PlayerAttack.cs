using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject cubePrefab; // Assign a Cube prefab in the inspector
    public float shootForce = 500f;
    public Transform shootOrigin; // Assign the place where cubes should spawn (e.g., camera or gun barrel)
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 1f); // Forward 1, Up 0.5
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left mouse button or Ctrl
        {
            ShootCube();
        }
    }

    void ShootCube()
    {
        if (cubePrefab == null || shootOrigin == null)
        {
            Debug.LogWarning("Cube Prefab or Shoot Origin is not assigned!");
            return;
        }//Catches exeption

        Vector3 offsetPosition = 
            shootOrigin.position +
            shootOrigin.forward * spawnOffset.z +
            shootOrigin.up * spawnOffset.y +
            shootOrigin.right * spawnOffset.x;

        GameObject cube = Instantiate(cubePrefab, offsetPosition, shootOrigin.rotation);

        Rigidbody rb = cube.GetComponent<Rigidbody>();

        rb.AddForce(shootOrigin.forward * shootForce);
    }
}

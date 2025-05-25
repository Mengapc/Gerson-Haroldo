using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject cubePrefab; // Assign in inspector
    public float shootForce = 500f;
    public Transform shootOrigin; // Assign in inspector
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 1f);
    public Transform equipArm; // Assign the EquipArm object in the inspector

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Always get the current weapon
            ItemInstance arma = equipArm.GetComponentInChildren<ItemInstance>();

            if (arma == null)
            {
                Debug.LogWarning("No weapon equipped.");
                return;
            }

            switch (arma.type)
            {
                case Armas.ItemType.Staff:
                    ShootCube(); // Cajado
                    break;
                case Armas.ItemType.Sword:
                    Debug.Log("Attacking with Sword");
                    break;
                case Armas.ItemType.Hammer:
                    Debug.Log("Attacking with Hammer");
                    break;
                default:
                    Debug.LogWarning("Unknown weapon type.");
                    break;
            }
        }
        }
    void ShootCube()
    {
        if (cubePrefab == null || shootOrigin == null)
        {
            Debug.LogWarning("Cube Prefab or Shoot Origin is not assigned!");
            return;
        }

        Vector3 offsetPosition =
            shootOrigin.position +
            shootOrigin.forward * spawnOffset.z +
            shootOrigin.up * spawnOffset.y +
            shootOrigin.right * spawnOffset.x;

        GameObject cube = Instantiate(cubePrefab, offsetPosition, shootOrigin.rotation);
    }

}


using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject ataqueCajadoPrefab; // Assign in inspector
    public GameObject ataqueEspadaPrefab;
    public GameObject ataqueMarteloPrefab;
    public float shootForce = 500f;
    public Transform shootOrigin; // Assign in inspector
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 1f);
    public Transform equipArm; // Assign the EquipArm object in the inspector
    public bool debug = true;

    void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // Always get the current weapon
                ItemInstance arma = equipArm.GetComponentInChildren<ItemInstance>();
        
                if (arma == null)
                {
                    if (debug)
                    {
                        ShootCajado(); // Debug mode: test staff shot
                    }
                    else
                    {
                        Debug.LogWarning("No weapon equipped.");
                    }
        
                    return; // Don't proceed to switch
                }
        
                switch (arma.type)
                {
                    case Armas.ItemType.Staff:
                        ShootCajado();
                        break;
                    case Armas.ItemType.Sword:
                        ShootSword();
                        break;
                    case Armas.ItemType.Hammer:
                        Debug.Log("Attacking with Hammer");
                        break;
                    default:
                        Debug.LogWarning("Unknown weapon type.");
                        break;
                }
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
        
    void ShootCajado()
    {
        if (ataqueCajadoPrefab == null || shootOrigin == null)
        {
            Debug.LogWarning("Cube Prefab or Shoot Origin is not assigned!");
            return;
        }

        Vector3 offsetPosition =
            shootOrigin.position +
            shootOrigin.forward * spawnOffset.z +
            shootOrigin.up * spawnOffset.y +
            shootOrigin.right * spawnOffset.x;

        GameObject cube = Instantiate(ataqueCajadoPrefab, offsetPosition, shootOrigin.rotation);

        Rigidbody rb = cube.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootOrigin.forward * shootForce);
        }
    }
    void ShootSword()
    {
        if (ataqueEspadaPrefab == null || shootOrigin == null)
        {
            Debug.LogWarning("espada ataque Prefab or Shoot Origin is not assigned!");
            return;
        }

        Vector3 offsetPosition =
            shootOrigin.position +
            shootOrigin.forward * spawnOffset.z +
            shootOrigin.up * spawnOffset.y +
            shootOrigin.right * spawnOffset.x;

        GameObject ataqueEspada = Instantiate(ataqueEspadaPrefab, offsetPosition, shootOrigin.rotation);

        Rigidbody rb = ataqueEspada.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootOrigin.forward * shootForce);
        }
    }
}


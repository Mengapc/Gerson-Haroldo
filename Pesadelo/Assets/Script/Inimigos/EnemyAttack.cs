using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10;  // Amount of damage the enemy deals
    public Transform player;  // Reference to the player's transform

    // On collision or trigger, deal damage to the player
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth script from the player and apply damage
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}

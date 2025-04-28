using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health value
    public int currentHealth = 100;  // Current health value
    public TextMeshProUGUI healthText;  // TextMeshProUGUI reference to display health (make sure this is public)
    public PlayerMovement playerMovement;

    // Initialize the player's health
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Function to handle damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;  // Reduce health by damage amount
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();  // Call Die if health reaches 0
        }
        UpdateHealthUI();
    }

    // Function to update the health UI (Optional)
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;  // Display the current health
        }
    }

    // Function to handle death (for example, restarting the level or ending the game)
    private void Die()
    {
        Debug.Log("Player Died!");
        playerMovement.BlockMovement();
        StartCoroutine(DelayedDeath());
        // Add your game-over logic or respawn logic here
    }
    private IEnumerator DelayedDeath()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos
        Destroy(gameObject); // Destroi o objeto onde esse script está
    }
}

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
        playerMovement.BlockMovement();  // Bloqueia o movimento do player
        StartCoroutine(DelayedDeath());
        StartCoroutine(BlinkPlayer());  // Adicionando o efeito de piscar
    }

    private IEnumerator DelayedDeath()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos
        playerMovement.Spawn(); // Chama a função de spawn
        currentHealth = maxHealth; // Restaura a saúde do player (opcional, se quiser restaurar a vida ao renascer)
        UpdateHealthUI();
        playerMovement.BlockMovement(); // Libera o movimento do player após renascer
    }

    private IEnumerator BlinkPlayer()
    {
        Renderer playerRenderer = playerMovement.player.GetComponent<Renderer>(); // Obtém o componente Renderer do player
        Color originalColor = playerRenderer.material.color;  // Guarda a cor original do player

        // Faz o player "piscar" por 1 segundo
        for (float t = 0; t < 1f; t += Time.deltaTime * 5f) // Duração do piscar
        {
            playerRenderer.material.color = Color.Lerp(originalColor, new Color(1f, 1f, 1f, 0f), t); // Torna o player transparente
            yield return null;
        }

        playerRenderer.material.color = new Color(1f, 1f, 1f, 0f); // Deixa o player completamente invisível
        yield return new WaitForSeconds(0.2f); // Espera um pouco antes de reverter a cor

        // Restaura a cor original
        for (float t = 0; t < 1f; t += Time.deltaTime * 5f)
        {
            playerRenderer.material.color = Color.Lerp(new Color(1f, 1f, 1f, 0f), originalColor, t);
            yield return null;
        }
        playerRenderer.material.color = originalColor; // Restaura a cor original
    }

}

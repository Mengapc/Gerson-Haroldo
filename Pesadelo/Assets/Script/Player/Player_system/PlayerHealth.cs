using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using System.Collections;
using BarthaSzabolcs.IsometricAiming;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health value
    public int currentHealth = 100;  // Current health value
    public float respawnTime = 3f; // Current Respawn Time value
    public TextMeshProUGUI healthText;  // TextMeshProUGUI reference to display health (make sure this is public)
    public TextMeshProUGUI deathText;  // TextMeshProUGUI reference to display player's death (make sure this is public)
    public PlayerMovement playerMovement;
    public IsometricAiming isometricAiming;
    public Rigidbody rb;

    // Initialize the player's health
    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
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
        playerMovement.BlockMovement();  // Bloqueia o movimento do player

        // BLOQUEIA TODOS OS INIMIGOS
        EnemyMovement[] allEnemies = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
        foreach (EnemyMovement enemy in allEnemies)
        {
            enemy.SetCanMove(false);
        }

        isometricAiming.SetAiming(false); // Desativa mira
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        StartCoroutine(DelayedDeath());
        StartCoroutine(BlinkPlayer());
    }

    private IEnumerator DelayedDeath()
    {
        float remainingTime = respawnTime;

        while (remainingTime > 0f)
        {
            deathText.text = "SE FUDEU\nRevivendo em: " + Mathf.CeilToInt(remainingTime);
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }
        deathText.text = ""; // Limpa o texto
        currentHealth = maxHealth; // Restaura a saúde
        UpdateHealthUI();
        playerMovement.BlockMovement(); // Libera o movimento (toggle)
        isometricAiming.SetAiming(true); // Reativa mira
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        EnemyMovement[] allEnemies = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
        foreach (EnemyMovement enemy in allEnemies)
        {
            enemy.SetCanMove(true);
        }
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
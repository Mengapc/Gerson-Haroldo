using UnityEngine;
using System.Collections; // Necessário para usar Coroutines

public class EnemySistem : MonoBehaviour
{
    public Transform player;
    public bool canMove = true;
    public float moveSpeed = 1.5f;
    public float detectionRange = 5f;
    public float rotationSpeed = 5f;

    // --- NOVAS VARIÁVEIS PARA O SLOW ---
    private float originalMoveSpeed;      // Guarda a velocidade original
    private Coroutine slowCoroutine;      // Referência para a corrotina de slow

    public int damageAmount = 10;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = -Mathf.Infinity;

    public int maxHealth = 3;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        // --- GUARDA A VELOCIDADE ORIGINAL NO INÍCIO ---
        originalMoveSpeed = moveSpeed;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Nenhum objeto com a tag 'Player' foi encontrado na cena.");
        }
    }

    private void Update()
    {
        if (player == null || !canMove)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            RotateTowardsPlayer();
            MoveTowardsPlayer();
        }
    }

    // ... (outros métodos como OnTriggerEnter, RotateTowardsPlayer, etc. continuam iguais) ...

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    lastAttackTime = Time.time;
                }
            }
        }
    }
    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
    public void SetCanMove(bool state)
    {
        canMove = state;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    // --- FUNÇÃO PÚBLICA PARA APLICAR LENTIDÃO ---
    /// <summary>
    /// Aplica lentidão ao inimigo por um tempo determinado.
    /// </summary>
    /// <param name="slowFactor">Fator de lentidão (0.5 para 50% de slow, 0.8 para 80%, etc).</param>
    /// <param name="duration">Duração do efeito em segundos.</param>
    public void SlowEnemy(float slowFactor, float duration)
    {
        // Se já existe uma corrotina de slow rodando, pare-a.
        // Isso garante que o novo efeito substitua o antigo.
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        // Inicia a nova corrotina de slow e guarda a referência dela.
        slowCoroutine = StartCoroutine(ApplySlow(slowFactor, duration));
    }

    // --- CORROTINA QUE GERENCIA O EFEITO DE LENTIDÃO ---
    private IEnumerator ApplySlow(float slowFactor, float duration)
    {
        // Garante que o fator de lentidão esteja entre 0 e 1
        slowFactor = Mathf.Clamp01(slowFactor);

        // Aplica a lentidão
        moveSpeed = originalMoveSpeed * (1f - slowFactor);

        // Espera pela duração do efeito
        yield return new WaitForSeconds(duration);

        // Restaura a velocidade original
        moveSpeed = originalMoveSpeed;

        // Limpa a referência da corrotina, pois ela terminou
        slowCoroutine = null;
    }
}
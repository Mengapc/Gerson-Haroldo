using UnityEngine;
using System.Collections;

public class EnemySistem : MonoBehaviour
{
    public Transform player;
    public bool canMove = true;
    public float moveSpeed = 1.5f;
    public float detectionRange = 5f;
    public float rotationSpeed = 5f;

    public int damageAmount = 10;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = -Mathf.Infinity;

    public int maxHealth = 3;
    private int currentHealth;

    private Rigidbody rb;
    private float originalMoveSpeed;
    private Coroutine activeSlowCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("O inimigo não tem um componente Rigidbody! Efeitos de Push/Pull não funcionarão.");
        }

        currentHealth = maxHealth;
        originalMoveSpeed = moveSpeed;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
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

    public void SlowEnemy(float factor, float duration)
    {
        if (activeSlowCoroutine != null)
        {
            StopCoroutine(activeSlowCoroutine);
        }
        activeSlowCoroutine = StartCoroutine(SlowDownCoroutine(factor, duration));
    }

    private IEnumerator SlowDownCoroutine(float factor, float duration)
    {
        moveSpeed = originalMoveSpeed * (1f - Mathf.Clamp01(factor));
        yield return new WaitForSeconds(duration);
        moveSpeed = originalMoveSpeed;
        activeSlowCoroutine = null;
    }

    public void PushFrom(Vector3 sourcePosition, float force)
    {
        if (rb == null) return;
        Vector3 direction = (transform.position - sourcePosition).normalized;
        direction.y = 0;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    public void PullTo(Vector3 destinationPosition, float force)
    {
        if (rb == null) return;
        Vector3 direction = (destinationPosition - transform.position).normalized;
        direction.y = 0;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
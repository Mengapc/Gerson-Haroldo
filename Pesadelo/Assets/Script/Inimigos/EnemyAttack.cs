using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10;  // Dano
    public float attackCooldown = 1.5f; // Tempo entre ataques em segundos
    private float lastAttackTime = -Mathf.Infinity; // Último tempo que atacou

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Só ataca se passou o tempo de cooldown
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    lastAttackTime = Time.time; // Atualiza o tempo do último ataque
                }
            }
        }
    }
}

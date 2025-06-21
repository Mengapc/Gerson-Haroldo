using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    public int bulletDamage = 1;
    public float knockbackForce = 0.001f;
    public float speed = 10f;
    public float destroyDelay = 1f;

    private Rigidbody rb;
    private bool hasHit = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3f); // Destroi a bala após 5 segundos
        
        rb.linearVelocity = transform.forward * speed; // Projétil vai na direção para frente
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        hasHit = true;

        if (other.CompareTag("basic_enemy"))
        {
            EnemySistem health = other.GetComponent<EnemySistem>();
            if (health != null)
                health.TakeDamage(bulletDamage);

            Rigidbody enemyRb = other.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 knockbackDir = transform.forward;
                enemyRb.AddExplosionForce(knockbackForce, transform.position, 1f, 0f, ForceMode.Impulse);
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Paredes"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        // Coloque aqui efeitos de partículas ou som
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
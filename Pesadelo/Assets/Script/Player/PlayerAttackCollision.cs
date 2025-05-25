using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    public int bulletDamage = 1;
    public float knockbackForce = 5f;
    public float speed = 10f;
    public float destroyDelay = 1f;

    private Rigidbody rb;
    private bool hasHit = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody não encontrado no projétil.");
        }

        // ✅ Projétil vai na direção para frente
        rb.linearVelocity = transform.forward * speed;

        Destroy(gameObject, 5f); // Destrói após 5s para segurança
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        hasHit = true;

        if (other.CompareTag("basic_enemy"))
        {
            EnemyHealth health = other.GetComponent<EnemyHealth>();
            if (health != null)
                health.TakeDamage(bulletDamage);

            Rigidbody enemyRb = other.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 knockbackDir = (other.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);
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
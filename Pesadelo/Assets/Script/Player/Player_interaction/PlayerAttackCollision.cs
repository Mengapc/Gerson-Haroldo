using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    public int bulletDamage = 1;
    public float knockbackForce = 0.001f;
    public float speed = 10f;
    public float destroyDelay = 1f;

    private Rigidbody rb;
    private bool hasHit = false;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveDirection = transform.forward.normalized * speed;
        Destroy(gameObject, 3f); // Destroy after 3 seconds
    }

    private void FixedUpdate()
    {
        if (rb == null) return;

        if (rb.isKinematic)
        {
            transform.position += moveDirection * Time.fixedDeltaTime;
        }
        else
        {
            rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        hasHit = true;

        if (other.CompareTag("basic_enemy"))
        {
            Debug.Log("Colidiu com o inimigo");
            EnemySistem health = other.GetComponent<EnemySistem>();
            if (health != null)
                health.TakeDamage(bulletDamage);
                Debug.Log("Dano aplicado");
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
            Destroy(gameObject);
        }
        else
        {
            // Treat all other objects (walls etc.) the same
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero; // replaces invalid .linearVelocity
                rb.isKinematic = true;
            }

            StartCoroutine(DestroyAfterDelay());
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        // Placeholder for particle or sound effect
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}

using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    public int bulletDamage = 1; // Dano que a bala causa

    private void Start()
    {
        Destroy(gameObject, 3f); // Destroi a bala após 5 segundos
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a bala colidiu com um inimigo
        if (collision.gameObject.CompareTag("basic_enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(bulletDamage);
            }
            Destroy(gameObject); // Sempre destrói a bala ao colidir
        }

        //Destroy(gameObject); // Sempre destrói a bala ao colidir
    }

}
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    private void Start()
{
    Destroy(gameObject, 5f); // Destroi o cubo automaticamente após 5 segundos
}

private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Destroi o cubo quando colidir
    }
}

using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;
    private bool isMoving = true;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        isMoving = true;
    }

    public void Stop()
    {
        speed = 0f;
        isMoving = false;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}

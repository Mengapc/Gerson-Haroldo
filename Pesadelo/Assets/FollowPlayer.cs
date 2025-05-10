using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("MiniMap Rotation")]
    public Transform PlayerReference;
    public float PlayerOffset = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerReference != null)
        {
            transform.position = new Vector3(PlayerReference.position.x, PlayerReference.position.y + PlayerOffset, PlayerReference.position.z);
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}

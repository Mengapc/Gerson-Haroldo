using UnityEngine;

public class Sacrifice : MonoBehaviour
{
    
    InventControler ic;
    public GameObject icObject;
    GameObject sacrifice;
    [SerializeField] private GameObject altar;
    public int sacrificLevel;
    public KeyCode input;
    [SerializeField] private float distance;
    [SerializeField] private GameObject player;

    void Start()
    {
        ic = icObject.GetComponent<InventControler>();
        if (ic == null)
        {
            Debug.LogError("InventControler não encontrado na cena!");
        }
    }
    private void Update()
    {

    }

    void Interage()
    {
        if (Vector3.Distance(player.transform.position, altar.transform.position) <= distance && Input.GetKey(input))
        {
          
        }
    }
}


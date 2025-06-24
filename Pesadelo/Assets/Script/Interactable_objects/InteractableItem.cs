using UnityEngine;

[DisallowMultipleComponent]

public class InteractableItem : MonoBehaviour, IInteractable
{
    public string promptMessage = "Pressione [E] para interagir";
    // public string weaponName; Caso sejam adicionados nomes procedurais mais tarde no jogo
    public InventControler inventControler;
    private InventBarSelect inventario;
    public RoomFirstDungeonGenerator roomFirst;

    void Start()
    {
        if (inventControler == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                inventControler = player.GetComponent<InventControler>();
            }
        }
    }

    public void Interact()
    {
        switch (gameObject.tag)
        {
            case "Arm":
                Equip();
                break;

            case "Bau":
                CollectKey();
                break;

            case "Altar":
                Sacrifice();
                break;

            case "Final":
                Endgame();
                break;

            default:
                Debug.Log("Intera��o padr�o: nenhum comportamento definido para a tag " + gameObject.tag);
                break;
        }
    }

    public void Equip()
    {
        Debug.Log("Arma coletada: " //+ weaponName
                                    );
        inventControler.ManagerInvetory(gameObject);
        Destroy(this);
    }

    public void CollectKey()
    {
        Debug.Log("Chave coletada!");
        Destroy(gameObject);
    }

    public void Sacrifice()
    {
        if (inventario.equipArm != null)
        {
            Debug.Log("Voc� interagiu com o altar.");
            inventario.DestroyArm();
        }

    }

public void Endgame()
    {
        if (inventario.equipArm != null)
        {
            Debug.Log("Voc� interagiu com a porta final.");
            roomFirst.CreateRooms();
        }

    }

    public string GetPrompt()
    {
        return promptMessage;
    }
}

using UnityEngine;

[DisallowMultipleComponent]

public class InteractableItem : MonoBehaviour, IInteractable
{
    public string promptMessage = "Pressione [E] para interagir";
    // public string weaponName; Caso sejam adicionados nomes procedurais mais tarde no jogo
    public InventControler inventControler;

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

            default:
                Debug.Log("Interação padrão: nenhum comportamento definido para a tag " + gameObject.tag);
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
        Debug.Log("Você interagiu com o altar. Sacrifício ainda não implementado.");
    }

    public string GetPrompt()
    {
        return promptMessage;
    }
}

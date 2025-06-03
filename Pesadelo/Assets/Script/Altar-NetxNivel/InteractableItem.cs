using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public string promptMessage = "Pressione [E] para coletar";

    public void Collect()
    {
        Debug.Log("Item coletado!");
        Destroy(gameObject); // Ou qualquer outra ação
    }

    public string GetPrompt()
    {
        return promptMessage;
    }
}
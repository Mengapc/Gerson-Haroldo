using UnityEngine;
using TMPro;

public class PlayerInteractor : MonoBehaviour
{
    public float interactionRange = 2f;
    public KeyCode interactKey = KeyCode.E;
    public TextMeshProUGUI interactionText;

    private IInteractable currentInteractable;

    void Update()
    {
        CheckForInteractable();

        if (currentInteractable != null)
        {
            interactionText.text = currentInteractable.GetPrompt();
            interactionText.enabled = true;

            if (Input.GetKeyDown(interactKey))
            {
                currentInteractable.Collect();
                interactionText.enabled = false;
            }
        }
        else
        {
            interactionText.enabled = false;
        }
    }

    void CheckForInteractable()
    {
        currentInteractable = null;

        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (var hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                break;
            }
        }
    }
}

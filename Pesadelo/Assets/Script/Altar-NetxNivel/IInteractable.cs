using UnityEngine;

public interface IInteractable
{
    void Collect();            // Ação ao interagir
    string GetPrompt();         // Texto exibido ao se aproximar
}


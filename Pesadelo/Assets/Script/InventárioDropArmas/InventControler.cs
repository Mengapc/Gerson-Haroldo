using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]

public class InventControler : MonoBehaviour
{
    [Header("Configuracoes gerais")]
    public GameObject[] slots;
    public Image[] slotsSprit;
    public GameObject player;
    public int slotBarSelect = 0;

    public void ManagerInvetory(GameObject arm)
    {
        bool inventoryFull = true;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                inventoryFull = false;
                break;
            }
        }

        if (inventoryFull)
        {
            Debug.Log("Inventario cheio, nao foi possivel adicionar " + arm.name);
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = arm;

                if (i < slotsSprit.Length && slotsSprit[i] != null)
                {
                    Image slotImage = slotsSprit[i].GetComponent<Image>();

                    if (slotImage != null)
                    {
                        ItemInstance itemInstance = arm.GetComponent<ItemInstance>();
                        if (itemInstance != null && itemInstance.spriteArm != null)
                        {
                            slotImage.sprite = itemInstance.spriteArm;
                            slotImage.enabled = true;
                            slotImage.color = Color.white;
                            Debug.Log($"InventControler: Sprite '{itemInstance.spriteArm.name}' atribuído ao slot UI {i}.");
                        }
                        else
                        {
                            if (itemInstance == null)
                            {
                                Debug.LogError($"InventControler: ItemInstance não encontrado no objeto da arma '{arm.name}'.");
                            }
                            else
                            {
                                Debug.LogError($"InventControler: itemInstance.spriteArm é NULO para a arma '{arm.name}'. Verifique a função SetSprite e a configuração da lista armSprits no ProceduralItens.");
                            }
                            slotImage.sprite = null;
                        }
                    }
                    else
                    {
                        Debug.LogError("Nenhum componente Image encontrado no GameObject do slotSprit no indice: " + i);
                    }
                }
                else
                {
                    Debug.LogError("Slot de sprite de inventario (slotsSprit[" + i + "]) invalido ou nao atribuido no Inspector.");
                }

                arm.SetActive(false);
                break;
            }
        }
    }
}
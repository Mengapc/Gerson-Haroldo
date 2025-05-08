using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 

public class InventControler : MonoBehaviour
{
    [Header("Configura��es gerais")]
    private GameObject tempArm = null;
    public GameObject[] slots;
    public Image[] slotsSprit;
    public GameObject player;


    [Header("Configura��es do inpectArm")]

    [SerializeField] float distanceInteractor;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tempArm = inspectArm();
        }

        switch (tempArm)
        {
            case null:
                break;
            default:
                ManagerInvetory(tempArm); 
                tempArm = null; 
                break; 
        }
    }

    private GameObject inspectArm()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        GameObject objetoAtingido = null; 

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                objetoAtingido = hit.collider.gameObject;

              
                if (objetoAtingido.CompareTag("Arm"))
                {
                    if (player != null && Vector3.Distance(objetoAtingido.transform.position, player.transform.position) <= distanceInteractor)
                    {
                        Debug.Log("Objeto inspecionado: " + objetoAtingido.name);
                        return objetoAtingido; 
                    }
                    else
                    {
                        Debug.Log("Objeto fora do alcance.");
                        return null; 
                    }
                }
                else
                {
                    Debug.Log("Objeto n�o � uma arma.");
                    return null; 
                }
            }
            else
            {
                Debug.Log("Nenhum collider atingido.");
                return null;
            }
        }
        else
        {
            Debug.Log("Nenhum objeto atingido.");
            return null; 
        }
    }

    private void ManagerInvetory(GameObject arm)
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
            Debug.Log("Invent�rio cheio, n�o foi poss�vel adicionar " + arm.name);
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = arm; // insere arma dentro do invent�rio

                if (i < slotsSprit.Length && slotsSprit[i] != null)
                {
                    Image slotImage = slotsSprit[i].GetComponent<Image>();

                    
                    if (slotImage != null)
                    {
                        
                        ItemInstance itemInstance = arm.GetComponent<ItemInstance>();
                        if (itemInstance != null && itemInstance.arm != null)
                        {
                            slotImage.sprite = itemInstance.arm.spriteArm;
                        }
                        else
                        {
                            Debug.LogError("Componente ItemInstance ou arm.spriteArm n�o encontrado no objeto da arma.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Nenhum componente Image encontrado no slot de invent�rio no �ndice: " + i);
                    }
                }
                else
                {
                    Debug.LogError("Slot de sprite de invent�rio inv�lido no �ndice: " + i);
                }

                arm.SetActive(false);
                break;
            }
        }
    }
}
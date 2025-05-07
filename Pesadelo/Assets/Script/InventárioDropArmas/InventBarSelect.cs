using Unity.Burst.Intrinsics;
using UnityEngine;

public class InventBarSelect : MonoBehaviour
{
    public float scroll;
    private InventControler ic;
    public GameObject handPlayer;
    [SerializeField]private int slotBarSelect = 0;

    void Start()
    {
        ic = FindObjectOfType<InventControler>();
        if (ic == null)
        {
            Debug.LogError("InventControler não encontrado na cena!");
        }
    }
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            SelectSlotBar();
            EquipArm();
        }
    }

    public void SelectSlotBar()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            slotBarSelect--;
        }
        else if (scroll < 0f)
        {
            slotBarSelect++;
        }

        if (slotBarSelect > 5)
        {
            slotBarSelect = 0;
        }
        else if (slotBarSelect < 0)
        {
            slotBarSelect = 5;
        }

        Debug.Log("Slot Selecionado: " + slotBarSelect);
    }

    public void EquipArm()
    {
        GameObject equipArm = null;
        Rigidbody rigidbody = null;
        for (int i = 0; i < 6; i++)
        {
            if (ic.slots[i] != null)
            {
                equipArm = ic.slots[i];
                equipArm.SetActive(false);  
                equipArm.transform.SetParent(null);
            }
        }

        switch (slotBarSelect)
        {
            case 0:
                if (ic.slots[0] != null)
                {
                    equipArm = ic.slots[0];
                    equipArm.SetActive(true);
                    equipArm.transform.SetPositionAndRotation(handPlayer.transform.position, handPlayer.transform.rotation);
                    equipArm.transform.SetParent(handPlayer.transform);
                    rigidbody = equipArm.GetComponent<Rigidbody>(); ;
                    rigidbody.useGravity = false;
                }
                break;

            case 1:
                if (ic.slots[1] != null)
                {
                    equipArm = ic.slots[1];
                    equipArm.SetActive(true);
                    equipArm.transform.SetPositionAndRotation(handPlayer.transform.position, handPlayer.transform.rotation);
                    equipArm.transform.SetParent(handPlayer.transform);
                    rigidbody = equipArm.GetComponent<Rigidbody>(); ;
                    rigidbody.useGravity = false;
                }
                break;

            case 2:
                if (ic.slots[2] != null)
                {
                    equipArm = ic.slots[2];
                    equipArm.SetActive(true);
                    equipArm.transform.SetPositionAndRotation(handPlayer.transform.position, handPlayer.transform.rotation);
                    equipArm.transform.SetParent(handPlayer.transform);
                    rigidbody = equipArm.GetComponent<Rigidbody>(); ;
                    rigidbody.useGravity = false;
                }
                break;

            case 3:
                if (ic.slots[3] != null)
                {
                    equipArm = ic.slots[3];
                    equipArm.SetActive(true);
                    equipArm.transform.SetPositionAndRotation(handPlayer.transform.position, handPlayer.transform.rotation);
                    equipArm.transform.SetParent(handPlayer.transform);
                    rigidbody = equipArm.GetComponent<Rigidbody>(); ;
                    rigidbody.useGravity = false;
                }
                break;

            case 4:
                if (ic.slots[4] != null)
                {
                    equipArm = ic.slots[4];
                    equipArm.SetActive(true);
                    equipArm.transform.SetPositionAndRotation(handPlayer.transform.position, handPlayer.transform.rotation);
                    equipArm.transform.SetParent(handPlayer.transform);
                    rigidbody = equipArm.GetComponent<Rigidbody>(); ;
                    rigidbody.useGravity = false;
                }
                break;

            case 5:
                if (ic.slots[5] != null)
                {
                    equipArm = ic.slots[5];
                    equipArm.SetActive(true);
                    equipArm.transform.SetPositionAndRotation(handPlayer.transform.position, handPlayer.transform.rotation);
                    equipArm.transform.SetParent(handPlayer.transform);
                    rigidbody = equipArm.GetComponent<Rigidbody>(); ;
                    rigidbody.useGravity = false;
                }
                break;
        }
    }

}

using Unity.Burst.Intrinsics;
using UnityEngine;

public class InventBarSelect : MonoBehaviour
{
    GameObject equipArm = null;
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
        if ( Input.GetKey(KeyCode.E) && equipArm != null)
        {
            Drop(slotBarSelect, equipArm);
            equipArm = null;
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
        if (equipArm != null)
        {
            equipArm.SetActive(false);
            equipArm.transform.SetParent(null);
        }

        if (ic.slots[slotBarSelect] != null)
        {
            Equip(slotBarSelect);
        }
    }

    void Equip(int slot)
    {
        GameObject arma = ic.slots[slot];

        if (arma != null)
        {
            arma.SetActive(true);
            arma.transform.SetPositionAndRotation(handPlayer.transform.position, handPlayer.transform.rotation);
            arma.transform.SetParent(handPlayer.transform);

            Rigidbody rb = arma.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            equipArm = arma;
        }
    }

    void Drop(int slot, GameObject testEquip)
    {
        if (testEquip != null)
        {
            testEquip.SetActive(true);
            testEquip.transform.SetParent(null);
            Vector3 dropPosition = handPlayer.transform.position + handPlayer.transform.forward * 1f;
            testEquip.transform.position = dropPosition;
            Rigidbody rb = testEquip.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.AddForce(handPlayer.transform.forward * 2f, ForceMode.Impulse);
            }
            ic.slots[slot] = null;
            testEquip = null;
            Debug.Log("Item dropado do slot: " + slot);
        }
    }
}

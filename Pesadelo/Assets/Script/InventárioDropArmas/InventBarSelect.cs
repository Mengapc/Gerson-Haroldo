using UnityEngine;

public class InventBarSelect : MonoBehaviour
{
    private GameObject equipArm = null;
    private InventControler ic;
    public GameObject handPlayer;
    public float scroll;

    void Start()
    {
        ic = FindObjectOfType<InventControler>();
        if (ic == null)
        {
            Debug.LogError("InventControler nao encontrado na cena!");
        }
    }

    void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            SelectSlotBar();
            EquipArm();
        }

        if (Input.GetKey(KeyCode.Q) && equipArm != null)
        {
            Drop(ic.slotBarSelect, equipArm);
            equipArm = null;
        }
    }

    void SelectSlotBar()
    {
        if (scroll > 0f) ic.slotBarSelect--;
        else if (scroll < 0f) ic.slotBarSelect++;

        if (ic.slotBarSelect > 5) ic.slotBarSelect = 0;
        else if (ic.slotBarSelect < 0) ic.slotBarSelect = 5;

        Debug.Log("Slot Selecionado: " + ic.slotBarSelect);
    }

    void EquipArm()
    {
        if (equipArm != null)
        {
            equipArm.SetActive(false);
            equipArm.transform.SetParent(null);
        }

        if (ic.slots[ic.slotBarSelect] != null)
        {
            equipArm = ic.slots[ic.slotBarSelect];
            Equip(equipArm);
        }
    }

    void Equip(GameObject arma)
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

        VfxDisble(arma);
    }

    void Drop(int slot, GameObject arma)
    {
        arma.SetActive(true);
        arma.transform.SetParent(null);
        arma.transform.position = handPlayer.transform.position + handPlayer.transform.forward * 1f;

        Rigidbody rb = arma.GetComponent<Rigidbody>();
        if (rb != null)
        {
            VfxEnable(arma);
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.AddForce(handPlayer.transform.forward * 2f, ForceMode.Impulse);
        }

        ic.slots[slot] = null;
        Debug.Log("Item dropado do slot: " + slot);
    }

    GameObject VfxDisble(GameObject parent, string tag = "VFX")
    {
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                child.gameObject.SetActive(false);
                return child.gameObject;
            }

            GameObject found = VfxDisble(child.gameObject, tag);
            if (found != null)
                return found;
        }

        return null;
    }
    GameObject VfxEnable(GameObject parent, string tag = "VFX")
    {
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                child.gameObject.SetActive(true);
                return child.gameObject;
            }

            GameObject found = VfxEnable(child.gameObject, tag);
            if (found != null)
                return found;
        }
        return null;
    }
}


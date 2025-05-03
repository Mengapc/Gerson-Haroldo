using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Armas;
using static UnityEditor.Progress;

public class ProceduralItens : MonoBehaviour
{
    public GameObject baseArm;
    public GameObject player;
    private ItemInstance ii;
    private RandomParts rp;
    private GameObject newItem;
    [SerializeField] private List<Sprite> armSprits;
    [Header("Taxa dos itens")]
    public float powerRate;
    public float powerDrop;
    void Awake() // Use Awake para inicializações
    {

        rp = GetComponent<RandomParts>();
        if (rp == null)
        {
            Debug.LogError("O script RandomParts não foi encontrado neste GameObject!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject newItem = GenerateItem(Vector3.zero); // Instancia o item (posição inicial não importa muito aqui)

        }
    }

    public GameObject GenerateItem(Vector3 position)
    {
        string newName = "Item_" + Random.Range(1, 1000);
        Armas.ItemType newType = GenerateType();
        Armas.Rarity newRarity = GenerateRarity();
        int newPower = GeneratePowerLevel(newRarity);
        bool newSpecialStatus = ThisSpecialStatus(newRarity);

        GameObject principalPart = rp.GeneratePrincipalPartArm(newType); // Obtém o prefab da parte principal
        newItem = Instantiate(principalPart, position, Quaternion.identity); // instancia a parte principal

        ii = baseArm.GetComponent<ItemInstance>();
        if (ii == null)
        {
            Debug.LogError("ItemInstance não encontrado no novo item!");
            return null;
        }

        rp.GenerationOutherParts(newType, newItem);
        setItem();
        ii.SetItemData(newName, newType, newRarity, newPower, newSpecialStatus);
        return newItem; // <--- Retorna o item criado
    }


    public Armas.Rarity GenerateRarity() //alterar para a taxa modificada
    {
        int chance = Random.Range(0, 100);
        if (chance < 50) return Armas.Rarity.Common;
        if (chance < 75) return Armas.Rarity.Uncommon;
        if (chance < 90) return Armas.Rarity.Rare;
        if (chance < 98) return Armas.Rarity.Epic;
        return Armas.Rarity.Legendary;
    }
    //private void 

    public bool ThisSpecialStatus(Armas.Rarity rarity)
    {
        return rarity >= Armas.Rarity.Rare;
    }
    public int GeneratePowerLevel(Armas.Rarity rarity)
    {
        return Mathf.RoundToInt(Random.Range(10, 12) * ((int)rarity + 1) * powerRate);
    }

    public Armas.ItemType GenerateType()
    {
        return (Armas.ItemType)Random.Range(0, System.Enum.GetValues(typeof(Armas.ItemType)).Length);
    }
    private void setItem()
    {
        Transform armasTransform = player.transform.Find("Armas");
        GameObject baseArmInstance = Instantiate(baseArm);
        if (armasTransform != null && newItem != null)
        {
            newItem.transform.SetParent(baseArmInstance.transform);
            baseArmInstance.transform.SetParent(armasTransform.transform);
            baseArmInstance.transform.localPosition = Vector3.zero;
            baseArmInstance.transform.localRotation = Quaternion.identity;
        }
    }
}
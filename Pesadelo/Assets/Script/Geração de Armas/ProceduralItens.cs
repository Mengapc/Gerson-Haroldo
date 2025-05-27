using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Armas;

public class ProceduralItens : MonoBehaviour
{
    public GameObject baseArm;
    public GameObject player;
    private ItemInstance ii;
    private RandomParts rp;
    public List<Sprite> armSprits;
    [Header("Taxa dos itens")]
    public float powerRate;
    public float powerDrop;

    void Awake()
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

            GenerateItem(new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z));
        }
    }

    public GameObject GenerateItem(Vector3 position)
    {
        string newName = "Item_" + Random.Range(1, 1000);
        Armas.ItemType newType = GenerateType();
        Debug.Log(newType);
        Armas.Rarity newRarity = GenerateRarity();
        int newPower = GeneratePowerLevel(newRarity);
        bool newSpecialStatus = ThisSpecialStatus(newRarity);
        Sprite newSprite = SetSprite(newType);

        GameObject baseArmInstance = Instantiate(baseArm, position, Quaternion.identity);

        GameObject principalPart = rp.GeneratePrincipalPartArm(newType, baseArmInstance.transform);

        if (principalPart != null)
        {
            principalPart.transform.SetParent(baseArmInstance.transform);
            principalPart.transform.localPosition = Vector3.zero;
            principalPart.transform.localRotation = Quaternion.identity;


            rp.GenerationOutherParts(newType, principalPart, newRarity);
        }
        else
        {
            Debug.LogError("A parte principal não foi gerada corretamente!");
        }

        ii = baseArmInstance.GetComponent<ItemInstance>();
        if (ii == null)
        {
            Debug.LogError("ItemInstance não encontrado na instância de baseArm!");
        }
        else
        {
            ii.SetItemData(newName, newType, newRarity, newPower, newSpecialStatus, newSprite);
        }

        return baseArmInstance;
    }

    public Armas.Rarity GenerateRarity()
    {
        int chance = Random.Range(0, 100);
        if (chance < 50) return Armas.Rarity.Common;
        if (chance < 75) return Armas.Rarity.Uncommon;
        if (chance < 90) return Armas.Rarity.Rare;
        if (chance < 98) return Armas.Rarity.Epic;
        return Armas.Rarity.Legendary;
    }

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
    public Sprite SetSprite(Armas.ItemType type)
    {
        Sprite spriteTemp = null; // Inicializa para garantir que sempre retorne algo
        int requiredSize = 0;

        switch (type)
        {
            case Armas.ItemType.Sword:
                requiredSize = 1;
                if (armSprits != null && armSprits.Count >= requiredSize && armSprits[0] != null)
                    spriteTemp = armSprits[0];
                break;
            case Armas.ItemType.Staff:
                requiredSize = 2;
                if (armSprits != null && armSprits.Count >= requiredSize && armSprits[1] != null)
                    spriteTemp = armSprits[1];
                break;
            case Armas.ItemType.Hammer:
                requiredSize = 3;
                if (armSprits != null && armSprits.Count >= requiredSize && armSprits[2] != null)
                    spriteTemp = armSprits[2];
                break;
            default:
                spriteTemp = null;
                break;
        }
        return spriteTemp;
    }

}

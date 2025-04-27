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
            GenerateItem(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
        }
    }

    public void GenerateItem(Vector3 position)
    {
        string newName = "Item_" + Random.Range(1, 1000);
        Armas.ItemType newType = GenerateType();
        Armas.Rarity newRarity = GenerateRarity();
        int newPower = GeneratePowerLevel(newRarity);
        bool newSpecialStatus = ThisSpecialStatus(newRarity);


        GameObject principalPart = rp.GeneratePrincipalPartArm(newType); // Obtém o prefab da parte principal
        newItem = Instantiate(principalPart, position, Quaternion.identity); // instancia a parte principal
        rp.GenerationOutherParts(newType, newItem);

        newItem = null;
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
}
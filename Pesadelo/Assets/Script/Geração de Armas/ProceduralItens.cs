using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Armas;
using static UnityEditor.Progress;

public class ProceduralItens : MonoBehaviour
{
    private GameObject newItem;
    [Header("Taxa dos itens")]
    public float powerRate;
    public float powerDrop;
    [Header("Partes das armas")]
    [Header("Cajado")]
    public List<GameObject> cajadoGema;
    public List<GameObject> cajadoPontaP;
    public List<GameObject> cajadoCabo;
    [Header("Espada")]
    public List<GameObject> espadaLamina;
    public List<GameObject> espadaGuardaP;
    public List<GameObject> espadaCabo;
    [Header("Ponta")]
    public List<GameObject> marteloGema;
    public List<GameObject> marteloCabeçaP;
    public List<GameObject> marteloCabo;



    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateItem();
        }
    }

    void GenerateItem()
    {
        string newName = "Item_" + Random.Range(1, 1000);
        Armas.ItemType newType = GenerateType();
        Armas.Rarity newRarity = GenerateRarity();
        int newPower = GeneratePowerLevel(newRarity);
        bool newSpecialStatus = ThisSpecialStatus(newRarity);
        GameObject principalPart = GeneratePrincipalPartArm(newType); // Obtém o prefab da parte principal
        newItem = Instantiate(principalPart, Vector3.zero, Quaternion.identity); // instancia a parte principal
        GenerationOutherParts(newType);
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
    GameObject GeneratePrincipalPartArm(Armas.ItemType itemType)
    {
        GameObject item = null;
        switch (itemType)
        {
            case Armas.ItemType.Sword:
                if (espadaGuardaP != null && espadaGuardaP.Count > 0)
                {
                    int randowPart = Random.Range(0, espadaGuardaP.Count);
                    item = espadaGuardaP[randowPart];
                }
                else
                {
                    Debug.LogWarning("A lista espadaGuardaP está vazia ou não foi atribuída!");
                    return null;
                }
                break;
            case Armas.ItemType.Staff:
                if (cajadoPontaP != null && cajadoPontaP.Count > 0)
                {
                    int randowPart = Random.Range(0, cajadoPontaP.Count);
                    item = cajadoPontaP[randowPart];
                }
                else
                {
                    Debug.LogWarning("A lista cajadoPontaP está vazia ou não foi atribuída!");
                    return null;
                }
                break;
            default:
                Debug.LogWarning("Tipo de item não suportado em GeneratePrincipalPartArm: " + itemType);
                return null;
        }
        return item;
    }
    private void GenerationOutherParts(Armas.ItemType itemType)
    {
        int randowPart;
        LockParts infoItem = newItem.GetComponent<LockParts>();
        switch (itemType)
        {
            case Armas.ItemType.Sword:

                randowPart = Random.Range(0, espadaLamina.Count);
                Instantiate(espadaLamina[randowPart], infoItem.armLock1.position, infoItem.armLock1.rotation);

                randowPart = Random.Range(0, espadaCabo.Count);
                Instantiate(espadaCabo[randowPart], infoItem.armLock2.position, infoItem.armLock2.rotation);

                break;
            case Armas.ItemType.Staff:
                randowPart = Random.Range(0, cajadoGema.Count);
                Instantiate(cajadoGema[randowPart], infoItem.armLock1.position, infoItem.armLock1.rotation);

                randowPart = Random.Range(0, cajadoCabo.Count);
                Instantiate(cajadoCabo[randowPart], infoItem.armLock2.position, infoItem.armLock2.rotation);
                break;

        }
    }
}
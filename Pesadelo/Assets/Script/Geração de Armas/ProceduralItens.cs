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
    private GameObject newItem; // Removi a atribuição inicial aqui
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
            // Gerar item a partir da posição do jogador
            GenerateItem(new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z));
        }
    }

    public GameObject GenerateItem(Vector3 position)
    {
        string newName = "Item_" + Random.Range(1, 1000);
        Armas.ItemType newType = GenerateType();
        Armas.Rarity newRarity = GenerateRarity();
        int newPower = GeneratePowerLevel(newRarity);
        bool newSpecialStatus = ThisSpecialStatus(newRarity);

        // Gera a parte principal da arma (apenas o visual da peça)
        GameObject baseArmInstance = Instantiate(baseArm, position, Quaternion.identity); // Agora está na cena

        // Chama a função para gerar a parte principal (pode ser pai do baseArmInstance)
        GameObject principalPart = rp.GeneratePrincipalPartArm(newType, baseArmInstance.transform); // Passando baseArmInstance como pai

        if (principalPart != null)
        {
            // Cria a parte principal como filho da base
            principalPart.transform.SetParent(baseArmInstance.transform); // principalPart agora é filho
            principalPart.transform.localPosition = Vector3.zero; // Ajusta a posição local
            principalPart.transform.localRotation = Quaternion.identity; // Ajusta a rotação local

            // Gera as outras partes (lâmina, cabo, gema, etc.)
            rp.GenerationOutherParts(newType, principalPart, newRarity); // Passando principalPart
        }
        else
        {
            Debug.LogError("A parte principal não foi gerada corretamente!");
        }

        // Obtém o ItemInstance da base instanciada
        ii = baseArmInstance.GetComponent<ItemInstance>();
        if (ii == null)
        {
            Debug.LogError("ItemInstance não encontrado na instância de baseArm!");
        }
        else
        {
            ii.SetItemData(newName, newType, newRarity, newPower, newSpecialStatus);
        }

        return baseArmInstance; // Retorna o item completo com as partes como filhos
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
}
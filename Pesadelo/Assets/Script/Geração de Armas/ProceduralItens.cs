using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Armas;

[DisallowMultipleComponent]

public class ProceduralItens : MonoBehaviour
{
    public GameObject baseArm;
    public GameObject player;
    private ItemInstance ii;
    private InstanceGem Gema;
    public string tagGem = "Gema";
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
        float newPower = GeneratePowerLevel(newRarity);
        bool newSpecialStatus = ThisSpecialStatus(newRarity);
        Sprite newSprite = SetSprite(newType);

        GameObject baseArmInstance = Instantiate(baseArm, position, Quaternion.identity);

        InteractableItem interactable = baseArmInstance.GetComponent<InteractableItem>();
        if (interactable == null)
        { 
            interactable = baseArmInstance.AddComponent<InteractableItem>();
        }
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

        Armas.Element newElemente = GeneretaElement(baseArmInstance);


        ii = baseArmInstance.GetComponent<ItemInstance>();
        if (ii == null)
        {
            Debug.LogError("ItemInstance não encontrado na instância de baseArm!");
        }
        else
        {
            ii.SetItemData(newName, newType, newRarity,newElemente, newPower, newSpecialStatus, newSprite);
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
    public Armas.ItemType GenerateType()
    {
        return (Armas.ItemType)Random.Range(0, System.Enum.GetValues(typeof(Armas.ItemType)).Length);
    }
    public Armas.Element GeneretaElement(GameObject arm)
        {
        GameObject gemaObject = EncontrarFilhoPorTagRecursivo(arm.transform, "Gema");

        if (gemaObject != null)
        {

            InstanceGem gemaInfo = gemaObject.GetComponent<InstanceGem>();

            if (gemaInfo != null)
            {
                Gemas.TypeGem tipoGemaEncontrado = gemaInfo.typeGem;
                Debug.Log(tipoGemaEncontrado);

                return ConverterGemaParaElemento(tipoGemaEncontrado);
            }
            else
            {

                Debug.LogError($"O objeto '{gemaObject.name}' tem a tag 'Gema', mas falta o componente 'GemaInfo'.", gemaObject);
            }
        }
        else
        {
            Debug.LogWarning($"Nenhum filho com a tag 'Gema' foi encontrado em '{arm.name}'. Retornando elemento padrão.");
        }
        return default(Armas.Element);
    }
    public int GeneratePowerLevel(Armas.Rarity rarity)
    {
        return (Mathf.RoundToInt(Random.Range(10, 12) * ((int)rarity + 1) * powerRate))/100;
    }
    public bool ThisSpecialStatus(Armas.Rarity rarity)
    {
        return rarity >= Armas.Rarity.Rare;
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
    public GameObject EncontrarFilhoPorTagRecursivo(Transform pai, string tag)
    {
        // Log para saber em qual objeto a busca está sendo feita atualmente.
        Debug.Log($"Buscando a tag '{tag}' dentro de: {pai.name}");

        // Itera por cada filho direto do objeto 'pai'.
        foreach (Transform filho in pai)
        {
            // Log para mostrar qual filho está sendo inspecionado.
            Debug.Log($"-- Verificando o filho: '{filho.name}', que possui a tag: '{filho.tag}'");

            // 1. Verifica se o filho atual tem a tag procurada.
            if (filho.CompareTag(tag))
            {
                // Log de sucesso, em verde para fácil visualização.
                Debug.Log($"<color=green>SUCESSO!</color> Encontrado o objeto '{filho.name}' com a tag '{tag}'.");
                return filho.gameObject;
            }

            // 2. Se não encontrou, chama a mesma função para o filho atual.
            // Isso faz com que a busca continue nos "netos", "bisnetos", etc.
            GameObject resultado = EncontrarFilhoPorTagRecursivo(filho, tag);

            // 3. Verifica se a chamada recursiva (a busca nos níveis inferiores) encontrou algo.
            if (resultado != null)
            {
                // Se encontrou, retorna o resultado para cima sem precisar procurar mais.
                return resultado;
            }
        }

        // Se o loop terminar e nada for encontrado nesta ramificação, a função retorna null.
        // Nenhum log é necessário aqui, pois a falta de um log de "SUCESSO" já indica a falha neste nível.
        return null;
    }
    private Armas.Element ConverterGemaParaElemento(Gemas.TypeGem tipoGema)
    {
        switch (tipoGema)
        {
            case Gemas.TypeGem.Water:
                return Armas.Element.Water; // Assumindo que Armas.Element.Water existe

            case Gemas.TypeGem.Wind:
                return Armas.Element.Wind; // Assumindo que Armas.Element.Wind existe

            case Gemas.TypeGem.Galaxy:
                return Armas.Element.Galaxy; // Assumindo que Armas.Element.Galaxy existe

            default:
                Debug.LogWarning($"Mapeamento não encontrado para o tipo de gema: {tipoGema}. Retornando valor padrão.");
                return default(Armas.Element);
        }
    }
}

using UnityEngine;
using System.Linq;
using static Armas;

public class SkillsManager : MonoBehaviour
{
    [Header("Configuração da Habilidade")]
    public float cooldownHabilidade = 5f;
    private float proximoUsoDisponivel = 0f;

    [Header("Mapeamento Visual de Habilidades")]
    [SerializeField] private skills[] habilidadesMapeadas;


    private InventBarSelect ib;

    private void Awake()
    {
        ib = FindFirstObjectByType<InventBarSelect>();
        if (ib == null)
        {
            Debug.LogError("Não foi possível encontrar o 'InventBarSelect' na cena!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TentarUsarHabilidade();
        }
    }

    public void TentarUsarHabilidade()
    {
        if (Time.time < proximoUsoDisponivel)
        {
            return;
        }

        if (ib == null || ib.equipArm == null)
        {
            return;
        }

        ItemInstance dadosDaArma = ib.equipArm.GetComponent<ItemInstance>();
        if (dadosDaArma == null) return;

        ExecutarHabilidadeEspecial(dadosDaArma);
    }

    private void ExecutarHabilidadeEspecial(ItemInstance dadosDaArma)
    {
        if (dadosDaArma.rarity < Rarity.Rare)
        {
            Debug.Log("Arma de raridade muito baixa para usar habilidade.");
            return;
        }

        Armas.ItemType tipoArma = dadosDaArma.type;
        Armas.Element elemento = dadosDaArma.element;
        Rarity raridadeArma = dadosDaArma.rarity;

        skills skillData = habilidadesMapeadas.FirstOrDefault(h => h.typeArm == tipoArma && h.element == elemento);

        if (string.IsNullOrEmpty(skillData.skillName) || skillData.preferbSkil == null)
        {
            Debug.LogWarning($"Habilidade não configurada para: {tipoArma} + {elemento}");
            return;
        }

        RarityScaling scalingModifier = skillData.scalingTiers.FirstOrDefault(s => s.rarity == raridadeArma);

        float finalStrength = skillData.baseEffectStrength;
        float finalLifetime = skillData.baseLifetime;
        float finalScale = 1f;

        if (scalingModifier.rarity == raridadeArma)
        {
            finalStrength *= scalingModifier.strengthMultiplier;
            finalLifetime *= scalingModifier.lifetimeMultiplier;
            finalScale = scalingModifier.scaleMultiplier;
        }

        GameObject areaGO = Instantiate(skillData.preferbSkil, transform.position, transform.rotation);

        areaGO.transform.localScale *= finalScale;

        AreaOfEffect aoeScript = areaGO.GetComponent<AreaOfEffect>();
        if (aoeScript != null)
        {
            aoeScript.Initialize(skillData.effectType, finalStrength, finalLifetime, skillData.effectTickRate,
                                 skillData.isMobile, skillData.moveSpeed, transform.forward);

            proximoUsoDisponivel = Time.time + cooldownHabilidade;
            Debug.Log($"Habilidade '{skillData.skillName}' ativada com Força:{finalStrength}, Duração:{finalLifetime}, Escala:{finalScale}x");
        }
        else
        {
            Debug.LogError($"O prefab '{skillData.preferbSkil.name}' não possui o script 'AreaOfEffect'!");
            Destroy(areaGO);
        }
    }
}
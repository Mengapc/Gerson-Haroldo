using UnityEngine;
using System.Linq;
using static Armas;

public class SkillsManager : MonoBehaviour
{
    [Header("Configuração da Habilidade")]
    [Tooltip("Tempo de recarga global em segundos entre o uso de qualquer habilidade.")]
    public float cooldownHabilidade = 5f;
    private float proximoUsoDisponivel = 0f;

    [Header("Mapeamento Visual de Habilidades")]
    [Tooltip("Array principal onde todas as habilidades são configuradas. Cada elemento representa uma combinação única de arma e elemento.")]
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
            return;
        }

        skills skillData = habilidadesMapeadas.FirstOrDefault(h => h.typeArm == dadosDaArma.type && h.element == dadosDaArma.element);

        if (string.IsNullOrEmpty(skillData.skillName) || skillData.preferbSkil == null)
        {
            return;
        }

        RarityScaling scalingModifier = skillData.scalingTiers.FirstOrDefault(s => s.rarity == dadosDaArma.rarity);

        float finalStrength = skillData.baseEffectStrength;
        float finalLifetime = skillData.baseLifetime;
        float finalDuration = skillData.baseEffectDuration;
        float finalScale = 1f;

        if (scalingModifier.rarity == dadosDaArma.rarity)
        {
            finalStrength *= scalingModifier.strengthMultiplier;
            finalLifetime *= scalingModifier.lifetimeMultiplier;
            finalDuration *= scalingModifier.durationMultiplier;
            finalScale = scalingModifier.scaleMultiplier;
        }

        Vector3 spawnPosition = transform.position + transform.forward * 1.5f;
        spawnPosition.y += 0.2f;

        GameObject areaGO = Instantiate(skillData.preferbSkil, spawnPosition, transform.rotation);

        areaGO.transform.localScale *= finalScale;

        AreaOfEffect aoeScript = areaGO.GetComponent<AreaOfEffect>();
        if (aoeScript != null)
        {
            // O Initialize foi ajustado na versão anterior, mas o código que você me passou estava com a versão antiga.
            // Vou corrigir para a versão que aceita todos os parâmetros calculados.
            aoeScript.Initialize(skillData, finalStrength, finalLifetime, finalDuration, transform.forward);

            proximoUsoDisponivel = Time.time + cooldownHabilidade;
        }
        else
        {
            Destroy(areaGO);
        }
    }
}
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


public class SkillsManager : MonoBehaviour
{
    [Header("Configuração da Habilidade")]
    public float cooldownHabilidade = 5f;
    private float proximoUsoDisponivel = 0f;

    [Header("Mapeamento Visual de Habilidades")]
    [SerializeField] private skills[] habilidadesMapeadas;

    private InventBarSelect ib;
    private Dictionary<Armas.ItemType, Dictionary<Armas.Element, Action<skills>>> habilidades;

    private void Awake()
    {
        ib = FindFirstObjectByType<InventBarSelect>();
        if (ib == null)
        {
            Debug.LogError("Não foi possível encontrar o 'InventBarSelect' na cena!");
        }
        InicializarDicionarioHabilidades();
    }

    private void InicializarDicionarioHabilidades()
    {
        habilidades = new Dictionary<Armas.ItemType, Dictionary<Armas.Element, Action<skills>>>();

        var MapeamentoDeFuncoes = new Dictionary<string, Action<skills>>
        {
            { "MarteloDeAgua", EspecialAtackHammerWhater },
            { "MarteloDeVento", EspecialAtackHammerWind },
            { "MarteloDeGalaxia", EspecialAtackHammerGalaxy },
            { "CajadoDeAgua", EspecialAtackStaffWhater },
            { "CajadoDeVento", EspecialAtackStaffWind },
            { "CajadoDeGalaxia", EspecialAtackStaffGalaxy },
            { "EspadaDeAgua", EspecialAtackSwordWhater },
            { "EspadaDeVento", EspecialAtackSwordWind },
            { "EspadaDeGalaxia", EspecialAtackSwordGalaxy }
        };

        foreach (var habilidadeData in habilidadesMapeadas)
        {
            if (!habilidades.ContainsKey(habilidadeData.typeArm))
            {
                habilidades[habilidadeData.typeArm] = new Dictionary<Armas.Element, Action<skills>>();
            }

            if (MapeamentoDeFuncoes.TryGetValue(habilidadeData.skillName, out var funcaoDaHabilidade))
            {
                habilidades[habilidadeData.typeArm][habilidadeData.element] = funcaoDaHabilidade;
            }
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
            float tempoRestante = proximoUsoDisponivel - Time.time;
            Debug.Log($"Habilidade em cooldown! Espere mais {tempoRestante:F1} segundos.");
            return;
        }

        if (ib == null || ib.equipArm == null)
        {
            Debug.LogWarning("Inventário ou arma não encontrados! Não é possível usar habilidade.");
            return;
        }

        ItemInstance dadosDaArma = ib.equipArm.GetComponent<ItemInstance>();
        if (dadosDaArma == null)
        {
            Debug.LogError($"A arma equipada '{ib.equipArm.name}' não possui o script 'ItemInstance'!");
            return;
        }

        ExecutarHabilidadeEspecial(dadosDaArma.type, dadosDaArma.element);
    }

    private void ExecutarHabilidadeEspecial(Armas.ItemType tipoArma, Armas.Element elemento)
    {
        skills skillData = habilidadesMapeadas.FirstOrDefault(h => h.typeArm == tipoArma && h.element == elemento);

        if (string.IsNullOrEmpty(skillData.skillName))
        {
            Debug.LogWarning($"Dados de habilidade não encontrados para -> Arma: {tipoArma}, Elemento: {elemento}");
            return;
        }

        if (habilidades.TryGetValue(tipoArma, out var habilidadesPorElemento))
        {
            if (habilidadesPorElemento.TryGetValue(elemento, out var habilidadeAction))
            {
                habilidadeAction?.Invoke(skillData);
                proximoUsoDisponivel = Time.time + cooldownHabilidade;
            }
        }
    }

    public void EspecialAtackHammerWhater(skills dadosDaHabilidade)
    {
        Debug.Log("Habilidade Martelo de Água Ativada.");
    }

    public void EspecialAtackHammerWind(skills dadosDaHabilidade)
    {
        Debug.Log("Habilidade Martelo de Vento Ativada.");
    }

    public void EspecialAtackHammerGalaxy(skills dadosDaHabilidade)
    {
        Debug.Log("Habilidade Martelo de Galáxia Ativada.");
    }

    public void EspecialAtackStaffWhater(skills dadosDaHabilidade)
    {
        Debug.Log("Habilidade Cajado de Água Ativada.");
    }

    public void EspecialAtackStaffWind(skills dadosDaHabilidade)
    {
        Debug.Log("Habilidade Cajado de Vento Ativada.");
    }

    public void EspecialAtackStaffGalaxy(skills dadosDaHabilidade)
    {
        Debug.Log("Habilidade Cajado de Galáxia Ativada.");
    }

    public void EspecialAtackSwordWhater(skills dadosDaHabilidade)
    {
        Debug.Log("Habilidade Espada de Água Ativada.");
    }

    public void EspecialAtackSwordWind(skills dadosDaHabilidade)
    {
        Debug.Log("Habilidade Espada de Vento Ativada.");
    }

    public void EspecialAtackSwordGalaxy(skills dadosDaHabilidade)
    {
        Debug.Log("Habilidade Espada de Galáxia Ativada.");
    }
}
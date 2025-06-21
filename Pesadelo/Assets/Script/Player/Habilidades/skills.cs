using UnityEngine;
using System;
using System.Collections.Generic;
using static Armas;

public enum EffectType { None, Slow, Push, Pull }

[System.Serializable]
public struct RarityScaling
{
    [Tooltip("A raridade que ativa este modificador de escalonamento.")]
    public Rarity rarity;

    [Tooltip("Multiplicador do tamanho da área (escala). Ex: 1.5 aumenta o tamanho em 50%. 1 = sem mudança.")]
    public float scaleMultiplier;

    [Tooltip("Multiplicador da força do efeito. Ex: 1.2 aumenta a força em 20%. 1 = sem mudança.")]
    public float strengthMultiplier;

    [Tooltip("Multiplicador da duração da área. Ex: 2 dobra a duração. 1 = sem mudança.")]
    public float lifetimeMultiplier;
}

[System.Serializable]
public struct skills
{
    [Header("Identificação")]
    [Tooltip("Nome de identificação da habilidade, usado internamente para depuração e organização.")]
    public string skillName;

    [Tooltip("O tipo de arma (Espada, Cajado, Martelo) necessário para ativar esta habilidade.")]
    public Armas.ItemType typeArm;

    [Tooltip("O elemento da arma necessário para ativar esta habilidade.")]
    public Armas.Element element;

    [Header("Comportamento Base (Raridade Comum/Incomum)")]
    [Tooltip("O Prefab da área de efeito que será instanciado ao usar a habilidade.")]
    public GameObject preferbSkil;

    [Tooltip("O tipo de efeito principal que esta habilidade aplica (Slow, Push, Pull).")]
    public EffectType effectType;

    [Tooltip("A força base do efeito (ex: 0.4 para 40% de slow, ou 15 para força de empurrão).")]
    public float baseEffectStrength;

    [Tooltip("A duração base em segundos que a área de efeito permanece ativa.")]
    public float baseLifetime;

    [Tooltip("O intervalo em segundos entre cada aplicação do efeito (ex: 1 = aplica o efeito a cada segundo).")]
    public float effectTickRate;

    [Header("Movimento (Opcional)")]
    [Tooltip("Marque esta opção se o prefab da habilidade deve se mover após ser criado.")]
    public bool isMobile;

    [Tooltip("A velocidade de movimento do prefab da habilidade, se for móvel.")]
    public float moveSpeed;

    [Header("Tabela de Escalonamento por Raridade")]
    [Tooltip("Lista de modificadores que escalam a habilidade com base na raridade da arma (Raro, Épico, Lendário).")]
    public List<RarityScaling> scalingTiers;
}
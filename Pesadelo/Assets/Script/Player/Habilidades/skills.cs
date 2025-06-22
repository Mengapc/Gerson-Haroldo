using UnityEngine;
using System;
using System.Collections.Generic;
using static Armas;

public enum EffectType { None, Slow, Push, Pull }

[System.Serializable]
public struct RarityScaling
{
    [Tooltip("A raridade da arma que ativa este conjunto de multiplicadores.")]
    public Rarity rarity;

    [Tooltip("Multiplicador do TAMANHO da área de efeito. 1 = sem mudança.")]
    public float scaleMultiplier;

    [Tooltip("Multiplicador da FORÇA do efeito (dano, slow, etc.). 1 = sem mudança.")]
    public float strengthMultiplier;

    [Tooltip("Multiplicador do TEMPO DE VIDA da área de efeito. 1 = sem mudança.")]
    public float lifetimeMultiplier;

    [Tooltip("Multiplicador da DURAÇÃO do debuff no inimigo (ex: tempo de slow). 1 = sem mudança.")]
    public float durationMultiplier;
}

[System.Serializable]
public struct skills
{
    [Header("Identificação")]
    [Tooltip("Nome de identificação da habilidade, usado para organização e depuração.")]
    public string skillName;

    [Tooltip("O tipo de arma (Espada, Cajado, Martelo) necessário para usar esta habilidade.")]
    public Armas.ItemType typeArm;

    [Tooltip("O elemento da arma necessário para usar esta habilidade.")]
    public Armas.Element element;

    [Header("Comportamento Base")]
    [Tooltip("O Prefab da área de efeito que será criado ao usar a habilidade.")]
    public GameObject preferbSkil;

    [Tooltip("O tipo de efeito principal que esta habilidade aplica (Slow, Push, Pull).")]
    public EffectType effectType;

    [Tooltip("A força base do efeito (ex: 0.4 para 40% de slow, ou 15 para força de empurrão).")]
    public float baseEffectStrength;

    [Tooltip("A duração base em segundos que a área de efeito permanece ativa no cenário.")]
    public float baseLifetime;

    [Tooltip("A duração base em segundos do debuff que permanece no inimigo (relevante para Slow).")]
    public float baseEffectDuration;

    [Tooltip("O intervalo em segundos entre cada aplicação do efeito (ex: 0.5 = aplica 2x por segundo).")]
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
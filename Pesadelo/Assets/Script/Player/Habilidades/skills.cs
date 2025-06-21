using UnityEngine;
using System;
using System.Collections.Generic;
using static Armas;

public enum EffectType { None, Slow, Push, Pull }

[System.Serializable]
public struct RarityScaling
{
    public Rarity rarity;
    [Tooltip("Multiplicador do tamanho da área. 1 = sem mudança.")]
    public float scaleMultiplier;
    [Tooltip("Multiplicador da força do efeito. 1 = sem mudança.")]
    public float strengthMultiplier;
    [Tooltip("Multiplicador da duração da área. 1 = sem mudança.")]
    public float lifetimeMultiplier;
}

[System.Serializable]
public struct skills
{
    [Header("Identificação")]
    public string skillName;
    public Armas.ItemType typeArm;
    public Armas.Element element;

    [Header("Comportamento Base (Raridade Comum/Incomum)")]
    public GameObject preferbSkil;
    public EffectType effectType;
    public float baseEffectStrength;
    public float baseLifetime;
    public float effectTickRate;

    [Header("Movimento (Opcional)")]
    public bool isMobile;
    public float moveSpeed;

    [Header("Tabela de Escalonamento por Raridade")]
    public List<RarityScaling> scalingTiers;
}
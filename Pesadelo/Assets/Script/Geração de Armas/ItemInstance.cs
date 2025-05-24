using System;
using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    public Armas arm;
    public string itemName;
    public Armas.ItemType type;
    public Armas.Rarity rarity;
    public int powerLevel;
    public bool specialStatus;
    public Sprite spriteArm;

    public void SetItemData(string name, Armas.ItemType itemType, Armas.Rarity itemRarity, int power, bool specialStatus, Sprite sprite)
    {
        itemName = name;
        type = itemType;
        rarity = itemRarity;
        powerLevel = power;
        this.specialStatus = specialStatus;
        spriteArm = sprite;


        Debug.Log($"Item Criado: {itemName}, Tipo: {type}, Raridade: {rarity}, Poder: {powerLevel}, Poder especial: {specialStatus}");
    }
}
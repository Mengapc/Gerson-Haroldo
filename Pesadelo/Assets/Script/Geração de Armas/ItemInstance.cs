using System;
using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    public Armas arm;
    public string itemName;
    public Armas.ItemType type;
    public Armas.Rarity rarity;
    public Armas.Element element;
    public int powerLevel;
    public bool specialStatus;
    public Sprite spriteArm;

    public void SetItemData(string name, Armas.ItemType itemType, Armas.Rarity itemRarity, Armas.Element element, int power, bool specialStatus, Sprite sprite)
    {
        itemName = name;
        type = itemType;
        rarity = itemRarity;
        powerLevel = power;
        this.specialStatus = specialStatus;
        spriteArm = sprite;
        this.element = element;


        Debug.Log($"Item Criado -> Nome: {itemName}, " +
                  $"Tipo: {type}, " +
                  $"Raridade: {rarity}, " +
                  $"Elemento: {this.element}, " +
                  $"Poder: {powerLevel}, " +
                  $"Status Especial: {specialStatus}, " +
                  $"Sprite: {spriteArm?.name}");
    }
}
using UnityEngine;
[CreateAssetMenu(fileName = "Armas", menuName = "Partes das Armas/Arma")]
public class Armas : ScriptableObject
{
    public enum ItemType { Sword, Staff, Hammer }
    public enum Element { Water, Wind, Galaxy }
    public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }
    public GameObject visialItem;
    public string nameItem;
    public ItemType type;
    public Rarity rarity;
    public float powerLevel;
    public bool specialStatus;
    public Sprite spriteArm;
}

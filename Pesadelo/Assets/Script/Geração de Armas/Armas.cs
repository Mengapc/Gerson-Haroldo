using UnityEngine;
[CreateAssetMenu(fileName = "Armas", menuName = "Arma")]
public class Armas : ScriptableObject
{
    public enum ItemType { Sword, Staff, Hammer}
    public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }
    public GameObject visialItem;
    public string nameItem;
    public ItemType type;
    public Rarity rarity;
    public bool specialStatus;
    public Sprite spriteArm;
}

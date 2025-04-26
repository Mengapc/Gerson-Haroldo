using UnityEngine;
[CreateAssetMenu(fileName = "Armas", menuName = "Arma")]
public class Armas : ScriptableObject
{
    public enum ItemType { Sword, Staff}
    public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }
    public GameObject visialItem;
    public string nameItem;
    public ItemType type;
    public Rarity rarity;
    public int powerLevel;
    public bool specialStatus;
}

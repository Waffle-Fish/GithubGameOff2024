using UnityEngine;
[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string description;
    public InventoryItemRarity rarity;
    public float weight;
    public int value;
    public Sprite itemIcon;
    public ItemType itemType;
    public int defaultQuantity;
}
public enum ItemType
{
    Fish,
    Trinket,
    Tool,
}

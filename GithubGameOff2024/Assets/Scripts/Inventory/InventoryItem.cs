using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string description;
    public InventoryItemRarity rarity;
    public float weight;
    public float value;
    //public Sprite itemIcon;
    //public GameObject model;



}

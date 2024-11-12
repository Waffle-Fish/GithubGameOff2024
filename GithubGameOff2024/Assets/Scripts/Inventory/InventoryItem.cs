using UnityEngine;
using UnityEngine.UIElements;
[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string description;
    public InventoryItemRarity rarity;
    public float weight;
    public float value;
    public Image itemIcon;
    //public GameObject model;



}

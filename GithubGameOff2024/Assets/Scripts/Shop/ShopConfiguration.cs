using UnityEngine;

[CreateAssetMenu(fileName = "ShopConfiguration", menuName = "Shop/Shop Configuration")]
public class ShopConfiguration : ScriptableObject
{
    public string shopName;
    public ShopType shopType;
    public InventoryItem[] availableItems;

    public enum ShopType
    {
        Progression,
        Quantity
    }
}
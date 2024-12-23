using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopConfiguration shopConfig;
    private string _shopName;
    private IShop _currentShop;
    private List<ShopItem> _shopItems = new List<ShopItem>();
    private System.Guid _shopID;

    private void Start()
    {
        InitializeShop();
    }

    private void InitializeShop()
    {
        if (shopConfig == null)
        {
            Debug.LogError("Shop Configuration is missing!");
            return;
        }
        _shopID = System.Guid.NewGuid();
        _shopName = shopConfig.shopName;

        // Convert configuration data to runtime shop items
        _shopItems = shopConfig.availableItems
            .Select(itemData => new ShopItem(itemData))
            .ToList();

        Debug.Log($"Created {_shopItems.Count} shop items from configuration");

        // Initialize appropriate shop type
        switch (shopConfig.shopType)
        {
            case ShopConfiguration.ShopType.Progression:
                InitializeProgressionShop(_shopItems);
                break;
            case ShopConfiguration.ShopType.Quantity:
                var quantities = _shopItems.ToDictionary(
                    item => item,
                    item => item.quantity
                );
                InitializeQuantityShop(_shopItems, quantities);
                break;
            default:
                Debug.LogError($"Unsupported shop type: {shopConfig.shopType}");
                break;
        }
    }
    public string GetShopName()
    {
        return _shopName;
    }

    private void InitializeProgressionShop(List<ShopItem> items)
    {
        var shop = gameObject.AddComponent<ProgressionShop>();
        shop.InitializeItems(items);
        _currentShop = shop;
        Debug.Log($"Initialized ProgressionShop with {items.Count} items");
    }

    private void InitializeQuantityShop(List<ShopItem> items, Dictionary<ShopItem, int> quantities)
    {
        var shop = gameObject.AddComponent<QuantityShop>();
        shop.InitializeItems(items);
        foreach (ShopItem item in items)
        {
            shop.SetItemQuantity(item, item.quantity);
        }
        _currentShop = shop;
        Debug.Log($"Initialized QuantityShop with {items.Count} items");
    }

    public bool PurchaseItem(ShopItem item)
    {
        if (_currentShop == null)
        {
            Debug.LogError("Shop not initialized!");
            return false;
        }
        return _currentShop.PurchaseItem(item);
    }

    public bool SellItem(InventoryItemInstance item)
    {
        if (_currentShop == null)
        {
            Debug.LogError("Shop not initialized!");
            return false;
        }
        return _currentShop.SellItem(item);
    }

    public List<ShopItem> GetAvailableItems()
    {
        if (_currentShop == null)
        {
            Debug.LogError("Shop not initialized!");
            return new List<ShopItem>();
        }
        return _currentShop.GetAvailableItems();
    }
    public int GetItemQuantity(ShopItem item)
    {
        if (_currentShop == null)
        {
            Debug.LogError("Shop not initialized!");
            return 0;
        }
        if (_currentShop is QuantityShop quantityShop)
        {
            return quantityShop.GetItemQuantity(item);
        }
        if (_currentShop is ProgressionShop progressionShop)
        {
            return progressionShop.GetItemQuantity(item);
        }
        return 0;
    }
    public bool CanPurchaseItem(ShopItem item)
    {
        return _currentShop.CanPurchaseItem(item);
    }
    public System.Guid GetShopID()
    {
        return _shopID;
    }
}
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShop : MonoBehaviour, IShop
{
    protected CurrencyManager _currencyManager;
    protected List<ShopItem> _shopItems = new List<ShopItem>();

    protected virtual void Awake()
    {
        _currencyManager = CurrencyManager.Instance;
    }

    public void InitializeItems(List<ShopItem> items)
    {
        _shopItems = new List<ShopItem>(items);
        Debug.Log($"BaseShop initialized with {_shopItems.Count} items");
    }

    public abstract bool CanPurchaseItem(ShopItem item);
    public abstract List<ShopItem> GetAvailableItems();
    protected abstract void OnItemPurchased(ShopItem item);
    public abstract void ResetShop();
    public virtual bool PurchaseItem(ShopItem item)
    {
        if (!CanPurchaseItem(item))
        {
            return false;
        }

        if (_currencyManager.RemoveCurrency(item.value) && InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Tools, item))
        {
            OnItemPurchased(item);
            return true;
        }

        return false;
    }

    // Selling logic is now final and identical for all shops
    public bool SellItem(InventoryItemInstance item)
    {
        if (InventoryManager.Instance.RemoveItem(InventoryManager.InventoryType.Fish, item))
        {
            _currencyManager.AddCurrency(item.value);
            return true;
        }
        return false;
    }
}
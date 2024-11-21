using System.Collections.Generic;
using System.Linq;

public class QuantityShop : BaseShop
{
    private Dictionary<ShopItem, int> _itemQuantities = new Dictionary<ShopItem, int>();

    public void SetItemQuantity(ShopItem item, int quantity)
    {
        _itemQuantities[item] = quantity;
    }
    public int GetItemQuantity(ShopItem item)
    {
        return _itemQuantities.ContainsKey(item) ? _itemQuantities[item] : 0;
    }

    public override bool CanPurchaseItem(ShopItem item)
    {
        return _itemQuantities.ContainsKey(item) && _itemQuantities[item] > 0;
    }

    public override List<ShopItem> GetAvailableItems()
    {
        return _shopItems;
    }

    protected override void OnItemPurchased(ShopItem item)
    {
        _itemQuantities[item]--;
    }

    public override void ResetShop()
    {
        foreach (var item in _shopItems)
        {
            _itemQuantities[item] = 1; // Or whatever default quantity you want
        }
    }
}
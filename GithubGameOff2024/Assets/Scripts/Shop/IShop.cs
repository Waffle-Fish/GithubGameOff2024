using System.Collections.Generic;

public interface IShop
{
    bool CanPurchaseItem(ShopItem item);
    bool PurchaseItem(ShopItem item);
    bool SellItem(InventoryItemInstance item);
    List<ShopItem> GetAvailableItems();
    void ResetShop();
}
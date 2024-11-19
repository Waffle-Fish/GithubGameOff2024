using System.Collections.Generic;

public class ProgressionShop : BaseShop
{
    private int _currentUnlockedIndex = 0;

    public override bool CanPurchaseItem(ShopItem item)
    {
        int itemIndex = _shopItems.IndexOf(item);
        return itemIndex >= 0 && itemIndex == _currentUnlockedIndex;
    }

    public override List<ShopItem> GetAvailableItems()
    {
        return _shopItems;
    }

    protected override void OnItemPurchased(ShopItem item)
    {
        _currentUnlockedIndex++;
    }

    public override void ResetShop()
    {
        _currentUnlockedIndex = 0;
    }
}
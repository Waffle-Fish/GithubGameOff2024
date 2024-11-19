using UnityEngine.UIElements;
using UnityEngine;
public class ShopSlot : VisualElement
{
    #region UXML
    public new class UxmlFactory : UxmlFactory<ShopSlot, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion

    private ShopUIManager _shopUIManager;
    public Image Icon;
    public Label PriceLabel;
    public System.Guid ItemGUID;

    public ShopSlot()
    {
        _shopUIManager = GameObject.FindFirstObjectByType<ShopUIManager>();

        // Create main container
        var container = new VisualElement();
        container.AddToClassList("slot");

        // Create icon
        Icon = new Image();
        Icon.AddToClassList("slot-icon");
        container.Add(Icon);

        // Create price tag
        var priceTag = new VisualElement();
        priceTag.AddToClassList("price-tag");

        var coinIcon = new VisualElement();
        coinIcon.AddToClassList("coin-icon");

        PriceLabel = new Label("0");
        PriceLabel.AddToClassList("price-amount");

        priceTag.Add(coinIcon);
        priceTag.Add(PriceLabel);
        container.Add(priceTag);

        Add(container);

        this.RegisterCallback<ClickEvent>(OnSlotClicked);
    }

    private void OnSlotClicked(ClickEvent evt)
    {
        if (_shopUIManager != null)
        {
            _shopUIManager.OnShopSlotClicked(this);
        }
    }

    public void SetItem(InventoryItemInstance itemInstance, int price)
    {
        Icon.sprite = itemInstance.item.itemIcon;
        Debug.Log("Setting item " + itemInstance.ItemGUID);
        ItemGUID = itemInstance.ItemGUID;
        PriceLabel.text = price.ToString();
    }

    public void ClearItem()
    {
        Icon.sprite = null;
        ItemGUID = System.Guid.Empty;
        PriceLabel.text = "0";
    }
}
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

    public void SetItem(InventoryItemInstance itemInstance)
    {
        Icon.sprite = itemInstance.item.itemIcon;
        Debug.Log("Setting item " + itemInstance.ItemGUID);
        ItemGUID = itemInstance.ItemGUID;
    }

    public void ClearItem()
    {
        Icon.sprite = null;
        ItemGUID = System.Guid.Empty;
    }
}
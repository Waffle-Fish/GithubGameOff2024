using UnityEngine.UIElements;
using UnityEngine.Scripting;
using UnityEngine;

public class ShopDescriptionSlot : VisualElement
{
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<ShopDescriptionSlot, UxmlTraits> { }

    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion

    private VisualElement M_Container;
    private VisualElement M_IconContainer;
    private VisualElement M_Icon;
    private Label M_Name;
    private Label M_Description;
    private VisualElement M_PriceTag;
    private Label M_Value;

    public ShopDescriptionSlot()
    {
        // Create main container
        M_Container = new VisualElement();
        M_Container.AddToClassList("shop-description-slot");

        // Create header with icon and name
        M_IconContainer = new VisualElement();
        M_IconContainer.AddToClassList("shop-description-header");

        M_Icon = new VisualElement();
        M_Icon.AddToClassList("shop-description-icon");

        M_Name = new Label();
        M_Name.AddToClassList("shop-item-name");

        // Create description
        M_Description = new Label();
        M_Description.AddToClassList("shop-item-description");

        // Create price tag
        M_PriceTag = new VisualElement();
        M_PriceTag.AddToClassList("price-tag");

        var coinIcon = new VisualElement();
        coinIcon.AddToClassList("coin-icon");

        M_Value = new Label();
        M_Value.AddToClassList("price-amount");

        // Build the hierarchy
        M_IconContainer.Add(M_Icon);
        M_IconContainer.Add(M_Name);

        M_PriceTag.Add(coinIcon);
        M_PriceTag.Add(M_Value);

        M_Container.Add(M_IconContainer);
        M_Container.Add(M_Description);
        M_Container.Add(M_PriceTag);

        Add(M_Container);

        ClearDetails();
    }

    public void SetItemDetails(InventoryItemInstance itemInstance)
    {
        if (itemInstance == null)
        {
            ClearDetails();
            return;
        }

        M_Icon.style.backgroundImage = new StyleBackground(itemInstance.item.itemIcon);
        M_Name.text = itemInstance.item.name;
        M_Description.text = itemInstance.item.description;
        M_Value.text = $"{itemInstance.value:F0}";

        // Add visual feedback for rarity
        M_Container.RemoveFromClassList("rarity-common");
        M_Container.RemoveFromClassList("rarity-uncommon");
        M_Container.RemoveFromClassList("rarity-rare");
        M_Container.AddToClassList($"rarity-{itemInstance.item.rarity.ToString().ToLower()}");
    }

    public void ClearDetails()
    {
        M_Icon.style.backgroundImage = null;
        M_Name.text = "Select an item";
        M_Description.text = "No item selected";
        M_Value.text = "--";

        M_Container.RemoveFromClassList("rarity-common");
        M_Container.RemoveFromClassList("rarity-uncommon");
        M_Container.RemoveFromClassList("rarity-rare");
    }
}
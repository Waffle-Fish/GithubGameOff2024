using UnityEngine.UIElements;
using UnityEngine.Scripting;
using UnityEngine;
public class DescriptionSlot : VisualElement
{
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<DescriptionSlot, UxmlTraits> { }

    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion

    private VisualElement M_Container;
    private VisualElement M_IconContainer;
    private VisualElement M_Icon;
    private Label M_Name;
    private Label M_Description;
    private Label M_Weight;
    private Label M_Rarity;
    private Label M_Value;

    public DescriptionSlot()
    {
        // Create a container for better layout
        M_Container = new VisualElement();
        M_Container.AddToClassList("description-slot");

        // Create icon container and icon
        M_IconContainer = new VisualElement();
        M_IconContainer.AddToClassList("description-header");

        M_Icon = new VisualElement();
        M_Name = new Label();
        M_Description = new Label();
        M_Weight = new Label();
        M_Rarity = new Label();
        M_Value = new Label();


        // Add icon and name to header container
        M_IconContainer.Add(M_Icon);
        M_IconContainer.Add(M_Name);

        // Update the container hierarchy
        M_Container.Add(M_IconContainer);
        M_Container.Add(M_Description);
        M_Container.Add(M_Weight);
        M_Container.Add(M_Rarity);
        M_Container.Add(M_Value);

        // Add container to description slot
        Add(M_Container);

        // Add classes for styling
        AddToClassList("description-slot");
        M_Name.AddToClassList("item-name");
        M_Description.AddToClassList("item-description");
        M_Weight.AddToClassList("item-weight");
        M_Rarity.AddToClassList("item-rarity");
        M_Value.AddToClassList("item-value");
        M_Icon.AddToClassList("description-icon");

        // Initialize with empty state
        ClearDetails();
    }

    public void SetItemDetails(InventoryItemInstance itemInstance)
    {
        if (itemInstance == null)
        {
            ClearDetails();
            return;
        }
        Debug.Log("item details" + itemInstance.item.name);

        M_Icon.style.backgroundImage = new StyleBackground(itemInstance.item.itemIcon);
        M_Icon.visible = true;
        M_Name.text = itemInstance.item.name;
        M_Description.text = itemInstance.item.description;
        M_Weight.text = $"{itemInstance.weight:F1} kg";
        M_Rarity.text = itemInstance.item.rarity.ToString();
        M_Value.text = $"{itemInstance.value:F0} coins";

        // Add visual feedback for rarity
        M_Container.RemoveFromClassList("rarity-common");
        M_Container.RemoveFromClassList("rarity-uncommon");
        M_Container.RemoveFromClassList("rarity-rare");
        M_Container.AddToClassList($"rarity-{itemInstance.item.rarity.ToString().ToLower()}");
    }
    public void ClearDetails()
    {
        M_Icon.visible = false;
        M_Icon.style.backgroundImage = null;
        M_Name.text = "Select an item";
        M_Description.text = "No item selected";
        M_Weight.text = "--";
        M_Rarity.text = "--";
        M_Value.text = "--";

        M_Container.RemoveFromClassList("rarity-common");
        M_Container.RemoveFromClassList("rarity-uncommon");
        M_Container.RemoveFromClassList("rarity-rare");
    }
}
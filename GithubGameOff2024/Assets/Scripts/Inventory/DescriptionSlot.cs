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
    private Label M_Name;
    private Label M_Description;
    private Label M_Weight;
    private Label M_Rarity;
    private Label M_Value;

    public DescriptionSlot()
    {
        // Create a container for better layout
        M_Container = new VisualElement();
        M_Container.AddToClassList("description-container");

        M_Name = new Label();
        M_Description = new Label();
        M_Weight = new Label();
        M_Rarity = new Label();
        M_Value = new Label();

        // Add elements to container
        M_Container.Add(M_Name);
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

        // Initialize with empty state
        ClearDetails();
    }

    public void SetItemDetails(InventoryItemInstance item)
    {
        if (item == null)
        {
            ClearDetails();
            return;
        }
        Debug.Log("item details" + item.ItemType.name);

        M_Name.text = item.ItemType.name;
        M_Description.text = item.ItemType.description;
        M_Weight.text = $"{item.weight:F1} kg";
        M_Rarity.text = item.rarity.ToString();
        M_Value.text = $"{item.value:F0} coins";

        // Add visual feedback for rarity
        M_Container.RemoveFromClassList("rarity-common");
        M_Container.RemoveFromClassList("rarity-uncommon");
        M_Container.RemoveFromClassList("rarity-rare");
        M_Container.AddToClassList($"rarity-{item.rarity.ToString().ToLower()}");
    }

    /// <summary>
    /// Clears the description slot.
    /// </summary>
    public void ClearDetails()
    {
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
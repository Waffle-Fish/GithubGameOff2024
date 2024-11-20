using UnityEngine.UIElements;

public class DescriptionSlot : VisualElement
{
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<DescriptionSlot, UxmlTraits> { }

    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion

    private Label M_Name;
    private Label M_Description;
    private Label M_Weight;
    private Label M_Rarity;
    private Label M_Value;

    public DescriptionSlot()
    {
        M_Name = new Label("Name");
        M_Description = new Label("Description");
        M_Weight = new Label("Weight");
        M_Rarity = new Label("Rarity");
        M_Value = new Label("Value");

        Add(M_Name);
        Add(M_Description);
        Add(M_Weight);
        Add(M_Rarity);
        Add(M_Value);

        // Add classes for styling
        M_Name.AddToClassList("ItemName");
        M_Description.AddToClassList("ItemDescription");
        M_Weight.AddToClassList("ItemWeight");
        M_Rarity.AddToClassList("ItemRarity");
        M_Value.AddToClassList("ItemValue");
    }

    /// <summary>
    /// Updates the description slot with the details of the selected item.
    /// </summary>
    /// <param name="item">The selected inventory item.</param>
    public void SetItemDetails(InventoryItemInstance item)
    {
        if (item == null)
        {
            ClearDetails();
            return;
        }

        M_Name.text = $"Name: {item.ItemType.name}";
        M_Description.text = $"Description: {item.ItemType.description}";
        M_Weight.text = $"Weight: {item.weight} kg";
        M_Rarity.text = $"Rarity: {item.rarity}";
        M_Value.text = $"Value: {item.value} coins";
    }

    /// <summary>
    /// Clears the description slot.
    /// </summary>
    public void ClearDetails()
    {
        M_Name.text = "Name";
        M_Description.text = "Description";
        M_Weight.text = "Weight";
        M_Rarity.text = "Rarity";
        M_Value.text = "Value";
    }
}
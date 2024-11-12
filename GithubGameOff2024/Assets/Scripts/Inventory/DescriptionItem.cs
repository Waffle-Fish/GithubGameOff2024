using UnityEngine;
using UnityEngine.UIElements;

public class DescriptionItem : VisualElement
{
    private Label M_Name;
    private Label M_Description;
    private Label M_Weight;
    private Label M_Rarity;
    private Label M_Value;

    public DescriptionItem()
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

}

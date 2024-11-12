using UnityEngine;
using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    public Image M_Icon;
    private string M_ItemGUID;

    public InventorySlot()
    {
        M_Icon = new Image();
        Add(M_Icon);

        M_Icon.AddToClassList("SlotIcon");
        AddToClassList("Slot");
    }

}

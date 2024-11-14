using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }

    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
    private InventoryUIManager M_InventoryUIManager;
    public Image M_Icon;
    public string M_ItemGUID;
    public InventorySlot()
    {
        M_InventoryUIManager = GameObject.FindFirstObjectByType<InventoryUIManager>();
        M_ItemGUID = null;
        M_Icon = new Image();
        Add(M_Icon);

        M_Icon.AddToClassList("SlotIcon");
        AddToClassList("Slot");

        // Add click event listener
        this.RegisterCallback<ClickEvent>(evt => OnSlotClicked());
    }

    private void OnSlotClicked()
    {
        // Notify the InventoryUIManager about the click
        M_InventoryUIManager.OnInventorySlotClicked(this);
    }

    public void HoldItem(InventoryItemInstance item)
    {
        M_Icon.sprite = item.ItemType.itemIcon;
        M_ItemGUID = item.ItemType.itemGUID;
    }
    public void ClearItem()
    {
        M_Icon.sprite = null;
        M_ItemGUID = null;
    }

}

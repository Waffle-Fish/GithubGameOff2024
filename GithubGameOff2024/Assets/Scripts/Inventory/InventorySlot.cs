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
    private InventoryUIManager _inventoryUIManager;
    public Image Icon;
    public System.Guid ItemGUID;
    public InventorySlot()
    {
        _inventoryUIManager = GameObject.FindFirstObjectByType<InventoryUIManager>();
        ItemGUID = System.Guid.Empty;
        Icon = new Image();
        Add(Icon);

        Icon.AddToClassList("SlotIcon");
        AddToClassList("Slot");

        this.RegisterCallback<ClickEvent>(OnSlotClicked);
    }

    private void OnSlotClicked(ClickEvent evt)
    {
        Debug.Log($"Slot clicked - GUID: {ItemGUID}, Has Manager: {_inventoryUIManager != null}");
        if (_inventoryUIManager != null)
        {
            _inventoryUIManager.OnInventorySlotClicked(this);
        }
        else
        {
            Debug.LogError("InventoryUIManager reference is null!");
        }
    }

    public void HoldItem(InventoryItemInstance item)
    {
        Icon.sprite = item.ItemType.itemIcon;
        ItemGUID = item.ItemGUID;
    }
    public void ClearItem()
    {
        Icon.sprite = null;
        ItemGUID = System.Guid.Empty;
    }

}

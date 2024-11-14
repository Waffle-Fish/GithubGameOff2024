using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIManager : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement fishInventoryContainer;
    private VisualElement toolsInventoryContainer;
    private VisualElement trinketsInventoryContainer;
    private VisualElement fishDescriptionSlot;
    private VisualElement toolsDescriptionSlot;
    private VisualElement trinketsDescriptionSlot;
    // Start is called before the first frame update
    void Awake()
    {
        InventoryManager.Instance.OnInventoryChanged += OnInventoryChanged;


        uiDocument = gameObject.GetComponent<UIDocument>();
        if (uiDocument != null)
        {
            // Get the root VisualElement
            var root = uiDocument.rootVisualElement;

            // Query the container that holds InventorySlot elements (replace "InventoryContainer" with your actual container name)
            fishInventoryContainer = root.Q<VisualElement>("FishSlotContainer");
            toolsInventoryContainer = root.Q<VisualElement>("ToolSlotContainer");
            trinketsInventoryContainer = root.Q<VisualElement>("TrinketSlotContainer");

            // Add the description slot to the root
            fishDescriptionSlot = root.Q<VisualElement>("FishDescriptionSlot");
            toolsDescriptionSlot = root.Q<VisualElement>("ToolDescriptionSlot");
            trinketsDescriptionSlot = root.Q<VisualElement>("TrinketDescriptionSlot");
        }
        else
        {
            Debug.LogError("UIDocument component not found on the GameObject.");
        }
    }
    void OnDestroy()
    {
        InventoryManager.Instance.OnInventoryChanged -= OnInventoryChanged;
    }


    void OnInventoryChanged(InventoryManager.InventoryType inventoryType, InventoryItemInstance item, bool added)
    {
        if (inventoryType == InventoryManager.InventoryType.Fish)
        {
            if (fishInventoryContainer == null)
            {
                Debug.LogError("Fish inventory container not found.");
                return;
            }
        }
        else if (inventoryType == InventoryManager.InventoryType.Tools)
        {
            if (toolsInventoryContainer == null)
            {
                Debug.LogError("Tools inventory container not found.");
                return;
            }
        }
        else if (inventoryType == InventoryManager.InventoryType.Trinkets)
        {
            if (trinketsInventoryContainer == null)
            {
                Debug.LogError("Trinkets inventory container not found.");
                return;
            }
        }
        // Get the appropriate container based on inventory type
        VisualElement container = null;
        switch (inventoryType)
        {
            case InventoryManager.InventoryType.Fish:
                container = fishInventoryContainer;
                break;
            case InventoryManager.InventoryType.Tools:
                container = toolsInventoryContainer;
                break;
            case InventoryManager.InventoryType.Trinkets:
                container = trinketsInventoryContainer;
                break;
        }

        // Loop through each child in the appropriate inventory container
        foreach (var child in container.Children())
        {
            // Attempt to cast the child to InventorySlot
            if (child is InventorySlot slot)
            {
                if (added)
                {
                    // Only update empty slots with the new item
                    if (string.IsNullOrEmpty(slot.M_ItemGUID))
                    {
                        slot.HoldItem(item);
                        break; // Exit after finding first empty slot
                    }
                }
                else
                {
                    // Clear the slot if it contains the removed item
                    if (slot.M_ItemGUID == item.itemGUID)
                    {
                        slot.ClearItem();
                        break;
                    }
                }
            }
        }
    }

    public void OnInventorySlotClicked(InventorySlot slot)
    {
        // Clear previous selections
        ClearAllSelections();

        // Set new selection
        slot.AddToClassList("selected");

        // Update description
        var item = InventoryManager.Instance.GetItemByGUID(slot.M_ItemGUID);
        if (slot.parent == fishInventoryContainer)
        {
            fishDescriptionSlot.Q<DescriptionSlot>().SetItemDetails(item);
        }
        else if (slot.parent == toolsInventoryContainer)
        {
            toolsDescriptionSlot.Q<DescriptionSlot>().SetItemDetails(item);
        }
        else if (slot.parent == trinketsInventoryContainer)
        {
            trinketsDescriptionSlot.Q<DescriptionSlot>().SetItemDetails(item);
        }
        Debug.Log("item clicked" + slot.M_ItemGUID);
    }

    private void ClearAllSelections()
    {
        ClearContainerSelections(fishInventoryContainer);
        ClearContainerSelections(toolsInventoryContainer);
        ClearContainerSelections(trinketsInventoryContainer);
    }

    private void ClearContainerSelections(VisualElement container)
    {
        if (container != null)
        {
            foreach (var child in container.Children())
            {
                if (child is InventorySlot slot)
                {
                    slot.RemoveFromClassList("selected");
                }
            }
        }
    }
}

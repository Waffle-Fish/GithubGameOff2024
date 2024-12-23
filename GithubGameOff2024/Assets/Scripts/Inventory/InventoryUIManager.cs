
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
public class InventoryUIManager : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement fishInventoryContainer;
    private VisualElement toolsInventoryContainer;
    private VisualElement trinketsInventoryContainer;
    private VisualElement fishDescriptionSlot;
    private VisualElement toolsDescriptionSlot;
    private VisualElement trinketsDescriptionSlot;

    private Label coinLabel;

    private const float ANIMATION_DELAY = 0.5f;

    void Awake()
    {
        InventoryManager.Instance.OnInventoryChanged += OnInventoryChanged;

        uiDocument = gameObject.GetComponent<UIDocument>();
        if (uiDocument != null)
        {
            // Get the root VisualElement
            var root = uiDocument.rootVisualElement;
            root.visible = false;
            // Query the container that holds InventorySlot elements (replace "InventoryContainer" with your actual container name)
            fishInventoryContainer = root.Q<VisualElement>("FishSlotContainer");
            toolsInventoryContainer = root.Q<VisualElement>("ToolSlotContainer");
            trinketsInventoryContainer = root.Q<VisualElement>("TrinketSlotContainer");

            // Add the description slot to the root
            fishDescriptionSlot = root.Q<VisualElement>("FishDescriptionSlot");
            toolsDescriptionSlot = root.Q<VisualElement>("ToolDescriptionSlot");
            trinketsDescriptionSlot = root.Q<VisualElement>("TrinketDescriptionSlot");

            coinLabel = root.Q<Label>("CoinAmount");

            for (int i = 0; i < InventoryManager.Instance.maxFish; i++)
            {
                fishInventoryContainer.Add(new InventorySlot());
            }
            for (int i = 0; i < InventoryManager.Instance.maxTools; i++)
            {
                toolsInventoryContainer.Add(new InventorySlot());
            }
            for (int i = 0; i < InventoryManager.Instance.maxTrinkets; i++)
            {
                trinketsInventoryContainer.Add(new InventorySlot());
            }

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
    public void OpenInventory()
    {
        uiDocument.rootVisualElement.visible = true;
        uiDocument.rootVisualElement.RemoveFromClassList("slide-out");
        uiDocument.rootVisualElement.RemoveFromClassList("slide-out-active");
        uiDocument.rootVisualElement.AddToClassList("slide-in");
        uiDocument.rootVisualElement.AddToClassList("slide-in-active");
    }
    public void CloseInventory()
    {
        uiDocument.rootVisualElement.RemoveFromClassList("slide-in");
        uiDocument.rootVisualElement.RemoveFromClassList("slide-in-active");
        uiDocument.rootVisualElement.AddToClassList("slide-out");
        uiDocument.rootVisualElement.AddToClassList("slide-out-active");

        StartCoroutine(HideAfterAnimation(uiDocument.rootVisualElement, ANIMATION_DELAY));
    }
    public bool IsInventoryOpen()
    {
        return uiDocument.rootVisualElement.visible;
    }
    public void PopulateInventory()
    {
        foreach (InventoryItemInstance item in InventoryManager.Instance.GetInventory(InventoryManager.InventoryType.Fish))
        {
            if (fishInventoryContainer.Q<InventorySlot>().ItemGUID == System.Guid.Empty)
            {
                fishInventoryContainer.Q<InventorySlot>().HoldItem(item);
            }
        }
        foreach (InventoryItemInstance item in InventoryManager.Instance.GetInventory(InventoryManager.InventoryType.Tools))
        {
            if (toolsInventoryContainer.Q<InventorySlot>().ItemGUID == System.Guid.Empty)
            {
                toolsInventoryContainer.Q<InventorySlot>().HoldItem(item);
            }
        }
        foreach (InventoryItemInstance item in InventoryManager.Instance.GetInventory(InventoryManager.InventoryType.Trinkets))
        {
            if (trinketsInventoryContainer.Q<InventorySlot>().ItemGUID == System.Guid.Empty)
            {
                trinketsInventoryContainer.Q<InventorySlot>().HoldItem(item);
            }
        }
        coinLabel.text = CurrencyManager.Instance.GetCurrency().ToString();
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
                    if (slot.ItemGUID == System.Guid.Empty)
                    {
                        slot.HoldItem(item);
                        break; // Exit after finding first empty slot
                    }
                }
                else
                {
                    // Clear the slot if it contains the removed item
                    if (slot.ItemGUID == item.ItemGUID)
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
        var item = InventoryManager.Instance.GetItemByGUID(slot.ItemGUID);
        if (item == null)
        {
            Debug.Log("item is null");
            if (slot.parent == fishInventoryContainer)
            {
                fishDescriptionSlot.Q<DescriptionSlot>().ClearDetails();
            }
            else if (slot.parent == toolsInventoryContainer)
            {
                toolsDescriptionSlot.Q<DescriptionSlot>().ClearDetails();
            }
            else if (slot.parent == trinketsInventoryContainer)
            {
                trinketsDescriptionSlot.Q<DescriptionSlot>().ClearDetails();
            }
            return;
        }
        if (slot.parent == fishInventoryContainer)
        {
            Debug.Log("fish description slot" + fishDescriptionSlot);
            fishDescriptionSlot.Q<DescriptionSlot>().SetItemDetails(item);
        }
        else if (slot.parent == toolsInventoryContainer)
        {
            Debug.Log("tools description slot" + toolsDescriptionSlot);
            toolsDescriptionSlot.Q<DescriptionSlot>().SetItemDetails(item);
        }
        else if (slot.parent == trinketsInventoryContainer)
        {
            Debug.Log("trinkets description slot" + trinketsDescriptionSlot);
            trinketsDescriptionSlot.Q<DescriptionSlot>().SetItemDetails(item);
        }
        Debug.Log("item clicked" + slot.ItemGUID);
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
    private IEnumerator HideAfterAnimation(VisualElement element, float delay)
    {
        yield return new WaitForSeconds(delay);
        // Clean up fade classes
        element.RemoveFromClassList("fade-out");
        element.RemoveFromClassList("fade-out-active");
        element.visible = false;
    }
}

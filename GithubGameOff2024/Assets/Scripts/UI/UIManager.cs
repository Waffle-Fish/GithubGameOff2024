using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIDocument shopUIDocument;
    [SerializeField] private UIDocument inventoryUIDocument;
    [SerializeField] private KeyCode inventoryToggleKey = KeyCode.I;

    private ShopUIManager shopUIManager;
    private InventoryUIManager inventoryUIManager;
    private CurrencyManager currencyManager;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        currencyManager = CurrencyManager.Instance;
        inventoryManager = InventoryManager.Instance;

        InitializeShopUI();
        InitializeInventoryUI();

        // Hide UIs initially
        shopUIDocument.rootVisualElement.visible = false;
        inventoryUIDocument.rootVisualElement.visible = false;
    }

    private void Update()
    {
        // Handle inventory toggle
        if (Input.GetKeyDown(inventoryToggleKey))
        {
            ToggleInventoryUI();
        }
    }

    private void InitializeShopUI()
    {
        if (shopUIDocument != null)
        {
            shopUIManager = GameObject.FindFirstObjectByType<ShopUIManager>();
            if (shopUIManager == null)
            {
                Debug.LogError("ShopUIManager not found in Scene.");
            }
        }
        else
        {
            Debug.LogError("Shop UIDocument is not assigned.");
        }
    }

    private void InitializeInventoryUI()
    {
        if (inventoryUIDocument != null)
        {
            inventoryUIManager = GameObject.FindFirstObjectByType<InventoryUIManager>();
            if (inventoryUIManager == null)
            {
                Debug.LogError("InventoryUIManager not found in Scene.");
            }
        }
        else
        {
            Debug.LogError("Inventory UIDocument is not assigned.");
        }
    }

    public void OpenShop(ShopManager shopManager)
    {
        if (shopUIManager == null) return;

        // Close inventory if it's open
        inventoryUIDocument.rootVisualElement.visible = false;

        // Update shop UI
        shopUIDocument.rootVisualElement.visible = true;
        shopUIManager.OpenShop(shopManager);
    }

    public void CloseShop()
    {
        if (shopUIDocument != null)
        {
            shopUIDocument.rootVisualElement.visible = false;
        }
    }
    private void OpenInventory()
    {
        inventoryUIDocument.rootVisualElement.visible = true;
        inventoryUIManager.PopulateInventory();
    }
    private void CloseInventory()
    {
        inventoryUIDocument.rootVisualElement.visible = false;
    }
    private void ToggleInventoryUI()
    {
        if (inventoryUIDocument.rootVisualElement.visible)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }
}

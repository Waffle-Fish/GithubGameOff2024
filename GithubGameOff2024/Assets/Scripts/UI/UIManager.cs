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

    public void OpenShop(ShopConfiguration shopConfig)
    {
        if (shopUIManager == null) return;

        // Close inventory if it's open
        inventoryUIDocument.rootVisualElement.visible = false;

        // Create and initialize shop manager for this specific shop
        var shopGameObject = new GameObject("ShopManager_" + shopConfig.shopName);
        var shopManager = shopGameObject.AddComponent<ShopManager>();
        shopManager.InitializeWithConfig(shopConfig); // You'll need to add this method to ShopManager

        // Update shop UI
        shopUIDocument.rootVisualElement.visible = true;
        shopUIManager.Initialize();
    }

    public void CloseShop()
    {
        if (shopUIDocument != null)
        {
            shopUIDocument.rootVisualElement.visible = false;

            // Clean up the temporary shop manager
            var shopManager = GameObject.FindFirstObjectByType<ShopManager>();
            if (shopManager != null)
            {
                Destroy(shopManager.gameObject);
            }
        }
    }

    private void ToggleInventoryUI()
    {
        bool isVisible = inventoryUIDocument.rootVisualElement.visible;
        if (isVisible)
        {
            inventoryUIManager.PopulateInventory();
        }
        inventoryUIDocument.rootVisualElement.visible = !isVisible;

        // Close shop if inventory is being opened
        if (!isVisible)
        {
            CloseShop();
        }
    }




}

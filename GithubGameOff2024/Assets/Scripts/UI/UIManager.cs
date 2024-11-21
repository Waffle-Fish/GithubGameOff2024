using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private KeyCode inventoryToggleKey = KeyCode.I;

    private ShopUIManager shopUIManager;
    private InventoryUIManager inventoryUIManager;
    private CurrencyManager currencyManager;
    private InventoryManager inventoryManager;

    private const float ANIMATION_DELAY = 0.55f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currencyManager = CurrencyManager.Instance;
        inventoryManager = InventoryManager.Instance;

        InitializeShopUI();
        InitializeInventoryUI();

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
        shopUIManager = GameObject.FindFirstObjectByType<ShopUIManager>();
        if (shopUIManager == null)
        {
            Debug.LogError("ShopUIManager not found in Scene.");
        }
    }

    private void InitializeInventoryUI()
    {
        inventoryUIManager = GameObject.FindFirstObjectByType<InventoryUIManager>();
        if (inventoryUIManager == null)
        {
            Debug.LogError("InventoryUIManager not found in Scene.");
        }
    }

    public void OpenShop(ShopManager shopManager)
    {
        if (shopUIManager == null) return;

        shopUIManager.OpenShop(shopManager);
    }

    public void CloseShop()
    {
        shopUIManager.CloseShop();
    }

    private void ToggleInventoryUI()
    {
        if (inventoryUIManager.IsInventoryOpen())
        {
            inventoryUIManager.CloseInventory();
        }
        else if (!shopUIManager.IsShopOpen())
        {
            inventoryUIManager.OpenInventory();
        }
    }


}

using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private KeyCode inventoryToggleKey = KeyCode.I;
    [SerializeField] private KeyCode pauseMenuKey = KeyCode.Escape;

    private ShopUIManager shopUIManager;
    private InventoryUIManager inventoryUIManager;
    private PauseMenuManager pauseMenuManager;
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
        InitializePauseMenu();
    }

    private void Update()
    {
        // Handle inventory toggle
        if (InputManager.Instance.WasInventoryButtonPressed() && !pauseMenuManager.IsPauseMenuOpen())
        {
            ToggleInventoryUI();
        }

        // Handle pause menu toggle
        if (InputManager.Instance.WasPauseButtonPressed())
        {
            TogglePauseMenu();
        }
    }

    private void InitializePauseMenu()
    {
        pauseMenuManager = GameObject.FindFirstObjectByType<PauseMenuManager>();
        if (pauseMenuManager == null)
        {
            Debug.LogError("PauseMenuManager not found in Scene.");
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
        else if (!shopUIManager.IsShopOpen() && !pauseMenuManager.IsPauseMenuOpen())
        {
            inventoryUIManager.OpenInventory();
        }
    }

    public void TogglePauseMenu()
    {
        if (pauseMenuManager.IsPauseMenuOpen())
        {
            ClosePauseMenu();
        }
        else if (!shopUIManager.IsShopOpen() && !inventoryUIManager.IsInventoryOpen())
        {
            OpenPauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        pauseMenuManager.OpenPauseMenu();
    }

    public void ClosePauseMenu()
    {
        pauseMenuManager.ClosePauseMenu();
    }

}

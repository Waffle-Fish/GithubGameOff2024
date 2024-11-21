using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIDocument shopUIDocument;
    [SerializeField] private UIDocument inventoryUIDocument;
    [SerializeField] private KeyCode inventoryToggleKey = KeyCode.I;

    private ShopUIManager shopUIManager;
    private InventoryUIManager inventoryUIManager;
    private CurrencyManager currencyManager;
    private InventoryManager inventoryManager;

    private const float ANIMATION_DELAY = 0.55f;

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

        CloseInventory();

        var root = shopUIDocument.rootVisualElement;

        // Setup root fade first
        root.visible = true;
        root.RemoveFromClassList("slide-out");
        root.RemoveFromClassList("slide-out-active");
        root.AddToClassList("slide-in");
        root.AddToClassList("slide-in-active");


        shopUIManager.OpenShop(shopManager);
    }

    public void CloseShop()
    {
        var root = shopUIDocument.rootVisualElement;

        // Start with sliding out the container
        root.RemoveFromClassList("slide-in");
        root.RemoveFromClassList("slide-in-active");
        root.AddToClassList("slide-out");
        root.AddToClassList("slide-out-active");

        StartCoroutine(HideAfterAnimation(root, ANIMATION_DELAY));
    }

    private void OpenInventory()
    {
        var root = inventoryUIDocument.rootVisualElement;

        root.visible = true;
        root.RemoveFromClassList("slide-out");
        root.RemoveFromClassList("slide-out-active");
        root.AddToClassList("slide-in");
        root.AddToClassList("slide-in-active");

        inventoryUIManager.PopulateInventory();

    }

    public void CloseInventory()
    {
        var root = inventoryUIDocument.rootVisualElement;

        // Start with sliding out the container
        root.RemoveFromClassList("slide-in");
        root.RemoveFromClassList("slide-in-active");
        root.AddToClassList("slide-out");
        root.AddToClassList("slide-out-active");

        StartCoroutine(HideAfterAnimation(root, ANIMATION_DELAY));
    }

    private void ToggleInventoryUI()
    {
        if (inventoryUIDocument.rootVisualElement.visible)
        {
            CloseInventory();
        }
        else if (!shopUIDocument.rootVisualElement.visible)
        {
            OpenInventory();
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

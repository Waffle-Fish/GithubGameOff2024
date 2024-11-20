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

    private const float FADE_DELAY = 0.2f;

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
        var shopContainer = root.Q<VisualElement>("Shop");

        // Setup root fade first
        root.visible = true;
        root.AddToClassList("fade-in");
        root.AddToClassList("fade-in-active");

        // Delay the slide animation
        StartCoroutine(DelayedSlideIn(shopContainer));

        shopUIManager.OpenShop(shopManager);
    }

    public void CloseShop()
    {
        var root = shopUIDocument.rootVisualElement;
        var shopContainer = root.Q<VisualElement>("Shop");

        // Start with sliding out the container
        shopContainer.RemoveFromClassList("slide-in");
        shopContainer.RemoveFromClassList("slide-in-active");
        shopContainer.AddToClassList("slide-out");
        shopContainer.AddToClassList("slide-out-active");

        // Delay the fade out
        StartCoroutine(DelayedFadeOut(root));
    }

    private void OpenInventory()
    {
        var root = inventoryUIDocument.rootVisualElement;
        var inventoryContainer = root.Q<VisualElement>("Inventory");

        // Setup root fade first
        root.visible = true;
        root.AddToClassList("fade-in");
        root.AddToClassList("fade-in-active");

        // Delay the slide animation
        StartCoroutine(DelayedSlideIn(inventoryContainer));

        inventoryUIManager.PopulateInventory();
    }

    public void CloseInventory()
    {
        var root = inventoryUIDocument.rootVisualElement;
        var inventoryContainer = root.Q<VisualElement>("Inventory");

        // Start with sliding out the container
        inventoryContainer.RemoveFromClassList("slide-in");
        inventoryContainer.RemoveFromClassList("slide-in-active");
        inventoryContainer.AddToClassList("slide-out");
        inventoryContainer.AddToClassList("slide-out-active");

        // Delay the fade out
        StartCoroutine(DelayedFadeOut(root));
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

    private IEnumerator HideAfterAnimation(VisualElement element, float delay)
    {
        yield return new WaitForSeconds(delay);
        // Clean up fade classes
        element.RemoveFromClassList("fade-out");
        element.RemoveFromClassList("fade-out-active");
        element.visible = false;
    }

    private IEnumerator DelayedSlideIn(VisualElement container)
    {
        yield return new WaitForSeconds(FADE_DELAY);
        container.RemoveFromClassList("slide-out");
        container.RemoveFromClassList("slide-out-active");
        container.AddToClassList("slide-in");
        container.AddToClassList("slide-in-active");
    }

    private IEnumerator DelayedFadeOut(VisualElement root)
    {
        yield return new WaitForSeconds(FADE_DELAY);
        root.RemoveFromClassList("fade-in");
        root.RemoveFromClassList("fade-in-active");
        root.AddToClassList("fade-out");
        root.AddToClassList("fade-out-active");

        StartCoroutine(HideAfterAnimation(root, 0.5f));
    }
}

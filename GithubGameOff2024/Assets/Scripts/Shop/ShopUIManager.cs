using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShopUIManager : MonoBehaviour
{
    private VisualElement root;
    private VisualElement playerInventoryContainer;
    private VisualElement shopInventoryContainer;
    private Label shopNameLabel;
    private Label coinAmountLabel;
    private Button sellButton;
    private Button buyButton;
    private ShopDescriptionSlot itemDescription;
    private Button sellAllButton;
    private Label statusLabel;
    private Button exitButton;

    private ShopSlot selectedSlot;

    private Dictionary<System.Guid, ShopSlot> playerSlots = new Dictionary<System.Guid, ShopSlot>();
    private Dictionary<System.Guid, ShopSlot> shopSlots = new Dictionary<System.Guid, ShopSlot>();

    private InventoryManager _playerInventory;
    private CurrencyManager _currencyManager;
    private ShopManager _shopManager;

    private const float ANIMATION_DELAY = 0.5f;

    private void Start()
    {
        InitializeUI();
        _playerInventory = InventoryManager.Instance;
        if (_playerInventory == null)
        {
            Debug.LogError("InventoryManager instance not found!");
            return;
        }
        _currencyManager = CurrencyManager.Instance;
        if (_currencyManager == null)
        {
            Debug.LogError("CurrencyManager instance not found!");
            return;
        }

    }

    private void InitializeUI()
    {
        Debug.Log("Initializing UI");
        // Get references to UI elements
        root = GetComponent<UIDocument>().rootVisualElement;
        root.visible = false;
        shopNameLabel = root.Q<Label>("ShopName");
        playerInventoryContainer = root.Q<VisualElement>("PlayerInventory");
        shopInventoryContainer = root.Q<VisualElement>("ShopInventory");
        coinAmountLabel = root.Q<Label>("CoinAmount");
        sellButton = root.Q<Button>("SellButton");
        buyButton = root.Q<Button>("BuyButton");
        itemDescription = root.Q<ShopDescriptionSlot>("ItemDescription");
        sellAllButton = root.Q<Button>("SellAllButton");
        statusLabel = root.Q<Label>("StatusLabel");
        statusLabel.style.display = DisplayStyle.None;
        exitButton = root.Q<Button>("ExitButton");
        exitButton.clicked += OnExitButtonClicked;

        // Set up button listeners
        sellButton.clicked += OnSellButtonClicked;
        buyButton.clicked += OnBuyButtonClicked;
        sellAllButton.clicked += OnSellAllButtonClicked;

        // Initialize buttons as hidden
        sellButton.style.display = DisplayStyle.None;
        buyButton.style.display = DisplayStyle.None;


    }
    public void OpenShop(ShopManager shopManager)
    {
        _shopManager = shopManager;
        PopulateShop();
        UpdatePlayerInventory();
        UpdateCoinDisplay();
        UpdatePlayerInventory();

        root.visible = true;
        root.RemoveFromClassList("slide-out");
        root.RemoveFromClassList("slide-out-active");
        root.AddToClassList("slide-in");
        root.AddToClassList("slide-in-active");
    }
    private void PopulateShop()
    {
        Debug.Log("Populating shop");
        shopInventoryContainer.Clear();
        shopSlots.Clear();

        shopNameLabel.text = _shopManager.GetShopName();

        var availableItems = _shopManager.GetAvailableItems();
        Debug.Log($"Available items: {availableItems.Count}");

        foreach (var shopItem in availableItems)
        {
            var slot = new ShopSlot();
            slot.SetItem(shopItem);
            shopInventoryContainer.Add(slot);
            shopSlots[shopItem.ItemGUID] = slot;

            // Add visual feedback for unpurchasable items
            if (!_shopManager.CanPurchaseItem(shopItem) || _currencyManager.GetCurrency() < shopItem.value)
            {
                slot.AddToClassList("disabled");
            }

            Debug.Log($"Added shop item: {shopItem.item.itemName}");
        }
    }

    private void UpdatePlayerInventory()
    {
        Debug.Log("Updating player inventory");
        playerInventoryContainer.Clear();
        playerSlots.Clear();

        // Get sellable items from player inventory
        foreach (var item in _playerInventory.GetInventory(InventoryManager.InventoryType.Fish))
        {
            var slot = new ShopSlot();
            // Show sell value instead of buy value for player items
            slot.SetItem(item);
            playerInventoryContainer.Add(slot);
            playerSlots[item.ItemGUID] = slot;
        }
    }

    public void OnShopSlotClicked(ShopSlot slot)
    {
        if (selectedSlot != null)
            selectedSlot.RemoveFromClassList("selected");

        selectedSlot = slot;
        slot.AddToClassList("selected");

        buyButton.style.display = DisplayStyle.None;
        sellButton.style.display = DisplayStyle.None;
        SetStatusText("");

        if (slot.parent == shopInventoryContainer)
        {
            var item = _shopManager.GetAvailableItems()
                .FirstOrDefault(i => i.ItemGUID == slot.ItemGUID);
            if (item != null)
            {
                itemDescription.SetItemDetails(item);
                itemDescription.SetItemQuantity(_shopManager.GetItemQuantity(item));

                if (!_shopManager.CanPurchaseItem(item))
                {
                    SetStatusText("Item not available!");
                }
                else if (_currencyManager.GetCurrency() < item.value)
                {
                    SetStatusText("Not enough coins!");
                }
                else
                {
                    buyButton.style.display = DisplayStyle.Flex;
                }
            }
        }
        else
        {
            var item = _playerInventory.GetItemByGUID(slot.ItemGUID);
            if (item != null)
            {
                itemDescription.SetItemDetails(item);
                sellButton.style.display = DisplayStyle.Flex;
            }
        }
    }

    private void OnSellButtonClicked()
    {
        if (selectedSlot == null) return;

        var item = _playerInventory.GetItemByGUID(selectedSlot.ItemGUID);
        if (item != null && _shopManager.SellItem(item))
        {
            UpdatePlayerInventory();
            UpdateCoinDisplay();


            SetStatusText($"Sold {item.item.itemName} for {item.value} coins!");
        }
    }

    private void OnBuyButtonClicked()
    {
        if (selectedSlot == null) return;

        var shopItem = _shopManager.GetAvailableItems()
            .FirstOrDefault(i => i.ItemGUID == selectedSlot.ItemGUID);

        if (shopItem != null)
        {
            if (_currencyManager.GetCurrency() < shopItem.value)
            {
                SetStatusText("Not enough coins!");
                return;
            }

            if (!_shopManager.CanPurchaseItem(shopItem))
            {
                SetStatusText("Item not available!");
                return;
            }

            if (_shopManager.PurchaseItem(shopItem))
            {
                UpdatePlayerInventory();
                UpdateCoinDisplay();
                PopulateShop();
                itemDescription.SetItemQuantity(_shopManager.GetItemQuantity(shopItem));


                SetStatusText($"Purchased {shopItem.item.itemName}!");
            }
        }
    }

    private void OnSellAllButtonClicked()
    {
        var fishInventory = _playerInventory.GetInventory(InventoryManager.InventoryType.Fish);
        if (fishInventory.Count == 0)
        {
            SetStatusText("No fish to sell!");
            return;
        }

        int totalSold = 0;
        int totalEarned = 0;

        var itemsToSell = new List<InventoryItemInstance>(fishInventory);

        foreach (var fish in itemsToSell)
        {
            if (_shopManager.SellItem(fish))
            {
                totalSold++;
                totalEarned += fish.value;
            }
        }

        // Update UI
        UpdatePlayerInventory();
        UpdateCoinDisplay();

        // Clear any selected item
        if (selectedSlot != null)
        {
            selectedSlot.RemoveFromClassList("selected");
            selectedSlot = null;
            sellButton.style.display = DisplayStyle.None;
            itemDescription.ClearDetails();
        }

        // Update status label
        if (totalSold > 0)
        {
            SetStatusText($"Sold {totalSold} fish for {totalEarned} coins!");
        }
        else
        {
            SetStatusText("No fish were sold.");
        }
    }

    private void UpdateCoinDisplay()
    {
        coinAmountLabel.text = _currencyManager.GetCurrency().ToString();
    }

    private void SetStatusText(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            statusLabel.style.display = DisplayStyle.None;
        }
        else
        {
            statusLabel.style.display = DisplayStyle.Flex;
            statusLabel.text = message;
        }
    }

    private void OnExitButtonClicked()
    {
        UIManager.Instance.CloseShop();
    }

    public void CloseShop()
    {
        root.RemoveFromClassList("slide-in");
        root.RemoveFromClassList("slide-in-active");
        root.AddToClassList("slide-out");
        root.AddToClassList("slide-out-active");

        StartCoroutine(HideAfterAnimation(root, ANIMATION_DELAY));
    }
    private IEnumerator HideAfterAnimation(VisualElement element, float delay)
    {
        yield return new WaitForSeconds(delay);
        // Clean up fade classes
        element.RemoveFromClassList("fade-out");
        element.RemoveFromClassList("fade-out-active");
        element.visible = false;
    }
    public bool IsShopOpen()
    {
        return root.visible;
    }
}
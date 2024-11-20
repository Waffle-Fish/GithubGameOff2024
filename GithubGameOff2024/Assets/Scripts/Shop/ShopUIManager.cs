using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

public class ShopUIManager : MonoBehaviour
{
    private VisualElement root;
    private VisualElement playerInventoryContainer;
    private VisualElement shopInventoryContainer;
    private Label coinAmountLabel;
    private Button sellButton;
    private Button buyButton;
    private ShopDescriptionSlot itemDescription;

    private ShopSlot selectedSlot;

    private Dictionary<System.Guid, ShopSlot> playerSlots = new Dictionary<System.Guid, ShopSlot>();
    private Dictionary<System.Guid, ShopSlot> shopSlots = new Dictionary<System.Guid, ShopSlot>();

    private InventoryManager _playerInventory;
    private CurrencyManager _currencyManager;
    private ShopManager _shopManager;

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
        playerInventoryContainer = root.Q<VisualElement>("PlayerInventory");
        shopInventoryContainer = root.Q<VisualElement>("ShopInventory");
        coinAmountLabel = root.Q<Label>("CoinAmount");
        sellButton = root.Q<Button>("SellButton");
        buyButton = root.Q<Button>("BuyButton");
        itemDescription = root.Q<ShopDescriptionSlot>("ItemDescription");

        // Set up button listeners
        sellButton.clicked += OnSellButtonClicked;
        buyButton.clicked += OnBuyButtonClicked;

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
    }
    private void PopulateShop()
    {
        Debug.Log("Populating shop");
        shopInventoryContainer.Clear();
        shopSlots.Clear();

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

        if (slot.parent == shopInventoryContainer)
        {
            var item = _shopManager.GetAvailableItems()
                .FirstOrDefault(i => i.ItemGUID == slot.ItemGUID);

            if (item != null)
            {
                itemDescription.SetItemDetails(item);
                buyButton.style.display =
                    _shopManager.CanPurchaseItem(item) && _currencyManager.GetCurrency() >= item.value
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;
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
            // UI will update via currency changed event
            UpdatePlayerInventory();
            UpdateCoinDisplay();

            // Clear selection
            selectedSlot = null;
            sellButton.style.display = DisplayStyle.None;
            itemDescription.ClearDetails();
        }
    }

    private void OnBuyButtonClicked()
    {
        if (selectedSlot == null) return;

        var shopItem = _shopManager.GetAvailableItems()
            .FirstOrDefault(i => i.ItemGUID == selectedSlot.ItemGUID);

        if (shopItem != null && _shopManager.PurchaseItem(shopItem))
        {
            // UI will update via currency changed event
            UpdatePlayerInventory();
            UpdateCoinDisplay();
            PopulateShop(); // Refresh shop in case quantities changed

            // Clear selection
            selectedSlot = null;
            buyButton.style.display = DisplayStyle.None;
            itemDescription.ClearDetails();
        }
        else
        {
            // TODO: Show error message UI
            Debug.Log("Cannot purchase item!");
        }
    }

    private void UpdateCoinDisplay()
    {
        coinAmountLabel.text = _currencyManager.GetCurrency().ToString();
    }


}
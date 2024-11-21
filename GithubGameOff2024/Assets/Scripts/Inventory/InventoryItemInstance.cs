using UnityEngine;

public class InventoryItemInstance
{
    public InventoryItem item { get; protected set; }
    public float weight { get; protected set; }
    public int value { get; protected set; }
    public int quantity { get; set; } = 1;

    public System.Guid ItemGUID { get; private set; }

    protected InventoryItemInstance(InventoryItem item)
    {
        this.item = item;
        this.ItemGUID = System.Guid.NewGuid();
        InitializeInstance();
    }

    protected virtual void InitializeInstance()
    {
        this.weight = item.weight;
        this.value = item.value;
        this.quantity = item.defaultQuantity;

    }
} 

[System.Serializable]
public class FishInstance : InventoryItemInstance
{
    public FishInstance(InventoryItem item) : base(item)
    {
    }

    protected override void InitializeInstance()
    {
        // Calculate randomized weight based on rarity
        weight = item.rarity switch
        {
            InventoryItemRarity.Common => Random.Range(0.5f, 2f),
            InventoryItemRarity.Uncommon => Random.Range(1.5f, 4f),
            InventoryItemRarity.Rare => Random.Range(3f, 7f),
            _ => 1f
        };

        // Calculate value based on weight and rarity multiplier
        float rarityMultiplier = item.rarity switch
        {
            InventoryItemRarity.Common => 2f,
            InventoryItemRarity.Uncommon => 5f,
            InventoryItemRarity.Rare => 10f,
            _ => 1f
        };

        value = (int)(weight * rarityMultiplier);
    }
}

[System.Serializable]
public class ShopItem : InventoryItemInstance
{
    public bool isPurchasable { get; set; } = true;

    public float priceMultiplier { get; set; } = 1f;

    public ShopItem(InventoryItem item, float priceMultiplier = 1f) : base(item)
    {
        this.priceMultiplier = priceMultiplier;
    }

    protected override void InitializeInstance()
    {
        base.InitializeInstance();
        value = (int)(value * priceMultiplier);

    }
}

public enum InventoryItemRarity
{
    Common,
    Uncommon,
    Rare,
}
using UnityEngine;

public class InventoryItemInstance
{
    public InventoryItem ItemType;
    public float weight;
    public float value;

    public InventoryItemRarity rarity;
    public InventoryItemInstance(InventoryItem item)
    {
        this.ItemType = item;
        this.weight = item.weight;
        this.value = item.value;
        this.rarity = item.rarity;
    }
}
public class FishInstance : InventoryItemInstance
{
    public FishInstance(InventoryItem item) : base(item)
    {
        this.ItemType = item;
        // Randomize weight based on rarity
        switch (item.rarity)
        {
            case InventoryItemRarity.Common:
                this.weight = Random.Range(0.5f, 2f);
                break;
            case InventoryItemRarity.Uncommon:
                this.weight = Random.Range(1.5f, 4f);
                break;
            case InventoryItemRarity.Rare:
                this.weight = Random.Range(3f, 7f);
                break;
        }
        // Calculate value based on weight and rarity multiplier
        float rarityMultiplier = 1f;
        switch (item.rarity)
        {
            case InventoryItemRarity.Common:
                rarityMultiplier = 2f;
                break;
            case InventoryItemRarity.Uncommon:
                rarityMultiplier = 5f;
                break;
            case InventoryItemRarity.Rare:
                rarityMultiplier = 10f;
                break;
        }
        // Max possible values:
        // Common: 2kg * 2 = 4 coins
        // Uncommon: 4kg * 5 = 20 coins  
        // Rare: 7kg * 10 = 70 coins
        this.value = this.weight * rarityMultiplier;
    }
}
public enum InventoryItemRarity
{
    Common,
    Uncommon,
    Rare,
}
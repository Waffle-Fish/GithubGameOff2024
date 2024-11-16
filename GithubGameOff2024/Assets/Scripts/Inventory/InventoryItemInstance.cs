using UnityEngine;


public class InventoryItemInstance
{
    public InventoryItem item;
    public float weight;
    public float value;
    public System.Guid ItemGUID;
    //!TODO: Remove rarity since it is already in InventoryItem
    public InventoryItemRarity rarity;
    public InventoryItemInstance(InventoryItem item)
    {
        this.item = item;
        this.weight = item.weight;
        this.value = item.value;
        this.ItemGUID = System.Guid.NewGuid();
    }
}
public class FishInstance : InventoryItemInstance
{
    public FishInstance(InventoryItem item) : base(item)
    {

        this.item = item;
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
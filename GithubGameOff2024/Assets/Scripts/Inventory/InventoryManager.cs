using UnityEngine;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<InventoryManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("InventoryManager");
                    _instance = go.AddComponent<InventoryManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public delegate void InventoryChanged(InventoryType inventoryType, InventoryItemInstance item, bool added);
    public event InventoryChanged OnInventoryChanged;


    public List<InventoryItemInstance> FishInventory = new List<InventoryItemInstance>();
    public int maxFish = 24;
    public List<InventoryItemInstance> ToolsInventory = new List<InventoryItemInstance>();
    public int maxTools = 4;
    public List<InventoryItemInstance> TrinketsInventory = new List<InventoryItemInstance>();
    public int maxTrinkets = 24;

    public enum InventoryType
    {
        Fish,
        Tools,
        Trinkets
    }
    public List<InventoryItemInstance> GetInventory(InventoryType inventoryType)
    {
        switch (inventoryType)
        {
            case InventoryType.Fish:
                return FishInventory;
            case InventoryType.Tools:
                return ToolsInventory;
            case InventoryType.Trinkets:
                return TrinketsInventory;
        }
        return null;
    }
    public InventoryItemInstance GetItemByGUID(System.Guid guid)
    {
        Debug.Log("getting item by guid" + guid);
        return FishInventory.Find(item => item.ItemGUID == guid);
    }
    public bool AddItem(InventoryType inventoryType, InventoryItemInstance item)
    {
        switch (inventoryType)
        {
            case InventoryType.Fish:
                if (FishInventory.Count < maxFish)
                {
                    FishInventory.Add(item);
                    OnInventoryChanged?.Invoke(inventoryType, item, true);
                    return true;
                }
                for (int i = 0; i < FishInventory.Count; i++)
                {
                    if (FishInventory[i] == null)
                    {
                        FishInventory[i] = item;
                        OnInventoryChanged?.Invoke(inventoryType, item, true);
                        return true;
                    }
                }
                break;
            case InventoryType.Tools:
                if (ToolsInventory.Count < maxTools)
                {
                    ToolsInventory.Add(item);
                    OnInventoryChanged?.Invoke(inventoryType, item, true);
                    return true;
                }
                for (int i = 0; i < ToolsInventory.Count; i++)
                {
                    if (ToolsInventory[i] == null)
                    {
                        ToolsInventory[i] = item;
                        OnInventoryChanged?.Invoke(inventoryType, item, true);
                        return true;
                    }
                }
                break;
            case InventoryType.Trinkets:
                if (TrinketsInventory.Count < maxTrinkets)
                {
                    TrinketsInventory.Add(item);
                    OnInventoryChanged?.Invoke(inventoryType, item, true);
                    return true;
                }
                for (int i = 0; i < TrinketsInventory.Count; i++)
                {
                    if (TrinketsInventory[i] == null)
                    {
                        TrinketsInventory[i] = item;
                        OnInventoryChanged?.Invoke(inventoryType, item, true);
                        return true;
                    }
                }
                break;
        }
        Debug.Log("Inventory is full");
        return false;
    }
    public bool RemoveItem(InventoryType inventoryType, InventoryItemInstance item)
    {
        switch (inventoryType)
        {
            case InventoryType.Fish:
                if (FishInventory.Contains(item))
                {
                    FishInventory.Remove(item);
                    OnInventoryChanged?.Invoke(inventoryType, item, false);
                    return true;
                }
                break;
            case InventoryType.Tools:
                if (ToolsInventory.Contains(item))
                {
                    ToolsInventory.Remove(item);
                    OnInventoryChanged?.Invoke(inventoryType, item, false);
                    return true;
                }
                break;
            case InventoryType.Trinkets:
                if (TrinketsInventory.Contains(item))
                {
                    TrinketsInventory.Remove(item);
                    OnInventoryChanged?.Invoke(inventoryType, item, false);
                    return true;
                }
                break;
        }
        return false;
    }

}



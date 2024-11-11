using UnityEngine;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour
{
    public List<InventoryItemInstance> inventory = new List<InventoryItemInstance>();
    public float maxWeight = 100;
    public float currentWeight = 0;
    // Update is called once per frame
    public bool AddItem(InventoryItemInstance item)
    {
        if (currentWeight + item.weight > maxWeight)
        {
            Debug.Log("Inventory is full");
            return false;
        }
        inventory.Add(item);
        currentWeight += item.weight;
        return true;
    }
    public void RemoveItem(InventoryItemInstance item)
    {
        inventory.Remove(item);
        currentWeight -= item.weight;
    }
}



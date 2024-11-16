using UnityEngine;

public class TestInventory : MonoBehaviour
{
    public InventoryItem fish1;
    public InventoryItem fish2;
    public InventoryItem fish3;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // Remove fish when number keys are pressed with left shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var items = InventoryManager.Instance.GetInventory(InventoryManager.InventoryType.Fish);
                foreach (var item in items)
                {
                    if (item.item == fish1)
                    {
                        InventoryManager.Instance.RemoveItem(InventoryManager.InventoryType.Fish, item);
                        break;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                var items = InventoryManager.Instance.GetInventory(InventoryManager.InventoryType.Fish);
                foreach (var item in items)
                {
                    if (item.item == fish2)
                    {
                        InventoryManager.Instance.RemoveItem(InventoryManager.InventoryType.Fish, item);
                        break;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                var items = InventoryManager.Instance.GetInventory(InventoryManager.InventoryType.Fish);
                foreach (var item in items)
                {
                    if (item.item == fish3)
                    {
                        InventoryManager.Instance.RemoveItem(InventoryManager.InventoryType.Fish, item);
                        break;
                    }
                }
            }
        }
        else
        {
            // Add fish when number keys are pressed
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                FishInstance fishInstance = new FishInstance(fish1);
                InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, fishInstance);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                FishInstance fishInstance = new FishInstance(fish2);
                InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, fishInstance);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                FishInstance fishInstance = new FishInstance(fish3);
                InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, fishInstance);
            }
        }
    }
}

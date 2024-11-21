using UnityEngine;

public class TestInventory : MonoBehaviour
{
    public InventoryItem fish1;
    public InventoryItem fish2;
    public InventoryItem fish3;
    public ShopConfiguration shopConfig;
    public UIManager UIManager;
    public ShopManager shopManager;
    public ShopManager shopManager2;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        CurrencyManager.Instance.AddCurrency(100);
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish1));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish2));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish3));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish1));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish2));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish3));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish1));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish2));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish3));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish1));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish2));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish3));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish1));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish2));
        InventoryManager.Instance.AddItem(InventoryManager.InventoryType.Fish, new FishInstance(fish3));
    }

    void Update()
    {

        // Add fish when number keys are pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UIManager.OpenShop(shopManager);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UIManager.CloseShop();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UIManager.OpenShop(shopManager2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UIManager.CloseShop();
        }

    }
}

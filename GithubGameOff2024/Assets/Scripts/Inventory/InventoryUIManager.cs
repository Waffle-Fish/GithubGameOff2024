using UnityEngine;
using UnityEngine.UIElements;


public class InventoryUIManager : MonoBehaviour
{
    private VisualElement M_Root;
    private VisualElement M_Slot;
    private VisualElement M_Description;
    private void Awake()
    {
        M_Root = GetComponent<UIDocument>().rootVisualElement;
        M_Slot = M_Root.Q<VisualElement>("FishSlotContainer");
        M_Description = M_Root.Q<VisualElement>("FishDescriptionSlot");

        for (int i = 0; i < 20; i++)
        {
            InventorySlot slot = new InventorySlot();
            M_Slot.Add(slot);
        }

        DescriptionItem descriptionItem = new DescriptionItem();
        M_Description.Add(descriptionItem);
    }
}

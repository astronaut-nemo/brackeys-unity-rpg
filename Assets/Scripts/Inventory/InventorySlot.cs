using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item; //keeps track of the current item in the slot
    public Image icon; // holds reference to icon for slot
    public Button removeButton; // holds reference to remove buttom for slot

    // Adding an item to the slot
    public void AddItem (Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    // Removing item from slot
    public void ClearSlot ()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    // Using item in slot
    public void UseItem ()
    {
        if (item != null) {
            item.Use(); // use the item
        }
    }

    // Action for Remove Button
    public void OnRemoveButton ()
    {
        Inventory.instance.Remove(item);
    }
}

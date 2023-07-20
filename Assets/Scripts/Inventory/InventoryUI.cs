using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory; // reference to Inventory
    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance; //caching reference
        inventory.onItemChangedCallback += UpdateUI; // Setting it such that UpdateUI() is called any time the inventory is changed i.e. an item is added or removed

        slots = itemsParent.GetComponentsInChildren<InventorySlot>(); // find all the inventory slots
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory")) {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    // Updating the items in our inventory
    void UpdateUI()
    {
        Debug.Log("UPDATING UI");

        for (int i = 0; i < slots.Length; i++) {// loop through all the slots

            if (i < inventory.items.Count){ // if there are more items to add
                slots[i].AddItem(inventory.items[i]); // add the item from the inventory to the slot
            }
            else { // if we don't have any more items to add
                slots[i].ClearSlot(); // clear the slot and sset it back to empty
            }
        }
    }
}

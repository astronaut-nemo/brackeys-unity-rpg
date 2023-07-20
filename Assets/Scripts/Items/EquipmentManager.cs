using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake () 
    {
        instance = this;
    }

    #endregion
    
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem); // delegate event to know when we have equipped or unequipped an item
    public OnEquipmentChanged onEquipmentChangedCallback;

    Equipment[] currentEquipment; // array to hold what is currently equipped
    Inventory inventory;

    void Start ()
    {
        inventory = Inventory.instance;
        
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; // initalise with the same number of slots as the EquipmentSlot enum
        currentEquipment = new Equipment[numberOfSlots];
    }

    // Equip an equipment
    public void Equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot; // get index of slot that the item is supposed to be inserted into i.e. which point on the player; cast it into an integer

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null) {// if there is already an item in the slot
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem); // then, unequip that item that's already in the slot and return it to the inventory
        }

        if (onEquipmentChangedCallback != null ) {
            onEquipmentChangedCallback.Invoke(newItem, oldItem);
        }
        
        currentEquipment[slotIndex] = newItem;
    }

    // Unequip an equipment
    public void Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null) {// only if there is already an item in the slot
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem); // then, unequip that item and return it to the inventory

            currentEquipment[slotIndex] = null;
        }
    }

    // Unequip all the equipment
    public void UnequipAll ()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.U)) {
            UnequipAll();
        }
    }
}

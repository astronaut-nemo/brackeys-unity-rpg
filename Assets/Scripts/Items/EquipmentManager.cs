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

    public SkinnedMeshRenderer targetMesh; // refer to Player mesh

    public Equipment[] defaultItems; // default equipment
    Equipment[] currentEquipment; // array to hold what is currently equipped
    SkinnedMeshRenderer[] currentMeshes; // keep track of meshes spawned in scene
    Inventory inventory;

    void Start ()
    {
        inventory = Inventory.instance;
        
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; // initalise with the same number of slots as the EquipmentSlot enum
        currentEquipment = new Equipment[numberOfSlots];
        currentMeshes = new SkinnedMeshRenderer[numberOfSlots];

        EquipDefaultItems();
    }

    // Equip an equipment
    public void Equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot; // get index of slot that the item is supposed to be inserted into i.e. which point on the player; cast it into an integer
        Equipment oldItem = Unequip(slotIndex); // remove item in previous slot

        if (currentEquipment[slotIndex] != null) {// if there is already an item in the slot
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem); // then, unequip that item that's already in the slot and return it to the inventory
        }

        // An item has been equipped so trigger callback
        if (onEquipmentChangedCallback != null ) {
            onEquipmentChangedCallback.Invoke(newItem, oldItem);
        }
        
        SetEquipmentBlendShapes(newItem, 100f);

        currentEquipment[slotIndex] = newItem; // insert item into the slot
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh); // instantiate a skinned mesh renderer
        newMesh.transform.parent = targetMesh.transform;

        // Deform newMesh based on bones of targetMesh
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        currentMeshes[slotIndex] = newMesh; // insert newMesh into currentMeshes array
    }

    // Unequip an equipment
    public Equipment Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null) {// only if there is already an item in the slot
            if (currentMeshes[slotIndex]) { // and also if the currentMesh at that slotIndex is present
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            
            // Add the item to the inventory
            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0f);
            inventory.Add(oldItem); // then, unequip that item and return it to the inventory

            currentEquipment[slotIndex] = null;

            // Equipment has been removed so trigger the callback
            if (onEquipmentChangedCallback != null ) {
                onEquipmentChangedCallback.Invoke(null, oldItem);
            }
            return oldItem;
        }

        return null;
    }

    // Unequip all the equipment
    public void UnequipAll ()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    // Set blend shapes on Player mesh based on teh equipped items
    void SetEquipmentBlendShapes (Equipment item, float weight) {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions) {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    // Equip default itmes
    void EquipDefaultItems ()
    {
        foreach (Equipment item in defaultItems) {
            Equip(item);
        }
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.U)) {
            UnequipAll();
        }
    }
}

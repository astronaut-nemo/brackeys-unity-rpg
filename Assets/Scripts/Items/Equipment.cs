using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{   
    public EquipmentSlot equipSlot; // Position/Slot to stotre equipment in
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions; // region covered by equipment mesh

    public int armorModifier;
    public int damageModifier;

    // Override of Item.Use()
    public override void Use()
    {
        base.Use();

        EquipmentManager.instance.Equip(this); // equip the item onto player
        RemoveFromInventory(); // remove the item from the inventory
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet}
public enum EquipmentMeshRegion { Legs, Arms, Torso } // corresponds to body blendshapes
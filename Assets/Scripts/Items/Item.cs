using UnityEngine;

// Blueprint Class for all Items
[CreateAssetMenu(fileName = "NewItem", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    // What all items will need
    new public string name = "New Item"; // default item name; override default name variable that is on all GameObjects
    public Sprite icon = null;
    public bool isDefaultItem = false; // determines if the item is a default for the player

    // Use item
    public virtual void Use ()
    {
        Debug.Log("Using " + name);
    }

    // Remove item from inventory
    public void RemoveFromInventory ()
    {
        Inventory.instance.Remove(this);
    }


}

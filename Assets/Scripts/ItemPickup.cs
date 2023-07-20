using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item; // item scriptable object

    // Overidding the Interactable.Interact()
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        
        bool wasPickedUp = Inventory.instance.Add(item); // add to inventory

        if (wasPickedUp) {
            Destroy(gameObject); // remove item from scene
        }
    }
}

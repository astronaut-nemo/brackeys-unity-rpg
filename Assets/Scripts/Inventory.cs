using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance; // Inventory singleton i.e. the only Instance of Inventory shared by all instances of the class

    void Awake()
    {
        if (instance != null){
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this; // access this instance/Singleton from anywhere, through Inventory.instance
    }

    #endregion

    public int space = 20;
    public delegate void OnItemChanged(); // delegate event to know when we have added or removed an item to the inventory
    public OnItemChanged onItemChangedCallback; // implementation of delegate that will be triggered

    public List<Item> items = new List<Item>(); // list to store all the Item objects i.e. the inventory

    public bool Add (Item item)
    {
        if (!item.isDefaultItem) {// only add an Item if it is NOT a default Item
            if (items.Count >= space) {
                Debug.Log("Not enough room in inventory");
                return false;
            }

            items.Add(item); // add item to inventory

            if (onItemChangedCallback != null) { // make sure there is a method to callback
                onItemChangedCallback.Invoke(); // trigger event to change UI when item is added
            }
        }
        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item); // remove item from inventory
        if (onItemChangedCallback != null) { // make sure there is a method to callback
            onItemChangedCallback.Invoke(); // trigger event to change UI when item is added
        }
    }
}

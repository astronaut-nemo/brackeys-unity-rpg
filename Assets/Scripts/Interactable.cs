using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The base class from which all items that the player can interact with will inherit from
public class Interactable : MonoBehaviour
{
    public float radius = 3f; // distance specifying how closer the player needs to be to the item in order to interact with it
    bool isFocus = false; // check if Interactable can be interacted with
    bool hasInteracted = false;

    Transform player; // player position
    public Transform interactionTransform; // point at which the player will interact with the Interactable (it will be different for chests, pickups etc)

    // Define the Interaction method for the Interactables that can be overridden
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
        // This method is meant to be overridden
    }

    // Update is called once per frame
    void Update()
    {
        if (isFocus) { // if the Interactable is currently being focused
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius && !hasInteracted) { // and, if the player is within the radius of interaction and has not yet been interacted with:
                // then interact
                Interact();
                hasInteracted = true;
            }
        }
    }
    // A way to check and set if the player is close enough to interact with the Interactable
    public void onFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void onDefocused ()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    // Visualise radius in the Unity editor
    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null) {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}

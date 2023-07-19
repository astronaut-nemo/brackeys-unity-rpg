using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))] // Making sure Unity will always add a PlayerMotor whenever the PlayerController component is used
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask; // filter through which objects we want to be clickable/interactable
    Camera cam; // store reference to the main camera
    PlayerMotor motor; // store reference to PlayerMotor component

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        // Left-click for movement
        if (Input.GetMouseButtonDown(0)){
            // Casting a ray from a point on the screen ie the mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            // Store info about what we hit in a variable
            RaycastHit hit;

            // Casting the actual ray
            if (Physics.Raycast(ray, out hit, 100, movementMask)){
                // Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                // Move our player to what we hit
                motor.MoveToPoint(hit.point);
                // Stop focusing any objects
            }
        }

        // Right-click for interaction
        if (Input.GetMouseButtonDown(1)){
            // Casting a ray from a point on the screen ie the mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            // Store info about what we hit in a variable
            RaycastHit hit;

            // Casting the actual ray
            if (Physics.Raycast(ray, out hit, 100)){
                // Check if we hit an interactable
                // If we did, set it as our focus
            }
        }
    }
}

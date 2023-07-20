using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))] // Making sure Unity will always add a PlayerMotor whenever the PlayerController component is used
public class PlayerController : MonoBehaviour
{
    public Interactable focus; // store current focus Interactable
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
        if (EventSystem.current.IsPointerOverGameObject()){ // check if we are currently hovering over UI
            return;
        }

        // Left-click for movement
        if (Input.GetMouseButtonDown(0)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // casting a ray from a point on the screen ie the mouse position

            RaycastHit hit; // variable to store info about what we hit with the ray

            // Casting the actual ray
            if (Physics.Raycast(ray, out hit, 100, movementMask)){
                // Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                motor.MoveToPoint(hit.point); // move our player to what we hit
                RemoveFocus(); // stop focusing any objects
            }
        }

        // Right-click for interaction
        if (Input.GetMouseButtonDown(1)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // casting a ray from a point on the screen ie the mouse position
            
            RaycastHit hit; // variable to store info about what we hit with the ray

            // Casting the actual ray
            if (Physics.Raycast(ray, out hit, 100)){
                Interactable interactable = hit.collider.GetComponent<Interactable>(); // check if we hit an object with an interactable component, and store it in an Interactable variable (like an instance of that class that we can now use here to interact with it)

                // If we hit an Interactable, set it as our focus
                if (interactable != null){
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus (Interactable newFocus)
    {
        if (newFocus != focus) { // if the new focus Interactable is different from the currently focused Interactable, then:
            if (focus != null ){// if the previous focus is null
                focus.onDefocused(); // reset the current focus Interactable
            }

            focus = newFocus; // set the current focus to be the newFocus Interactable
            motor.FollowTarget(newFocus); // move player towards currently focused Interactable and keep following it
        }
        
        newFocus.onFocused(transform); // set the current focus' isFocused to true; placed her to make sure the Interactable is notified every time it is clicked on
    }

    void RemoveFocus ()
    {   
        if (focus != null ){// if the previous focus is null
            focus.onDefocused(); // reset the current focus Interactable
        }

        focus = null; // reset the focus
        motor.StopFollowingTarget(); // stop moving the player towards the previously focused Interactable
    }
}

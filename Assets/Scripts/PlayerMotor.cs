using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))] // Making sure Unity will always add a Nav Mesh Agent whenever the PlayerMotor component is used
public class PlayerMotor : MonoBehaviour
{
    Transform target; // target to follow
    NavMeshAgent agent; // reference to the player's Nav Mesh Agent component so we can control it

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(target != null) {
            agent.SetDestination(target.position); // move continuously towards the target/Interactable
            FaceTarget();
        }
    }

    // Defines player movement to a given point
    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point); // Move player to given point
    }

    // Defines player movement to follow a moving Interactable that is the current focus
    public void FollowTarget (Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;

        target = newTarget.interactionTransform; // setting the new target/Interactable to follow
    }

    // Cancels/Stops player movement to follow a moving Interactable that is the current focus
    public void StopFollowingTarget ()
    {
        agent.stoppingDistance = 0f; // resetting stopping distance
        agent.updateRotation = true; // allow rotation within Interactable radius

        target = null; // resetting the target/Interactable to follow
    }

    // Sets player to face the target even when in teh Interactable's radius
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized; // find the direction of Interactable from player
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)); // how to rotate player to face Interactable but prevent rotation in y-direction

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // set player rotation and smooth the rotation
    }
}

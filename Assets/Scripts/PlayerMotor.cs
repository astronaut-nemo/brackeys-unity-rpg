using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))] // Making sure Unity will always add a Nav Mesh Agent whenever the PlayerMotor component is used
public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent; // reference to the player's Nav Mesh Agent component so we can control it

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Defines player movement to a given point
    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point); // Move player to given point
    }
}

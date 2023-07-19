using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // References
    public Transform target;

    // Variables
    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float pitch = 2f; // offset from the center of the player, which happens to be on the ground
    public float yawSpeed = 100f; // speed of frotation of the camera

    private float currentZoom = 10f;
    private float currentYaw = 0f; // rotation value of camera
    
    // Update is called once per frame
    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Rotate camera using horizontal input
        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    // LateUpdate is called once per frame after Update is called
    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom; // set camera position
        transform.LookAt(target.position + Vector3.up * pitch); // make camera look at the player and be higher by the height of the player object

        transform.RotateAround(target.position, Vector3.up, currentYaw); // make camera rotate around the player's position, around the y-axis and for the angle created by the player input
    }
}

/*
 * This script descibes the behaviour of the thrid person camera. 
 * The Camera is justified to the player and can be moved around the 
 * player by moving the mouse. It is also possible to zoom in and out 
 * by scrolling the mouse wheel.
 */

using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // reference to the target player
    public GameObject target;

    // speed of rotating camera can be justified
    public float speed = 2f;
    // distance to the player can be justified
    public float distance = 70f;
    // angle of camera view can be justified
    public float offsetY = 20f;

    // variables for getting mouse inputs
    private float mouseRotation = 0f;
    private float zoomY;

    void LateUpdate()
    {
        // get the rotation input of the mouse 
        mouseRotation += Input.GetAxisRaw("Mouse X") * speed * Time.deltaTime;

        // adjust camera position to the mouse input, player position and the given values for distance to the player
        Vector3 cameraPosition = new Vector3(
           target.transform.position.x + Mathf.Sin(mouseRotation * 2 * Mathf.PI) * distance,
           target.transform.position.y + offsetY,
           target.transform.position.z + Mathf.Cos(mouseRotation * 2 * Mathf.PI) * distance
            );

        // set the position and rotate the camera to the player 
        transform.position = cameraPosition;
        transform.LookAt(target.transform);
    }
}

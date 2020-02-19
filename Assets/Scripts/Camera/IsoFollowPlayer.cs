/*
 * This script descripts the behaviour of the isometric camera.
 * The new position justified to the player.
 */

using UnityEngine;

public class IsoFollowPlayer : MonoBehaviour
{
    private Transform player;
    private float distance = 40f;
    private float oneOverSqrt = 1 / Mathf.Sqrt(2);
    private Vector3 newPosition = new Vector3();


    //get the transform component of the player for getting its position
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //calculate position of the camera to the player
        newPosition.x = player.position.x - distance * oneOverSqrt;
        newPosition.y = distance * oneOverSqrt + player.position.y;
        newPosition.z = player.position.z - distance * oneOverSqrt;
        transform.position = newPosition;

        //zoom in and out by scrolling the mousewheel and justify the orthographic size
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && Camera.main.orthographicSize > 6) // forward
        {
            Camera.main.orthographicSize--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && Camera.main.orthographicSize < 18) // backwards
        {
            Camera.main.orthographicSize++;
        }
    }
}

/*
 * This script is for switching between two referenced cameras.
 * It is possible to switch between the isometric and the third person camera
 * by pressing "c".
 */
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    // referenced cameras
    public Camera iso;
    public Camera thirdPerson;

    // set iso to active camera by default
    void Start()
    {
        iso.enabled = true;
        thirdPerson.enabled = false;
    }

    /* check if "C"- key is pressed, then disable the current camera
     * and enable the other camera.
     */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            iso.enabled = !iso.enabled;
            thirdPerson.enabled = !thirdPerson.enabled;
        }
    }
}

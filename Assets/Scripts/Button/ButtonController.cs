/*
 * This script describes the behaviour of the button, after the
 * player collides with it.
 */

using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //set reference to the material of the button to change color
    public Material green, red;

    private GameObject gate;
    private new Renderer renderer;
    private bool isOpen;

    //get the renderer and the gate object of the scene
    void Start()
    {
        renderer = GetComponent<Renderer>();
        gate = GameObject.FindGameObjectWithTag("Gate");
    }

    /* If the player collides with the button, the button changes color and 
     * the boolean of the animator for opening the gate is changed.
     */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (isOpen)
            {
                renderer.sharedMaterial = red;
                gate.GetComponent<Animator>().SetBool("Open", false);
                isOpen = false;
            }
            else
            {
                renderer.sharedMaterial = green;
                gate.GetComponent<Animator>().SetBool("Open", true);
                isOpen = true;
            }

        }
    }
}

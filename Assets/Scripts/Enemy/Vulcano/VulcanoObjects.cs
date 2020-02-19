/*
 * This script describes the behaviour of the objects, which are spawned 
 * by the vulcano. A random force is added to each object, to destribute them
 * randomly in the given radius of force.
 */
using UnityEngine;

public class VulcanoObjects : MonoBehaviour
{
    //setting max force for object
    public float upForce = 10f;
    public float sideForce = 2f;


    void Start()
    {
        //getting random force for gameobject and add it by creation
        float xForce = Random.Range(-sideForce, sideForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, upForce, zForce);
        GetComponent<Rigidbody>().velocity = force;
    }

}

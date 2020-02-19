/*
 * This script describes the behaviour of the vulcano. 
 * If the player is in the trigger radius, the vulcano starts
 * spawning new vulcano objects in the given time intervall. If
 * the player leaves the radius, the objects will be destroyed in 
 * the same time intervall.
 */
using System.Collections.Generic;
using UnityEngine;

public class DeployObjects : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> respawns;
    public new Rigidbody rigidbody;
    //intervall in which new objects spawn can be justified
    public float respawnTime = 1f;

    //variables for checking, if objects can be spawned
    private bool isStarted;
    private float spawnTimer;

    private void Start()
    {
        spawnTimer = respawnTime;
    }

    //spawn objects of the referenced prefab
    private void SpawnObject()
    {
        GameObject gameObject = Instantiate(prefab) as GameObject;
        gameObject.transform.position = rigidbody.transform.position;
        respawns.Add(gameObject);
    }

    /*
     * Check if player triggers with the trigger radius of the vulcano and spawn 
     * objects in the given intervall. If the player is not in the radius, the 
     * previous spawned objects will be destroyed by the same intervall.
     */
    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            //spwan if player is in range
            if (isStarted)
            {
                SpawnObject();
                spawnTimer = respawnTime;
            }
            //destroy objects if player is out of range
            else if (!isStarted && respawns.Count != 0)
            {
                Destroy(respawns[respawns.Count - 1]);
                respawns.RemoveAt(respawns.Count - 1);
                spawnTimer = respawnTime;
            }
        }
    }

    //check if valucano triggers with player and start ejecting objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isStarted = true;
        }

    }

    //check if player exits the trigger and stop spwaning objects
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isStarted = false;

        }
    }
}
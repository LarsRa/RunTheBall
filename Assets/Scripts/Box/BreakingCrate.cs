/*
 * Script for destroying crate and spawn coins.
 */

using UnityEngine;

public class BreakingCrate : MonoBehaviour
{
    //referenced prefabs of cracked crate and coin
    public GameObject crackedCrate;
    public GameObject coinPrefab;

    //variable for spawning coins after cracking the crate
    private int nrOfCoins = 10;
    private float spawnRange = 1f;
    public float upForce = 1.5f;

    //checking if the player triggers the top of the box
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Spawn a cracked crate at the same position
            Instantiate(crackedCrate, transform.position, transform.rotation);

            //Force variables for the spawned coins
            //Force variables for the spawned coins
            float xForce, zForce;

            //spawn coins and add random force in the given range to distribute them
            for (int i = 0; i < nrOfCoins; i++)
            {
                xForce = Random.Range(-spawnRange, spawnRange);
                zForce = Random.Range(-spawnRange, spawnRange);
                Vector3 force = new Vector3(xForce, upForce, zForce);

                GameObject coin = Instantiate(coinPrefab, transform.position, transform.rotation) as GameObject;
                coin.GetComponent<Rigidbody>().velocity = force;
            }
        }

        //Remove the not cracked crate
        Destroy(gameObject);
    }
}

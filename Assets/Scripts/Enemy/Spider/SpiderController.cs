/*
 * This scrpit describes the behaviour of the spider. 
 * If the player is in the trigger radius of the spider, the spider starts
 * running towards the player by using a static mesh of the NavMeshAgent.
 */
using UnityEngine;
using UnityEngine.AI;

public class SpiderController : MonoBehaviour
{
    // movement speed of the spider can be justified
    public float speed = 2f;

    //is set if the player is in trigger radius
    private bool playerIsNear;

    private CharacterController controller;
    private Animator animator;
    private GameObject player;
    private NavMeshAgent agent;

    //get components of the spider and the player object
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /* 
     * If the player is in the trigger radius, the destination of
     * the NavMeshAgent is set to the players position and updated by every frame.
     * The spider starts running to the players direction with the given speed.
     */
    private void Update()
    {
        if (playerIsNear)
        {
            agent.destination = player.transform.position;
            agent.speed = speed;
        }
    }

    /*
     * Check if the player is in the trigger radius of the spider. 
     * Set boolean for the animator and NavMeshAgent to start running.
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsNear = true;
            animator.SetBool("Run", true);
        }
    }

    /*
    * Check if the player exits the trigger radius of the spider. 
    * Set boolean for the animator and NavMeshAgent to stop running.
    */
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsNear = false;
            animator.SetBool("Run", false);
            agent.speed = 0;
        }
    }

    /*
    * Check if the spider collides with the player. 
    * Set boolean for the animator to attack the player and apply damage to the player.
    */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            animator.SetBool("Attack", true);
            player.GetComponent<Health>().applyDamage(25);
        }
    }

    /*
    * Check if the player exits the collision and stop the attack animation.
    */
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            animator.SetBool("Attack", false);
        }
    }
}

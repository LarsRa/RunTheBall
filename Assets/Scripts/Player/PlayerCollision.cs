/*
 * This script manages all collisions between the player and game objects.
 * If the player collides with ground, variables in the movement script will be justified.
 * If the player collides with enemys, functions in the health script of the player are 
 * called. 
 * If the player collides with the game over ground or the finish, the game manager is called.
 */

using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerMovement playerMovement;
    private Health playerHealth;

    //getting references to the effected scripts
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerHealth = gameObject.GetComponent<Health>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    //check for collison--------------------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        //check if player is on movement ground
        if ((collision.collider.tag == "moveGround" || collision.collider.tag == "Sand") && !playerMovement.IsGrounded)
        {
            playerMovement.IsGrounded = true;
            playerMovement.JumpCounter = 1;
        }

        // check if player colides with sand ground and call movement script
        if (collision.collider.tag == "Sand")
        {
            playerMovement.IsSand = true;
        }

        // check if player colides with jump ground and call movement script
        if (collision.collider.tag == "Jump")
        {
            playerMovement.IsGrounded = true;
            playerMovement.JumpCounter = 2;
        }

        /* 
         * Check if player colides with game over ground.
         * Call game manager and set player back to start.
         */
        if (collision.collider.tag == "GameOverGround")
        {
            gameManager.LostRound();
            if (!gameManager.GameEnded)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.transform.position = new Vector3(0f, 0.5f, 0f);               
                playerHealth.SetFullLife();
            }
        }

        // check if player colides with vucano object and call health script
        if (collision.collider.tag == "VulcanoObject")
        {
            playerHealth.applyDamage(15);
        }

        // check if player colides with bomb and call health script
        if (collision.collider.tag == "Bomb")
        {
            playerHealth.applyDamage(25);
            Destroy(collision.collider.gameObject);
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        // check if player exits sand ground and adjust movement variables
        if (collision.collider.tag == "Sand")
        {
            playerMovement.IsSand = false;
        }
    }


    //check for trigger--------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        //check if player triggers with coin and call game manager
        if (other.tag == "coin")
        {
            gameManager.Score = 10;
            Destroy(other.gameObject);
        }

        //check if player triggers with finish and call game manager
        if (other.tag == "Finish")
        {
            gameManager.CompleteLevel();
        }

        //check if player triggers with enemy(spider) and call helth script
        if (other.tag == "Enemy")
        {
            playerHealth.applyDamage(25);
        }
    }
}

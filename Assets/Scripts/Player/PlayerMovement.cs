/*
 * This script describes the movement of the player. 
 * It checks if a movement key is pressed and adds forces relating
 * to the ground the player is moving on.
 */

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*----------------------------------------------------------------------------
     *       Variables for adjusting movement to the ground and key input
     * ----------------------------------------------------------------------------
     */
    public new Rigidbody rigidbody;

    //movement speed can be justified
    public float moveForce = 10f;
    public float jumpForce = 200f;
    public float maxSpeed = 20f;
    public float fallDownMultiplier = 2f;

    //variable for checking, if player is grounded
    private bool isGrounded = false;

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        set
        {
            isGrounded = value;
        }
    }

    /*
     * Variable for slowing down to adjust the movement speed to the ground 
     * after collision with sand
     */
    private bool isSand;
    public bool IsSand
    {
        set
        {
            isSand = value;
        }
    }

    private float actualMoveForce;
    private float actualJumpForce;
    private float actualMaxSpeed;
    private float maxSandSpeed;
    private float brakeFactor = 2f;

    //variable to increase the jumping force with jumppad after collision
    private int jumpCounter = 1;
    public int JumpCounter
    {
        set
        {
            if (value == 1)
            {
                jumpCounter = value;
            }
            else if (jumpCounter < 3)
            {
                jumpCounter++;
            }
        }
    }

    //boolean for checking if key in movement direction is pressed
    private bool forward, back, left, right, jump;

    private GameManager gameManager;

    //direction vector for movement in camera direction
    private Vector3 forwardVec, sideVec;

    /*-----------------------------------------------------------------------------
     *              Funktions for checking and updating movement
     * ----------------------------------------------------------------------------
     */
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        maxSandSpeed = maxSpeed * 0.5f;

        //calculating direction for movement in iso camera direction
        forwardVec = Camera.main.transform.forward;
        forwardVec.y = 0;
        forwardVec = Vector3.Normalize(forwardVec);
        sideVec = Quaternion.Euler(new Vector3(0, 90, 0)) * forwardVec;
    }

    //check for ground and adjust speed. also check if keys for movement are pressed
    private void Update()
    {
        //updating direction for movement in camera direction
        forwardVec = Camera.main.transform.forward;
        forwardVec.y = 0;
        forwardVec = Vector3.Normalize(forwardVec);
        sideVec = Quaternion.Euler(new Vector3(0, 90, 0)) * forwardVec;

        //adjust speed to the ground. Slow down, if the ground is sand
        if (isSand)
        {
            actualMoveForce = moveForce * 0.5f;
            actualJumpForce = jumpForce * 0.5f;
            actualMaxSpeed = maxSpeed * 0.5f;
            if (rigidbody.velocity.magnitude > maxSandSpeed)
            {
                rigidbody.AddForce(-brakeFactor * rigidbody.velocity);
            }
        }
        else
        {
            actualMoveForce = moveForce;
            actualJumpForce = jumpForce;
            actualMaxSpeed = maxSpeed;
        }

        //check for pressed keys if the game is started
        if (gameManager.GameStarted)
        {
            CheckForPressedKey();
        }
    }

    //update the movement for the pressed keys
    void FixedUpdate()
    {
        UpdateMovement();
    }

    //check if keys for movement are pressed and set booleans for movement
    void CheckForPressedKey()
    {
        //forward button pressed - w
        if (Input.GetKeyDown("w"))
        {
            forward = true;
        }
        if (Input.GetKeyUp("w"))
        {
            forward = false;
        }

        //forward button pressed - s
        if (Input.GetKeyDown("s"))
        {
            back = true;
        }
        if (Input.GetKeyUp("s"))
        {
            back = false;
        }

        //right button pressed - d
        if (Input.GetKeyDown("d"))
        {
            right = true;
        }
        if (Input.GetKeyUp("d"))
        {
            right = false;
        }

        //left button pressed - a
        if (Input.GetKeyDown("a"))
        {
            left = true;
        }
        if (Input.GetKeyUp("a"))
        {
            left = false;
        }

        //space pressed - jumping
        if (Input.GetKeyDown("space") && isGrounded)
        {
            jump = true;
        }
    }

    //adding force in pressed direction
    void UpdateMovement()
    {
        //only update if max speed is not reached and the player is grounded
        if (rigidbody.velocity.magnitude < actualMaxSpeed && isGrounded)
        {
            if (forward)
            {
                rigidbody.AddForce(forwardVec * actualMoveForce);
            }

            if (back)
            {
                rigidbody.AddForce(-forwardVec * actualMoveForce);
            }

            if (right)
            {
                rigidbody.AddForce(sideVec * actualMoveForce);
            }

            if (left)
            {
                rigidbody.AddForce(-sideVec * actualMoveForce);
            }
            if (jump)
            {
                rigidbody.AddForce(0, jumpCounter * actualJumpForce, 0);
                isGrounded = false;
                jump = false;
            }
        }
    }
}

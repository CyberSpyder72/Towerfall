using Unity.Mathematics;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //public GameObject groundGrass;
    //private Rigidbody groundRb;
    //public float groundVelocity;
    //public bool isFalling = false;
    //public float fallSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject gameController; //Reference to the game controller

    private Rigidbody playerRb;
    public Animator animator;


    public InputAction moveAction;
    private Vector2 moveInput;
    public int speed = 5;
    


    public InputAction jumpAction;
    private float jumpForce = 10;
    public bool IsOnGround = true;
    public bool isOnLadder = false;

    private float zCenter = -2; //To make sure they don't become misaligned
    private Quaternion lastRotation;

    private float playerBumpForce = 2.5f; //How much players push eachother


    //Attacking
    public InputAction attackAction;
    private float attackOffset;
    private float attackOffsetValue = 2;
    private float attackForce = 10;
    private float attackCooldown;
    private float attackCooldownSet = 2;
    public GameObject fireball;
    public GameObject bomb;
    public GameObject bounceBall;

    //Game Overs
    private float killFloor = -20;
    public bool isDead = false;
    
    

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        //groundRb = groundGrass.GetComponent<Rigidbody>();
        //GetGrassVelocity();

        moveAction.Enable();
        jumpAction.Enable();
        attackAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //fallSpeed = playerRb.linearVelocity.y;

        //transform.rotation = startRotation;
        if (transform.position.z != zCenter)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zCenter);
        }

        transform.rotation = lastRotation;

        //Movement
        moveInput = moveAction.ReadValue<Vector2>();
        transform.Translate(Vector3.right * Mathf.Abs(moveInput.x) * Time.deltaTime * speed);


        if (isOnLadder == true)
        {

            transform.Translate(Vector3.down * Time.deltaTime);
            transform.Translate(Vector3.up * moveInput.y * Time.deltaTime * speed);

        }


        if (moveInput.x == -1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);   //these two "if" statements change the direction the player is facing in game. this is good for animations.
            lastRotation = Quaternion.Euler(0, 180, 0);
            attackOffset = -attackOffsetValue;
        }
        else if (moveInput.x == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            lastRotation = Quaternion.Euler(0, 0, 0);
            attackOffset = attackOffsetValue;
        }
        


        //Jumping

        if (jumpAction.triggered && IsOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }

        CheckAnimationStates();


        //Attacking
        if (attackAction.triggered && attackCooldown<=0)
        {
            if (moveInput.y == 1)
            {
                //Upward attack
                GameObject newBomb = Instantiate(bomb, new Vector3(transform.position.x + attackOffset, transform.position.y + math.abs(attackOffset)), transform.rotation);
                var bombScript = newBomb.GetComponent<Bomb>();
                bombScript.direction = attackOffset;
                attackCooldown = attackCooldownSet;
                animator.SetTrigger("isFiring");
            }
            else if (moveInput.y == -1)
            {
                //Downward attack
                GameObject newBounceBall = Instantiate(bounceBall, new Vector3(transform.position.x + attackOffset, transform.position.y), transform.rotation);
                var bounceBallScript = newBounceBall.GetComponent<BounceBall>();
                bounceBallScript.direction = attackOffset;
                attackCooldown = attackCooldownSet;
                animator.SetTrigger("isFiring");
            }
            else
            {
                //Middle attack
                Instantiate(fireball, new Vector3 (transform.position.x + attackOffset, transform.position.y), transform.rotation);
                attackCooldown = attackCooldownSet;
                animator.SetTrigger("isFiring");

            }
        }
        
        if(attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }


            //Dying
            if (transform.position.y < killFloor)
        {
            isDead = true;
            gameController.GetComponent<SpawnSection>().GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && transform.position.y > collision.transform.position.y)
        {
            IsOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 bumpPlayers = (transform.position - collision.gameObject.transform.position);
            playerRb.AddForce(bumpPlayers * playerBumpForce, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Attack"))
        {
            Vector3 bumpPlayers = (transform.position - collision.gameObject.transform.position);
            playerRb.AddForce(bumpPlayers * attackForce, ForceMode.Impulse);
        }
        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            playerRb.useGravity = false;
            playerRb.linearVelocity = Vector3.zero;
            isOnLadder = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            playerRb.useGravity = true;
            isOnLadder = false;
        }
    }



    private void CheckAnimationStates()
    {
        if (isOnLadder)
        {
            animator.SetBool("isClimbingUp", true);
        }
        else if (!isOnLadder)
        {
            animator.SetBool("isClimbingUp", false);
        }

        if (IsOnGround == true)
        {
            animator.SetBool("isGrounded", true);
        }
        if (IsOnGround == false)
        {
            animator.SetBool("isGrounded", false);
        }

        if (playerRb.linearVelocity.y == 0)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }
        else if(playerRb.linearVelocity.y < 0 && !IsOnGround)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
            //isFalling = true;
        }
        else if (playerRb.linearVelocity.y > 0)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", true);
        }

        if (moveInput.x != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else if (moveInput.x == 0)
        {
            animator.SetBool("isRunning", false);
        }

        if(attackCooldown <= 0)
        {
            animator.SetBool("coolDownDone", true);
        }
        else if (attackCooldown > 0)
        {
            animator.SetBool("coolDownDone", false);
        }
    }
    /*
    private void GetGrassVelocity()
    {
        groundVelocity = groundRb.linearVelocity.y;
    }
    */
}

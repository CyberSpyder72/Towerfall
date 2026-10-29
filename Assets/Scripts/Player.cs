using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private Rigidbody playerRb;


    public InputAction moveAction;
    private Vector2 moveInput;
    private int speed = 5;
    


    public InputAction jumpAction;
    private float jumpForce = 10;
    private bool IsOnGround = true;

    private float zCenter = -2; //To make sure they don't become misaligned
    //private Quaternion startRotation = Quaternion.Euler(0,0,0);

    private float playerBumpForce = 2.5f; //How much players push eachother

    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        moveAction.Enable();
        jumpAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        

        //transform.rotation = startRotation;
        if(transform.position.z != zCenter)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zCenter);
        }

        //Movement
        moveInput = moveAction.ReadValue<Vector2>();
        transform.Translate(Vector3.right * Mathf.Abs(moveInput.x) * Time.deltaTime * speed);

        if (moveInput.x == -1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); ;
        }
        if (moveInput.x == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); ;
        }

        //Jumping

        if (jumpAction.triggered && IsOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 bumpPlayers = (transform.position - collision.gameObject.transform.position);
            playerRb.AddForce(bumpPlayers * playerBumpForce, ForceMode.Impulse);
        }
    }
}

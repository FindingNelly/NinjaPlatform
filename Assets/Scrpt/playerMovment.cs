using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovment : MonoBehaviour
{    //referances
    Rigidbody2D myRigidbody;
    Animator myAnimatior;
    CapsuleCollider2D myCapsuleCollider;

    //inspector
    [Header("Player movment")]
    [SerializeField] float runSpeed = 7.5f;
    [SerializeField] float jumpSpeed = 22f;
    [SerializeField] float climbSpeed = 7.5f;

    //variables
    Vector2 moveInput;
    Vector2 playerVelocity;
    float normalPlayerGravity;


    void Start()
    {
        //catching ref
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myAnimatior = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        normalPlayerGravity = myRigidbody.gravityScale;
    }

    
    void Update()
    {
        Run();
        FlipSprite();
        isJumping();
        isClimbing();
       
    }
    
    //bools
    bool isPlayerRunning()
    {
        return Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    }

    bool isPlayerOnGround()
    {
        return myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    bool isPlayerInLadder()
    {
        return myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }


    //from inputmanger methots

    void OnJump(InputValue value)
    {
        if (isPlayerOnGround() && value.isPressed)
        {
            myRigidbody.velocity = new Vector2(1f, jumpSpeed);
        }         
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }



    //other methots

    void OnCollisionEnter2D(Collision2D collision)
    {
        //jumping animation off when thoughing ground layer

        myAnimatior.SetBool("isJumping", false);
    }


    void FlipSprite()
    {
        if  (isPlayerRunning())
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }


    void Run()
    {
        playerVelocity= new Vector2(moveInput.x*runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        
        myAnimatior.SetBool("isRunning",isPlayerRunning());

    }


    void isJumping()
    {
        if (!isPlayerOnGround())
        {
            myAnimatior.SetBool("isRunning", false);
            myAnimatior.SetBool("isJumping", true);
        }
    }


    void isClimbing()
    {
        if (isPlayerInLadder())
        {
            playerVelocity=new Vector2(myRigidbody.velocity.x,moveInput.y*climbSpeed);
            myRigidbody.velocity=playerVelocity;

            myAnimatior.SetBool("isJumping", false);

            myRigidbody.gravityScale = 0;
        }
        else
        {
            myRigidbody.gravityScale = normalPlayerGravity;
        }
    }
    
     
}

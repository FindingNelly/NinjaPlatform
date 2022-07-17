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
    [SerializeField] float playerSpeed = 7.5f;
    [SerializeField] float jumpSpeed = 22f;
   
    //variables
    Vector2 moveInput;
    
    void Start()
    {
        //catching ref
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myAnimatior = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Run();
        FlipSprite();
        isJumping();
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


   
    //from inputmanger methots

    void OnJump(InputValue value)
    {
        if (isPlayerOnGround() && value.isPressed)
        {
            myRigidbody.velocity = new Vector2(1f, jumpSpeed);
            myAnimatior.SetBool("isRunning", false);

            myAnimatior.SetBool("isJumping", true);

        }
       
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myAnimatior.SetBool("isJumping", false);

    }
    //other methots
    void FlipSprite()
    {
        if  (isPlayerRunning())
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
    void Run()
    {
        Vector2 playerVelocity= new Vector2(moveInput.x*playerSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        
        myAnimatior.SetBool("isRunning",isPlayerRunning());

    }
    void isJumping()
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            
        }
        
    }
    
     
}

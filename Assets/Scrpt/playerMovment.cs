using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovment : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Animator myAnimation;

    [Header("Player movment")]
    [SerializeField] float playerSpeed = 7.5f;
    [SerializeField] float jumpSpeed = 22f;




    Vector2 moveInput;
    
    void Start()
    {
        myAnimation = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        
        Run();
        FlipSprite();
        
    }

    private bool isPlayerRunning()
    {
        return Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            myRigidbody.velocity = new Vector2(1f, jumpSpeed);
        }
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
        Vector2 playerVelocity= new Vector2(moveInput.x*playerSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        
        myAnimation.SetBool("isRunning",isPlayerRunning());
    }
    
     
}

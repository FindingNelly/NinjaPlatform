using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovment : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField]float playerSpeed = 10;
    

    Vector2 moveInput;
    
    void Start()
    {
       myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        
    }
    void OnMove(InputValue value)
    {
        moveInput=value.Get<Vector2>();
        Run();
    }
    void Run()
    {
        Vector2 playerVelocity= new Vector2(moveInput.x*playerSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    }
    
     
}

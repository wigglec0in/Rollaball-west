using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    //int jumpCount;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    //help for double jump feature given from a question on stack overflow

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winTextObject.SetActive(false);
        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
       /*
        //Keys
        bool jump = Input.GetKeyDown(KeyCode.Space);

        //Statics
        bool isGrounded = Physics.CheckSphere(groundDetector.position, 0.1f, ground);
        bool isJumping = jump && isGrounded;
        if(isGrounded)
        {
            jumpCount = 0;
        }

        if(isJumping && isGrounded)
        {
            if (isJumping && (jumpCount <= 2))
            {
                jumpCount++;
                if((jumpCount == 1)||(jumpCount == 2))  
                {
                    rb.AddForce(Vector3.up * movement);
                }
            }

        }
        */

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            count = count +1;
            other.gameObject.SetActive(false);   
            SetCountText();
        if(count >=11)
        {
            winTextObject.SetActive(true);
        }   
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    //double jump
    public float jumpHeight = 8;
    public int MaxJump = 2;
    public int remaining = 0;
    public bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        isGrounded = true;
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

    void Update()
    {
        //main logic for jumping
        //did we press space? if so be jumping
        //is there are remaining jumps? if  so allow more jumping
        if((Input.GetKeyDown(KeyCode.Space))&&(remaining > 0))
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse); //impulse instantly applies
            remaining--;
            //main jump logic! 
        }
    }

    void FixedUpdate()
    {
        //when something happens this will happen regardless of what
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            count = count +1;
            other.gameObject.SetActive(false);   
            SetCountText();
        if(count >=12)
        {
            winTextObject.SetActive(true);
        }   
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //are we hitting the ground?
        if(collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            remaining = MaxJump;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //are we off of the ground?
        if((collision.gameObject.CompareTag("ground")))
        {
            isGrounded = false;
        }
    }

}

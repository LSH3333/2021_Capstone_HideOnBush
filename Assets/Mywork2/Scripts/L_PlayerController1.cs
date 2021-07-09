using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_PlayerController1 : MonoBehaviour
{
    public float movementSpeed = 3;
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    Rigidbody rb;
    Animator ani;

    void Start()
    {
        //ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ControllPlayer();
    }

    void ControllPlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);


        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            ani.SetBool("Idle", false);
            ani.SetTrigger("Walk");
        }
        else
        {
            ani.SetBool("Idle", true);
        }

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        
        if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
            rb.AddForce(0, jumpForce, 0);            
            canJump = Time.time + timeBeforeNextJump;
            
        }
        
    }
}

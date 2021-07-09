using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class L_PlayerController : MonoBehaviour
{
    public float speed = 60f; // character speed 
    public float maxSpeed = 10f; // limit spped
    private Rigidbody rb;
    private Vector3 movement;

    public float dashSpeed = 20f;
    private bool dashPossible = false;
    private bool dashing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        // spped가 maxspeed에 도달하면 고정시킴 
        if (rb.velocity.magnitude > maxSpeed && !dashing)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        moveCharacter(movement);        
        Dash();
    }

    void moveCharacter(Vector3 dir)
    {
        // player가 이동방향 바라보게함 
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            //ani.SetBool("Idle", false);
            //ani.SetTrigger("Walk");
        }

        //rb.AddForce(dir * speed);
        rb.MovePosition(transform.position + (dir * speed * Time.deltaTime));


    }


    void Dash()
    {
        // space 누르면 대쉬
        if (Input.GetButtonDown("Jump"))
        {
            dashing = true;
            rb.AddForce(transform.forward * 500f);                
            //rb.MovePosition(transform.position + (transform.forward * dashSpeed * Time.deltaTime));
        }

        // dash 해서 velocity가 일정이상 넘어가면 dash stop
        if(rb.velocity.magnitude > 30f)
        {
            dashing = false; 
        }
    }
}

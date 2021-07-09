using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class L_ChickController : MonoBehaviour
{
    public float speed = 60f; // character speed 
    public float maxSpeed = 10f; // limit spped
    private Rigidbody rb;
    private Animator ani;
    private Vector3 movement;

    public float dashSpeed = 20f;
    private bool dashPossible = false;
    private bool dashing = false;

    // EatMushroom 스크립트를 위한 변수 
    // chick이 먹는중인지 아닌지 판별 
    [HideInInspector]
    public bool chickEating = false; // chick이 eating 중일때 

    // sound
    [SerializeField]
    private AudioSource footStepSound;
    [SerializeField]
    private AudioSource eatingSound;
    [SerializeField]
    private AudioSource dashSound;
    // 덤불 숨을때 나갈때 소리 
    [SerializeField]
    private AudioSource bushSound;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        BushSoundPlay();


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
        Eat();
    }

    void moveCharacter(Vector3 dir)
    {
        // player가 이동방향 바라보게함 
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            ani.SetBool("Walk", true);            
            if(!footStepSound.isPlaying)
            {
                footStepSound.Play();
            }

        }
        else
        {
            ani.SetBool("Walk", false);
            footStepSound.Stop();
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
            dashSound.Play();
            //rb.MovePosition(transform.position + (transform.forward * dashSpeed * Time.deltaTime));
        }

        // dash 해서 velocity가 일정이상 넘어가면 dash stop
        if (rb.velocity.magnitude > 30f)
        {
            dashing = false;
        }
    }

    // chick이 먹이를 먹는다 
    void Eat()
    {
        // left ctrl button을 누르고있으면 먹는다 
        if(Input.GetButton("Eat"))
        {
            // 먹는 animation 
            ani.SetBool("Eat", true);
            // 먹는 sound
            if (!eatingSound.isPlaying)
                eatingSound.Play();

            chickEating = true; // chickEating set true when eating 
        }
        else
        {
            ani.SetBool("Eat", false);
            chickEating = false;
        }
        
    }

    void BushSoundPlay()
    {
        if(L_BushHide.BushSound == 1)
        {
            bushSound.Play();
            L_BushHide.BushSound = 0;
        }
        else if(L_BushHide.BushSound == 2)
        {
            bushSound.Play();
            L_BushHide.BushSound = 0;
        }
    }
}

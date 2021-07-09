using System;
using UnityEngine;

public class MyCharacterControl : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private Rigidbody2D _rb2d;
    [SerializeField]
    private SpriteRenderer _renderer;

    private float moveInput_hor;
    private float moveInput_ver;
    [SerializeField]
    private float movePower = 1f;

    private void FixedUpdate()
    {
        moveInput_hor = Input.GetAxis("Horizontal");
        _rb2d.velocity = new Vector2(moveInput_hor * movePower, _rb2d.velocity.y);

        moveInput_ver = Input.GetAxis("Vertical");
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, moveInput_ver * movePower);

        Animating(moveInput_hor, moveInput_ver);
    }

    void Animating(float h, float v)
    {
        // walking animation
        bool walking = (h != 0f) || (v != 0f);
        _anim.SetBool("Walking", walking);
        
        // 캐릭터 이동방향으로 바라보게 
        if(h < 0)
        {
            _renderer.flipX = true;
        }
        else
        {
            _renderer.flipX = false;
        }

    }
}

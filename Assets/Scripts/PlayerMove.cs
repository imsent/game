using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D coll;


    
    

    public int coins;

    
    [SerializeField] private AudioSource jumpSound;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (anim.GetBool("death")) return;
        if (isGrounded())
        {
            anim.SetBool("isFalling",false);
        }
        else
        {
            if (!(anim.GetBool("isJumping") || anim.GetBool("isDouble")))
            {
                anim.SetBool("isFalling",true);
            }
        }

        Crouching();
        Moving();
        Jumping();

    }
    
    private void Falling()
    {
        anim.SetBool("isFalling",true);
        anim.SetBool("isJumping",false);
        anim.SetBool("isDouble",false);
    }
    
    public float speedPercent;
    public float speed;
    private float Move;
    private void Moving()
    {
        Move = Input.GetAxis("Horizontal");
        rb.velocity = anim.GetBool("isCrouch") ? new Vector2(crouchSpeed * Move, rb.velocity.y) : new Vector2(speed * Move, rb.velocity.y);
        anim.SetBool("isRunning", Move != 0f);
        if (anim.GetBool("isRunning"))
        {
            transform.rotation = Quaternion.Euler(0, rb.velocity.x > 0 ? 0 : 180, 0);
        }
            
            
    }

    private bool doubleJumpAvailable;
    public float jumpPercent;
    public float jump;
    private void Jumping()
    {
        if (!Input.GetButtonDown("Jump")) return;
        if (isGrounded())
        {
            jumpSound.Play();
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            doubleJumpAvailable = true;
            anim.SetBool("isJumping",true);
        }
        else if (doubleJumpAvailable)
        {
            anim.SetBool("isDouble",true);
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            doubleJumpAvailable = false;
        }
    }

    public float crouchSpeed;
    private void Crouching()
    {
        anim.SetBool("isCrouch", Input.GetKey(KeyCode.C));
    }
    
    public LayerMask layerGround;
    private bool isGrounded()
    {
        return  Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, layerGround).collider != null;
    }
    
}

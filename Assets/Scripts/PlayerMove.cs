using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public Animator anim;
    private Rigidbody2D rb;
    private PolygonCollider2D coll;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        coll = GetComponent<PolygonCollider2D>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (isGrounded())
        {
            anim.SetBool("isFalling",false);
        }
        Crouching();
        Moving();
        Jumping();

    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground")) return;
        
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (!anim.GetBool("isJumping")) Falling();
        
    }

    private void Falling()
    {
        anim.SetBool("isFalling",true);
        anim.SetBool("isJumping",false);
    }

    public float speed;
    private float Move;
    private void Moving()
    {
        Move = Input.GetAxis("Horizontal");
        rb.velocity = anim.GetBool("isCrouch") ? new Vector2(crouchSpeed * Move, rb.velocity.y) : new Vector2(speed * Move, rb.velocity.y);
        anim.SetBool("isRunning", Move != 0f);
        if (anim.GetBool("isRunning"))
            transform.rotation = Quaternion.Euler(0, rb.velocity.x > 0 ? 0 : 180, 0);
    }

    public bool isJumping;
    private bool doubleJumpAvailable;
    public float jump;
    private void Jumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded())
            {
                rb.AddForce(new Vector2(rb.velocity.x, jump));
                doubleJumpAvailable = true;
                anim.SetBool("isJumping",true);
            }
            else if (doubleJumpAvailable)
            {
                rb.AddForce(new Vector2(rb.velocity.x, jump));
                doubleJumpAvailable = false;
            }
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
        var s = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, layerGround).collider;
        return  s != null;
    }
    
}

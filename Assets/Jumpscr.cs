using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Jumpscr : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Jump system")]
    [SerializeField] float jumptime;
    [SerializeField] int jumppower;
    [SerializeField] float fallmultipier;
    [SerializeField] float jumpMultiplier;

    public CapsuleCollider2D feet;
    public Transform groundcheck;
    public LayerMask groundLayer;
    //bool isGrounded;
    Vector2 vecgravity;

    bool isJumping;
    float jumpCounter;




    void Start()
    {
        vecgravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        feet = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Jump") && isGrounded()) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumppower);
            isJumping = true;
            jumpCounter = 0;
        }

        if(rb.velocity.y >0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if(jumpCounter > jumptime) isJumping = false;

            float t = jumpCounter / jumptime;
            float CurrentJumpM = jumpMultiplier;

            if (t > 0.5f)
            {
                CurrentJumpM = jumpMultiplier * (1 - t);
            }

            rb.velocity += vecgravity * jumpMultiplier * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpCounter = 0;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y*0.6f);
            }
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecgravity * fallmultipier * Time.deltaTime;
        }
    }
    bool isGrounded() 
    {
        return Physics2D.OverlapCapsule(groundcheck.position, new Vector2(0.5f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        //return feet;
    
    }

}

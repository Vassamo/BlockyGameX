using UnityEngine;

public class Jumpscr : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Jump system")]
    [SerializeField] float jumptime;
    [SerializeField] int jumppower;
    [SerializeField] float fallmultipier;
    [SerializeField] float jumpMultiplier;

    public Transform groundcheck;
    public LayerMask groundLayer;
    Vector2 vecgravity;

    bool isJumping;
    float jumpCounter;

    Animator anim;


    void Start()
    {
        vecgravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 37f);}


        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumppower);
            isJumping = true;
            jumpCounter = 0;
        }

        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumptime) isJumping = false;

            float t = jumpCounter / jumptime;
            float CurrentJumpM = jumpMultiplier;

            if (t > 0.5f) CurrentJumpM = jumpMultiplier * (1 - t);

            rb.velocity += vecgravity * CurrentJumpM * Time.deltaTime;
            
        }

        if (!isGrounded())
        {            
            anim.SetInteger("fallY", (int)rb.velocity.y);
            anim.SetBool("jumbool", true);

            //isBlobReady
            if (Physics2D.OverlapBox(new Vector2(groundcheck.position.x, groundcheck.position.y - 2f), new Vector2(0.2f, 0.5f), 0, groundLayer) && (rb.velocity.y < -20f))
                anim.SetTrigger("blob");
        }
        else
        {
            anim.ResetTrigger("blob");
            anim.SetInteger("fallY", 0);
        }


        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpCounter = 0;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
                
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecgravity * fallmultipier * Time.deltaTime;
            if(isGrounded()) anim.SetBool("jumbool", false);
        }
    }
    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundcheck.position, new Vector2(0.5f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }


}

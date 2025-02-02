using UnityEngine;

public class Jumpscr : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Jump system")]
    [SerializeField] float jumptime;
    [SerializeField] int jumppower = 2;
    [SerializeField] float fallmultipier;
    [SerializeField] float jumpMultiplier;

    public AudioSource playjump;
    public AudioSource PlayMegaJump;

    public Transform groundcheck;
    public LayerMask groundLayer;
    private string jumpSTR = "Jump";
    Vector2 vecgravity;

    bool isJumping;
    float jumpCounter;

    public Animator anim;
    private PlayerMovement PlayerMovement;
    //float MoveSpeedInt;


    void Start()
    {
        vecgravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        //MoveSpeedInt = PlayerMovement.moveSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 37f);
            PlayMegaJump.Play();
        }


        if (Input.GetButtonDown(jumpSTR) || Input.GetKeyDown(KeyCode.W) 
            || Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("spacejump", true);
            if (isGrounded())
            {
                PlayerMovement.SplatSound.Stop(); //zakonczenie dzwieku wslizgu
                playjump.Play(); //odtworzenie dzwieku skoku
                rb.velocity = new Vector2(rb.velocity.x, jumppower);
                isJumping = true;
                jumpCounter = 0;
            }
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
            anim.SetBool("jumbool", false);
        }


        if (Input.GetButtonUp(jumpSTR) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
            jumpCounter = 0;
            anim.SetBool("spacejump", false);

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
                
        }


        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecgravity * fallmultipier * Time.deltaTime;
            //if(isGrounded()) anim.SetBool("jumbool", false);
        }

    }
    public bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundcheck.position, new Vector2(0.4f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundcheck.position, new Vector2(0.4f, 0.1f)); // U¿ywamy DrawWireCube, aby narysowaæ kapsu³ê
    }

}

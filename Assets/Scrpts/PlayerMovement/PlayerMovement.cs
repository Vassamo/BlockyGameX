using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    private SpriteRenderer sr;

    public float moveInput;

    /*    public float jumpForce;
        private bool isGrounded;
        public Transform feetPos;
        public float checkRadius;
        public LayerMask whatIsGround;
        public float jumpStartTime;
        private float jumpTime;
        private bool isJumping;
        [SerializeField] float fallmultipier;
        Vector2 vecgravity;*/

    public float slideSpeedInt; // Prêdkoœæ wœlizgu
    public float slideDuration; // Czas trwania wœlizgu
    public float slideCooldown; // Czas odnowienia wœlizgu
    public float slideDeceleration;

    //private Rigidbody2D rb;
    private float slideTime;
    private float lastSlideTime;
    private bool isSliding = false;
    private float slideSpeed;

    private Jumpscr jumpscr;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        jumpscr = GetComponent<Jumpscr>();
        //vecgravity = new Vector2(0, -Physics2D.gravity.y);
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        FaceMoveDirection();
        //Jump();
        //Debug.Log(moveInput);

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
    && Time.time >= lastSlideTime + slideCooldown && jumpscr.isGrounded())
        {
            StartSlide();
        }

        if (isSliding && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.DownArrow))
        {
            StopSlide();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (isSliding)
        {
            if (moveInput != 0)
            {
                // Ustawiamy prêdkoœæ wœlizgu w osi X
                rb.velocity = new Vector2(slideSpeed * Mathf.Sign(rb.velocity.x), rb.velocity.y);
                slideTime += Time.fixedDeltaTime;

                // Zmniejszamy prêdkoœæ wœlizgu w czasie
                slideSpeed -= slideDeceleration * Time.fixedDeltaTime;

                // Zakoñcz wœlizg, jeœli prêdkoœæ spadnie poni¿ej zera
                if (slideSpeed <= 0)
                {
                    // Zatrzymaj ruch, jeœli klawisz jest wciœniêty
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                    {

                        rb.velocity = new Vector2(0, rb.velocity.y); // Zatrzymaj ruch w osi X

                    }
                    else
                    {
                        StopSlide();
                    }
                }
                if (!jumpscr.isGrounded())
                {
                    StopSlide();
                }
            }
        }
        else
        {
            // Normalny ruch w osi X
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

    }
    void StartSlide()
    {
        isSliding = true;
        slideTime = 0f;
        lastSlideTime = Time.time;
        if (moveInput != 0)
            slideSpeed = slideSpeedInt;
        else
            slideSpeed = 0f;
        jumpscr.anim.SetBool("isCrouch", true);
    }
    /*    void Jump()
        {


            if (IsGrounded() && Input.GetButtonDown("Jump"))
            {

                isJumping = true;
                jumpTime = jumpStartTime;
                rb.velocity = Vector2.up * jumpForce;
            }

            if (Input.GetButton("Jump") && isJumping == true)
            {
                if (jumpTime > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;

                    jumpTime -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }

            if (rb.velocity.y < 0)
            {
                rb.velocity -= vecgravity * fallmultipier * Time.deltaTime;
            }
        }*/

    /*   bool IsGrounded()
       {
           return Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
       }*/

    void FaceMoveDirection()
    {
        if (moveInput > 0)
        {
            sr.flipX = false;
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
        }
    }

    void StopSlide()
    {
        isSliding = false;
        rb.velocity = new Vector2(0, rb.velocity.y); // Przywróæ normaln¹ prêdkoœæ w osi Y
        jumpscr.anim.SetBool("isCrouch", false);
    }

    /*    void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(feetPos.position, checkRadius);
        }*/
}

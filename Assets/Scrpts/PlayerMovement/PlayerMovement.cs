using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    private SpriteRenderer sr;

    public float moveInput;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        jumpscr = GetComponent<Jumpscr>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        //FaceMoveDirection(); //jak bdziesz obracanie robil wizualnie

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
}

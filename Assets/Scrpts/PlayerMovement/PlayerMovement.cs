using System.Collections;
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

    public AudioSource SplatSound;

    public AudioSource WalkingSound; 
    public float fadeDuration = 0.5f; 
    public float walkingVolume = 1.0f; 

    //private Rigidbody2D rb;
    private float slideTime;
    private float lastSlideTime;
    private bool isSliding = false;
    private float slideSpeed;

    private Jumpscr jumpscr;

    private bool isWalking = false;
    private Coroutine fadeCoroutine;

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
        //if (!jumpscr.isGrounded())
        //{
        //    SplatSound.Stop();
        //}

        WalkerIfs();
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
        SplatSound.Play();
        jumpscr.anim.SetBool("isCrouch", true);
    }
    void StopSlide()
    {
        isSliding = false;
        rb.velocity = new Vector2(0, rb.velocity.y); // Przywróæ normaln¹ prêdkoœæ w osi Y
        SplatSound.Stop();
        jumpscr.anim.SetBool("isCrouch", false);
    }
    private bool IsMoving()
    {
        
        return Mathf.Abs(moveInput) > 0.1f; // U¿ywamy progu, aby unikn¹æ fa³szywych pozytywnych wyników
    }

    void WalkerIfs()
    {
        if (IsMoving() && jumpscr.isGrounded() && !isSliding)
        {
            if (!isWalking)
            {
                // Jeœli dŸwiêk jest w trakcie fade-out, zatrzymaj go i rozpocznij fade-in
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(PlayWalkingSound());
            }
        }
        else
        {
            if (isWalking)
            {
                // Jeœli dŸwiêk jest w trakcie fade-in, zatrzymaj go i rozpocznij fade-out
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(StopWalkingSound());
            }
        }
    }
    private IEnumerator PlayWalkingSound()
    {
        isWalking = true;
        WalkingSound.volume = 0f; // Ustaw g³oœnoœæ na 0
        WalkingSound.Play(); // Rozpocznij odtwarzanie dŸwiêku

        // Fade-in
        while (WalkingSound.volume < walkingVolume)
        {
            WalkingSound.volume += Time.deltaTime / fadeDuration;
            yield return null; // Czekaj na nastêpn¹ klatkê
        }

        WalkingSound.volume = walkingVolume; // Ustaw g³oœnoœæ na docelow¹
    }

    private IEnumerator StopWalkingSound()
    {
        // Fade-out
        while (WalkingSound.volume > 0f)
        {
            WalkingSound.volume -= Time.deltaTime / fadeDuration;
            yield return null; // Czekaj na nastêpna klatkê
        }

        WalkingSound.Stop(); // Zatrzymaj odtwarzanie dŸwiêku
        WalkingSound.volume = 1f; // Przywróæ g³oœnoœæ do domyœlnej wartoœci
        isWalking = false;
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

}

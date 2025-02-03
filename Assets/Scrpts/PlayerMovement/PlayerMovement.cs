using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

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
    
    public AudioClip WalkingSoundGrass; 
    public AudioClip WalkingSoundStone; 
    public AudioClip WalkingSoundWood;

    public float fadeDuration = 0.5f; 
    public float walkingVolume = 1.0f; 

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
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.S) 
            || Input.GetKeyDown(KeyCode.DownArrow))
            && jumpscr.isGrounded())
        {
            StartSlide();
        }

        if (isSliding && !Input.GetKey(KeyCode.LeftShift) 
            && !Input.GetKey(KeyCode.S) 
            && !Input.GetKey(KeyCode.DownArrow))
        {
            StopSlide();
        }

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
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

    }
    void StartSlide()
    {
        isSliding = true;
        slideTime = 0f;

        if (moveInput != 0)
            slideSpeed = slideSpeedInt;
        else
            slideSpeed = 0f;

        SplatSound.Play(); //odtworzenie dzwieku wslizgu
        jumpscr.anim.SetBool("isCrouch", true);
    }
    void StopSlide()
    {
        isSliding = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        SplatSound.Stop(); // przerwanie dzwieku wslizgu
        jumpscr.anim.SetBool("isCrouch", false);
    }
    private bool IsMoving()
    {
        
        return Mathf.Abs(moveInput) > 0.1f;
    }

    void WalkerIfs()
    {
        if (IsMoving() && jumpscr.isGrounded() && !isSliding)
        {
            if (!isWalking)
            {
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

        //zmiana dzwieku podloza
        CheckSurface();
        WalkingSound.volume = 0f;
        WalkingSound.Play();
       
        while (WalkingSound.volume < walkingVolume)
        {
            WalkingSound.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        WalkingSound.volume = walkingVolume; // glosnosc docelowa
    }

    private IEnumerator StopWalkingSound()
    {
        while (WalkingSound.volume > 0f)
        {
            WalkingSound.volume -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        WalkingSound.Stop(); 
        WalkingSound.volume = 1f;
        isWalking = false;
    }

    void CheckSurface()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(jumpscr.groundcheck.position, 0.1f);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("grass"))
            {   
                WalkingSound.clip = WalkingSoundGrass;
                return; 
            }
            else if (hitCollider.CompareTag("stone"))
            {   
                WalkingSound.clip = WalkingSoundStone;
                return; 
            }
            else if (hitCollider.CompareTag("wood"))
            {
                WalkingSound.clip = WalkingSoundWood;
                return;
            }
        }
    }

/*    void FaceMoveDirection()
    {
        if (moveInput > 0)
        {
            sr.flipX = false;
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
        }
    }*/

}

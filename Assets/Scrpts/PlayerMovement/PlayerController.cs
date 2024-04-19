using UnityEngine;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rgdBody;

    Animator anim;
    [SerializeField] int jumpPower;
    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isGrounded;
    public Transform startPoint;

    public float heroSpeed;
    //bool dirToRight = true;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rgdBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.3f, 0.13f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        float horizontalMove = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(horizontalMove));
        rgdBody.velocity = new Vector2(horizontalMove * heroSpeed, rgdBody.velocity.y);


        /*        if (horizontalMove < 0 && dirToRight)
                {
                    Flip();
                }
                if (horizontalMove > 0 && !dirToRight)
                {
                    Flip();
                }*/
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("dead"))
        {
            rgdBody.velocity = Vector2.zero;
            return;
        }
    }
    /*    void Flip()
        {
            dirToRight = !dirToRight;
            Vector3 heroScale = gameObject.transform.localScale;
            heroScale.x *= -1;
            gameObject.transform.localScale = heroScale;

        }*/

    public void restartHero()
    {
        gameObject.transform.position = startPoint.position;
    }
}
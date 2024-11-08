using UnityEngine;
using UnityEngine.Audio;

public class WaterCollision : MonoBehaviour
{
    private BoxCollider2D playerCollider;
    private Rigidbody2D body2D;
    public LayerMask WaterLayer;    //also zobacz to bo sie odpina w unity
    public AudioSource WaterBubbles;
    public AudioSource WaterIn;
    public AudioSource WaterOut;
    public AudioMixerGroup MasterMusic;
    private float intGravScale;
    private float intMoveSpeed;
    bool amIUnderWater;
    private PlayerMovement PlayerMovement;
    private int WaterIDAfter; //jesli zyebala sie dynamiczna muzyka wody to patrz nizej :)

    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        body2D = GetComponent<Rigidbody2D>();
        intGravScale = body2D.gravityScale;
        PlayerMovement = GetComponent<PlayerMovement>();
        intMoveSpeed = PlayerMovement.moveSpeed;
        WaterIDAfter = WaterLayer.value - 12;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("x: " + other.gameObject.layer);
        //Debug.Log("d: "  + WaterIDAfter);           // < bugfixing situation 
        
        if (other.gameObject.layer == WaterIDAfter) //bo 4 layer i musze odjac 12 bo hihi thans unity
        {
            Debug.Log("in");
            body2D.gravityScale = intGravScale / 2;
            MasterMusic.audioMixer.SetFloat("MusicFilter", 700);
            WaterIn.Play();
            WaterBubbles.Play();
            PlayerMovement.moveSpeed = intMoveSpeed / 2;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.gameObject.layer == WaterIDAfter)
        {
            Debug.Log("out");
            body2D.gravityScale = intGravScale;
            MasterMusic.audioMixer.SetFloat("MusicFilter", 20000);
            WaterOut.Play();
            WaterBubbles.Stop();
            PlayerMovement.moveSpeed = intMoveSpeed;
        }
    }

}

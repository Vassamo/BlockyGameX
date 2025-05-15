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
    private int WaterID; //jesli zyebala sie dynamiczna muzyka wody to patrz nizej :)

    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        body2D = GetComponent<Rigidbody2D>();
        intGravScale = body2D.gravityScale;
        PlayerMovement = GetComponent<PlayerMovement>();
        intMoveSpeed = PlayerMovement.moveSpeed;
        WaterID = WaterLayer.value - 12;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {     
        if (other.gameObject.layer == WaterID)
        {
            Debug.Log("in");
            body2D.gravityScale = intGravScale / 2;
            
            //Nalozenie filtru dolnoprzepustowego
            MasterMusic.audioMixer.SetFloat("MusicFilter", 700);
            WaterIn.Play(); //Wskoczenie do wody
            WaterBubbles.Play(); //Dzwiek babelkow
            PlayerMovement.moveSpeed = intMoveSpeed / 2;
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.gameObject.layer == WaterID)
        {
            Debug.Log("out");
            body2D.gravityScale = intGravScale;

            //Przywrocenie poprzednich wartosc filtra
            MasterMusic.audioMixer.SetFloat("MusicFilter", 20000);
            WaterOut.Play(); //Wyskoczenie z wody
            WaterBubbles.Stop(); //Zakonczenie babelkow
            PlayerMovement.moveSpeed = intMoveSpeed;
        }
    }

}

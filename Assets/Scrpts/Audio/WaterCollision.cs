using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class WaterCollision : MonoBehaviour
{

    private BoxCollider2D playerCollider;
    private Rigidbody2D body2D;
    public static LayerMask WaterLayer;
    public AudioSource WaterBubbles;
    public AudioSource WaterIn;
    public AudioSource WaterOut;
    public AudioMixerGroup MasterMusic;
    private float intGravScale;
    private float intMoveSpeed;
    bool amIUnderWater;
    private PlayerMovement PlayerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        body2D = GetComponent<Rigidbody2D>();
        intGravScale = body2D.gravityScale;
        PlayerMovement = GetComponent<PlayerMovement>();
        intMoveSpeed = PlayerMovement.moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("x: " + other.gameObject.layer);
        //Debug.Log("d: "  + WaterLayer.value);
        
        if (other.gameObject.layer == WaterLayer.value - 12) //bo 4 layer i musze odjac 12 bo hihi thans unity
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
        
        if (other.gameObject.layer == WaterLayer.value - 12)
        {
            Debug.Log("out");
            body2D.gravityScale = intGravScale;
            MasterMusic.audioMixer.SetFloat("MusicFilter", 20000);
            WaterOut.Play();
            WaterBubbles.Stop();
            PlayerMovement.moveSpeed = intMoveSpeed;
        }
    }


    //private void checkig()
    //{
    //    if (amIUnderWater)
    //    {
    //        if (transitionTimer < transitionDuration)
    //        {
    //            // Interpolacja do spowolnienia
    //            float t = transitionTimer / transitionDuration;
    //            MasterMixer.SetFloat("MasterFilter", Mathf.Lerp(20000, FilterValue, t));
    //            Time.timeScale = Mathf.Lerp(originalTimeScale, targetTimeScale, t);
    //            transitionTimer += Time.unscaledDeltaTime;
    //        }
    //        else if (transitionTimer >= transitionDuration && transitionTimer < transitionDuration + slowMotionDuration)
    //        {
    //            // Utrzymywanie spowolnienia
    //            Time.timeScale = targetTimeScale;
    //            MasterMixer.SetFloat("MasterFilter", FilterValue);
    //            transitionTimer += Time.unscaledDeltaTime;
    //        }
    //        else
    //        {
    //            // Rozpoczêcie zakoñczenia spowolnienia
    //            isSlowMotionActive = false;
    //            isEndingSlowMotion = true;
    //            transitionTimer = 0f; // Resetujemy timer dla fazy koñcowej
    //        }
    //    }
    //}

}

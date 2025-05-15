using UnityEngine;
using UnityEngine.Audio;

public class Volume2D : MonoBehaviour
{
    public Transform listenerTransform;
    public AudioSource audioSourceRadio;
    //public AudioMixerGroup radioMixerGroup;
    public AudioMixerGroup bgMusicMixerGroup;
    public float DuckValue = 40;
    private CircleCollider2D circleCollider2D;
    public float minDist = 1;
    static float maxDist;
    private string ParamName = "BgMusicVolumeRadio";

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>(); // scale broken bdw
        maxDist = circleCollider2D.radius;
        audioSourceRadio.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D gracz)
    {
        if (gracz.CompareTag("Player") && 
            audioSourceRadio.isPlaying == false)
        {
            audioSourceRadio.enabled = true;
            audioSourceRadio.time = Random.Range(1, 30);
            audioSourceRadio.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float dist = Vector3.Distance(transform.position, listenerTransform.position);

            if (dist < minDist)
            {
                audioSourceRadio.volume = 1;
                bgMusicMixerGroup.audioMixer.SetFloat(ParamName, -DuckValue);
            }
            else if (dist > maxDist)
            {
                audioSourceRadio.volume = 0;
                bgMusicMixerGroup.audioMixer.SetFloat(ParamName, 0);
            }
            else
            {
                float rangeDist = Mathf.Clamp(dist, minDist, maxDist) - minDist;                        // Calculate distance in the range from minDist to maxDist
                float distval = rangeDist / (maxDist - minDist);                                        // Calculate volume value based on the distance in the range from minDist to maxDist
                audioSourceRadio.volume = 1 - distval;
                bgMusicMixerGroup.audioMixer.SetFloat(ParamName, Mathf.Lerp(-DuckValue, 0, distval));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSourceRadio.Stop();
            audioSourceRadio.enabled = false;
            bgMusicMixerGroup.audioMixer.SetFloat(ParamName, 0);                            // Restore background music volume when exiting
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDist);
    }
}

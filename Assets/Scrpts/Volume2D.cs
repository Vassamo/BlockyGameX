using UnityEngine;
using UnityEngine.Audio;

public class Volume2D : MonoBehaviour
{
    public Transform listenerTransform;
    public AudioSource audioSourceRadio;
    public AudioMixerGroup radioMixerGroup;
    public AudioMixerGroup bgMusicMixerGroup;
    public int DuckValue = 40; 
    private CircleCollider2D circleCollider2D;
    public float minDist = 1;
    static float maxDist;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>(); // scale broken bdw
        maxDist = circleCollider2D.radius;
        audioSourceRadio.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D gracz)
    {
        if (audioSourceRadio.isPlaying == false)
        {
            audioSourceRadio.enabled = true;
            audioSourceRadio.time = Random.Range(1, 30);
            audioSourceRadio.Play();
            Debug.Log("play");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float dist = Vector3.Distance(transform.position, listenerTransform.position);

        if (dist < minDist)
        {
            audioSourceRadio.volume = 1;
            bgMusicMixerGroup.audioMixer.SetFloat("BgMusicVolume", -DuckValue); // Mute background music
        }
        else if (dist > maxDist)
        {
            audioSourceRadio.volume = 0;
            bgMusicMixerGroup.audioMixer.SetFloat("BgMusicVolume", 0); // Max volume for background music
        }
        else
        {
            // Calculate distance in the range from minDist to maxDist
            float rangeDist = Mathf.Clamp(dist, minDist, maxDist) - minDist;

            // Calculate volume value based on the distance in the range from minDist to maxDist
            float distval = rangeDist / (maxDist - minDist);
            audioSourceRadio.volume = 1 - distval;
            bgMusicMixerGroup.audioMixer.SetFloat("BgMusicVolume", Mathf.Lerp(-DuckValue, 0, distval));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        audioSourceRadio.Stop();
        Debug.Log("stop");
        audioSourceRadio.enabled = false;
        bgMusicMixerGroup.audioMixer.SetFloat("BgMusicVolume", 0); // Restore background music volume when exiting
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDist);
    }
}

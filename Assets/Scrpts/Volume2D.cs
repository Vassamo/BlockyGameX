using UnityEngine;

public class Volume2D : MonoBehaviour
{
    public Transform listenerTransform;
    public AudioSource audioSourceRadio;
    public AudioSource audioSourceBgMusic;
    private CircleCollider2D CircleCollider2D;
    public float minDist = 1;
    static float maxDist;

    private void Awake()
    {
        CircleCollider2D = GetComponent<CircleCollider2D>();
        maxDist = CircleCollider2D.radius;
    }

    private void OnTriggerEnter2D(Collider2D gracz)
    {
        if (audioSourceRadio.isPlaying == false)
        {
            //audioSourceRadio.PlayDelayed(Random.Range(1,3));
            audioSourceRadio.time = (Random.Range(1, 30));
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
            audioSourceBgMusic.volume = 0;
        }
        else if (dist > maxDist)
        {
            audioSourceRadio.volume = 0;
            audioSourceBgMusic.volume = 1;
        }
        else
        {
            // Oblicz dystans w zakresie od minDist do maxDist
            float rangeDist = Mathf.Clamp(dist, minDist, maxDist) - minDist;

            // Oblicz wartoœæ g³oœnoœci w oparciu o dystans w zakresie minDist do maxDist
            float distval = rangeDist / (maxDist - minDist);
            //float distval = Mathf.Clamp01(dist / maxDist);
            audioSourceRadio.volume = 1 - distval;
            audioSourceBgMusic.volume = distval;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        audioSourceRadio.Stop();
        Debug.Log("stop");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDist);
    }
}

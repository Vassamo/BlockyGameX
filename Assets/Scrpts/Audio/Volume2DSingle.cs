using UnityEngine;

public class Volume2DSingle : MonoBehaviour
{
    public Transform listenerTransform;
    public AudioSource SourceSound;
    private CircleCollider2D circleCollider2D;
    public float minDist = 1;
    static float maxDist;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>(); // scale broken bdw
        maxDist = circleCollider2D.radius;
        SourceSound.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D gracz)
    {
        if (gracz.gameObject.CompareTag("Player"))
        {
            if (SourceSound != null && !SourceSound.isPlaying)
            {
                SourceSound.enabled = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float dist = Vector3.Distance(transform.position, listenerTransform.position);

        if (dist < minDist)
        {
            SourceSound.volume = 1;
        }
        else if (dist > maxDist)
        {
            SourceSound.volume = 0;
        }
        else
        {
            // Calculate distance in the range from minDist to maxDist
            float rangeDist = Mathf.Clamp(dist, minDist, maxDist) - minDist;

            // Calculate volume value based on the distance in the range from minDist to maxDist
            float distval = rangeDist / (maxDist - minDist);
            SourceSound.volume = 1 - distval;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (SourceSound != null)
                SourceSound.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDist);
    }
}

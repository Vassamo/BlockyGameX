using UnityEngine;

public class doortrigger : MonoBehaviour
{
    public AudioSource audioSource;
    private bool isPlayerInTrigger = false;

    private void Update()
    {
        // SprawdŸ, czy naciœniêto klawisz "E"
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInTrigger)
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
using UnityEngine;


public class Coin : MonoBehaviour
{
    public AudioSource collectSound; // Przypisz AudioSource w inspektorze
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (collectSound != null) collectSound.Play();

            Destroy(this.gameObject, 0.05f); // Zniszcz obiekt po 0.5 sekundy
        }
    }

}

using UnityEngine;

public class FeetKiller : MonoBehaviour
{
    public Rigidbody2D playerRigBody;
    const int jumpForce = 20; 
    private Collider2D feetcol;
    public AudioSource deathSound;

    private void Start()
    {
        //playerRigBody = GetComponent<Rigidbody2D>();
        feetcol = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("omg");
            // Zniknięcie przeciwnika

            Bounce();
            deathSound.Play();
            Destroy(other.transform.parent.gameObject);
        }
    }
    private void Bounce()
    {
        // Ustaw prędkość gracza w górę
        playerRigBody.velocity = new Vector2(playerRigBody.velocity.x, jumpForce);
    }
}

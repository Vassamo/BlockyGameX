using System;
using UnityEngine;
using UnityEngine.Audio;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f; // Czas ¿ycia pocisku
    public AudioSource playerDeath;
    public AudioMixerGroup allmusic;
    public event Action OnHitPlayer;
    void Start()
    {
        Destroy(gameObject, lifetime); // Zniszczenie pocisku po okreœlonym czasie
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnHitPlayer?.Invoke(); // Wywo³anie eventu
            Destroy(gameObject); // Zniszczenie pocisku
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EnemyKillPlayer : MonoBehaviour
{
    private Collider2D killerRange;
    public AudioSource playerDeath;
    public AudioMixerGroup allmusic;
    float liczbaSekund = 4f;
    void Start()
    {
        killerRange = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("gracz zostal rozkurwiony na amen");
            playerDeath.Play();
            allmusic.audioMixer.SetFloat("BgMusicVolume",-6);
            StartCoroutine(PrzykładKorutyny());
        }
    }

    IEnumerator PrzykładKorutyny()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(liczbaSekund);
        // Kod do wykonania po odczekaniu
        Debug.Log("restart");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}

using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EnemyKillPlayer : MonoBehaviour
{
    //private Collider2D killerRange;
    public AudioSource playerDeath;
    public AudioMixerGroup allmusic;
    float liczbaSekund = 4f;
    float startBgMusicVol;
    void Start()
    {
        //killerRange = GetComponent<Collider2D>();
        allmusic.audioMixer.GetFloat("BgMusicVolume", out startBgMusicVol);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        Debug.Log("przegrales :(");
        playerDeath.Play();
        allmusic.audioMixer.SetFloat("BgMusicVolume", -19);
        StartCoroutine(KoniecKorutyny());
    }

    IEnumerator KoniecKorutyny()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(liczbaSekund);
        // Kod do wykonania po odczekaniu
        Debug.Log("restart");
        Time.timeScale = 1;
        allmusic.audioMixer.SetFloat("BgMusicVolume", startBgMusicVol);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}

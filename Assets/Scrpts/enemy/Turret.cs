using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab pocisku
    public float shootCooldown = 1f; // Czas miêdzy strza³ami
    public float bulletSpeed = 5f; // Prêdkoœæ pocisku
    public AudioSource ShootSound;
    public AudioSource playerDeath;
    public AudioMixerGroup allmusic;
    public Transform obj1;
    public Transform obj2;

    float liczbaSekund = 4f;
    float startBgMusicVol;

    private float lastShotTime;
    private int direction = 1; // 1 = prawo, -1 = lewo
    
    void Start()
    {
        lastShotTime = Time.time;
        StartCoroutine(ChangeDirection());
        allmusic.audioMixer.GetFloat("BgMusicVolume", out startBgMusicVol);
    }

    void Update()
    {
        if (Time.time - lastShotTime > shootCooldown)
        {
            if (ShootSound.isActiveAndEnabled)
            {
            Shoot();
            lastShotTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        ShootSound.Play();
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction * bulletSpeed, 0);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.OnHitPlayer += HandleHitPlayer;

    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Zmiana kierunku co 2 sekundy
            direction *= -1; // Zmiana kierunku
            transform.localScale = new Vector3(direction, 1, 1); // Obrót wie¿yczki
            obj1.RotateAround(obj2.position, Vector3.forward, direction * 180f);
        }
    }


    private void HandleHitPlayer()
    {
        Debug.Log("Turret: Player has been hit!");

        PlayerDeath();
    }

    private void PlayerDeath()
    {
        Debug.Log("przegrales :(");
        ShootSound.Stop();
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

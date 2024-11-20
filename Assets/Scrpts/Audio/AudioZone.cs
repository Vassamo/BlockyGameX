using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioZone : MonoBehaviour
{
    [System.Serializable]
    public class AudioPoint
    {
        public float x1;
        public float x2;
        public AudioSource source1;
        public AudioSource source2;
    }

    [System.Serializable]
    public class AudioPointY
    {
        public float y1;
        public float y2;
        public AudioSource[] audioSources;
        public float EndingVolumeDB;
        public float EndingFilter;
     
    }

    public AudioPoint[] pointsX;
    public AudioPointY[] pointsY;
    public Transform playerTransform;
    private float initVol;
    private float initHighFiler;
    public AudioMixerGroup MusicBgMixer;

    private float currentT = 0f; // Aktualna wartość t
    private float targetT = 0f; // Docelowa wartość t
    private float speedMultiplier = 1.5f;

    private void Start()
    {
        MusicBgMixer.audioMixer.GetFloat("BgMusicVolume", out initVol);
        MusicBgMixer.audioMixer.GetFloat("BGHighPass", out initHighFiler);
    }

    private void Update()
    {
        float playerX = playerTransform.position.x;
        float playerY = playerTransform.position.y;

        foreach (var point in pointsX)
        {
            if (playerX < point.x1)
            {
                point.source1.volume = 1;
                point.source2.volume = 0;
            }
            else if (playerX > point.x2)
            {
                point.source1.volume = 0;
                point.source2.volume = 1;
            }
            else
            {
                float t = (playerX - point.x1) / (point.x2 - point.x1);
                point.source1.volume = 1 - t;
                point.source2.volume = t;
            }
        }

        foreach (var point in pointsY)
        {
                targetT = (playerY - point.y1) / (point.y2 - point.y1); //old t
                targetT = Mathf.Clamp01(targetT);

                currentT = Mathf.Lerp(currentT, targetT, Time.deltaTime * speedMultiplier);

                float currentVolume = Mathf.Lerp(initVol, -point.EndingVolumeDB, currentT);
                float currentFiler = Mathf.Lerp(initHighFiler, point.EndingFilter, currentT);

                MusicBgMixer.audioMixer.SetFloat("BgMusicVolume", currentVolume);
                MusicBgMixer.audioMixer.SetFloat("BGHighPass", currentFiler);
        }
    }

    private void OnDrawGizmos()
    {
        if (pointsX == null) return;
        if (pointsY == null) return;

        Gizmos.color = Color.red;
        foreach (var point in pointsX)
        {
            Gizmos.DrawLine(new Vector3(point.x1, -1000, 0), new Vector3(point.x1, 1000, 0));
            Gizmos.DrawLine(new Vector3(point.x2, -1000, 0), new Vector3(point.x2, 1000, 0));
        }

        Gizmos.color = Color.blue;
        foreach (var point in pointsY)
        {
            Gizmos.DrawLine(new Vector3(-1000, point.y1, 0), new Vector3(1000, point.y1, 0)); // Pozioma linia na y1
            Gizmos.DrawLine(new Vector3(-1000, point.y2, 0), new Vector3(1000, point.y2, 0)); // Pozioma linia na y2
        }
}
}

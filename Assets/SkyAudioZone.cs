using UnityEngine;
using UnityEngine.Audio;

public class SkyAudioZone : MonoBehaviour
{
    [System.Serializable]
    public class AudioPointY
    {
        public float y1;
        public float y2;
        public AudioSource[] audioSources;
        public float EndingVolumeDB;
        public float EndingFilter;

    }

    public AudioPointY[] pointsY;
    public Transform playerTransform;
    public AudioMixerGroup MusicBgMixer;
    private float initVol;
    private float initHighFiler;

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
        float playerY = playerTransform.position.y;

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
        if (pointsY == null) return;

        Gizmos.color = Color.blue;
        
        foreach (var point in pointsY)
        {
            Gizmos.DrawLine(new Vector3(-1000, point.y1, 0), new Vector3(1000, point.y1, 0)); // Pozioma linia na y1
            Gizmos.DrawLine(new Vector3(-1000, point.y2, 0), new Vector3(1000, point.y2, 0)); // Pozioma linia na y2
        }
    }

}

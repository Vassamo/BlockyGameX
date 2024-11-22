using System;
using UnityEngine;


public class AudioZoneYsfx : MonoBehaviour
{
    [System.Serializable]
    public class AudioPointY
    {
        public float y1;
        public float y2;
        public AudioSource[] audioSources;
    }

    public AudioPointY[] points;
    public Transform playerTransform;
    private float targetT = 0f;
    private float currentT = 0f;
    private float speedMultiplier = 1f;

    private void Start()
    {
        foreach (var point in points)
            foreach (AudioSource sound in point.audioSources)
                sound.Play();
    }

    private void Update()
    {
        float playerY = playerTransform.position.y;
      

        foreach (var point in points)
        {
            targetT = (playerY - point.y1) / (point.y2 - point.y1);
            targetT = Mathf.Clamp01(targetT);
            currentT = Mathf.Lerp(currentT, targetT, Time.deltaTime * speedMultiplier);
                foreach (AudioSource sound in point.audioSources)
                {
                    sound.volume = Mathf.Lerp(0, 1, currentT);
                }
        }
    }

    private void OnDrawGizmos()
    {
        if (points == null) return;

        Gizmos.color = Color.cyan;

        foreach (var point in points)
        {
            Gizmos.DrawLine(new Vector3(-1000, point.y1, 0), new Vector3(1000, point.y1, 0)); // Pozioma linia na y1
            Gizmos.DrawLine(new Vector3(-1000, point.y2, 0), new Vector3(1000, point.y2, 0)); // Pozioma linia na y2
        }
    }

    private void OnDestroy()
    {
        foreach (var point in points)
            foreach (AudioSource sound in point.audioSources)
                sound.Stop();
    }
}
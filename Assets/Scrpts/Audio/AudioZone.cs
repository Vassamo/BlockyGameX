using UnityEngine;

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

    public AudioPoint[] points;
    public Transform playerTransform;

    private void Update()
    {
        float playerX = playerTransform.position.x;

        foreach (var point in points)
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
    }

    private void OnDrawGizmos()
    {
        if (points == null) return;

        Gizmos.color = Color.red;
        foreach (var point in points)
        {
            Gizmos.DrawLine(new Vector3(point.x1, -1000, 0), new Vector3(point.x1, 1000, 0));
            Gizmos.DrawLine(new Vector3(point.x2, -1000, 0), new Vector3(point.x2, 1000, 0));
        }
    }
}
using UnityEngine;
public class camerafol : MonoBehaviour
{
    public GameObject hero;
    public float smoothTime;
    private Vector3 currentVel;
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 newCameraPosition = new Vector3(hero.transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, newCameraPosition, ref currentVel, smoothTime);
    }
}
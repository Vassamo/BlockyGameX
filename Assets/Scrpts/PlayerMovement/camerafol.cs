using UnityEngine;
public class camerafol : MonoBehaviour
{
    public GameObject hero;
    public float smoothTime;
    private Vector3 currentVel;
    public float ValY = 3f;
    // Update is called once per frame
    void Update()
    {
        Vector3 newCameraPosition = new Vector3(hero.transform.position.x, hero.transform.position.y + ValY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, newCameraPosition, ref currentVel, smoothTime);
        
        
        if (Input.GetKeyDown(KeyCode.Equals))
            ValY += 2f;
        if (Input.GetKeyDown(KeyCode.Minus))
            ValY -= 2f;
    }
}
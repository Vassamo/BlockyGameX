using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    // Referencja do obiektu, który ma byæ w³¹czany/wy³¹czany
    public GameObject tooltip;
    public GameObject tooltip2;
    public AudioSource NotifyMute;

    private void Start()
    {
        tooltip.SetActive(false);
    }

    void Update()
    {
        // SprawdŸ, czy klawisz M zosta³ naciœniêty
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Zmieñ aktywnoœæ obiektu
            if (tooltip != null)
            {
                tooltip.SetActive(!tooltip.activeSelf);
            }
            else
            {
                Debug.LogWarning("objectToToggle is not assigned in the inspector.");
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            // Zmieñ aktywnoœæ obiektu
            if (tooltip2 != null)
            {
                tooltip2.SetActive(!tooltip2.activeSelf);
            }
            else
            {
                Debug.LogWarning("objectToToggle is not assigned in the inspector.");
            }
        }
        if (!tooltip2.activeSelf)
            NotifyMute.mute = true;
        else
            NotifyMute.mute = false;

    }
}
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    // Referencja do obiektu, kt�ry ma by� w��czany/wy��czany
    public GameObject tooltip;
    public GameObject tooltip2;
    public AudioSource NotifyMute;

    private void Start()
    {
        tooltip.SetActive(false);
    }

    void Update()
    {
        // Sprawd�, czy klawisz M zosta� naci�ni�ty
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Zmie� aktywno�� obiektu
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
            // Zmie� aktywno�� obiektu
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
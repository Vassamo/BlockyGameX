using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    // Referencja do obiektu, kt�ry ma by� w��czany/wy��czany
    public GameObject tooltip;
    public GameObject tooltipsound;

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
            if (tooltipsound != null)
            {
                tooltipsound.SetActive(!tooltipsound.activeSelf);
            }
            else
            {
                Debug.LogWarning("objectToToggle is not assigned in the inspector.");
            }
        }

    }
}
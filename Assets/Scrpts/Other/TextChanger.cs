using UnityEngine;
using TMPro; // Upewnij siê, ¿e dodajesz ten namespace

public class TextChanger : MonoBehaviour
{
    public TextMeshProUGUI textComponent; 
    public TextMeshProUGUI textComponent2; 

    [TextArea(3, 10)] 
    public string newText; 
    [TextArea(3, 10)] 
    public string newText2;

    //private void Start()
    //{
    //    //if (newText2 == null) newText2 = "";
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Zmieñ tekst
            textComponent.text = newText;
            textComponent2.text = newText2;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textComponent.text = "";
            textComponent2.text = "";
        }
    }
}
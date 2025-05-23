using UnityEngine;
using TMPro; // Upewnij si�, �e dodajesz ten namespace

public class TextChanger : MonoBehaviour
{
    public TextMeshProUGUI textComponent; 
    public TextMeshProUGUI textComponent2;
    public AudioSource notify;

    [TextArea(3, 10)] 
    public string newText; 
    [TextArea(3, 10)] 
    public string newText2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (notify != null)
            notify.Play(); //dzwiek wzkazowki
            textComponent.text = newText;
            textComponent2.text = newText2;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (notify != null)
            notify.Stop();//zatrzymanie dzwieku
            textComponent.text = "";
            textComponent2.text = "";
        }
    }
}
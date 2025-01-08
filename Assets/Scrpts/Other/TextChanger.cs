using UnityEngine;
using TMPro; // Upewnij si�, �e dodajesz ten namespace

public class TextChanger : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Przypisz komponent TextMeshProUGUI w inspektorze

    [TextArea(3, 10)] // Umo�liwia wprowadzenie tekstu z nowymi liniami
    public string newText; // Tekst, na kt�ry chcesz zmieni�

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawd�, czy obiekt, kt�ry wszed� w collider, ma tag "Player" (lub inny, kt�ry chcesz)
        if (other.CompareTag("Player"))
        {
            // Zmie� tekst
            textComponent.text = newText;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Opcjonalnie: Przywr�� oryginalny tekst, gdy obiekt opu�ci collider
        if (other.CompareTag("Player"))
        {
            textComponent.text = "";
        }
    }
}
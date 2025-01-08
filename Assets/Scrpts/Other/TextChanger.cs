using UnityEngine;
using TMPro; // Upewnij siê, ¿e dodajesz ten namespace

public class TextChanger : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Przypisz komponent TextMeshProUGUI w inspektorze

    [TextArea(3, 10)] // Umo¿liwia wprowadzenie tekstu z nowymi liniami
    public string newText; // Tekst, na który chcesz zmieniæ

    private void OnTriggerEnter2D(Collider2D other)
    {
        // SprawdŸ, czy obiekt, który wszed³ w collider, ma tag "Player" (lub inny, który chcesz)
        if (other.CompareTag("Player"))
        {
            // Zmieñ tekst
            textComponent.text = newText;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Opcjonalnie: Przywróæ oryginalny tekst, gdy obiekt opuœci collider
        if (other.CompareTag("Player"))
        {
            textComponent.text = "";
        }
    }
}
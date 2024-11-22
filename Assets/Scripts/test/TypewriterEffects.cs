using TMPro;
using UnityEngine;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent; // Referencia al componente TextMeshPro
    [SerializeField] private float typingSpeed = 0.05f; // Velocidad de escritura
    private string fullText; // Texto completo

    void Start()
    {
        fullText = textComponent.text; // Captura el texto completo
        textComponent.text = ""; // Limpia el texto
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in fullText.ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Espera entre cada letra
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogTypewriterController : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public List<string> sentences;
    private int sentenceIndex = 0;
    private bool isTyping = false;
    private bool isDialogFinished = false;

    [SerializeField] private float typingSpeed = 0.05f; // Velocidad de escritura normal
    [SerializeField] private float fastTypingSpeed = 0.01f; // Velocidad de escritura r�pida
    private float currentTypingSpeed; // Velocidad actual de escritura

    void Start()
    {
        dialogText.text = "";
        currentTypingSpeed = typingSpeed; // Inicializa la velocidad actual
    }

    public void StartDialog()
    {
        sentenceIndex = 0;
        isDialogFinished = false;
        StartCoroutine(TypeSentence(sentences[sentenceIndex]));
    }

    public void DisplayNextSentence()
    {
        if (isTyping) return; // No avanzar hasta que la oraci�n actual termine de escribirse

        sentenceIndex++;

        if (sentenceIndex < sentences.Count)
        {
            dialogText.text += "\n";
            StartCoroutine(TypeSentence(sentences[sentenceIndex]));
        }
        else
        {
            isDialogFinished = true; // Marcar el di�logo como terminado
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(currentTypingSpeed); // Usa la velocidad actual
        }
        isTyping = false;
    }

    void Update()
    {
        // Cambia la velocidad de escritura al mantener presionada la tecla espacio
        if (Input.GetKey(KeyCode.Space))
        {
            currentTypingSpeed = fastTypingSpeed; // Velocidad r�pida
        }
        else
        {
            currentTypingSpeed = typingSpeed; // Velocidad normal
        }
    }

    // Funci�n para verificar si el di�logo ha terminado
    public bool IsDialogFinished()
    {
        return isDialogFinished;
    }
}
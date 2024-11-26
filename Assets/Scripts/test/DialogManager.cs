using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private List<DialogTypewriterController> dialogBoxes; // Lista de cuadros de texto (dialog boxes)
    private int currentDialogIndex = 0; // Índice del cuadro de texto actual
    private bool dialogActive = true; // Estado del diálogo (si está activo o no)

    void Start()
    {
        // Activamos el primer cuadro de texto al inicio y desactivamos el resto
        for (int i = 0; i < dialogBoxes.Count; i++)
        {
            dialogBoxes[i].gameObject.SetActive(i == 0); // Solo activamos el primero
        }

        // Iniciamos el diálogo del primer cuadro
        if (dialogBoxes.Count > 0)
        {
            dialogBoxes[0].StartDialog();
        }
    }

    void Update()
    {
        // Verificar si se presiona espacio y el cuadro actual ha terminado de mostrar el diálogo
        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            // Preguntamos si el cuadro de texto actual ha terminado todas sus oraciones
            if (dialogBoxes[currentDialogIndex].IsDialogFinished())
            {
                // Avanzamos al siguiente cuadro de texto si ya terminó el actual
                ActivateNextDialogBox();
            }
            else
            {
                // Si no ha terminado, mostramos la siguiente oración del cuadro actual
                dialogBoxes[currentDialogIndex].DisplayNextSentence();
            }
        }
    }

    // Activa el siguiente cuadro de diálogo en la lista
    private void ActivateNextDialogBox()
    {
        // Desactivamos el cuadro actual
        dialogBoxes[currentDialogIndex].gameObject.SetActive(false);

        // Avanzamos al siguiente cuadro de texto
        currentDialogIndex++;

        // Si aún hay más cuadros de texto en la lista, los activamos
        if (currentDialogIndex < dialogBoxes.Count)
        {
            dialogBoxes[currentDialogIndex].gameObject.SetActive(true);
            dialogBoxes[currentDialogIndex].StartDialog();
        }
        else
        {
            Debug.Log("Todos los cuadros de texto han terminado.");
            dialogActive = false;
            gameObject.SetActive(false); // Desactiva el objeto del DialogManager
        }
    }
}

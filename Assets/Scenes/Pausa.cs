using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour
{
    public GameObject pauseMenu; // Cambi� el nombre de la variable para mayor claridad
    private bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        // Detectar la tecla Escape para pausar y despausar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Despausa(); // Llama a la funci�n de despausa si ya est� en pausa
            }
            else
            {
                Pausa1(); // Llama a la funci�n de pausa si no est� en pausa
            }
        }
    }

    public void Pausa1()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0; // Pausar el tiempo del juego
        }
    }

    public void Despausa()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1; // Reanudar el tiempo del juego
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject winPanel; // Panel de victoria a mostrar
    private Miel sistemaDeMiel; // Referencia al script Miel
    private bool gameWon = false;

    void Start()
    {
        // Buscar el script de Miel en la escena
        sistemaDeMiel = FindObjectOfType<Miel>();

        // Asegurarse de que el panel de victoria esté inactivo al inicio
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si se ha alcanzado el máximo de Miel y si aún no se ha ganado el juego
        if (sistemaDeMiel != null && sistemaDeMiel.currentMiel >= sistemaDeMiel.maxMiel && !gameWon)
        {
            GanarJuego();
        }
    }

    // Método para gestionar la victoria
    private void GanarJuego()
    {
        gameWon = true;

        // Activar el panel de victoria
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        // Pausar el tiempo del juego
        Time.timeScale = 0f;

        Debug.Log("¡Has ganado!");
    }
}

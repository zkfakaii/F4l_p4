using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private GameObject winPanel; // Panel de victoria
    [SerializeField] private Image whiteScreen;  // Imagen blanca para la transici�n
    [SerializeField] private float transitionDuration = 1f; // Duraci�n de la transici�n
    private Miel sistemaDeMiel; // Referencia al script Miel
    private bool gameWon = false;

    private void Start()
    {
        // Buscar el script de Miel en la escena
        sistemaDeMiel = FindObjectOfType<Miel>();

        // Asegurarse de que el panel de victoria est� inactivo al inicio
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        // Asegurarse de que la pantalla blanca est� completamente transparente
        if (whiteScreen != null)
        {
            whiteScreen.color = new Color(1, 1, 1, 0);
            whiteScreen.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Verificar si se ha alcanzado el m�ximo de Miel y si a�n no se ha ganado el juego
        if (sistemaDeMiel != null && sistemaDeMiel.currentMiel >= sistemaDeMiel.maxMiel && !gameWon)
        {
            GanarJuego();
        }
    }

    private void GanarJuego()
    {
        gameWon = true;

        // Iniciar la transici�n de fundido a blanco y mostrar el panel de victoria
        StartCoroutine(FadeToWinPanel());
    }

    private IEnumerator FadeToWinPanel()
    {
        // Activar la pantalla blanca
        if (whiteScreen != null)
        {
            whiteScreen.gameObject.SetActive(true);
        }

        // Gradualmente aumentar la opacidad de la pantalla blanca
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / transitionDuration);
            if (whiteScreen != null)
            {
                whiteScreen.color = new Color(1, 1, 1, alpha);
            }
            yield return null;
        }

        // Activar el panel de victoria
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        // Detener el tiempo del juego
        Time.timeScale = 0f;
        Debug.Log("�Has ganado!");
    }
}

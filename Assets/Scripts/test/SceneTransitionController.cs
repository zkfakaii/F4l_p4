using System.Collections; // Espacio de nombres necesario para IEnumerator
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionController : MonoBehaviour
{
    [SerializeField] private GameObject panelToActivate; // Panel a activar (Game Over o similar)
    [SerializeField] private Image blackScreen; // Imagen negra para la transición
    [SerializeField] private float transitionDuration = 1f; // Duración de la transición en segundos

    private bool isTransitioning = false; // Para evitar múltiples transiciones al mismo tiempo

    private void Start()
    {
        Debug.Log("SceneTransitionController iniciado.");

        // Asegurarse de que la pantalla negra comience completamente transparente
        if (blackScreen != null)
        {
            blackScreen.color = new Color(0, 0, 0, 0);
            blackScreen.gameObject.SetActive(false); // Desactivar hasta que se necesite
            Debug.Log("Pantalla negra inicializada como transparente.");
        }
        else
        {
            Debug.LogWarning("BlackScreen no está asignado en el inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter activado por objeto con tag: {other.tag}");

        if (isTransitioning)
        {
            Debug.LogWarning("Ya se está ejecutando una transición. Ignorando nueva colisión.");
            return;
        }

        if (other.tag == "Enemy")
        {
            Debug.Log("Iniciando transición por colisión con un enemigo.");
            StartCoroutine(HandleSceneTransition());
        }
        else
        {
            Debug.Log("Objeto no válido para activar la transición.");
        }
    }

    private IEnumerator HandleSceneTransition()
    {
        isTransitioning = true;

        // Activar la pantalla negra
        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true);
            Debug.Log("Pantalla negra activada.");
        }

        // Gradualmente aumentar la opacidad de la pantalla negra
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / transitionDuration);
            if (blackScreen != null)
            {
                blackScreen.color = new Color(0, 0, 0, alpha);
                Debug.Log($"Transición en progreso. Tiempo transcurrido: {elapsedTime:F2}s, Alpha: {alpha:F2}");
            }
            yield return null;
        }

        // Activar el panel (por ejemplo, Game Over)
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
            Debug.Log("Panel activado.");
        }
        else
        {
            Debug.LogWarning("Panel no asignado en el inspector.");
        }
    }
}

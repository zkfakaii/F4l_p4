using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransicionXMuerte : MonoBehaviour
{
    /*[Header("Configuración de Transición")]
    [SerializeField] private string targetSceneName; // Nombre de la escena a cargar al final
    [SerializeField] private Image blackScreen; // Imagen negra para la transición
    [SerializeField] private float transitionDuration = 1f; // Duración de la transición en segundos
    [SerializeField] private float startDelay = 5f; // Tiempo de espera desde el inicio de la escena antes de que el script empiece

    private HashSet<GameObject> enemiesInTrigger = new HashSet<GameObject>(); // Lista de enemigos dentro del trigger
    private bool isTransitioning = false; // Para evitar múltiples transiciones al mismo tiempo
    private bool canTransition = false; // Bandera que indica si la transición puede comenzar
    private float sceneStartTime; // Tiempo en que la escena comienza

    private void Start()
    {
        // Registrar el tiempo de inicio de la escena
        sceneStartTime = Time.time;

        // Asegurarse de que la pantalla negra comience completamente transparente
        if (blackScreen != null)
        {
            blackScreen.color = new Color(0, 0, 0, 0); // Inicialmente invisible
            blackScreen.gameObject.SetActive(false); // Desactivamos la pantalla negra hasta que se necesite
        }

        // Comenzamos el temporizador de espera
        StartCoroutine(WaitForTransitionStart());
    }

    private IEnumerator WaitForTransitionStart()
    {
        // Esperar el tiempo configurado antes de permitir que el script empiece a funcionar
        yield return new WaitForSeconds(startDelay);

        // Habilitar la capacidad de realizar la transición
        canTransition = true;
        Debug.Log("El temporizador ha terminado, ahora se puede hacer la transición.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar si el objeto que entra en el trigger tiene el tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            enemiesInTrigger.Add(other.gameObject);
            Debug.Log($"Enemigo {other.gameObject.name} ha entrado al trigger.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detectar cuando un enemigo sale del trigger
        if (other.CompareTag("Enemy"))
        {
            enemiesInTrigger.Remove(other.gameObject);
            Debug.Log($"Enemigo {other.gameObject.name} ha salido del trigger.");
        }
    }

    private void Update()
    {
        // Si la transición está habilitada y no hay enemigos dentro del trigger, iniciar la transición
        if (canTransition && enemiesInTrigger.Count == 0 && !isTransitioning)
        {
            StartCoroutine(HandleSceneTransition());
        }
    }

    private IEnumerator HandleSceneTransition()
    {
        isTransitioning = true;

        // Activar la pantalla negra
        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true);
        }

        // Gradualmente aumentar la opacidad de la pantalla negra
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / transitionDuration);
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Cargar la nueva escena
        Debug.Log($"Cargando la escena: {targetSceneName}");
        SceneManager.LoadScene(targetSceneName);
    }*/
}

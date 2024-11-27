using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemigosHP : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // Vida m�xima de la unidad.
    private int currentHealth;                   // Vida actual de la unidad.

    [Header("Scene Transition Settings")]
    [SerializeField] private bool triggersSceneTransition = false; // �Activar� una transici�n al morir?
    [SerializeField] private string targetSceneName; // Nombre de la escena a cargar al morir.
    [SerializeField] private Image blackScreen; // Imagen para el efecto de transici�n.
    [SerializeField] private float transitionDuration = 1f; // Duraci�n de la transici�n en segundos.
    [SerializeField] private float transitionDelay = 0f; // Retraso antes de iniciar la transici�n, en segundos.

    private void Start()
    {
        // Inicializa la salud de la unidad al valor m�ximo al inicio
        currentHealth = maxHealth;

        if (blackScreen != null)
        {
            blackScreen.color = new Color(0, 0, 0, 0);
            blackScreen.gameObject.SetActive(false); // Desactivar hasta que se necesite.
        }
    }

    /// <summary>
    /// Aplica da�o a la unidad y verifica si la unidad debe morir.
    /// </summary>
    /// <param name="damage">Cantidad de da�o recibido.</param>
    public void TakeDamage(int damage)
    {
        // Aplica el da�o a la salud de la unidad
        ApplyDamage(damage);
    }

    /// <summary>
    /// Aplica el da�o directo a la unidad y verifica si debe morir.
    /// </summary>
    /// <param name="damage">Cantidad de da�o recibido.</param>
    private void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name} recibi� {damage} de da�o. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Elimina la unidad del juego.
    /// </summary>
    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");

        if (triggersSceneTransition && !string.IsNullOrEmpty(targetSceneName))
        {
            Debug.Log($"Iniciando transici�n a la escena: {targetSceneName}");
            StartCoroutine(HandleSceneTransition());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Maneja la transici�n a la escena con un efecto de fundido en negro y un retraso configurable.
    /// </summary>
    private IEnumerator HandleSceneTransition()
    {
        // Espera el retraso antes de iniciar la transici�n
        if (transitionDelay > 0f)
        {
            yield return new WaitForSeconds(transitionDelay);
        }

        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true);
            float elapsedTime = 0f;

            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / transitionDuration);
                blackScreen.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }

        SceneManager.LoadScene(targetSceneName);
    }
}

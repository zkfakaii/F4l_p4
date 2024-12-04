using System.Collections;
using UnityEngine;

public class UnitStateManager : MonoBehaviour
{
    [SerializeField] private string normalLayer = "Normal";
    [SerializeField] private string aerialLayer = "Aerial";
    [SerializeField] private string generatingLayer = "Generating";

    [SerializeField] private float stateChangeDelay = 1f; // Retraso en segundos antes de cambiar de estado.
    [SerializeField] private Animator animator; // Referencia al Animator

    private string currentLayer;   // Estado actual.
    private string previousLayer; // Estado previo.
    private bool isChangingState = false; // Indica si ya se está procesando un cambio de estado.

    private void Start()
    {
        // Inicializa el estado inicial como "Normal".
        ChangeLayerImmediately(normalLayer);
    }

    private void OnMouseOver()
    {
        // Si ya está en proceso un cambio de estado, ignorar.
        if (isChangingState) return;

        // Detectar clic izquierdo.
        if (Input.GetMouseButtonDown(0))
        {
            if (currentLayer == normalLayer)
            {
                StartCoroutine(ChangeLayerWithDelay(generatingLayer));
            }
            else if (currentLayer == generatingLayer)
            {
                StartCoroutine(ChangeLayerWithDelay(normalLayer));
            }
            else if (currentLayer == aerialLayer)
            {
                StartCoroutine(ChangeLayerWithDelay(normalLayer)); // Si está en Aerial, vuelve a Normal.
            }
        }
        // Detectar clic derecho.
        else if (Input.GetMouseButtonDown(1))
        {
            if (currentLayer == aerialLayer)
            {
                StartCoroutine(ChangeLayerWithDelay(previousLayer)); // Vuelve al estado previo.
            }
            else
            {
                StartCoroutine(ChangeLayerWithDelay(aerialLayer)); // Cambia a Aerial.
            }
        }
    }

    private IEnumerator ChangeLayerWithDelay(string layerName)
    {
        isChangingState = true; // Marcar que se está procesando un cambio de estado.

        // Esperar el tiempo configurado antes de cambiar de estado.
        yield return new WaitForSeconds(stateChangeDelay);

        ChangeLayerImmediately(layerName);

        isChangingState = false; // Permitir nuevos cambios de estado.
    }

    private void ChangeLayerImmediately(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        if (layer == -1)
        {
            Debug.LogWarning($"El layer '{layerName}' no existe. Asegúrate de configurarlo en el proyecto.");
            return;
        }

        // Guarda el layer actual como el previo.
        previousLayer = currentLayer;

        // Cambia el layer del objeto y de todos sus hijos.
        gameObject.layer = layer;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = layer;
        }

        currentLayer = layerName; // Actualiza el estado actual.
        UpdateAnimatorState(); // Actualiza las animaciones
        Debug.Log($"Cambiado al estado: {currentLayer}");
    }

    /// <summary>
    /// Actualiza los parámetros del Animator en función del estado actual.
    /// </summary>
    private void UpdateAnimatorState()
    {
        if (animator == null)
        {
            Debug.LogWarning("No se ha asignado un Animator al UnitStateManager.");
            return;
        }

        // Resetear todos los parámetros relacionados con los estados
        animator.SetBool("IsNormal", false);
        animator.SetBool("IsAerial", false);
        animator.SetBool("IsGenerating", false);

        // Activar el parámetro correspondiente al estado actual
        if (currentLayer == normalLayer)
        {
            animator.SetBool("IsNormal", true);
        }
        else if (currentLayer == aerialLayer)
        {
            animator.SetBool("IsAerial", true);
        }
        else if (currentLayer == generatingLayer)
        {
            animator.SetBool("IsGenerating", true);
        }
    }
}

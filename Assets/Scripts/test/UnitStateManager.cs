using System.Collections;
using UnityEngine;

public class UnitStateManager : MonoBehaviour
{
    [SerializeField] private string normalLayer = "Normal";
    [SerializeField] private string aerialLayer = "Aerial";
    [SerializeField] private string generatingLayer = "Generating";

    [SerializeField] private float stateChangeDelay = 1f; // Retraso en segundos antes de cambiar de estado.

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

    /// <summary>
    /// Cambia el estado después de un retraso.
    /// </summary>
    /// <param name="layerName">El nombre del layer al que cambiar.</param>
    private IEnumerator ChangeLayerWithDelay(string layerName)
    {
        isChangingState = true; // Marcar que se está procesando un cambio de estado.

        // Esperar el tiempo configurado antes de cambiar de estado.
        yield return new WaitForSeconds(stateChangeDelay);

        ChangeLayerImmediately(layerName);

        isChangingState = false; // Permitir nuevos cambios de estado.
    }

    /// <summary>
    /// Cambia el layer del objeto inmediatamente.
    /// </summary>
    /// <param name="layerName">El nombre del layer al que cambiar.</param>
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
        Debug.Log($"Cambiado al estado: {currentLayer}");
    }
}

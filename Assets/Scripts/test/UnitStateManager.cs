using System.Collections;
using UnityEngine;
public class UnitStateManager : MonoBehaviour
{
    [SerializeField] private string normalLayer = "Normal";
    [SerializeField] private string aerialLayer = "Aerial";
    [SerializeField] private string generatingLayer = "Generating";
    [SerializeField] private float stateChangeDelay = 1f;
    [SerializeField] private Animator animator;

    private string currentLayer;
    private string previousLayer;
    private bool isChangingState = false;

    // Referencia al ColocadorObjetos
     private ColocadorObjetos colocadorObjetos;

    private void Start()
    {
        ChangeLayerImmediately(normalLayer);
    }

    private void OnMouseOver()
    {
        // Si el colocador indica que la unidad no está colocada, no permitir cambios
        if (colocadorObjetos != null && !colocadorObjetos.UnidadColocada) return;

        if (isChangingState) return;

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
                StartCoroutine(ChangeLayerWithDelay(normalLayer));
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (currentLayer == aerialLayer)
            {
                StartCoroutine(ChangeLayerWithDelay(previousLayer));
            }
            else
            {
                StartCoroutine(ChangeLayerWithDelay(aerialLayer));
            }
        }
    }

    private IEnumerator ChangeLayerWithDelay(string layerName)
    {
        isChangingState = true;
        yield return new WaitForSeconds(stateChangeDelay);
        ChangeLayerImmediately(layerName);
        isChangingState = false;
    }

    private void ChangeLayerImmediately(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        if (layer == -1)
        {
            Debug.LogWarning($"El layer '{layerName}' no existe.");
            return;
        }

        previousLayer = currentLayer;
        gameObject.layer = layer;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = layer;
        }

        currentLayer = layerName;
        UpdateAnimatorState();
        Debug.Log($"Cambiado al estado: {currentLayer}");
    }

    private void UpdateAnimatorState()
    {
        if (animator == null)
        {
            Debug.LogWarning("No se ha asignado un Animator.");
            return;
        }

        animator.SetBool("IsNormal", false);
        animator.SetBool("IsAerial", false);
        animator.SetBool("IsGenerating", false);

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

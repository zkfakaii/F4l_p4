using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Vector3 movementDirection = Vector3.forward; // Direcci�n en la que se mover� el enemigo.
    [SerializeField] private float moveSpeed = 5f; // Velocidad de movimiento.

    [Header("Movement Control")]
    [SerializeField] private bool isFrozen = false; // Indica si el movimiento est� congelado.
    [SerializeField] private float slowFactor = 1f; // Factor de ralentizaci�n (1 significa velocidad normal, 0 significa detenido).

    private void Update()
    {
        // Aplica el movimiento si no est� congelado.
        if (!isFrozen)
        {
            float adjustedSpeed = moveSpeed * Mathf.Clamp(slowFactor, 0f, 1f); // Ajusta la velocidad seg�n el slowFactor.
            transform.Translate(movementDirection.normalized * adjustedSpeed * Time.deltaTime, Space.World);
        }
    }

    /// <summary>
    /// Congela el movimiento del enemigo.
    /// </summary>
    public void FreezeMovement()
    {
        isFrozen = true;
    }

    /// <summary>
    /// Descongela el movimiento del enemigo.
    /// </summary>
    public void UnfreezeMovement()
    {
        isFrozen = false;
    }

    /// <summary>
    /// Establece el factor de ralentizaci�n para el movimiento.
    /// </summary>
    /// <param name="factor">Un valor entre 0 (detenido) y 1 (velocidad normal).</param>
    public void SetSlowFactor(float factor)
    {
        slowFactor = Mathf.Clamp(factor, 0f, 1f);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja una l�nea para mostrar la direcci�n del movimiento.
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + movementDirection.normalized * 2f);
    }
}

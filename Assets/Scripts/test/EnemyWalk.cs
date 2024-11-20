using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemyWalk : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Vector3 movementDirection = Vector3.forward; // Dirección en la que se moverá el enemigo.
    [SerializeField] private float moveSpeed = 5f; // Velocidad de movimiento.

    [Header("Freeze Settings")]
    [SerializeField] private float defaultFreezeDuration = 2f; // Duración predeterminada del congelamiento.

    [Header("Movement Control")]
    [SerializeField] private bool isFrozen = false; // Indica si el movimiento está congelado.
    [SerializeField] private float slowFactor = 1f; // Factor de ralentización (1 significa velocidad normal, 0 significa detenido).

    private Coroutine freezeCoroutine;

    private void Update()
    {
        if (!isFrozen)
        {
            float adjustedSpeed = moveSpeed * Mathf.Clamp(slowFactor, 0f, 1f);
            transform.Translate(movementDirection.normalized * adjustedSpeed * Time.deltaTime, Space.World);
        }
    }

    public void FreezeMovement(float freezeDuration = -1f)
    {
        if (freezeDuration < 0)
        {
            freezeDuration = defaultFreezeDuration; // Usa la duración del inspector si no se especifica.
        }

        isFrozen = true;

        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }
        freezeCoroutine = StartCoroutine(UnfreezeAfterDelay(freezeDuration));
    }

    public void UnfreezeMovement()
    {
        isFrozen = false;
    }

    private IEnumerator UnfreezeAfterDelay(float freezeDuration)
    {
        yield return new WaitForSeconds(freezeDuration);
        UnfreezeMovement();
        freezeCoroutine = null;
    }
}

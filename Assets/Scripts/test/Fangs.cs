using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fangs : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private Vector3 detectionBoxSize = new Vector3(5f, 5f, 5f); // Tamaño del cubo de detección.
    [SerializeField] private LayerMask enemyLayer; // Capa que representa a los enemigos.
    [SerializeField] private string enemyTag = "Enemy"; // Tag de los enemigos.

    [Header("Damage Settings")]
    [SerializeField] private int damagePerTick = 10; // Daño aplicado en cada tick.
    [SerializeField] private float tickInterval = 1f; // Intervalo de tiempo entre ticks de daño.

    private Coroutine damageRoutine; // Rutina de daño activa.

    private void Start()
    {
        // Comienza la rutina de detección y daño.
        damageRoutine = StartCoroutine(DamageEnemiesInRange());
    }

    private IEnumerator DamageEnemiesInRange()
    {
        while (true)
        {
            // Encuentra a todos los enemigos en el rango.
            Collider[] enemiesInRange = Physics.OverlapBox(transform.position, detectionBoxSize / 2, Quaternion.identity, enemyLayer);

            foreach (Collider enemy in enemiesInRange)
            {
                // Verifica si el objeto tiene el tag "Enemy" y si sus físicas son interactuables.
                if (enemy.CompareTag(enemyTag) && !Physics.GetIgnoreLayerCollision(gameObject.layer, enemy.gameObject.layer))
                {
                    EnemigosHP enemyHP = enemy.GetComponent<EnemigosHP>();
                    if (enemyHP != null)
                    {
                        enemyHP.TakeDamage(damagePerTick);
                    }
                }
            }

            // Espera el intervalo antes de aplicar el siguiente daño.
            yield return new WaitForSeconds(tickInterval);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el área de detección en la escena.
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, detectionBoxSize);
    }
}

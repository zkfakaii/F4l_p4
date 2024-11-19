using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piumbi : MonoBehaviour
{
    [Header("Charge Settings")]
    [SerializeField] private float chargeTime = 5f; // Tiempo necesario para cargarse.
    private bool isCharged = false; // Indica si la unidad está cargada.

    [Header("Explosion Settings")]
    [SerializeField] private Vector3 explosionArea = new Vector3(5f, 5f, 5f); // Área de la explosión.
    [SerializeField] private int explosionDamage = 50; // Daño de la explosión.
    [SerializeField] private LayerMask enemyLayer; // Capas de los enemigos.
    [SerializeField] private string enemyTag = "Enemy"; // Tag de los enemigos.

    private List<GameObject> enemiesInTrigger = new List<GameObject>(); // Lista de enemigos dentro del trigger.

    private Coroutine chargeRoutine;

    private void Start()
    {
        // Comienza la rutina de carga al aparecer en escena.
        chargeRoutine = StartCoroutine(ChargeCoroutine());
    }

    private IEnumerator ChargeCoroutine()
    {
        Debug.Log($"{gameObject.name} está cargándose...");
        yield return new WaitForSeconds(chargeTime);
        isCharged = true;
        Debug.Log($"{gameObject.name} está CARGADO y listo para explotar.");

        // Verifica si hay enemigos dentro del trigger al completar la carga.
        if (enemiesInTrigger.Count > 0)
        {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto tiene el tag correcto y puede interactuar con las físicas.
        if (other.CompareTag(enemyTag) && !Physics.GetIgnoreLayerCollision(gameObject.layer, other.gameObject.layer))
        {
            // Agrega el enemigo a la lista si no está ya incluido.
            if (!enemiesInTrigger.Contains(other.gameObject))
            {
                enemiesInTrigger.Add(other.gameObject);
            }

            // Si ya está cargado, explota inmediatamente.
            if (isCharged)
            {
                Explode();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Remueve el enemigo de la lista al salir del trigger.
        if (enemiesInTrigger.Contains(other.gameObject))
        {
            enemiesInTrigger.Remove(other.gameObject);
        }
    }

    private void Explode()
    {
        Debug.Log($"{gameObject.name} explota, causando daño en el área.");

        // Encuentra todos los enemigos dentro del área de explosión.
        Collider[] enemiesInRange = Physics.OverlapBox(transform.position, explosionArea / 2, Quaternion.identity, enemyLayer);

        foreach (Collider enemy in enemiesInRange)
        {
            // Verifica si el enemigo tiene el componente de vida y aplica daño.
            EnemigosHP enemyHP = enemy.GetComponent<EnemigosHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(explosionDamage);
            }
        }

        // Destruye el objeto después de la explosión.
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el área de detección de la explosión.
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, explosionArea);

        // Cambia el color si está cargado.
        if (isCharged)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, explosionArea);
        }
    }
}

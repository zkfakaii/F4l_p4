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

    [Header("Animator Settings")]
    [SerializeField] private Animator animator; // Referencia al Animator

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

        // Activar animación de carga
        if (animator != null)
        {
            animator.SetBool("IsCharging", true);
        }

        yield return new WaitForSeconds(chargeTime);

        isCharged = true;

        Debug.Log($"{gameObject.name} está CARGADO y listo para explotar.");

        // Cambiar a animación cargada
        if (animator != null)
        {
            animator.SetBool("IsCharging", false);
            animator.SetBool("IsCharged", true);
        }

        // Verifica si hay enemigos dentro del trigger al completar la carga.
        if (enemiesInTrigger.Count > 0)
        {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag) && !Physics.GetIgnoreLayerCollision(gameObject.layer, other.gameObject.layer))
        {
            if (!enemiesInTrigger.Contains(other.gameObject))
            {
                enemiesInTrigger.Add(other.gameObject);
            }

            if (isCharged)
            {
                Explode();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemiesInTrigger.Contains(other.gameObject))
        {
            enemiesInTrigger.Remove(other.gameObject);
        }
    }

    private void Explode()
    {
        Debug.Log($"{gameObject.name} explota, causando daño en el área.");

        Collider[] enemiesInRange = Physics.OverlapBox(transform.position, explosionArea / 2, Quaternion.identity, enemyLayer);

        foreach (Collider enemy in enemiesInRange)
        {
            EnemigosHP enemyHP = enemy.GetComponent<EnemigosHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(explosionDamage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // Resetear animaciones al destruir el objeto
        if (animator != null)
        {
            animator.SetBool("IsCharging", false);
            animator.SetBool("IsCharged", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, explosionArea);

        if (isCharged)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, explosionArea);
        }
    }
}

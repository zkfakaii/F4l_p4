using UnityEngine;

public class Lanzador : MonoBehaviour
{
    [Header("Configuración del Lanzador")]
    [SerializeField] private GameObject normalProjectilePrefab;  // Prefab del proyectil en modo Normal
    [SerializeField] private GameObject aerialProjectilePrefab;  // Prefab del proyectil en modo Aerial
    [SerializeField] private float fireRate = 1f;  // Frecuencia de disparo
    private float nextFireTime = 0f;  // Tiempo de disparo siguiente

    [Header("Dirección de Disparo")]
    [SerializeField] private Vector3 fireDirection = Vector3.right;  // Dirección en la que dispara (ejemplo: Vector3.right = hacia la derecha)

    [Header("Daño del Proyectil")]
    [SerializeField] private int projectileDamage = 10;  // Daño fijo del proyectil

    [Header("Rango de Detección")]
    [SerializeField] private Vector3 detectionCenter = Vector3.zero;  // Centro del rango de detección (relativo al objeto)
    [SerializeField] private Vector3 detectionSize = new Vector3(5f, 5f, 5f);  // Tamaño del rango de detección

    private void Update()
    {
        // Verificar que el objeto está en un layer "Normal" o "Aerial"
        if (gameObject.layer == LayerMask.NameToLayer("Normal") || gameObject.layer == LayerMask.NameToLayer("Aerial"))
        {
            // Detectar si hay algún enemigo dentro del rango
            if (IsEnemyInRange() && Time.time >= nextFireTime)
            {
                FireProjectile();
                nextFireTime = Time.time + 1f / fireRate;  // Actualiza el tiempo para el siguiente disparo
            }
        }
    }

    private bool IsEnemyInRange()
    {
        // Obtiene todos los colliders en el rango de detección
        Collider[] hitColliders = Physics.OverlapBox(transform.position + detectionCenter, detectionSize / 2, Quaternion.identity);

        // Busca un objeto con el tag "Enemy"
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                return true;  // Si encuentra un enemigo, devuelve true
            }
        }

        return false;  // Si no encuentra enemigos, devuelve false
    }

    private void FireProjectile()
    {
        // Determinar el prefab del proyectil según el layer actual
        GameObject projectilePrefabToUse = (gameObject.layer == LayerMask.NameToLayer("Normal")) ? normalProjectilePrefab : aerialProjectilePrefab;

        // Instancia el proyectil
        GameObject projectile = Instantiate(projectilePrefabToUse, transform.position, Quaternion.identity);

        // Obtiene el componente Proyectil del objeto instanciado
        Proyectil proyectilScript = projectile.GetComponent<Proyectil>();

        // Asigna la dirección del disparo
        proyectilScript.SetFireDirection(fireDirection);

        // Asigna el valor fijo de daño al proyectil
        proyectilScript.SetDamage(projectileDamage);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja un gizmo para visualizar el rango de detección en la escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + detectionCenter, detectionSize);
    }
}

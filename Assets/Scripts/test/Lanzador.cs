using UnityEngine;

public class Lanzador : MonoBehaviour
{
    [Header("Configuraci�n del Lanzador")]
    [SerializeField] private GameObject normalProjectilePrefab;  // Prefab del proyectil en modo Normal
    [SerializeField] private GameObject aerialProjectilePrefab;  // Prefab del proyectil en modo Aerial
    [SerializeField] private float fireRate = 1f;  // Frecuencia de disparo
    private float nextFireTime = 0f;  // Tiempo de disparo siguiente

    [Header("Direcci�n de Disparo")]
    [SerializeField] private Vector3 fireDirection = Vector3.right;  // Direcci�n en la que dispara (ejemplo: Vector3.right = hacia la derecha)

    [Header("Da�o del Proyectil")]
    [SerializeField] private int projectileDamage = 10;  // Da�o fijo del proyectil

    [Header("Rango de Detecci�n")]
    [SerializeField] private Vector3 detectionCenter = Vector3.zero;  // Centro del rango de detecci�n (relativo al objeto)
    [SerializeField] private Vector3 detectionSize = new Vector3(5f, 5f, 5f);  // Tama�o del rango de detecci�n

    private void Update()
    {
        // Verificar que el objeto est� en un layer "Normal" o "Aerial"
        if (gameObject.layer == LayerMask.NameToLayer("Normal") || gameObject.layer == LayerMask.NameToLayer("Aerial"))
        {
            // Detectar si hay alg�n enemigo dentro del rango
            if (IsEnemyInRange() && Time.time >= nextFireTime)
            {
                FireProjectile();
                nextFireTime = Time.time + 1f / fireRate;  // Actualiza el tiempo para el siguiente disparo
            }
        }
    }

    private bool IsEnemyInRange()
    {
        // Obtiene todos los colliders en el rango de detecci�n
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
        // Determinar el prefab del proyectil seg�n el layer actual
        GameObject projectilePrefabToUse = (gameObject.layer == LayerMask.NameToLayer("Normal")) ? normalProjectilePrefab : aerialProjectilePrefab;

        // Instancia el proyectil
        GameObject projectile = Instantiate(projectilePrefabToUse, transform.position, Quaternion.identity);

        // Obtiene el componente Proyectil del objeto instanciado
        Proyectil proyectilScript = projectile.GetComponent<Proyectil>();

        // Asigna la direcci�n del disparo
        proyectilScript.SetFireDirection(fireDirection);

        // Asigna el valor fijo de da�o al proyectil
        proyectilScript.SetDamage(projectileDamage);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja un gizmo para visualizar el rango de detecci�n en la escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + detectionCenter, detectionSize);
    }
}

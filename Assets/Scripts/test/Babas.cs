using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Babas : MonoBehaviour
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

    [Header("Congelamiento")]
    [SerializeField] private float freezeDuration = 2f;  // Duración del congelamiento
    [SerializeField] [Range(0f, 1f)] private float freezeProbability = 0.2f;  // Probabilidad de congelar al enemigo al ser alcanzado

    private void Update()
    {
        // Verificar que el objeto está en un layer "Normal" o "Aerial"
        if (gameObject.layer == LayerMask.NameToLayer("Normal") || gameObject.layer == LayerMask.NameToLayer("Aerial"))
        {
            // Disparar continuamente si ha pasado el tiempo de espera
            if (Time.time >= nextFireTime)
            {
                FireProjectile();
                nextFireTime = Time.time + 1f / fireRate;  // Actualiza el tiempo para el siguiente disparo
            }
        }
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
        // Asigna la probabilidad de congelar al proyectil
        proyectilScript.SetFreezeSettings(freezeProbability, freezeDuration);
    }
}

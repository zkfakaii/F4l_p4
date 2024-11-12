using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : MonoBehaviour
{
    public GameObject normalProjectilePrefab; // Prefab del proyectil en modo Normal
    public GameObject aerialProjectilePrefab; // Prefab del proyectil en modo Aéreo
    public Transform shootPoint;             // Punto desde donde se dispara el proyectil
    public float shootInterval = 1.5f;       // Intervalo de disparo en segundos

    private float shootTimer;
    private HeightManager heightManager;     // Referencia al script HeightManager

    void Start()
    {
        // Buscar el script HeightManager en la escena
        heightManager = FindObjectOfType<HeightManager>();
    }

    void Update()
    {
        // Incrementar el temporizador
        shootTimer += Time.deltaTime;

        // Disparar un proyectil si ha pasado el tiempo de intervalo
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void Shoot()
    {
        // Determinar el prefab a disparar según el estado actual
        GameObject projectileToShoot = (heightManager != null && heightManager.IsAerial) ? aerialProjectilePrefab : normalProjectilePrefab;

        // Instanciar el proyectil en el punto de disparo y con su rotación
        Instantiate(projectileToShoot, shootPoint.position, shootPoint.rotation);
    }
}

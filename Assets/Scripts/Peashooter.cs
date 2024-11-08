using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform shootPoint;        // Punto desde donde se dispara el proyectil
    public float shootInterval = 1.5f;  // Intervalo de disparo en segundos

    private float shootTimer;

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
        // Instanciar el proyectil en el punto de disparo y con su rotación
        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
    }
}

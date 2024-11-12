using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : MonoBehaviour
{
    public GameObject normalProjectilePrefab; // Prefab del proyectil en modo Normal
    public GameObject aerialProjectilePrefab; // Prefab del proyectil en modo Aéreo
    public Transform shootPoint;              // Punto desde donde se dispara el proyectil
    public float shootInterval = 1.5f;        // Intervalo de disparo en segundos

    private float shootTimer;
    private HeightManager heightManager;      // Referencia al script HeightManager

    void Start()
    {
        // Obtener referencia a HeightManager
        heightManager = FindObjectOfType<HeightManager>();
        if (heightManager == null)
        {
            Debug.LogError("HeightManager no encontrado en la escena.");
        }
    }

    void Update()
    {
        // Incrementar el temporizador
        shootTimer += Time.deltaTime;

        // Disparar un proyectil si ha pasado el tiempo de intervalo
        if (shootTimer >= shootInterval)
        {
            ShootBasedOnMode();
            shootTimer = 0f;
        }
    }

    private void ShootBasedOnMode()
    {
        // Verificar el estado actual y disparar el proyectil correspondiente
        if (heightManager != null)
        {
            if (heightManager.IsAerial)  // Suponiendo que IsAerial es una propiedad booleana en HeightManager
            {
                Debug.Log("Disparando proyectil en modo Aéreo");
                Instantiate(aerialProjectilePrefab, shootPoint.position, shootPoint.rotation);
            }
            else
            {
                Debug.Log("Disparando proyectil en modo Normal");
                Instantiate(normalProjectilePrefab, shootPoint.position, shootPoint.rotation);
            }
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class mySpawnerType
{
    public bool first;
    public bool second;
    public bool third;
}

public class EnemySpawner : MonoBehaviour
{
    public mySpawnerType myType;
    public GameObject enemyPrefab;           // Prefab del enemigo
    public Transform[] spawnLineStarts;      // Puntos de inicio para spawn
    public Transform[] spawnLineEnds;        // Puntos finales para spawn

    public bool IsWaveActive { get; private set; }  // Si la ola está activa
    private int enemiesSpawnedInWave = 0;             // Contador de enemigos generados en la ola actual
    private List<SpawnEvent> spawnEvents = new List<SpawnEvent>(); // Lista de eventos de spawn

    private float waveTimer = 0f;            // Temporizador de la ola
    private int currentEventIndex = 0;       // Índice del evento que está sucediendo

    public float minEnemySpeed = 1f;         // Velocidad mínima de los enemigos
    public float maxEnemySpeed = 3f;         // Velocidad máxima de los enemigos

    void Update()
    {
        if (IsWaveActive)
        {
            // Aumentar el temporizador de la ola
            waveTimer += Time.deltaTime;

            // Verificar si hay eventos que se deben activar en este momento
            if (currentEventIndex < spawnEvents.Count)
            {
                SpawnEvent currentEvent = spawnEvents[currentEventIndex];

                // Si es el momento del evento, disparar el spawn
                if (waveTimer >= currentEvent.spawnTime)
                {
                    TriggerSpawnEvent(currentEvent);
                    currentEventIndex++;
                }
            }
        }
    }

    // Iniciar la ola
    public void StartWave()
    {
        enemiesSpawnedInWave = 0;
        IsWaveActive = true;
        waveTimer = 0f;
        currentEventIndex = 0;
        Debug.Log("¡Ola iniciada!");
    }

    // Detener la ola
    public void EndWave()
    {
        IsWaveActive = false;
        Debug.Log("¡Ola finalizada!");
    }

    // Establecer la lista de eventos de spawn
    public void SetSpawnEvents(List<SpawnEvent> events)
    {
        spawnEvents = events;
    }

    // Generar enemigos en un evento de spawn
    private void TriggerSpawnEvent(SpawnEvent spawnEvent)
    {
        for (int i = 0; i < spawnEvent.enemyCount; i++)
        {
            if((spawnEvent.spawnType.first == myType.first && myType.first == true) ||
               (spawnEvent.spawnType.second == myType.second && myType.second == true) ||
               (spawnEvent.spawnType.third == myType.third && myType.third == true) 
                ) { 
                SpawnEnemy();
            }
        }
    }

    // Función para generar un enemigo
    private void SpawnEnemy()
    {
        // Elegir una línea de spawn aleatoria
        int randomLineIndex = Random.Range(0, spawnLineStarts.Length);
        Transform spawnStart = spawnLineStarts[randomLineIndex];
        Transform spawnEnd = spawnLineEnds[randomLineIndex];

        // Generar el enemigo en la posición de inicio
        Vector3 spawnPosition = spawnStart.position;

        // Crear el enemigo en la posición de spawn
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            // Configurar la posición final (target) para que el enemigo se mueva hacia el final
            enemyScript.SetTargetPosition(spawnEnd.position);

            // Establecer la velocidad del enemigo
            float enemySpeed = Random.Range(minEnemySpeed, maxEnemySpeed);
            enemyScript.SetSpeed(enemySpeed);
        }
        else
        {
            Debug.LogError("El prefab del enemigo no tiene un componente 'Enemy'.");
        }

        enemiesSpawnedInWave++;
    }
}

[System.Serializable]
public class SpawnEvent
{
    public mySpawnerType spawnType;
    public float spawnTime;     // Tiempo en el que debe aparecer este evento (en segundos)
    public int enemyCount;      // Cantidad de enemigos a generar en este evento
}

using System.Collections.Generic;
using UnityEngine;

public class NingenSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName; // Nombre de la oleada (para identificar en el inspector).
        public GameObject[] prefabs; // Prefabs a generar en esta oleada.
        public int[] quantities; // Cantidad de cada prefab (debe coincidir con prefabs[]).
        public float spawnTime; // Momento en segundos en que inicia esta oleada.
    }

    [Header("Configuración de oleadas")]
    public List<Wave> waves; // Lista de oleadas configurables desde el inspector.
    public Transform[] spawnPoints; // Puntos de generación.

    [Header("Temporizador general")]
    public float levelDuration = 300f; // Duración total del nivel en segundos.
    private float levelTimer; // Temporizador interno del nivel.

    private int currentWaveIndex = 0; // Índice de la oleada actual.
    private bool spawningWave = false; // Controla si una oleada está activa.

    void Update()
    {
        // Actualizar el temporizador del nivel.
        levelTimer += Time.deltaTime;

        // Revisar si hay oleadas pendientes y si es hora de la siguiente.
        if (currentWaveIndex < waves.Count && levelTimer >= waves[currentWaveIndex].spawnTime && !spawningWave)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++; // Avanzar a la siguiente oleada.
        }
    }

    private System.Collections.IEnumerator SpawnWave(Wave wave)
    {
        spawningWave = true;

        Debug.Log($"Iniciando oleada: {wave.waveName}");

        // Iterar sobre los prefabs y cantidades configurados en la oleada.
        for (int i = 0; i < wave.prefabs.Length; i++)
        {
            for (int j = 0; j < wave.quantities[i]; j++)
            {
                // Elegir un punto de spawn aleatorio.
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Generar el prefab en el punto de spawn.
                Instantiate(wave.prefabs[i], spawnPoint.position, spawnPoint.rotation);
                Debug.Log("sisi");

                // Pausa opcional entre spawns individuales (ajustable).
                yield return new WaitForSeconds(0.2f);
            }
        }

        Debug.Log($"Oleada {wave.waveName} completada.");
        spawningWave = false;
    }
}

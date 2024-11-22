using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NingenHoneySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;         // Nombre de la oleada.
        public GameObject[] prefabs;    // Prefabs a generar en esta oleada.
        public int[] quantities;        // Cantidades de cada prefab.
        public int requiredHoney;       // Cantidad de miel necesaria para activar esta oleada.
    }

    [Header("Configuración de oleadas")]
    public List<Wave> waves;            // Lista de oleadas configurables desde el inspector.
    public Transform[] spawnPoints;     // Puntos de generación.

    [Header("Referencia al sistema de miel")]
    public Miel mielSystem;             // Referencia al script de Miel.

    private int currentWaveIndex = 0;   // Índice de la oleada actual.
    private HashSet<int> triggeredWaves = new HashSet<int>(); // Oleadas ya activadas.

    void Start()
    {
        // Validar si los puntos de spawn están asignados.
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No se han asignado puntos de generación en el inspector.");
        }

        // Validar si el sistema de miel está asignado.
        if (mielSystem == null)
        {
            Debug.LogError("No se ha asignado el sistema de Miel.");
        }

        // Validar configuración de oleadas.
        foreach (var wave in waves)
        {
            if (wave.prefabs.Length != wave.quantities.Length)
            {
                Debug.LogError($"La oleada '{wave.waveName}' tiene una configuración incorrecta: " +
                               "la cantidad de prefabs no coincide con las cantidades.");
            }
        }
    }

    void Update()
    {
        // Verificar si la miel alcanza el requisito para activar una oleada.
        for (int i = 0; i < waves.Count; i++)
        {
            if (!triggeredWaves.Contains(i) && mielSystem.currentMiel >= waves[i].requiredHoney)
            {
                StartCoroutine(SpawnWave(waves[i]));
                triggeredWaves.Add(i); // Registrar que esta oleada ya fue activada.
            }
        }
    }

    private System.Collections.IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log($"Iniciando oleada: {wave.waveName}");

        // Iterar sobre los prefabs y cantidades configurados en la oleada.
        for (int i = 0; i < wave.prefabs.Length; i++)
        {
            if (wave.prefabs[i] == null)
            {
                Debug.LogError($"El prefab en la posición {i} de la oleada '{wave.waveName}' es nulo.");
                continue;
            }

            for (int j = 0; j < wave.quantities[i]; j++)
            {
                // Elegir un punto de spawn aleatorio.
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Generar el prefab en el punto de spawn.
                Instantiate(wave.prefabs[i], spawnPoint.position, spawnPoint.rotation);

                Debug.Log($"Generado {wave.prefabs[i].name} en {spawnPoint.position}");

                // Pausa opcional entre spawns individuales.
                yield return new WaitForSeconds(0.2f);
            }
        }

        Debug.Log($"Oleada {wave.waveName} completada.");
    }
}

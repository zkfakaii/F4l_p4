using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner[] spawners;  // Arreglo de EnemySpawners
    public List<SpawnEvent> se;

    void Start()
    {
        StartWave();
        
    }

    public void StartWave()
    {

        // Configurar cada spawner con los eventos de spawn
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.SetSpawnEvents(se);
            spawner.StartWave();  // Iniciar la ola en cada spawner
        }

        Debug.Log("¡Iniciando nueva ola!");
    }
}

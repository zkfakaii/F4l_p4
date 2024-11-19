using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;  // Arreglo de EnemySpawners
    public int enemiesPerWave = 5;        // Enemigos por ola
    public float waveInterval = 5f;       // Intervalo de tiempo entre olas
    private float waveTimer = 0f;         // Temporizador para controlar el intervalo entre olas

    private bool isWaveActive = false;    // Indicador de si la ola está activa
    private int enemiesInCurrentWave = 0; // Enemigos generados en la ola actual

    private void Update()
    {
        // Aumentar el temporizador de la ola
        waveTimer += Time.deltaTime;

        // Si ha pasado el intervalo de tiempo de la ola y no hay ola activa, iniciar una nueva ola
        if (waveTimer >= waveInterval && !isWaveActive)
        {
            StartWave();
        }

        // Si la ola está activa, comprobamos si cada EnemySpawner debe generar enemigos
        if (isWaveActive)
        {
            foreach (EnemySpawner spawner in enemySpawners)
            {
                if (spawner != null && !spawner.IsWaveActive)
                {
                    spawner.StartWave(); // Iniciar la ola en cada EnemySpawner
                }
            }
        }
    }

    void StartWave()
    {
        // Iniciar la ola en cada EnemySpawner
        isWaveActive = true;
        enemiesInCurrentWave = enemiesPerWave;  // Ajusta la cantidad de enemigos por ola

        // Iniciar la ola en cada EnemySpawner
        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.SetEnemiesPerWave(enemiesInCurrentWave);  // Asigna la cantidad de enemigos para cada spawner
                spawner.StartWave(); // Inicia la ola en el spawner
            }
        }
        waveTimer = 0f;  // Reiniciar el temporizador de la ola
    }

    // Puedes agregar más métodos aquí para gestionar las olas (finalizar, reiniciar, etc.)
}

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Prefab del enemigo
    public Transform spawnLineStart;     // Punto inicial de la línea de generación
    public Transform spawnLineEnd;       // Punto final de la línea de generación

    public float spawnInterval = 2f;     // Intervalo de tiempo entre cada generación de enemigo
    public float waveInterval = 5f;      // Intervalo de tiempo entre olas de enemigos
    private float spawnTimer = 0f;       // Temporizador para contar el tiempo de generación de enemigos
    private float waveTimer = 0f;        // Temporizador para controlar el intervalo entre olas
    private bool isWaveActive = false;   // Indicador de si la ola está activa

    public int enemiesPerWave = 5;       // Cantidad de enemigos por ola
    private int enemiesSpawnedInWave = 0; // Contador de enemigos generados en la ola actual

    public float enemySpeed = 3f;        // Velocidad base de los enemigos
    public float waveSpeedMultiplier = 1f; // Factor que multiplica la velocidad de los enemigos en cada ola

    public float spawnSpacing = 1f;      // Espaciado entre enemigos cuando se generen a lo largo de la línea

    void Update()
    {
        // Aumentar el temporizador de la ola
        waveTimer += Time.deltaTime;

        // Si ha pasado el intervalo de tiempo de la ola, inicia la ola
        if (waveTimer >= waveInterval && !isWaveActive)
        {
            StartWave();
        }

        // Si la ola está activa, generamos enemigos con spawnTimer
        if (isWaveActive)
        {
            spawnTimer += Time.deltaTime;

            // Si ha pasado el intervalo de generación y aún no se generaron todos los enemigos de la ola
            if (spawnTimer >= spawnInterval && enemiesSpawnedInWave < enemiesPerWave)
            {
                SpawnEnemy();
                spawnTimer = 0f; // Reiniciar el temporizador de generación de enemigos
            }
        }
    }

    void StartWave()
    {
        // Reiniciar los contadores y activar la ola
        isWaveActive = true;
        enemiesSpawnedInWave = 0; // Reiniciar el contador de enemigos por ola
        Debug.Log("¡Iniciando nueva ola!");
    }

    void SpawnEnemy()
    {
        // Calcular una posición aleatoria a lo largo de la línea entre spawnLineStart y spawnLineEnd, con un espaciado
        float distanceBetween = Vector3.Distance(spawnLineStart.position, spawnLineEnd.position);
        float spawnPositionOffset = (enemiesSpawnedInWave * spawnSpacing); // Desplazamiento basado en el número de enemigos generados

        // Asegurarnos de que no nos salimos de la distancia disponible
        spawnPositionOffset = Mathf.Clamp(spawnPositionOffset, 0, distanceBetween);

        // Calcular la posición de cada enemigo a lo largo de la línea de generación
        Vector3 spawnPosition = Vector3.Lerp(spawnLineStart.position, spawnLineEnd.position, spawnPositionOffset / distanceBetween);

        // Crear el enemigo en la posición generada
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Asignar el objetivo de la línea de movimiento (fin de la línea) al enemigo
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            // Configurar la posición final de la línea para que el enemigo se mueva hacia allí
            enemyScript.SetTargetPosition(spawnLineEnd.position);

            // Configurar la velocidad del enemigo en función de la ola
            //enemyScript.SetSpeed(enemySpeed * waveSpeedMultiplier);
        }
        else
        {
            Debug.LogError("El prefab del enemigo no tiene un componente 'Enemy'.");
        }

        // Incrementar el contador de enemigos en la ola
        enemiesSpawnedInWave++;

        // Si se han generado todos los enemigos de la ola, desactivar la ola
        if (enemiesSpawnedInWave >= enemiesPerWave)
        {
            isWaveActive = false;
            waveTimer = 0f; // Reiniciar el temporizador para la siguiente ola
        }
    }

    // Método para cambiar el intervalo de generación de enemigos en tiempo de ejecución
    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;
        Debug.Log("Nuevo intervalo de generación: " + spawnInterval);
    }

    // Método para cambiar la velocidad de los enemigos en tiempo de ejecución
    public void SetEnemySpeed(float newSpeed)
    {
        enemySpeed = newSpeed;
        Debug.Log("Nueva velocidad de enemigos: " + enemySpeed);
    }

    // Método para cambiar el intervalo de las olas de enemigos en tiempo de ejecución
    public void SetWaveInterval(float newWaveInterval)
    {
        waveInterval = newWaveInterval;
        Debug.Log("Nuevo intervalo de olas: " + waveInterval);
    }

    // Método para cambiar la cantidad de enemigos por ola en tiempo de ejecución
    public void SetEnemiesPerWave(int newEnemyCount)
    {
        enemiesPerWave = newEnemyCount;
        Debug.Log("Nueva cantidad de enemigos por ola: " + enemiesPerWave);
    }

    // Método para cambiar la velocidad de las olas (modificador) en tiempo de ejecución
    public void SetWaveSpeedMultiplier(float newMultiplier)
    {
        waveSpeedMultiplier = newMultiplier;
        Debug.Log("Nuevo multiplicador de velocidad de la ola: " + waveSpeedMultiplier);
    }
}

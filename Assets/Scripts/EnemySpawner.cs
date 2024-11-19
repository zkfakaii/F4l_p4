using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;         // Prefab del enemigo
    public Transform[] spawnLineStarts;    // Arreglo de puntos de inicio para spawn
    public Transform[] spawnLineEnds;      // Arreglo de puntos finales para spawn

    public bool IsWaveActive { get; private set; } // Propiedad para saber si la ola est� activa

    private int enemiesPerWave = 5;         // Cantidad de enemigos por ola
    private int enemiesSpawnedInWave = 0;  // Contador de enemigos generados en la ola actual

    public float minSpawnInterval = 2f;    // Intervalo m�nimo entre la aparici�n de enemigos
    public float maxSpawnInterval = 4f;    // Intervalo m�ximo entre la aparici�n de enemigos
    private float nextSpawnTime;           // Controlador de tiempo entre enemigos

    public float minEnemySpeed = 1f;       // Velocidad m�nima de los enemigos
    public float maxEnemySpeed = 3f;       // Velocidad m�xima de los enemigos

    void Update()
    {
        // Si la ola est� activa, comprobamos si es el momento de generar enemigos
        if (IsWaveActive)
        {
            // Si el tiempo actual ha superado el siguiente tiempo de aparici�n
            if (Time.time >= nextSpawnTime && enemiesSpawnedInWave < enemiesPerWave)
            {
                SpawnEnemies();
                nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval); // Establecer el siguiente tiempo de aparici�n aleatorio
            }
        }
    }

    // Establecer el n�mero de enemigos por ola
    public void SetEnemiesPerWave(int newEnemyCount)
    {
        enemiesPerWave = newEnemyCount;
    }

    // Iniciar la ola
    public void StartWave()
    {
        enemiesSpawnedInWave = 0;  // Reiniciar el contador de enemigos por ola
        IsWaveActive = true;       // Activar la ola
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval); // Establecer el siguiente tiempo de aparici�n aleatorio
        Debug.Log("�Ola iniciada en: " + gameObject.name + "!");
    }

    // Detener la ola
    public void EndWave()
    {
        IsWaveActive = false;
        Debug.Log("�Ola finalizada en: " + gameObject.name + "!");
    }

    void SpawnEnemies()
    {
        // Determinar cu�ntos enemigos aparecer�n en esta ronda
        int enemiesToSpawn = Random.Range(1, 3); // 1 o 2 enemigos en esta aparici�n (puedes ajustar este rango)

        for (int i = 0; i < enemiesToSpawn && enemiesSpawnedInWave < enemiesPerWave; i++)
        {
            // Elegir una l�nea de spawn aleatoriamente
            int randomLineIndex = Random.Range(0, spawnLineStarts.Length);
            Transform spawnStart = spawnLineStarts[randomLineIndex];
            Transform spawnEnd = spawnLineEnds[randomLineIndex];

            // Generar enemigos en el punto de inicio (SpawnLineStart)
            Vector3 spawnPosition = spawnStart.position;

            // Crear el enemigo en la posici�n calculada (en SpawnLineStart)
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Asignar el objetivo de la l�nea de movimiento (fin de la l�nea) al enemigo
            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                // Configurar la posici�n final de la l�nea para que el enemigo se mueva hacia SpawnLineEnd
                enemyScript.SetTargetPosition(spawnEnd.position);

                // Ajustar la velocidad del enemigo en funci�n de los valores p�blicos
                float enemySpeed = Random.Range(minEnemySpeed, maxEnemySpeed);  // Velocidad aleatoria dentro de un rango
                enemyScript.SetSpeed(enemySpeed);  // Pasar la velocidad al enemigo
            }
            else
            {
                Debug.LogError("El prefab del enemigo no tiene un componente 'Enemy'.");
            }

            // Incrementar el contador de enemigos generados
            enemiesSpawnedInWave++;

            // Si ya hemos generado todos los enemigos, salir
            if (enemiesSpawnedInWave >= enemiesPerWave)
            {
                break;
            }
        }

        // Si se han generado todos los enemigos de la ola, desactivar la ola
        if (enemiesSpawnedInWave >= enemiesPerWave)
        {
            EndWave();
        }
    }
}

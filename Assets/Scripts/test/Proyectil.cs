

using UnityEngine;
using System.Collections;  // Agregar esta línea

public class Proyectil : MonoBehaviour
{
    [Header("Configuración del Proyectil")]
    [SerializeField] private float speed = 10f;  // Velocidad del proyectil
    private Vector3 fireDirection;  // Dirección del proyectil
    private int damage;             // Daño del proyectil

    [Header("Congelamiento")]
    private float freezeDuration;   // Duración del congelamiento
    private float freezeProbability; // Probabilidad de congelar al enemigo

    // Método para asignar la dirección del proyectil
    public void SetFireDirection(Vector3 direction)
    {
        fireDirection = direction.normalized;  // Asegura que la dirección esté normalizada
    }

    // Método para asignar el valor de daño al proyectil
    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }

    // Método para configurar la probabilidad y duración de congelamiento
    public void SetFreezeSettings(float probability, float duration)
    {
        freezeProbability = probability;
        freezeDuration = duration;
    }

    private void Update()
    {
        // Mover el proyectil en la dirección indicada
        transform.Translate(fireDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el proyectil colisiona con un enemigo
        EnemigosHP enemyHP = other.GetComponent<EnemigosHP>();
        if (enemyHP != null)
        {
            // Aplica el daño al enemigo
            enemyHP.TakeDamage(damage);

            // Verificar si el proyectil tiene una probabilidad de congelar
            if (Random.value <= freezeProbability)
            {
                EnemyWalk enemyWalk = other.GetComponent<EnemyWalk>();
                if (enemyWalk != null)
                {
                    // Congela el movimiento del enemigo
                    enemyWalk.FreezeMovement();

                    // Descongelar después de un tiempo
                    StartCoroutine(UnfreezeAfterDelay(enemyWalk));
                }
            }

            // El proyectil se destruye después de la colisión
            Destroy(gameObject);
        }
    }

    private IEnumerator UnfreezeAfterDelay(EnemyWalk enemyWalk)
    {
        // Espera el tiempo de congelamiento
        yield return new WaitForSeconds(freezeDuration);

        // Descongela el movimiento del enemigo
        enemyWalk.UnfreezeMovement();
    }
}
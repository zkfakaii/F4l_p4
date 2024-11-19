

using UnityEngine;
using System.Collections;  // Agregar esta l�nea

public class Proyectil : MonoBehaviour
{
    [Header("Configuraci�n del Proyectil")]
    [SerializeField] private float speed = 10f;  // Velocidad del proyectil
    private Vector3 fireDirection;  // Direcci�n del proyectil
    private int damage;             // Da�o del proyectil

    [Header("Congelamiento")]
    private float freezeDuration;   // Duraci�n del congelamiento
    private float freezeProbability; // Probabilidad de congelar al enemigo

    // M�todo para asignar la direcci�n del proyectil
    public void SetFireDirection(Vector3 direction)
    {
        fireDirection = direction.normalized;  // Asegura que la direcci�n est� normalizada
    }

    // M�todo para asignar el valor de da�o al proyectil
    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }

    // M�todo para configurar la probabilidad y duraci�n de congelamiento
    public void SetFreezeSettings(float probability, float duration)
    {
        freezeProbability = probability;
        freezeDuration = duration;
    }

    private void Update()
    {
        // Mover el proyectil en la direcci�n indicada
        transform.Translate(fireDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el proyectil colisiona con un enemigo
        EnemigosHP enemyHP = other.GetComponent<EnemigosHP>();
        if (enemyHP != null)
        {
            // Aplica el da�o al enemigo
            enemyHP.TakeDamage(damage);

            // Verificar si el proyectil tiene una probabilidad de congelar
            if (Random.value <= freezeProbability)
            {
                EnemyWalk enemyWalk = other.GetComponent<EnemyWalk>();
                if (enemyWalk != null)
                {
                    // Congela el movimiento del enemigo
                    enemyWalk.FreezeMovement();

                    // Descongelar despu�s de un tiempo
                    StartCoroutine(UnfreezeAfterDelay(enemyWalk));
                }
            }

            // El proyectil se destruye despu�s de la colisi�n
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
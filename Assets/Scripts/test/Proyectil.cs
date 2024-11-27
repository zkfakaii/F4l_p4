

using UnityEngine;
using System.Collections;  // Agregar esta l�nea

public class Proyectil : MonoBehaviour
{
    [Header("Configuraci�n del Proyectil")]
    [SerializeField] private float speed = 10f;         // Velocidad del proyectil
    [SerializeField] private float lifetime = 5f;      // Tiempo de vida del proyectil antes de destruirse
    private Vector3 fireDirection;                     // Direcci�n del proyectil
    private int damage;                                // Da�o del proyectil

    [Header("Congelamiento")]
    private float freezeDuration;                      // Duraci�n del congelamiento
    private float freezeProbability;                   // Probabilidad de congelar al enemigo

    private void Start()
    {
        // Destruir el proyectil autom�ticamente despu�s de 'lifetime' segundos
        Destroy(gameObject, lifetime);
    }

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
        EnemigosHP enemyHP = other.GetComponent<EnemigosHP>();
        if (enemyHP != null)
        {
            enemyHP.TakeDamage(damage);

            if (Random.value <= freezeProbability)
            {
                EnemyWalk enemyWalk = other.GetComponent<EnemyWalk>();
                if (enemyWalk != null)
                {
                    // Congela el movimiento del enemigo con la duraci�n del proyectil.
                    enemyWalk.FreezeMovement(freezeDuration);
                }
            }

            Destroy(gameObject);
        }
    }
}

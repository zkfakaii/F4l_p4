using System.Collections;
using UnityEngine;

public class EnemigosHP : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // Vida máxima de la unidad.
    private int currentHealth;                   // Vida actual de la unidad.

    private void Start()
    {
        // Inicializa la salud de la unidad al valor máximo al inicio
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Aplica daño a la unidad y verifica si la unidad debe morir.
    /// </summary>
    /// <param name="damage">Cantidad de daño recibido.</param>
    public void TakeDamage(int damage)
    {
        // Aplica el daño a la salud de la unidad
        ApplyDamage(damage);
    }

    /// <summary>
    /// Aplica el daño directo a la unidad y verifica si debe morir.
    /// </summary>
    /// <param name="damage">Cantidad de daño recibido.</param>
    private void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name} recibió {damage} de daño. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Elimina la unidad del juego.
    /// </summary>
    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        Destroy(gameObject);
    }
}

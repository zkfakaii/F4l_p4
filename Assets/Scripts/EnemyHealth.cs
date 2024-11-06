using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // Salud máxima del enemigo
    private int currentHealth;

    void Start()
    {
        // Establecer la salud actual igual a la salud máxima al inicio
        currentHealth = maxHealth;
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo recibió daño, salud actual: " + currentHealth);

        // Si la salud llega a cero o menos, destruir al enemigo
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para manejar la muerte del enemigo
    private void Die()
    {
        Debug.Log("Enemigo murió");
        Destroy(gameObject);
    }
}

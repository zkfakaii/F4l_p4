using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // Salud m�xima del enemigo
    private int currentHealth;

    void Start()
    {
        // Establecer la salud actual igual a la salud m�xima al inicio
        currentHealth = maxHealth;
    }

    // M�todo para recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo recibi� da�o, salud actual: " + currentHealth);

        // Si la salud llega a cero o menos, destruir al enemigo
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para manejar la muerte del enemigo
    private void Die()
    {
        Debug.Log("Enemigo muri�");
        Destroy(gameObject);
    }
}

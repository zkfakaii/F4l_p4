using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;

    void Update()
    {
        // Movimiento constante en la dirección en la que fue instanciado
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Detectar colisión con enemigos
        if (collision.CompareTag("Enemy"))
        {
            // Reducir la vida del enemigo
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Destruir el proyectil al colisionar
            Destroy(gameObject);
        }
    }
}


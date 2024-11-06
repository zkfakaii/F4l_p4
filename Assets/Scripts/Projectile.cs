using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;

    void Update()
    {
        // Movimiento constante hacia la derecha
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detectar colisión con enemigos
        if (collision.CompareTag("Enemy"))
        {
            // Aquí podrías reducir la vida del enemigo
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

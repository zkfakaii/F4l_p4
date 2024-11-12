using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum AttackType { Normal, Aerial, Area }  // Definici�n de los tipos de ataque
    public AttackType attackType = AttackType.Normal; // Tipo de ataque, ajustable desde el Inspector

    public float speed = 5f;
    public int normalDamage = 1;
    public int aerialDamage = 2;
    public int areaDamage = 3;

    private int damage; // Da�o actual basado en el tipo de ataque

    void Start()
    {
        // Configurar el da�o en base al tipo de ataque seleccionado
        switch (attackType)
        {
            case AttackType.Normal:
                damage = normalDamage;
                break;
            case AttackType.Aerial:
                damage = aerialDamage;
                break;
            case AttackType.Area:
                damage = areaDamage;
                break;
        }
    }

    void Update()
    {
        // Movimiento constante en la direcci�n en la que fue instanciado
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Detectar colisi�n con enemigos
        if (collision.CompareTag("Enemy"))
        {
            // Reducir la vida del enemigo seg�n el da�o del tipo de ataque
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

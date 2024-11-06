using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2f;          // Rango en el que el enemigo puede atacar
    public int damagePerTick = 1;           // Daño que se inflige por tic
    public float tickInterval = 1f;         // Intervalo de tiempo entre cada tic de daño
    public LayerMask unitLayer;             // Capa para detectar los objetos con tag "Unit"

    private float tickTimer;
    private GameObject targetUnit;
    private Animator animator;

    void Start()
    {

     animator = GetComponentInParent<Animator>();

    }




    void Update()
    {
        // Buscar unidades dentro del rango de ataque
        DetectUnit();

        // Si hay una unidad en rango, aplicar daño en tics
        if (targetUnit != null)
        {
                animator.SetBool("atacando", true);
            ApplyDamageOverTime();
        }
        else
        {
           animator.SetBool("atacando", false);
        }

    }

    private void DetectUnit()
    {
        // Revisar si hay colisiones en el rango de ataque con el tag "Unit"
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, unitLayer);
        
        if (hits.Length > 0)
        {
            // Tomar la primera unidad detectada en el rango
            targetUnit = hits[0].gameObject;
        }
        else
        {
            targetUnit = null; // Si no hay unidades, establecer el objetivo como nulo
        }
    }

    private void ApplyDamageOverTime()
    {
        // Incrementar el temporizador de tic
        tickTimer += Time.deltaTime;

        // Si el tiempo del tic ha pasado, infligir daño y reiniciar el temporizador
        if (tickTimer >= tickInterval)
        {
            // Aplicar daño al objetivo
            UnitHealth unitHealth = targetUnit.GetComponent<UnitHealth>();
            if (unitHealth != null)
            {
                unitHealth.TakeDamage(damagePerTick);
            }

            // Reiniciar el temporizador de tic
            tickTimer = 0f;
        }
    }

    // Método para visualizar el rango de ataque en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

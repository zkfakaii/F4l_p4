using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public enum AttackType { Ground, Aerial, Area } // Agregado el tipo de ataque "Area"
    public AttackType attackType = AttackType.Ground; // Tipo de ataque, predeterminado a "Ground"

    public float attackRange = 2f;          // Rango en el que el enemigo puede atacar
    public int damagePerTick = 1;           // Da�o que se inflige por tic
    public float tickInterval = 1f;         // Intervalo de tiempo entre cada tic de da�o
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

        // Si hay una unidad en rango, aplicar da�o en tics
        if (targetUnit != null)
        {
            // Cambia la animaci�n seg�n el tipo de ataque
            if (attackType == AttackType.Aerial)
            {
                animator.SetBool("aerialAttack", true); // Activar animaci�n de ataque a�reo
            }
            else if (attackType == AttackType.Area)
            {
                animator.SetBool("areaAttack", true); // Activar animaci�n de ataque �rea (puedes agregarla en el Animator)
            }
            else
            {
                animator.SetBool("atacando", true);     // Activar animaci�n de ataque terrestre
            }

            ApplyDamageOverTime();
        }
        else
        {
            // Desactiva las animaciones cuando no hay un objetivo
            animator.SetBool("atacando", false);
            animator.SetBool("aerialAttack", false);
            animator.SetBool("areaAttack", false); // Desactivar animaci�n de ataque �rea
        }
    }

    private void DetectUnit()
    {
        // Revisar si hay colisiones en el rango de ataque con el tag "Unit"
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, unitLayer);

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

        // Si el tiempo del tic ha pasado, infligir da�o y reiniciar el temporizador
        if (tickTimer >= tickInterval)
        {
            // Aplicar da�o al objetivo
            UnitHealth unitHealth = targetUnit.GetComponent<UnitHealth>();
            if (unitHealth != null)
            {
                unitHealth.TakeDamage(damagePerTick, (UnitHealth.AttackType)attackType);
            }

            // Reiniciar el temporizador de tic
            tickTimer = 0f;
        }
    }

    // M�todo para visualizar el rango de ataque en el editor
    private void OnDrawGizmosSelected()
    {
        // Cambiar el color dependiendo del tipo de ataque
        if (attackType == AttackType.Aerial)
        {
            Gizmos.color = Color.blue;
        }
        else if (attackType == AttackType.Area)
        {
            Gizmos.color = Color.green; // Color para el ataque �rea
        }
        else
        {
            Gizmos.color = Color.red; // Color para el ataque terrestre
        }

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

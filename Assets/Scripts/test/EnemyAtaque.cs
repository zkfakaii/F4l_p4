using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtaque : MonoBehaviour
{
    public enum AttackType { Ground, Aerial, Area } // Tipos de ataque (Terrestre, A�reo, �rea)
    public AttackType attackType = AttackType.Ground; // Tipo de ataque, por defecto "Ground"

    public float attackRange = 2f;          // Rango de ataque del enemigo
    public int damagePerTick = 1;           // Da�o infligido por tic
    public float tickInterval = 1f;         // Intervalo de tiempo entre cada tic de da�o
    public LayerMask unitLayer;             // Capa para detectar unidades

    private float tickTimer;
    private GameObject targetUnit;
    private Animator animator;

    private EnemyWalk enemyWalk;            // Referencia al script EnemyWalk

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        enemyWalk = GetComponent<EnemyWalk>(); // Obtener referencia al script EnemyWalk
    }

    void Update()
    {
        // Detectar unidades dentro del rango de ataque
        DetectUnit();

        // Si hay una unidad en el rango, detener el movimiento y atacar
        if (targetUnit != null)
        {

            // Cambiar la animaci�n seg�n el tipo de ataque
            if (attackType == AttackType.Aerial)
            {
                animator.SetBool("aerialAttack", true); // Activar animaci�n de ataque a�reo
            }
            else if (attackType == AttackType.Area)
            {
                animator.SetBool("areaAttack", true); // Activar animaci�n de ataque �rea
            }
            else
            {
                animator.SetBool("atacando", true);     // Activar animaci�n de ataque terrestre
            }

            ApplyDamageOverTime();
        }
        else
        {

            // Desactivar las animaciones cuando no hay un objetivo
            animator.SetBool("atacando", false);
            animator.SetBool("aerialAttack", false);
            animator.SetBool("areaAttack", false); // Desactivar animaci�n de ataque �rea
        }
    }

    private void DetectUnit()
    {
        // Revisar si hay unidades en el rango de ataque con la capa "Unit"
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
            UnidadHP unidadHP = targetUnit.GetComponent<UnidadHP>();
            if (unidadHP != null)
            {
                // Convertir AttackType a UnidadHP.DamageType
                UnidadHP.DamageType damageType = (UnidadHP.DamageType)attackType;

                // Aplicar el da�o dependiendo del tipo de ataque
                unidadHP.TakeDamage(damagePerTick, damageType);
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

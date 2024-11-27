using UnityEngine;

public class EnemyAtaque : MonoBehaviour
{
    public enum AttackType { Ground, Aerial, Area } // Tipos de ataque
    public AttackType attackType = AttackType.Ground;

    public float attackRange = 2f;           // Rango de ataque del enemigo
    public int damagePerTick = 1;           // Daño infligido por tic
    public float tickInterval = 1f;         // Intervalo de tiempo entre cada tic de daño
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

        // Si hay una unidad en el rango y las físicas entre las capas son interactivas, detener el movimiento y atacar
        if (targetUnit != null && CanInteractWithUnit(targetUnit))
        {
            if (!enemyWalk.isFrozen)
            {
                enemyWalk.FreezeMovement(); // Detener el movimiento del enemigo
            }

            ApplyDamageOverTime();
        }
        else
        {
            // Si no hay objetivo válido, detener el ataque y permitir movimiento
            animator.SetBool("atacando", false);

            if (enemyWalk.isFrozen)
            {
                enemyWalk.UnfreezeMovement(); // Reanudar el movimiento del enemigo
            }
        }
    }

    private void DetectUnit()
    {
        // Detectar todas las unidades dentro del rango de ataque
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, unitLayer);

        targetUnit = null; // Reiniciar el objetivo
        foreach (var hit in hits)
        {
            // Verificar si el objeto tiene el tag "Unit"
            if (hit.CompareTag("Unit"))
            {
                targetUnit = hit.gameObject;
                break; // Salir del bucle al encontrar la primera unidad válida
            }
        }
    }

    private bool CanInteractWithUnit(GameObject unit)
    {
        if (unit == null) return false;

        // Obtener las capas del enemigo y la unidad
        int enemyLayer = gameObject.layer;
        int unitLayer = unit.layer;

        // Comprobar si las físicas entre las capas son interactivas
        return Physics.GetIgnoreLayerCollision(enemyLayer, unitLayer) == false;
    }

    private void ApplyDamageOverTime()
    {
        // Incrementar el temporizador de tic
        tickTimer += Time.deltaTime;

        // Si el tiempo del tic ha pasado, infligir daño y reiniciar el temporizador
        if (tickTimer >= tickInterval)
        {
            UnidadHP unidadHP = targetUnit.GetComponent<UnidadHP>();
            if (unidadHP != null)
            {
                UnidadHP.DamageType damageType = (UnidadHP.DamageType)attackType;
                unidadHP.TakeDamage(damagePerTick, damageType);
            }

            tickTimer = 0f; // Reiniciar el temporizador de tic
        }
    }

    // Método para visualizar el rango de ataque en el editor
    private void OnDrawGizmosSelected()
    {
        if (attackType == AttackType.Aerial)
        {
            Gizmos.color = Color.blue;
        }
        else if (attackType == AttackType.Area)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

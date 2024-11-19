using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Configuración del Proyectil")]
    [SerializeField] private float speed = 10f;  // Velocidad del proyectil
    private Vector3 fireDirection;  // Dirección del proyectil
    private int damage;             // Daño del proyectil

    // Método para asignar la dirección del proyectil
    public void SetFireDirection(Vector3 direction)
    {
        fireDirection = direction.normalized;  // Asegura que la dirección esté normalizada
    }

    // Método para asignar el valor de daño al proyectil
    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }

    private void Update()
    {
        // Mover el proyectil en la dirección indicada
        transform.Translate(fireDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el proyectil colisiona con un enemigo
        EnemigosHP enemyHP = other.GetComponent<EnemigosHP>();
        Debug.Log(other.name);
        if (enemyHP != null)
        {
            // Aplica el daño al enemigo
            enemyHP.TakeDamage(damage);
            Destroy(gameObject);  // El proyectil se destruye después de la colisión
        }
    }
}

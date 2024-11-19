using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Configuraci�n del Proyectil")]
    [SerializeField] private float speed = 10f;  // Velocidad del proyectil
    private Vector3 fireDirection;  // Direcci�n del proyectil
    private int damage;             // Da�o del proyectil

    // M�todo para asignar la direcci�n del proyectil
    public void SetFireDirection(Vector3 direction)
    {
        fireDirection = direction.normalized;  // Asegura que la direcci�n est� normalizada
    }

    // M�todo para asignar el valor de da�o al proyectil
    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }

    private void Update()
    {
        // Mover el proyectil en la direcci�n indicada
        transform.Translate(fireDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el proyectil colisiona con un enemigo
        EnemigosHP enemyHP = other.GetComponent<EnemigosHP>();
        Debug.Log(other.name);
        if (enemyHP != null)
        {
            // Aplica el da�o al enemigo
            enemyHP.TakeDamage(damage);
            Destroy(gameObject);  // El proyectil se destruye despu�s de la colisi�n
        }
    }
}

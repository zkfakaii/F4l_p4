using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 targetPosition; // La posición objetivo hacia donde el enemigo se mueve.
    [SerializeField] private float speed = 3f; // Velocidad de movimiento del enemigo (ajustable desde Unity Inspector)

    // Establecer la posición objetivo (fin de la línea)
    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    // Establecer la velocidad del enemigo
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        // Mover al enemigo hacia la posición objetivo
        if (targetPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Cuando el enemigo llegue a la posición objetivo, se destruye
            if (transform.position == targetPosition)
            {
                Destroy(gameObject); // Destruir el enemigo al llegar a la meta
            }
        }
    }
}

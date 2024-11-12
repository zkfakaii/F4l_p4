using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 targetPosition;    // La posición a la que el enemigo debe moverse
    public float moveSpeed = 2f;       // Velocidad de movimiento del enemigo
    private bool isMoving = false;     // Indicador de si el enemigo está en movimiento

    // Este método configura la posición objetivo de la línea de movimiento
    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    void Update()
    {
        // Si el enemigo está en movimiento, moverlo hacia la posición objetivo
        if (isMoving)
        {
            // Calcular el movimiento del enemigo hacia la posición objetivo
            float step = moveSpeed * Time.deltaTime;  // La distancia que recorrerá en esta actualización
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            // Si ha llegado al objetivo, detener el movimiento (opcionalmente destruir o hacer algo más)
            if (transform.position == targetPosition)
            {
                isMoving = false;
                // Aquí puedes poner cualquier acción que deba realizar el enemigo al llegar al destino
                // Ejemplo: Destroy(gameObject); // Destruir el enemigo al llegar
                Debug.Log("¡El enemigo ha llegado a su destino!");
            }
        }
    }
}

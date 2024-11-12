using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 targetPosition;    // La posici�n a la que el enemigo debe moverse
    public float moveSpeed = 2f;       // Velocidad de movimiento del enemigo
    private bool isMoving = false;     // Indicador de si el enemigo est� en movimiento

    // Este m�todo configura la posici�n objetivo de la l�nea de movimiento
    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    void Update()
    {
        // Si el enemigo est� en movimiento, moverlo hacia la posici�n objetivo
        if (isMoving)
        {
            // Calcular el movimiento del enemigo hacia la posici�n objetivo
            float step = moveSpeed * Time.deltaTime;  // La distancia que recorrer� en esta actualizaci�n
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            // Si ha llegado al objetivo, detener el movimiento (opcionalmente destruir o hacer algo m�s)
            if (transform.position == targetPosition)
            {
                isMoving = false;
                // Aqu� puedes poner cualquier acci�n que deba realizar el enemigo al llegar al destino
                // Ejemplo: Destroy(gameObject); // Destruir el enemigo al llegar
                Debug.Log("�El enemigo ha llegado a su destino!");
            }
        }
    }
}

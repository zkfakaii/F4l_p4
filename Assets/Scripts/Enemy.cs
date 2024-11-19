using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 targetPosition; // La posición objetivo hacia donde el enemigo se mueve.
    [SerializeField] private float speed = 3f; // Velocidad de movimiento del enemigo (ajustable desde Unity Inspector)
    private bool isStopped = false; // Indica si el enemigo está detenido

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
        // Mover al enemigo hacia la posición objetivo solo si no está detenido
        if (!isStopped && targetPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    // Método que detiene permanentemente el movimiento
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("unidad")) // Asegúrate de que las plantas tengan la etiqueta "Plant"
        {
            isStopped = true; // Detener el movimiento para siempre
        }
    }
}

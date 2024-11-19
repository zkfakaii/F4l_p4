using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 targetPosition; // La posici�n objetivo hacia donde el enemigo se mueve.
    [SerializeField] private float speed = 3f; // Velocidad de movimiento del enemigo (ajustable desde Unity Inspector)
    private bool isStopped = false; // Indica si el enemigo est� detenido

    // Establecer la posici�n objetivo (fin de la l�nea)
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
        // Mover al enemigo hacia la posici�n objetivo solo si no est� detenido
        if (!isStopped && targetPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    // M�todo que detiene permanentemente el movimiento
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("unidad")) // Aseg�rate de que las plantas tengan la etiqueta "Plant"
        {
            isStopped = true; // Detener el movimiento para siempre
        }
    }
}

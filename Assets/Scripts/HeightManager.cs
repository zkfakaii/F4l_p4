using UnityEngine;

public class HeightManager : MonoBehaviour
{
    private enum UnitState { Normal, Generating, Aerial }  // Definimos los tres estados posibles
    private UnitState currentState = UnitState.Normal;     // Estado inicial en Normal

    public MonoBehaviour aerialScript;      // Script para el modo Aéreo
    private UnitState previousState;        // Almacena el estado anterior al modo Aéreo

    private bool isAerial = false;          // Indicador de si estamos en modo aéreo

    
    void Start()
    {
        // Inicializar el script de comportamiento normal en el modo inicial (Normal)
        SetActiveState(UnitState.Normal);

       
    }

    void Update()
    {
        // Cambiar al modo Aéreo al hacer clic derecho
        if (Input.GetMouseButtonDown(1)) // Detecta clic derecho
        {
            if (isAerial)
            {
                // Regresar al estado anterior cuando hacemos clic derecho
                SetActiveState(previousState);
                isAerial = false; // Se sale del modo aéreo

                
            }
            else
            {
                // Guardar el estado anterior y entrar en modo aéreo
                previousState = currentState;
                SetActiveState(UnitState.Aerial);
                isAerial = true;
                Debug.Log("arria");
                // Entramos en el modo aéreo

                // Activar la animación de Aéreo
               
            }
        }
    }

    // Método para gestionar la activación de estados
    private void SetActiveState(UnitState newState)
    {
        // Desactivar el script aéreo
        if (aerialScript != null) aerialScript.enabled = false;

        // Activar el script correspondiente al nuevo estado
        switch (newState)
        {
            case UnitState.Normal:
                // Aquí no estamos gestionando el script de Normal Mode
                break;
            case UnitState.Aerial:
                if (aerialScript != null) aerialScript.enabled = true;
                break;
        }

        // Actualizar el estado actual
        currentState = newState;
    }
}


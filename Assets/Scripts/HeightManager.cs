using UnityEngine;

public class HeightManager : MonoBehaviour
{
    private enum UnitState { Normal, Generating, Aerial }  // Definimos los tres estados posibles
    private UnitState currentState = UnitState.Normal;     // Estado inicial en Normal

    public MonoBehaviour aerialScript;      // Script para el modo A�reo
    private UnitState previousState;        // Almacena el estado anterior al modo A�reo

    private bool isAerial = false;          // Indicador de si estamos en modo a�reo

    // Propiedad p�blica para acceder al estado a�reo
    public bool IsAerial => isAerial;

    void Start()
    {
        // Inicializar el script de comportamiento normal en el modo inicial (Normal)
        SetActiveState(UnitState.Normal);
    }

    void Update()
    {
        // Cambiar al modo A�reo al hacer clic derecho
        if (Input.GetMouseButtonDown(1)) // Detecta clic derecho
        {
            if (isAerial)
            {
                // Regresar al estado anterior cuando hacemos clic derecho
                SetActiveState(previousState);
                isAerial = false; // Se sale del modo a�reo
            }
            else
            {
                // Guardar el estado anterior y entrar en modo a�reo
                previousState = currentState;
                SetActiveState(UnitState.Aerial);
                isAerial = true;  // Se entra al modo a�reo
                Debug.Log("Modo a�reo activado");
            }
        }
    }

    // M�todo para gestionar la activaci�n de estados
    private void SetActiveState(UnitState newState)
    {
        // Desactivar el script a�reo si lo hay
        if (aerialScript != null) aerialScript.enabled = false;

        // Activar el script correspondiente al nuevo estado
        switch (newState)
        {
            case UnitState.Normal:
                // Aqu� no estamos gestionando el script de Normal Mode
                break;
            case UnitState.Aerial:
                if (aerialScript != null) aerialScript.enabled = true;
                break;
        }

        // Actualizar el estado actual
        currentState = newState;
    }
}

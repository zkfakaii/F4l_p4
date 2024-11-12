using UnityEngine;

public class GeneratingMode : MonoBehaviour
{
    public float mielPerTick = 5f;           // Cantidad de Miel generada por tic
    public float tickInterval = 2f;          // Intervalo de generación en segundos
    private float tickTimer;                 // Temporizador para los tics de generación

    private bool isGenerating = false;       // Estado de generación
    private bool isAerial = false;           // Estado Aéreo
    public MonoBehaviour behaviorScript;     // Script de comportamiento específico a desactivar/activar
    private Miel mielSystem;                 // Referencia al sistema de Miel

    public Animator animator;                // Referencia al Animator
    public string[] stateTriggers = { "NormalMode", "GeneratingMode", "AerialMode" }; // Triggers para las animaciones

    void Start()
    {
        // Buscar el sistema de Miel en la escena
        mielSystem = FindObjectOfType<Miel>();

        // Validación para asegurar que se ha asignado un script de comportamiento
        if (behaviorScript == null)
        {
            Debug.LogWarning("No se ha asignado un script de comportamiento en " + gameObject.name);
        }
    }

    void Update()
    {
        if (isGenerating)
        {
            tickTimer += Time.deltaTime;

            if (tickTimer >= tickInterval)
            {
                GenerateMiel();
                tickTimer = 0f;  // Resetear el temporizador
            }
        }

        // Cambiar al modo Aéreo si se presiona clic derecho
        if (Input.GetMouseButtonDown(1)) // Detecta clic derecho
        {
            if (isAerial)
            {
                // Regresar al estado anterior
                SetStateToNormal(); // Cambia al modo normal
            }
            else
            {
                // Cambiar a Aéreo
                SetStateToAerial(); // Cambia al modo aéreo
            }
        }
    }

    // Método para alternar entre el modo generador y el modo de comportamiento original
    private void OnMouseDown()
    {
        isGenerating = !isGenerating;

        // Activar/desactivar el script de comportamiento
        if (behaviorScript != null)
        {
            behaviorScript.enabled = !isGenerating;
        }

        // Cambiar la animación dependiendo del estado de generación
        if (animator != null)
        {
            if (isGenerating)
            {
                animator.SetTrigger(stateTriggers[1]); // Generating Mode
            }
            else
            {
                animator.SetTrigger(stateTriggers[0]); // Normal Mode
            }
        }
    }

    // Cambiar al estado Aéreo
    private void SetStateToAerial()
    {
        isAerial = true;

        // Cambiar la animación a Aerial Mode
        if (animator != null)
        {
            animator.SetTrigger(stateTriggers[2]); // Aerial Mode
        }

        // Desactivar el script de generación si estaba activado
        if (behaviorScript != null)
        {
            behaviorScript.enabled = false;
        }
    }

    // Cambiar al estado Normal
    private void SetStateToNormal()
    {
        isAerial = false;

        // Cambiar la animación a Normal Mode
        if (animator != null)
        {
            animator.SetTrigger(stateTriggers[0]); // Normal Mode
        }

        // Activar el script de comportamiento nuevamente
        if (behaviorScript != null)
        {
            behaviorScript.enabled = true;
        }
    }

    // Método para generar Miel
    private void GenerateMiel()
    {
        if (mielSystem != null)
        {
            mielSystem.currentMiel += (int)mielPerTick;
            mielSystem.currentMiel = Mathf.Clamp(mielSystem.currentMiel, 0, (int)mielSystem.maxMiel);
        }
    }
}

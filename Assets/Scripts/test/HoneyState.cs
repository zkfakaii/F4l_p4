using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class HoneyState : MonoBehaviour
{
    public int honeyPerTick = 5;        // Cantidad de miel generada por tick.
    public float tickInterval = 1f;     // Intervalo de tiempo entre cada tick en segundos.
    public int maxHoney = 100;          // Máximo de miel que puede generar la unidad.

    private int currentHoney = 0;       // Miel actual generada por esta unidad.
    private float tickTimer = 0f;       // Temporizador para controlar los ticks.
    private bool isGenerating = false;  // Estado para saber si la unidad está generando miel.

    public Miel globalMielSystem;       // Referencia al sistema global de Miel

    void Start()
    {
        if (globalMielSystem == null)
        {
            Debug.LogWarning("No se ha asignado el sistema global de Miel. Asegúrate de conectarlo en el Inspector.");
        }
    }

    void Update()
    {

        // Si la unidad está en modo Generating, generar miel
        if (gameObject.layer == LayerMask.NameToLayer("Generating"))
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= tickInterval)
            {
                GenerateHoney();
                tickTimer -= tickInterval;
            }
        }
    }

    void GenerateHoney()
    {
        // Generar miel y enviarla al sistema global, respetando el límite
        if (globalMielSystem != null && globalMielSystem.currentMiel < (int)globalMielSystem.maxMiel)
        {
            int remainingCapacity = (int)globalMielSystem.maxMiel - globalMielSystem.currentMiel;
            int honeyToGenerate = Mathf.Min(honeyPerTick, remainingCapacity);

            globalMielSystem.currentMiel += honeyToGenerate;
            currentHoney += honeyToGenerate;

            // Limitar la miel generada localmente al máximo permitido
            currentHoney = Mathf.Clamp(currentHoney, 0, maxHoney);

            Debug.Log($"Unidad generó {honeyToGenerate} de miel. Total global: {globalMielSystem.currentMiel}");
        }
        else
        {
            Debug.Log("La miel global está llena. No se puede generar más por ahora.");
        }
    }
}

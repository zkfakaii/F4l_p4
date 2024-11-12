using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    public enum AttackType { Ground, Aerial, Area }
    public enum UnitMode { Normal, Aerial, Generating }

    [Header("Health Settings")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Damage Vulnerabilities (Normal Mode)")]
    public bool canTakeGroundDamageInNormalMode = true;
    public bool canTakeAerialDamageInNormalMode = true;
    public bool canTakeAreaDamageInNormalMode = true;

    [Header("Damage Vulnerabilities (Aerial Mode)")]
    public bool canTakeGroundDamageInAerialMode = false;
    public bool canTakeAerialDamageInAerialMode = true;
    public bool canTakeAreaDamageInAerialMode = false;

    [Header("Current Mode")]
    public UnitMode currentMode = UnitMode.Normal;

    public GeneratingMode generatingModeScript; // Referencia al script GeneratingMode
    private HeightManager heightManager;   // Referencia al script HeightManager

    void Start()
    {
        currentHealth = maxHealth;
        generatingModeScript = FindObjectOfType<GeneratingMode>();  // Buscar GeneratingMode en la escena
        heightManager = FindObjectOfType<HeightManager>();    // Buscar HeightManager en la escena
    }

    void Update()
    {
        // Comprobar si el modo de generaci�n est� activo
        if (generatingModeScript != null && generatingModeScript.IsGenerating)
        {
            Debug.Log("Modo de Generaci�n activado");
        }
        else
        {
            Debug.Log("Modo de Generaci�n desactivado");
        }

        // Comprobar si la unidad est� en el aire
        if (heightManager != null)
        {
            if (heightManager.IsAerial)  // Usamos la propiedad p�blica IsAerial
            {
                currentMode = UnitMode.Aerial;
            }
            else
            {
                currentMode = UnitMode.Normal;
            }
        }
    }

    public void TakeDamage(int damage, AttackType attackType)
    {
        if (attackType == AttackType.Aerial || attackType == AttackType.Area || CanTakeDamageInCurrentMode(attackType))
        {
            currentHealth -= damage;
            Debug.Log("Unidad recibi� da�o, salud actual: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        else
        {
            Debug.Log("Ataque no afect� a la unidad en el modo actual.");
        }
    }

    private bool CanTakeDamageInCurrentMode(AttackType attackType)
    {
        switch (currentMode)
        {
            case UnitMode.Normal:
            case UnitMode.Generating:
                return (attackType == AttackType.Ground && canTakeGroundDamageInNormalMode) ||
                       (attackType == AttackType.Aerial && canTakeAerialDamageInNormalMode) ||
                       (attackType == AttackType.Area && canTakeAreaDamageInNormalMode);

            case UnitMode.Aerial:
                return (attackType == AttackType.Ground && canTakeGroundDamageInAerialMode) ||
                       (attackType == AttackType.Aerial && canTakeAerialDamageInAerialMode) ||
                       (attackType == AttackType.Area && canTakeAreaDamageInAerialMode);

            default:
                return false;
        }
    }

    private void Die()
    {
        Debug.Log("Unidad muri�");
        Destroy(gameObject);
    }
}


using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UnidadHP : MonoBehaviour
{
    public enum DamageType
    {
        Terrestrial,
        Aerial,
        Area
    }

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // Vida m�xima de la unidad.
    private int currentHealth;                   // Vida actual de la unidad.

    [Header("Layer Settings")]
    [SerializeField] private string normalLayer = "Normal";
    [SerializeField] private string aerialLayer = "Aerial";
    [SerializeField] private string generatingLayer = "Generating"; 
    

    private string currentLayer; // El layer actual de la unidad.

    private void Start()
    {
        // Inicializa la salud y obtiene el estado inicial del layer.
        currentHealth = maxHealth;

        // Obt�n el nombre del layer actual del objeto.
        currentLayer = LayerMask.LayerToName(gameObject.layer);
    }

    private void Update()
    {
        // Actualiza constantemente el nombre del layer actual.
        currentLayer = LayerMask.LayerToName(gameObject.layer);
    }

    /// <summary>
    /// Aplica da�o a la unidad seg�n el tipo de da�o y el estado actual.
    /// </summary>
    /// <param name="damage">Cantidad de da�o recibido.</param>
    /// <param name="damageType">Tipo de da�o.</param>
    public void TakeDamage(int damage, DamageType damageType)
    {
        // Validar el da�o dependiendo del tipo y del estado actual de la unidad.
        if (damageType == DamageType.Terrestrial && 
            (currentLayer == normalLayer || currentLayer == generatingLayer))
        {
            ApplyDamage(damage);
        }
        else if (damageType == DamageType.Aerial && currentLayer == aerialLayer)
        {
            ApplyDamage(damage);
        }
        else if (damageType == DamageType.Area)
        {
            ApplyDamage(damage);
        }
    }

    /// <summary>
    /// Aplica el da�o directo a la unidad y verifica si debe morir.
    /// </summary>
    /// <param name="damage">Cantidad de da�o recibido.</param>
    private void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name} recibi� {damage} de da�o. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Elimina la unidad del juego.
    /// </summary>
    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leus : MonoBehaviour
{
    [Header("Layers Configurados")]
    [SerializeField] private string normalLayer = "Normal";
    [SerializeField] private string generatingLayer = "Generating";
    [SerializeField] private string aerialLayer = "Aerial";

    [SerializeField] private string enemyGroundLayer = "EnemyGround";
    [SerializeField] private string enemyAerialLayer = "EnemyAerial";

    private void Start()
    {
        // Configura las colisiones iniciales según el estado inicial del layer
        UpdateCollisions();
    }

    private void Update()
    {
        // Si el layer cambia, actualiza las colisiones
        UpdateCollisions();
    }

    /// <summary>
    /// Actualiza las colisiones basándose en el layer actual del objeto.
    /// </summary>
    private void UpdateCollisions()
    {
        if (gameObject.layer == LayerMask.NameToLayer(normalLayer) || gameObject.layer == LayerMask.NameToLayer(generatingLayer))
        {
            // En estado Normal o Generating
            Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(enemyAerialLayer), true); // Ignorar colisiones con EnemyAerial
            Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(enemyGroundLayer), false); // Permitir colisiones con EnemyGround
        }
        else if (gameObject.layer == LayerMask.NameToLayer(aerialLayer))
        {
            // En estado Aerial
            Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(enemyGroundLayer), true); // Ignorar colisiones con EnemyGround
            Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(enemyAerialLayer), false); // Permitir colisiones con EnemyAerial
        }
    }
}

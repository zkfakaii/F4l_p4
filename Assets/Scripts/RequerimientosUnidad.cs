using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequerimientosUnidad : MonoBehaviour
{
    public int costoUnidad = 25;          // Costo en Miel para invocar una unidad
    private Miel sistemaDeMiel;           // Referencia al script Miel

    void Awake()
    {
        // Buscar el componente Miel en la escena
        sistemaDeMiel = FindObjectOfType<Miel>();
    }

    // Verifica si hay suficiente Miel para invocar la unidad
    public bool TieneMielSuficiente()
    {
        if (sistemaDeMiel == null) return false;
        return sistemaDeMiel.currentMiel >= costoUnidad;
    }

    // Descuenta la Miel necesaria después de invocar una unidad
    public void DescontarMiel()
    {
        if (TieneMielSuficiente())
        {
            sistemaDeMiel.currentMiel -= costoUnidad;
        }
    }
}

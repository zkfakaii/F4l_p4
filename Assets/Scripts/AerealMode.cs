using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialMode : MonoBehaviour
{
    // Esta clase es solo para pruebas, no tiene funcionalidad real.
    
    void Start()
    {
        Debug.Log(gameObject.name + " ha entrado en el modo A�reo.");
    }

    void Update()
    {
        // Aqu� no estamos a�adiendo funcionalidad real, pero podr�amos poner una simple rotaci�n para observar que est� activo
        if (enabled)
        {
            // Por ejemplo, rotar la unidad mientras est� en modo a�reo solo como prueba
            transform.Rotate(Vector3.up, 20f * Time.deltaTime);
        }
    }
}

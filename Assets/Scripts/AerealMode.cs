using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialMode : MonoBehaviour
{
    // Esta clase es solo para pruebas, no tiene funcionalidad real.
    
    void Start()
    {
        Debug.Log(gameObject.name + " ha entrado en el modo Aéreo.");
    }

    void Update()
    {
        // Aquí no estamos añadiendo funcionalidad real, pero podríamos poner una simple rotación para observar que está activo
        if (enabled)
        {
            // Por ejemplo, rotar la unidad mientras está en modo aéreo solo como prueba
            transform.Rotate(Vector3.up, 20f * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarObjeto : MonoBehaviour
{
    public GameObject objetoAActivar; // Objeto que se activará

    public void Activar()
    {
        if (objetoAActivar != null)
        {
            objetoAActivar.SetActive(true); // Activar el objeto
        }
    }
}

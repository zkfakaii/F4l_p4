using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    public CanvasGroup canvas;
    public GameObject dialogo;

    public void activar()
    {
        canvas.alpha = 1;
        dialogo.SetActive(true);
    }
}
using UnityEngine;
using UnityEngine.UI;

public class Miel : MonoBehaviour
{
    public Image mielImage;             // Imagen de la barra de Miel
    public float maxMiel = 100f;        // Máximo valor de Miel
    public int currentMiel = 0;         // Valor inicial de Miel (comienza en 0)
    public float regenerationRate = 1f; // Tasa de regeneración (cantidad de Miel regenerada por segundo)

    private float mielAccumulator = 0f; // Acumulador para la regeneración fraccionada

    void Start()
    {
        // Actualizar la barra de Miel al estado inicial
        UpdateMielImage();
    }

    void Update()
    {
        // Regenerar Miel constantemente en valores enteros
        RegenerateMiel();

        // Actualizar la barra de Miel
        UpdateMielImage();
    }

    void RegenerateMiel()
    {
        // Acumular la regeneración usando un valor fraccionado
        mielAccumulator += regenerationRate * Time.deltaTime;

        // Solo aumentar `currentMiel` cuando el acumulador llega a un valor entero
        if (mielAccumulator >= 1f)
        {
            int regenAmount = Mathf.FloorToInt(mielAccumulator); // Convertir acumulado a entero
            currentMiel += regenAmount;
            mielAccumulator -= regenAmount; // Restar el entero acumulado

            // Restringir el valor de Miel al máximo
            currentMiel = Mathf.Clamp(currentMiel, 0, (int)maxMiel);
        }
    }

    void UpdateMielImage()
    {
        // Ajustar el fillAmount de la imagen para reflejar el porcentaje actual de Miel
        float fillValue = (float)currentMiel / maxMiel;
        mielImage.fillAmount = fillValue;
    }
}

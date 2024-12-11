using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Miel : MonoBehaviour
{
    public Image mielImage;             // Imagen de la barra de Miel
    public TextMeshProUGUI currentMielText; // Texto para mostrar la Miel actual
    public TextMeshProUGUI maxMielText;     // Texto para mostrar la Miel máxima

    public float maxMiel = 100f;        // Máximo valor de Miel
    public int currentMiel = 0;         // Valor inicial de Miel (comienza en 0)
    public float regenerationRate = 1f; // Tasa de regeneración (cantidad de Miel regenerada por segundo)

    private float mielAccumulator = 0f; // Acumulador para la regeneración fraccionada

    void Start()
    {
        // Actualizar la barra de Miel y textos al estado inicial
        UpdateMielUI();
    }

    void Update()
    {
        // Regenerar Miel constantemente en valores enteros
        RegenerateMiel();

        // Actualizar la barra de Miel y textos
        UpdateMielUI();
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

    void UpdateMielUI()
    {
        // Ajustar el fillAmount de la imagen para reflejar el porcentaje actual de Miel
        float fillValue = (float)currentMiel / maxMiel;
        mielImage.fillAmount = fillValue;

        // Actualizar textos
        if (currentMielText != null)
            currentMielText.text = currentMiel.ToString();

        if (maxMielText != null)
            maxMielText.text = maxMiel.ToString();
    }
}

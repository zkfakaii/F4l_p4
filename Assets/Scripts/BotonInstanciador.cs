using UnityEngine;

public class BotonIniciador : MonoBehaviour
{
    public GameObject unidadPrefab;                  // Prefab de la unidad a instanciar
    public RequerimientosUnidad requerimientosUnidad; // Referencia al script RequerimientosUnidad
    public MovimientoObjeto movimientoObjeto;        // Referencia al script que controla el movimiento de la unidad

    private Button boton;                             // Referencia al bot�n (se obtiene desde el inspector)

    void Start()
    {
        // Obtener el componente del bot�n
        boton = GetComponent<Button>();
        // Asegurarnos de que el bot�n solo funcione si hay suficiente Miel
        boton.onClick.AddListener(IntentarInvocarUnidad);
    }

    // M�todo para intentar invocar una unidad
    public void IntentarInvocarUnidad()
    {
        // Verificar si hay suficiente Miel para la unidad
        if (requerimientosUnidad.PuedeInvocarUnidad())
        {
            // Instanciar la unidad en la posici�n inicial deseada (se podr�a modificar esto)
            GameObject objetoInstanciado = Instantiate(unidadPrefab, transform.position, Quaternion.identity);

            // Llamar al m�todo para iniciar el movimiento de la unidad
            movimientoObjeto.IniciarMovimiento(objetoInstanciado);

            // Descontar Miel
            requerimientosUnidad.DescontarMiel();
        }
        else
        {
            Debug.Log("No hay suficiente Miel para invocar la unidad.");
        }
    }
}

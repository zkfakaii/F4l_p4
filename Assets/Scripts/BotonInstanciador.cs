using UnityEngine;

public class BotonIniciador : MonoBehaviour
{
    public GameObject unidadPrefab;                  // Prefab de la unidad a instanciar
    public RequerimientosUnidad requerimientosUnidad; // Referencia al script RequerimientosUnidad
    public MovimientoObjeto movimientoObjeto;        // Referencia al script que controla el movimiento de la unidad

    private Button boton;                             // Referencia al botón (se obtiene desde el inspector)

    void Start()
    {
        // Obtener el componente del botón
        boton = GetComponent<Button>();
        // Asegurarnos de que el botón solo funcione si hay suficiente Miel
        boton.onClick.AddListener(IntentarInvocarUnidad);
    }

    // Método para intentar invocar una unidad
    public void IntentarInvocarUnidad()
    {
        // Verificar si hay suficiente Miel para la unidad
        if (requerimientosUnidad.PuedeInvocarUnidad())
        {
            // Instanciar la unidad en la posición inicial deseada (se podría modificar esto)
            GameObject objetoInstanciado = Instantiate(unidadPrefab, transform.position, Quaternion.identity);

            // Llamar al método para iniciar el movimiento de la unidad
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

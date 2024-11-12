using UnityEngine;
using UnityEngine.UI;

public class BotonInstanciador : MonoBehaviour
{
    public GameObject objetoAInstanciar;          // El objeto que se va a instanciar
    public Button boton;                          // El botón que activa la acción
    private MovimientoObjeto movimientoObjeto;
    private RequerimientosUnidad requerimientosUnidad;

    void Start()
    {
        if (boton != null)
        {
            boton.onClick.AddListener(OnBotonClick);
        }
    }

    void Awake()
    {
        movimientoObjeto = FindObjectOfType<MovimientoObjeto>();
        requerimientosUnidad = FindObjectOfType<RequerimientosUnidad>();
    }

    void OnBotonClick()
    {
        // Verificar si hay suficiente Miel para invocar la unidad
        if (requerimientosUnidad != null && requerimientosUnidad.TieneMielSuficiente())
        {
            GameObject objetoInstanciado = Instantiate(objetoAInstanciar);
            movimientoObjeto.IniciarMovimiento(objetoInstanciado);

            // Descontar la Miel después de invocar la unidad
            requerimientosUnidad.DescontarMiel();
        }
        else
        {
            Debug.Log("No hay suficiente Miel para invocar la unidad.");
        }
    }
}

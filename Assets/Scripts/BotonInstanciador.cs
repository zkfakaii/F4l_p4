using UnityEngine;
using UnityEngine.UI;

public class BotonInstanciador : MonoBehaviour
{
    public GameObject objetoAInstanciar; // Prefab del objeto que se va a colocar.
    public Button boton;                 // Botón que activa la acción.
    private ColocadorObjetos colocadorObjetos;
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
        colocadorObjetos = FindObjectOfType<ColocadorObjetos>();
        requerimientosUnidad = FindObjectOfType<RequerimientosUnidad>();
    }

    void OnBotonClick()
    {
        // Verificar si hay suficiente recurso para invocar la unidad.
        if (requerimientosUnidad != null && requerimientosUnidad.TieneMielSuficiente())
        {
            // Llama al método para iniciar la colocación del objeto.
            colocadorObjetos.SeleccionarObjeto(objetoAInstanciar);

            // Descontar los recursos después de invocar la unidad.
            requerimientosUnidad.DescontarMiel();
        }
        else
        {
            Debug.Log("No hay suficientes recursos para invocar la unidad.");
        }
    }
}

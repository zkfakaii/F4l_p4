using UnityEngine;
using UnityEngine.UI;

public class BotonInstanciador : MonoBehaviour
{
    public GameObject objetoAInstanciar; // Prefab del objeto que se va a colocar.
    public Button boton;                 // Bot�n que activa la acci�n.
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
            // Llama al m�todo para iniciar la colocaci�n del objeto.
            colocadorObjetos.SeleccionarObjeto(objetoAInstanciar);

            // Descontar los recursos despu�s de invocar la unidad.
            requerimientosUnidad.DescontarMiel();
        }
        else
        {
            Debug.Log("No hay suficientes recursos para invocar la unidad.");
        }
    }
}

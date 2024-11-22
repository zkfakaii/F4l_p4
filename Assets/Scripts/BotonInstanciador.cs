using UnityEngine;
using UnityEngine.UI;

public class BotonInstanciador : MonoBehaviour
{
    public GameObject objetoAInstanciar; // Prefab del objeto que se va a colocar.
    public Button boton;                 // Bot�n que activa la acci�n.
    public float tiempoCooldown = 5f;    // Tiempo de cooldown en segundos.

    private ColocadorObjetos colocadorObjetos;
    private RequerimientosUnidad requerimientosUnidad;
    private bool enCooldown = false;

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
        if (enCooldown)
        {
            Debug.Log("Bot�n en cooldown. Espere antes de volver a usarlo.");
            return;
        }

        // Verificar si hay suficiente recurso para invocar la unidad.
        if (requerimientosUnidad != null && requerimientosUnidad.TieneMielSuficiente())
        {
            // Llama al m�todo para iniciar la colocaci�n del objeto.
            colocadorObjetos.SeleccionarObjeto(objetoAInstanciar);

            // Descontar los recursos despu�s de invocar la unidad.
            requerimientosUnidad.DescontarMiel();

            // Inicia el cooldown.
            IniciarCooldown();
        }
        else
        {
            Debug.Log("No hay suficientes recursos para invocar la unidad.");
        }
    }

    private void IniciarCooldown()
    {
        enCooldown = true;                // Activa el estado de cooldown.
        boton.interactable = false;      // Desactiva el bot�n.
        Invoke(nameof(FinalizarCooldown), tiempoCooldown); // Programa el final del cooldown.
    }

    private void FinalizarCooldown()
    {
        enCooldown = false;               // Desactiva el estado de cooldown.
        boton.interactable = true;       // Reactiva el bot�n.
    }
}

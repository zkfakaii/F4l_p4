using UnityEngine;
using UnityEngine.UI;
public class BotonInstanciador : MonoBehaviour
{
    public GameObject objetoAInstanciar; // El objeto que se va a instanciar
    public Button boton;  // El botón que activa la acción
    private MovimientoObjeto movimientoObjeto;
    void Start()
    {
        // Asignar la función al botón cuando se hace clic
        if (boton != null)
        {
            boton.onClick.AddListener(OnBotonClick);
        }
    }
    void Awake()
    {
        // Buscar el componente MovimientoObjeto en la escena (si es necesario)
        movimientoObjeto = FindObjectOfType<MovimientoObjeto>();
    }
    // Método que se llama cuando se hace clic en el botón
    void OnBotonClick()
    {
        // Si el objeto y el script están bien asignados
        if (objetoAInstanciar != null && movimientoObjeto != null)
        {
            // Instanciar el objeto en la escena
            GameObject objetoInstanciado = Instantiate(objetoAInstanciar);
            // Llamamos al método IniciarMovimiento para mover el objeto a la posición del ratón
            movimientoObjeto.IniciarMovimiento(objetoInstanciado);
        }
    }
}
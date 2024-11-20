using UnityEngine;

public class ColocadorObjetos : MonoBehaviour
{
    public LayerMask mapaLayer; // Layer que representa el plano del mapa.
    public Vector3 posicionOffset = Vector3.zero; // Offset para ajustar la posición final.
    public float tamanioCelda = 1f; // Tamaño de las celdas de la grilla.

    private GameObject objetoSeleccionado;

    void Update()
    {
        if (objetoSeleccionado != null)
        {
            // Mueve el objeto con el mouse.
            Vector3 posicionMouse = ObtenerPosicionMouse();
            objetoSeleccionado.transform.position = posicionMouse;

            // Coloca el objeto al hacer clic izquierdo.
            if (Input.GetMouseButtonDown(0))
            {
                ColocarObjeto(posicionMouse);
            }
        }
    }

    public void SeleccionarObjeto(GameObject prefab)
    {
        // Instanciar y seleccionar el objeto para colocarlo.
        objetoSeleccionado = Instantiate(prefab);
    }

    private Vector3 ObtenerPosicionMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, mapaLayer))
        {
            Vector3 posicionImpacto = hitInfo.point;

            // Ajustar a la grilla redondeando las coordenadas al tamaño de la celda.
            float x = Mathf.Round(posicionImpacto.x / tamanioCelda) * tamanioCelda;
            float z = Mathf.Round(posicionImpacto.z / tamanioCelda) * tamanioCelda;

            return new Vector3(x, posicionImpacto.y, z) + posicionOffset;
        }

        return Vector3.zero; // Si no se detecta impacto, retorna posición por defecto.
    }

    private void ColocarObjeto(Vector3 posicion)
    {
        // Fija la posición del objeto y lo libera del control.
        objetoSeleccionado.transform.position = posicion;
        objetoSeleccionado = null; // Se libera el objeto actual.
    }
}

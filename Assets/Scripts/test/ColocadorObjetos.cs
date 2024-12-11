using UnityEngine;

public class ColocadorObjetos : MonoBehaviour
{
    public LayerMask mapaLayer;
    public Vector3 posicionOffset = Vector3.zero;
    public float tamanioCelda = 1f;
    public int anchoGrilla = 10;
    public int altoGrilla = 10;

    private GameObject objetoSeleccionado;
    private bool unidadColocada = false;

    public bool UnidadColocada => unidadColocada;

    void Update()
    {
        if (objetoSeleccionado != null)
        {
            Vector3 posicionMouse = ObtenerPosicionMouse();
            if (posicionMouse != Vector3.zero)
            {
                objetoSeleccionado.transform.position = posicionMouse;

                if (Input.GetMouseButtonDown(0))
                {
                    ColocarObjeto(posicionMouse);
                }
            }
        }
    }

    public void SeleccionarObjeto(GameObject prefab)
    {
        objetoSeleccionado = Instantiate(prefab);
        unidadColocada = false; // Unidad no colocada aún
    }

    private void ColocarObjeto(Vector3 posicion)
    {
        objetoSeleccionado.transform.position = posicion;
        objetoSeleccionado = null;
        unidadColocada = true; // Marca que la unidad ha sido colocada
    }

    private Vector3 ObtenerPosicionMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, mapaLayer))
        {
            Vector3 posicion = hitInfo.point;
            float x = Mathf.Round((posicion.x - posicionOffset.x) / tamanioCelda) * tamanioCelda + posicionOffset.x;
            float z = Mathf.Round((posicion.z - posicionOffset.z) / tamanioCelda) * tamanioCelda + posicionOffset.z;

            return new Vector3(x, posicion.y + posicionOffset.y, z);
        }

        return Vector3.zero;
    }
}

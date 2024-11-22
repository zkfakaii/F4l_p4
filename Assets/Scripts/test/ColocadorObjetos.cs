using UnityEngine;

public class ColocadorObjetos : MonoBehaviour
{
    public LayerMask mapaLayer; // Capa que representa el plano del mapa.
    public Vector3 posicionOffset = Vector3.zero; // Offset para ajustar la posición final.
    public float tamanioCelda = 1f; // Tamaño de las celdas de la grilla.
    public int anchoGrilla = 10; // Número de casillas en el eje X.
    public int altoGrilla = 10; // Número de casillas en el eje Z.

    private GameObject objetoSeleccionado;

    void Update()
    {
        if (objetoSeleccionado != null)
        {
            // Mueve el objeto con el mouse.
            Vector3 posicionMouse = ObtenerPosicionMouse();
            if (posicionMouse != Vector3.zero)
            {
                objetoSeleccionado.transform.position = posicionMouse;

                // Coloca el objeto al hacer clic izquierdo.
                if (Input.GetMouseButtonDown(0))
                {
                    ColocarObjeto(posicionMouse);
                }
            }
        }
    }

    public void SeleccionarObjeto(GameObject prefab)
    {
        // Instanciar el objeto y prepararlo para colocarlo.
        objetoSeleccionado = Instantiate(prefab);
    }

    private Vector3 ObtenerPosicionMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, mapaLayer))
        {
            Vector3 posicion = hitInfo.point;

            // Ajustar la posición al grid.
            float x = Mathf.Round((posicion.x - posicionOffset.x) / tamanioCelda) * tamanioCelda + posicionOffset.x;
            float z = Mathf.Round((posicion.z - posicionOffset.z) / tamanioCelda) * tamanioCelda + posicionOffset.z;

            return new Vector3(x, posicion.y + posicionOffset.y, z); // Ajustar la posición y el offset.
        }

        return Vector3.zero;
    }

    private void ColocarObjeto(Vector3 posicion)
    {
        // Fija la posición final del objeto y lo libera del control.
        objetoSeleccionado.transform.position = posicion;
        objetoSeleccionado = null;
    }

    // Método para dibujar la grilla con Gizmos.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int x = 0; x <= anchoGrilla; x++)
        {
            for (int z = 0; z <= altoGrilla; z++)
            {
                // Calcula la posición de cada casilla.
                Vector3 casillaPos = new Vector3(
                    x * tamanioCelda + posicionOffset.x,
                    posicionOffset.y,
                    z * tamanioCelda + posicionOffset.z
                );

                // Dibuja un cubo transparente en cada casilla.
                Gizmos.DrawWireCube(casillaPos, new Vector3(tamanioCelda, 0.01f, tamanioCelda));
            }
        }
    }
}

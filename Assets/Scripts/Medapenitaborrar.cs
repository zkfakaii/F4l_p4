using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medapenitaborrar : MonoBehaviour

{
    
    /*
    private Camera cam;
    private bool estaMoviendo = false;
    private GameObject objetoEnMovimiento;
    [SerializeField] GameObject positionPlane;
    [SerializeField] Vector3 positionOffset;
    [SerializeField] LayerMask lm;
    [SerializeField] List<Vector3> lanes = new List<Vector3>();
    [SerializeField] int totalLanesx = 5;
    [SerializeField] int totalLanesz = 3;
    [SerializeField] float laneUnits = 3;
    [SerializeField] float initialLane = 0;

    void Start()
    {
        cam = Camera.main;  // Obtener la c�mara principal
    }

    // M�todo que se llamar� cuando se haga clic en el bot�n
    public void IniciarMovimiento(GameObject objeto)
    {
        objetoEnMovimiento = objeto;
        estaMoviendo = true;
        objetoEnMovimiento.SetActive(true);  // Activamos el objeto
    }

    void Update()
    {
        // Si estamos moviendo el objeto, actualizamos su posici�n
        if (estaMoviendo && objetoEnMovimiento != null)
        {
            // Actualizamos la posici�n del objeto al rat�n
            Vector3 mousePos = GetMouseWorldPos();

            print(mousePos);
            objetoEnMovimiento.transform.position = mousePos;

            // Cuando se hace clic en el mapa, el objeto se queda en la posici�n
            if (Input.GetMouseButtonDown(0))  // Bot�n izquierdo del rat�n
            {
                // Detener el movimiento y fijar la posici�n
                estaMoviendo = false;

                // (Opcional) Desactivar cualquier funcionalidad adicional, si es necesario.
                // Por ejemplo, podr�as a�adir un script que haga que el objeto se convierta en interactivo
            }
        }
    }

    // Convierte la posici�n del rat�n en coordenadas del mundo
    private Vector3 GetMouseWorldPos()
    {
        Vector3 worldPosition = Vector3.zero;
        float distance = 1000;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        Physics.Raycast(ray, out info, distance, lm);

        if (info.transform != null)
        {
            worldPosition = info.point;
        }

        int xValue = Mathf.RoundToInt(worldPosition.x);
        int zValue = Mathf.RoundToInt(worldPosition.z);

        for (int i = 0; i < totalLanesx; i++)
        {
            for (int j = 0; j < totalLanesz; j++)
            {

            }
        }

        int finalX = xValue;
        int finalZ = zValue;

        worldPosition = new Vector3(finalX, worldPosition.y, finalZ) + positionOffset;
        return worldPosition;
    }*/
}


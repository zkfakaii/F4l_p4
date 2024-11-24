using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DibujarGrilla : MonoBehaviour
{
    public int rows = 5;
    public int columns = 5;
    public float cellSize = 1f;
    public Material lineMaterial;

    private void Start()
    {
        DrawGrid();
    }

    void DrawGrid()
    {
        for (int i = 0; i <= rows; i++)
        {
            Vector3 start = new Vector3(0, 0, i * cellSize);
            Vector3 end = new Vector3(columns * cellSize, 0, i * cellSize);
            CreateLine(start, end);
        }

        // Crear las líneas verticales
        for (int i = 0; i <= columns; i++)
        {
            Vector3 start = new Vector3(i * cellSize, 0, 0);
            Vector3 end = new Vector3(i * cellSize, 0, rows * cellSize);
            CreateLine(start, end);
        }
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject line = new GameObject("Line");
        LineRenderer lr = line.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.material = lineMaterial;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Pathfinding : MonoBehaviour
{
    public Color GridColor = Color.magenta;
    public Vector2Int GridSize;

    private float cellHeight;
    private float cellWidth;
    private float gridHeight;
    private float gridWidth;
    private Grid grid;
    private Vector3[,] gridCellCenterMap;
    private Vector3 gridPerimeterX;
    private Vector3 gridPerimeterY;
    private Vector3 gridPerimeterXY;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        GenerateGridProperties();
    }

    private void OnValidate()
    {
        grid = FindObjectOfType<Grid>();
        GenerateGridProperties();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, gridPerimeterX, GridColor);
        Debug.DrawLine(transform.position, gridPerimeterY, GridColor);
        Debug.DrawLine(gridPerimeterX, gridPerimeterXY, GridColor);
        Debug.DrawLine(gridPerimeterY, gridPerimeterXY, GridColor);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GridColor;
        for (var x = 0; x < GridSize.x; x++)
        {
            for (var y = 0; y < GridSize.y; y++)
            {
                Gizmos.DrawSphere(gridCellCenterMap[x, y], 0.1f);
            }
        }
    }

    private void GenerateGridProperties()
    {
        cellHeight = grid.cellSize.y + grid.cellGap.y;
        cellWidth = grid.cellSize.x + grid.cellGap.x;
        gridHeight = GridSize.y * cellHeight;
        gridWidth = GridSize.x * cellWidth;
        gridPerimeterX = transform.position + new Vector3(gridWidth, 0, 0);
        gridPerimeterY = transform.position + new Vector3(0, gridHeight, 0);
        gridPerimeterXY = transform.position + new Vector3(gridWidth, gridHeight, 0);
        gridCellCenterMap = new Vector3[GridSize.x, GridSize.y];
        for (var x = 0; x < GridSize.x; x++)
        {
            for (var y = 0; y < GridSize.y; y++)
            {
                var cellPosition = transform.position + new Vector3(x * cellWidth, y * cellHeight, 0);
                var cell = grid.WorldToCell(cellPosition);
                var cellCenter = grid.GetCellCenterWorld(cell);
                gridCellCenterMap[x, y] = cellCenter;
            }
        }
    }
}

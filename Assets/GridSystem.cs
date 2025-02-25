using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 2f;
    [SerializeField] private Transform debugPrefab;

    private GridObject[,] gridArray;

    void Start()
    {
        InitializeGrid();
        CreateDebugObjects();
    }

    void InitializeGrid()
    {
        // Initialize the grid and assign objects to each cell
        gridArray = new GridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridArray[x, z] = new GridObject(gridPosition);
            }
        }
    }

    void CreateDebugObjects()
    {
        // Instantiate debug objects to visualize the grid
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform debugTransform = Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject debugObject = debugTransform.GetComponent<GridDebugObject>();
                debugObject.SetGridObject(gridArray[x, z]);
            }
        }
    }
    
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        // Converts grid coordinates to world position
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        // Converts world position to grid coordinates
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }
}


// Stores Grid Coordinates
public struct GridPosition
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return $"x: {x}, z: {z}";
    }
}

// Represents an object stored in a grid cell
public class GridObject
{
    private GridPosition gridPosition;

    public GridObject(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }
}




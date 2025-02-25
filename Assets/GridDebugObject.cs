using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Handles Debug Visualization of Grid Cells
public class GridDebugObject : MonoBehaviour
{
    private GridObject gridObject;
    [SerializeField] private TextMeshPro textMeshPro;

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    private void Update()
    {
        // Display the grid position as text
        textMeshPro.text = gridObject.ToString();
    }
}
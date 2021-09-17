using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapGenericVisuals : MonoBehaviour
{
    private GridCustom<bool> grid;
    private Mesh mesh;
    private bool updateMesh;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(GridCustom<bool> grid)
    {
        this.grid = grid;
        UpdateHeatMapVisual();

//        grid.OnGridObjectChanged += Grid_onGridValueChanged;
    }

    private void Grid_OnGridValueChanged(object sender, GridCustom<bool>.OnGridObjectChangedEventArgs e)
    {
        updateMesh = true;
    }

    private void LateUpdate()
    {
        if (updateMesh)
        {
            updateMesh = false;
            UpdateHeatMapVisual();
        }
    }

    private void UpdateHeatMapVisual()
    {
    }
}

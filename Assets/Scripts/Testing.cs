using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private GridCustom<HeatMapGridObject> grid;
    
    // Start is called before the first frame update
    void Start()
    {
        grid = new GridCustom<HeatMapGridObject>(10, 10, 10f, new Vector3(0,0,0), (GridCustom<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = GetMousePosition();
            //            grid.SetValue(worldPos, true);
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(worldPos);

            if (heatMapGridObject != null)
            {
                heatMapGridObject.AddValue(5);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetGridObject(GetMousePosition()));
            
        }
    }
    

    public Vector3 GetMousePosition()
    {
        Plane plane = new Plane(Vector3.up, 0);
        Vector3 worldPos;
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out distance))
        {
            worldPos = ray.GetPoint(distance);
            return worldPos;
        }
        return new Vector3(-100, -100, -100);
    }
}

public class HeatMapGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;

    private GridCustom<HeatMapGridObject> grid;
    private int x;
    private int y;
    private int value;

    public HeatMapGridObject(GridCustom<HeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();   
    }
}
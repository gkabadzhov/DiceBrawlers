using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing_v2 : MonoBehaviour
{
    [SerializeField] private CharacterPathfindingHandler characterPathfinding;
    private Pathfinding pathfinding;

    private void Start()
    {
        pathfinding = new Pathfinding(10, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition3D();
            Debug.Log(pathfinding.GetGrid().GetGridObject(mouseWorldPosition));
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i< path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, 0, path[i].y) * 10f + Vector3.one * 5f,
                                    new Vector3(path[i+1].x, 0, path[i+1].y) * 10f + Vector3.one * 5f, Color.green);

                }
            }

            characterPathfinding.SetTargetPosition(mouseWorldPosition);
       
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition3D();
            pathfinding.UpdateWalkableArea(2, mouseWorldPosition);
        }
    }
}

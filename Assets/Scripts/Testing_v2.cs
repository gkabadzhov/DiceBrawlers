using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing_v2 : MonoBehaviour
{
    [SerializeField] private CharacterPathfindingHandler characterPathfinding;
    [SerializeField] private GridVisuals gridVisual;
    private Pathfinding pathfinding;
    private List<PathNode> walkableArea;
    private bool areaUpdateFlag;
    

    private enum GameState
    {
        AreaUpdate,
        MovementUpdate,
        CombatUpdate
    }

    private GameState state;

    private void Start()
    {
        pathfinding = new Pathfinding(10, 10);
        gridVisual.SetGrid(pathfinding.GetGrid());
        walkableArea = new List<PathNode>();
        areaUpdateFlag = false;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            state = GameState.AreaUpdate;
            Debug.Log("current state is: " + state);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            state = GameState.MovementUpdate;
            Debug.Log("current state is: " + state);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            state = GameState.CombatUpdate;
            Debug.Log("current state is: " + state);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (state == GameState.MovementUpdate)
            {
                Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition3D();
                Debug.Log(pathfinding.GetGrid().GetGridObject(mouseWorldPosition));
                pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

                List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
                if (path != null)
                {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        Debug.DrawLine(new Vector3(path[i].x, 0, path[i].y) * 10f + Vector3.one * 5f,
                                        new Vector3(path[i + 1].x, 0, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green);

                    }
                }

                characterPathfinding.SetTargetPosition(mouseWorldPosition);
            }
            else if (state == GameState.AreaUpdate)
            {
                if (!areaUpdateFlag)
                {
                    Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition3D();
                    walkableArea = pathfinding.GetWalkableArea(6, mouseWorldPosition, 1);
                }
                else
                {
                    pathfinding.UpdateWalkableArea(walkableArea);
                }
            }
       
        }

        if (Input.GetMouseButton(1))
        {
            if (state == GameState.MovementUpdate)
            {

            }
            else if (state == GameState.AreaUpdate)
            {
                Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition3D();
                walkableArea = pathfinding.GetWalkableArea(6, mouseWorldPosition, 1);
            }
            else if (state == GameState.CombatUpdate)
            {

            }
        }

    }
}

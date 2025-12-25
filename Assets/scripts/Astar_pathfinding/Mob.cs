using UnityEngine;
using System.Collections.Generic;

public class Mob : MonoBehaviour
{
    public Transform target;
    public CubeGrid cubeGrid;
    List<Cube> path;

    void Update()
    {
        Move();
    }

    void Move(){
        path=PathFinder.FindPath(cubeGrid.GetCubeFromPos(transform.position),cubeGrid.GetCubeFromPos(target.position),cubeGrid.grid,cubeGrid.gridSize);
        SimplifyPath(path);
    }

    void SimplifyPath(List<Cube> path){
        List<Cube> waypoints=new List<Cube>();
        Vector3 oldRot=Vector3.zero;
        waypoints.Add(path[0]);
        for(int i=1;i<path.Count;i++){
            if(oldRot!=path[i].pos-path[i-1].pos){
                waypoints.Add(path[i]);
            }
            oldRot=path[i].pos-path[i-1].pos;
        }
        waypoints.Add(path[path.Count-1]);
        path=waypoints;
    }

    void OnDrawWithGizmos(){
        foreach(Cube n in path){
            Gizmos.color=Color.black;
            Gizmos.DrawCube(n.pos,new Vector3(cubeGrid.cubeRadius*2-1,cubeGrid.cubeRadius*2-1,cubeGrid.cubeRadius*2-1));
        }
    }
}

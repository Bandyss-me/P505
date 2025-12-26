using UnityEngine;
using System.Collections.Generic;

public class Mob : MonoBehaviour
{
    public Transform target;
    public CubeGrid cubeGrid;
    List<Cube> path;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            Cube seeker=cubeGrid.GetCubeFromPos(transform.position);
            Cube Target=cubeGrid.GetCubeFromPos(target.position);
            Debug.LogWarning("Seeker: "+seeker);
            Debug.LogWarning("Target: "+Target);
            if(seeker!=null)
                Debug.LogWarning("Seeker walkable: "+seeker.walkable);
            if(Target!=null)
                Debug.LogWarning("Target walkable: "+Target.walkable);
            path=PathFinder.FindPath(seeker,Target,cubeGrid.grid,cubeGrid.gridSize);
            Debug.LogWarning("Path Result: "+(path==null?"NULL":"OK"));
        }
    }

    void Move(){

        path=PathFinder.FindPath(cubeGrid.GetCubeFromPos(transform.position),cubeGrid.GetCubeFromPos(target.position),cubeGrid.grid,cubeGrid.gridSize);
        //SimplifyPath(path);
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

    void OnDrawGizmos(){
        foreach(Cube n in path){
            Gizmos.color=Color.green;
            Gizmos.DrawCube(n.pos,new Vector3(cubeGrid.cubeRadius*2-1,cubeGrid.cubeRadius*2-1,cubeGrid.cubeRadius*2-1));
        }
    }
}

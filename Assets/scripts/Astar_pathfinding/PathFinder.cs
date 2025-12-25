using UnityEngine;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour
{
    public static List<Cube> FindPath(Cube seeker,Cube target,Cube[,,] grid,Vector3Int gridSize){
        List<Cube> path=new List<Cube>();
        List<Cube> openSet=new List<Cube>();
        List<Cube> closedSet=new List<Cube>();
        if(seeker.walkable && target.walkable){
            openSet.Add(seeker);
            bool followingPath=true;
            while(followingPath){
                Cube minCube=new Cube(true,Vector3Int.zero,Vector3Int.zero);
                minCube.dis=999999999;
                for(int i=0;i<openSet.Count;i++){
                    for(int x=-1;x<=1;x++){
                        for(int y=-1;y<=1;y++){
                            for(int z=-1;z<=1;z++){
                                int nx=Mathf.Clamp(openSet[i].gridPos.x+x,0,gridSize.x-1);
                                int ny=Mathf.Clamp(openSet[i].gridPos.y+y,0,gridSize.y-1);
                                int nz=Mathf.Clamp(openSet[i].gridPos.z+z,0,gridSize.z-1);
                                Cube neighbour=grid[nx,ny,nz];
                                if(x==0 && y==0 && z==0 && !neighbour.walkable)
                                    continue;
                                neighbour.parents=openSet[i].parents;
                                neighbour.parents.Add(openSet[i]);
                                openSet.Add(neighbour);
                                if(neighbour.dis<minCube.dis)
                                    minCube=neighbour;
                                if(neighbour.pos==target.pos){
                                    target=neighbour;
                                    followingPath=false;
                                    break;
                                }
                            }
                        }
                    }
                    closedSet.Add(openSet[i]);
                }
            }
        }
        target.parents.Add(target);
        return target.parents;
    }
}

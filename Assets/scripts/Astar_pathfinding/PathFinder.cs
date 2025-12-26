using UnityEngine;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour
{
    public static List<Cube> FindPath(Cube seeker,Cube target,Cube[,,] grid,Vector3Int gridSize){
        if(!seeker.walkable || !target.walkable)
            return null;
        foreach(Cube c in grid){
            c.gCost=int.MaxValue;
            c.hCost=0;
            c.parent=null;
        }
        seeker.gCost=0;
        seeker.hCost=Heuristic(seeker,target);
        List<Cube> openSet=new List<Cube>();
        HashSet<Cube> closedSet=new HashSet<Cube>();
        openSet.Add(seeker);
        while(openSet.Count>0){
            Cube current=openSet[0];
            for(int i=1;i<openSet.Count;i++){
                if(openSet[i].fCost<current.fCost || openSet[i].fCost==current.fCost && openSet[i].hCost<current.hCost){
                    current=openSet[i];
                }
            }
            openSet.Remove(current);
            closedSet.Add(current);
            if(current.gridPos==target.gridPos)
                return BuildPath(current);
            foreach(Cube neighbour in GetNeighbours(current,grid,gridSize)){
                if(!neighbour.walkable || closedSet.Contains(neighbour))
                    continue;
                int newCost=current.gCost+MovementCost(current,neighbour);
                if(newCost<neighbour.gCost){
                    neighbour.gCost=newCost;
                    neighbour.hCost=Heuristic(neighbour,target);
                    neighbour.parent=current;
                    if(!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        return null;
    }

    static List<Cube> BuildPath(Cube end){
        List<Cube> path=new List<Cube>();
        Cube current=end;
        while(current!=null){
            path.Add(current);
            current=current.parent;
        }
        path.Reverse();
        return path;
    }

    static int Heuristic(Cube a,Cube b){
        int dx=Mathf.Abs(a.gridPos.x-b.gridPos.x);
        int dy=Mathf.Abs(a.gridPos.y-b.gridPos.y);
        int dz=Mathf.Abs(a.gridPos.z-b.gridPos.z);
        int minXY=Mathf.Min(dx,dy);
        int maxXY=Mathf.Max(dx,dy);
        int diag2D=14*minXY+10*(maxXY-minXY);
        int minXYZ=Mathf.Min(diag2D,dz);
        int maxXYZ=Mathf.Max(diag2D,dz);
        return 17*minXYZ+10*(maxXYZ-minXYZ);
    }

    static List<Cube> GetNeighbours(Cube c,Cube[,,] grid,Vector3Int size){
        List<Cube> list=new List<Cube>();
        int x=c.gridPos.x;
        int y=c.gridPos.y;
        int z=c.gridPos.z;
        for(int dx=-1;dx<=1;dx++){
            for(int dy=-1;dy<=1;dy++){
                for(int dz=-1;dz<=1;dz++){
                    if(dx==0 && dy==0 && dz==0)
                        continue;
                    int nx=x+dx;
                    int ny=y+dy;
                    int nz=z+dz;
                    if(nx>=0 && nx<size.x && ny>=0 && ny<size.y && nz>=0 && nz<size.z){
                        list.Add(grid[nx,ny,nz]);
                    }
                }
            }
        }
        return list;
    }

    static int MovementCost(Cube a,Cube b){
        int dx=Mathf.Abs(a.gridPos.x-b.gridPos.x);
        int dy=Mathf.Abs(a.gridPos.y-b.gridPos.y);
        int dz=Mathf.Abs(a.gridPos.z-b.gridPos.z);
        int axes=dx+dy+dz;
        if(axes==1)
            return 10;
        if(axes==2)
            return 14;
        return 17;
    }
}

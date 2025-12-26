using UnityEngine;
using System.Collections.Generic;

public class Cube
{
    public bool walkable;
    public Vector3 pos;
    public Vector3Int gridPos;
    public int gCost;
    public int hCost;
    public int fCost=>gCost+hCost;
    public Cube parent;

    public Cube(bool _walkable,Vector3 _pos,Vector3Int _gridPos){
        walkable=_walkable;
        pos=_pos;
        gridPos=_gridPos;
    }
}

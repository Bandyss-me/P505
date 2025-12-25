using UnityEngine;
using System.Collections.Generic;

public class Cube
{
    public bool walkable;
    public Vector3 pos;
    public Vector3Int gridPos;
    public int dis;
    public List<Cube> parents;

    public Cube(bool _walkable,Vector3 _pos,Vector3Int _gridPos){
        walkable=_walkable;
        pos=_pos;
        gridPos=_gridPos;
    }
}

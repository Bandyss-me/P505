using UnityEngine;

public class CubeGrid : MonoBehaviour
{
    public Vector3Int gridSize;
    public bool displayGridGizmos;
    public int cubeRadius;
    public Cube[,,] grid;
    void Start(){
        CreateCubeGrid();
    }

    void CreateCubeGrid(){
        grid=new Cube[gridSize.x,gridSize.y,gridSize.z];
        Vector3 posBottomLeft = transform.position - gridSize/cubeRadius;
        for(int x=0;x<gridSize.x;x++){
            for(int y=0;y<gridSize.y;y++){
                for(int z=0;z<gridSize.z;z++){
                    bool walkable;
                    Vector3 worldPos=posBottomLeft+new Vector3(x*cubeRadius*2,y*cubeRadius*2,z*cubeRadius*2);
                    bool insideWall=Physics.CheckBox(worldPos,Vector3.one*(cubeRadius*0.1f));
                    bool touchingSurface=Physics.CheckBox(worldPos,Vector3.one*(cubeRadius));
                    walkable=!insideWall && touchingSurface;
                    grid[x,y,z]=new Cube(walkable,worldPos,new Vector3Int(x,y,z));
                }
            }
        }
    }

    public Cube GetCubeFromPos(Vector3 pos){
        float cubeDiameter=cubeRadius*2;
        Vector3 gridOrigin=transform.position-new Vector3(gridSize.x*cubeDiameter,gridSize.y*cubeDiameter,gridSize.z*cubeDiameter)*0.5f;
        int x=Mathf.FloorToInt((pos.x-gridOrigin.x)/cubeDiameter);
        int y=Mathf.FloorToInt((pos.y-gridOrigin.y)/cubeDiameter);
        int z=Mathf.FloorToInt((pos.z-gridOrigin.z)/cubeDiameter);
        x=Mathf.Clamp(x,0,gridSize.x-1);
        y=Mathf.Clamp(y,0,gridSize.y-1);
        z=Mathf.Clamp(z,0,gridSize.z-1);
        return grid[x,y,z];
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position,gridSize);
        if(grid!=null && displayGridGizmos==true){
            foreach(Cube n in grid){
                if(n.walkable){
                    Gizmos.color=Color.black;
                    Gizmos.DrawCube(n.pos, Vector3.one*(cubeRadius*0.1f));
                }
            }
        }
    }
}

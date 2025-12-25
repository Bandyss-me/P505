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
        Vector3 posBottomLeft=transform.position-gridSize/2;
        for(int x=0;x<gridSize.x;x++){
            for(int y=0;y<gridSize.y;y++){
                for(int z=0;z<gridSize.z;z++){
                    bool walkable;
                    Vector3 worldPos=posBottomLeft+new Vector3(x*cubeRadius*2,y*cubeRadius*2,z*cubeRadius*2);
                    RaycastHit hit;
                    int nr=0;
                    if(Physics.Raycast(new Vector3(worldPos.x,worldPos.y,worldPos.z), Vector3.up, out hit,cubeRadius)){
                        nr++;
                    }
                    if(Physics.Raycast(new Vector3(worldPos.x,worldPos.y,worldPos.z), Vector3.down, out hit,cubeRadius)){
                        nr++;
                    }
                    if(Physics.Raycast(new Vector3(worldPos.x,worldPos.y,worldPos.z), Vector3.left, out hit,cubeRadius)){
                        nr++;
                    }
                    if(Physics.Raycast(new Vector3(worldPos.x,worldPos.y,worldPos.z), Vector3.right, out hit,cubeRadius)){
                        nr++;
                    }
                    if(Physics.Raycast(new Vector3(worldPos.x,worldPos.y,worldPos.z), Vector3.forward, out hit,cubeRadius)){
                        nr++;
                    }
                    if(Physics.Raycast(new Vector3(worldPos.x,worldPos.y,worldPos.z), Vector3.back, out hit,cubeRadius)){
                        nr++;
                    }
                    if(nr==0 || nr==6){
                        walkable=false;
                    }
                    else{
                        walkable=true;
                    }
                    grid[x,y,z]=new Cube(walkable,worldPos,new Vector3Int(x,y,z));
                }
            }
        }
    }

    public Cube GetCubeFromPos(Vector3 pos){
        float percentX=(pos.x+gridSize.x/cubeRadius)/gridSize.x;
        float percentY=(pos.y+gridSize.y/cubeRadius)/gridSize.y;
        float percentZ=(pos.z+gridSize.z/cubeRadius)/gridSize.z;
        percentX=Mathf.Clamp01(percentX);
        percentY=Mathf.Clamp01(percentY);
        percentZ=Mathf.Clamp01(percentZ);
        int x=Mathf.RoundToInt((gridSize.x-1)*percentX);
        int y=Mathf.RoundToInt((gridSize.y-1)*percentY);
        int z=Mathf.RoundToInt((gridSize.z-1)*percentZ);
        return grid[x,y,z];
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position,gridSize);
        if(grid!=null && displayGridGizmos==true){
            foreach(Cube n in grid){
                if(!n.walkable){
                    Gizmos.color=Color.red;
                    Gizmos.DrawCube(n.pos, Vector3.one*(cubeRadius*2f-0.1f));
                }
            }
        }
    }
}

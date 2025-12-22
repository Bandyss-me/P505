using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class cave_generation : MonoBehaviour
{
    public int tunnelCount;
    public int radius;
    public int seed;
    public int density=100;
    public int width=100;
    public int height=100;
    public int depth=100;
    public int smooth_level=5;
    public Material mat;
    float[,,] map;
    float[,,] fillMap;

    void Start(){
        seed=Random.Range(0,999999999);
        GenerateCave();
        CarveTunnels();
        for(int k=0;k<smooth_level;k++){
            SmoothMap();
        }
        fillMap=new float[width,depth,height];
        GetFillVoxels(2,2,height-2);
        Mesh innerMesh,outerMesh;
        innerMesh = MarchingCubes.GenerateMesh(fillMap,0,5f);
        outerMesh = MarchingCubes.GenerateMesh(fillMap,2,5f);
        MarchingCubes.FlipMesh(innerMesh);
        Mesh finalMesh=MarchingCubes.CombineMeshes(outerMesh,innerMesh);
        GetComponent<MeshFilter>().mesh = finalMesh;
        GetComponent<MeshCollider>().sharedMesh = finalMesh;
        GetComponent<MeshRenderer>().material=mat;
    }

    void GenerateCave(){
        map=new float[width,depth,height];
        System.Random rand=new System.Random(seed);
        for(int x=0; x<width; x++){
            for(int y=0; y<depth; y++){
                for(int z=0; z<height; z++){
                    if(rand.Next(100)<density){
                        map[x,y,z]=1;
                    }
                    else{
                        map[x,y,z]=0;
                    }
                }
            }
        }
    }

    void CarveTunnels(){
        System.Random rand=new System.Random(seed);
        for(int t=0;t<tunnelCount;t++){
            for(int i=0;i<=rand.Next(20);i++){
                int x=0,y=0,z=height-1;
                int fx=rand.Next(1,width-radius);
                int fy=rand.Next(1,depth-radius);
                int fz=rand.Next(1,height-radius);
                while(x!=fx || y!=fy || z!=fz){
                    for(int dx=-radius;dx<=radius;dx++){
                        for(int dy=-radius;dy<=radius;dy++){
                            for(int dz=-radius;dz<=radius;dz++){
                                if(dx*dx+dy*dy+dz*dz<=radius*radius){
                                    int nx=Mathf.Clamp(x+dx,1,width-2);
                                    int ny=Mathf.Clamp(y+dy,1,depth-2);
                                    int nz=Mathf.Clamp(z+dz,1,height-2);
                                    map[nx,ny,nz]=0;
                                }
                            }
                        }
                    }
                    if(fx>x)
                        x++;
                    else if(fx<x)
                        x--;
                    if(fy>y)
                        y++;
                    else if(fy<y)
                        y--;
                    if(fz>z)
                        z++;
                    else if(fz<z)
                        z--;
                }
            }
        }
    }

    float GetWallCount(int x,int y,int z){
        float nr=0;
        for(int a=x-1; a<=x+1; a++){
            for(int b=y-1; b<=y+1; b++){
                for(int c=z-1; c<=z+1; c++){
                    if(a==x && b==y && c==z)
                        continue;
                    if(a<0 || a>=width || b<0 || b>=depth || c<0 || c>=height){
                        nr++;
                    }
                    else{
                        nr+=map[a,b,c];
                    }
                }
            }
        }
        return nr;
    }

    void SmoothMap(){
        for(int x=0; x<width; x++){
            for(int y=0; y<depth; y++){
                for(int z=0; z<height; z++){
                    float neighbors=GetWallCount(x,y,z);
                    if(neighbors>15)
                        map[x,y,z]=1;
                    else if(neighbors<10)
                        map[x,y,z]=0;
                }
            }
        }
    }

    void GetFillVoxels(int x,int y,int z){
        Queue<Vector3Int> queue=new Queue<Vector3Int>();
        queue.Enqueue(new Vector3Int(x,y,z));
        while(queue.Count>0){
            Vector3Int pos=queue.Dequeue();
            int a=pos.x;
            int b=pos.y;
            int c=pos.z;
            if(a<=0 || a>=width-1 || b<=0 || b>=depth-1 || c<=0 || c>=height-1)
                continue;
            if(fillMap[a,b,c]!=0)
                continue;
            if(map[a,b,c]==1){
                fillMap[a,b,c]=1;
                continue;
            }
            else{
                fillMap[a,b,c]=-1;
            }
            queue.Enqueue(new Vector3Int(a+1,b,c));
            queue.Enqueue(new Vector3Int(a-1,b,c));
            queue.Enqueue(new Vector3Int(a,b+1,c));
            queue.Enqueue(new Vector3Int(a,b-1,c));
            queue.Enqueue(new Vector3Int(a,b,c+1));
            queue.Enqueue(new Vector3Int(a,b,c-1));
        }
    }
}

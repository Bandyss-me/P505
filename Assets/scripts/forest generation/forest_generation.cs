using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class forest_generation : MonoBehaviour
{
    public GameObject tree_prefab;
    public int xlength=1000;
    public int zlength=1000;
    public int ylength=100;
    public int seed;
    public int smooth_level=5;
    public Material mat;
    float[,,] map;

    void Start(){
        seed=Random.Range(0,999999999);
        GenerateLandscape();
        for(int i=0;i<smooth_level;i++){
            SmoothMap();
        }
        Mesh mesh;
        mesh=MarchingCubes.GenerateMesh(map,0.5f,1f);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshRenderer>().material=mat;
        PutTrees();
    }

    void GenerateLandscape(){
        map=new float[xlength,ylength,zlength];
        float scale=0.01f;
        for(int x=0;x<xlength;x++){
            for(int y=0;y<ylength;y++){
                for(int z=0;z<zlength;z++){
                    float noiseValue=Mathf.PerlinNoise(x*scale,z*scale);
                    if(y<noiseValue*ylength*0.5f)
                        map[x,y,z]=1f;
                    else map[x,y,z]=0f;
                }
            }
        }
    }

    void SmoothMap(){
        float[,,] temp=new float[xlength,ylength,zlength];
        for(int x=0;x<xlength;x++){
            for(int y=0;y<ylength;y++){
                for(int z=0;z<zlength;z++){
                    float sum=0;
                    int nr=0;
                    for(int a=x-1;a<=x+1;a++){
                        for(int b=y-1;b<=y+1;b++){
                            for(int c=z-1;c<=z+1;c++){
                                if(a<0 || a>=xlength || b<0 || b>=ylength || c<0 || c>=zlength)
                                    continue;
                                sum+=map[a,b,c];
                                nr++;
                            }
                        }
                    }
                    temp[x,y,z]=sum/nr;
                }
            }
        }
        map=temp;
    }

    float GetWallCount(int x,int y,int z){
        float nr=0;
        for(int a=x-1; a<=x+1; a++){
            for(int b=y-1; b<=y+1; b++){
                for(int c=z-1; c<=z+1; c++){
                    if(a==x && b==y && c==z)
                        continue;
                    if(a<0 || a>=xlength || b<0 || b>=ylength || c<0 || c>=zlength){
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

    void PutTrees(){
        for(int x=0;x<xlength;x++){
            for(int z=0;z<zlength;z++){
                for(int y=ylength-1;y>0;y--){
                    if(map[x,y,z]==1 && Random.Range(0,250)==2)
                        Instantiate(tree_prefab,new Vector3(x,y,z),Quaternion.identity,transform);
                    if(map[x,y,z]==1)
                        break;
                }
            }
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class forest_generation : MonoBehaviour
{
    public GameObject tree_prefab;
    public int xlength=1000;
    public int zlength=1000;
    public int ylength=100;
    public int medium_height=50;
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
        Debug.Log(map);
        Mesh mesh;
        mesh=MarchingCubes.GenerateMesh(map,0.5f,1f);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshRenderer>().material=mat;
        PutTrees();
    }

    void GenerateLandscape(){
        map=new float[xlength,ylength,zlength];
        float noiseScale=0.008f;
        int octaves=4;
        float persistance=0.5f;
        float lacunarity=2f;
        for(int x=0;x<xlength;x++){
            for(int z=0;z<zlength;z++){
                float noise=0f;
                float amplitude=1f;
                float frequency=1f;
                for(int i=0;i<octaves;i++){
                    float nx=(x+seed) * noiseScale * frequency;
                    float nz=(z+seed) * noiseScale * frequency;
                    noise+=Mathf.PerlinNoise(nx,nz)*amplitude;
                    amplitude*=persistance;
                    frequency*=lacunarity;
                }
                int h=Mathf.RoundToInt(noise*(ylength-1));
                h=Mathf.Clamp(h,1,ylength-1);
                for(int y=0;y<=h;y++){
                    float density=(h-y)/10f;
                    map[x,y,z]=density;
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
                    if(map[x,y-1,z]==1 && Random.Range(0,3)==2)
                        Instantiate(tree_prefab,new Vector3(x,y,z),Quaternion.identity,transform);
                    if(map[x,y-1,z]==1)
                        continue;
                }
            }
        }
    }
}

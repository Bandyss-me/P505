using UnityEngine;

public class cave_generation : MonoBehaviour
{
    public GameObject prefab;
    public int seed;
    public int density;
    public int width=100;
    public int height=100;
    public int depth=100;
    public int smooth_level=5;
    int[,,] map;

    void Start(){
        GenerateCave();
        CarveTunnels();
        for(int k=0;k<smooth_level;k++){
            SmoothMap();
        }
        Fill();
    }

    void GenerateCave(){
        map=new int[width,depth,height];
        System.Random rand=new System.Random(seed);
        for(int x=0; x<width; x++){
            for(int y=0; y<depth; y++){
                for(int z=0; z<height; z++){
                    float axisFactor=Mathf.Abs((float)y-depth/2)/(depth/2);
                    int adjustedDensity=(int)(density+axisFactor*30);
                    if(rand.Next(100)<adjustedDensity){
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

        int tunnelCount=2;
        for(int t=0;t<tunnelCount;t++){
            int x=rand.Next(1, width-2);
            int y=rand.Next(1, depth-2);
            int z=rand.Next(1, height-2);
        }
    }

    int GetWallCount(int x,int y,int z){
        int nr=0;
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
                    int neighbors=GetWallCount(x,y,z);
                    if(neighbors>15)
                        map[x,y,z]=1;
                    else if(neighbors<10)
                        map[x,y,z]=0;
                }
            }
        }
    }

    bool IsSurface(int x,int y,int z){
        if(x+1>=width || map[x+1,y,z]==0 || x-1<0 || map[x-1,y,z]==0 || y+1>=depth || map[x,y+1,z]==0 || y-1<0 || map[x,y-1,z]==0 || z+1>=height || map[x,y,z+1]==0 || z-1<0 || map[x,y,z-1]==0)
            return true;
        return false;
    }

    void Fill(){
        for(int x=0; x<width; x++){
            for(int y=0; y<depth; y++){
                for(int z=0; z<height; z++){
                    if(map[x,y,z]==1 && IsSurface(x,y,z))
                        Instantiate(prefab,new Vector3(x,z,y),Quaternion.identity,transform);
                }
            }
        }
    }
}

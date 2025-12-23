using UnityEngine;

public class spawn_mobs : MonoBehaviour
{
    public GameObject mob;
    public float mob_per_unit;
    public int unit_size;
    public Vector3Int start_pos,fin_pos;

    void SpawnMobs(){
        float sum=0;
        for(int x=start_pos.x;x<=fin_pos.x;x+=unit_size){
            for(int z=start_pos.z;z<=fin_pos.z;z+=unit_size){
                sum+=mob_per_unit;
                for(int y=fin_pos.y;y>=start_pos.y;y+=unit_size){
                    RaycastHit upHit,hit;
                    if(Physics.Raycast(new Vector3(x,y,z),Vector3.up,out upHit,unit_size)==false && Physics.Raycast(new Vector3(x,y,z),Vector3.down,out hit,unit_size)){
                        for(int i=0;i<sum/1;i++){
                            Instantiate(mob, new Vector3(x,y,z), Quaternion.identity, transform);
                        }
                    }
                }
                sum%=1;
            }
        }
    }
}

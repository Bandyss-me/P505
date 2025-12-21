using UnityEngine;

public class Item_LoadMe : MonoBehaviour
{
    public GameObject obj;

    void LoadItems(){
        var data=Save_manager.Instance.data;
        for(int i=0;i<data.tag.Length;i++){
            if(data.tag[i]==gameObject.tag){
                GameObject gb=Instantiate(obj, data.pos[i], Quaternion.Euler(data.rot[i]));
                if(gb.GetComponent<Item_SaveMyData>()==null)
                    gb.AddComponent<Item_SaveMyData>();
            }
        }
    }

    public void Load(){
        LoadItems();
    }
}

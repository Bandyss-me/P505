using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Item_SaveMyData : MonoBehaviour
{
    public void AddItem(){
        var data=Save_manager.Instance.data;
        List<Vector3> posList=new List<Vector3>(data.pos);
        List<Vector3> rotList=new List<Vector3>(data.rot);
        List<string> tagList=new List<string>(data.tag);
        posList.Add(gameObject.transform.position);
        rotList.Add(gameObject.transform.eulerAngles);
        tagList.Add(gameObject.tag);
        data.pos=posList.ToArray();
        data.rot=rotList.ToArray();
        data.tag=tagList.ToArray();
    }
}

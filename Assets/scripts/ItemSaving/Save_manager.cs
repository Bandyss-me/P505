using UnityEngine;
using System.IO;

public class Save_manager : MonoBehaviour
{
    public static Save_manager Instance;
    public Items_data data;
    string items_save_path;

    void Awake(){
        Instance=this;
        items_save_path=Path.Combine(Application.persistentDataPath,"itemsave.json");
        Directory.CreateDirectory(Path.GetDirectoryName(items_save_path));
    }

    void Start(){
        Load();
        Item_LoadMe[] instances=FindObjectsOfType<Item_LoadMe>();
        foreach(Item_LoadMe item in instances){
            item.Load();
        }
    }
    
    void Load(){
        string json=File.ReadAllText(items_save_path);
        data=JsonUtility.FromJson<Items_data>(json);
    }

    void OnApplicationQuit(){
        string json=JsonUtility.ToJson(data,true);
        File.WriteAllText(items_save_path,json);
    }
}

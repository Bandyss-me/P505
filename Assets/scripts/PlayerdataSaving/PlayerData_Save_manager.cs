using UnityEngine;
using System.IO;

public class PlayerData_Save_manager : MonoBehaviour
{
    public static PlayerData_Save_manager Instance;
    public PlayerData data;
    string playerdata_path;

    void Awake(){
        Instance=this;

        playerdata_path=Path.Combine(Application.persistentDataPath,"playerdata_save.json");
        Directory.CreateDirectory(Path.GetDirectoryName(playerdata_path));
    }

    void Start(){
        Load();
    }
    
    void Load(){
        if (File.Exists(playerdata_path)){
            string json = File.ReadAllText(playerdata_path);
            data = JsonUtility.FromJson<PlayerData>(json);
        }
        else{
            data = new PlayerData();
        }
    }

    void OnApplicationQuit(){
        string json=JsonUtility.ToJson(data,true);
        File.WriteAllText(playerdata_path,json);
    }

}

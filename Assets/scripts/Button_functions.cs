using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Button_functions : MonoBehaviour
{
    public GameObject canva;
    public static bool paused;

    void Start(){
        paused=false;
        Time.timeScale=1f;
    }

    public void SaveItems(){
        Clear();
        Item_SaveMyData[] instances=FindObjectsOfType<Item_SaveMyData>();
        Debug.Log(instances.Length);
        foreach(Item_SaveMyData item in instances){
            item.AddItem();
        }
    }

    public void Clear(){
        var data=Save_manager.Instance.data;
        data.pos = new Vector3[0];
        data.rot = new Vector3[0];
        data.tag = new string[0];
    }

    public void Resume(){
        ResumeGame();
    }

    void ResumeGame(){
        paused = false;
        canva.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void PauseGame()
    {
        paused = true;
        canva.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused) ResumeGame();
            else PauseGame();
        }
    }
}

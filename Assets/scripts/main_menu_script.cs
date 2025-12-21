using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;

public class main_menu_script : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if(EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        StartCoroutine(BlockClickForOneFrame());
    }

    IEnumerator BlockClickForOneFrame()
    {
        yield return null; // wait 1 frame
        Input.ResetInputAxes();
    }

    public void Play(){
        SceneManager.LoadScene(1);
    }

    public void Quit(){
        Application.Quit();
    }
}

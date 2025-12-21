using UnityEngine;
using UnityEngine.SceneManagement;

public class button_click3d_script : MonoBehaviour
{
    [SerializeField]
    Transform mainCamera;
    [SerializeField]
    GameObject player;

    void Update(){
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)){
            Click();
        }
    }

    void Click(){
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, 3f)){
            if(hit.collider.gameObject==gameObject){
                DontDestroyOnLoad(player);
                SceneManager.LoadScene("Cave");
            }
        }
    }
}

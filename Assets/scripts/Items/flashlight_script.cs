using UnityEngine;

public class flashlight_script : MonoBehaviour
{
    [SerializeField]
    GameObject light;
    bool on=false;
    public int Arm;
    
    void Update()
    {
        if(Input.GetMouseButtonDown(Arm)){
            if(on==false)
                on=true;
            else on=false;
            light.SetActive(on);
        }
    }
}

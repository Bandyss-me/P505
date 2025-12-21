using UnityEngine;

public class knockback_gun_script : MonoBehaviour
{
    public Transform MainCamera,player;
    public bool Arm;
    int arm;
    Rigidbody rb;

    void OnEnable(){
        rb=player.GetComponent<Rigidbody>();
        if(Arm==false)
            arm=0;
        else arm=1;
    }

    void Update(){
        if(Input.GetMouseButtonDown(arm))
            Shoot();
    }

    void Shoot(){
        RaycastHit hit;
        if(Physics.Raycast(MainCamera.position,MainCamera.forward,out hit,5f))
            rb.linearVelocity+=(player.position-hit.point).normalized*(5f-hit.distance)*7f;
    }
}

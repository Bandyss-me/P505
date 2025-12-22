using UnityEngine;

public class knockback_gun_script : MonoBehaviour
{
    public Transform MainCamera,player;
    public int Arm;
    Rigidbody rb;

    void OnEnable(){
        rb=player.GetComponent<Rigidbody>();
    }

    void Update(){
        if(Input.GetMouseButtonDown(Arm))
            Shoot();
    }

    void Shoot(){
        RaycastHit hit;
        if(Physics.Raycast(MainCamera.position,MainCamera.forward,out hit,5f))
            rb.linearVelocity+=(player.position-hit.point).normalized*(5f-hit.distance)*7f;
    }
}
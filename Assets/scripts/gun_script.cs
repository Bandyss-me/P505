using UnityEngine;

public class gun_script : MonoBehaviour
{
    public GameObject bullet;
    public GameObject gun;
    public Transform MainCamera;
    public bool Arm;
    int arm;

    void OnEnable(){
        if(Arm==false)
            arm=0;
        else arm=1;
    }

    void Update(){
        if(Input.GetMouseButtonDown(arm))
            Shoot();
    }

    void Shoot(){
        GameObject newBullet=Instantiate(bullet, bullet.transform.position+bullet.transform.up, bullet.transform.rotation);
        RaycastHit hit;
        if(Physics.Raycast(MainCamera.position, MainCamera.forward, out hit, Mathf.Infinity)){
            newBullet.transform.LookAt(hit.point);
            newBullet.transform.Rotate(90f,0f,0f,Space.Self);
        }
        newBullet.AddComponent<gun_bullet_script>();
    }
}

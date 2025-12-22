using UnityEngine;

public class player_pickup_script : MonoBehaviour
{
    [SerializeField]
    Transform left_arm,right_arm,mainCamera,Player;
    [SerializeField]
    GameObject Player_gb;
    [SerializeField]
    float pickup_range;

    Rigidbody lheldrb,rheldrb;
    Collider lcol,rcol;
    LayerMask layer;

    void Awake(){
        layer=LayerMask.GetMask("items");
    }

    void ItemEnable(int arm, GameObject obj){
        if(obj.GetComponent<grappler_script>()){
            grappler_script script=obj.GetComponent<grappler_script>();
            script.MainCamera=mainCamera;
            script.player=Player;
            script.Arm=arm;
            script.enabled=true;
        }
        else if(obj.GetComponent<gun_script>()){
            gun_script script=obj.GetComponent<gun_script>();
            script.MainCamera=mainCamera;
            script.Arm=arm;
            script.enabled=true;
        }
        else if(obj.GetComponent<knockback_gun_script>()){
            knockback_gun_script script=obj.GetComponent<knockback_gun_script>();
            script.MainCamera=mainCamera;
            script.player=Player;
            script.Arm=arm;
            script.enabled=true;
        }
        else if(obj.GetComponent<flashlight_script>()){
            flashlight_script script=obj.GetComponent<flashlight_script>();
            script.Arm=arm;
            script.enabled=true;
        }
        else if(obj.GetComponent<speed_potion_script>()){
            speed_potion_script script=obj.GetComponent<speed_potion_script>();
            script.player=Player_gb;
            script.Arm=arm;
            script.enabled=true;
        }
        else if(obj.GetComponent<jumpboost_script>()){
            jumpboost_script script=obj.GetComponent<jumpboost_script>();
            script.player=Player_gb;
            script.Arm=arm;
            script.enabled=true;
        }
    }

    void ItemDisable(GameObject obj){
        if(obj.GetComponent<grappler_script>()){
            grappler_script script=obj.GetComponent<grappler_script>();
            script.MainCamera=null;
            script.player=null;
            script.enabled=false;
        }
        else if(obj.GetComponent<gun_script>()){
            gun_script script=obj.GetComponent<gun_script>();
            script.MainCamera=null;
            script.enabled=false;
        }
        else if(obj.GetComponent<knockback_gun_script>()){
            knockback_gun_script script=obj.GetComponent<knockback_gun_script>();
            script.MainCamera=null;
            script.player=null;
            script.enabled=false;
        }
        else if(obj.GetComponent<flashlight_script>()){
            flashlight_script script=obj.GetComponent<flashlight_script>();
            script.enabled=false;
        }
        else if(obj.GetComponent<speed_potion_script>()){
            speed_potion_script script=obj.GetComponent<speed_potion_script>();
            script.player=null;
            script.enabled=false;
        }
        else if(obj.GetComponent<jumpboost_script>()){
            jumpboost_script script=obj.GetComponent<jumpboost_script>();
            script.player=null;
            script.enabled=false;
        }
    }

    void Update(){
        if(Input.GetMouseButtonDown(0) && lheldrb==null){
            Pickup(0);
        }
        if(Input.GetMouseButtonDown(1) && rheldrb==null){
            Pickup(1);
        }
        if(Input.GetKey(KeyCode.Q) && lheldrb!=null){
            DropObj(0);
        }
        if(Input.GetKey(KeyCode.E) && rheldrb!=null){
            DropObj(1);
        }
    }

    void Pickup(int arm){
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, pickup_range, layer)){
            Rigidbody rb=hit.collider.GetComponent<Rigidbody>();
            Collider col=hit.collider.GetComponent<Collider>();
            if(hit.collider.GetComponent<Item_SaveMyData>())
                Destroy(hit.collider.GetComponent<Item_SaveMyData>());
            ItemEnable(arm,hit.collider.gameObject);
            if(rb){
                if(arm==0){
                    lcol=col;
                    lheldrb=rb;
                    lheldrb.useGravity=false;
                    lheldrb.isKinematic=true;
                    lcol.enabled=false;

                    lheldrb.transform.SetParent(left_arm);
                    lheldrb.transform.localPosition=Vector3.zero;
                    lheldrb.transform.localRotation=Quaternion.identity;
                }
                else if(arm==1){
                    rcol=col;
                    rheldrb=rb;
                    rheldrb.useGravity=false;
                    rheldrb.isKinematic=true;
                    rcol.enabled=false;

                    rheldrb.transform.SetParent(right_arm);
                    rheldrb.transform.localPosition=Vector3.zero;
                    rheldrb.transform.localRotation=Quaternion.identity;
                }
            }
        }
    }

    void DropObj(int arm){
        if(arm==0){
            ItemDisable(lheldrb.gameObject);
            lheldrb.useGravity=true;
            lheldrb.isKinematic=false;
            lheldrb.linearVelocity=left_arm.transform.forward*5f;
            lheldrb.transform.SetParent(null);
            lheldrb=null;
            lcol.enabled=true;
            if(lheldrb.gameObject.GetComponent<Item_SaveMyData>()==null)
                lheldrb.gameObject.AddComponent<Item_SaveMyData>();
        }
        else if(arm==1){
            ItemDisable(rheldrb.gameObject);
            rheldrb.useGravity=true;
            rheldrb.isKinematic=false;
            rheldrb.linearVelocity=right_arm.transform.forward*5f;
            rheldrb.transform.SetParent(null);
            rheldrb=null;
            rcol.enabled=true;
            if(rheldrb.gameObject.GetComponent<Item_SaveMyData>()==null)
                rheldrb.gameObject.AddComponent<Item_SaveMyData>();
        }
    }
}

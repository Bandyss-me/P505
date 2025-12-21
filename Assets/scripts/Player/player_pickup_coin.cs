using UnityEngine;

public class player_pickup_coin : MonoBehaviour
{
    [SerializeField]
    Transform MainCamera;
    [SerializeField]
    float pickup_range;

    void Update(){
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)){
            Pickup_coin();
        }
    }

    void Pickup_coin(){
        RaycastHit hit;
        if(Physics.Raycast(MainCamera.position, MainCamera.forward, out hit, pickup_range)){
            if(hit.collider.gameObject.tag=="coin"){
                var data=PlayerData_Save_manager.Instance.data;
                data.homecoins++;
                Debug.Log("coin");
                Destroy(hit.collider.gameObject);
            }
        }
    }
}

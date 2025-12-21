using UnityEngine;

public class set_price_script : MonoBehaviour
{
    public int price;

    void Update(){
        var data=PlayerData_Save_manager.Instance.data;
        if(data.homecoins>=price){
            gameObject.layer=LayerMask.NameToLayer("items");
        }
        else{
            gameObject.layer=LayerMask.NameToLayer("Default");
        }
    }
}

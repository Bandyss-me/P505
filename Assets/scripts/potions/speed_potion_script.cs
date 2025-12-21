using UnityEngine;
using System.Collections;

public class speed_potion_script : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    float dur, meff,seff;
    public bool Arm;
    int arm;

    void OnEnable(){
        if(Arm==false)
            arm=0;
        else arm=1;
    }

    void Update(){
        if(Input.GetMouseButtonDown(arm))
            Use();
    }

    void Use(){
        player.GetComponent<player_movement>().StartCoroutine(player.GetComponent<player_movement>().AddSpeed(dur, meff, seff));
        Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class jumpboost_script : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    float dur, jeff;
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
        player.GetComponent<player_movement>().StartCoroutine(player.GetComponent<player_movement>().AddJump(dur, jeff));
        Destroy(gameObject);
    }
}

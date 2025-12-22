using UnityEngine;
using System.Collections;

public class jumpboost_script : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    float dur, jeff;
    public int Arm;

    void Update(){
        if(Input.GetMouseButtonDown(Arm))
            Use();
    }

    void Use(){
        player.GetComponent<player_movement>().StartCoroutine(player.GetComponent<player_movement>().AddJump(dur, jeff));
        Destroy(gameObject);
    }
}

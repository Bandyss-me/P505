using UnityEngine;
using System.Collections;

public class speed_potion_script : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    float dur, meff,seff;
    public int Arm;

    void Update(){
        if(Input.GetMouseButtonDown(Arm))
            Use();
    }

    void Use(){
        player.GetComponent<player_movement>().StartCoroutine(player.GetComponent<player_movement>().AddSpeed(dur, meff, seff));
        Destroy(gameObject);
    }
}

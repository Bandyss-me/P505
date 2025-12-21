using UnityEngine;
using UnityEngine.AI;

public class mob_move : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    float seeDistance=30f;
    NavMeshAgent agent;

    void Start(){
        agent=GetComponent<NavMeshAgent>();
    }

    void Update(){
        if(Physics.Raycast(transform.position,transform.position-player.transform.position,out RaycastHit hit, seeDistance)){
            agent.SetDestination(player.transform.position);
        }
    }
}

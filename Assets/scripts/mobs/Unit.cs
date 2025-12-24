using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public Transform target;
    public float speed=30f;
    public float turnDis=5;
    Pathh path;

    void Start(){
        PathRequestManager.RequestPath(transform.position,target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] waypoints,bool pathSuccessful){
        if(pathSuccessful){
            path=new Pathh(waypoints,transform.position,turnDis);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath(){
        while(true){

            yield return null;
        }
    }

    public void OnDrawGizmos(){
        if(path!=null){
            path.DrawWithGizmos();
        }
    }
}

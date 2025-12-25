using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime=0.2f;
    const float pathUpdateMoveThreshold=0.5f;
    public Transform target;
    public float speed=30f;
    public float turnSpeed=3f;
    public float turnDis=5;
    public float stoppingDis=10f;
    Pathh path;

    void Start(){
        StartCoroutine(UpdatePath());
    }

    public void OnPathFound(Vector3[] waypoints,bool pathSuccessful){
        if(pathSuccessful){
            path=new Pathh(waypoints,transform.position,turnDis,stoppingDis);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath(){
        if(Time.timeSinceLevelLoad<0.3f){
            yield return new WaitForSeconds(0.3f);
        }
        PathRequestManager.RequestPath(new PathRequest(transform.position,target.position, OnPathFound));
        float sqrMoveTreshold=pathUpdateMoveThreshold*pathUpdateMoveThreshold;
        Vector3 targetPosOld=target.position;
        while(true){
            yield return new WaitForSeconds(minPathUpdateTime);
            if((target.position-targetPosOld).sqrMagnitude>sqrMoveTreshold){
                PathRequestManager.RequestPath(new PathRequest(transform.position,target.position, OnPathFound));
                targetPosOld=target.position;
            }
        }
    }

    IEnumerator FollowPath(){
        bool followingPath=true;
        int pathIndex=0;
        transform.LookAt(path.lookPoints[0]);
        float speedPercent=1f;
        while(followingPath){
            Vector3 pos2d=new Vector2(transform.position.x,transform.position.z);
            while(path.turnBoundaries[pathIndex].HasCrossedLine(pos2d)){
                if(pathIndex==path.finishLineIndex){
                    followingPath=false;
                    break;
                }
                else{
                    pathIndex++;
                }
            }
            if(followingPath){
                if(pathIndex>=path.slowDownIndex && stoppingDis>0){
                    speedPercent=Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2d)/stoppingDis);
                    if(speedPercent>0.3f){
                        //followingPath=false;
                    }
                }
                Quaternion targetRotation=Quaternion.LookRotation(path.lookPoints[pathIndex]-transform.position);
                transform.rotation=Quaternion.Lerp(transform.rotation,targetRotation,Time.deltaTime*turnSpeed);
                transform.Translate(Vector3.forward*Time.deltaTime*speed*speedPercent,Space.Self);
            }
            yield return null;
        }
    }

    public void OnDrawGizmos(){
        if(path!=null){
            path.DrawWithGizmos();
        }
    }
}
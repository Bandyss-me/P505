using UnityEngine;

public class grappler_script : MonoBehaviour
{
    LineRenderer lr;
    Vector3 gunpoint;
    public LayerMask grapple_layer;
    public Transform guntip, MainCamera, player;
    public float maxDistance;
    public bool Arm;
    SpringJoint joint;
    int arm;

    void OnEnable(){
        lr=GetComponent<LineRenderer>();
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        if(Arm==false)
            arm=0;
        else arm=1;
    }

    void OnDisable(){
        StopGrapple();
    }

    void Update(){
        if(Input.GetMouseButtonDown(arm))
            StarGrapple();
        if(Input.GetMouseButtonUp(arm))
            StopGrapple();
    }

    void LateUpdate(){
        DrawRope();
    }

    void StarGrapple(){
        RaycastHit hit;
        if(Physics.Raycast(MainCamera.position, MainCamera.forward, out hit, Mathf.Infinity)){
            gunpoint=hit.point;
            joint=player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor=false;
            joint.connectedAnchor=gunpoint;

            float distanceFromGun=Vector3.Distance(player.position,gunpoint);
            joint.minDistance=0.01f;
            joint.maxDistance=1f;

            joint.spring=6f;
            joint.damper=2.5f;
            joint.massScale=0.5f;

            lr.positionCount=2;
        }
    }

    void StopGrapple(){
        lr.positionCount=0;
        Destroy(joint);
    }

    void DrawRope(){
        if(!joint) return;
        lr.SetPosition(0, guntip.position);
        lr.SetPosition(1, gunpoint);
    }
}
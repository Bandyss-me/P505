using UnityEngine;

public class gun_bullet_script : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb=gameObject.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.useGravity=false;
        rb.linearVelocity=transform.up*600f;
    }

    void OnCollisionEnter(Collision col){
        Destroy(gameObject);
    }
}

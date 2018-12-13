using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePart : MonoBehaviour
{
    public Rigidbody TheRigidBody;
    private bool hitTheGround, destroyed;
    
	void Start ()
	{
	    TheRigidBody = GetComponent<Rigidbody>();
	}
	
	void Update () {
	    if (hitTheGround && !destroyed)
	    {
            Destroy(gameObject, 0.5f);
	    }
	}

    void OnCollisionEnter(Collision collision)
    {
        MeshCollider mc = collision.collider as MeshCollider;
        if (mc != null)
        {
            hitTheGround = true;
        }
    }

}

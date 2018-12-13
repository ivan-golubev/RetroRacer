using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float ThrustMultiplier = 10.0f;

    private Vector3 direction;
    private bool directionSet;

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        transform.rotation = Quaternion.FromToRotation(transform.forward, direction);
        directionSet = true;
    }

    void FixedUpdate ()
    {
        if (directionSet)
	    {
            rigidBody.AddForce(direction * ThrustMultiplier);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float ThrustMultiplier = 10.0f;
    [SerializeField] private float LifetimeSec = 3.0f;

    private Vector3 direction;
    private bool directionSet;
    private float timeElapsed;

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        transform.rotation = Quaternion.FromToRotation(transform.forward, direction);
        directionSet = true;
    }

    void Update()
    {
        if (directionSet)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= LifetimeSec)
            {
                Destroy(gameObject);
            }
        }
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
        Obstacle o = collision.gameObject.GetComponentInParent<Obstacle>();
        if (o != null)
        {
            o.TakeLaserDamage(collision.contacts[0]);
        }
    }

}

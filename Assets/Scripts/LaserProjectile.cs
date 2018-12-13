using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float ThrustMultiplier = 10.0f;
	
	void FixedUpdate () {
        // TODO: the model should be facing the correct direction
	    rigidBody.AddForce(Vector3.back * ThrustMultiplier);
    }
}

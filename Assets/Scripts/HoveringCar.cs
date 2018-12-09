using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringCar : MonoBehaviour {

    [SerializeField] private Rigidbody CarRigidBody;
    [SerializeField] private Transform[] HowerPoints;

    [SerializeField] private float ForwardAcceleration = 100.0f;
    [SerializeField] private float BackwardAcceleration = 25.0f;
    [SerializeField] private float TurnStrength = 10.0f;

    [SerializeField] private float HowerHeight = 2.0f;
    [SerializeField] private float HowerForce = 10.0f;

    private const float threshold = 0.1f;
    private float thrust, turn;
    private LayerMask hoverLayerMask;

    void Start() {
        hoverLayerMask = LayerMask.GetMask("RacingTrack");
    }

    void Update()
    {
        thrust = 0.0f;
        float verticalInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(verticalInput) > threshold)
        {
            thrust = verticalInput * (verticalInput > 0 ? ForwardAcceleration : BackwardAcceleration);
        }

        turn = 0.0f;
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalInput) > threshold)
        {
            turn = horizontalInput * TurnStrength;
        }
    }

    void FixedUpdate()
    {
        Hover();
        /* forward */
        if (Mathf.Abs(thrust) > 0)
        {
            CarRigidBody.AddForce(transform.forward * thrust);
        }
        /* turn */
        if (Mathf.Abs(turn) > 0)
        {
            CarRigidBody.AddRelativeTorque(Vector3.up * turn);
        }
    }

    private void Hover()
    {
        foreach (var hoverPoint in HowerPoints)
        {
            RaycastHit hit;
            if (Physics.Raycast(hoverPoint.position, Vector3.down, out hit, HowerHeight, hoverLayerMask))
            {
                CarRigidBody.AddForceAtPosition(
                    Vector3.up * HowerForce * (1.0f - (hit.distance / HowerHeight)),
                    hoverPoint.position
                );
            }
            else {
                int sign = transform.position.y > hoverPoint.position.y ? 1 : -1;
                CarRigidBody.AddForceAtPosition(
                    hoverPoint.up * sign * HowerForce,
                    hoverPoint.position
                );
            }
        }
    }

}

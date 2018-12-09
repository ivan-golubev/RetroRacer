using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    [SerializeField] private WheelCollider frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;
    [SerializeField] private Transform frontLeftWheelT, frontRightWheelT, rearLeftWheelT, rearRightWheelT;

    [SerializeField] private Rigidbody CarRigidBody;
    [SerializeField] private Transform CenterOfMass;

    [SerializeField] private float MaxSteeringAngle = 30.0f;
    [SerializeField] private float MotorForce = 50.0f;

    private float verticalInput, horizontalInput;

    private void Start()
    {
        CarRigidBody.centerOfMass = CenterOfMass.transform.localPosition;
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {        
        Steer();
        Accelerate();
        RotateWheels();
    }

    private void Steer()
    {
        if (frontLeftWheel != null) { frontLeftWheel.steerAngle = horizontalInput * MaxSteeringAngle; }
        if (frontRightWheel != null) { frontRightWheel.steerAngle = horizontalInput * MaxSteeringAngle; }
    }

    private void Accelerate()
    {
        if (frontLeftWheel != null) { frontLeftWheel.motorTorque += verticalInput * MotorForce; }
        if (frontRightWheel != null) { frontRightWheel.motorTorque += verticalInput * MotorForce; }
        if (rearRightWheel != null) { rearRightWheel.motorTorque += verticalInput * MotorForce; }
        if (rearLeftWheel != null) { rearLeftWheel.motorTorque += verticalInput * MotorForce; }
    }

    private void RotateWheels()
    {
        UpdateWheelPos(frontLeftWheel, frontLeftWheelT);
        UpdateWheelPos(frontRightWheel, frontRightWheelT);
        UpdateWheelPos(rearLeftWheel, rearLeftWheelT);
        UpdateWheelPos(rearRightWheel, rearRightWheelT);
    }

    private void UpdateWheelPos(WheelCollider c, Transform t)
    {
        if (c == null || t == null) { return; }
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        c.GetWorldPose(out pos, out rot);

        t.position = pos;
        t.rotation = rot;
    }

}

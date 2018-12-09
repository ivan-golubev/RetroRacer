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
    [SerializeField] private float zRotationLimit = 45.0f;

    private void Start()
    {
        CarRigidBody.centerOfMass = CenterOfMass.transform.localPosition;
    }

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Steer(horizontalInput);
        Accelerate(verticalInput);
        RotateWheels();
        //PreventFlippingOver();
    }

    private void Steer(float horizontalInput)
    {
        if (frontLeftWheel != null) { frontLeftWheel.steerAngle = horizontalInput * MaxSteeringAngle; }
        if (frontRightWheel != null) { frontRightWheel.steerAngle = horizontalInput * MaxSteeringAngle; }
    }

    private void Accelerate(float verticalInput)
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

    private void PreventFlippingOver()
    {
        Vector3 localRot = transform.localEulerAngles;
        if (Mathf.Abs(localRot.z) > zRotationLimit)
        {
            transform.localEulerAngles = new Vector3(localRot.x, localRot.y, 0);
        }
    }

}

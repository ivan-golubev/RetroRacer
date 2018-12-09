using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    [SerializeField] private WheelCollider frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;
    [SerializeField] private Transform frontLeftWheelT, frontRightWheelT, rearLeftWheelT, rearRightWheelT;

    [SerializeField] private float MaxSteeringAngle = 30.0f;
    [SerializeField] private float MotorForce = 50.0f;

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            Steer(horizontalInput);
        }
        if (verticalInput != 0)
        {
            Accelerate(verticalInput);
        }
        RotateWheels();
    }

    private void Steer(float horizontalInput)
    {
        if (frontLeftWheel != null) { frontLeftWheel.steerAngle = horizontalInput * MaxSteeringAngle; }
        if (frontRightWheel != null) { frontRightWheel.steerAngle = horizontalInput * MaxSteeringAngle; }
    }

    private void Accelerate(float verticalInput)
    {
        if (frontLeftWheel != null) { frontLeftWheel.motorTorque = verticalInput * MotorForce; }
        if (frontRightWheel != null) { frontRightWheel.motorTorque = verticalInput * MotorForce; }
        if (rearRightWheel != null) { rearRightWheel.motorTorque = verticalInput * MotorForce; }
        if (rearLeftWheel != null) { rearLeftWheel.motorTorque = verticalInput * MotorForce; }
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

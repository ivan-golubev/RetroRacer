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
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");
        frontLeftWheel.steerAngle = frontRightWheel.steerAngle = horizontalInput * MaxSteeringAngle;
        frontLeftWheel.motorTorque = frontRightWheel.motorTorque = verticalInput * MotorForce;
        rearRightWheel.motorTorque = rearLeftWheel.motorTorque = verticalInput * MotorForce;

        UpdateWheelPos(frontLeftWheel, frontLeftWheelT);
        UpdateWheelPos(frontRightWheel, frontRightWheelT);
        UpdateWheelPos(rearLeftWheel, rearLeftWheelT);
        UpdateWheelPos(rearRightWheel, rearRightWheelT);
    }

    private void UpdateWheelPos(WheelCollider c, Transform t)
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        c.GetWorldPose(out pos, out rot);

        t.position = pos;
        t.rotation = rot;
    }

}

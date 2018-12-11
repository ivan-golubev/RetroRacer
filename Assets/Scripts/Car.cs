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
    [SerializeField] private float DownwardForce = 10.0f;

    [SerializeField] private Vector3 Velocity;

    private float verticalInput, horizontalInput;

    void Start()
    {
        CarRigidBody.centerOfMass = CenterOfMass.transform.localPosition;
        StartCoroutine(DropCar());
    }

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {        
        Steer();
        Accelerate();
        RotateWheels();
        Velocity = CarRigidBody.velocity;
        //ControlDamping();
        ApplyDownwardForce();
    }

    private void Steer()
    {
        if (frontLeftWheel != null) { frontLeftWheel.steerAngle = horizontalInput * MaxSteeringAngle; }
        if (frontRightWheel != null) { frontRightWheel.steerAngle = horizontalInput * MaxSteeringAngle; }
    }

    private void Accelerate()
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

    private void ControlDamping()
    {
        float minDamping = 10.0f;
        float maxDamping = 90.0f;
        float p = (1 - verticalInput);
        if (frontLeftWheel != null) { frontLeftWheel.wheelDampingRate = minDamping + p * maxDamping; }
        if (frontRightWheel != null) { frontRightWheel.motorTorque = minDamping + p * maxDamping; }
        if (rearRightWheel != null) { rearRightWheel.motorTorque = minDamping + p * maxDamping; }
        if (rearLeftWheel != null) { rearLeftWheel.motorTorque = minDamping + p * maxDamping; }
    }

    private void ApplyDownwardForce() {
        CarRigidBody.AddForce(-transform.up * DownwardForce);
    }

    IEnumerator DropCar() {
        float mass = CarRigidBody.mass;
        CarRigidBody.mass = 100000.0f;
        yield return new WaitForSeconds(1.0f);
        CarRigidBody.mass = mass;
    }

}

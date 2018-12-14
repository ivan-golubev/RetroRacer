using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    [SerializeField] private WheelCollider frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;
    [SerializeField] private Transform frontLeftWheelT, frontRightWheelT, rearLeftWheelT, rearRightWheelT;

    [SerializeField] private Rigidbody CarRigidBody;
    [SerializeField] private Transform CenterOfMass;
    public Transform CameraPosition;

    [SerializeField] private float MaxSteeringAngle = 30.0f;
    [SerializeField] private float MotorForce = 50.0f;
    [SerializeField] private float DownwardForce = 10.0f;
    public float CrashSpeed = 100.0f;

    private Joystick joystick;
    private HUD hud;
    private float verticalInput, horizontalInput;
    private float CurrentSpeed;
    private bool Stopped = true;

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        hud = FindObjectOfType<HUD>();
        CarRigidBody.centerOfMass = CenterOfMass.transform.localPosition;
        StartCoroutine(DropCar());
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (joystick != null)
        {
            joystick.gameObject.SetActive(false);
        }
#endif
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
#endif
#if UNITY_IOS || UNITY_ANDROID
        verticalInput = joystick.Vertical;
        horizontalInput = joystick.Horizontal;
#endif
        CurrentSpeed = CarRigidBody.velocity.magnitude * 3.6f;
        hud.SetSpeed(CurrentSpeed);
    }

    void FixedUpdate()
    {        
        Steer();
        Accelerate();
        RotateWheels();
        ApplyDownwardForce();
    }

    public float GetSpeed()
    {
        return CurrentSpeed;
    }

    private void Steer()
    {
        if (Stopped) { return; }
        if (frontLeftWheel != null) { frontLeftWheel.steerAngle = horizontalInput * MaxSteeringAngle; }
        if (frontRightWheel != null) { frontRightWheel.steerAngle = horizontalInput * MaxSteeringAngle; }
    }

    private void Accelerate()
    {
        if (Stopped) { return; }
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

        //if (c.isGrounded) { t.position = pos; }
        t.rotation = rot;
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

    public void StopCar()
    {
        Stopped = true;
    }

    public void StartCar()
    {
        Stopped = false;
        InitialBoost();
    }

    public void InitialBoost()
    {
        verticalInput = 100.0f;
        Accelerate();
    }

    public void Speedup()
    {
        verticalInput = 250.0f;
        Accelerate();
    }

}

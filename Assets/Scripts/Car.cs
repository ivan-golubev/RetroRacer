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
   
    private float verticalInput, horizontalInput;

    void Start()
    {
        CarRigidBody.centerOfMass = CenterOfMass.transform.localPosition;
        StartCoroutine(DropCar());
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_EDITOR
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
#endif
#if UNITY_ANDROID || UNITY_IOS
        ProceeMobileInput();
#endif
    }

#if UNITY_ANDROID || UNITY_IOS
    // taken from https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/adding-mobile-controls
    private Vector2 touchOrigin = -Vector2.one; //Used to store location of screen touch origin for mobile controls.
    private void ProceeMobileInput()
    {
        //Check if Input has registered more than zero touches
        if (Input.touchCount > 0)
        {
            //Store the first touch detected.
            Touch myTouch = Input.touches[0];

            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began)
            {
                //If so, set touchOrigin to the position of that touch
                touchOrigin = myTouch.position;
            }

            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                //Set touchEnd to equal the position of this touch
                Vector2 touchEnd = myTouch.position;

                //Calculate the difference between the beginning and end of the touch on the x axis.
                float x = touchEnd.x - touchOrigin.x;

                //Calculate the difference between the beginning and end of the touch on the y axis.
                float y = touchEnd.y - touchOrigin.y;

                //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                touchOrigin.x = -1;

                //Check if the difference along the x axis is greater than the difference along the y axis.
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                    horizontalInput = x > 0 ? 1 : -1;
                else
                    //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                    verticalInput = y > 0 ? 1 : -1;
            }
        }        
    }
#endif

    void FixedUpdate()
    {        
        Steer();
        Accelerate();
        RotateWheels();
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

}

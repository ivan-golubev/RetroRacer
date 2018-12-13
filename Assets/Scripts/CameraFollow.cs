
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    [SerializeField] private float smoothspeed = 0.125f;
    [SerializeField] private float rotationSmoothSpeed = 0.1f;
    [SerializeField] private Vector3 offset;

    private Transform target;

    void Start()
    {
        target = FindObjectOfType<Car>().CameraPosition;
    }


    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, smoothspeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSmoothSpeed);        
    }

}

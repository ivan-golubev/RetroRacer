
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    [SerializeField] private float smoothspeed = 0.125f;
    [SerializeField] private float rotationSmoothSpeed = 0.1f;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, smoothspeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSmoothSpeed);        
    }

}

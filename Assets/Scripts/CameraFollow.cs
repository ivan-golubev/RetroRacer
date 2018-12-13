
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    [SerializeField] private float positionSmoothSpeed = 1f;
    [SerializeField] private float rotationSmoothSpeed = 0.5f;    

    private Transform target;

    void LateUpdate()
    {
        if (target != null)
        {
            if (positionSmoothSpeed >= 1.0f)
            {
                transform.position = target.position;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, target.position, positionSmoothSpeed);
            }
            if (rotationSmoothSpeed >= 1.0f)
            {
                transform.rotation = target.rotation;
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSmoothSpeed);
            }

        }
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }
}

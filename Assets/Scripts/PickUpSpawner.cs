using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private float respawnTime = 2.0f;    
    [SerializeField] private SpeedboostPickup pickupPrefab;

    private float timer;
    private SpeedboostPickup pickupInstance;

	void Start ()
	{
	    timer = Time.realtimeSinceStartup;
	}
		
	void Update ()
	{
	    float elapsedTime = Time.realtimeSinceStartup - timer;
	    if (pickupInstance == null && elapsedTime > respawnTime)
	    {
	        pickupInstance = Instantiate(pickupPrefab, gameObject.transform, false);
            pickupInstance.transform.localPosition = Vector3.zero;
	        pickupInstance.SetSpawner(this);
	    }
	}

    public void OnPickupDestroyed()
    {
        timer = Time.realtimeSinceStartup;
        pickupInstance = null;
    }
}

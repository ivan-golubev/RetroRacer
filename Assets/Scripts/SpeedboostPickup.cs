using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedboostPickup : MonoBehaviour
{
    private PickUpSpawner spawner;

    public void SetSpawner(PickUpSpawner spawner)
    {
        this.spawner = spawner;
    }

    void OnTriggerEnter(Collider c)
    {
        Car car = c.gameObject.GetComponent<Car>();
        if (car != null)
        {
            spawner.OnPickupDestroyed();
            Destroy(gameObject);
            car.Speedup();            
        }
    }
}

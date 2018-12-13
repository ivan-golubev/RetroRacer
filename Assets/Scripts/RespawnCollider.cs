using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCollider : MonoBehaviour
{
    private GameController gameController;

	void Start ()
	{
	    gameController = FindObjectOfType<GameController>();

	}

    void OnTriggerEnter(Collider c)
    {
        Car car = c.gameObject.GetComponent<Car>();
        if (car != null)
        {        
            gameController.OnCarCrashed(car.GetInstanceID());
        }
    }

}

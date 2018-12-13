using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSideCollider : MonoBehaviour {

    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();

    }

    void OnCollisionEnter(Collision c)
    {
        Car car = c.gameObject.GetComponent<Car>();
        if (car != null && car.GetSpeed() >= car.CrashSpeed)
        {
            gameController.OnCarCrashed(car.GetInstanceID());
        }
    }
}

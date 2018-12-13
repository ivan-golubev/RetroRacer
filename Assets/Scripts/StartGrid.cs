using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGrid : MonoBehaviour
{
    [SerializeField] private float MinTimeBetweenLaps = 10.0f;

    private GameController gameController;
    private float LastLapTimestamp;

	void Start ()
	{
	    gameController = FindObjectOfType<GameController>();
	}

    void OnTriggerEnter(Collider c)
    {
        if (gameController.CurrentLap() == 0 || Time.realtimeSinceStartup - LastLapTimestamp >= MinTimeBetweenLaps)
        {
            /* check that it is the player */
            Car car = c.gameObject.GetComponent<Car>();
            if (car != null)
            {
                LastLapTimestamp = Time.realtimeSinceStartup;
                gameController.IncrementLaps();
            }
        }
    }

}

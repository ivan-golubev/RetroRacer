using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimeLabel;
    [SerializeField] private TextMeshProUGUI SpeedLabel;
    [SerializeField] private TextMeshProUGUI LapsLabel;

    private bool updateTimer;
    private float startTime;

	void Start ()
	{
	    TimeLabel.text = string.Format("{0:D2}:{1:D2}", 0, 0);
        SetSpeed(500);
	}
	
	void Update ()
	{
	    if (updateTimer)
	    {
	        var timeSpan = TimeSpan.FromSeconds(Time.realtimeSinceStartup - startTime);
	        TimeLabel.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
	    }
	}

    public void SetLap(int currentLap, int totalLaps)
    {
        LapsLabel.text = string.Format("{0}/{1} lap", currentLap, totalLaps);
    }

    public void SetSpeed(int kph)
    {
        SpeedLabel.text = string.Format("{0} km/h", kph);
    }

    public void StartTimer(float startTime)
    {
        updateTimer = true;
        this.startTime = startTime;
    }

    public void StopTimer()
    {
        updateTimer = false;
        startTime = 0;
    }

}

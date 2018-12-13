using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int TotalLaps = 3;

    private HUD hud;

    private int currentLap;
    private float gameStartTime;
    
    void Start()
    {
        hud = FindObjectOfType<HUD>();
        hud.SetLap(currentLap, TotalLaps);
    }

    public int CurrentLap()
    {
        return currentLap;
    }

    public void IncrementLaps()
    {
        if (currentLap == TotalLaps)
        {
            return;
        }
        currentLap++;
        hud.SetLap(currentLap, TotalLaps);
        if (currentLap == 1)
        {
            StartTimer();
        } else if (currentLap == TotalLaps)
        {
            hud.StopTimer();
        }
    }

    private void StartTimer()
    {
        gameStartTime = Time.realtimeSinceStartup;
        hud.StartTimer(gameStartTime);
    }

}

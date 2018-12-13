using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Car CarPrefab;
    [SerializeField] private int TotalLaps = 3;   

    private HUD hud;
    private CameraFollow cameraFollow;

    private Car playerCar;
    private int currentLap;
    private float gameStartTime;
    
    void Start()
    {
        hud = FindObjectOfType<HUD>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        hud.SetLap(currentLap, TotalLaps);
        StartGame();
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

    public void StartGame()
    {
        playerCar = Instantiate(CarPrefab, transform.position, transform.rotation);
        cameraFollow.SetTarget(playerCar.CameraPosition);
    }

    private void StartTimer()
    {
        gameStartTime = Time.realtimeSinceStartup;
        hud.StartTimer(gameStartTime);
    }

    public void Respawn()
    {
        playerCar = Instantiate(CarPrefab, transform.position, transform.rotation);
        cameraFollow.SetTarget(playerCar.CameraPosition);
    }

    public void OnCarCrashed(int carInstanceId)
    {
        if (playerCar.GetInstanceID() == carInstanceId)
        {
            cameraFollow.SetTarget(null);
            Destroy(playerCar.gameObject);
            Respawn();
        }
    }

}

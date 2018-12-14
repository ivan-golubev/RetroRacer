using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Car CarPrefab;
    [SerializeField] private int TotalLaps = 3;
    [SerializeField] private GameObject CheckPointsRoot;

    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip[] deathSounds;

    private HUD hud;
    private CameraFollow cameraFollow;

    private Car playerCar;
    private int currentLap;
    private float gameStartTime;

    private Checkpoint[] checkPoints;
    private Checkpoint lastCheckPoint;
    
    void Start()
    {
        hud = FindObjectOfType<HUD>();
        checkPoints = CheckPointsRoot.GetComponentsInChildren<Checkpoint>();
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
        var respawnPos = lastCheckPoint == null ? transform.position : lastCheckPoint.transform.position;
        var respawnRot = lastCheckPoint == null ? transform.rotation : lastCheckPoint.transform.rotation * Quaternion.Euler(0, 180, 0);
        playerCar = Instantiate(CarPrefab, respawnPos, respawnRot);
        cameraFollow.SetTarget(playerCar.CameraPosition);
    }

    public void OnCarCrashed(int carInstanceId)
    {
        if (playerCar.GetInstanceID() == carInstanceId)
        {
            cameraFollow.SetTarget(null);
            Destroy(playerCar.gameObject);
            if (deathSounds.Length > 0)
            {
                audioSrc.clip = deathSounds[Random.Range(0, deathSounds.Length)];
                audioSrc.Play();
            }
            Respawn();
        }
    }

    public void SaveCheckpoint(Checkpoint c)
    {
        this.lastCheckPoint = c;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

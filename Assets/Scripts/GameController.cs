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
    private StartScreen startScreen;
    private EndScreen endScreen;

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
        startScreen = FindObjectOfType<StartScreen>();
        endScreen = FindObjectOfType<EndScreen>();
        hud.SetLap(currentLap, TotalLaps);
        ShowStartScreen();
    }

    public void ShowStartScreen()
    {
        startScreen.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
        hud.gameObject.SetActive(false);
    }

    public void ShowEndScreen()
    {
        startScreen.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(true);
        hud.gameObject.SetActive(false);
    }

    public int CurrentLap()
    {
        return currentLap;
    }

    public void IncrementLaps()
    {
        if (currentLap != TotalLaps)
        {
            currentLap++;
        }
        
        hud.SetLap(currentLap, TotalLaps);
        if (currentLap == 1)
        {
            StartTimer();
        }
        if (currentLap == TotalLaps)
        {
            hud.StopTimer();
            playerCar.StopCar();
            StartCoroutine(CommenceEndCeremony());
        }
    }

    public void StartGame()
    {
        startScreen.gameObject.SetActive(false);
        playerCar = Instantiate(CarPrefab, transform.position, transform.rotation);
        hud.gameObject.SetActive(true);
        cameraFollow.SetTarget(playerCar.CameraPosition);
        StartCoroutine(CommenceStart());
    }

    IEnumerator CommenceStart()
    {
        yield return new WaitForSeconds(1f);
        hud.PlayStartAnimation();
        yield return new WaitForSeconds(1.5f);
        playerCar.StartCar();
    }

    IEnumerator CommenceEndCeremony()
    {
        yield return new WaitForSeconds(2.0f);
        ShowEndScreen();
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
        playerCar.StartCar();
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

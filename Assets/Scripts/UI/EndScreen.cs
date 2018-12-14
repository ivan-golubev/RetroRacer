using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI TotalTimeLabel;
    [SerializeField] private TextMeshProUGUI[] LapsTimeLabels;

    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void RestartPressed()
    {
        gameController.RestartGame();
    }

    public void SetLapTimes(TimeSpan[] lapTimes)
    {
        foreach (var t in LapsTimeLabels)
        {
            t.gameObject.SetActive(false);
        }
        for (int i = 0; i < LapsTimeLabels.Length && i < lapTimes.Length; i++)
        {
            LapsTimeLabels[i].gameObject.SetActive(true);
            LapsTimeLabels[i].text = string.Format("Lap {0} time: {1:D2}:{2:D2}", i+1, lapTimes[i].Minutes, lapTimes[i].Seconds);
        }
        var totalSpan = new TimeSpan(lapTimes.Sum(item => item.Ticks));
        TotalTimeLabel.text = string.Format("Total time: {0:D2}:{1:D2}", totalSpan.Minutes, totalSpan.Seconds);
    }

}

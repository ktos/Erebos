using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimer : MonoBehaviour
{
    public static Action<float> AfterMostRecentScoreSet;
    private Text timeText;
    private float gameTime = 0f;
    private bool raceFinished = false;

    private static float mostRecentScore;

    public static float MostRecentScore
    {
        get
        {
            return mostRecentScore;
        }

        set
        {
            mostRecentScore = value;
        }
    }

    private void Start()
    {
        timeText = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateLapTime();
    }

    private void UpdateLapTime()
    {
        if (!raceFinished)
        {
            gameTime += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);

            timeText.text = timeSpan.ToString(@"mm\:ss");
        }
    }

    private void OnRaceFinished()
    {
        if (!raceFinished)
        {
            raceFinished = true;
            MostRecentScore = gameTime;
        }
    }

    private void OnEnable()
    {
        //Checkpoint.RaceFinished += OnRaceFinished;
    }

    private void OnDisable()
    {
        //Checkpoint.RaceFinished -= OnRaceFinished;
    }
}
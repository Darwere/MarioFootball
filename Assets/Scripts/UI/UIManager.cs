using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Score1;
    public Text Score2;
    public Text Timer;
    public Text Score1End;
    public Text Score2End;
    public GameObject image;
    public GameObject Camera;
    public GameObject CanvasEndGame;
    public GameObject CanvasPause;
    public AudioSource MusicEnd;
    public List<AudioSource> AudiosGame = new List<AudioSource>();
    public static UIManager instance;

    private Vector3 camPosition;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        camPosition = Camera.transform.position;
        Score1.text = Field.Team2.ConcededGoals.ToString();
        Score2.text = Field.Team1.ConcededGoals.ToString();
    }

    public static void ActualiseTimer(uint timer)
    {
        uint minutes = (uint)Math.Floor(timer / 60f);
        uint secondes = timer % 60;
        instance.Timer.text = minutes.ToString() + " : " + secondes.ToString();
    }

    public static void ActualiseScore()
    {
        instance.Score1.text = Field.Team2.ConcededGoals.ToString();
        instance.Score2.text = Field.Team1.ConcededGoals.ToString();
    }

    public static void TimeOut()
    {
        instance.Camera.transform.position = instance.camPosition;
        instance.CanvasEndGame.SetActive(true);
        instance.image.gameObject.SetActive(false);
        instance.Timer.gameObject.SetActive(false);
        instance.Score1.gameObject.SetActive(false);
        instance.Score2.gameObject.SetActive(false);

        foreach (AudioSource audio in instance.AudiosGame)
        {
            audio.Stop();
        }
        instance.Score1End.text = Field.Team2.ConcededGoals.ToString();
        instance.Score2End.text = Field.Team1.ConcededGoals.ToString();
    }

    public void ActiveMenuPause()
    {
        Time.timeScale = 0;
        CanvasPause.SetActive(true);
        foreach (AudioSource audio in instance.AudiosGame)
        {
            audio.Pause();
        }
    }

    public static void BackToGame()
    {
        instance.CanvasPause.SetActive(false);
        Time.timeScale = 1f;
        foreach (AudioSource audio in instance.AudiosGame)
        {
            audio.UnPause();
        }

    }
}

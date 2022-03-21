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
    private static UIManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Score1.text = Field.Team2.ConcededGoals.ToString();
        Score2.text = Field.Team1.ConcededGoals.ToString();
    }

    public static void ActualiseTimer(uint timer)
    {
        uint minutes = (uint)Math.Floor(timer / 60f);
        uint secondes = timer % 60;
        instance.Timer.text = minutes.ToString() + " : " + secondes.ToString();
    }
}

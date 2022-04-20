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

    public Image Item1P1;
    public Image Item2P1;

    public Image Item1P2;
    public Image Item2P2;

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

    public static void ActualiseScore()
    {
        instance.Score1.text = Field.Team2.ConcededGoals.ToString();
        instance.Score2.text = Field.Team1.ConcededGoals.ToString();
    }

    public static void PlaceItem(Team team, Sprite sprite)
    {
        if (team == Field.Team1)
        {
            if (instance.Item1P1.sprite == null)
            {
                instance.Item1P1.sprite = sprite;
                instance.changeColorTo255(instance.Item1P1);
            }
            else 
            {
                instance.Item2P1.sprite = sprite;
                instance.changeColorTo255(instance.Item2P1);
            }

        }
        else
        {
            if (instance.Item1P2.sprite == null)
            {
                instance.Item1P1.sprite = sprite;
                instance.changeColorTo255(instance.Item1P2);
            }
            else
            {
                instance.Item2P2.sprite = sprite;
                instance.changeColorTo255(instance.Item2P2);
            }
        }
    }

    public void changeColorTo255(Image image)
    {
        Color color = image.color;
        color.a = 255;
        image.color = color;
    }
    public void changeColorTo0(Image image)
    {
        Color color = image.color;
        color.a = 0;
        image.color = color;
    }
    public static void RemoveItem(Team team)
    {
        if (team == Field.Team1)
        {
            instance.Item1P1.sprite = null;
            instance.changeColorTo0(instance.Item1P1);
            if (instance.Item2P1.sprite != null)
            {
                instance.Item1P1.sprite = instance.Item2P1.sprite;
                instance.changeColorTo255(instance.Item1P1);
                instance.Item2P1.sprite = null;
                instance.changeColorTo0(instance.Item2P1);
            }
            else
            {

            }
        }
        else
        {
            instance.Item1P2.sprite = null;
            instance.changeColorTo0(instance.Item1P2);
            if (instance.Item2P2.sprite != null)
            {
                instance.Item1P2.sprite = instance.Item2P2.sprite;
                instance.changeColorTo255(instance.Item1P2);
                instance.Item2P2.sprite = null;
                instance.changeColorTo0(instance.Item2P2);
            }
        }
    }
}

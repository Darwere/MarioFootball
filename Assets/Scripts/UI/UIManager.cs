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

    public void PlaceItem(Team team, Sprite sprite)
    {
        if (team == Field.Team1)
        {
            if (Item1P1.sprite == null)
            {
                Item1P1.sprite = sprite;
                changeColorTo255(Item1P1);
            }
            else 
            {
                Item2P1.sprite = sprite;
                changeColorTo255(Item2P1);
            }

        }
        else
        {
            if (Item1P2.sprite == null)
            {
                Item1P1.sprite = sprite;
                changeColorTo255(Item1P2);
            }
            else
            {
                Item2P2.sprite = sprite;
                changeColorTo255(Item2P2);
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
    public void RemoveItem(Team team)
    {
        if (team == Field.Team1)
        {
            Item1P1.sprite = null;
            changeColorTo0(Item1P1);
            if (Item2P1 != null)
            {
                Item1P1.sprite = Item2P1.sprite;
                Item2P1 = null;
                changeColorTo0(Item2P1);
            }
        }
        else
        {
            Item1P2.sprite = null;
            changeColorTo0(Item1P2);
            if (Item2P2 != null)
            {
                Item1P2.sprite = Item2P2.sprite;
                Item2P2 = null;
                changeColorTo0(Item2P2);
            }
        }
    }
}

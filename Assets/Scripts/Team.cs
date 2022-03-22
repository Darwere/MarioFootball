using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Team : MonoBehaviour
{
    [SerializeField] private string ateamBrainType;

    public Type TeamBrainType => Type.GetType(ateamBrainType);

    [SerializeField] private string agoalBrainType;
    public Type GoalBrainType => Type.GetType(agoalBrainType);

    public Player[] Players { get; private set; }
    public PlayerBrain[] Brains { get; private set; }
    public Player Goal { get; private set; }

    public int ConcededGoals;
    public InputBrain Brain { get; private set; }
    [SerializeField]
    private Transform[] ShootPoints;
    public Transform[] ShootPoint => ShootPoints;

    private Queue<Item> items;
    private int itemCapacity = 3;

    private void Awake()
    {
        Brain = GetComponent<InputBrain>();
    }

    /// <summary>
    /// Ajoute un item à la file d'items de l'équipe, dans le cas où celle-ci n'est pas pleine
    /// </summary>
    public void GainItem()
    {

    }

    /// <summary>
    /// Supprime l'item le plus ancien de la file d'items de l'équipe
    /// </summary>
    /// <returns>L'item supprimé</returns>
    public Item GetItem()
    {
        return items.Dequeue();
    }

    /// <summary>
    /// Initialise les joueurs et la file d'items de l'équipe
    /// </summary>
    /// <param name="players">Les joueurs sans le gardien</param>
    /// <param name="goalKeeper">Le gardien</param>
    public void Init(Player[] players, Player goalKeeper)
    {
        Players = players;
        Goal = goalKeeper;

        items = new Queue<Item>(itemCapacity);

        Brains = Players.Select(player => player.IABrain).ToArray();

        players[0].IsPiloted = true;
        Brain.SetPlayer(players[0]);
    }

    public Player GetPlayerWithDirection(Vector3 startPos, Vector3 dir)
    {
        Player targetPlayer = new Player();

        float angle = float.MaxValue;
        foreach(Player player in Players)
        {
            if(player.transform.position != startPos)
            {
                if(angle > Vector3.Angle(player.transform.position, dir))
                {
                    angle = Vector3.Angle(player.transform.position, dir);
                    targetPlayer = player;
                }       
            }
        }

        return targetPlayer;
    }

    public Player GetNearestPlayer(Vector3 point)
    {
        Player playerTarget = new Player();
        float rangeSqrt = float.MaxValue;
        Vector3 vector;

        foreach (Player player in Players)
        {
            if (!player.IsPiloted)
            {
                vector = point - player.transform.position;

                if (rangeSqrt > Vector3.SqrMagnitude(vector))
                {
                    rangeSqrt = Vector3.SqrMagnitude(vector);
                    playerTarget = player;
                }
            }

        }

        return playerTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            ++ConcededGoals;
            ball.Restart();
            UIManager.ActualiseScore();
        }
    }
}

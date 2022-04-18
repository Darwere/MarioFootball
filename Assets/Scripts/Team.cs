using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Collections;

public class Team : MonoBehaviour
{
    [SerializeField] private string ateamBrainType;

    public Type TeamBrainType => Type.GetType(ateamBrainType);

    [SerializeField] private string agoalBrainType;
    public Type GoalBrainType => Type.GetType(agoalBrainType);

    [SerializeField] private string aPilotedBrainType;
    public Type PilotedBrainType => Type.GetType(aPilotedBrainType);

    public Player[] Players { get; private set; }
    public PlayerBrain[] Brains { get; private set; }
    public Player Goal { get; private set; }

    public int ConcededGoals;
    public PlayerBrain Brain { get; private set; }
    [SerializeField]
    private Transform[] ShootPoints;
    public Transform[] ShootPoint => ShootPoints;

    private Queue<Item> items;
    private int itemCapacity = 3;


    [SerializeField] private GameObject pilotedIndicatorPrefab;
    private GameObject pilotedIndicator;
    private Vector3 indicatorOffSet;
    private object goalBrain;

    private void Awake()
    {
        Brain = GetComponent<PlayerBrain>();

        if (PilotedBrainType != Brain.GetType()) //désactiver les actions si ce n'est pas une personne qui contrôle le joueur
                                                 //event unity necessite que inputBrain soit un component par défaut
        {
            Destroy(GetComponent<PlayerBrain>());
            gameObject.AddComponent(PilotedBrainType);
            Brain = GetComponent<BehaviorTree.MyTree>();
        }

        indicatorOffSet = new Vector3(pilotedIndicatorPrefab.transform.position.x, pilotedIndicatorPrefab.transform.position.y, pilotedIndicatorPrefab.transform.position.z);
    }

    /// <summary>
    /// Ajoute un item � la file d'items de l'�quipe, dans le cas o� celle-ci n'est pas pleine
    /// </summary>
    public void GainItem()
    {

    }

    /// <summary>
    /// Supprime l'item le plus ancien de la file d'items de l'�quipe
    /// </summary>
    /// <returns>L'item supprim�</returns>
    public Item GetItem()
    {
        return items.Dequeue();
    }

    /// <summary>
    /// Initialise les joueurs et la file d'items de l'�quipe
    /// </summary>
    /// <param name="players">Les joueurs sans le gardien</param>
    /// <param name="goalKeeper">Le gardien</param>
    public void Init(Player[] players, Player goalKeeper)
    {
        
        Players = players;
        Goal = goalKeeper;

        items = new Queue<Item>(itemCapacity);

        Brains = Players.Select(player => player.IABrain).ToArray();

        pilotedIndicator = Instantiate(pilotedIndicatorPrefab, players[0].transform);
        Brain.SetPlayer(players[0]);
        Brain.Init();

        BehaviorTree.MyTree goalBrain = Goal.GetComponent<BehaviorTree.MyTree>();
        goalBrain.SetPlayer(goalKeeper);
        goalBrain.Init();
    }

    public Player GetPlayerWithDirection(Vector3 startPos, Vector3 dir, float angleThreshold)
    {
        Player targetPlayer = null;
        float minAngle = angleThreshold;
        float newAngle;

        foreach (Player player in Players)
        {
            if(player.transform.position != startPos)
            {
                newAngle = Vector3.Angle(player.transform.position - startPos, dir);

                if (newAngle < minAngle)
                { 
                    minAngle = newAngle;
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

    public void ChangePilotedPlayer(Player player)
    {
        if (Goal.IsPiloted)
        {
            Goal.IsPiloted = false;
            player.IsPiloted = true;
        }
        else
        {
            foreach (Player allie in Players)
            {
                if (allie.IsPiloted)
                {
                    allie.IsPiloted = false;
                    player.IsPiloted = true;
                }
            }
        }

        Brain.SetPlayer(player);
    }

    public void ChangePilotedIndicator(Player player)
    {
        pilotedIndicator.transform.parent = player.transform;
        pilotedIndicator.transform.localPosition = Vector3.zero + indicatorOffSet;
    }

    public void WaitKickOff()
    {
        foreach(Player player in Players)
        {
            player.KickOff();
        }
        Goal.KickOff();
        StartCoroutine(EndKickOff());
    }

    private IEnumerator EndKickOff()
    {
        yield return new WaitUntil(() => Field.Ball.transform.parent == null);
        EndingKickOff();
    }

    private void EndingKickOff()
    {
        foreach (Player player in Players)
        {
            if(player.State != Player.PlayerState.Waiting)
            player.StartPlaying();
        }
        Goal.StartPlaying();
    }

    private IEnumerator NewKickOff()
    {
        yield return new WaitForSeconds(2f);
        Field.Ball.Restart();
        Field.SetTeamPosition(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            ++ConcededGoals;
            UIManager.ActualiseScore();
            StartCoroutine(NewKickOff());
        }
    }

}

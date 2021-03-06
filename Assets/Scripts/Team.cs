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

    [SerializeField] private GameObject goalEffect;

    public Player[] Players { get; private set; }
    public PlayerBrain[] Brains { get; private set; }
    public Player Goal { get; private set; }

    public int ConcededGoals { get; private set; }
    public PlayerBrain Brain { get; private set; }

    [SerializeField] private Transform[] ShootPoints;
    public Transform[] ShootPoint => ShootPoints;

    private Queue<Item> items;
    [SerializeField] private int itemCapacity = 2;
    public float ItemCount => items.Count;

    [SerializeField] private GameObject pilotedIndicatorPrefab;
    private GameObject pilotedIndicator;
    private Vector3 indicatorOffSet;

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
    /// Ajoute un item � la file d'items de l'�quipe, dans le cas ou celle-ci n'est pas pleine
    /// </summary>
    public void GainItem()
    {
        if(items.Count < itemCapacity)
        {
            int index = UnityEngine.Random.Range(0, PrefabManager.Item.Count);
            Item script = PrefabManager.Item[index];
            items.Enqueue(script);
            GameObject item;
            PrefabManager.PrefabItems.TryGetValue(script, out item);

            if(item != null)
            {
                script = item.GetComponent<Item>();
                UIManager.PlaceItem(this, script.sprite);
            }
            
        }
    }

    /// <summary>
    /// Supprime l'item le plus ancien de la file d'items de l'equipe
    /// </summary>
    /// <returns>L'item supprim�</returns>
    public Item GetItem()
    {
        UIManager.RemoveItem(this);
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

    #region KickOffFunction

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
        GameManager.isPlayable = true;
    }

    private IEnumerator NewKickOff()
    {
        yield return new WaitForSeconds(2f);
        Field.Ball.Restart();
        Field.SetTeamPosition(this);
    }

    #endregion

    public void SetIABrain()
    {
        aPilotedBrainType = "OpponentTree";
        Destroy(GetComponent<PlayerBrain>());
        gameObject.AddComponent(PilotedBrainType);
        Brain = GetComponent<BehaviorTree.MyTree>();
        Brain.Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null && !ball.InGoal)
        {
            ball.InGoal = true;
            ++ConcededGoals;
            SongManager.GoalSong();
            GameObject particule = Instantiate(goalEffect, transform.position, Quaternion.identity);
            Destroy(particule, 0.3f);
            UIManager.ActualiseScore();

            for(int i = 0; i < Players.Length; i++)
            {
                Field.Team1.Players[i].StartWaiting();
                Field.Team2.Players[i].StartWaiting();
            }
            Field.Team1.Goal.StartWaiting();
            Field.Team2.Goal.StartWaiting();

            StartCoroutine(NewKickOff());
        }
    }

}

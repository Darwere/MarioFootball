using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Moving,
        Tackling,
        Headbutting,
        Passing,
        Shooting,
        Falling,
        Shocked,
        KickOff,
        Waiting
    }

    [SerializeField] private PlayerSpecs specs;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip tackleAnimation;

    private Rigidbody rgbd;

    [SerializeField] public PlayerBrain IABrain;

    public PlayerState State { get; private set; }
    public Team Team { get; private set; }

    public bool CanGetBall => !IsStunned && State != PlayerState.Headbutting && !HasBall;
    public bool IsStunned => State == PlayerState.Shocked || State == PlayerState.Falling;

    public bool HasBall => Field.Ball.transform.parent == transform;
    public bool IsDoped { get; private set; }
    public bool CanMove => State == PlayerState.Moving;
    public bool IsKickOff => State == PlayerState.KickOff;

    public bool IsPiloted { get; set; } = false;
    public PlayerSpecs Species => specs;

    public Vector3 Position => transform.position;

    private Dictionary<PlayerAction.ActionType, Action> actionMethods = new Dictionary<PlayerAction.ActionType, Action>();
    private PlayerAction lastAction = new PlayerAction();
    private PlayerAction savedAction = new PlayerAction(); //for action that trigger with delay

    public static Player CreatePlayer(GameObject prefab, Team team, bool isGoalKeeper = false)
    {
        Player player = Instantiate(prefab).GetComponent<Player>();

        Component brain = player.gameObject.AddComponent(isGoalKeeper ? team.GoalBrainType : team.TeamBrainType);

        player.IABrain = (PlayerBrain)player.GetComponent(brain.GetType());

        player.Team = team;

        player.GetComponent<Rigidbody>().isKinematic = true;

        return player;
    }

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rgbd.mass = specs.weight;
        gameObject.name = specs.name;

        if (Team == Field.Team1)
            gameObject.name += " team1";
        else
            gameObject.name += " team2";

        actionMethods.Add(PlayerAction.ActionType.None, Idle);
        actionMethods.Add(PlayerAction.ActionType.Move, Move);
        actionMethods.Add(PlayerAction.ActionType.Pass, Pass);
        actionMethods.Add(PlayerAction.ActionType.ChangePlayer, SwitchPlayer);
        actionMethods.Add(PlayerAction.ActionType.Shoot, Shoot);
        actionMethods.Add(PlayerAction.ActionType.Tackle, Tackle);
        actionMethods.Add(PlayerAction.ActionType.Dribble, Dribble);
        actionMethods.Add(PlayerAction.ActionType.Headbutt, Headbutt);
        actionMethods.Add(PlayerAction.ActionType.Throw, SendObject);
    }

    private void Update()
    {
        if (CanMove || (State == PlayerState.KickOff && HasBall))
        {
            if (IsPiloted)
            {
                lastAction = Team.Brain.Act();
                actionMethods[lastAction.type].DynamicInvoke(); //Team.Brain.Act() return une ActionType
            }
                
            else
            {
                lastAction = IABrain.Act();

                actionMethods[lastAction.type].DynamicInvoke();//IABrain.Act() return une ActionType
            }
                 
        }
    }

    #region Control Player Methods

    protected void Idle()
    {

    }

    protected void Move()
    {
        Vector3 movement = lastAction.direction * Time.deltaTime * Species.speed;
        Collider collider = GetComponent<Collider>();
        Vector3 startPosition = transform.position + new Vector3(0, collider.bounds.extents.y, 0);

        if (Physics.Raycast(startPosition, lastAction.direction, movement.magnitude * 2))
            movement = Vector3.zero;

        transform.position += movement;

        if (lastAction.direction != Vector3.zero)
            transform.forward = lastAction.direction;

        animator.SetBool("Moving", true);
    }

    protected void Pass()
    {
        State = PlayerState.Passing;
        transform.forward = lastAction.direction;

        savedAction = lastAction;
        animator.SetTrigger("Pass");
        animator.SetBool("Moving", false);
    }


    protected void SwitchPlayer()
    {
        IsPiloted = false; //last player piloted

        Team.Brain.SetPlayer(lastAction.target);
        animator.SetBool("Moving", false);
    }

    protected void Shoot()
    {
        State = PlayerState.Shooting;
        Team ennemies = Team == Field.Team1? Field.Team2 : Field.Team1;
        Field.Ball.Shoot(ennemies.ShootPoint, lastAction.shootForce, lastAction.direction, lastAction.duration);

        Field.Ball.DetachFromParent();

        animator.SetTrigger("Shoot");
        animator.SetBool("Moving", false);
    }

    protected void Tackle()
    {
        transform.forward = lastAction.direction;
        StartCoroutine(Tackling(lastAction.direction));

        State = PlayerState.Tackling;
        animator.SetTrigger("Tackle");
        animator.SetBool("Moving", false);
    }

    protected void Dribble()
    {
        animator.SetBool("Moving", false);
    }

    protected void Headbutt()
    {
        State = PlayerState.Headbutting;
        animator.SetTrigger("HeadButting");
        animator.SetBool("Moving", false);
    }

    protected void SendObject()
    {
        Item item = Team.GetItem();
        GameObject prefab;
        PrefabManager.PrefabItems.TryGetValue(item, out prefab);

        if(prefab != null)
        {
            GameObject itemSpawn = Instantiate(prefab, transform.position, Quaternion.identity);
            item = itemSpawn.GetComponent<Item>();
            item.Init(this);
        }

        //Debug.Log("SendObject");
    }

    #endregion

    public IEnumerator Tackling(Vector3 direction)
    {
        yield return new WaitForEndOfFrame(); //Wait next frame to change the State

        while (State == PlayerState.Tackling)
        {
            transform.position += direction * (specs.tackleRange * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return null;
    }

    public IEnumerator GotHit(Vector3 direction)
    {
        yield return new WaitForEndOfFrame(); //Wait next frame to change the State
        Debug.Log(State);
        while (State == PlayerState.Falling)
        {
            transform.position += direction * 1.5f * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return null;
    }

    #region EventFunction

    public void LaunchPass()
    {
        Field.Ball.Move(savedAction.duration, savedAction.startPosition, savedAction.endPosition, savedAction.bezierPoint);

        if(savedAction.target != null)
        {
            SwitchPlayer();
            savedAction.target.Wait();
        }
    }

    public void StartMoving()
    {
        State = PlayerState.Moving;
    }

    public void StartWaiting()
    {
        State = PlayerState.Waiting;
    }

    public void CheckPlayerHit()
    {
        Collider collider = GetComponent<Collider>();
        RaycastHit hit;
        Vector3 startPosition = transform.position + new Vector3(0, collider.bounds.extents.y, 0);
        float distance = Species.headButtRange;
        Debug.DrawRay(startPosition, transform.forward, Color.red, distance);
        Physics.Raycast(startPosition, transform.forward, out hit, distance);

        if (hit.collider != null)
        {
            Player player = hit.collider.gameObject.GetComponent<Player>();

            if (player != null && player.State != PlayerState.Falling)
            {
                Vector3 direction = player.transform.position - transform.position;
                player.GetHeadbutted(direction);
            }
        }
    }

    #endregion

    public void Wait()
    {
        State = PlayerState.Waiting;
        StartCoroutine(PassInMovement());
    }

    public void KickOff()
    {
        State = PlayerState.KickOff;
    }

    public void StartPlaying()
    {
            State = PlayerState.Moving;
    }

    IEnumerator PassInMovement()
    {
        yield return new WaitUntil(() => Field.Ball.transform.parent != null);
        State = PlayerState.Moving;
    }

    public void GetTackled()
    {
        State = PlayerState.Falling;
        animator.SetTrigger("TackleFall");

        if (HasBall)
            Field.Ball.DetachFromParent();
    }

    public void GetFreeze()
    {
        State = PlayerState.Shocked;

        if (HasBall)
            Field.Ball.DetachFromParent();
    }

    public void GetHeadbutted(Vector3 direction)
    {
        State = PlayerState.Falling;
        animator.SetTrigger("Knocked");

        if (HasBall)
            Field.Ball.DetachFromParent();

        transform.LookAt(direction);
        StartCoroutine(GotHit(direction));

    }

    private void OnCollisionEnter(Collision collision)
    {  
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            if(Team != player.Team)
            {
                if (State == PlayerState.Tackling)
                {
                    player.GetTackled();
                    if (player.HasBall)
                        Field.Ball.DetachFromParent();
                    else
                    {
                        player.Team.GainItem();
                        //Debug.Log("Gain Item");
                    }
                }
            }
        }
    }
}

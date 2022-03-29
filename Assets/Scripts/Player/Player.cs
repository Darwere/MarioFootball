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
        Shooting,
        Falling,
        Shocked,
        Waiting
    }

    [SerializeField] private PlayerSpecs specs;

    private Ball ball;

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

    public bool IsPiloted { get; set; } = false;
    public PlayerSpecs Species => specs;

    public Vector3 Position => transform.position;

    private Dictionary<PlayerAction.ActionType, Action> animationMethods = new Dictionary<PlayerAction.ActionType, Action>();

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

        animationMethods.Add(PlayerAction.ActionType.None, NoAction);
        animationMethods.Add(PlayerAction.ActionType.Move, MoveAction);
        animationMethods.Add(PlayerAction.ActionType.Pass, PassAction);
        animationMethods.Add(PlayerAction.ActionType.Shoot, ShootAction);
        animationMethods.Add(PlayerAction.ActionType.Tackle, TackleAction);
        animationMethods.Add(PlayerAction.ActionType.Dribble, DribbleAction);
        animationMethods.Add(PlayerAction.ActionType.Headbutt, HeadButtAction);
        animationMethods.Add(PlayerAction.ActionType.ChangePlayer, NoAction);
        animationMethods.Add(PlayerAction.ActionType.Throw, NoAction);
    }

    private void Update()
    {
        if (IsPiloted)
            animationMethods[Team.Brain.Act()].DynamicInvoke(); //Team.Brain.Act() return une ActionType
        else
            animationMethods[IABrain.Act()].DynamicInvoke(); //IABrain.Act() return une ActionType
    }

    public void GetBall(Ball ball)
    {
        this.ball = ball;
    }

    public IEnumerator Tackle(Vector3 direction)
    {
        yield return new WaitForEndOfFrame(); //Wait next frame to change the State

        while (State == PlayerState.Tackling)
        {
            transform.position += direction * (specs.tackleRange * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return null;
    }

    #region Action

    private void MoveAction()
    {
        animator.SetBool("Moving", true);
    }

    private void PassAction()
    { 
        animator.SetTrigger("Pass");
        animator.SetBool("Moving", false);
    }

    private void ShootAction()
    {
        animator.SetTrigger("Shoot");
        animator.SetBool("Moving", false);
    }

    private void TackleAction()
    {
        State = PlayerState.Tackling;
        animator.SetTrigger("Tackle");
        animator.SetBool("Moving", false);
    }

    private void HeadButtAction()
    {
        animator.SetBool("Moving", false);
    }

    private void DribbleAction()
    {
        animator.SetBool("Moving", false);
    }

    private void NoAction()
    {
        animator.SetBool("Moving", false);
    }

    #endregion

    #region EventFunction

    public void EndOfPass()
    {
        ball = null;
        State = PlayerState.Moving;
    }

    public void EndOfFall()
    {
        ball = null;
        State = PlayerState.Moving;
    }

    #endregion

    public void GetPass()
    {
        this.ball = Field.Ball;
        State = PlayerState.Waiting;
    }

    public void GetTackled()
    {
        State = PlayerState.Falling;
        animator.SetTrigger("TackleFall");
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
                }
            }
        }
    }
}

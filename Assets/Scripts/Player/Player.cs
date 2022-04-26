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
        Waiting,
        InPause
    }

    [SerializeField] private PlayerSpecs specs;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip tackleAnimation;
    [SerializeField] private AnimationClip knockedAnimation;
    [SerializeField] private AnimationClip gettingUpAnimation;

    public PlayerBrain IABrain { get; private set; }

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

    private PlayerState bufferState = new PlayerState();

    public static Player CreatePlayer(GameObject prefab, Team team, bool isGoalKeeper = false)
    {
        Player player = Instantiate(prefab).GetComponent<Player>();
        Component brain = player.gameObject.AddComponent(isGoalKeeper ? team.GoalBrainType : team.TeamBrainType);

        player.IABrain = (PlayerBrain)player.GetComponent(brain.GetType());

        player.Team = team;

        player.GetComponent<Rigidbody>().isKinematic = true;

        return player;
    }

    private void Start()
    {
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
        actionMethods.Add(PlayerAction.ActionType.BlockBall, Dive);
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

    private void Idle()
    {
        animator.SetBool("Moving", false);
    }

    private void Move()
    {
        Vector3 movement = lastAction.direction * Time.deltaTime * Species.speed;
        Collider collider = GetComponent<Collider>();
        Vector3 startPosition = transform.position + new Vector3(0, collider.bounds.extents.y, 0);

        if (Physics.Raycast(startPosition, lastAction.direction, movement.magnitude * 2))
            movement = Vector3.zero;

        transform.position += movement;

        if (lastAction.direction != Vector3.zero)
            transform.forward = lastAction.direction;

        if (Team.Goal == this)
        {
            Vector3 dir = Field.Ball.transform.position - transform.position;
            dir.y = transform.forward.y;
            transform.forward = dir.normalized;
        }
            

        animator.SetBool("Moving", true);
    }

    private void Pass()
    {
        State = PlayerState.Passing;
        transform.forward = (lastAction.target.transform.position - transform.position).normalized;

        savedAction = lastAction;
        animator.SetTrigger("Pass");
        animator.SetBool("Moving", false);
    }


    private void SwitchPlayer()
    {
        IsPiloted = false; //last player piloted

        Team.Brain.SetPlayer(lastAction.target);
        animator.SetBool("Moving", false);
    }

    private void Shoot()
    {
        transform.forward = lastAction.direction;
        State = PlayerState.Shooting;
        Team ennemies = Team == Field.Team1 ? Field.Team2 : Field.Team1;
        if ((ennemies == Field.Team1 && transform.position.x < 0) || (ennemies == Field.Team2 && transform.position.x > 0))
        {
            Field.Ball.Shoot(ennemies.ShootPoint, lastAction.shootForce, lastAction.direction, lastAction.duration);
        }
        else
        {
            Field.Ball.BadShoot(ennemies.ShootPoint,lastAction.direction);
        }
        Field.Ball.DetachFromParent();

        animator.SetTrigger("Shoot");
        animator.SetBool("Moving", false);
    }

    private void Tackle()
    {
        transform.forward = lastAction.direction;
        StartCoroutine(Tackling(lastAction.direction));

        State = PlayerState.Tackling;
        animator.SetTrigger("Tackle");
        animator.SetBool("Moving", false);
    }

    private void Dribble()
    {
        animator.SetBool("Moving", false);
    }

    private void Headbutt()
    {
        State = PlayerState.Headbutting;
        animator.SetTrigger("HeadButting");
        animator.SetBool("Moving", false);
    }

    private void SendObject()
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
    }

    private void Dive()
    {
        Vector3 direction = lastAction.direction;
        float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        StartWaiting();
        animator.SetFloat("Dive", angle);
        animator.SetTrigger("Diving");
    }

    #endregion

    #region StateFunction

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
        float timer = 0;
        Debug.Log(knockedAnimation.length);
        while (timer < knockedAnimation.length)
        {
            transform.position += direction * 1.5f * Time.deltaTime;
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        StartWaiting();

        timer = 0;

        while(timer < gettingUpAnimation.length)
        {
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        StartPlaying();

        yield return null;
    }

    public void Wait()
    {
        if (State == PlayerState.InPause)
            bufferState = PlayerState.Moving;
        else
            State = PlayerState.Waiting;
        StartCoroutine(PassInMovement());
    }

    public void KickOff()
    {
        if (State == PlayerState.InPause)
            bufferState = PlayerState.KickOff;
        else
            State = PlayerState.KickOff;
    }

    public void StartPlaying()
    {
        if (State == PlayerState.InPause)
            bufferState = PlayerState.Moving;
        else
            State = PlayerState.Moving;
    }

    IEnumerator PassInMovement()
    {
        yield return new WaitUntil(() => Field.Ball.transform.parent != null);

        if (State == PlayerState.InPause)
            bufferState = PlayerState.Moving;
        else
            State = PlayerState.Moving;
    }

    public void GetTackled()
    {
        if (State == PlayerState.InPause)
            bufferState = PlayerState.Falling;
        else
            State = PlayerState.Falling;
        animator.SetBool("Moving", false);
        animator.SetTrigger("TackleFall");
        animator.SetBool("Moving", false);

        SongManager.HitSong();

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
        if (State == PlayerState.InPause)
            bufferState = PlayerState.Falling;
        else
            State = PlayerState.Falling;

        animator.SetTrigger("Knocked");
        animator.SetBool("Moving", false);

        SongManager.HitSong();

        if (HasBall)
            Field.Ball.DetachFromParent();

        transform.LookAt(direction);
        StartCoroutine(GotHit(direction));

    }

    public void StartMoving()
    {
        if (State != PlayerState.KickOff)
        {
            if (State == PlayerState.InPause)
                bufferState = PlayerState.Moving;
            else
                State = PlayerState.Moving;
        }
    }

    public void StartWaiting()
    {
        if (State == PlayerState.InPause)
            bufferState = PlayerState.Waiting;
        else
            State = PlayerState.Waiting;
        animator.SetBool("Moving", false);
    }

    public void Pause()
    {
        bufferState = State;
        State = PlayerState.InPause;
    }

    public void UnPause()
    {
        State = bufferState;
    }

    #endregion

    #region EventFunction

    public void LaunchPass()
    {
        Field.Ball.Move(savedAction.duration, savedAction.startPosition, savedAction.endPosition, savedAction.bezierPoint);

        if (savedAction.target != null)
        {
            SwitchPlayer();
            savedAction.target.Wait();
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            if (Team != player.Team)
            {
                if (State == PlayerState.Tackling)
                {
                    if (!player.HasBall)
                        player.Team.GainItem();

                    player.GetTackled();
                }
            }
        }
    }
}

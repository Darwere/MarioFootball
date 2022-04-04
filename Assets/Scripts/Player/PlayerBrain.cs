using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBrain : MonoBehaviour
{
    protected Player Player;

    protected Team Allies => Player.Team;
    protected Team Enemies => Allies == Field.Team1 ? Field.Team2 : Field.Team1;

    protected PlayerAction action;
    protected Dictionary<PlayerAction.ActionType, Action> actionMethods = new Dictionary<PlayerAction.ActionType, Action>();

    protected virtual void Awake()
    {
        Player = GetComponent<Player>();
        action = new PlayerAction();
    }

    protected virtual void Start()
    {
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

    protected bool IsOther(Player player)
    {
        return player != Player;
    }

    public void SetPlayer(Player player)
    {
        Player = player;
    }

    #region Control Player Methods

    protected void Idle()
    {

    }

    protected void Move()
    {
        Vector3 movement = action.direction * Time.deltaTime * Player.Species.speed;
        Collider collider = Player.GetComponent<Collider>();
        Vector3 startPosition = Player.transform.position + new Vector3(0, collider.bounds.extents.y, 0);
        
        if (Physics.Raycast(startPosition, action.direction, movement.magnitude * 2))
            movement = Vector3.zero;
       
        Player.transform.position += movement;

        if (action.direction != Vector3.zero)
            Player.transform.forward = action.direction;
    }

    protected void Pass()
    {
        Field.Ball.Move(action.duration, action.startPosition, action.endPosition, action.bezierPoint);
        SwitchPlayer();
        action.target.Wait();

        action.type = PlayerAction.ActionType.None;
    }


    protected void SwitchPlayer()
    {
        Player.IsPiloted = false; //last player piloted

        Player = action.target;
        Player.IsPiloted = true; //new player piloted

        action.type = PlayerAction.ActionType.None;
    }

    protected void Shoot()
    {

        Field.Ball.Shoot(Enemies.ShootPoint, action.shootForce, action.direction, action.duration);

        Field.Ball.DetachFromParent();
        action.type = PlayerAction.ActionType.None;
    }

    protected void Tackle()
    {
        //action.type = PlayerAction.ActionType.None;
        StartCoroutine(Player.Tackle(action.direction));

        Debug.Log("Tackle");
    }

    protected void Dribble()
    {

        action.type = PlayerAction.ActionType.None;
        Debug.Log("Drible");
    }

    protected void Headbutt()
    {

        action.type = PlayerAction.ActionType.None;
        Debug.Log("Headbutt");
    }

    protected void SendObject()
    {

        action.type = PlayerAction.ActionType.None;
        Debug.Log("SendObject");
    }

    #endregion

    /// <summary>
    /// Calcule le déplacement que l'IA doit appliquer au joueur/que la manette détecte
    /// </summary>
    /// <param name="team">L'équipe du joueur</param>
    /// <returns>Le vecteur de déplacement.</returns>

    //public abstract PlayerAction.ActionType Act();

    public PlayerAction.ActionType Act()
    {
        PlayerAction.ActionType lastActionType = action.type;

        if (Player.CanMove ||
            (Player.IsKickOff && Player.HasBall && action.type == PlayerAction.ActionType.Pass))
        {
            actionMethods[action.type].DynamicInvoke();

            if (action.type != PlayerAction.ActionType.Move)
                action.type = PlayerAction.ActionType.None; //Reset l'action
        }
        else
            lastActionType = PlayerAction.ActionType.None;

        return lastActionType;
    }
}

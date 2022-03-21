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

    protected abstract void Idle();

    protected abstract void Move();

    protected abstract void Pass();

    protected abstract void SwitchPlayer();

    protected abstract void Shoot();

    protected abstract void Tackle();

    protected abstract void Dribble();

    protected abstract void Headbutt();

    protected abstract void SendObject();

    #endregion
    /// <summary>
    /// Calcule le déplacement que l'IA doit appliquer au joueur/que la manette détecte
    /// </summary>
    /// <param name="team">L'équipe du joueur</param>
    /// <returns>Le vecteur de déplacement.</returns>
    public abstract Vector3 MoveInput();

    public abstract PlayerAction.ActionType Act();
}

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
        action.type = PlayerAction.ActionType.None;
    }

    protected bool IsOther(Player player)
    {
        return player != Player;
    }

    public void SetPlayer(Player player)
    {
        Player = player;
        Player.IsPiloted = true;
    }



    /// <summary>
    /// Calcule le déplacement que l'IA doit appliquer au joueur/que la manette détecte
    /// </summary>
    /// <param name="team">L'équipe du joueur</param>
    /// <returns>Le vecteur de déplacement.</returns>

    //public abstract PlayerAction.ActionType Act();

    public PlayerAction Act()
    {
        PlayerAction lastAction = new PlayerAction();

        if (Player.CanMove ||
            (Player.IsKickOff && Player.HasBall && action.type == PlayerAction.ActionType.Pass))
        {
            lastAction = action;
            if (action.type != PlayerAction.ActionType.Move)
                action.type = PlayerAction.ActionType.None; //Reset l'action
        }
        else
            lastAction.type = PlayerAction.ActionType.None;

        return lastAction;
    }
}

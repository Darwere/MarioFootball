using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBrain : MonoBehaviour
{
    protected Player Player;

    protected Team Allies => Player.Team;
    protected Team Enemies => Allies == Field.Team1 ? Field.Team2 : Field.Team1;

    protected PlayerAction action;

    public virtual void Init()
    {
        return;
    }

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
        //Debug.Log("SetPlayer : " + player.name);
        Player = player;
        Player.IsPiloted = true;
        Allies.ChangePilotedIndicator(player);
    }

    /// <summary>
    /// Calcule le d�placement que l'IA doit appliquer au joueur/que la manette d�tecte
    /// </summary>
    /// <param name="team">L'�quipe du joueur</param>
    /// <returns>Le vecteur de d�placement.</returns>

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

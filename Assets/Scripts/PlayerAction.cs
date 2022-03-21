using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerAction
{
    public enum ActionType
    {
        None,
        Shoot,
        Pass,
        Move,
        Tackle,
        Headbutt,
        Dribble,
        Throw,
        ChangePlayer
    }

    public ActionType type;
    public Vector2 deltaMove;
    public float shootForce;
    public Vector3 direction;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public Vector3 bezierPoint;
    public float duration;
    public bool isSprinting;
    public Player playerToSwitch;

    public static PlayerAction Shoot(float shootForce, Vector3 direction, Vector3 startPosition, float duration)
    {
        PlayerAction action = new PlayerAction();
        action.type = ActionType.Shoot;
        action.direction = direction;
        action.startPosition = startPosition;
        action.duration = duration;

        return action;
    }

    public static PlayerAction Pass(Vector3 direction, Vector3 startPosition, Vector3 endPosition, float duration, Player targetPlayer)
    {
        PlayerAction action = new PlayerAction();
        action.type = ActionType.Pass;
        action.direction = direction;
        action.startPosition = startPosition;
        action.endPosition = endPosition;
        action.bezierPoint = (endPosition - startPosition) / 2;
        action.duration = duration;
        action.playerToSwitch = targetPlayer;

        return action;
    }

    public static PlayerAction Pass(Vector3 direction, Vector3 startPosition, Vector3 endPosition, Vector3 bezeierPoint, float duration, Player targetPlayer)
    {
        PlayerAction action = PlayerAction.Pass(direction, startPosition, endPosition, duration, targetPlayer);
        action.type = ActionType.Pass;
        action.bezierPoint = bezeierPoint;

        return action;
    }

    public static PlayerAction Move(Vector3 direction)
    {
        PlayerAction action = new PlayerAction();
        action.type = ActionType.Move;
        action.direction = direction;

        return action;
    }

    public static PlayerAction Tackle(Vector3 direction)
    {
        PlayerAction action = new PlayerAction();
        action.type = ActionType.Tackle;
        action.direction = direction;

        return action;
    }

    public static PlayerAction HeadButt(Vector3 direction)
    {
        PlayerAction action = new PlayerAction();
        action.type = ActionType.Headbutt;
        action.direction = direction;

        return action;
    }

    public static PlayerAction Dribble()
    {
        PlayerAction action = new PlayerAction();
        action.type = ActionType.Dribble;

        return action;
    }

    public static PlayerAction Throw(Vector3 direction)
    {
        PlayerAction action = new PlayerAction();
        action.type = ActionType.Throw;
        action.direction = direction;

        return action;
    }

    public static PlayerAction ChangePlayer()
    {
        PlayerAction action = new PlayerAction();
        action.type = ActionType.ChangePlayer;

        return action;
    }
}

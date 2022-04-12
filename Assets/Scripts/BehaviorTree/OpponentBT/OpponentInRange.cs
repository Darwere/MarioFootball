using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class OpponentInRange : Node
{
    float squareRange = 0f;
    Team ennemies;

    public OpponentInRange(float range, Team ennemies)
    {
        this.squareRange = range * range;
        this.ennemies = ennemies; //ennemies is the script attach to opponent goal
    }

    public override NodeState Evaluate()
    {
        Player player = (Player)GetData("player");
        float opponentSquareRange;
        List<Player> opponents = new List<Player>();

        foreach (Player opponent in ennemies.Players)
        {
            if (!opponent.CanMove)
                break;

            opponentSquareRange = (opponent.transform.position - player.transform.position).sqrMagnitude;

            if(opponentSquareRange < squareRange)
                opponents.Add(opponent);
        }
        
        if(opponents.Count > 0)
        {
            Node root = GetRootNode();
            root.SetData("opponents", opponents);
            state = NodeState.Succes;
            return state;
        }

        state = NodeState.Failure;
        return state;
    }
}

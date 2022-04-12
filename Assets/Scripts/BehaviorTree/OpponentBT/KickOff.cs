using BehaviorTree;
using UnityEngine;

public class KickOff : Node
{
    private Team allies;

    public KickOff(Team allies)
    {
        this.allies = allies;
    }

    public override NodeState Evaluate()
    {
        Player player = (Player)GetData("player");

        if (player.State == Player.PlayerState.KickOff && player.HasBall)
        {
            int index = Random.Range(1, allies.Players.Length);

            Player target = allies.Players[index];
            Node root = GetRootNode();
            root.SetData("targetPass", target);
            state = NodeState.Succes;
            return state;
        }

        state = NodeState.Failure;
        return state;
    }
    
}

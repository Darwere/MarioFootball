using BehaviorTree;
using UnityEngine;

public class CanMove : Node
{
    public override NodeState Evaluate()
    {
        Player player = (Player)GetData("player");

        state = player.State == Player.PlayerState.Moving? NodeState.Succes : NodeState.Failure;
        return state;
    }
}

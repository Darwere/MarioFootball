using BehaviorTree;
using UnityEngine;

public class SwitchPlayer : Node
{
    public override NodeAction Evaluate()
    {
        Debug.Log("SwitchPlayer");
        Player target = (Player)GetData("playerToSwitch");

        if(target.GetType() == typeof(Player))
        {
            Debug.Log("WP");
            nodeAction.State = NodeState.Succes;
            nodeAction.Action = PlayerAction.ChangePlayer(target);

            Node root = GetRootNode();
            root.ClearData("playerToSwitch");
            root.ClearData("playerTransform");
            root.SetData("playerTransform", target.transform);

            return nodeAction;
        }

        nodeAction.State = NodeState.Failure;
        return nodeAction;
    }
}

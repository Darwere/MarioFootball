using BehaviorTree;
using UnityEngine;

namespace OpponentTreeSpace
{
    public class Pass : Node
    {
        public override NodeState Evaluate()
        {
            Player player = (Player)GetData("player");
            Player target = (Player)GetData("targetPass");
            PlayerAction action;

            Vector3 direction = (target.transform.position - player.transform.position).normalized;
            action = PlayerAction.Pass(direction, player.transform.position, target.transform.position, 2f, target);

            Node root = GetRootNode();
            root.SetData("action", action);
            root.ClearData("targetPass");

            state = NodeState.Succes;
            return state;
        }
    }
}

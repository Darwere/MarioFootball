using BehaviorTree;

namespace OpponentTreeSpace
{
    public class SwitchPlayer : Node
    {
        public override NodeState Evaluate()
        {
            Player target = (Player)GetData("playerToSwitch");

            if (target != null)
            {

                state = NodeState.Succes;
                Node root = GetRootNode();
                root.ClearData("playerToSwitch");
                root.SetData("player", target);
                PlayerAction action = PlayerAction.ChangePlayer(target);
                root.SetData("action", action);
                return state;
            }

            state = NodeState.Failure;
            return state;
        }
    }
}

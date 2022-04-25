using BehaviorTree;

namespace GoalTreeSpace
{
    public class DontMove : Node
    {

        public override NodeState Evaluate()
        {
            PlayerAction action = new PlayerAction();
            action.type = PlayerAction.ActionType.None;

            state = NodeState.Succes;
            return state;
        }
    }
}

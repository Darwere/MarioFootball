using BehaviorTree;

namespace GoalTreeSpace
{
    public class BallGotShooted : Node
    {
        public override NodeState Evaluate()
        {
            if (Field.Ball.GotShooted)
                state = NodeState.Succes;
            else
                state = NodeState.Failure;

            return state;
        }
    }
}

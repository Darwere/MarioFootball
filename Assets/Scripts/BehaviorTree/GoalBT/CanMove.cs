using BehaviorTree;

namespace GoalTreeSpace
{
    public class CanMove : Node
    {
        private Player goal;

        public CanMove(Player goal)
        {
            this.goal = goal;
        }

        public override NodeState Evaluate()
        {
            if (goal.CanMove)
                state = NodeState.Succes;
            else
                state = NodeState.Failure;

            return state;
        }
    }
}

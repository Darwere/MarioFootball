using BehaviorTree;
using UnityEngine;

namespace GoalTreeSpace
{
    public class MoveToStopGoal : Node
    {
        private Player goal;

        public MoveToStopGoal(Player goal)
        {
            this.goal = goal;
        }

        public override NodeState Evaluate()
        {
            Vector3 direction = Field.Ball.ShootPoint - goal.transform.position;
            direction.y = 0;
            direction.x = 0;

            PlayerAction action = PlayerAction.Move(direction.normalized);
            Node root = GetRootNode();
            root.SetData("action", action);

            state = NodeState.Succes;
            return state;
        }
    }
}

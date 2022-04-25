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
            Vector3 destination = (Vector3)GetData("destination");
            Vector3 direction = destination - goal.transform.position;
            Node root = GetRootNode();
            PlayerAction action = new PlayerAction();

            if (direction.sqrMagnitude < 1f)
            {
                state = NodeState.Failure;
                return state;
            }    
            else
            {
                action = PlayerAction.Move(direction.normalized);
                root.SetData("action", action);
                state = NodeState.Succes;
                return state;
            }
        }
    }
}

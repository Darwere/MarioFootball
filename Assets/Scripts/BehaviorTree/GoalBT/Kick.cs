using BehaviorTree;
using UnityEngine;

namespace GoalTreeSpace
{
    public class Kick : Node
    {
        public override NodeState Evaluate()
        {
            Vector3 direction = (Vector3)GetData("directionKick");

            if (direction != null)
            {
                PlayerAction action = PlayerAction.HeadButt(direction);
                Node root = GetRootNode();
                root.SetData("action", action);

                state = NodeState.Succes;
                return state;
            }

            state = NodeState.Failure;
            return state;
        }
    }
}

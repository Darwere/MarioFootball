using BehaviorTree;
using UnityEngine;

namespace GoalTreeSpace
{
    public class Dive : Node
    {
        Player goal;

        public Dive (Player goal)
        {
            this.goal = goal;
        }


        public override NodeState Evaluate()
        {
            Vector3 destination = (Vector3)GetData("destination");
            PlayerAction action = PlayerAction.BlockBall(destination - goal.transform.position);
            Node root = GetRootNode();
            root.SetData("action", action);
            root.ClearData("destination");
            root.ClearData("canSave");

            state = NodeState.Succes;
            return state;
        }
    }
}

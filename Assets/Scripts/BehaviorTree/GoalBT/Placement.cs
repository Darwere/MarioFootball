using BehaviorTree;
using UnityEngine;

namespace GoalTreeSpace
{
    public class Placement : Node
    {
        private Player goal;

        private Vector3 goalPos;
        float goalRadius;
        float tolerance = 1f;

        public Placement(Player goal, float goalRadius)
        {
            this.goalRadius = goalRadius;
            this.goal = goal;

            if (goal.Team == Field.Team1)
                goalPos = (Field.TopRightCorner + Field.BottomRightCorner) / 2;
            else
                goalPos = (Field.TopLeftCorner + Field.BottomLeftCorner) / 2;
        }

        public override NodeState Evaluate()
        {
            PlayerAction action;
            Vector3 ballToGoal = Field.Ball.transform.position - goalPos;
            Vector3 posCircle = goalPos + goalRadius * ballToGoal.normalized;

            if (Vector3.Distance(posCircle, goal.transform.position) > tolerance)
            {
                Vector3 toPosCircle = posCircle - goal.transform.position;
                toPosCircle = new Vector3(toPosCircle.x, 0, toPosCircle.z);
                Vector3 direction = toPosCircle.normalized;
                action = PlayerAction.Move(direction);
            }

            else 
                action = PlayerAction.Move(Vector3.zero);

            Node Root = GetRootNode();
            Root.SetData("action", action);
            state = NodeState.Succes;
            return state;
        }
        
    }
}

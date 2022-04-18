using BehaviorTree;
using UnityEngine;

namespace GoalTreeSpace
{
    public class OpponentInRange : Node
    {
        private Player goal;
        private float rangeDetection;
        private Team ennemies;

        public OpponentInRange(Player goal, float rangeDetection, Team ennemies)
        {
            this.goal = goal;
            this.rangeDetection = rangeDetection;
            this.ennemies = ennemies;
        }

        public override NodeState Evaluate()
        {
            foreach (Player player in ennemies.Players)
            {
                float squareDistance = (player.transform.position - goal.transform.position).sqrMagnitude;

                if (squareDistance < rangeDetection * rangeDetection)
                {
                    Debug.Log("OpponentInRange");
                    Vector3 direction = player.transform.position - goal.transform .position;

                    Node root = GetRootNode();
                    root.SetData("directionKick", direction.normalized);
                    
                    state = NodeState.Succes;
                    return state;
                }
            }

            state = NodeState.Failure;
            return state;
        }
    }
}

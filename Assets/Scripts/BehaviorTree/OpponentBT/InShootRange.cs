using BehaviorTree;
using UnityEngine;

namespace OpponentTreeSpace
{
    public class InShootRange : Node
    {
        float range = 0f;
        Team ennemies;

        public InShootRange(float range, Team ennemies)
        {
            this.range = range;
            this.ennemies = ennemies; //ennemies is the script attach to opponent goal
        }

        public override NodeState Evaluate()
        {
            Player player = (Player)GetData("player");
            Vector3 direction = ennemies.transform.position - player.transform.position;
            float squareDistance = direction.sqrMagnitude;

            if (squareDistance < range * range)
            {
                Node root = GetRootNode();
                root.SetData("shootDirection", direction.normalized);
                state = NodeState.Succes;
                return state;
            }

            state = NodeState.Failure;
            return state;
        }
    }
}

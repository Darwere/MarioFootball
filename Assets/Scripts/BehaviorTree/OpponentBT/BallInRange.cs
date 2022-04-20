using BehaviorTree;
using UnityEngine;

namespace OpponentTreeSpace
{
    public class BallInRange : Node
    {
        private float range = 0f;

        public BallInRange(float range)
        {
            this.range = range;
        }

        public override NodeState Evaluate()
        {
            Player playerTransform = (Player)GetData("player");
            Vector3 direction = Field.Ball.transform.position - playerTransform.transform.position;

            if (direction.sqrMagnitude < range * range)
                state = NodeState.Succes;
            else
                state = NodeState.Failure;

            return state;
        }

    }
}

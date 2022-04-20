using UnityEngine;
using BehaviorTree;

namespace OpponentTreeSpace
{
    public class ApproachBall : Node
    {
        public override NodeState Evaluate()
        {
            Player player = (Player)GetData("player");

            Vector3 direction = Field.Ball.transform.position - player.transform.position;
            direction.y = 0f;
            PlayerAction action = PlayerAction.Move(direction.normalized);
            Node root = GetRootNode();
            root.SetData("action", action);
            

            state = NodeState.Running;
            return state;
        }
    }
}

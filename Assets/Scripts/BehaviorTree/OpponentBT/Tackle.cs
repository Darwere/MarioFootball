using BehaviorTree;
using UnityEngine;

namespace OpponentTreeSpace
{
    public class Tackle : Node
    {
        public override NodeState Evaluate()
        {
            Node root = GetRootNode();
            Player player = (Player)root.GetData("player");
            Vector3 direction = Field.Ball.transform.position - player.transform.position;
            direction.y = 0;

            PlayerAction action = PlayerAction.Tackle(direction.normalized);
            root.SetData("action", action);

            state = NodeState.Succes;
            return state;
        }
    }
}

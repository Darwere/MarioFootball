using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

namespace OpponentTreeSpace
{
    public class ThrowItem : Node
    {

        public override NodeState Evaluate()
        {
            Player player = (Player)GetData("player");
            Vector3 direction = (Vector3)GetData("throwDirection");

            player.transform.forward = direction.normalized;
            PlayerAction action = PlayerAction.Throw(direction.normalized);
            Node root = GetRootNode();
            root.SetData("action", action);

            state = NodeState.Succes;
            return state;
        }

    }
}

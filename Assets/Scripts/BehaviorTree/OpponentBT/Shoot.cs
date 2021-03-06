using BehaviorTree;
using UnityEngine;

namespace OpponentTreeSpace
{
    public class Shoot : Node
    {
        public override NodeState Evaluate()
        {
            Vector3 shootDirection = (Vector3)GetData("shootDirection");
            float shootForce = 2f;
            float duration = 1f;

            PlayerAction action = PlayerAction.Shoot(shootForce, shootDirection, Field.Ball.transform.position, duration);
            Node root = GetRootNode();
            root.SetData("action", action);
            root.ClearData("shootDirection");

            state = NodeState.Succes;
            return state;
        }
    }
}

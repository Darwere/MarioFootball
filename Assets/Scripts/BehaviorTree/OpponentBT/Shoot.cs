using BehaviorTree;
using UnityEngine;

public class Shoot : Node
{
    public override NodeState Evaluate()
    {
        Vector3 shootDirection = (Vector3)GetData("shootDirection");
        ClearData("shootDirection");
        float shootForce = 2f; 
        float duration = 2f;

        PlayerAction action = PlayerAction.Shoot(shootForce, shootDirection, Field.Ball.transform.position, duration);

        Node root = GetRootNode();
        root.SetData("action", action);

        state = NodeState.Succes;
        return state;
    }
}

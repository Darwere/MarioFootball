using BehaviorTree;
using UnityEngine;

public class Tackle : Node
{
    public override NodeState Evaluate()
    {
        Node root = GetRootNode();
        Transform playerTransform = (Transform)root.GetData("playerTransform");
        Vector3 direction = Field.Ball.transform.position - playerTransform.position;

        PlayerAction action = PlayerAction.Tackle(direction.normalized);
        root.SetData("action", action);
        state = NodeState.Succes;

        return state;
    }
}

using BehaviorTree;
using UnityEngine;

public class GotBall : Node
{
    public override NodeState Evaluate()
    {
        Transform playerTransform = (Transform)GetData("playerTransform");
        state = Field.Ball.transform.parent == playerTransform? NodeState.Succes : NodeState.Failure;
        return state;
    }
}

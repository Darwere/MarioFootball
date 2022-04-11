using BehaviorTree;
using UnityEngine;

public class BallInRange : Node
{
    private float range = 0f;

    public BallInRange(float range)
    {
        this.range = range;
    }

    public override NodeState Evaluate()
    {
        Transform playerTransform = (Transform)GetData("playerTransform");
        Vector3 direction = Field.Ball.transform.position - playerTransform.position;

        if (direction.sqrMagnitude < range * range)
            state = NodeState.Succes;
        else
            state = NodeState.Failure;

        return state;
    }

}

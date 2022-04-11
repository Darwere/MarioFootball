using BehaviorTree;
using UnityEngine;

public class BallInRange : Node
{
    private Transform player;
    private float range = 0f;

    public BallInRange(float range, Transform player)
    {
        this.range = range;
        this.player = player;
    }

    public override NodeAction Evaluate()
    {
        Vector3 direction = Field.Ball.transform.position - player.transform.position;

        if (direction.sqrMagnitude < range * range)
            nodeAction.State = NodeState.Succes;
        else
            nodeAction.State = NodeState.Failure;

        return nodeAction;
    }

}

using UnityEngine;

using BehaviorTree;

public class ApproachBall : Node
{
    private Transform playerTransform;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public ApproachBall(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }


    public override NodeAction Evaluate()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;

            if (waitCounter >= waitTime)
                waiting = false;
        }
        else
        {
            Vector3 direction = Field.Ball.transform.position - playerTransform.position;
            if (direction.magnitude < 1f)
            {
                waitCounter = 0f;
                waiting = true;
            }
            else
                nodeAction.Action = PlayerAction.Move(direction);
        }
        nodeAction.State = NodeState.Running;
        return nodeAction;

    }
}

using UnityEngine;

using BehaviorTree;

public class ApproachBall : Node
{
    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public override NodeState Evaluate()
    {
        Transform playerTransform = (Transform)GetData("playerTransform");
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
            {
                PlayerAction action = PlayerAction.Move(direction.normalized);
                Node root = GetRootNode();
                root.SetData("action", action);
            }
                
        }
        state = NodeState.Running;
        return state;
    }
}

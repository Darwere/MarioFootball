using BehaviorTree;
using UnityEngine;

public class InShootRange : Node
{
    float range = 0f;
    Team opponent;

    public InShootRange(float range, Team opponent)
    {
        this.range = range;
        this.opponent = opponent; //opponent is the script attach to opponent goal
    }

    public override NodeState Evaluate()
    {
        Transform playerTransform = (Transform)GetData("playerTransform");
        Vector3 direction = opponent.transform.position - playerTransform.position;
        float squareDistance = direction.sqrMagnitude;

        if(squareDistance < range * range)
        {
            Node root = GetRootNode();
            root.SetData("shootDirection", direction.normalized);
            state = NodeState.Succes;
            return state;
        }
        
        state = NodeState.Failure;
        return state;
    }
}

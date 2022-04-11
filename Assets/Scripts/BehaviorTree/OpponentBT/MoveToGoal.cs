using BehaviorTree;
using UnityEngine;

public class MoveToGoal : Node
{
    Team opponent;
    public MoveToGoal(Team opponent)
    {
        this.opponent = opponent; //opponent is the script attach to opponent goal
    }

    public override NodeState Evaluate()
    {
        Transform playerTransform = (Transform)GetData("playerTransform");
        Vector3 direction = opponent.transform.position - playerTransform.position; 
        PlayerAction action = PlayerAction.Move(direction.normalized);
        Node root = GetRootNode();
        root.SetData("action", action);

        state = NodeState.Running;
        return state;
    }
}

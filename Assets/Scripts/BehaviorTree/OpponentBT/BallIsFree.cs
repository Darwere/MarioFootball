using BehaviorTree;

public class BallIsFree : Node
{
    public override NodeAction Evaluate()
    {
        nodeAction.State = Field.Ball.transform.parent == null ? NodeState.Succes : NodeState.Failure;
        return nodeAction;
    }
}

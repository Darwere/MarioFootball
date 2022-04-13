using BehaviorTree;

public class BallIsNotFree : Node
{
    public override NodeState Evaluate()
    {
        state = Field.Ball.transform.parent == null ? NodeState.Failure : NodeState.Succes;
        return state;
    }
}

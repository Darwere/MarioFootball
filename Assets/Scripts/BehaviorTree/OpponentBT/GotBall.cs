using BehaviorTree;

namespace OpponentTreeSpace
{
    public class GotBall : Node
    {
        public override NodeState Evaluate()
        {
            Player player = (Player)GetData("player");
            state = Field.Ball.transform.parent == player.transform ? NodeState.Succes : NodeState.Failure;
            return state;
        }
    }
}
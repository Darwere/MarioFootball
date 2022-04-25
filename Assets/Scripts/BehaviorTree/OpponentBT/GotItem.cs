using BehaviorTree;

namespace OpponentTreeSpace
{
    public class GotITem : Node
    {
        public override NodeState Evaluate()
        {
            Player player = (Player)GetData("player");
            state = player.Team.ItemCount > 0 ? NodeState.Succes : NodeState.Failure;
            return state;
        }
    }
}

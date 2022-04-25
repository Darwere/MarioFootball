using BehaviorTree;

namespace GoalTreeSpace
{
    public class BallGotShooted : Node
    {
        public override NodeState Evaluate()
        {
            if (Field.Ball.GotShooted)
                state = NodeState.Succes;
            else
            {
                Node root = GetRootNode();
                if (root.GetData("canSave") != null)
                    root.ClearData("canSave");
                state = NodeState.Failure;
            }
                

            return state;
        }
    }
}

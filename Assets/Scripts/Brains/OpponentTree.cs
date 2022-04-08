using BehaviorTree;

public class OpponentTree : Tree
{
    protected override Node SetUpTree()
    {
        Node root = new ApproachBall(Player.transform);

        return root;
    }
}

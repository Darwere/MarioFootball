using System.Collections.Generic;
using BehaviorTree;

public class OpponentTree : MyTree
{
    protected override void Update()
    {
        if (root != null && root.GetData("playerTransform") == null)
            root.SetData("playerTransform", Player.transform);

        base.Update();
    }

    protected override Node SetUpTree()
    {
        Node root = new Selector(new List<Node>
        {
             HasBallBranch(),
             SwitchBranch(),
             TackleBranch(),
             new ApproachBall()
        });

        root.SetData("this", this);
        return root;
    }

    #region BranchCreation

    private Node SwitchBranch()
    {
        return new Sequence(new List<Node>
             {
                 new NotNearestAllie(Allies.Players),
                 new SwitchPlayer()
             });
    }

    private Node TackleBranch()
    {
        return new Sequence(new List<Node>
             {
                 new BallInRange(Player.Species.tackleRange * 2/3), //reduce range for a better takcle interaction
                 new BallIsNotFree(),
                 new Tackle()
             });
    }

    private Node HasBallBranch()
    {
        return new Sequence(new List<Node>
             {
                 new GotBall(),
                 new Selector(new List<Node>
                 {
                    ShootBranch(),
                    new MoveToGoal(Enemies)
                 })
             });
    }

    private Node ShootBranch()
    {
        return new Sequence(new List<Node>
             {
                 new InShootRange(Player.Species.shootRange, Enemies),
                 new Shoot()
             });
    }

    #endregion
}

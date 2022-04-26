using System.Collections.Generic;
using BehaviorTree;
using OpponentTreeSpace;

public class OpponentTree : MyTree
{
    float rangeDetection = 5f;
    float angleDetection = 20f;

    protected override void Update()
    {
        if (root != null && (Player)root.GetData("player") != Player)
            root.SetData("player", Player);

        base.Update();
    }

    protected override Node SetUpTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node> {
                new KickOff(Allies),
                new Pass()
            }),
            new Sequence(new List<Node> 
            {
                new CanMove(),
                new Selector(new List<Node>
                {
                    HasBallBranch(),
                    SwitchBranch(),
                    TackleBranch(),
                    new ApproachBall()
                })
            })
        });

        root.SetData("player", Player);
        return root;
    }

    #region BranchCreation



    private Node HasBallBranch()
    {
        return new Sequence(new List<Node>
            {
                new GotBall(),
                new Selector(new List<Node>
                {
                ShootBranch(),
                new Selector(new List<Node>
                {
                    PassBranch()
                }),
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

    private Node PassBranch()
    {
        return new Sequence(new List<Node>
            {
                new OpponentInRange(rangeDetection, Enemies),
                new Selector(new List<Node>
                {
                    ItemBranch(),
                    new Sequence(new List<Node>
                    {
                        new AllieFree(Allies, Enemies, angleDetection),
                        new Pass()
                    })
                })
                
            });
    }

    private Node ItemBranch()
    {
        return new Sequence(new List<Node>
            {
                new GotITem(),
                new PlayerInFront(Enemies, rangeDetection, 90f),
                new ThrowItem()
            });
    }

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
        return new Selector(new List<Node> {
                new Sequence(new List<Node>
                {
                    new BallInRange(Player.Species.tackleRange * 2/3), //reduce range for a better takcle interaction
                    new BallIsNotFree(),
                    new Tackle()
                }),
                ItemBranch()
            });
    }

    #endregion
}

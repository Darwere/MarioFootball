using UnityEngine;
using BehaviorTree;
using GoalTreeSpace;
using System.Collections.Generic;

public class GoalTree : MyTree
{
    float goalRadius = 12f;
    float rangeDetection = 4f;

    protected override Node SetUpTree()
    {
        return new Sequence(new List<Node>
        {
            new CanMove(Player),
            new Selector(new List<Node>
            {
                BlockBallBranch(),
                KickBranch(),
                new Movement(Player, goalRadius)
            })
        });
            
    }

    #region BranchCreation

    private Node KickBranch()
    {
        return new Sequence(new List<Node>
        {
            new OpponentInRange(Player, Player.Species.headButtRange, Enemies),
            new Kick()
        });
    }

    private Node BlockBallBranch()
    {
        return new Sequence(new List<Node>
        {
            new BallGotShooted(),
            new MoveToStopGoal(Player)
        });
    }

    #endregion
}

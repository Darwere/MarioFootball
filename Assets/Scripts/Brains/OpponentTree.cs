using UnityEngine;
using System.Collections.Generic;
using BehaviorTree;

public class OpponentTree : MyTree
{
    protected override Node SetUpTree()
    {
        Node root = new Selector( new List<Node>
        {
             new Sequence( new List<Node>
             {
                 new NotNearestAllie(Allies.Players),
                 new SwitchPlayer()
             }),
             new ApproachBall()
        });

        root.SetData("this", this);
        return root;
    }

    protected override void Update()
    {
        base.Update();

        if(root != null && root.GetData("playerTransform") == null)
            root.SetData("playerTransform", Player.transform);
    }
}

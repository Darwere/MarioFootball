using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class NotNearestAllie : Node
{
    private List<Player> allies = new List<Player>();

    public NotNearestAllie(Player[] allies)
    {
        foreach (Player allie in allies)
            this.allies.Add(allie);
    }

    public override NodeState Evaluate()
    {
        
        Player player = (Player)GetData("player");
        float MinSquareDistance = float.MaxValue;
        Player playerToSwitch = null;

        foreach(Player allie in allies)
        {
            Vector3 vector = allie.transform.position - Field.Ball.transform.position;
            if(allie.transform.position != player.transform.position && vector.sqrMagnitude < MinSquareDistance)
            {
                MinSquareDistance = vector.sqrMagnitude;
                playerToSwitch = allie;
            }  
        }
        float playerSquareDistance = (player.transform.position - Field.Ball.transform.position).sqrMagnitude;

        if (MinSquareDistance < playerSquareDistance - 1f)
        {
            state = NodeState.Succes;
            Node root = GetRootNode();
            root.SetData("playerToSwitch", playerToSwitch);
            return state;
        }
        state = NodeState.Failure;
        return state;
    }
}

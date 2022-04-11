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

    public override NodeAction Evaluate()
    {
        Debug.Log("NotNearestAllie");
        Transform playerTransform = (Transform)GetData("playerTransform");
        float MinSquareDistance = float.MaxValue;
        Player playerToSwitch = null;

        foreach(Player allie in allies)
        {
            Vector3 vector = allie.transform.position - Field.Ball.transform.position;
            if(vector.sqrMagnitude < MinSquareDistance)
            {
                MinSquareDistance = vector.sqrMagnitude;
                playerToSwitch = allie;
            }  
        }
        float playerSquareDistance = (playerTransform.position - Field.Ball.transform.position).sqrMagnitude;

        if (MinSquareDistance < playerSquareDistance)
        {
            nodeAction.State = NodeState.Succes;
            Node root = GetRootNode();
            root.SetData("playerToSwitch", playerToSwitch);

            return nodeAction;
        }

        nodeAction.State = NodeState.Failure;
        return nodeAction;
    }
}

using BehaviorTree;
using UnityEngine;

public class KickOff : Node
{
    private Team allies;
    private float waitingTime;
    private float timer = 0f;
    public KickOff(Team allies)
    {
        this.allies = allies;
        waitingTime = Random.Range(2, 5);
    }

    public override NodeState Evaluate()
    {
        Player player = (Player)GetData("player");

        if (player.State == Player.PlayerState.KickOff && player.HasBall)
        {
            if(timer < waitingTime)
                timer += Time.deltaTime;
            else
            {
                timer = 0f;
                int index = Random.Range(1, allies.Players.Length);

                Player target = allies.Players[index];
                Node root = GetRootNode();
                root.SetData("targetPass", target);
                state = NodeState.Succes;
                return state;
            }
        }

        state = NodeState.Failure;
        return state;
    }
    
}

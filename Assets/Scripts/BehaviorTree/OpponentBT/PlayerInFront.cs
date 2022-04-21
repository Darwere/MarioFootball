using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

namespace OpponentTreeSpace
{
    public class PlayerInFront : Node
    {
        private float range = 0f;
        private float angleDetection;
        Team ennemies;

        public PlayerInFront(Team ennemies, float range, float angleDetection)
        {
            this.range = range;
            this.ennemies = ennemies;
            this.angleDetection = angleDetection;
        }

        public override NodeState Evaluate()
        {
            Player player = (Player)GetData("player");
            Vector3 distance;
            foreach(Player opponent in ennemies.Players)
            {
                distance = opponent.transform.position;

                if(distance.sqrMagnitude < range * range)
                {
                    Vector3 opponentDirection = opponent.transform.position - player.transform.position;
                    float angle = Vector3.Angle(player.transform.forward, opponentDirection);

                    if(angle < angleDetection)
                    {
                        Node root = GetRootNode();
                        root.SetData("throwDirection", opponentDirection);
                        state = NodeState.Succes;
                        return state;
                    }
                }
            }

            state = NodeState.Failure;
            return state;
        }

    }
}

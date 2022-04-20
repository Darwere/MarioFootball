using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

namespace OpponentTreeSpace
{
    public class AllieFree : Node
    {
        Team allies;
        Team ennemies;
        float angleDetection;

        public AllieFree(Team allies, Team ennemies, float angleDetection)
        {
            this.allies = allies;
            this.ennemies = ennemies;
            this.angleDetection = angleDetection;
        }

        public override NodeState Evaluate()
        {
            Player player = (Player)GetData("player");
            List<Player> alliesFree = new List<Player>();
            Vector3 direction;

            foreach (Player allie in allies.Players)
            {
                if (allie.transform != player.transform)
                {
                    direction = allie.transform.position - player.transform.position;

                    if (ennemies.GetPlayerWithDirection(player.transform.position, direction, angleDetection) == null)
                        alliesFree.Add(allie);
                }
            }

            if (alliesFree.Count > 0)
            {
                float lowestRange = float.MaxValue;
                float squareRangeToGoal;
                Player target = new Player();

                foreach (Player allie in alliesFree)
                {
                    squareRangeToGoal = (allie.transform.position - ennemies.transform.position).sqrMagnitude;

                    if (squareRangeToGoal < lowestRange)
                    {
                        lowestRange = squareRangeToGoal;
                        target = allie;
                    }
                }

                Node root = GetRootNode();
                root.SetData("targetPass", target);
                state = NodeState.Succes;
                return state;
            }

            state = NodeState.Succes;
            return state;
        }
    }
}

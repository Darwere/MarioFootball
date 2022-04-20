using BehaviorTree;
using UnityEngine;

namespace OpponentTreeSpace
{
    public class MoveToGoal : Node
    {
        Team opponent;
        public MoveToGoal(Team opponent)
        {
            this.opponent = opponent; //opponent is the script attach to opponent goal
        }

        public override NodeState Evaluate()
        {
            Player player = (Player)GetData("player");
            Vector3 direction = opponent.transform.position - player.transform.position;
            direction.y = 0;
            PlayerAction action = PlayerAction.Move(direction.normalized);
            Node root = GetRootNode();
            root.SetData("action", action);

            state = NodeState.Running;
            return state;
        }
    }
}
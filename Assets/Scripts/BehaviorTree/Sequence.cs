using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }


        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach(Node node in children)
            {
                switch(node.Evaluate())
                {

                    case NodeState.Failure:
                        state = NodeState.Failure;
                        return state;

                    case NodeState.Succes:
                        continue;

                    case NodeState.Running:
                        anyChildIsRunning = true;
                        continue;

                    default:
                        state = NodeState.Succes;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.Running : NodeState.Succes;
            return state;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }


        public override NodeAction Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach(Node node in children)
            {
                switch(node.Evaluate().State)
                {

                    case NodeState.Failure:
                        nodeAction.State = NodeState.Failure;
                        return nodeAction;

                    case NodeState.Succes:
                        continue;

                    case NodeState.Running:
                        anyChildIsRunning = true;
                        continue;

                    default:
                        nodeAction.State = NodeState.Succes;
                        return nodeAction;
                }
            }

            nodeAction.State = anyChildIsRunning ? NodeState.Running : NodeState.Succes;
            return nodeAction;
        }
    }
}

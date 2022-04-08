using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }


        public override NodeAction Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate().State)
                {

                    case NodeState.Failure:
                        continue;

                    case NodeState.Succes:
                        nodeAction.State = NodeState.Succes;
                        return nodeAction;

                    case NodeState.Running:
                        nodeAction.State = NodeState.Running;
                        return nodeAction;

                    default:
                        continue;
                }
            }

            nodeAction.State = anyChildIsRunning ? NodeState.Running : NodeState.Succes;
            return nodeAction;
        }
    }
}

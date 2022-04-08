using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : PlayerBrain
    {
        private Node root = null;
        protected NodeAction nodeAction = new NodeAction();

        protected void Start()
        {
            root = SetUpTree();
        }

        protected void Update()
        {
            if(root != null)
            {
                nodeAction = root.Evaluate();
                action = nodeAction.Action;
            }    
        }

        protected abstract Node SetUpTree();
    }
}

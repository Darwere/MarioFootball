using UnityEngine;

namespace BehaviorTree
{
    public abstract class MyTree : PlayerBrain
    {
        protected Node root = null;
        protected NodeAction nodeAction = new NodeAction();

        public override void Init()
        {
            Debug.Log("test");
            root = SetUpTree();
        }

        protected virtual void Update()
        {
            if (root != null)
            {
                nodeAction = root.Evaluate();
                action = nodeAction.Action;
            }    
        }

        protected abstract Node SetUpTree();
    }
}

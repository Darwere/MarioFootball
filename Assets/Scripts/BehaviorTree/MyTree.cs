using UnityEngine;

namespace BehaviorTree
{
    public abstract class MyTree : PlayerBrain
    {
        protected Node root = null;

        public override void Init()
        {
            root = SetUpTree();
        }

        protected virtual void Update()
        {
            if (root != null)
            {
                root.Evaluate();
                action = (PlayerAction)root.GetData("action");
                root.ClearData("action");
                if(action.type == PlayerAction.ActionType.ChangePlayer)
                    Debug.Log(action.target);
            }    
        }

        protected abstract Node SetUpTree();
    }
}

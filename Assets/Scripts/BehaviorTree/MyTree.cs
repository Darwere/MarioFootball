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
                object obj = root.GetData("action");
                
                if(obj != null)
                {
                    action = (PlayerAction)obj;
                    root.ClearData("action");
                }
            }    
        }

        protected abstract Node SetUpTree();
    }
}

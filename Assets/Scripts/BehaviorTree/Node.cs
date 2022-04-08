using System.Collections;
using System.Collections.Generic;


namespace BehaviorTree
{
    public enum NodeState
    {
        Running,
        Succes,
        Failure
    }

    public class NodeAction
    {
        public NodeState State;
        public PlayerAction Action;

        public NodeAction()
        {
            State = NodeState.Failure;
            Action = new PlayerAction();
            Action.type = PlayerAction.ActionType.None;
        }
    }

    public class Node
    {
        protected NodeAction nodeAction = new NodeAction();

        public Node Parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> dataContext = new Dictionary<string, object>();

        public Node()
        {
            Parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
                Attach(child);
        }

        private void Attach(Node node)
        {
            node.Parent = this;
            children.Add(node);
        }

        public virtual NodeAction Evaluate() => new NodeAction();

        public void SetData(string key, object value)
        {
            dataContext[key] = value; 
        }

        public object GetData(string key)
        {
            object value = null;
            if (dataContext.TryGetValue(key, out value))
                return value;

            Node node = Parent;
            while (node != null)
            {
                value = node.GetData(key);
                if(value != null)
                    return value;
                node = node.Parent;
            }

            return null;
        }

        public bool ClearData(string key)
        {
            if(dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }

            Node node = Parent;
            while (node != null)
            {
                bool cleared = ClearData(key);
                if (cleared)
                    return true;
                node = node.Parent;
            }

            return false;
        }
    }
}

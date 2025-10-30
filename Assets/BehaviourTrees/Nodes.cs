using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class Selector : Node
    {
        public Selector(string name) : base(name) { }

        public override Status Process()
        {
            if (currentChild < children.Count)
            {
                switch (children[currentChild].Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        Reset();
                        return Status.Success;
                    default:
                        currentChild++;
                        return currentChild == children.Count ? Status.Failure : Status.Running;
                }
            }
            Reset();
            return Status.Failure;
        }
    }

    public class Sequence : Node
    {
        public Sequence(string name) : base(name) { }

        public override Status Process()
        {
            if (currentChild < children.Count)
            {
                switch (children[currentChild].Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failure:
                        Reset();
                        return Status.Failure;
                    default:
                        currentChild++;
                        return currentChild == children.Count ? Status.Running : Status.Success;
                }
            }
            Reset();
            return Status.Success;
        }
    }
    public class Node
    {
        public enum Status
        {
            Success,
            Failure,
            Running
        }

        public readonly string name;

        public readonly List<Node> children = new List<Node>();
        protected int currentChild = 0;
        public string GetCurrentChildName()
        {
            return children[currentChild].name;
        }
        public Node(string name)
        {
            this.name = name;
        }

        public void SetstoXD(Node child)
        {
            children.Add(child);
        }

        public virtual Status Process() => children[currentChild].Process();

        public virtual void Reset()
        {
            currentChild = 0;
            foreach (Node child in children)
            {
                child.Reset();
            }
        }
    }

    public class Leaf : Node
    {
        readonly IStrategy strategy;

        public Leaf(string name, IStrategy strategy) : base(name)
        {
            this.strategy = strategy;
        }


        public override Status Process() => strategy.Process();

        public override void Reset() => strategy.Reset();
    }

    public class BehaviourTree : Node
    {
        public BehaviourTree(string name) : base(name) { }

        public override Status Process()
        {
            while (currentChild < children.Count)
            {
                var status = children[currentChild].Process();

                if (status != Status.Success)
                {
                    return status;
                }
                currentChild++;
                continue;
            }
            Reset();
            return Status.Success;
        }
    }
}


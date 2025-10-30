using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    public interface IStrategy
    {
        Node.Status Process();
        void Reset()
        {
            //Mejor NADOTA
        }
    }

    public class Condition : IStrategy
    {
        readonly Func<bool> predicate;

        public Condition(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        public Node.Status Process() => predicate() ? Node.Status.Success : Node.Status.Failure;
    }

    public class ActionStrategy : IStrategy
    {
        readonly Action doSomething;

        public ActionStrategy(Action doSomething)
        {
            this.doSomething = doSomething;
        }

        public Node.Status Process()
        {
            doSomething();
            return Node.Status.Success;
        }
    }

    public class PatrolStrategy : IStrategy
    {
        public Transform entity;
        public NavMeshAgent agent;
        public List<Transform> patrolPoints;
        public float patrolSpeed;
        private int currentIndex;

        bool isPathCalculated;

        public PatrolStrategy(Transform entity, NavMeshAgent agent, List<Transform> patrolPoints, float patrolSpeed = 2f)
        {
            this.entity = entity;
            this.agent = agent;
            this.patrolPoints = patrolPoints;
            this.patrolSpeed = patrolSpeed;
        }

        public Node.Status Process()
        {
            if (currentIndex == patrolPoints.Count) return Node.Status.Success;

            var target = patrolPoints[currentIndex];
            agent.SetDestination(target.position);
            entity.LookAt(target);

            if (isPathCalculated == true && agent.remainingDistance > 0.1f)
            {
                isPathCalculated = false;
                currentIndex++;
            }

            if (agent.pathPending)
            {
                isPathCalculated = true;
            }

            return Node.Status.Running;
        }

        public void Reset() => currentIndex = 0;
    }
}


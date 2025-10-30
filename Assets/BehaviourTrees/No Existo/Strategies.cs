using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.AI;

namespace Home.BehaviourTrees
{
    public interface IStrategy
    {
        Node.Status Process();
        void Reset();
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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    public class Morgana : MonoBehaviour
    {
        public BehaviourTree tree;
        public GameObject prize;
        public List<Transform> patrolPoints = new List<Transform>();

        public NavMeshAgent agent;

        private void Awake()
        {
            tree = new BehaviourTree("Morgana :3");

            agent = GetComponent<NavMeshAgent>();

            var foo = new Condition(() => prize.activeSelf);
            Leaf isPrizePresent = new Leaf("IsPrizePresent", foo);

            Leaf moveToPrize = new Leaf("MoveToPrize", new ActionStrategy(() => { agent.SetDestination(prize.transform.position); Debug.Log("Go There bish"); }));

            Sequence findPrize = new Sequence("FindPrize");
            findPrize.SetstoXD(isPrizePresent);
            findPrize.SetstoXD(moveToPrize);

            Debug.Log(findPrize.children[1].name);

            Selector baseSelector = new Selector("Base Selector");
            baseSelector.SetstoXD(findPrize);
            baseSelector.SetstoXD(new Leaf("Patrol", new PatrolStrategy(transform, agent, patrolPoints)));

            tree.SetstoXD(baseSelector);
        }
        private void Update()
        {
            var foo = tree.Process();
            Debug.Log(tree.GetCurrentChildName() + " with a: " + foo);
        }
    }
}


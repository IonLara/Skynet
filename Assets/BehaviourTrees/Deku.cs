using UnityEngine;
using UnityEngine.AI;
using Home.BehaviourTrees;
using System.Collections.Generic;

public class Deku : MonoBehaviour
{
    /*
    public NavMeshAgent agent;
    public List<Transform> waypoints;
    private BehaviourTree tree;

    public GameObject treasure;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        tree = new BehaviourTree("Deku");
        Leaf isTreasurePresent = new Leaf("IsTreasurePResent", new Home.BehaviourTrees.Condition(() => treasure.activeSelf));
        Leaf moveToTreasure = new Leaf("MoveToTreasure", new ActionStrategy(() => agent.SetDestination(treasure.transform.position)));
        Sequence findTreasure = new Sequence("FindTreasure");
        findTreasure.AddChild(isTreasurePresent);
        findTreasure.AddChild(moveToTreasure);

        Selector baseSelector = new Selector("BaseSelector");
        baseSelector.AddChild(findTreasure);
        baseSelector.AddChild(new Leaf("Patrol", new PatrolStrategy(transform, agent, waypoints)));

        tree.AddChild(baseSelector);
    }

    void Update()
    {
        tree.Process();
    }
    */
}

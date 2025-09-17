using UnityEngine;
using System.Linq;

public class MoreBobMoreSPA : BobSPA
{
    public Transform[] safeSpots = new Transform[2];
    protected override void Start()
    {
        base.Start();
        actionScores.Add("Panic", 0);
    }

    protected override void Plan()
    {
        if (health / maxHealth <= criticalHealthLimit)
        {
            actionScores["Panic"] = 15f;
        }
        base.Plan();
    }

    protected override void Act()
    {
        if (chosenAction == "Panic")
        {
            Panic();
        }
        else
        {
            base.Act();
        }
    }

    private void Panic()
    {
        var foo = safeSpots.Aggregate((l, r) => Vector3.Distance(transform.position, l.position) < Vector3.Distance(transform.position, r.position) ? l : r).position;
        agentSmith.destination = foo;
    }
}

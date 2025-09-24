using UnityEngine;
using System.Linq;
using UnityEngine.AI;

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
        UpdatePrediction();
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

    protected override void Flee()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 targetPosition = transform.position + (fleeDirection * fleeLength);
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(targetPosition, out hit, 1, NavMesh.AllAreas))
        {
            SetSafeDestination(FindFleeAlternative(fleeDirection));
        }
        else
        {
            SetSafeDestination(targetPosition);
        }
    }

    #region FleeAlternative
    [Header("Flee Alternatives")]
    public float maxDistFromDir = 45f;
    public float step = 15f;
    public float fleeLength = 3f;
    private Vector3 FindFleeAlternative(Vector3 fleeDirection)
    {
        Vector3 bestPosition = transform.position;
        float maxDistanceToPlayer = 0f;

        for (float angle = -maxDistFromDir; angle <= maxDistFromDir; angle += step)
        {
            Vector3 dir = Quaternion.Euler(0, angle, 0) * fleeDirection;
            Vector3 candidate = transform.position + dir * fleeLength;

            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                float distToPlayer = Vector3.Distance(hit.position, player.position);
                if (distToPlayer > maxDistanceToPlayer)
                {
                    maxDistanceToPlayer = distToPlayer;
                    Debug.Log("Best position Found from " + transform.position + " = " + hit.position);
                    bestPosition = hit.position;
                }
            }
        }
        return bestPosition;
    }

    #endregion

    #region Predict
    Vector3 lastPlayerPos = new Vector3();
    Vector3 predictedPlayerPos = new Vector3();

    private void UpdatePrediction()
    {
        Vector3 currentPlayerPos = player.position;
        Vector3 movementDir = (currentPlayerPos - lastPlayerPos).normalized;

        float predictionDistance = distanceToPlayer * 0.5f;

        predictedPlayerPos = currentPlayerPos + movementDir * predictionDistance;

        lastPlayerPos = currentPlayerPos;
    }

    protected override void Chase()
    {
        SetSafeDestination(predictedPlayerPos);
    }
    #endregion
}

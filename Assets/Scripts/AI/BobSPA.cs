using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
using TMPro;

public class BobSPA : MonoBehaviour
{
    public float health = 50;
    protected float maxHealth = 50;

    public Transform player;

    protected float distanceToPlayer;
    public float fleeDistance = 4f;
    protected bool lineOfSight = false;

    protected Dictionary<string, float> actionScores;

    public Transform[] patrolPoints = new Transform[4];
    protected int patrolIndex = 0;
    public float distanceCheck = 1;

    protected NavMeshAgent agentSmith;

    public TextMeshProUGUI fleeText;
    public TextMeshProUGUI chaseText;
    public TextMeshProUGUI patrolText;

    public float criticalHealthLimit = 0.2f;

    protected string chosenAction; //TODO

    protected virtual void Start()
    {
        health = maxHealth;
        actionScores = new Dictionary<string, float>()
        {
            {"Flee", 0f },
            {"Chase", 0f },
            {"Patrol", 0f },
        };

        gameObject.TryGetComponent(out agentSmith);
    }

    protected virtual void Update()
    {
        Sense();
        Plan();
        Act();
    }

    protected virtual void Act()
    {
        switch (chosenAction)
        {
            //ACT
            case "Flee":
                Flee();
                break;
            case "Chase":
                Chase();
                break;
            case "Patrol":
                Patrol();
                break;
            default:
                break;
        }
    }

    protected virtual void Plan()
    {
        //PLAN
        float healthRatio = Mathf.Clamp01(health / maxHealth);
        float distanceRatio = Mathf.Clamp01(distanceToPlayer / fleeDistance);
        if (healthRatio <= criticalHealthLimit) { distanceRatio = 0; }

        float riskFactor = (1 - healthRatio) * (1 - distanceRatio);
        float aggroFactor = healthRatio * distanceRatio;

        float total = riskFactor + aggroFactor;
        riskFactor /= total;
        aggroFactor /= total;
        aggroFactor *= healthRatio > criticalHealthLimit ? 1 : 0;

        actionScores["Flee"] = riskFactor * 10 * (lineOfSight == true ? 1 : 0);
        fleeText.text = "FLEE = " + actionScores["Flee"];
        actionScores["Chase"] = aggroFactor * 10 * (lineOfSight == true ? 1 : 0);
        chaseText.text = "CHASE = " + actionScores["Chase"];
        actionScores["Patrol"] = 3f;
        patrolText.text = "PATROL = " + actionScores["Patrol"];

        chosenAction = actionScores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
    }

    protected virtual void Sense()
    {
        //SENSE
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Ray ray = new Ray(transform.position + (Vector3.up * 0.5f), player.position - transform.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            lineOfSight = hit.collider.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement _);
        }
        if (Vector3.Distance(patrolPoints[patrolIndex].position, transform.position) < distanceCheck)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    protected virtual void Flee()
    {
        Vector3 fleeDir = transform.position + (transform.position - player.position);
        agentSmith.SetDestination(fleeDir);
    }
    protected virtual void Chase()
    {
        agentSmith.SetDestination(player.position);
    }
    protected virtual void Patrol()
    {
        agentSmith.SetDestination(patrolPoints[patrolIndex].position);
    }
}

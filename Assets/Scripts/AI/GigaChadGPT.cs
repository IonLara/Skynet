using UnityEngine;
using UnityEngine.AI;

public class GigaChadGPT : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    private void Start()
    {
        gameObject.TryGetComponent(out agent);
    }

    private void Update()
    {
        agent.SetDestination(player.position);
    }
}

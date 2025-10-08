using UnityEngine;

[CreateAssetMenu(fileName = "CanSeePlayer", menuName = "FSM/Conditions/CanSeePlayer")]
public class CanSeePlayer : Condition
{
    public float viewDistance = 10f;
    private GameObject player;

    public override bool Check(StateMachine stateMachine)
    {
        if (player == null) { player = GameObject.FindGameObjectWithTag("Player"); }
        if (player == null) { return false; }

        return Vector3.Distance(stateMachine.transform.position, player.transform.position) <= viewDistance;
    }
}

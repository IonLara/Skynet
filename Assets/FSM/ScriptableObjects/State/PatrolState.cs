using UnityEngine;

[CreateAssetMenu(fileName = "PatrolState", menuName = "FSM/States/PatrolState")]
public class PatrolState : State
{
    public float speed = 2f;

    public override void UpdateState(StateMachine stateMachine)
    {
        stateMachine.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

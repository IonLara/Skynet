using UnityEngine;

public abstract class State : ScriptableObject
{
    public Transition[] transitions;

    public virtual void EnterState(StateMachine stateMachine) { }
    public virtual void UpdateState(StateMachine stateMachine) { }
    public virtual void ExitState(StateMachine stateMachine) { }

    public void CheckTransitions(StateMachine stateMachine)
    {
        foreach (Transition t in transitions)
        {
            if (t.condition != null && t.condition.Check(stateMachine))
            {
                stateMachine.ChangeState(t.targetState);
                break;
            }
        }
    }
}

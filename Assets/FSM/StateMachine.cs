using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State initialState;
    public State currentState;

    void Start()
    {
        ChangeState(initialState);
    }

    void Update()
    {
        if (currentState == null)
        {
            return;
        }

        currentState.UpdateState(this);
        currentState.CheckTransitions(this);
    }

    public void ChangeState(State newState)
    {
        if (newState == currentState || newState == null)
        {
            return;
        }

        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        currentState.EnterState(this);
    }
}

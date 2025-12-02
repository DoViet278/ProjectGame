using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public EntityState currentState { get; private set; }
    public bool canChangeState;

    public void Init(EntityState startState)
    {
        canChangeState = true;
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        if (!canChangeState || newState == currentState)
            return;
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void UpdateActiveState()
    {
        currentState.Update();
    }

    public void SwitchOffStateMachine() => canChangeState = false;
}

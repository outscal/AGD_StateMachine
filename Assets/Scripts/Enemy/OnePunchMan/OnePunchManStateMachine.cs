using StatePattern.Enemy;
using System.Collections.Generic;

public class OnePunchManStateMachine
{
    // Reference to the owner of the state machine
    private OnePunchManController Owner;

    // Dictionary to store available states, mapped to enum values
    protected Dictionary<OnePunchManStates, IState> States = new Dictionary<OnePunchManStates, IState>();

    public OnePunchManStateMachine(OnePunchManController Owner)
    {
        this.Owner = Owner;
        CreateStates();
        SetOwner();
    }

    // Create and initialize the states
    private void CreateStates()
    {
        States.Add(OnePunchManStates.IDLE, new IdleState(this));
        States.Add(OnePunchManStates.ROTATING, new RotatingState(this));
        States.Add(OnePunchManStates.SHOOTING, new ShootingState(this));
    }

    // Set the owner for each state
    private void SetOwner()
    {
        foreach (IState state in States.Values)
        {
            state.Owner = Owner;
        }
    }

    // Reference to the current state
    private IState currentState;

    // Update the current state (if it exists)
    public void Update() => currentState?.Update();

    // Change the current state to a new state
    protected void ChangeState(IState newState)
    {
        currentState?.OnStateExit(); // Exit the current state (if it exists)
        currentState = newState; // Set the new state
        currentState?.OnStateEnter(); // Enter the new state
    }

    public void ChangeState(OnePunchManStates newState) => ChangeState(States[newState]);
}
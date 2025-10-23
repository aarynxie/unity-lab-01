using UnityEngine;

public class StateController : MonoBehaviour
{
    public State startState;
    public State previousState;
    public State currentState;
    public State remainState;
    public bool transitionStateChanged = false;
    [HideInInspector] public float stateTimeElapsed;

    public bool isActive = true;

    public virtual void Start()
    {
        OnSetupState(); // setup when game starts
    }

    public virtual void OnSetupState()
    {
        if (currentState)
            currentState.DoSetupActions(this);
    }

    public virtual void OnExitState()
    {
        // reset time in this state
        stateTimeElapsed = 0;
        if (currentState)
            currentState.DoExitActions(this);
    }

    // for visual aid to indicate which state the object is currently at
    public virtual void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(this.transform.position, 1.0f);
        }
    }

    //Regular methods-------
    // strictly for transitions, not actions
    public void TransitionToState(State nextState)
    {
        if (nextState == remainState) return; // don't transition if we're in the "RemainInState" state

        // only contrinue to execute if nextState !remainState
        OnExitState(); // cast exit action if any

        // transition the state
        previousState = currentState;
        currentState = nextState;
        transitionStateChanged = true;

        OnSetupState(); // cast entry action if any
    }

    // default method to check if we've been in the state for long enough
    // assumes it'll be called once per frame
    // Time.deltaTime is the interval in seconds from the last frame to the current one
    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return stateTimeElapsed >= duration;
    }

    public void Update()
    {
        if (!isActive) return; // separate from gameObject active

        currentState.UpdateState(this);
    }

    public void Fire()
    {
        this.currentState.DoEventTriggeredActions(this, ActionType.Attack);
        // call the Act method of all registered event-triggered actions whose type matches ActionType.Attack
    }

}

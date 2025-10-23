using System;
using NUnit.Framework;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "PluggableSM/State")]
public class State : ScriptableObject
{
    public Action[] setupActions;
    public Action[] actions;
    public EventAction[] eventTriggeredActions;
    public Action[] exitActions;
    public Transition[] transitions;
    // for visualization at the Scene
    public Color sceneGizmoColor = Color.grey;

    // REGULAR METHODS------------
    // cannot be overriden

    // called at every frame update by statecontroller
    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    protected void DoActions(StateController controller) // called every frame update by UpdateState - useful for npcs
    {
        for (int i = 0; i < actions.Length; i++)
            actions[i].Act(controller);
    }
    public void DoSetupActions(StateController controller) // do exactly once, arriving at this state
    {
        for (int i = 0; i < setupActions.Length; i++) setupActions[i].Act(controller);
    }
    public void DoExitActions(StateController controller) // do exactly once, leaving this state
    {
        for (int i = 0; i < exitActions.Length; i++) exitActions[i].Act(controller);
    }

    public void DoEventTriggeredActions(StateController controller, ActionType type = ActionType.Default) // actions that are executed whenever the controller casts it
    {
        foreach (EventAction eventTriggeredAction in eventTriggeredActions)
        {
            if (eventTriggeredAction.type == type)
            {
                eventTriggeredAction.action.Act(controller);
            }
        }
    }

    protected void CheckTransitions(StateController controller) // goes through every transition listed under transitions, if gameObject has changed states we break
    {
        controller.transitionStateChanged = false; // reset
        for (int i = 0; i < transitions.Length; ++i)
        {
            // check if the previous transition has caused a change, if yes, stop. let update() in statecontroller run again in the next state
            if (controller.transitionStateChanged)
            {
                break;
            }
            bool decisionSucceeded = transitions[i].decision.Decide(controller);
            if (decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }

        }
    }
}

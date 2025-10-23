using UnityEngine;

// exact implementation on each action is different
public abstract class Action : ScriptableObject
{
    public abstract void Act(StateController controller);
}

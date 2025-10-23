using UnityEngine;

// implementation varies 
public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(StateController controller);
}

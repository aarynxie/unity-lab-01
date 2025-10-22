using UnityEngine;

public class Void { } // dummy class
// no arguments
[CreateAssetMenu(fileName = "SimpleGameEvent", menuName = "ScriptableObjects/SimpleGameEvent", order = 3)]
public class SimpleGameEvent : GameEvent<Void>
{
    // calls base' Raise with Void arg (no argument)
    public void Raise() => Raise(new Void());
}
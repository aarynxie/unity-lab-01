// a struct containing an Action and its type
// allow State to loop through each registered EventAction and cast an Action whose Type matches the user defined type for any particular event
public enum ActionType
{
    Attack = 0,
    Default = 1
}

[System.Serializable]
public struct EventAction
{
    public Action action;
    public ActionType type;
}
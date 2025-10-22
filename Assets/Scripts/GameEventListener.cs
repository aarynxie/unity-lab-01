using UnityEngine;
using UnityEngine.Events;

// never attach this to anything that might be disabled, attach it to a parent object instead
public class GameEventListener<T> : MonoBehaviour
{
    public GameEvent<T> Event;
    public UnityEvent<T> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    // this is also called when object is destroyed and can be used for any cleanup code
    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(T data)
    {
        Response.Invoke(data);
    }
}

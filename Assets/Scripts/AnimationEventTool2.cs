using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTool2 : MonoBehaviour
{
    public UnityEvent useEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    public void TriggerAnimationEvent()
    {
        useEvent.Invoke();
    }
}
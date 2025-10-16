using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTool : MonoBehaviour
{
    public UnityEvent useEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.gameOver.AddListener(TriggerAnimationEvent);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerAnimationEvent()
    {
        useEvent.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTool : MonoBehaviour
{
    //public UnityEvent useEvent;
    public UnityEvent gameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerAnimationEvent()
    {
        Debug.Log("AnimationEventTool triggering gameover");
        //GameManager.instance.gameOver.Invoke();
        gameOver.Invoke();
    }
}

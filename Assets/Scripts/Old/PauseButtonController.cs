using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
    private bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;
    private Image image;

    public AudioSource bgm;

    public UnityEvent gamePaused;
    public UnityEvent gameResumed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClick()
    {
        Time.timeScale = isPaused ? 1.0f : 0.0f;
        isPaused = !isPaused;
        if (isPaused)
        {
            image.sprite = playIcon;
            if (bgm.isPlaying) bgm.Pause();
            gameResumed.Invoke();
        }
        else
        {
            image.sprite = pauseIcon;
            if (!bgm.isPlaying) bgm.UnPause();
            gamePaused.Invoke();
        }
    }
}

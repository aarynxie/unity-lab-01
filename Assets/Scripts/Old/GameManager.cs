using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IPowerupApplicable
{
    public UnityEvent gameStart;

    //private int score = 0;
    public UnityEvent updateScore;

    public IntVariable gameScore;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // public override void Awake()
    // {
    //     base.Awake();
    //     DontDestroyOnLoad(this.gameObject);
    // }

    IEnumerator Start()
    {
        yield return null;
        gameStart.Invoke();
        Time.timeScale = 1.0f;
        SetScore();

        // subscribe to scene manager scene change 
        SceneManager.activeSceneChanged += SceneSetup;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        gameScore.Value = 0;
        SetScore();
        // gameRestart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(increment);
        SetScore();
    }

    public void SetScore()
    {
        updateScore.Invoke();
        // scoreChange.Invoke(score);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        // gameOver.Invoke();
    }

    public void UpdateScore()
    {
        updateScore.Invoke();
    }

    public void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore();
    }

    public void RequestPowerupEffect(IPowerup i)
    {
        // Debug.Log("GameManager requesting powerup effect");
        // increase score
        i.ApplyPowerup(this);
    }

    public void PauseGame()
    {
        //todo: pause game
    }

    public void ResumeGame()
    {
        //todo:resume game
    }
}

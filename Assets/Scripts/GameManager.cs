using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;

    private int score = 0;

    // spawn goomba 
    public GameObject goombaPrefab;
    private GameObject currentGoomba;
    private Vector3 goombaSpawnPos = new Vector3(1.56F, -2.4F, 0);
    public Transform enemyParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
        SpawnEnemy(goombaPrefab, goombaSpawnPos);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        score = 0;
        SetScore(score);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;

        if (currentGoomba != null)
        {
            Destroy(currentGoomba);
        }
        Debug.Log("game restart");
        SpawnEnemy(goombaPrefab, goombaSpawnPos);
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        SetScore(score);
        //Debug.Log(score);
    }

    public void SetScore(int score)
    {
        //Debug.Log($"setting score as: {score}");
        scoreChange.Invoke(score);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }

    void SpawnEnemy(GameObject prefab, Vector3 position)
    {
        Debug.Log("spawning goomba");
        currentGoomba = Instantiate(prefab, position, Quaternion.identity, enemyParent);
    }
}

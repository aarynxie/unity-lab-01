using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject goombaPrefab;
    private GameObject currentGoomba;
    private Vector3 goombaSpawnPos = new Vector3(1.56F, -2.4F, 0);
    private Transform enemyParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyParent = GameObject.Find("Enemies").transform;
        //goombaPrefab = GameObject.Find("Goomba");
        SpawnEnemy(goombaPrefab, goombaSpawnPos);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        if (currentGoomba != null)
        {
            Destroy(currentGoomba);
        }
        SpawnEnemy(goombaPrefab, goombaSpawnPos);

        foreach (Transform child in transform)
        {
            child.GetComponent<EnemyMovement>().GameRestart();
        }
    }

    void SpawnEnemy(GameObject prefab, Vector3 position)
    {
        currentGoomba = Instantiate(prefab, position, Quaternion.identity, enemyParent);
        currentGoomba.SetActive(true);
    }
}

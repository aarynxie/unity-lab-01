using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstaints", order = 1)]
public class GameConstants : ScriptableObject
{
    //lives
    public int maxLives;
    // mario movement
    public int speed;
    public int maxSpeed;
    public int upSpeed;
    public int deathImpulse;
    public Vector3 marioStartingPosition;
    // goomba movement
    public float goombaPatrolTime;
    public float goombaMaxOffset;

    public int flickerInterval;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

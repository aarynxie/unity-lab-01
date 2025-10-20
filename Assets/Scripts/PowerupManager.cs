using UnityEngine;
using UnityEngine.Events;


public class PowerupManager : Singleton<PowerupManager>
{
    public UnityEvent<BasePowerup> powerupCollected;
    public override void Awake()
    {
        base.Awake();
        powerupCollected.AddListener(PowerupCollected);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PowerupCollected(BasePowerup powerup)
    {
        powerup.ApplyPowerup(GameManager.instance);
        // logic statements
        // switch (powerup.powerupType)
        // {
        //     case PowerupType.Coin:
        //         Debug.Log("coin");
        //         GameManager.instance.IncreaseScore(1);
        //         break;
        // }
    }
}

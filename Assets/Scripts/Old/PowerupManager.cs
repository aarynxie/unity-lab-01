using UnityEngine;
using UnityEngine.Events;


public class PowerupManager : MonoBehaviour
{

    public UnityEvent<IPowerup> powerupAffectsManager;

    public UnityEvent<IPowerup> powerupAffectsPlayer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FilterAndCastPowerup(IPowerup powerup)
    {
        //filter powerup;  
        switch (powerup.powerupType)
        {
            case PowerupType.Coin:
                Debug.Log("PowerupManager FilterAndCastPowerup coin");
                //GameManager.instance.IncreaseScore(1);
                powerupAffectsManager.Invoke(powerup);
                break;
            case PowerupType.MagicMushroom:
            case PowerupType.FireFlower:
            case PowerupType.OneUpMushroom:
            case PowerupType.StarMan:
                Debug.Log("PowerupManager FilterAndCastPowerup not coin");
                powerupAffectsPlayer.Invoke(powerup);
                break;
        }


    }
}

using UnityEngine;
using UnityEngine.Events;

public class CoinPowerup : BasePowerup
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        this.type = PowerupType.Coin; // set to coin enum type
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void SpawnPowerup()
    {
        Debug.Log("coin spawned");
        spawned = true;
        // play the sound
        AudioSource source = this.GetComponent<AudioSource>();
        source.PlayOneShot(source.clip);
        // invoke PowerupCollected event
        PowerupManager.instance.powerupCollected.Invoke(this);
    }

    // hide the base destroy method
    public new void DestroyPowerup()
    {

    }

    public override void ApplyPowerup(MonoBehaviour i)
    {
        Debug.Log("CoinPowerup running AppyPowerup");
        GameManager manager;
        bool result = i.TryGetComponent<GameManager>(out manager);

        if (result)
        {
            manager.IncreaseScore(1);
            Debug.Log("CoinPowerup increasing score");
        }
    }
}

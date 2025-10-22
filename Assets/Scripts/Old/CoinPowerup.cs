using UnityEngine;
using UnityEngine.Events;

public class CoinPowerup : BasePowerup
{
    public UnityEvent<IPowerup> powerupCollected;
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
        spawned = true;
        // play the sound
        AudioSource source = this.GetComponentInChildren<AudioSource>();
        source.PlayOneShot(source.clip);
        // invoke PowerupCollected event
        Debug.Log("Coin powerup invoking powerupcollected");
        powerupCollected.Invoke(this);
    }

    // hide the base destroy method
    public new void DestroyPowerup()
    {

    }

    public override void ApplyPowerup(MonoBehaviour i)
    {
        GameManager manager;
        bool result = i.TryGetComponent<GameManager>(out manager);

        if (result)
        {
            Debug.Log("Coin powerup asking manager to increase score by 1");
            manager.IncreaseScore(1);
        }
    }

    public override void GameRestart()
    {
        base.GameRestart();
        this.GetComponentInChildren<Animator>().Play("default");
    }
}

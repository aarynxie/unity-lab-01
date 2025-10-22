using UnityEngine;
using UnityEngine.Events;


public abstract class BasePowerup : MonoBehaviour, IPowerup
{
    public PowerupType type;
    public bool spawned = false;
    protected bool consumed = false;
    protected bool goRight = true;
    protected Rigidbody2D rigidBody;

    [Header("SO Event References")]
    public GameEvent<IPowerup> onPowerupCollected;

    // base methods
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //GameManager.instance.gameRestart.AddListener(GameRestart);
    }

    // interface methods
    // 1. concrete methods
    public PowerupType powerupType
    {
        get
        {
            return type;
        }
    }

    public bool hasSpawned
    {
        get
        {
            return spawned;
        }
    }

    public void DestroyPowerup()
    {
        //Destroy(this.gameObject);
        //this.gameObject.SetActive(false);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Mario") && spawned)
        {
            Debug.Log("BasePowerup Mario collided with a basepowerup and raising onPowerupCollected");
            onPowerupCollected.Raise(this);


            // then destroy powerup
            DestroyPowerup();
        }
        else if (col.gameObject.layer == 10) // else if hitting pipe, flip travel direction
        {
            // remember to set pipe to layer 10
            if (spawned)
            {
                goRight = !goRight;
                rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);
            }
        }
    }

    public virtual void GameRestart()
    {
        spawned = false;
        goRight = true;
        //this.gameObject.SetActive(true);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    // 2. abstract methods, to be implemented by derived classes
    public abstract void SpawnPowerup();
    public abstract void ApplyPowerup(MonoBehaviour i);
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagicMushroomPowerup : BasePowerup
{
    // set up this object's type

    //instantiate variables
    protected override void Start()
    {
        base.Start();
        this.type = PowerupType.MagicMushroom;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Mario") && spawned)
        {
            // do something when colliding with player

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

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse);
    }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // do something with the object
    }
}

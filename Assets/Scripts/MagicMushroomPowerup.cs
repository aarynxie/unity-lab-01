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

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse);
    }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        Debug.Log("MagicMushroomPowerup running AppyPowerup");
        // do something with the object
    }
}

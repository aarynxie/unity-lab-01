using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MagicMushroomPowerup : BasePowerup
{
    public Animator mushroomAnimator;
    private Vector3 startingPos = new Vector3(0f, 0.999f, 0f);
    // set up this object's type

    //instantiate variables
    protected override void Start()
    {
        base.Start();
        this.type = PowerupType.MagicMushroom;
        GameManager.instance.gameRestart.AddListener(GameRestart);
        mushroomAnimator = GetComponentInChildren<Animator>();
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        //rigidBody.typ
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<Collider2D>().enabled = true;

        StartCoroutine(WaitNextFrame());
    }

    IEnumerator WaitNextFrame()
    {
        // suspend execution for 5 seconds
        yield return new WaitForEndOfFrame();
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse);
    }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        Debug.Log("MagicMushroomPowerup running AppyPowerup");
        // do something with the object
    }

    public override void GameRestart()
    {
        base.GameRestart();
        // reset position of rigid body
        rigidBody.bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.transform.localPosition = startingPos;
        mushroomAnimator.Play("Mushroom-idle");
        //gameObject.GetComponent<Anim
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BrickPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup;
    public bool isBreakable = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Mario")
        {
            if (!powerup.hasSpawned)
            {
                // enable sprite
                this.GetComponent<SpriteRenderer>().enabled = true;
                // set bounce trigger for animator
                this.GetComponent<Animator>().SetTrigger("bounce");
                // spawn powerup through the powerup's animator
                powerupAnimator.SetTrigger("spawned");

                if (!isBreakable)
                {
                    // show disabled sprite by setting trigger to spawned and changing the animation state
                    this.GetComponent<Animator>().SetTrigger("spawned");
                }

            }
            else
            {
                if (isBreakable)
                {
                    // else if it's the breakable brick type, can still bounce even though powerup has spawned
                    this.GetComponent<Animator>().SetTrigger("bounce");
                }
            }
        }
    }

    // disable not necessary here because we use animation for brick bounce, not spring
    // but put here to conform to interface 
    public void Disable()
    {

    }
}

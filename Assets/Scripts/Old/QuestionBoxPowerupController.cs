using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup;


    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("QuestionBoxPowerup Controller collided with somethig");
        if (other.gameObject.tag == "Mario" && !powerup.hasSpawned)
        {
            Debug.Log("QuestionBoxPowerup Controller collided with mario & powerup has not spawned");
            this.GetComponent<Animator>().SetTrigger("spawned");
            powerupAnimator.SetTrigger("spawned");
            Debug.Log("setting trigger");
            AudioSource source = this.GetComponent<AudioSource>();
            source.PlayOneShot(source.clip);
        }
    }

    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void GameRestart()
    {
        Debug.Log("Questionbox restart called");
        powerupAnimator.ResetTrigger("spawned");
        this.GetComponent<Animator>().Play("idle");
    }
}

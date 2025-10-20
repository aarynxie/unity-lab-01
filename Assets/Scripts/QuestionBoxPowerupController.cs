using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Mario" && !powerup.hasSpawned)
        {
            this.GetComponent<Animator>().SetTrigger("spawned");
            powerupAnimator.SetTrigger("spawned");
        }
    }

    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void GameRestart()
    {
        this.GetComponent<Animator>().Play("idle");
    }
}

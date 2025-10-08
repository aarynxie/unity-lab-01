using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Build.Content;
using System.Xml.Schema;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D marioBody;

    public float maxSpeed = 20;

    public float upSpeed = 10;
    private bool onGroundState = true;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    //reset game variables
    public GameObject enemies;

    public Animator marioAnimator;

    public AudioSource marioAudio;

    public AudioClip marioDeath;
    public float deathImpulse = 15;

    [System.NonSerialized]
    public bool alive = true;

    public Transform gameCamera;

    int collisionLayerMask = (1 << 3) | (1 << 7) | (1 << 8);

    private bool moving = false;

    private bool jumpedState = false;

    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        marioAnimator.SetBool("onGround", onGroundState);
    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));
    }

    void FlipMarioSprite(int value)
    {
        // flip mario sprite based on direction
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.linearVelocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");
        }

        if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.linearVelocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Enemies") || col.gameObject.CompareTag("Enemies")) && !onGroundState)
        // {
        //     onGroundState = true;
        //     marioAnimator.SetBool("onGround", onGroundState);
        // }

        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }

        // coin block collision
        if (col.gameObject.CompareTag("Coin"))
        {

            // if mario hits block tagged with coin
            // trigger coin animation
            //turn rigid body to static 
            // only if mario is going up
            // if (marioBody.linearVelocity.y > 0.1f)

            // blockBody.bodyType = RigidbodyType2D.Static;
            // Debug.Log("mario hit block");
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            Debug.Log("collided with goomba!");

            marioAnimator.Play("mario-die");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
        }
    }

    void FixedUpdate()
    {
        // only run physics code if mario is alive
        if (alive && moving)
        {

            Move(faceRightState == true ? 1 : -1);
        }
    }

    // restart game
    public void RestartButtonCallback(int input)
    {
        //Debug.Log("Restart!");
        ResetGame();
        Time.timeScale = 1.0f;
    }

    private void ResetGame()
    {

        // // reset score
        // scoreText.text = "Score: 0";

        // //Debug.Log("ResetGame is called");
        // foreach (Transform eachChild in enemies.transform)
        // {
        //     eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        // }

        // jumpOverGoomba.score = 0;

        // // reset restart screen
        // gameOverUi.transform.localPosition = new Vector3(0.0f, -1563.0f, 0.0f);
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    // void GameOverScene()
    // {
    //     Time.timeScale = 0.0f;
    //     GameOver();
    // }

    // void GameOver()
    // {

    //     // draw gameover screen
    //     gameOverUi.transform.localPosition = new Vector3(8.5f, 295.0f, 0.0f);
    // }

    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);

        if (marioBody.linearVelocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;

            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;
        }
    }

    public void GameRestart()
    {
        // reset pos
        marioBody.transform.position = new Vector3(-3.04f, 0.0f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset cam pos
        gameCamera.position = new Vector3(0, 0, -10);
    }
}

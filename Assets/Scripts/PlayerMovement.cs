using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Build.Content;

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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextGameOver;
    public GameObject enemies;

    public JumpOverGoomba jumpOverGoomba;

    public GameObject gameOverUi;

    public Animator marioAnimator;

    public AudioSource marioAudio;

    public AudioClip marioDeath;
    public float deathImpulse = 15;

    [System.NonSerialized]
    public bool alive = true;

    public Transform gameCamera;

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
        // flip mario sprite based on direction
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.linearVelocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.linearVelocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") && !onGroundState)
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
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
        if (alive)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);

                if (marioBody.linearVelocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
            }

            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                marioBody.linearVelocity = Vector2.zero;
            }

            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;

                marioAnimator.SetBool("onGround", onGroundState);
            }
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
        // reset pos
        marioBody.transform.position = new Vector3(-3.04f, 0.0f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // reset score
        scoreText.text = "Score: 0";
        scoreTextGameOver.text = "Score: 0";
        // reset Goomba

        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        gameCamera.position = new Vector3(0, 0, -10);

        //Debug.Log("ResetGame is called");
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }

        jumpOverGoomba.score = 0;

        // reset restart screen
        gameOverUi.transform.localPosition = new Vector3(0.0f, -1563.0f, 0.0f);
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void GameOverScene()
    {
        Time.timeScale = 0.0f;
        GameOver();
    }

    void GameOver()
    {

        // draw gameover screen
        gameOverUi.transform.localPosition = new Vector3(8.5f, 295.0f, 0.0f);
    }
}

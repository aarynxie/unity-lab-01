using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Build.Content;
using System.Xml.Schema;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerMovement : Singleton<PlayerMovement>
{
    public GameConstants gameConstants;

    float deathImpulse;
    float speed;
    float maxSpeed;
    float upSpeed;

    private Rigidbody2D marioBody;
    private bool onGroundState = true;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    //reset game variables

    public Animator marioAnimator;

    public AudioSource marioAudio;
    public AudioSource marioDeathAudio;


    [System.NonSerialized]
    public bool alive = true;

    public Transform gameCamera;

    int collisionLayerMask = (1 << 3) | (1 << 7) | (1 << 8);

    private bool moving = false;

    private bool jumpedState = false;

    override public void Awake()
    {
        base.Awake();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        deathImpulse = gameConstants.deathImpulse;
        upSpeed = gameConstants.upSpeed;

        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        marioAnimator.SetBool("onGround", onGroundState);

        //SceneManager.activeSceneChanged += SetStartingPosition;
    }
    // public void SetStartingPosition(Scene current, Scene next)
    // {
    //     if (next.name == "world2")
    //     {
    //         this.transform.position = new Vector3(-6.15f, -2.35f, 0f);
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));
    }

    // flip mario sprite based on direction
    void FlipMarioSprite(int value)
    {
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
        // if the thing mario is colliding with is on one of the layers defined in collisionLayerMask & mario is not on the ground, set onGroundState to true + update animation
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if mario collides with goomba, mario dies
        // don't trigger this if goomba is not alive
        if (other.gameObject.CompareTag("Enemy") && alive && other.gameObject.activeSelf)
        {
            Debug.Log("PlayerMovement collided with goomba!");

            marioAnimator.Play("mario-die");
            marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
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

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    // moves mario if mario has not exceeded max speed 
    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);

        if (marioBody.linearVelocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    // changes the value of moving & moves mario
    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            // moves mario
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    // what happens if ActionManager calls the jump
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
        // reset marios position 
        marioBody.transform.position = new Vector3(-3.04f, 0.0f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset cam pos
        gameCamera.position = new Vector3(0, 0, -10);
    }

    public void RequestPowerupEffect()
    {
        //todo: request powerup
    }
}

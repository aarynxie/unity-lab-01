using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Build.Content;
using System.Xml.Schema;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UIElements;
using System.Linq.Expressions;

public class PlayerMovement : MonoBehaviour
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

    public BoolVariable marioFaceRight;
    public UnityEvent damagePlayer;

    public AudioSource powerupSound;

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
        if (alive)
            marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));
    }

    // flip mario sprite based on direction
    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            updateMarioShouldFaceRight(false);
            marioSprite.flipX = true;
            if (marioBody.linearVelocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");
        }

        if (value == 1 && !faceRightState)
        {
            updateMarioShouldFaceRight(true);
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
        // TODO: change this to onplayerdamage instead
        if (other.gameObject.CompareTag("Enemy") && alive && other.gameObject.activeSelf)
        {
            Debug.Log("PlayerMovement collided with goomba!");
            damagePlayer.Invoke();
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
        if (!marioDeathAudio.isPlaying) marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
        alive = false;
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
        updateMarioShouldFaceRight(true);
        marioSprite.flipX = false;
        onGroundState = true;
        marioAnimator.SetBool("onGround", onGroundState);

        marioAnimator.SetTrigger("gameRestart");
        //marioAnimator.Play("mario-idle");
        alive = true;

        // reset cam pos
        gameCamera.position = new Vector3(0, 0, -10);
    }

    public void RequestPowerupEffect(IPowerup i)
    {
        Debug.Log("PlayerMovement requestpowerupeffect");
        //todo: request powerup
        // pass this to magic mushroom via ApplyPowerup
        i.ApplyPowerup(this);

    }

    private void updateMarioShouldFaceRight(bool value)
    {
        faceRightState = value;
        marioFaceRight.SetValue(faceRightState);
    }

    // called when OnDamagePlayer is invoked
    public void DamageMario()
    {
        // pass this to stateController to see if Mario should die
        // can cross refer to mariostatecontroller because they're on the same gamobject
        GetComponent<MarioStateController>().SetPowerup(PowerupType.Damage);
    }

    void PlayPowerupSound()
    {
        powerupSound.PlayOneShot(powerupSound.clip);
    }
}

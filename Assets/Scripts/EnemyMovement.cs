using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);

    public bool dead = false;

    public AudioSource goombaSound;

    GameManager gameManager;

    public Animator GoombaAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();

        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();

        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void Movegoomba()
    {
        if (!dead)
            enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {
            Movegoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }
    }

    public void GameRestart()
    {
        dead = false;
        gameObject.SetActive(true);
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();

        GoombaAnimator.SetTrigger("gameRestart");
    }

    // call this method when head trigger is hit
    public void GoombaDie()
    {
        if (!dead)
        {
            dead = true;


            // play death animation & sound here
            gameManager.IncreaseScore(1);
            // move rb down
            // Animator anim = GetComponent<Animator>();
            // float offset = -0.3F;
            // Debug.Log($"offset:{offset}");
            // Vector2 currentPos = enemyBody.position;
            // float newY = currentPos.y + velocity.y * Time.fixedDeltaTime + offset;
            // Vector2 targetPos = new Vector2(currentPos.x, newY);
            // enemyBody.MovePosition(targetPos);
            GoombaAnimator.Play("goomba-die");


            // destroy gameObject after 1 sec
            //Destroy(gameObject, 1f);
            StartCoroutine(waitOneSec());


        }

        IEnumerator waitOneSec()
        {
            // suspend execution for 1 second
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }

    void PlayGoombaSound()
    {
        goombaSound.PlayOneShot(goombaSound.clip);
    }
}

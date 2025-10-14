using UnityEngine;

public class GoombaHeadTrigger : MonoBehaviour
{
    private EnemyMovement goomba;

    private PlayerMovement player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        goomba = GetComponentInParent<EnemyMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit goomba head");
        // don't trigger this if dead
        Debug.Log(player.alive);
        if (other.CompareTag("Mario") && player.alive)
        {
            goomba.GoombaDie();

            // bounce player upwards
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            // don't do this if mario is dead
            if (rb)
            {
                rb.linearVelocityY = 0;
                rb.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
            }
        }
    }
}

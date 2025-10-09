using UnityEngine;

public class GoombaHeadTrigger : MonoBehaviour
{
    private EnemyMovement goomba;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        if (other.CompareTag("Mario"))
        {
            goomba.GoombaDie();

            // bounce player upwards
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.linearVelocityY = 0;
                rb.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
            }
        }
    }
}

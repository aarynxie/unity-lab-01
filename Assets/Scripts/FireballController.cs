using System.Collections;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private float scaleSpeed = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ScaleAndDestroyCoroutine());
        GetComponent<AudioSource>().Play();
    }

    private IEnumerator ScaleAndDestroyCoroutine()
    {
        // wait for 2 seconds
        yield return new WaitForSecondsRealtime(2);
        // gradually scale down the gameobject
        while (transform.localScale.x > 0.01f)
        {
            transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
            yield return null;
        }

        // ensur the gameobject is completely scaled down
        transform.localScale = Vector3.zero;

        // destory the gameobject
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // destory self
            Destroy(gameObject);
        }
    }
}

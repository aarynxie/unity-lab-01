using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPopOut : MonoBehaviour
{
    //public Rigidbody2D blockBody;
    public Animator blockAnimator;
    public Animator coinAnimator;

    public Rigidbody2D blockBody;



    public bool isBrick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Mario"))
        {
            if (!blockAnimator.GetBool("GotCoin"))
            {
                if (coinAnimator != null)
                    coinAnimator.SetTrigger("GotCoin");
            }
            blockAnimator.SetBool("GotCoin", true);
            if (isBrick)
            {
                blockAnimator.SetTrigger("Bounce");
                //Debug.Log("CoinPopOut setting trigger to bounce");
            }



            // if mario hits block tagged with coin
            // trigger coin animation
            //turn rigid body to static
            //lockBody.bodyType = RigidbodyType2D.Static;

            StartCoroutine(waitOneSec());
        }

        IEnumerator waitOneSec()
        {
            // suspend execution for 5 seconds
            yield return new WaitForSeconds(0.5f);
            blockBody.bodyType = RigidbodyType2D.Static;
        }

    }
}

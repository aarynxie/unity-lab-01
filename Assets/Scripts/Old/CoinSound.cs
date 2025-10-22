using UnityEngine;

public class CoinSound : MonoBehaviour
{

    public AudioSource coinAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayCoinSound()
    {
        coinAudio.PlayOneShot(coinAudio.clip);
    }
}

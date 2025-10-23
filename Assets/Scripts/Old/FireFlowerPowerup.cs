using UnityEngine;
using System.Collections;

public class FireFlowerPowerup : BasePowerup
{
    public Animator fireFlowerAnimator;
    private Vector3 startingPos = new Vector3(0, 1, 0);

    [SerializeField] private GameObject fireFlowerChild;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        this.type = PowerupType.FireFlower;
        fireFlowerAnimator = GetComponentInChildren<Animator>();
    }

    public override void SpawnPowerup()
    {
        Debug.Log("FireFlowerPowerup spawned powerup");
        spawned = true;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<Collider2D>().enabled = true;

        // waiting for next frame because cannot set rb body type to dynamci and addforce in the same frame
        StartCoroutine(WaitNextFrame());

    }

    IEnumerator WaitNextFrame()
    {
        yield return new WaitForEndOfFrame();
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse);
    }

    //implement interface
    public override void ApplyPowerup(MonoBehaviour i)
    {
        Debug.Log("FlowerFlowerPowerup running applypowerup");
        MarioStateController mario;
        bool result = i.TryGetComponent<MarioStateController>(out mario);
        if (result)
        {
            mario.SetPowerup(this.powerupType);
        }
    }

    public override void GameRestart()
    {
        base.GameRestart();
        // reset
        rigidBody.bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<Collider2D>().enabled = false;
        fireFlowerChild.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localPosition = startingPos;
        fireFlowerAnimator.Play("Mushroom-idle"); // TODO change this animation later
    }
}

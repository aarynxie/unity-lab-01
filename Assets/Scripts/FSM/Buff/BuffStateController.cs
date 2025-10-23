using UnityEngine;
using System;
using System.Collections;

public class BuffStateController : StateController
{
    public PowerupType currentPowerupTypeBuff = PowerupType.Default;
    public MarioState shouldBeNextStateBuff = MarioState.Default;

    private SpriteRenderer spriteRenderer;
    public GameConstants gameConstants;


    public override void Start()
    {
        base.Start();
        GameRestart();
    }
    public void GameRestart()
    {
        // restart the game
        currentPowerupTypeBuff = PowerupType.Default;
    }

    public void SetBuff(PowerupType i)
    {
        currentPowerupTypeBuff = i;
    }

    public void SetRendererToFlicker()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkSpriteRenderer());
    }

    IEnumerator BlinkSpriteRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        while (string.Equals(currentState.name, "Invincible", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("running blinking");
            // toggle the visibility of the sprite renderer
            spriteRenderer.enabled = !spriteRenderer.enabled;

            // wait for the specified blink interval
            yield return new WaitForSeconds(gameConstants.flickerInterval);
        }

        spriteRenderer.enabled = true;
    }


}

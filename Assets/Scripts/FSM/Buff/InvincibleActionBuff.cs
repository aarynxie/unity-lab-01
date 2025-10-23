using UnityEngine;

// SO cannot implement coroutines, so we'll use mariostatecontroller to help
[CreateAssetMenu(menuName = "PluggableSM/Actions/SetupInvncibilityBuff")]
public class InvincibleActionBuff : Action
{
    public AudioClip invincibilityStart;
    public override void Act(StateController controller)
    {
        Debug.Log("InvincibleActionBuff runing");
        BuffStateController b = (BuffStateController)controller;
        b.gameObject.GetComponent<AudioSource>().PlayOneShot(invincibilityStart); // TODO: set this up
        b.SetRendererToFlicker();
    }
}

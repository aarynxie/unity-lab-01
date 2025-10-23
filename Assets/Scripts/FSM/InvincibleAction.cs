using UnityEngine;

// SO cannot implement coroutines, so we'll use mariostatecontroller to help
[CreateAssetMenu(menuName = "PluggableSM/Actions/SetupInvncibility")]
public class InvincibleAction : Action
{
    public AudioClip invincibilityStart;
    public override void Act(StateController controller)
    {
        MarioStateController m = (MarioStateController)controller;
        m.gameObject.GetComponent<AudioSource>().PlayOneShot(invincibilityStart); // TODO: set this up
        m.SetRendererToFlicker();
    }
}

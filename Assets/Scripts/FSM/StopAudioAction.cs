using UnityEngine;

// SO cannot implement coroutines, so we'll use mariostatecontroller to help
[CreateAssetMenu(menuName = "PluggableSM/Actions/StopAudioAction")]
public class StopAudioAction : Action
{
    //public AudioClip invincibilityStart;
    public override void Act(StateController controller)
    {
        Debug.Log("stopping audio");
        BuffStateController m = (BuffStateController)controller;
        m.gameObject.GetComponent<AudioSource>().Stop();
    }
}

//gameObject.GetComponent<AudioSource>().Stop();

using UnityEngine;
// check if we've been in a state longer than a stipulated buffduration by using checkifcountdownelapsed
[CreateAssetMenu(menuName = "PluggableSM/Decisions/Countdown")]
public class CountdownDecision : Decision
{
    public float buffDuration;
    public override bool Decide(StateController controller)
    {
        return controller.CheckIfCountDownElapsed(buffDuration);
    }
}

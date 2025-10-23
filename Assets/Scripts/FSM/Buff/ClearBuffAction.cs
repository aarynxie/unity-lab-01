using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/ClearBuff")]
public class ClearBuffAction : Action
{
    public override void Act(StateController controller)
    {
        BuffStateController m = (BuffStateController)controller;
        m.currentPowerupTypeBuff = PowerupType.Default;
    }
}

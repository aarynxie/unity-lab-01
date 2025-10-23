using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/TransformBuff")]
public class TransformDecisionBuff : Decision
{
    public StateTransformMapBuff[] map;
    public override bool Decide(StateController controller)
    {
        BuffStateController b = (BuffStateController)controller;
        // assume the same is named after one of the posisble values in MarioState
        // convert current state name into the MarioState enum using custom class EnumExtension
        MarioState toCompareState = EnumExtension.ParseEnum<MarioState>(b.currentState.name);

        // loop through state transform and see if it matches the current transformation we are looking for
        for (int i = 0; i < map.Length; i++)
        {
            if (b.currentPowerupTypeBuff == map[i].powerupCollected)
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public struct StateTransformMapBuff
{
    public PowerupType powerupCollected;
}
